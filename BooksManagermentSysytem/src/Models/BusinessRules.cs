using System;
using System.Collections.Generic;
using System.Linq;

namespace BooksManagermentSysytem.Models
{
    /// <summary>
    /// 借阅规则类 - 定义借阅业务规则
    /// </summary>
    public static class BorrowRules
    {
        /// <summary>
        /// 最大借阅本数
        /// </summary>
        public const int MaxBooksPerBorrow = 3;

        /// <summary>
        /// 最大借阅分类数
        /// </summary>
        public const int MaxCategoriesPerBorrow = 2;

        /// <summary>
        /// 借阅天数
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
        /// 验证借阅请求
        /// </summary>
        /// <param name="currentBorrowedCount">当前已借数量</param>
        /// <param name="requestedBooks">请求借阅的书籍列表（包含分类信息）</param>
        /// <param name="errorMessage">错误信息</param>
        /// <returns>是否验证通过</returns>
        public static bool ValidateBorrowRequest(int currentBorrowedCount, 
            List<BookItem> requestedBooks, out string errorMessage)
        {
            errorMessage = string.Empty;

            // 检查数量限制
            if (requestedBooks == null || requestedBooks.Count == 0)
            {
                errorMessage = "请选择要借阅的书籍";
                return false;
            }

            if (requestedBooks.Count > MaxBooksPerBorrow)
            {
                errorMessage = $"一次最多借阅{MaxBooksPerBorrow}本书籍";
                return false;
            }

            if (currentBorrowedCount + requestedBooks.Count > MaxBooksPerBorrow)
            {
                errorMessage = $"您当前已借阅{currentBorrowedCount}本，最多还能借{MaxBooksPerBorrow - currentBorrowedCount}本";
                return false;
            }

            // 检查分类限制
            var categories = requestedBooks.Select(b => b.CategoryCode).Distinct().ToList();
            if (categories.Count > MaxCategoriesPerBorrow)
            {
                errorMessage = $"一次最多借阅{MaxCategoriesPerBorrow}个分类的书籍";
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
        /// 计算到期日期
        /// </summary>
        public static DateTime CalculateDueDate(DateTime borrowDate)
        {
            return borrowDate.AddDays(BorrowDays);
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
        public static bool IsOverdue(DateTime borrowDate)
        {
            return DateTime.Now > CalculateDueDate(borrowDate);
        }

        /// <summary>
        /// 计算逾期天数
        /// </summary>
        public static int CalculateOverdueDays(DateTime borrowDate)
        {
            var dueDate = CalculateDueDate(borrowDate);
            if (DateTime.Now <= dueDate)
                return 0;
            return (int)(DateTime.Now - dueDate).TotalDays;
        }
    }

    /// <summary>
    /// 罚款计算器类
    /// </summary>
    public static class FineCalculator
    {
        /// <summary>
        /// 逾期罚款系数（书价）
        /// </summary>
        public const decimal OverduePriceRate = 0.1m;

        /// <summary>
        /// 逾期罚款系数（每天）
        /// </summary>
        public const decimal OverdueDayRate = 0.1m;

        /// <summary>
        /// 丢失赔偿系数
        /// </summary>
        public const decimal LostRate = 1.0m;

        /// <summary>
        /// 损坏赔偿系数
        /// </summary>
        public const decimal DamagedRate = 0.5m;

        /// <summary>
        /// 计算逾期罚款
        /// 规则：书籍单价*0.1 + 逾期天数*0.1
        /// </summary>
        public static decimal CalculateOverdueFine(decimal bookPrice, int overdueDays)
        {
            if (overdueDays <= 0)
                return 0;

            return bookPrice * OverduePriceRate + overdueDays * OverdueDayRate;
        }

        /// <summary>
        /// 计算丢失赔偿
        /// 规则：书籍原价
        /// </summary>
        public static decimal CalculateLostFine(decimal bookPrice)
        {
            return bookPrice * LostRate;
        }

        /// <summary>
        /// 计算损坏赔偿
        /// 规则：书籍原价 * 50%
        /// </summary>
        public static decimal CalculateDamagedFine(decimal bookPrice)
        {
            return bookPrice * DamagedRate;
        }

        /// <summary>
        /// 计算罚款金额
        /// </summary>
        public static decimal CalculateFine(FineType fineType, decimal bookPrice, int overdueDays = 0)
        {
            switch (fineType)
            {
                case FineType.Overdue:
                    return CalculateOverdueFine(bookPrice, overdueDays);
                case FineType.Lost:
                    return CalculateLostFine(bookPrice);
                case FineType.Damaged:
                    return CalculateDamagedFine(bookPrice);
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
