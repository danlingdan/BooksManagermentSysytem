using System;
using System.Collections.Generic;
using System.Data;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Models;

namespace BooksManagermentSysytem.Services
{
    /// <summary>
    /// 逾期自动提醒服务
    /// 系统每日自动检测即将逾期（未来10天内）和已逾期的借阅记录，
    /// 计算逾期天数，生成提醒信息，通过系统消息、邮件、短信等方式推送给读者
    /// </summary>
    public class OverdueReminderService
    {
        /// <summary>
        /// 即将到期提醒天数（提前几天提醒）
        /// </summary>
        public const int SoonDueDays = 3;

        /// <summary>
        /// 检测并处理所有逾期和即将逾期的图书
        /// </summary>
        /// <returns>处理的提醒数量</returns>
        public static int ProcessAllOverdueReminders()
        {
            int count = 0;

            // 处理已逾期的图书
            count += ProcessOverdueBooks();

            // 处理即将逾期的图书
            count += ProcessSoonDueBooks();

            return count;
        }

        /// <summary>
        /// 处理已逾期的图书提醒
        /// </summary>
        /// <returns>处理的提醒数量</returns>
        public static int ProcessOverdueBooks()
        {
            try
            {
                // 查询所有逾期的借阅记录
                string sql = @"
                    SELECT bb.bookborrow_id, bb.cardID, bb.bookID, bb.borrowdate,
                           CASE 
                               WHEN bb.last_renew_time IS NOT NULL THEN bb.last_renew_time
                               ELSE bb.borrowdate
                           END AS effective_borrow_date,
                           r.readername, r.readertype,
                           bib.bibliography_name, bib.ISBN,
                           COALESCE(bi.price, bib.price, 0) AS book_price
                    FROM bookborrow bb
                    INNER JOIN reader r ON bb.cardID = r.cardID
                    INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                    INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                    WHERE bb.overdate IS NULL";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql);

                int count = 0;
                foreach (DataRow row in dt.Rows)
                {
                    DateTime effectiveDate = Convert.ToDateTime(row["effective_borrow_date"]);
                    string readerType = row["readertype"].ToString();
                    
                    if (BorrowRules.IsOverdue(effectiveDate, readerType))
                    {
                        int overdueDays = BorrowRules.CalculateOverdueDays(effectiveDate, readerType);
                        
                        if (CreateOverdueReminder(row, overdueDays))
                        {
                            count++;
                        }
                    }
                }

                return count;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"处理逾期图书失败：{ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 处理即将逾期的图书提醒
        /// </summary>
        /// <returns>处理的提醒数量</returns>
        public static int ProcessSoonDueBooks()
        {
            try
            {
                // 查询即将到期的借阅记录（未来SoonDueDays天内）
                string sql = @"
                    SELECT bb.bookborrow_id, bb.cardID, bb.bookID, bb.borrowdate,
                           CASE 
                               WHEN bb.last_renew_time IS NOT NULL THEN bb.last_renew_time
                               ELSE bb.borrowdate
                           END AS effective_borrow_date,
                           r.readername, r.readertype,
                           bib.bibliography_name, bib.ISBN,
                           COALESCE(bi.price, bib.price, 0) AS book_price
                    FROM bookborrow bb
                    INNER JOIN reader r ON bb.cardID = r.cardID
                    INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                    INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                    WHERE bb.overdate IS NULL";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql);

                int count = 0;
                foreach (DataRow row in dt.Rows)
                {
                    DateTime effectiveDate = Convert.ToDateTime(row["effective_borrow_date"]);
                    string readerType = row["readertype"].ToString();
                    DateTime dueDate = BorrowRules.CalculateDueDate(effectiveDate, readerType);
                    
                    int daysLeft = (dueDate - DateTime.Now).Days;
                    
                    // 即将到期但还未逾期
                    if (daysLeft >= 0 && daysLeft <= SoonDueDays)
                    {
                        if (CreateSoonDueReminder(row, daysLeft, dueDate))
                        {
                            count++;
                        }
                    }
                }

                return count;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"处理即将逾期图书失败：{ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 创建逾期提醒
        /// </summary>
        private static bool CreateOverdueReminder(DataRow row, int overdueDays)
        {
            try
            {
                string cardID = row["cardID"].ToString();
                string bookID = row["bookID"].ToString();
                string readerName = row["readername"].ToString();
                string bookName = row["bibliography_name"].ToString();
                string readerType = row["readertype"].ToString();
                decimal bookPrice = Convert.ToDecimal(row["book_price"]);
                DateTime effectiveDate = Convert.ToDateTime(row["effective_borrow_date"]);
                DateTime dueDate = BorrowRules.CalculateDueDate(effectiveDate, readerType);

                // 检查今天是否已经发送过提醒（避免重复发送）
                if (HasReminderToday(cardID, bookID))
                {
                    return false;
                }

                // 计算预计罚款
                decimal estimatedFine = FineCalculator.CalculateOverdueFine(bookPrice, overdueDays, readerType);

                // 记录到逾期提醒日志
                LogOverdueReminder(cardID, bookID, effectiveDate, dueDate, overdueDays, estimatedFine);

                // 创建系统消息
                var parameters = new Dictionary<string, string>
                {
                    { "BookName", bookName },
                    { "OverdueDays", overdueDays.ToString() },
                    { "DueDate", dueDate.ToString("yyyy-MM-dd") },
                    { "EstimatedFine", estimatedFine.ToString("F2") }
                };

                long messageId = NotificationService.Instance.CreateMessageFromTemplate(
                    cardID, "OVERDUE_REMINDER", parameters, bookID, "BookBorrow");

                return messageId > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 创建即将到期提醒
        /// </summary>
        private static bool CreateSoonDueReminder(DataRow row, int daysLeft, DateTime dueDate)
        {
            try
            {
                string cardID = row["cardID"].ToString();
                string bookID = row["bookID"].ToString();
                string bookName = row["bibliography_name"].ToString();

                // 检查今天是否已经发送过提醒
                if (HasReminderToday(cardID, bookID))
                {
                    return false;
                }

                // 创建系统消息
                var parameters = new Dictionary<string, string>
                {
                    { "BookName", bookName },
                    { "DaysLeft", daysLeft.ToString() },
                    { "DueDate", dueDate.ToString("yyyy-MM-dd") }
                };

                long messageId = NotificationService.Instance.CreateMessageFromTemplate(
                    cardID, "SOON_DUE_REMINDER", parameters, bookID, "BookBorrow");

                if (messageId > 0)
                {
                    // 记录到日志（逾期天数为0表示即将逾期）
                    DateTime borrowDate = Convert.ToDateTime(row["effective_borrow_date"]);
                    LogOverdueReminder(cardID, bookID, borrowDate, dueDate, 0, 0);
                }

                return messageId > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 记录逾期提醒日志
        /// </summary>
        private static bool LogOverdueReminder(string cardID, string bookID, DateTime borrowDate, 
            DateTime dueDate, int overdueDays, decimal estimatedFine)
        {
            try
            {
                string sql = @"
                    INSERT INTO overdue_reminder_log 
                    (cardID, bookID, borrow_date, due_date, overdue_days, estimated_fine, is_sent, channel)
                    VALUES (@cardID, @bookID, @borrowDate, @dueDate, @overdueDays, @estimatedFine, 1, N'InApp')";

                int affected = DatabaseHelper.ExecuteNonQuery(sql,
                    DatabaseHelper.CreateParameter("@cardID", cardID),
                    DatabaseHelper.CreateParameter("@bookID", bookID),
                    DatabaseHelper.CreateParameter("@borrowDate", borrowDate),
                    DatabaseHelper.CreateParameter("@dueDate", dueDate),
                    DatabaseHelper.CreateParameter("@overdueDays", overdueDays),
                    DatabaseHelper.CreateParameter("@estimatedFine", estimatedFine));

                return affected > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 检查今天是否已发送过提醒
        /// </summary>
        private static bool HasReminderToday(string cardID, string bookID)
        {
            try
            {
                string sql = @"
                    SELECT COUNT(*) 
                    FROM overdue_reminder_log 
                    WHERE cardID = @cardID 
                      AND bookID = @bookID 
                      AND CAST(reminder_time AS DATE) = CAST(GETDATE() AS DATE)";

                object result = DatabaseHelper.ExecuteScalar(sql,
                    DatabaseHelper.CreateParameter("@cardID", cardID),
                    DatabaseHelper.CreateParameter("@bookID", bookID));

                return result != null && Convert.ToInt32(result) > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取读者的逾期提醒历史
        /// </summary>
        /// <param name="cardID">借书证号</param>
        /// <param name="days">查询最近几天的记录，0表示全部</param>
        /// <returns>提醒历史</returns>
        public static DataTable GetReminderHistory(string cardID, int days = 30)
        {
            try
            {
                string sql = @"
                    SELECT orl.reminder_id, orl.cardID, orl.bookID, orl.borrow_date, 
                           orl.due_date, orl.overdue_days, orl.estimated_fine, 
                           orl.reminder_time, orl.channel,
                           bib.bibliography_name AS book_name
                    FROM overdue_reminder_log orl
                    LEFT JOIN BOOK_ITEM bi ON orl.bookID = bi.item_barcode
                    LEFT JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                    WHERE orl.cardID = @cardID";

                if (days > 0)
                {
                    sql += " AND orl.reminder_time >= DATEADD(DAY, -@days, GETDATE())";
                }

                sql += " ORDER BY orl.reminder_time DESC";

                return DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@cardID", cardID),
                    DatabaseHelper.CreateParameter("@days", days));
            }
            catch
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// 处理借书证即将到期的提醒
        /// </summary>
        /// <param name="daysBeforeExpire">提前几天提醒</param>
        /// <returns>处理的提醒数量</returns>
        public static int ProcessCardExpireReminders(int daysBeforeExpire = 30)
        {
            try
            {
                string sql = @"
                    SELECT r.cardID, r.readername, rc.overdate
                    FROM reader r
                    INNER JOIN readcard rc ON r.cardID = rc.cardID
                    WHERE rc.state = N'正常'
                      AND rc.overdate BETWEEN CAST(GETDATE() AS DATE) 
                      AND DATEADD(DAY, @days, CAST(GETDATE() AS DATE))";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@days", daysBeforeExpire));

                int count = 0;
                foreach (DataRow row in dt.Rows)
                {
                    string cardID = row["cardID"].ToString();
                    DateTime expireDate = Convert.ToDateTime(row["overdate"]);
                    int daysLeft = (expireDate - DateTime.Now).Days;

                    // 检查是否已发送过提醒
                    if (HasCardExpireReminderToday(cardID))
                    {
                        continue;
                    }

                    var parameters = new Dictionary<string, string>
                    {
                        { "DaysLeft", daysLeft.ToString() },
                        { "ExpireDate", expireDate.ToString("yyyy-MM-dd") }
                    };

                    long messageId = NotificationService.Instance.CreateMessageFromTemplate(
                        cardID, "CARD_EXPIRE_REMINDER", parameters, cardID, "ReaderCard");

                    if (messageId > 0)
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
        /// 检查今天是否已发送过借书证到期提醒
        /// </summary>
        private static bool HasCardExpireReminderToday(string cardID)
        {
            try
            {
                string sql = @"
                    SELECT COUNT(*) 
                    FROM system_message 
                    WHERE cardID = @cardID 
                      AND message_type = N'CardExpireReminder'
                      AND CAST(created_time AS DATE) = CAST(GETDATE() AS DATE)";

                object result = DatabaseHelper.ExecuteScalar(sql,
                    DatabaseHelper.CreateParameter("@cardID", cardID));

                return result != null && Convert.ToInt32(result) > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
