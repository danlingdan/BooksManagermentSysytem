-- =============================================
-- 借阅规则和处罚规则初始化数据脚本
-- 为三种读者类型设置默认规则
-- =============================================

USE LibraryDB;
GO

PRINT '开始初始化规则数据...';
PRINT '';

-- =============================================
-- 1. 清空现有数据（可选）
-- =============================================
-- DELETE FROM FINE_RULES;
-- DELETE FROM BORROW_RULES;
-- PRINT '✓ 已清空现有规则数据';
-- GO

-- =============================================
-- 2. 插入借阅规则
-- =============================================

-- 2.1 本校学生借阅规则
IF NOT EXISTS (SELECT 1 FROM BORROW_RULES WHERE reader_type = N'本校学生')
BEGIN
    INSERT INTO BORROW_RULES (
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
    )
    VALUES (
        N'本校学生',
        3,      -- 最多借3本
        2,      -- 最多2个分类
        7,      -- 借期7天
        2,      -- 最多续借2次
        7,      -- 每次续借7天
        0,      -- 不允许借工具书
        1,      -- 允许借新书
        1,      -- 允许借热门书
        N'本校学生标准借阅规则'
    );
    PRINT '✓ 插入：本校学生借阅规则';
END
ELSE
BEGIN
    PRINT '! 本校学生借阅规则已存在，跳过插入';
END
GO

-- 2.2 本校教师借阅规则
IF NOT EXISTS (SELECT 1 FROM BORROW_RULES WHERE reader_type = N'本校教师')
BEGIN
    INSERT INTO BORROW_RULES (
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
    )
    VALUES (
        N'本校教师',
        5,      -- 最多借5本（教师优惠）
        3,      -- 最多3个分类
        14,     -- 借期14天（教师借期更长）
        3,      -- 最多续借3次
        14,     -- 每次续借14天
        1,      -- 允许借工具书（教师特权）
        1,      -- 允许借新书
        1,      -- 允许借热门书
        N'本校教师优惠借阅规则，借期和数量更多'
    );
    PRINT '✓ 插入：本校教师借阅规则';
END
ELSE
BEGIN
    PRINT '! 本校教师借阅规则已存在，跳过插入';
END
GO

-- 2.3 校外人员借阅规则
IF NOT EXISTS (SELECT 1 FROM BORROW_RULES WHERE reader_type = N'校外人员')
BEGIN
    INSERT INTO BORROW_RULES (
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
    )
    VALUES (
        N'校外人员',
        2,      -- 最多借2本（限制更严）
        2,      -- 最多2个分类
        5,      -- 借期5天（借期较短）
        1,      -- 最多续借1次
        5,      -- 每次续借5天
        0,      -- 不允许借工具书
        0,      -- 不允许借新书（新书优先内部）
        0,      -- 不允许借热门书（热门书优先内部）
        N'校外人员限制性借阅规则，借期和数量较少'
    );
    PRINT '✓ 插入：校外人员借阅规则';
END
ELSE
BEGIN
    PRINT '! 校外人员借阅规则已存在，跳过插入';
END
GO

-- =============================================
-- 3. 插入处罚规则
-- =============================================

-- 3.1 本校学生处罚规则
IF NOT EXISTS (SELECT 1 FROM FINE_RULES WHERE reader_type = N'本校学生')
BEGIN
    INSERT INTO FINE_RULES (
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
    )
    VALUES (
        N'本校学生',
        0.1000,     -- 逾期：书价的10%
        0.10,       -- 逾期：每天0.1元
        1.0000,     -- 丢失：赔偿100%
        0.5000,     -- 严重破损：赔偿50%
        0.2500,     -- 轻微破损：赔偿25%
        50.00,      -- 最大逾期罚款50元
        NULL,       -- 无总罚款上限
        1,          -- 1天宽限期
        N'本校学生标准处罚规则，有1天宽限期'
    );
    PRINT '✓ 插入：本校学生处罚规则';
END
ELSE
BEGIN
    PRINT '! 本校学生处罚规则已存在，跳过插入';
END
GO

-- 3.2 本校教师处罚规则
IF NOT EXISTS (SELECT 1 FROM FINE_RULES WHERE reader_type = N'本校教师')
BEGIN
    INSERT INTO FINE_RULES (
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
    )
    VALUES (
        N'本校教师',
        0.0800,     -- 逾期：书价的8%（教师优惠）
        0.08,       -- 逾期：每天0.08元
        1.0000,     -- 丢失：赔偿100%
        0.5000,     -- 严重破损：赔偿50%
        0.2000,     -- 轻微破损：赔偿20%
        100.00,     -- 最大逾期罚款100元（上限更高）
        NULL,       -- 无总罚款上限
        3,          -- 3天宽限期（教师优惠）
        N'本校教师优惠处罚规则，罚款更低，宽限期更长'
    );
    PRINT '✓ 插入：本校教师处罚规则';
END
ELSE
BEGIN
    PRINT '! 本校教师处罚规则已存在，跳过插入';
END
GO

-- 3.3 校外人员处罚规则
IF NOT EXISTS (SELECT 1 FROM FINE_RULES WHERE reader_type = N'校外人员')
BEGIN
    INSERT INTO FINE_RULES (
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
    )
    VALUES (
        N'校外人员',
        0.1500,     -- 逾期：书价的15%（罚款更高）
        0.15,       -- 逾期：每天0.15元
        1.5000,     -- 丢失：赔偿150%（含管理成本）
        0.7000,     -- 严重破损：赔偿70%
        0.3000,     -- 轻微破损：赔偿30%
        30.00,      -- 最大逾期罚款30元（上限较低）
        NULL,       -- 无总罚款上限
        0,          -- 无宽限期
        N'校外人员严格处罚规则，罚款更高，无宽限期'
    );
    PRINT '✓ 插入：校外人员处罚规则';
END
ELSE
BEGIN
    PRINT '! 校外人员处罚规则已存在，跳过插入';
END
GO

-- =============================================
-- 4. 验证数据
-- =============================================
PRINT '';
PRINT '========================================';
PRINT '数据验证：';
PRINT '========================================';

PRINT '借阅规则：';
SELECT 
    reader_type AS 读者类型,
    max_borrow_count AS 最大借阅数,
    borrow_days AS 借期天数,
    max_renew_count AS 最大续借次数,
    CASE WHEN allow_reference_books = 1 THEN N'是' ELSE N'否' END AS 可借工具书
FROM BORROW_RULES
ORDER BY 
    CASE reader_type 
        WHEN N'本校教师' THEN 1 
        WHEN N'本校学生' THEN 2 
        WHEN N'校外人员' THEN 3 
    END;

PRINT '';
PRINT '处罚规则：';
SELECT 
    reader_type AS 读者类型,
    CAST(overdue_price_rate * 100 AS VARCHAR) + '%' AS 逾期书价比例,
    CAST(overdue_day_rate AS VARCHAR) + '元' AS 每天罚款,
    CAST(damaged_rate * 100 AS VARCHAR) + '%' AS 破损赔偿比例,
    free_overdue_days AS 宽限天数
FROM FINE_RULES
ORDER BY 
    CASE reader_type 
        WHEN N'本校教师' THEN 1 
        WHEN N'本校学生' THEN 2 
        WHEN N'校外人员' THEN 3 
    END;

PRINT '';
PRINT '========================================';
PRINT '✓✓✓ 规则数据初始化完成！';
PRINT '========================================';
PRINT '';
PRINT '说明：';
PRINT '  • 本校教师：最优惠的借阅条件和罚款政策';
PRINT '  • 本校学生：标准的借阅条件和罚款政策';
PRINT '  • 校外人员：最严格的借阅条件和罚款政策';
PRINT '';
