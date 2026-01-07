/* =========================================================
   消息通知系统 - 数据库表结构
   创建时间：2025年1月
   功能：支持系统消息、逾期提醒等通知功能
   ========================================================= */

USE LibraryDB;
GO

------------------------------------------------------------
-- 1) 系统消息表 system_message
------------------------------------------------------------
IF OBJECT_ID('dbo.system_message','U') IS NOT NULL 
    DROP TABLE dbo.system_message;
GO

CREATE TABLE dbo.system_message(
    message_id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    cardID NVARCHAR(20) NOT NULL,                    -- 接收者借书证号
    message_type NVARCHAR(30) NOT NULL,              -- 消息类型
    title NVARCHAR(200) NOT NULL,                    -- 消息标题
    content NVARCHAR(MAX) NOT NULL,                  -- 消息内容
    priority NVARCHAR(20) NOT NULL DEFAULT(N'Normal'), -- 优先级
    [status] NVARCHAR(20) NOT NULL DEFAULT(N'Unread'), -- 状态
    created_time DATETIME2(0) NOT NULL DEFAULT(SYSDATETIME()), -- 创建时间
    read_time DATETIME2(0) NULL,                     -- 阅读时间
    related_id NVARCHAR(50) NULL,                    -- 关联ID（如借阅ID、罚款ID等）
    related_type NVARCHAR(30) NULL,                  -- 关联类型

    CONSTRAINT FK_system_message_reader 
        FOREIGN KEY(cardID) REFERENCES dbo.reader(cardID),
    
    CONSTRAINT CK_system_message_type CHECK (message_type IN 
        (N'System', N'OverdueReminder', N'SoonDueReminder', N'ReservationReady', 
         N'ReservationExpired', N'FineNotice', N'CardExpireReminder', N'Announcement')),
    
    CONSTRAINT CK_system_message_priority CHECK (priority IN 
        (N'Low', N'Normal', N'High', N'Urgent')),
    
    CONSTRAINT CK_system_message_status CHECK ([status] IN 
        (N'Unread', N'Read', N'Deleted'))
);
GO

-- 创建索引以优化查询
CREATE INDEX IX_system_message_cardID_status 
    ON dbo.system_message(cardID, [status], created_time DESC);

CREATE INDEX IX_system_message_type_time 
    ON dbo.system_message(message_type, created_time DESC);
GO

------------------------------------------------------------
-- 2) 逾期提醒记录表 overdue_reminder_log
------------------------------------------------------------
IF OBJECT_ID('dbo.overdue_reminder_log','U') IS NOT NULL 
    DROP TABLE dbo.overdue_reminder_log;
GO

CREATE TABLE dbo.overdue_reminder_log(
    reminder_id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    cardID NVARCHAR(20) NOT NULL,                    -- 读者借书证号
    bookID NVARCHAR(30) NOT NULL,                    -- 图书馆藏码
    borrow_date DATETIME2(0) NOT NULL,               -- 借阅日期
    due_date DATETIME2(0) NOT NULL,                  -- 应还日期
    overdue_days INT NOT NULL,                       -- 逾期天数（0表示即将逾期）
    estimated_fine DECIMAL(10,2) NOT NULL DEFAULT(0), -- 预计罚款
    reminder_time DATETIME2(0) NOT NULL DEFAULT(SYSDATETIME()), -- 提醒时间
    is_sent BIT NOT NULL DEFAULT(0),                 -- 是否已发送
    channel NVARCHAR(20) NOT NULL DEFAULT(N'InApp'), -- 通知渠道
    
    CONSTRAINT FK_overdue_reminder_reader 
        FOREIGN KEY(cardID) REFERENCES dbo.reader(cardID),
    
    CONSTRAINT FK_overdue_reminder_book 
        FOREIGN KEY(bookID) REFERENCES dbo.BOOK_ITEM(item_barcode),
    
    CONSTRAINT CK_overdue_reminder_days CHECK (overdue_days >= 0),
    CONSTRAINT CK_overdue_reminder_fine CHECK (estimated_fine >= 0),
    CONSTRAINT CK_overdue_reminder_channel CHECK (channel IN 
        (N'InApp', N'Email', N'SMS'))
);
GO

-- 创建索引
CREATE INDEX IX_overdue_reminder_cardID 
    ON dbo.overdue_reminder_log(cardID, reminder_time DESC);

CREATE INDEX IX_overdue_reminder_sent 
    ON dbo.overdue_reminder_log(is_sent, reminder_time DESC);
GO

------------------------------------------------------------
-- 3) 消息模板表（可选，用于标准化消息内容）
------------------------------------------------------------
IF OBJECT_ID('dbo.message_template','U') IS NOT NULL 
    DROP TABLE dbo.message_template;
GO

CREATE TABLE dbo.message_template(
    template_id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    template_code NVARCHAR(50) NOT NULL UNIQUE,      -- 模板代码
    message_type NVARCHAR(30) NOT NULL,              -- 消息类型
    title_template NVARCHAR(200) NOT NULL,           -- 标题模板
    content_template NVARCHAR(MAX) NOT NULL,         -- 内容模板
    priority NVARCHAR(20) NOT NULL DEFAULT(N'Normal'), -- 默认优先级
    is_active BIT NOT NULL DEFAULT(1),               -- 是否启用
    created_time DATETIME2(0) NOT NULL DEFAULT(SYSDATETIME()),
    updated_time DATETIME2(0) NULL,
    
    CONSTRAINT CK_message_template_type CHECK (message_type IN 
        (N'System', N'OverdueReminder', N'SoonDueReminder', N'ReservationReady', 
         N'ReservationExpired', N'FineNotice', N'CardExpireReminder', N'Announcement'))
);
GO

------------------------------------------------------------
-- 4) 插入默认消息模板
------------------------------------------------------------
INSERT INTO dbo.message_template(template_code, message_type, title_template, content_template, priority) VALUES
(N'OVERDUE_REMINDER', N'OverdueReminder', 
 N'逾期提醒：您有{0}本图书逾期未还', 
 N'尊敬的读者，您借阅的图书《{BookName}》已逾期{OverdueDays}天，请尽快归还。应还日期：{DueDate}，预计罚款：¥{EstimatedFine}。', 
 N'High'),

(N'SOON_DUE_REMINDER', N'SoonDueReminder', 
 N'即将到期提醒：您有图书即将到期', 
 N'尊敬的读者，您借阅的图书《{BookName}》将在{DaysLeft}天后到期（{DueDate}），请注意及时归还或续借。', 
 N'Normal'),

(N'RESERVATION_READY', N'ReservationReady', 
 N'预约到书通知：您预约的图书已可取', 
 N'尊敬的读者，您预约的图书《{BookName}》已归还，现已准备好供您借阅。请于{ExpireDate}前到馆取书，逾期预约将自动取消。', 
 N'Normal'),

(N'RESERVATION_EXPIRED', N'ReservationExpired', 
 N'预约过期通知', 
 N'尊敬的读者，您预约的图书《{BookName}》已超过取书期限，预约已自动取消。', 
 N'Low'),

(N'FINE_NOTICE', N'FineNotice', 
 N'罚款通知', 
 N'尊敬的读者，您有一笔罚款待支付。原因：{Reason}，金额：¥{Amount}。请尽快到图书馆服务台缴纳。', 
 N'High'),

(N'CARD_EXPIRE_REMINDER', N'CardExpireReminder', 
 N'借书证到期提醒', 
 N'尊敬的读者，您的借书证将在{DaysLeft}天后到期（{ExpireDate}），请及时办理续期手续。', 
 N'Normal'),

(N'SYSTEM_ANNOUNCEMENT', N'Announcement', 
 N'系统公告：{Title}', 
 N'{Content}', 
 N'Normal');
GO

PRINT N'✅ 消息通知系统表结构创建完成！';
PRINT N'   - system_message: 系统消息表';
PRINT N'   - overdue_reminder_log: 逾期提醒记录表';
PRINT N'   - message_template: 消息模板表';
GO
