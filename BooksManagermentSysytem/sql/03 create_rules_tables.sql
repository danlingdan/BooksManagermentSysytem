-- =============================================
-- 借阅规则表和处罚规则表创建脚本
-- 用于支持不同读者类型的灵活规则配置
-- =============================================

USE LibraryDB;
GO

-- =============================================
-- 1. 借阅规则表 (BORROW_RULES)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'BORROW_RULES')
BEGIN
    CREATE TABLE BORROW_RULES (
        rule_id INT IDENTITY(1,1) PRIMARY KEY,
        reader_type NVARCHAR(20) NOT NULL,           -- 读者类型：本校学生/本校教师/校外人员
        max_borrow_count INT NOT NULL DEFAULT 3,     -- 最大借阅数量
        max_category_count INT NOT NULL DEFAULT 2,   -- 最大借阅分类数
        borrow_days INT NOT NULL DEFAULT 7,          -- 借阅天数
        max_renew_count INT NOT NULL DEFAULT 2,      -- 最大续借次数
        renew_days INT NOT NULL DEFAULT 7,           -- 每次续借天数
        allow_reference_books BIT NOT NULL DEFAULT 0, -- 是否允许借工具书（0=否，1=是）
        allow_new_books BIT NOT NULL DEFAULT 1,      -- 是否允许借新书（0=否，1=是）
        allow_hot_books BIT NOT NULL DEFAULT 1,      -- 是否允许借热门书（0=否，1=是）
        is_active BIT NOT NULL DEFAULT 1,            -- 是否启用
        created_time DATETIME NOT NULL DEFAULT GETDATE(),
        updated_time DATETIME NULL,
        remark NVARCHAR(500) NULL,                   -- 备注说明
        
        CONSTRAINT UQ_BORROW_RULES_READER_TYPE UNIQUE (reader_type),
        CONSTRAINT CK_BORROW_RULES_MAX_BORROW CHECK (max_borrow_count > 0 AND max_borrow_count <= 10),
        CONSTRAINT CK_BORROW_RULES_MAX_CATEGORY CHECK (max_category_count > 0 AND max_category_count <= 5),
        CONSTRAINT CK_BORROW_RULES_BORROW_DAYS CHECK (borrow_days > 0 AND borrow_days <= 90),
        CONSTRAINT CK_BORROW_RULES_RENEW_COUNT CHECK (max_renew_count >= 0 AND max_renew_count <= 5),
        CONSTRAINT CK_BORROW_RULES_RENEW_DAYS CHECK (renew_days > 0 AND renew_days <= 30)
    );
    
    PRINT '✓ 借阅规则表 BORROW_RULES 创建成功';
END
ELSE
BEGIN
    PRINT '! 借阅规则表 BORROW_RULES 已存在';
END
GO

-- =============================================
-- 2. 处罚规则表 (FINE_RULES)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'FINE_RULES')
BEGIN
    CREATE TABLE FINE_RULES (
        rule_id INT IDENTITY(1,1) PRIMARY KEY,
        reader_type NVARCHAR(20) NOT NULL,           -- 读者类型：本校学生/本校教师/校外人员
        overdue_price_rate DECIMAL(5,4) NOT NULL DEFAULT 0.1000,  -- 逾期罚款系数（书价）
        overdue_day_rate DECIMAL(5,2) NOT NULL DEFAULT 0.10,      -- 逾期罚款系数（每天）
        lost_rate DECIMAL(5,4) NOT NULL DEFAULT 1.0000,           -- 丢失赔偿系数
        damaged_rate DECIMAL(5,4) NOT NULL DEFAULT 0.5000,        -- 损坏赔偿系数
        minor_damaged_rate DECIMAL(5,4) NOT NULL DEFAULT 0.2500,  -- 轻微破损赔偿系数
        max_overdue_fine DECIMAL(10,2) NULL,         -- 最大逾期罚款（NULL=无限制）
        max_total_fine DECIMAL(10,2) NULL,           -- 最大总罚款（NULL=无限制）
        free_overdue_days INT NOT NULL DEFAULT 0,    -- 免罚天数（宽限期）
        is_active BIT NOT NULL DEFAULT 1,            -- 是否启用
        created_time DATETIME NOT NULL DEFAULT GETDATE(),
        updated_time DATETIME NULL,
        remark NVARCHAR(500) NULL,                   -- 备注说明
        
        CONSTRAINT UQ_FINE_RULES_READER_TYPE UNIQUE (reader_type),
        CONSTRAINT CK_FINE_RULES_OVERDUE_PRICE_RATE CHECK (overdue_price_rate >= 0 AND overdue_price_rate <= 1),
        CONSTRAINT CK_FINE_RULES_OVERDUE_DAY_RATE CHECK (overdue_day_rate >= 0 AND overdue_day_rate <= 10),
        CONSTRAINT CK_FINE_RULES_LOST_RATE CHECK (lost_rate >= 0.5 AND lost_rate <= 3),
        CONSTRAINT CK_FINE_RULES_DAMAGED_RATE CHECK (damaged_rate >= 0.1 AND damaged_rate <= 1),
        CONSTRAINT CK_FINE_RULES_MINOR_DAMAGED_RATE CHECK (minor_damaged_rate >= 0 AND minor_damaged_rate <= 0.5),
        CONSTRAINT CK_FINE_RULES_FREE_DAYS CHECK (free_overdue_days >= 0 AND free_overdue_days <= 7)
    );
    
    PRINT '✓ 处罚规则表 FINE_RULES 创建成功';
END
ELSE
BEGIN
    PRINT '! 处罚规则表 FINE_RULES 已存在';
END
GO

-- =============================================
-- 3. 创建索引
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_BORROW_RULES_ACTIVE' AND object_id = OBJECT_ID('BORROW_RULES'))
BEGIN
    CREATE INDEX IX_BORROW_RULES_ACTIVE ON BORROW_RULES(is_active);
    PRINT '✓ 索引 IX_BORROW_RULES_ACTIVE 创建成功';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_FINE_RULES_ACTIVE' AND object_id = OBJECT_ID('FINE_RULES'))
BEGIN
    CREATE INDEX IX_FINE_RULES_ACTIVE ON FINE_RULES(is_active);
    PRINT '✓ 索引 IX_FINE_RULES_ACTIVE 创建成功';
END
GO

-- =============================================
-- 4. 创建视图：活跃规则
-- =============================================
IF OBJECT_ID('V_ACTIVE_BORROW_RULES', 'V') IS NOT NULL
    DROP VIEW V_ACTIVE_BORROW_RULES;
GO

CREATE VIEW V_ACTIVE_BORROW_RULES AS
SELECT 
    rule_id,
    reader_type AS 读者类型,
    max_borrow_count AS 最大借阅数量,
    max_category_count AS 最大分类数,
    borrow_days AS 借阅天数,
    max_renew_count AS 最大续借次数,
    renew_days AS 续借天数,
    CASE WHEN allow_reference_books = 1 THEN N'允许' ELSE N'不允许' END AS 工具书,
    CASE WHEN allow_new_books = 1 THEN N'允许' ELSE N'不允许' END AS 新书,
    CASE WHEN allow_hot_books = 1 THEN N'允许' ELSE N'不允许' END AS 热门书,
    remark AS 备注,
    created_time AS 创建时间,
    updated_time AS 更新时间
FROM BORROW_RULES
WHERE is_active = 1;
GO

IF OBJECT_ID('V_ACTIVE_FINE_RULES', 'V') IS NOT NULL
    DROP VIEW V_ACTIVE_FINE_RULES;
GO

CREATE VIEW V_ACTIVE_FINE_RULES AS
SELECT 
    rule_id,
    reader_type AS 读者类型,
    CAST(overdue_price_rate * 100 AS DECIMAL(5,2)) AS 逾期书价百分比,
    overdue_day_rate AS 每天罚款,
    CAST(lost_rate * 100 AS DECIMAL(5,2)) AS 丢失赔偿百分比,
    CAST(damaged_rate * 100 AS DECIMAL(5,2)) AS 严重破损百分比,
    CAST(minor_damaged_rate * 100 AS DECIMAL(5,2)) AS 轻微破损百分比,
    max_overdue_fine AS 最大逾期罚款,
    max_total_fine AS 最大总罚款,
    free_overdue_days AS 免罚天数,
    remark AS 备注,
    created_time AS 创建时间,
    updated_time AS 更新时间
FROM FINE_RULES
WHERE is_active = 1;
GO

PRINT '✓ 视图创建成功';
GO

-- =============================================
-- 5. 创建存储过程：获取读者类型的借阅规则
-- =============================================
IF OBJECT_ID('sp_GetBorrowRuleByReaderType', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetBorrowRuleByReaderType;
GO

CREATE PROCEDURE sp_GetBorrowRuleByReaderType
    @reader_type NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        rule_id,
        reader_type,
        max_borrow_count,
        max_category_count,
        borrow_days,
        max_renew_count,
        renew_days,
        allow_reference_books,
        allow_new_books,
        allow_hot_books,
        remark
    FROM BORROW_RULES
    WHERE reader_type = @reader_type AND is_active = 1;
END
GO

-- =============================================
-- 6. 创建存储过程：获取读者类型的处罚规则
-- =============================================
IF OBJECT_ID('sp_GetFineRuleByReaderType', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetFineRuleByReaderType;
GO

CREATE PROCEDURE sp_GetFineRuleByReaderType
    @reader_type NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        rule_id,
        reader_type,
        overdue_price_rate,
        overdue_day_rate,
        lost_rate,
        damaged_rate,
        minor_damaged_rate,
        max_overdue_fine,
        max_total_fine,
        free_overdue_days,
        remark
    FROM FINE_RULES
    WHERE reader_type = @reader_type AND is_active = 1;
END
GO

PRINT '✓ 存储过程创建成功';
GO

PRINT '';
PRINT '========================================';
PRINT '✓✓✓ 规则表创建脚本执行完成！';
PRINT '========================================';
