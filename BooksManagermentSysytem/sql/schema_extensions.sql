/* =========================================================
   图书馆管理系统 - 数据库扩展脚本
   扩展内容：用户认证、图书预约、搜索日志
   ========================================================= */

USE LibraryDB;
GO

SET NOCOUNT ON;

-- 1) 系统用户表 [system_user]
------------------------------------------------------------
IF OBJECT_ID(N'dbo.[system_user]', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.[system_user](
        user_id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        username NVARCHAR(50) NOT NULL UNIQUE,
        password_hash NVARCHAR(256) NOT NULL,
        salt NVARCHAR(64) NOT NULL,
        user_role NVARCHAR(20) NOT NULL,
        cardID NVARCHAR(20) NULL,
        windows_account NVARCHAR(100) NULL,
        display_name NVARCHAR(50) NOT NULL,
        is_active BIT NOT NULL CONSTRAINT DF_system_user_active DEFAULT(1),
        created_time DATETIME2(0) NOT NULL CONSTRAINT DF_system_user_created DEFAULT(SYSDATETIME()),
        last_login_time DATETIME2(0) NULL,
        CONSTRAINT CK_system_user_role CHECK (user_role IN (N'Reader', N'Librarian', N'Cataloger', N'Admin')),
        CONSTRAINT FK_system_user_reader FOREIGN KEY(cardID) REFERENCES dbo.reader(cardID)
    );

    CREATE INDEX IX_system_user_windows
    ON dbo.[system_user](windows_account)
    WHERE windows_account IS NOT NULL;

    PRINT N'✅ 创建表 [system_user]';
END
GO

/* =========================================================
   2) 图书预约表 book_reservation - 读者预约借阅
   ========================================================= */
IF OBJECT_ID('dbo.book_reservation', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.book_reservation(
        reservation_id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        cardID NVARCHAR(20) NOT NULL,
        bookID NVARCHAR(30) NOT NULL,                    -- 对应 BOOK_ITEM.item_barcode
        reservation_type NVARCHAR(20) NOT NULL,          -- BORROW_RESERVE=借阅预约, NEW_BOOK=新书预约
        reservation_time DATETIME2(0) NOT NULL CONSTRAINT DF_book_reservation_time DEFAULT(SYSDATETIME()),
        expire_time DATETIME2(0) NOT NULL,               -- 预约过期时间（借阅预约3天，新书预约根据到货时间）
        pickup_time DATETIME2(0) NULL,                   -- 实际取书时间
        reservation_status NVARCHAR(20) NOT NULL CONSTRAINT DF_book_reservation_status DEFAULT(N'PENDING'),
        note NVARCHAR(200) NULL,
        CONSTRAINT CK_book_reservation_type CHECK (reservation_type IN (N'BORROW_RESERVE', N'NEW_BOOK')),
        CONSTRAINT CK_book_reservation_status CHECK (reservation_status IN (N'PENDING', N'FULFILLED', N'EXPIRED', N'CANCELLED')),
        CONSTRAINT FK_book_reservation_reader FOREIGN KEY(cardID) REFERENCES dbo.reader(cardID),
        CONSTRAINT FK_book_reservation_item FOREIGN KEY(bookID) REFERENCES dbo.BOOK_ITEM(item_barcode)
    );
    
    -- 同一读者对同一本书只能有一条有效预约
    CREATE UNIQUE INDEX UX_book_reservation_active 
    ON dbo.book_reservation(cardID, bookID) 
    WHERE reservation_status = N'PENDING';
    
    PRINT N'✅ 创建表 book_reservation';
END
GO

------------------------------------------------------------
-- 3) 搜索日志表 search_log（外键引用也要改）
------------------------------------------------------------
IF OBJECT_ID(N'dbo.search_log', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.search_log(
        log_id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        search_keyword NVARCHAR(200) NULL,
        bibliography_id INT NULL,
        item_barcode NVARCHAR(30) NULL,
        search_time DATETIME2(0) NOT NULL CONSTRAINT DF_search_log_time DEFAULT(SYSDATETIME()),
        user_id INT NULL,
        CONSTRAINT FK_search_log_bib  FOREIGN KEY(bibliography_id) REFERENCES dbo.BIBLIOGRAPHY(bibliography_id),
        CONSTRAINT FK_search_log_user FOREIGN KEY(user_id)         REFERENCES dbo.[system_user](user_id)
    );

    CREATE INDEX IX_search_log_date_bib ON dbo.search_log(search_time, bibliography_id);

    PRINT N'✅ 创建表 search_log';
END
GO

/* =========================================================
   4) 新书上架跟踪表 new_book_tracking - 新书区3个月自动移出
   ========================================================= */
IF OBJECT_ID('dbo.new_book_tracking', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.new_book_tracking(
        tracking_id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        item_barcode NVARCHAR(30) NOT NULL UNIQUE,
        shelve_date DATE NOT NULL,                        -- 上架日期
        original_location_id INT NULL,                    -- 原始位置（移出新书区后恢复）
        is_in_new_zone BIT NOT NULL CONSTRAINT DF_new_book_tracking_zone DEFAULT(1),
        moved_out_date DATE NULL,                         -- 移出新书区日期
        CONSTRAINT FK_new_book_tracking_item FOREIGN KEY(item_barcode) REFERENCES dbo.BOOK_ITEM(item_barcode)
    );
    
    PRINT N'✅ 创建表 new_book_tracking';
END
GO

------------------------------------------------------------
-- 5) 插入默认管理员（表名要改）
-- 另外：你原来的 hash=8c6976... 实际是 SHA256('admin')，不是 admin123
------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM dbo.[system_user] WHERE username = N'admin')
BEGIN
    DECLARE @salt NVARCHAR(64) = N'default_salt';
    DECLARE @pwd  NVARCHAR(100) = N'admin123';

    INSERT INTO dbo.[system_user](username, password_hash, salt, user_role, display_name, windows_account)
    VALUES (
        N'admin',
        CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', CONVERT(VARBINARY(4000), @pwd + @salt)), 2),
        @salt,
        N'Admin',
        N'系统管理员',
        NULL
    );

    PRINT N'✅ 创建默认管理员账户 admin';
END
GO

/* =========================================================
   6) 创建视图：热门图书统计（每日搜索量）
   ========================================================= */
IF OBJECT_ID('dbo.vw_hot_books', 'V') IS NOT NULL
    DROP VIEW dbo.vw_hot_books;
GO

CREATE VIEW dbo.vw_hot_books
AS
SELECT 
    b.bibliography_id,
    b.bibliography_name,
    b.ISBN,
    COUNT(*) AS search_count_today,
    CAST(GETDATE() AS DATE) AS stat_date
FROM dbo.search_log sl
INNER JOIN dbo.BIBLIOGRAPHY b ON sl.bibliography_id = b.bibliography_id
WHERE CAST(sl.search_time AS DATE) = CAST(GETDATE() AS DATE)
GROUP BY b.bibliography_id, b.bibliography_name, b.ISBN
HAVING COUNT(*) >= 10;
GO

PRINT N'✅ 创建视图 vw_hot_books';
GO

/* =========================================================
   7) 创建视图：读者未还书籍明细
   ========================================================= */
IF OBJECT_ID('dbo.vw_reader_borrowed_books', 'V') IS NOT NULL
    DROP VIEW dbo.vw_reader_borrowed_books;
GO

CREATE VIEW dbo.vw_reader_borrowed_books
AS
SELECT 
    r.cardID,
    r.readername,
    r.readertype,
    rc.state AS card_state,
    rc.overdate AS card_expire_date,
    bb.bookborrow_id,
    bb.bookID,
    bi.bibliography_id,
    bib.bibliography_name,
    bib.ISBN,
    bc.category_code,
    bc.category_name,
    bb.borrowdate,
    DATEADD(DAY, 7, bb.borrowdate) AS due_date,
    CASE 
        WHEN GETDATE() > DATEADD(DAY, 7, bb.borrowdate) THEN 1 
        ELSE 0 
    END AS is_overdue,
    CASE 
        WHEN GETDATE() > DATEADD(DAY, 7, bb.borrowdate) 
        THEN DATEDIFF(DAY, DATEADD(DAY, 7, bb.borrowdate), GETDATE()) 
        ELSE 0 
    END AS overdue_days,
    COALESCE(bi.price, bib.price, 0) AS book_price
FROM dbo.reader r
INNER JOIN dbo.readcard rc ON r.cardID = rc.cardID
INNER JOIN dbo.bookborrow bb ON r.cardID = bb.cardID
INNER JOIN dbo.BOOK_ITEM bi ON bb.bookID = bi.item_barcode
INNER JOIN dbo.BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
INNER JOIN dbo.BOOK_CATEGORY bc ON bib.category_id = bc.category_id
WHERE bb.overdate IS NULL;  -- 未归还
GO

PRINT N'✅ 创建视图 vw_reader_borrowed_books';
GO

/* =========================================================
   8) 创建视图：读者待支付罚款
   ========================================================= */
IF OBJECT_ID('dbo.vw_reader_unpaid_fines', 'V') IS NOT NULL
    DROP VIEW dbo.vw_reader_unpaid_fines;
GO

CREATE VIEW dbo.vw_reader_unpaid_fines
AS
SELECT 
    r.cardID,
    r.readername,
    f.fine_id,
    f.reason,
    f.amount,
    f.created_time,
    f.fine_status
FROM dbo.reader r
INNER JOIN dbo.fine f ON r.cardID = f.cardID
WHERE f.fine_status = N'未支付';
GO

PRINT N'✅ 创建视图 vw_reader_unpaid_fines';
GO

/* =========================================================
   9) 创建存储过程：计算罚款金额
   规则：逾期罚款 = 书籍单价*0.1 + 逾期天数*0.1
         丢失赔偿 = 书籍原价
         损坏赔偿 = 书籍原价*0.5
   ========================================================= */
IF OBJECT_ID('dbo.sp_calculate_fine', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_calculate_fine;
GO

CREATE PROCEDURE dbo.sp_calculate_fine
    @bookID NVARCHAR(30),
    @fineType NVARCHAR(20),  -- 'OVERDUE', 'LOST', 'DAMAGED'
    @overdueDays INT = 0,
    @fineAmount DECIMAL(10,2) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @bookPrice DECIMAL(10,2);
    
    -- 获取书籍价格（优先使用馆藏价格，其次书目价格）
    SELECT @bookPrice = COALESCE(bi.price, bib.price, 0)
    FROM dbo.BOOK_ITEM bi
    INNER JOIN dbo.BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
    WHERE bi.item_barcode = @bookID;
    
    IF @bookPrice IS NULL
        SET @bookPrice = 0;
    
    SET @fineAmount = CASE @fineType
        WHEN N'OVERDUE' THEN @bookPrice * 0.1 + @overdueDays * 0.1
        WHEN N'LOST' THEN @bookPrice
        WHEN N'DAMAGED' THEN @bookPrice * 0.5
        ELSE 0
    END;
END;
GO

PRINT N'✅ 创建存储过程 sp_calculate_fine';
GO

/* =========================================================
   10) 创建存储过程：生成借书证号
   格式：BRW-年份-类别码-顺序号(6位)
   ========================================================= */
IF OBJECT_ID('dbo.sp_generate_card_id', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_generate_card_id;
GO

CREATE PROCEDURE dbo.sp_generate_card_id
    @readerType NVARCHAR(10),
    @cardID NVARCHAR(20) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @year CHAR(4) = CAST(YEAR(GETDATE()) AS CHAR(4));
    DECLARE @typeCode CHAR(1);
    DECLARE @seqNum INT;
    DECLARE @prefix NVARCHAR(15);
    
    -- 确定类别码
    SET @typeCode = CASE @readerType
        WHEN N'本校学生' THEN '1'
        WHEN N'本校教师' THEN '2'
        WHEN N'校外人员' THEN '3'
        ELSE '1'
    END;
    
    SET @prefix = N'BRW-' + @year + N'-' + @typeCode + N'-';
    
    -- 获取当前最大序号
    SELECT @seqNum = ISNULL(MAX(CAST(RIGHT(cardID, 6) AS INT)), 0) + 1
    FROM dbo.readcard
    WHERE cardID LIKE @prefix + '%';
    
    SET @cardID = @prefix + RIGHT('000000' + CAST(@seqNum AS VARCHAR(6)), 6);
END;
GO

PRINT N'✅ 创建存储过程 sp_generate_card_id';
GO

PRINT N'';
PRINT N'========================================';
PRINT N'✅ 数据库扩展脚本执行完成！';
PRINT N'========================================';
GO
