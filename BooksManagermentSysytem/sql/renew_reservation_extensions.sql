/* =========================================================
   图书续借与预约管理 - 数据库扩展脚本
   功能：支持图书续借、预约管理
   ========================================================= */

USE LibraryDB;
GO

SET NOCOUNT ON;

PRINT N'========================================';
PRINT N'开始执行续借与预约管理数据库扩展...';
PRINT N'========================================';
PRINT N'';

/* =========================================================
   1) 为 bookborrow 表添加续借字段
   ========================================================= */
IF NOT EXISTS (SELECT 1 FROM sys.columns 
               WHERE object_id = OBJECT_ID(N'dbo.bookborrow') 
               AND name = N'renew_count')
BEGIN
    ALTER TABLE dbo.bookborrow
    ADD renew_count INT NOT NULL CONSTRAINT DF_bookborrow_renew_count DEFAULT(0),
        last_renew_time DATETIME2(0) NULL;
    
    PRINT N'✅ 为 bookborrow 表添加续借字段：renew_count, last_renew_time';
END
ELSE
BEGIN
    PRINT N'ℹ️  bookborrow 表已包含续借字段，跳过';
END
GO

/* =========================================================
   2) 创建存储过程：获取读者类型的借阅规则
   ========================================================= */
IF OBJECT_ID('dbo.sp_GetBorrowRuleByReaderType', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetBorrowRuleByReaderType;
GO

CREATE PROCEDURE dbo.sp_GetBorrowRuleByReaderType
    @reader_type NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT rule_id, reader_type, max_borrow_count, max_category_count,
           borrow_days, max_renew_count, renew_days,
           allow_reference_books, allow_new_books, allow_hot_books,
           is_active, created_time, updated_time, remark
    FROM dbo.borrow_rules
    WHERE reader_type = @reader_type
      AND is_active = 1;
END;
GO

PRINT N'✅ 创建存储过程 sp_GetBorrowRuleByReaderType';
GO

/* =========================================================
   3) 创建存储过程：获取读者类型的处罚规则
   ========================================================= */
IF OBJECT_ID('dbo.sp_GetFineRuleByReaderType', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetFineRuleByReaderType;
GO

CREATE PROCEDURE dbo.sp_GetFineRuleByReaderType
    @reader_type NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT rule_id, reader_type, overdue_price_rate, overdue_day_rate,
           lost_rate, damaged_rate, minor_damaged_rate,
           max_overdue_fine, max_total_fine, free_overdue_days,
           is_active, created_time, updated_time, remark
    FROM dbo.fine_rule
    WHERE reader_type = @reader_type
      AND is_active = 1;
END;
GO

PRINT N'✅ 创建存储过程 sp_GetFineRuleByReaderType';
GO

/* =========================================================
   4) 创建存储过程：校验续借资格
   ========================================================= */
IF OBJECT_ID('dbo.sp_ValidateRenewEligibility', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ValidateRenewEligibility;
GO

CREATE PROCEDURE dbo.sp_ValidateRenewEligibility
    @bookborrow_id BIGINT,
    @can_renew BIT OUTPUT,
    @error_message NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @cardID NVARCHAR(20);
    DECLARE @bookID NVARCHAR(30);
    DECLARE @overdate DATETIME2(0);
    DECLARE @renew_count INT;
    DECLARE @last_renew_time DATETIME2(0);
    DECLARE @borrowdate DATETIME2(0);
    DECLARE @readertype NVARCHAR(10);
    DECLARE @max_renew_count INT;
    DECLARE @borrow_days INT;
    DECLARE @reservation_count INT;
    DECLARE @unpaid_fines DECIMAL(10,2);
    
    SET @can_renew = 0;
    SET @error_message = N'';
    
    -- 查询借阅记录
    SELECT @cardID = bb.cardID,
           @bookID = bb.bookID,
           @overdate = bb.overdate,
           @renew_count = ISNULL(bb.renew_count, 0),
           @last_renew_time = bb.last_renew_time,
           @borrowdate = bb.borrowdate,
           @readertype = r.readertype
    FROM dbo.bookborrow bb
    INNER JOIN dbo.reader r ON bb.cardID = r.cardID
    WHERE bb.bookborrow_id = @bookborrow_id;
    
    IF @@ROWCOUNT = 0
    BEGIN
        SET @error_message = N'未找到该借阅记录';
        RETURN;
    END
    
    -- 检查是否已归还
    IF @overdate IS NOT NULL
    BEGIN
        SET @error_message = N'该书籍已归还，无法续借';
        RETURN;
    END
    
    -- 获取借阅规则
    SELECT @max_renew_count = max_renew_count,
           @borrow_days = borrow_days
    FROM dbo.borrow_rules
    WHERE reader_type = @readertype
      AND is_active = 1;
    
    IF @max_renew_count IS NULL
    BEGIN
        SET @max_renew_count = 2; -- 默认值
        SET @borrow_days = 7;
    END
    
    -- 检查续借次数限制
    IF @renew_count >= @max_renew_count
    BEGIN
        SET @error_message = N'该书籍已续借' + CAST(@renew_count AS NVARCHAR) + 
                            N'次，已达到最大续借次数限制（' + CAST(@max_renew_count AS NVARCHAR) + N'次）';
        RETURN;
    END
    
    -- 检查是否逾期
    DECLARE @effective_date DATETIME2(0) = ISNULL(@last_renew_time, @borrowdate);
    DECLARE @due_date DATETIME2(0) = DATEADD(DAY, @borrow_days, @effective_date);
    
    IF SYSDATETIME() > @due_date
    BEGIN
        DECLARE @overdue_days INT = DATEDIFF(DAY, @due_date, SYSDATETIME());
        SET @error_message = N'该书籍已逾期' + CAST(@overdue_days AS NVARCHAR) + N'天，请先归还后再借阅';
        RETURN;
    END
    
    -- 检查是否有人预约此书
    SELECT @reservation_count = COUNT(*)
    FROM dbo.book_reservation
    WHERE bookID = @bookID
      AND reservation_status = N'PENDING';
    
    IF @reservation_count > 0
    BEGIN
        SET @error_message = N'该书籍已有其他读者预约，无法续借';
        RETURN;
    END
    
    -- 检查是否有未支付罚款
    SELECT @unpaid_fines = ISNULL(SUM(amount), 0)
    FROM dbo.fine
    WHERE cardID = @cardID
      AND fine_status = N'未支付';
    
    IF @unpaid_fines > 0
    BEGIN
        SET @error_message = N'您有未支付罚款 ¥' + CAST(@unpaid_fines AS NVARCHAR(20)) + N'，请先缴纳罚款后再续借';
        RETURN;
    END
    
    -- 全部校验通过
    SET @can_renew = 1;
END;
GO

PRINT N'✅ 创建存储过程 sp_ValidateRenewEligibility';
GO

/* =========================================================
   5) 创建存储过程：执行续借操作
   ========================================================= */
IF OBJECT_ID('dbo.sp_ProcessRenew', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ProcessRenew;
GO

CREATE PROCEDURE dbo.sp_ProcessRenew
    @bookborrow_id BIGINT,
    @success BIT OUTPUT,
    @error_message NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @can_renew BIT;
    
    -- 校验续借资格
    EXEC dbo.sp_ValidateRenewEligibility 
        @bookborrow_id = @bookborrow_id,
        @can_renew = @can_renew OUTPUT,
        @error_message = @error_message OUTPUT;
    
    IF @can_renew = 0
    BEGIN
        SET @success = 0;
        RETURN;
    END
    
    -- 执行续借
    UPDATE dbo.bookborrow
    SET renew_count = ISNULL(renew_count, 0) + 1,
        last_renew_time = SYSDATETIME()
    WHERE bookborrow_id = @bookborrow_id;
    
    IF @@ROWCOUNT = 0
    BEGIN
        SET @success = 0;
        SET @error_message = N'续借失败，未找到借阅记录';
        RETURN;
    END
    
    SET @success = 1;
    SET @error_message = N'';
END;
GO

PRINT N'✅ 创建存储过程 sp_ProcessRenew';
GO

/* =========================================================
   6) 创建存储过程：检查并处理过期预约
   ========================================================= */
IF OBJECT_ID('dbo.sp_ExpireReservations', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ExpireReservations;
GO

CREATE PROCEDURE dbo.sp_ExpireReservations
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @expired_count INT = 0;
    
    -- 更新过期预约状态
    UPDATE dbo.book_reservation
    SET reservation_status = N'EXPIRED',
        note = N'系统自动取消：预约已过期'
    WHERE reservation_status = N'PENDING'
      AND expire_time < SYSDATETIME();
    
    SET @expired_count = @@ROWCOUNT;
    
    -- 恢复已预约图书的状态（如果是借阅预约）
    UPDATE bi
    SET bi.current_status = N'BORROWED',
        bi.status_changed_date = SYSDATETIME()
    FROM dbo.BOOK_ITEM bi
    INNER JOIN dbo.book_reservation br ON bi.item_barcode = br.bookID
    WHERE br.reservation_status = N'EXPIRED'
      AND br.reservation_type = N'BORROW_RESERVE'
      AND bi.current_status = N'RESERVED'
      AND br.note = N'系统自动取消：预约已过期';
    
    RETURN @expired_count;
END;
GO

PRINT N'✅ 创建存储过程 sp_ExpireReservations';
GO

/* =========================================================
   7) 创建视图：读者可续借书籍列表
   ========================================================= */
IF OBJECT_ID('dbo.vw_renewable_books', 'V') IS NOT NULL
    DROP VIEW dbo.vw_renewable_books;
GO

CREATE VIEW dbo.vw_renewable_books
AS
SELECT bb.bookborrow_id,
       bb.cardID,
       bb.bookID,
       bb.borrowdate,
       bb.last_renew_time,
       ISNULL(bb.renew_count, 0) AS renew_count,
       r.readername,
       r.readertype,
       bib.bibliography_id,
       bib.bibliography_name,
       bib.ISBN,
       bc.category_code,
       bc.category_name,
       COALESCE(bi.price, bib.price, 0) AS book_price,
       -- 有效借阅日期（最后续借时间或初始借阅时间）
       CASE 
           WHEN bb.last_renew_time IS NOT NULL THEN bb.last_renew_time
           ELSE bb.borrowdate
       END AS effective_borrow_date,
       -- 当前到期日
       CASE 
           WHEN bb.last_renew_time IS NOT NULL 
           THEN DATEADD(DAY, br.borrow_days, bb.last_renew_time)
           ELSE DATEADD(DAY, br.borrow_days, bb.borrowdate)
       END AS current_due_date,
       -- 是否逾期
       CASE 
           WHEN SYSDATETIME() > CASE 
               WHEN bb.last_renew_time IS NOT NULL 
               THEN DATEADD(DAY, br.borrow_days, bb.last_renew_time)
               ELSE DATEADD(DAY, br.borrow_days, bb.borrowdate)
           END THEN 1
           ELSE 0
       END AS is_overdue,
       -- 最大续借次数
       br.max_renew_count,
       -- 续借天数
       br.renew_days,
       -- 剩余可续借次数
       br.max_renew_count - ISNULL(bb.renew_count, 0) AS remaining_renew_count
FROM dbo.bookborrow bb
INNER JOIN dbo.reader r ON bb.cardID = r.cardID
INNER JOIN dbo.BOOK_ITEM bi ON bb.bookID = bi.item_barcode
INNER JOIN dbo.BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
INNER JOIN dbo.BOOK_CATEGORY bc ON bib.category_id = bc.category_id
LEFT JOIN dbo.borrow_rules br ON r.readertype = br.reader_type AND br.is_active = 1
WHERE bb.overdate IS NULL;  -- 未归还
GO

PRINT N'✅ 创建视图 vw_renewable_books';
GO

/* =========================================================
   8) 创建视图：待处理预约列表
   ========================================================= */
IF OBJECT_ID('dbo.vw_pending_reservations', 'V') IS NOT NULL
    DROP VIEW dbo.vw_pending_reservations;
GO

CREATE VIEW dbo.vw_pending_reservations
AS
SELECT br.reservation_id,
       br.cardID,
       br.bookID,
       br.reservation_type,
       br.reservation_time,
       br.expire_time,
       r.readername,
       r.readertype,
       bib.bibliography_id,
       bib.bibliography_name,
       bib.ISBN,
       bc.category_code,
       bc.category_name,
       bi.current_status,
       -- 是否已过期
       CASE 
           WHEN br.expire_time < SYSDATETIME() THEN 1
           ELSE 0
       END AS is_expired,
       -- 剩余小时数
       DATEDIFF(HOUR, SYSDATETIME(), br.expire_time) AS hours_remaining
FROM dbo.book_reservation br
INNER JOIN dbo.reader r ON br.cardID = r.cardID
INNER JOIN dbo.BOOK_ITEM bi ON br.bookID = bi.item_barcode
INNER JOIN dbo.BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
INNER JOIN dbo.BOOK_CATEGORY bc ON bib.category_id = bc.category_id
WHERE br.reservation_status = N'PENDING';
GO

PRINT N'✅ 创建视图 vw_pending_reservations';
GO

/* =========================================================
   9) 创建索引优化查询性能
   ========================================================= */
-- 为续借查询创建索引
IF NOT EXISTS (SELECT 1 FROM sys.indexes 
               WHERE name = N'IX_bookborrow_renew' 
               AND object_id = OBJECT_ID(N'dbo.bookborrow'))
BEGIN
    CREATE INDEX IX_bookborrow_renew
    ON dbo.bookborrow(cardID, overdate)
    INCLUDE (renew_count, last_renew_time, borrowdate, bookID);
    
    PRINT N'✅ 创建索引 IX_bookborrow_renew';
END
ELSE
BEGIN
    PRINT N'ℹ️  索引 IX_bookborrow_renew 已存在，跳过';
END
GO

-- 为预约查询创建索引
IF NOT EXISTS (SELECT 1 FROM sys.indexes 
               WHERE name = N'IX_book_reservation_status_expire' 
               AND object_id = OBJECT_ID(N'dbo.book_reservation'))
BEGIN
    CREATE INDEX IX_book_reservation_status_expire
    ON dbo.book_reservation(reservation_status, expire_time)
    INCLUDE (cardID, bookID, reservation_type);
    
    PRINT N'✅ 创建索引 IX_book_reservation_status_expire';
END
ELSE
BEGIN
    PRINT N'ℹ️  索引 IX_book_reservation_status_expire 已存在，跳过';
END
GO

/* =========================================================
   10) 插入示例数据（可选）
   ========================================================= */
-- 更新现有借阅记录，添加一些续借示例
UPDATE TOP (3) dbo.bookborrow
SET renew_count = 1,
    last_renew_time = DATEADD(DAY, -5, SYSDATETIME())
WHERE overdate IS NULL
  AND renew_count = 0;

PRINT N'✅ 更新示例借阅记录，添加续借数据';
GO

-- 插入一些预约示例（如果不存在）
IF NOT EXISTS (SELECT 1 FROM dbo.book_reservation)
BEGIN
    DECLARE @cardID1 NVARCHAR(20);
    DECLARE @cardID2 NVARCHAR(20);
    DECLARE @bookID1 NVARCHAR(30);
    DECLARE @bookID2 NVARCHAR(30);
    
    -- 获取一些示例读者和书籍
    SELECT TOP 1 @cardID1 = cardID FROM dbo.reader WHERE readertype = N'本校学生' ORDER BY cardID;
    SELECT TOP 1 @cardID2 = cardID FROM dbo.reader WHERE readertype = N'本校教师' ORDER BY cardID;
    SELECT TOP 1 @bookID1 = item_barcode FROM dbo.BOOK_ITEM WHERE current_status = N'BORROWED' ORDER BY item_barcode;
    SELECT TOP 1 @bookID2 = item_barcode FROM dbo.BOOK_ITEM WHERE current_status = N'AVAILABLE' 
           AND location_id IN (SELECT location_id FROM dbo.STORAGE_LOCATION WHERE location_type = N'NEW_BOOK')
           ORDER BY item_barcode;
    
    IF @cardID1 IS NOT NULL AND @bookID1 IS NOT NULL
    BEGIN
        INSERT INTO dbo.book_reservation (cardID, bookID, reservation_type, expire_time, reservation_status)
        VALUES (@cardID1, @bookID1, N'BORROW_RESERVE', DATEADD(DAY, 3, SYSDATETIME()), N'PENDING');
        
        PRINT N'✅ 插入借阅预约示例';
    END
    
    IF @cardID2 IS NOT NULL AND @bookID2 IS NOT NULL
    BEGIN
        INSERT INTO dbo.book_reservation (cardID, bookID, reservation_type, expire_time, reservation_status)
        VALUES (@cardID2, @bookID2, N'NEW_BOOK', DATEADD(DAY, 7, SYSDATETIME()), N'PENDING');
        
        PRINT N'✅ 插入新书预约示例';
    END
END
GO

PRINT N'';
PRINT N'========================================';
PRINT N'✅ 续借与预约管理数据库扩展完成！';
PRINT N'========================================';
PRINT N'';
PRINT N'已完成的操作：';
PRINT N'  1. 为 bookborrow 表添加续借字段';
PRINT N'  2. 创建借阅规则和处罚规则查询存储过程';
PRINT N'  3. 创建续借资格校验和执行存储过程';
PRINT N'  4. 创建预约过期处理存储过程';
PRINT N'  5. 创建可续借书籍和待处理预约视图';
PRINT N'  6. 创建查询性能优化索引';
PRINT N'  7. 插入示例数据';
PRINT N'';
GO
