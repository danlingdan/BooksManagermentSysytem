using System;

namespace BooksManagermentSysytem.Models
{
    /// <summary>
    /// 系统用户实体类
    /// </summary>
    public class SystemUser
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public UserRole Role { get; set; }
        public string CardID { get; set; }
        public string WindowsAccount { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 检查用户是否有指定权限
        /// </summary>
        public bool HasPermission(UserRole requiredRole)
        {
            // Admin 拥有所有权限
            if (Role == UserRole.Admin)
                return true;

            return Role == requiredRole;
        }

        /// <summary>
        /// 检查是否是管理员
        /// </summary>
        public bool IsAdmin => Role == UserRole.Admin;

        /// <summary>
        /// 检查是否是图书管理员
        /// </summary>
        public bool IsLibrarian => Role == UserRole.Librarian || Role == UserRole.Admin;

        /// <summary>
        /// 检查是否是采编员
        /// </summary>
        public bool IsCataloger => Role == UserRole.Cataloger || Role == UserRole.Admin;

        /// <summary>
        /// 检查是否是读者
        /// </summary>
        public bool IsReader => Role == UserRole.Reader;
    }

    /// <summary>
    /// 读者实体类
    /// </summary>
    public class Reader
    {
        public string CardID { get; set; }
        public string ReaderName { get; set; }
        public string ReaderType { get; set; }
        public string Unit { get; set; }
        public string Number { get; set; }
        public string BorrowedBooksInfo { get; set; }
        public DateTime? BorrowedDate { get; set; }
        public string BorrowNote { get; set; }

        // 关联的借书证信息
        public DateTime StartDate { get; set; }
        public DateTime OverDate { get; set; }
        public string CardState { get; set; }

        /// <summary>
        /// 检查借书证是否有效
        /// </summary>
        public bool IsCardValid()
        {
            return CardState == "正常" && OverDate >= DateTime.Today;
        }

        /// <summary>
        /// 获取读者类型枚举
        /// </summary>
        public ReaderType GetReaderTypeEnum()
        {
            switch (ReaderType)
            {
                case "本校学生": return Models.ReaderType.Student;
                case "本校教师": return Models.ReaderType.Teacher;
                case "校外人员": return Models.ReaderType.External;
                default: return Models.ReaderType.Student;
            }
        }
    }

    /// <summary>
    /// 借阅记录实体类
    /// </summary>
    public class BorrowRecord
    {
        public long BorrowRecordId { get; set; }
        public string CardID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? OverDate { get; set; }
        public string BComplete { get; set; }
        public string AddNote { get; set; }
    }

    /// <summary>
    /// 借阅明细实体类
    /// </summary>
    public class BookBorrow
    {
        public long BookBorrowId { get; set; }
        public long? BorrowRecordId { get; set; }
        public string CardID { get; set; }
        public string BookID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? OverDate { get; set; }
        public string AddNote { get; set; }

        // 扩展信息
        public string BookName { get; set; }
        public string ISBN { get; set; }
        public string CategoryCode { get; set; }
        public decimal BookPrice { get; set; }
        public DateTime DueDate => BorrowDate.AddDays(7);
        public bool IsOverdue => OverDate == null && DateTime.Now > DueDate;
        public int OverdueDays => IsOverdue ? (int)(DateTime.Now - DueDate).TotalDays : 0;
    }

    /// <summary>
    /// 图书预约实体类
    /// </summary>
    public class BookReservation
    {
        public long ReservationId { get; set; }
        public string CardID { get; set; }
        public string BookID { get; set; }
        public string ReservationType { get; set; }
        public DateTime ReservationTime { get; set; }
        public DateTime ExpireTime { get; set; }
        public DateTime? PickupTime { get; set; }
        public string ReservationStatus { get; set; }
        public string Note { get; set; }

        // 扩展信息
        public string BookName { get; set; }
        public string ISBN { get; set; }
    }

    /// <summary>
    /// 罚款记录实体类
    /// </summary>
    public class Fine
    {
        public long FineId { get; set; }
        public string CardID { get; set; }
        public string ReaderName { get; set; }
        public string Reason { get; set; }
        public decimal Amount { get; set; }
        public string FineStatus { get; set; }
        public DateTime CreatedTime { get; set; }
    }

    /// <summary>
    /// 书目实体类
    /// </summary>
    public class Bibliography
    {
        public int BibliographyId { get; set; }
        public string ISBN { get; set; }
        public string BibliographyName { get; set; }
        public string Publish { get; set; }
        public DateTime? PublishDate { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public decimal? Price { get; set; }
        public DateTime CreateTime { get; set; }

        // 扩展信息
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string Authors { get; set; }
    }

    /// <summary>
    /// 馆藏实体类
    /// </summary>
    public class BookItem
    {
        public string ItemBarcode { get; set; }
        public int BibliographyId { get; set; }
        public string CurrentStatus { get; set; }
        public int LocationId { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public decimal? Price { get; set; }
        public string PhysicalCondition { get; set; }
        public DateTime StatusChangedDate { get; set; }

        // 扩展信息
        public string BookName { get; set; }
        public string ISBN { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string CategoryCode { get; set; }
    }

    /// <summary>
    /// 图书分类实体类
    /// </summary>
    public class BookCategory
    {
        public int CategoryId { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public int? ParentCategoryId { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }

    /// <summary>
    /// 库位实体类
    /// </summary>
    public class StorageLocation
    {
        public int LocationId { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public int? ParentLocationId { get; set; }
        public string LocationType { get; set; }
        public int MaxCapacity { get; set; }
        public int CurrentQuantity { get; set; }
        public string Status { get; set; }
    }
}
