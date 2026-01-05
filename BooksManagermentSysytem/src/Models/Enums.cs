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

    /// <summary>
    /// 权限代码枚举 - 功能权限精细化管理
    /// </summary>
    public enum PermissionCode
    {
        /// <summary>
        /// 图书编目
        /// </summary>
        BOOK_CATALOG,

        /// <summary>
        /// 删除书目
        /// </summary>
        BOOK_CATALOG_DELETE,

        /// <summary>
        /// 馆藏管理
        /// </summary>
        BOOK_ITEM_MANAGE,

        /// <summary>
        /// 分类管理
        /// </summary>
        BOOK_CATEGORY_MANAGE,

        /// <summary>
        /// 库位管理
        /// </summary>
        BOOK_LOCATION_MANAGE,

        /// <summary>
        /// 读者信息查看
        /// </summary>
        READER_INFO_VIEW,

        /// <summary>
        /// 读者信息修改
        /// </summary>
        READER_INFO_MODIFY,

        /// <summary>
        /// 借书证管理
        /// </summary>
        READER_CARD_MANAGE,

        /// <summary>
        /// 借阅操作
        /// </summary>
        BORROW_OPERATION,

        /// <summary>
        /// 续借操作
        /// </summary>
        BORROW_RENEW,

        /// <summary>
        /// 预约管理
        /// </summary>
        RESERVATION_MANAGE,

        /// <summary>
        /// 借阅规则设置
        /// </summary>
        BORROW_RULE_SETTING,

        /// <summary>
        /// 处罚规则设置
        /// </summary>
        FINE_RULE_SETTING,

        /// <summary>
        /// 罚款管理
        /// </summary>
        FINE_MANAGE,

        /// <summary>
        /// 用户管理
        /// </summary>
        USER_MANAGE,

        /// <summary>
        /// 角色权限管理
        /// </summary>
        ROLE_PERMISSION_MANAGE,

        /// <summary>
        /// 系统日志查看
        /// </summary>
        SYSTEM_LOG_VIEW,

        /// <summary>
        /// 系统配置
        /// </summary>
        SYSTEM_CONFIG,

        /// <summary>
        /// 借阅统计报表
        /// </summary>
        REPORT_BORROW_STATS,

        /// <summary>
        /// 读者统计报表
        /// </summary>
        REPORT_READER_STATS,

        /// <summary>
        /// 图书统计报表
        /// </summary>
        REPORT_BOOK_STATS
    }

    /// <summary>
    /// 消息类型枚举
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// 系统消息
        /// </summary>
        System,

        /// <summary>
        /// 逾期提醒
        /// </summary>
        OverdueReminder,

        /// <summary>
        /// 即将逾期提醒
        /// </summary>
        SoonDueReminder,

        /// <summary>
        /// 预约到书通知
        /// </summary>
        ReservationReady,

        /// <summary>
        /// 预约过期通知
        /// </summary>
        ReservationExpired,

        /// <summary>
        /// 罚款通知
        /// </summary>
        FineNotice,

        /// <summary>
        /// 借书证到期提醒
        /// </summary>
        CardExpireReminder,

        /// <summary>
        /// 系统公告
        /// </summary>
        Announcement
    }

    /// <summary>
    /// 消息状态枚举
    /// </summary>
    public enum MessageStatus
    {
        /// <summary>
        /// 未读
        /// </summary>
        Unread,

        /// <summary>
        /// 已读
        /// </summary>
        Read,

        /// <summary>
        /// 已删除
        /// </summary>
        Deleted
    }

    /// <summary>
    /// 消息优先级枚举
    /// </summary>
    public enum MessagePriority
    {
        /// <summary>
        /// 低
        /// </summary>
        Low,

        /// <summary>
        /// 普通
        /// </summary>
        Normal,

        /// <summary>
        /// 高
        /// </summary>
        High,

        /// <summary>
        /// 紧急
        /// </summary>
        Urgent
    }

    /// <summary>
    /// 通知渠道枚举
    /// </summary>
    public enum NotificationChannel
    {
        /// <summary>
        /// 系统内消息
        /// </summary>
        InApp,

        /// <summary>
        /// 邮件
        /// </summary>
        Email,

        /// <summary>
        /// 短信
        /// </summary>
        SMS
    }
}
