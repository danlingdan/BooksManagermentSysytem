namespace BooksManagermentSysytem.Models
{
    /// <summary>
    /// 用户角色枚举
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// 读者
        /// </summary>
        Reader = 1,

        /// <summary>
        /// 图书管理员
        /// </summary>
        Librarian = 2,

        /// <summary>
        /// 图书采编员
        /// </summary>
        Cataloger = 3,

        /// <summary>
        /// 系统管理员
        /// </summary>
        Admin = 4
    }

    /// <summary>
    /// 借书证状态枚举
    /// </summary>
    public enum CardState
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal,

        /// <summary>
        /// 注销
        /// </summary>
        Cancelled,

        /// <summary>
        /// 挂失
        /// </summary>
        Lost,

        /// <summary>
        /// 补办中
        /// </summary>
        Replacing
    }

    /// <summary>
    /// 读者类型枚举
    /// </summary>
    public enum ReaderType
    {
        /// <summary>
        /// 本校学生
        /// </summary>
        Student,

        /// <summary>
        /// 本校教师
        /// </summary>
        Teacher,

        /// <summary>
        /// 校外人员
        /// </summary>
        External
    }

    /// <summary>
    /// 图书状态枚举
    /// </summary>
    public enum BookStatus
    {
        /// <summary>
        /// 可借阅
        /// </summary>
        Available,

        /// <summary>
        /// 已借出
        /// </summary>
        Borrowed,

        /// <summary>
        /// 下架
        /// </summary>
        OffShelf,

        /// <summary>
        /// 已预约
        /// </summary>
        Reserved
    }

    /// <summary>
    /// 图书物理状态枚举
    /// </summary>
    public enum BookCondition
    {
        /// <summary>
        /// 完好
        /// </summary>
        Good,

        /// <summary>
        /// 损坏
        /// </summary>
        Damaged,

        /// <summary>
        /// 待修复
        /// </summary>
        Repair
    }

    /// <summary>
    /// 预约类型枚举
    /// </summary>
    public enum ReservationType
    {
        /// <summary>
        /// 借阅预约
        /// </summary>
        BorrowReserve,

        /// <summary>
        /// 新书预约
        /// </summary>
        NewBook
    }

    /// <summary>
    /// 预约状态枚举
    /// </summary>
    public enum ReservationStatus
    {
        /// <summary>
        /// 待处理
        /// </summary>
        Pending,

        /// <summary>
        /// 已完成
        /// </summary>
        Fulfilled,

        /// <summary>
        /// 已过期
        /// </summary>
        Expired,

        /// <summary>
        /// 已取消
        /// </summary>
        Cancelled
    }

    /// <summary>
    /// 罚款类型枚举
    /// </summary>
    public enum FineType
    {
        /// <summary>
        /// 逾期
        /// </summary>
        Overdue,

        /// <summary>
        /// 丢失
        /// </summary>
        Lost,

        /// <summary>
        /// 损坏
        /// </summary>
        Damaged
    }

    /// <summary>
    /// 罚款状态枚举
    /// </summary>
    public enum FineStatus
    {
        /// <summary>
        /// 未支付
        /// </summary>
        Unpaid,

        /// <summary>
        /// 已支付
        /// </summary>
        Paid
    }

    /// <summary>
    /// 库位类型枚举
    /// </summary>
    public enum LocationType
    {
        /// <summary>
        /// 普通书架
        /// </summary>
        RegularShelf,

        /// <summary>
        /// 热门区
        /// </summary>
        HotZone,

        /// <summary>
        /// 新书区
        /// </summary>
        NewBook,

        /// <summary>
        /// 工具书区
        /// </summary>
        Reference,

        /// <summary>
        /// 期刊区
        /// </summary>
        Journal,

        /// <summary>
        /// 预约书架
        /// </summary>
        ReservationShelf,

        /// <summary>
        /// 仅供查阅
        /// </summary>
        ToolOnly,

        /// <summary>
        /// 待修复区
        /// </summary>
        RepairArea
    }
}
