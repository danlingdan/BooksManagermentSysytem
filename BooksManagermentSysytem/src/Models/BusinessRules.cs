using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BooksManagermentSysytem.Data;

namespace BooksManagermentSysytem.Models
{
    /// <summary>
    /// 借阅规则类 - 从数据库动态读取规则
    /// </summary>
    public static class BorrowRules
    {
        private static Dictionary<string, BorrowRule> ruleCache = new Dictionary<string, BorrowRule>();
        private static DateTime lastCacheTime = DateTime.MinValue;
        private const int CacheMinutes = 10;

        /// <summary>
        /// 默认最大借阅本数（用于兜底）
        /// </summary>
        public const int MaxBooksPerBorrow = 3;

        /// <summary>
        /// 默认最大借阅分类数（用于兜底）
        /// </summary>
        public const int MaxCategoriesPerBorrow = 2;

        /// <summary>
        /// 默认借阅天数（用于兜底）
        /// </summary>
        public const int BorrowDays = 7;

        /// <summary>
        /// 最大预约本数
        /// </summary>
        public const int MaxReservations = 3;

        /// <summary>
        /// 最大预约分类数
        /// </summary>
        public const int MaxReservationCategories = 2;

        /// <summary>
        /// 预约有效天数
        /// </summary>
        public const int ReservationDays = 3;

        /// <summary>
        /// 新书区保留月数
        /// </summary>
        public const int NewBookZoneMonths = 3;

        /// <summary>
        /// 热门图书每日搜索阈值
        /// </summary>
        public const int HotBookSearchThreshold = 10;

        /// <summary>
        /// 获取指定读者类型的借阅规则
        /// </summary>
        public static BorrowRule GetRuleByReaderType(string readerType)
        {
            RefreshCacheIfNeeded();

            if (ruleCache.ContainsKey(readerType))
            {
                return ruleCache[readerType];
            }

            // 从数据库加载
            try
            {
                string sql = "EXEC sp_GetBorrowRuleByReaderType @reader_type";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@reader_type", readerType));

                if (dt.Rows.Count > 0)
                {
                    var rule = MapFromDataRow(dt.Rows[0]);
                    ruleCache[readerType] = rule;
                    return rule;
                }
            }
            catch
            {
                // 数据库读取失败，返回默认规则
            }

            // 返回默认规则
            return GetDefaultRule(readerType);
        }

        private static void RefreshCacheIfNeeded()
        {
            if ((DateTime.Now - lastCacheTime).TotalMinutes > CacheMinutes)
            {
                ruleCache.Clear();
                lastCacheTime = DateTime.Now;
            }
        }

        private static BorrowRule MapFromDataRow(DataRow row)
        {
            return new BorrowRule
            {
                RuleId = Convert.ToInt32(row["rule_id"]),
                ReaderType = row["reader_type"].ToString(),
                MaxBorrowCount = Convert.ToInt32(row["max_borrow_count"]),
                MaxCategoryCount = Convert.ToInt32(row["max_category_count"]),
                BorrowDays = Convert.ToInt32(row["borrow_days"]),
                MaxRenewCount = Convert.ToInt32(row["max_renew_count"]),
                RenewDays = Convert.ToInt32(row["renew_days"]),
                AllowReferenceBooks = Convert.ToBoolean(row["allow_reference_books"]),
                AllowNewBooks = Convert.ToBoolean(row["allow_new_books"]),
                AllowHotBooks = Convert.ToBoolean(row["allow_hot_books"])
            };
        }

        private static BorrowRule GetDefaultRule(string readerType)
        {
            return new BorrowRule
            {
                ReaderType = readerType,
                MaxBorrowCount = MaxBooksPerBorrow,
                MaxCategoryCount = MaxCategoriesPerBorrow,
                BorrowDays = BorrowDays,
                MaxRenewCount = 2,
                RenewDays = 7,
                AllowReferenceBooks = false,
                AllowNewBooks = true,
                AllowHotBooks = true
            };
        }

        /// <summary>
        /// 验证借阅请求（支持读者类型）
        /// </summary>
        public static bool ValidateBorrowRequest(string readerType, int currentBorrowedCount,
            List<BookItem> requestedBooks, out string errorMessage)
        {
            var rule = GetRuleByReaderType(readerType);
            return ValidateBorrowRequest(currentBorrowedCount, requestedBooks, out errorMessage, rule);
        }

        /// <summary>
        /// 验证借阅请求（原有方法，保持兼容）
        /// </summary>
        public static bool ValidateBorrowRequest(int currentBorrowedCount, 
            List<BookItem> requestedBooks, out string errorMessage, BorrowRule rule = null)
        {
            errorMessage = string.Empty;

            if (rule == null)
            {
                rule = GetDefaultRule("");
            }

            // 检查数量限制
            if (requestedBooks == null || requestedBooks.Count == 0)
            {
                errorMessage = "请选择要借阅的书籍";
                return false;
            }

            if (requestedBooks.Count > rule.MaxBorrowCount)
            {
                errorMessage = $"一次最多借阅{rule.MaxBorrowCount}本书籍";
                return false;
            }

            if (currentBorrowedCount + requestedBooks.Count > rule.MaxBorrowCount)
            {
                errorMessage = $"您当前已借阅{currentBorrowedCount}本，最多还能借{rule.MaxBorrowCount - currentBorrowedCount}本";
                return false;
            }

            // 检查分类限制
            var categories = requestedBooks.Select(b => b.CategoryCode).Distinct().ToList();
            if (categories.Count > rule.MaxCategoryCount)
            {
                errorMessage = $"一次最多借阅{rule.MaxCategoryCount}个分类的书籍";
                return false;
            }

            // 检查书籍状态
            foreach (var book in requestedBooks)
            {
                if (book.CurrentStatus != "AVAILABLE")
                {
                    errorMessage = $"书籍 {book.BookName}({book.ItemBarcode}) 当前状态为 {book.CurrentStatus}，无法借阅";
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 验证预约请求
        /// </summary>
        public static bool ValidateReservationRequest(int currentReservationCount,
            List<BookItem> requestedBooks, bool hasPendingReservation, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (hasPendingReservation)
            {
                errorMessage = "您有未完成的预约，请先完成当前预约后再进行新的预约";
                return false;
            }

            if (requestedBooks == null || requestedBooks.Count == 0)
            {
                errorMessage = "请选择要预约的书籍";
                return false;
            }

            if (requestedBooks.Count > MaxReservations)
            {
                errorMessage = $"一次最多预约{MaxReservations}本书籍";
                return false;
            }

            var categories = requestedBooks.Select(b => b.CategoryCode).Distinct().ToList();
            if (categories.Count > MaxReservationCategories)
            {
                errorMessage = $"一次最多预约{MaxReservationCategories}个分类的书籍";
                return false;
            }

            return true;
        }

        /// <summary>
        /// 计算到期日期（支持读者类型）
        /// </summary>
        public static DateTime CalculateDueDate(DateTime borrowDate, string readerType = null)
        {
            if (string.IsNullOrEmpty(readerType))
            {
                return borrowDate.AddDays(BorrowDays);
            }

            var rule = GetRuleByReaderType(readerType);
            return borrowDate.AddDays(rule.BorrowDays);
        }

        /// <summary>
        /// 计算预约过期时间
        /// </summary>
        public static DateTime CalculateReservationExpireTime(DateTime reservationTime)
        {
            return reservationTime.AddDays(ReservationDays);
        }

        /// <summary>
        /// 检查是否逾期
        /// </summary>
        public static bool IsOverdue(DateTime borrowDate, string readerType = null)
        {
            return DateTime.Now > CalculateDueDate(borrowDate, readerType);
        }

        /// <summary>
        /// 计算逾期天数
        /// </summary>
        public static int CalculateOverdueDays(DateTime borrowDate, string readerType = null)
        {
            var dueDate = CalculateDueDate(borrowDate, readerType);
            if (DateTime.Now <= dueDate)
                return 0;
            return (int)(DateTime.Now - dueDate).TotalDays;
        }

        /// <summary>
        /// 清除缓存（用于规则更新后）
        /// </summary>
        public static void ClearCache()
        {
            ruleCache.Clear();
            lastCacheTime = DateTime.MinValue;
        }
    }

    /// <summary>
    /// 罚款计算器类 - 从数据库动态读取规则
    /// </summary>
    public static class FineCalculator
    {
        private static Dictionary<string, FineRule> ruleCache = new Dictionary<string, FineRule>();
        private static DateTime lastCacheTime = DateTime.MinValue;
        private const int CacheMinutes = 10;

        /// <summary>
        /// 默认逾期罚款系数（书价）
        /// </summary>
        public const decimal OverduePriceRate = 0.1m;

        /// <summary>
        /// 默认逾期罚款系数（每天）
        /// </summary>
        public const decimal OverdueDayRate = 0.1m;

        /// <summary>
        /// 默认丢失赔偿系数
        /// </summary>
        public const decimal LostRate = 1.0m;

        /// <summary>
        /// 默认损坏赔偿系数
        /// </summary>
        public const decimal DamagedRate = 0.5m;

        /// <summary>
        /// 获取指定读者类型的处罚规则
        /// </summary>
        public static FineRule GetRuleByReaderType(string readerType)
        {
            RefreshCacheIfNeeded();

            if (ruleCache.ContainsKey(readerType))
            {
                return ruleCache[readerType];
            }

            // 从数据库加载
            try
            {
                string sql = "EXEC sp_GetFineRuleByReaderType @reader_type";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@reader_type", readerType));

                if (dt.Rows.Count > 0)
                {
                    var rule = MapFromDataRow(dt.Rows[0]);
                    ruleCache[readerType] = rule;
                    return rule;
                }
            }
            catch
            {
                // 数据库读取失败，返回默认规则
            }

            // 返回默认规则
            return GetDefaultRule(readerType);
        }

        private static void RefreshCacheIfNeeded()
        {
            if ((DateTime.Now - lastCacheTime).TotalMinutes > CacheMinutes)
            {
                ruleCache.Clear();
                lastCacheTime = DateTime.Now;
            }
        }

        private static FineRule MapFromDataRow(DataRow row)
        {
            return new FineRule
            {
                RuleId = Convert.ToInt32(row["rule_id"]),
                ReaderType = row["reader_type"].ToString(),
                OverduePriceRate = Convert.ToDecimal(row["overdue_price_rate"]),
                OverdueDayRate = Convert.ToDecimal(row["overdue_day_rate"]),
                LostRate = Convert.ToDecimal(row["lost_rate"]),
                DamagedRate = Convert.ToDecimal(row["damaged_rate"]),
                MinorDamagedRate = Convert.ToDecimal(row["minor_damaged_rate"]),
                MaxOverdueFine = row["max_overdue_fine"] != DBNull.Value ? 
                    (decimal?)Convert.ToDecimal(row["max_overdue_fine"]) : null,
                FreeOverdueDays = Convert.ToInt32(row["free_overdue_days"])
            };
        }

        private static FineRule GetDefaultRule(string readerType)
        {
            return new FineRule
            {
                ReaderType = readerType,
                OverduePriceRate = OverduePriceRate,
                OverdueDayRate = OverdueDayRate,
                LostRate = LostRate,
                DamagedRate = DamagedRate,
                MinorDamagedRate = 0.25m,
                MaxOverdueFine = 50m,
                FreeOverdueDays = 0
            };
        }

        /// <summary>
        /// 计算逾期罚款（支持读者类型和宽限期）
        /// </summary>
        public static decimal CalculateOverdueFine(decimal bookPrice, int overdueDays, string readerType = null)
        {
            if (overdueDays <= 0)
                return 0;

            FineRule rule = string.IsNullOrEmpty(readerType) ? 
                GetDefaultRule("") : GetRuleByReaderType(readerType);

            // 扣除宽限期
            int chargeableDays = overdueDays - rule.FreeOverdueDays;
            if (chargeableDays <= 0)
                return 0;

            decimal fine = bookPrice * rule.OverduePriceRate + chargeableDays * rule.OverdueDayRate;

            // 应用上限
            if (rule.MaxOverdueFine.HasValue && fine > rule.MaxOverdueFine.Value)
            {
                fine = rule.MaxOverdueFine.Value;
            }

            return fine;
        }

        /// <summary>
        /// 计算丢失赔偿
        /// </summary>
        public static decimal CalculateLostFine(decimal bookPrice, string readerType = null)
        {
            FineRule rule = string.IsNullOrEmpty(readerType) ? 
                GetDefaultRule("") : GetRuleByReaderType(readerType);
            return bookPrice * rule.LostRate;
        }

        /// <summary>
        /// 计算损坏赔偿
        /// </summary>
        public static decimal CalculateDamagedFine(decimal bookPrice, string readerType = null)
        {
            FineRule rule = string.IsNullOrEmpty(readerType) ? 
                GetDefaultRule("") : GetRuleByReaderType(readerType);
            return bookPrice * rule.DamagedRate;
        }

        /// <summary>
        /// 计算罚款金额
        /// </summary>
        public static decimal CalculateFine(FineType fineType, decimal bookPrice, int overdueDays = 0, string readerType = null)
        {
            switch (fineType)
            {
                case FineType.Overdue:
                    return CalculateOverdueFine(bookPrice, overdueDays, readerType);
                case FineType.Lost:
                    return CalculateLostFine(bookPrice, readerType);
                case FineType.Damaged:
                    return CalculateDamagedFine(bookPrice, readerType);
                default:
                    return 0;
            }
        }

        /// <summary>
        /// 获取罚款原因描述
        /// </summary>
        public static string GetFineReason(FineType fineType, string bookName, int overdueDays = 0)
        {
            switch (fineType)
            {
                case FineType.Overdue:
                    return $"图书《{bookName}》逾期{overdueDays}天";
                case FineType.Lost:
                    return $"图书《{bookName}》丢失，需赔偿原价";
                case FineType.Damaged:
                    return $"图书《{bookName}》损坏，需赔偿50%";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// 清除缓存（用于规则更新后）
        /// </summary>
        public static void ClearCache()
        {
            ruleCache.Clear();
            lastCacheTime = DateTime.MinValue;
        }
    }

    /// <summary>
    /// 借书证状态辅助类
    /// </summary>
    public static class CardStateHelper
    {
        /// <summary>
        /// 检查借书证状态是否允许借阅
        /// </summary>
        public static bool CanBorrow(string state, DateTime expireDate)
        {
            if (state != "正常")
                return false;

            if (expireDate < DateTime.Today)
                return false;

            return true;
        }

        /// <summary>
        /// 获取状态描述
        /// </summary>
        public static string GetStateDescription(string state, DateTime expireDate)
        {
            if (expireDate < DateTime.Today)
                return "借书证已过期";

            switch (state)
            {
                case "正常":
                    return "借书证状态正常";
                case "注销":
                    return "借书证已注销";
                case "挂失":
                    return "借书证已挂失";
                case "补办中":
                    return "借书证正在补办中";
                default:
                    return "未知状态";
            }
        }

        /// <summary>
        /// 检查是否需要续期
        /// </summary>
        public static bool NeedsRenewal(DateTime expireDate, int daysBeforeWarning = 30)
        {
            return (expireDate - DateTime.Today).TotalDays <= daysBeforeWarning;
        }
    }
}
