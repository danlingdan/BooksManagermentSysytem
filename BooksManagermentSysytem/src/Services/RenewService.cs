using System;
using System.Data;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Models;

namespace BooksManagermentSysytem.Services
{
    /// <summary>
    /// 图书续借服务
    /// </summary>
    public class RenewService
    {
        /// <summary>
        /// 校验续借资格
        /// </summary>
        /// <param name="bookborrowId">借阅明细ID</param>
        /// <param name="errorMessage">错误信息</param>
        /// <returns>是否可以续借</returns>
        public static bool ValidateRenewEligibility(long bookborrowId, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                // 查询借阅记录详细信息
                string sql = @"
                    SELECT bb.bookborrow_id, bb.cardID, bb.bookID, bb.borrowdate, bb.overdate,
                           bb.renew_count, bb.last_renew_time,
                           r.readername, r.readertype,
                           bi.bibliography_id, bib.bibliography_name, bib.ISBN,
                           COALESCE(bi.price, bib.price, 0) AS book_price
                    FROM bookborrow bb
                    INNER JOIN reader r ON bb.cardID = r.cardID
                    INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                    INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                    WHERE bb.bookborrow_id = @bookborrowId";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@bookborrowId", bookborrowId));

                if (dt.Rows.Count == 0)
                {
                    errorMessage = "未找到该借阅记录";
                    return false;
                }

                DataRow row = dt.Rows[0];

                // 检查是否已归还
                if (row["overdate"] != DBNull.Value)
                {
                    errorMessage = "该书籍已归还，无法续借";
                    return false;
                }

                // 检查是否逾期
                DateTime borrowDate = row["last_renew_time"] != DBNull.Value ? 
                    Convert.ToDateTime(row["last_renew_time"]) : 
                    Convert.ToDateTime(row["borrowdate"]);
                string readerType = row["readertype"].ToString();
                
                if (BorrowRules.IsOverdue(borrowDate, readerType))
                {
                    int overdueDays = BorrowRules.CalculateOverdueDays(borrowDate, readerType);
                    errorMessage = $"该书籍已逾期{overdueDays}天，请先归还后再借阅";
                    return false;
                }

                // 检查续借次数限制
                int renewCount = row["renew_count"] != DBNull.Value ? 
                    Convert.ToInt32(row["renew_count"]) : 0;
                
                BorrowRule rule = BorrowRules.GetRuleByReaderType(readerType);
                if (renewCount >= rule.MaxRenewCount)
                {
                    errorMessage = $"该书籍已续借{renewCount}次，已达到最大续借次数限制（{rule.MaxRenewCount}次）";
                    return false;
                }

                // 检查是否有人预约此书
                string checkReservationSql = @"
                    SELECT COUNT(*) 
                    FROM book_reservation 
                    WHERE bookID = @bookID 
                      AND reservation_status = N'PENDING'";

                int reservationCount = Convert.ToInt32(DatabaseHelper.ExecuteScalar(
                    checkReservationSql,
                    DatabaseHelper.CreateParameter("@bookID", row["bookID"].ToString())));

                if (reservationCount > 0)
                {
                    errorMessage = "该书籍已有其他读者预约，无法续借";
                    return false;
                }

                // 检查读者是否有未支付罚款
                string checkFinesSql = @"
                    SELECT SUM(amount) 
                    FROM fine 
                    WHERE cardID = @cardID 
                      AND fine_status = N'未支付'";

                object unpaidObj = DatabaseHelper.ExecuteScalar(checkFinesSql,
                    DatabaseHelper.CreateParameter("@cardID", row["cardID"].ToString()));
                decimal unpaidFines = unpaidObj != null && unpaidObj != DBNull.Value ? 
                    Convert.ToDecimal(unpaidObj) : 0;

                if (unpaidFines > 0)
                {
                    errorMessage = $"您有未支付罚款 ¥{unpaidFines:F2}，请先缴纳罚款后再续借";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "校验失败：" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 执行续借操作
        /// </summary>
        /// <param name="bookborrowId">借阅明细ID</param>
        /// <param name="errorMessage">错误信息</param>
        /// <returns>是否成功</returns>
        public static bool ProcessRenew(long bookborrowId, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!ValidateRenewEligibility(bookborrowId, out errorMessage))
            {
                return false;
            }

            try
            {
                // 更新续借信息
                string updateSql = @"
                    UPDATE bookborrow 
                    SET renew_count = ISNULL(renew_count, 0) + 1,
                        last_renew_time = SYSDATETIME()
                    WHERE bookborrow_id = @bookborrowId";

                int affected = DatabaseHelper.ExecuteNonQuery(updateSql,
                    DatabaseHelper.CreateParameter("@bookborrowId", bookborrowId));

                if (affected == 0)
                {
                    errorMessage = "续借失败，未找到借阅记录";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "续借失败：" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 获取读者的可续借书籍列表
        /// </summary>
        /// <param name="cardID">借书证号</param>
        /// <returns>可续借书籍列表</returns>
        public static DataTable GetRenewableBooks(string cardID)
        {
            string sql = @"
                SELECT bb.bookborrow_id, bb.bookID, bb.borrowdate, bb.last_renew_time,
                       ISNULL(bb.renew_count, 0) AS renew_count,
                       bib.bibliography_name, bib.ISBN,
                       bc.category_code, bc.category_name,
                       r.readertype,
                       COALESCE(bi.price, bib.price, 0) AS book_price,
                       -- 计算当前到期日（基于最后续借时间或借阅时间）
                       CASE 
                           WHEN bb.last_renew_time IS NOT NULL THEN bb.last_renew_time
                           ELSE bb.borrowdate
                       END AS effective_borrow_date
                FROM bookborrow bb
                INNER JOIN reader r ON bb.cardID = r.cardID
                INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                INNER JOIN BOOK_CATEGORY bc ON bib.category_id = bc.category_id
                WHERE bb.cardID = @cardID 
                  AND bb.overdate IS NULL
                ORDER BY bb.borrowdate DESC";

            return DatabaseHelper.ExecuteQuery(sql,
                DatabaseHelper.CreateParameter("@cardID", cardID));
        }

        /// <summary>
        /// 计算续借后的新到期日期
        /// </summary>
        /// <param name="currentBorrowDate">当前有效借阅日期（最后续借时间或初始借阅时间）</param>
        /// <param name="readerType">读者类型</param>
        /// <returns>续借后的到期日期</returns>
        public static DateTime CalculateRenewedDueDate(DateTime currentBorrowDate, string readerType)
        {
            BorrowRule rule = BorrowRules.GetRuleByReaderType(readerType);
            // 从当前日期开始，延长续借期限
            return DateTime.Now.AddDays(rule.RenewDays);
        }

        /// <summary>
        /// 获取续借信息摘要
        /// </summary>
        /// <param name="bookborrowId">借阅明细ID</param>
        /// <returns>续借信息摘要</returns>
        public static string GetRenewSummary(long bookborrowId)
        {
            try
            {
                string sql = @"
                    SELECT bb.renew_count, bb.borrowdate, bb.last_renew_time,
                           r.readertype, bib.bibliography_name
                    FROM bookborrow bb
                    INNER JOIN reader r ON bb.cardID = r.cardID
                    INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                    INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                    WHERE bb.bookborrow_id = @bookborrowId";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@bookborrowId", bookborrowId));

                if (dt.Rows.Count == 0)
                    return "未找到借阅记录";

                DataRow row = dt.Rows[0];
                int renewCount = row["renew_count"] != DBNull.Value ? 
                    Convert.ToInt32(row["renew_count"]) : 0;
                DateTime effectiveDate = row["last_renew_time"] != DBNull.Value ?
                    Convert.ToDateTime(row["last_renew_time"]) :
                    Convert.ToDateTime(row["borrowdate"]);
                string readerType = row["readertype"].ToString();
                string bookName = row["bibliography_name"].ToString();

                BorrowRule rule = BorrowRules.GetRuleByReaderType(readerType);
                DateTime currentDueDate = BorrowRules.CalculateDueDate(effectiveDate, readerType);
                DateTime newDueDate = CalculateRenewedDueDate(effectiveDate, readerType);

                return $"《{bookName}》\n" +
                       $"已续借次数：{renewCount}/{rule.MaxRenewCount}\n" +
                       $"当前到期日：{currentDueDate:yyyy-MM-dd}\n" +
                       $"续借后到期日：{newDueDate:yyyy-MM-dd}\n" +
                       $"可再续借：{rule.MaxRenewCount - renewCount}次";
            }
            catch
            {
                return "获取续借信息失败";
            }
        }
    }
}
