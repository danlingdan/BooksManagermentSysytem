using System;
using System.Data;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Models;

namespace BooksManagermentSysytem.Services
{
    /// <summary>
    /// 图书预约服务
    /// </summary>
    public class ReservationService
    {
        /// <summary>
        /// 创建预约
        /// </summary>
        /// <param name="cardID">借书证号</param>
        /// <param name="bookID">图书条码</param>
        /// <param name="reservationType">预约类型（BORROW_RESERVE/NEW_BOOK）</param>
        /// <param name="errorMessage">错误信息</param>
        /// <returns>预约ID，失败返回0</returns>
        public static long CreateReservation(string cardID, string bookID, string reservationType, out string errorMessage)
        {
            errorMessage = string.Empty;

            // 校验预约资格
            if (!ValidateReservationEligibility(cardID, bookID, reservationType, out errorMessage))
            {
                return 0;
            }

            try
            {
                // 计算过期时间
                DateTime expireTime = BorrowRules.CalculateReservationExpireTime(DateTime.Now);

                string insertSql = @"
                    INSERT INTO book_reservation (cardID, bookID, reservation_type, expire_time, reservation_status)
                    VALUES (@cardID, @bookID, @reservationType, @expireTime, N'PENDING');
                    SELECT SCOPE_IDENTITY();";

                object result = DatabaseHelper.ExecuteScalar(insertSql,
                    DatabaseHelper.CreateParameter("@cardID", cardID),
                    DatabaseHelper.CreateParameter("@bookID", bookID),
                    DatabaseHelper.CreateParameter("@reservationType", reservationType),
                    DatabaseHelper.CreateParameter("@expireTime", expireTime));

                if (result == null || result == DBNull.Value)
                {
                    errorMessage = "创建预约失败";
                    return 0;
                }

                // 如果是借阅预约，更新图书状态为RESERVED
                if (reservationType == "BORROW_RESERVE")
                {
                    string updateStatusSql = @"
                        UPDATE BOOK_ITEM 
                        SET current_status = N'RESERVED', 
                            status_changed_date = SYSDATETIME()
                        WHERE item_barcode = @bookID 
                          AND current_status = N'BORROWED'";

                    DatabaseHelper.ExecuteNonQuery(updateStatusSql,
                        DatabaseHelper.CreateParameter("@bookID", bookID));
                }

                return Convert.ToInt64(result);
            }
            catch (Exception ex)
            {
                errorMessage = "创建预约失败：" + ex.Message;
                return 0;
            }
        }

        /// <summary>
        /// 校验预约资格
        /// </summary>
        public static bool ValidateReservationEligibility(string cardID, string bookID, string reservationType, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                // 检查读者借书证状态
                string checkReaderSql = @"
                    SELECT r.readername, r.readertype, rc.state, rc.overdate
                    FROM reader r
                    INNER JOIN readcard rc ON r.cardID = rc.cardID
                    WHERE r.cardID = @cardID";

                DataTable readerDt = DatabaseHelper.ExecuteQuery(checkReaderSql,
                    DatabaseHelper.CreateParameter("@cardID", cardID));

                if (readerDt.Rows.Count == 0)
                {
                    errorMessage = "未找到该读者信息";
                    return false;
                }

                DataRow readerRow = readerDt.Rows[0];
                string cardState = readerRow["state"].ToString();
                DateTime cardExpire = Convert.ToDateTime(readerRow["overdate"]);

                if (cardState != "正常" || cardExpire < DateTime.Today)
                {
                    errorMessage = "借书证状态不正常或已过期，无法预约";
                    return false;
                }

                // 检查是否有未支付罚款
                string checkFinesSql = @"
                    SELECT SUM(amount) 
                    FROM fine 
                    WHERE cardID = @cardID 
                      AND fine_status = N'未支付'";

                object unpaidObj = DatabaseHelper.ExecuteScalar(checkFinesSql,
                    DatabaseHelper.CreateParameter("@cardID", cardID));
                decimal unpaidFines = unpaidObj != null && unpaidObj != DBNull.Value ? 
                    Convert.ToDecimal(unpaidObj) : 0;

                if (unpaidFines > 0)
                {
                    errorMessage = $"您有未支付罚款 ¥{unpaidFines:F2}，请先缴纳罚款后再预约";
                    return false;
                }

                // 检查该读者是否已有该书的有效预约
                string checkDuplicateSql = @"
                    SELECT COUNT(*) 
                    FROM book_reservation 
                    WHERE cardID = @cardID 
                      AND bookID = @bookID 
                      AND reservation_status = N'PENDING'";

                int existingCount = Convert.ToInt32(DatabaseHelper.ExecuteScalar(
                    checkDuplicateSql,
                    DatabaseHelper.CreateParameter("@cardID", cardID),
                    DatabaseHelper.CreateParameter("@bookID", bookID)));

                if (existingCount > 0)
                {
                    errorMessage = "您已预约过该书籍，请勿重复预约";
                    return false;
                }

                // 检查预约数量限制
                string checkCountSql = @"
                    SELECT COUNT(*) 
                    FROM book_reservation 
                    WHERE cardID = @cardID 
                      AND reservation_status = N'PENDING'";

                int pendingCount = Convert.ToInt32(DatabaseHelper.ExecuteScalar(
                    checkCountSql,
                    DatabaseHelper.CreateParameter("@cardID", cardID)));

                if (pendingCount >= BorrowRules.MaxReservations)
                {
                    errorMessage = $"您已有{pendingCount}个待处理预约，最多只能同时预约{BorrowRules.MaxReservations}本书";
                    return false;
                }

                // 检查图书状态
                string checkBookSql = @"
                    SELECT bi.current_status, bi.location_id, sl.location_type
                    FROM BOOK_ITEM bi
                    INNER JOIN STORAGE_LOCATION sl ON bi.location_id = sl.location_id
                    WHERE bi.item_barcode = @bookID";

                DataTable bookDt = DatabaseHelper.ExecuteQuery(checkBookSql,
                    DatabaseHelper.CreateParameter("@bookID", bookID));

                if (bookDt.Rows.Count == 0)
                {
                    errorMessage = "未找到该书籍";
                    return false;
                }

                DataRow bookRow = bookDt.Rows[0];
                string bookStatus = bookRow["current_status"].ToString();
                string locationType = bookRow["location_type"].ToString();

                // 工具书区不可预约
                if (locationType == "REFERENCE" || locationType == "TOOL_ONLY")
                {
                    errorMessage = "工具书区/仅供查阅书籍不可预约";
                    return false;
                }

                // 借阅预约：必须是已借出状态或已被预约状态
                if (reservationType == "BORROW_RESERVE")
                {
                    if (bookStatus == "AVAILABLE")
                    {
                        errorMessage = "该书籍当前未被借出，无需预约，可直接借阅";
                        return false;
                    }
                    else if (bookStatus == "RESERVED")
                    {
                        errorMessage = "该书籍已被其他读者预约，暂时无法预约";
                        return false;
                    }
                    else if (bookStatus != "BORROWED")
                    {
                        errorMessage = $"该书籍当前状态（{bookStatus}）不允许预约";
                        return false;
                    }
                }

                // 新书预约：必须是新书区且可借
                if (reservationType == "NEW_BOOK" && locationType != "NEW_BOOK")
                {
                    errorMessage = "该书籍不在新书区，无法进行新书预约";
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
        /// 处理预约取书
        /// </summary>
        /// <param name="reservationId">预约ID</param>
        /// <param name="errorMessage">错误信息</param>
        /// <returns>是否成功</returns>
        public static bool ProcessReservationPickup(long reservationId, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                // 查询预约信息
                string querySql = @"
                    SELECT reservation_id, cardID, bookID, reservation_status, expire_time
                    FROM book_reservation
                    WHERE reservation_id = @reservationId";

                DataTable dt = DatabaseHelper.ExecuteQuery(querySql,
                    DatabaseHelper.CreateParameter("@reservationId", reservationId));

                if (dt.Rows.Count == 0)
                {
                    errorMessage = "未找到该预约记录";
                    return false;
                }

                DataRow row = dt.Rows[0];
                string status = row["reservation_status"].ToString();
                DateTime expireTime = Convert.ToDateTime(row["expire_time"]);

                if (status != "PENDING")
                {
                    errorMessage = $"该预约已处理，状态为：{status}";
                    return false;
                }

                if (DateTime.Now > expireTime)
                {
                    errorMessage = "该预约已过期";
                    // 自动更新为过期状态
                    CancelReservation(reservationId, "系统自动取消：预约已过期", out _);
                    return false;
                }

                // 更新预约状态
                string updateSql = @"
                    UPDATE book_reservation 
                    SET reservation_status = N'FULFILLED',
                        pickup_time = SYSDATETIME()
                    WHERE reservation_id = @reservationId";

                DatabaseHelper.ExecuteNonQuery(updateSql,
                    DatabaseHelper.CreateParameter("@reservationId", reservationId));

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "处理预约取书失败：" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="reservationId">预约ID</param>
        /// <param name="cancelReason">取消原因</param>
        /// <param name="errorMessage">错误信息</param>
        /// <returns>是否成功</returns>
        public static bool CancelReservation(long reservationId, string cancelReason, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                // 查询预约信息
                string querySql = @"
                    SELECT bookID, reservation_status, reservation_type
                    FROM book_reservation
                    WHERE reservation_id = @reservationId";

                DataTable dt = DatabaseHelper.ExecuteQuery(querySql,
                    DatabaseHelper.CreateParameter("@reservationId", reservationId));

                if (dt.Rows.Count == 0)
                {
                    errorMessage = "未找到该预约记录";
                    return false;
                }

                DataRow row = dt.Rows[0];
                string status = row["reservation_status"].ToString();
                string bookID = row["bookID"].ToString();
                string reservationType = row["reservation_type"].ToString();

                if (status != "PENDING")
                {
                    errorMessage = $"该预约已处理，状态为：{status}，无法取消";
                    return false;
                }

                // 判断是过期还是手动取消
                bool isExpired = cancelReason.Contains("过期");
                string newStatus = isExpired ? "EXPIRED" : "CANCELLED";

                // 更新预约状态
                string updateSql = @"
                    UPDATE book_reservation 
                    SET reservation_status = @newStatus,
                        note = @cancelReason
                    WHERE reservation_id = @reservationId";

                DatabaseHelper.ExecuteNonQuery(updateSql,
                    DatabaseHelper.CreateParameter("@reservationId", reservationId),
                    DatabaseHelper.CreateParameter("@newStatus", newStatus),
                    DatabaseHelper.CreateParameter("@cancelReason", cancelReason));

                // 如果是借阅预约，恢复图书状态
                if (reservationType == "BORROW_RESERVE")
                {
                    string updateBookSql = @"
                        UPDATE BOOK_ITEM 
                        SET current_status = N'BORROWED',
                            status_changed_date = SYSDATETIME()
                        WHERE item_barcode = @bookID 
                          AND current_status = N'RESERVED'";

                    DatabaseHelper.ExecuteNonQuery(updateBookSql,
                        DatabaseHelper.CreateParameter("@bookID", bookID));
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "取消预约失败：" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 检查并处理过期预约
        /// </summary>
        /// <returns>处理的过期预约数量</returns>
        public static int CheckAndExpireReservations()
        {
            try
            {
                string sql = @"
                    SELECT reservation_id 
                    FROM book_reservation
                    WHERE reservation_status = N'PENDING'
                      AND expire_time < SYSDATETIME()";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql);

                int count = 0;
                foreach (DataRow row in dt.Rows)
                {
                    long reservationId = Convert.ToInt64(row["reservation_id"]);
                    if (CancelReservation(reservationId, "系统自动取消：预约已过期", out _))
                    {
                        count++;
                    }
                }

                return count;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取读者的预约列表
        /// </summary>
        /// <param name="cardID">借书证号</param>
        /// <param name="includeFulfilled">是否包含已完成的预约</param>
        /// <returns>预约列表</returns>
        public static DataTable GetReaderReservations(string cardID, bool includeFulfilled = false)
        {
            string statusFilter = includeFulfilled ? 
                "" : "AND br.reservation_status = N'PENDING'";

            string sql = $@"
                SELECT br.reservation_id, br.bookID, br.reservation_type, 
                       br.reservation_time, br.expire_time, br.pickup_time,
                       br.reservation_status, br.note,
                       bib.bibliography_name, bib.ISBN,
                       bc.category_code, bc.category_name,
                       bi.current_status,
                       CASE 
                           WHEN br.reservation_status = N'PENDING' AND br.expire_time < SYSDATETIME() THEN 1
                           ELSE 0
                       END AS is_expired
                FROM book_reservation br
                INNER JOIN BOOK_ITEM bi ON br.bookID = bi.item_barcode
                INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                INNER JOIN BOOK_CATEGORY bc ON bib.category_id = bc.category_id
                WHERE br.cardID = @cardID
                  {statusFilter}
                ORDER BY br.reservation_time DESC";

            return DatabaseHelper.ExecuteQuery(sql,
                DatabaseHelper.CreateParameter("@cardID", cardID));
        }

        /// <summary>
        /// 获取所有待处理预约（馆员使用）
        /// </summary>
        /// <returns>待处理预约列表</returns>
        public static DataTable GetPendingReservations()
        {
            string sql = @"
                SELECT br.reservation_id, br.cardID, br.bookID, br.reservation_type,
                       br.reservation_time, br.expire_time,
                       r.readername, r.readertype,
                       bib.bibliography_name, bib.ISBN,
                       bc.category_code,
                       bi.current_status,
                       CASE 
                           WHEN br.expire_time < SYSDATETIME() THEN 1
                           ELSE 0
                       END AS is_expired,
                       DATEDIFF(HOUR, SYSDATETIME(), br.expire_time) AS hours_remaining
                FROM book_reservation br
                INNER JOIN reader r ON br.cardID = r.cardID
                INNER JOIN BOOK_ITEM bi ON br.bookID = bi.item_barcode
                INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                INNER JOIN BOOK_CATEGORY bc ON bib.category_id = bc.category_id
                WHERE br.reservation_status = N'PENDING'
                ORDER BY br.reservation_time ASC";

            return DatabaseHelper.ExecuteQuery(sql);
        }

        /// <summary>
        /// 检查图书归还时是否有预约
        /// </summary>
        /// <param name="bookID">图书条码</param>
        /// <returns>预约信息，无预约返回null</returns>
        public static DataRow CheckReservationOnReturn(string bookID)
        {
            string sql = @"
                SELECT TOP 1 br.reservation_id, br.cardID, r.readername, 
                       br.reservation_time, br.expire_time
                FROM book_reservation br
                INNER JOIN reader r ON br.cardID = r.cardID
                WHERE br.bookID = @bookID
                  AND br.reservation_status = N'PENDING'
                  AND br.reservation_type = N'BORROW_RESERVE'
                  AND br.expire_time >= SYSDATETIME()
                ORDER BY br.reservation_time ASC";

            DataTable dt = DatabaseHelper.ExecuteQuery(sql,
                DatabaseHelper.CreateParameter("@bookID", bookID));

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }
    }
}
