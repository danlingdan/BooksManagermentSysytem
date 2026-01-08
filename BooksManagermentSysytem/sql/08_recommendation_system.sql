-- =============================================
-- 图书推荐系统数据库扩展脚本
-- 用于支持热门榜、相似书推荐、个性化推荐功能
-- 包含预计算相似度矩阵的存储过程
-- 注意：表名和字段名需与01 database.sql保持一致
-- =============================================

USE LibraryDB;
GO

-- 1. 用户行为记录表（可选，用于更精细的用户行为追踪）
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'user_behavior')
BEGIN
    CREATE TABLE user_behavior (
        behavior_id BIGINT IDENTITY(1,1) PRIMARY KEY,
        cardID NVARCHAR(20) NOT NULL,           -- 对应 readcard.cardID
        bibliography_id INT NOT NULL,            -- 对应 BIBLIOGRAPHY.bibliography_id
        behavior_type TINYINT NOT NULL,          -- 1:浏览, 2:借阅, 3:收藏, 4:评分, 5:预约
        behavior_time DATETIME NOT NULL DEFAULT GETDATE(),
        rating TINYINT NULL,                     -- 评分（1-5）
        weight DECIMAL(3,2) NOT NULL DEFAULT 1.0,
        
        CONSTRAINT FK_user_behavior_card FOREIGN KEY (cardID) 
            REFERENCES readcard(cardID),
        CONSTRAINT FK_user_behavior_bibliography FOREIGN KEY (bibliography_id) 
            REFERENCES BIBLIOGRAPHY(bibliography_id)
    );
    
    PRINT '表 user_behavior 创建成功';
END
GO

-- 2. 推荐缓存表（用于存储计算好的推荐结果）
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'recommendation_cache')
BEGIN
    CREATE TABLE recommendation_cache (
        cache_id BIGINT IDENTITY(1,1) PRIMARY KEY,
        cache_key NVARCHAR(100) NOT NULL,
        recommendation_type TINYINT NOT NULL,    -- 1:热门榜, 2:相似书, 3:个性化
        related_id NVARCHAR(50) NULL,            -- 关联ID（用户ID或书目ID）
        result_json NVARCHAR(MAX) NOT NULL,
        created_time DATETIME NOT NULL DEFAULT GETDATE(),
        expire_time DATETIME NOT NULL,
        
        CONSTRAINT UQ_recommendation_cache_key UNIQUE (cache_key)
    );
    
    PRINT '表 recommendation_cache 创建成功';
END
GO

-- 3. 图书相似度表（预计算的相似度矩阵）
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'book_similarity')
BEGIN
    CREATE TABLE book_similarity (
        similarity_id BIGINT IDENTITY(1,1) PRIMARY KEY,
        source_bibliography_id INT NOT NULL,
        target_bibliography_id INT NOT NULL,
        similarity_score DECIMAL(5,4) NOT NULL,  -- 0.0000 - 1.0000
        similarity_type TINYINT NOT NULL,        -- 1:基于内容, 2:协同过滤, 3:混合
        calculated_time DATETIME NOT NULL DEFAULT GETDATE(),
        
        CONSTRAINT FK_book_similarity_source FOREIGN KEY (source_bibliography_id) 
            REFERENCES BIBLIOGRAPHY(bibliography_id),
        CONSTRAINT FK_book_similarity_target FOREIGN KEY (target_bibliography_id) 
            REFERENCES BIBLIOGRAPHY(bibliography_id),
        CONSTRAINT UQ_book_similarity UNIQUE (source_bibliography_id, target_bibliography_id, similarity_type)
    );
    
    PRINT '表 book_similarity 创建成功';
END
GO

-- 4. 创建索引以优化查询性能

-- 借阅明细索引（用于热门榜计算）
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_bookborrow_date')
BEGIN
    CREATE NONCLUSTERED INDEX IX_bookborrow_date 
    ON bookborrow(borrowdate DESC)
    INCLUDE (cardID, bookID);
    
    PRINT '索引 IX_bookborrow_date 创建成功';
END
GO

-- 借阅明细索引（用于用户历史查询）
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_bookborrow_card_date')
BEGIN
    CREATE NONCLUSTERED INDEX IX_bookborrow_card_date 
    ON bookborrow(cardID, borrowdate DESC)
    INCLUDE (bookID);
    
    PRINT '索引 IX_bookborrow_card_date 创建成功';
END
GO

-- 用户行为索引
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'user_behavior')
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_user_behavior_card_time')
    BEGIN
        CREATE NONCLUSTERED INDEX IX_user_behavior_card_time 
        ON user_behavior(cardID, behavior_time DESC)
        INCLUDE (bibliography_id, behavior_type);
        
        PRINT '索引 IX_user_behavior_card_time 创建成功';
    END
    
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_user_behavior_bibliography')
    BEGIN
        CREATE NONCLUSTERED INDEX IX_user_behavior_bibliography 
        ON user_behavior(bibliography_id, behavior_type)
        INCLUDE (cardID, behavior_time);
        
        PRINT '索引 IX_user_behavior_bibliography 创建成功';
    END
END
GO

-- 相似度表索引（优化查询）
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'book_similarity')
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_book_similarity_source')
    BEGIN
        CREATE NONCLUSTERED INDEX IX_book_similarity_source 
        ON book_similarity(source_bibliography_id, similarity_type, similarity_score DESC)
        INCLUDE (target_bibliography_id, calculated_time);
        
        PRINT '索引 IX_book_similarity_source 创建成功';
    END
    
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_book_similarity_type_time')
    BEGIN
        CREATE NONCLUSTERED INDEX IX_book_similarity_type_time
        ON book_similarity(similarity_type, calculated_time DESC);
        
        PRINT '索引 IX_book_similarity_type_time 创建成功';
    END
END
GO

-- 缓存表索引
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'recommendation_cache')
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_recommendation_cache_expire')
    BEGIN
        CREATE NONCLUSTERED INDEX IX_recommendation_cache_expire 
        ON recommendation_cache(expire_time);
        
        PRINT '索引 IX_recommendation_cache_expire 创建成功';
    END
END
GO

-- 5. 创建清理过期缓存的存储过程
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_cleanup_recommendation_cache')
BEGIN
    DROP PROCEDURE sp_cleanup_recommendation_cache;
END
GO

CREATE PROCEDURE sp_cleanup_recommendation_cache
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM recommendation_cache
    WHERE expire_time < GETDATE();
    
    SELECT @@ROWCOUNT AS deleted_count;
END
GO

PRINT '存储过程 sp_cleanup_recommendation_cache 创建成功';
GO

-- 6. 创建获取相似度统计信息的存储过程
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_get_similarity_statistics')
BEGIN
    DROP PROCEDURE sp_get_similarity_statistics;
END
GO

CREATE PROCEDURE sp_get_similarity_statistics
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        similarity_type,
        CASE similarity_type 
            WHEN 1 THEN N'内容相似度'
            WHEN 2 THEN N'协同过滤'
            WHEN 3 THEN N'混合相似度'
        END AS type_name,
        COUNT(*) AS total_records,
        COUNT(DISTINCT source_bibliography_id) AS source_books,
        AVG(similarity_score) AS avg_score,
        MIN(similarity_score) AS min_score,
        MAX(similarity_score) AS max_score,
        MAX(calculated_time) AS last_calculated
    FROM book_similarity
    GROUP BY similarity_type
    ORDER BY similarity_type;
END
GO

PRINT '存储过程 sp_get_similarity_statistics 创建成功';
GO

-- 7. 创建清除指定类型相似度数据的存储过程
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_clear_similarity_by_type')
BEGIN
    DROP PROCEDURE sp_clear_similarity_by_type;
END
GO

CREATE PROCEDURE sp_clear_similarity_by_type
    @similarity_type TINYINT
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM book_similarity
    WHERE similarity_type = @similarity_type;
    
    SELECT @@ROWCOUNT AS deleted_count;
END
GO

PRINT '存储过程 sp_clear_similarity_by_type 创建成功';
GO

-- 8. 创建获取书目借阅者映射的视图（用于相似度计算）
IF EXISTS (SELECT * FROM sys.views WHERE name = 'vw_bibliography_borrowers')
BEGIN
    DROP VIEW vw_bibliography_borrowers;
END
GO

CREATE VIEW vw_bibliography_borrowers AS
SELECT 
    bi.bibliography_id,
    bb.cardID,
    COUNT(*) AS borrow_count
FROM bookborrow bb
INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
GROUP BY bi.bibliography_id, bb.cardID;
GO

PRINT '视图 vw_bibliography_borrowers 创建成功';
GO

-- 9. 创建获取热门书籍的视图
IF EXISTS (SELECT * FROM sys.views WHERE name = 'vw_trending_books')
BEGIN
    DROP VIEW vw_trending_books;
END
GO

CREATE VIEW vw_trending_books AS
SELECT 
    b.bibliography_id,
    b.bibliography_name,
    b.ISBN,
    b.publish,
    c.category_code,
    c.category_name,
    COUNT(bb.bookborrow_id) AS borrow_count_7days,
    (SELECT COUNT(*) FROM bookborrow bb2 
     INNER JOIN BOOK_ITEM bi2 ON bb2.bookID = bi2.item_barcode
     WHERE bi2.bibliography_id = b.bibliography_id 
       AND bb2.borrowdate >= DATEADD(day, -30, GETDATE())) AS borrow_count_30days
FROM BIBLIOGRAPHY b
INNER JOIN BOOK_ITEM bi ON bi.bibliography_id = b.bibliography_id
INNER JOIN bookborrow bb ON bb.bookID = bi.item_barcode
LEFT JOIN BOOK_CATEGORY c ON c.category_id = b.category_id
WHERE bb.borrowdate >= DATEADD(day, -7, GETDATE())
GROUP BY b.bibliography_id, b.bibliography_name, b.ISBN, b.publish, 
         c.category_code, c.category_name;
GO

PRINT '视图 vw_trending_books 创建成功';
GO

-- 10. 创建书目作者关联视图
IF EXISTS (SELECT * FROM sys.views WHERE name = 'vw_bibliography_author')
BEGIN
    DROP VIEW vw_bibliography_author;
END
GO

CREATE VIEW vw_bibliography_author AS
SELECT 
    b.bibliography_id,
    b.bibliography_name,
    STRING_AGG(a.author_name, N', ') WITHIN GROUP (ORDER BY ba.author_order) AS authors
FROM BIBLIOGRAPHY b
LEFT JOIN BIBLIO_AUTHOR ba ON ba.bibliography_id = b.bibliography_id
LEFT JOIN AUTHOR a ON a.author_id = ba.author_id
GROUP BY b.bibliography_id, b.bibliography_name;
GO

PRINT '视图 vw_bibliography_author 创建成功';
GO

-- 11. 创建获取相似书籍的存储过程（使用预计算数据）
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_get_similar_books')
BEGIN
    DROP PROCEDURE sp_get_similar_books;
END
GO

CREATE PROCEDURE sp_get_similar_books
    @bibliography_id INT,
    @similarity_type TINYINT = 3,  -- 默认使用混合相似度
    @top_n INT = 10
AS
BEGIN
    SET NOCOUNT ON;
    
    -- 优先使用指定类型，若无数据则尝试其他类型
    DECLARE @actual_type TINYINT = @similarity_type;
    
    IF NOT EXISTS (SELECT 1 FROM book_similarity 
                   WHERE source_bibliography_id = @bibliography_id 
                     AND similarity_type = @similarity_type)
    BEGIN
        -- 尝试协同过滤
        IF EXISTS (SELECT 1 FROM book_similarity 
                   WHERE source_bibliography_id = @bibliography_id 
                     AND similarity_type = 2)
            SET @actual_type = 2;
        -- 尝试内容相似度
        ELSE IF EXISTS (SELECT 1 FROM book_similarity 
                        WHERE source_bibliography_id = @bibliography_id 
                          AND similarity_type = 1)
            SET @actual_type = 1;
        ELSE
        BEGIN
            -- 无预计算数据，返回空
            SELECT 
                0 AS bibliography_id,
                N'' AS bibliography_name,
                N'' AS ISBN,
                N'' AS authors,
                N'' AS category_name,
                0 AS borrow_count,
                0.0 AS similarity_score,
                N'无预计算数据' AS recommendation_reason
            WHERE 1 = 0;
            RETURN;
        END
    END
    
    SELECT TOP (@top_n)
        b.bibliography_id,
        b.bibliography_name,
        b.ISBN,
        va.authors,
        c.category_name,
        ISNULL(bc.borrow_count, 0) AS borrow_count,
        bs.similarity_score,
        CASE @actual_type
            WHEN 1 THEN N'内容特征相似'
            WHEN 2 THEN N'借阅行为相似'
            WHEN 3 THEN N'综合相似推荐'
        END + N' (相似度: ' + CAST(CAST(bs.similarity_score * 100 AS INT) AS NVARCHAR) + N'%)' AS recommendation_reason
    FROM book_similarity bs
    INNER JOIN BIBLIOGRAPHY b ON b.bibliography_id = bs.target_bibliography_id
    LEFT JOIN BOOK_CATEGORY c ON c.category_id = b.category_id
    LEFT JOIN vw_bibliography_author va ON va.bibliography_id = b.bibliography_id
    LEFT JOIN (
        SELECT bi.bibliography_id, COUNT(*) AS borrow_count
        FROM bookborrow bb
        INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
        GROUP BY bi.bibliography_id
    ) bc ON bc.bibliography_id = b.bibliography_id
    WHERE bs.source_bibliography_id = @bibliography_id
      AND bs.similarity_type = @actual_type
    ORDER BY bs.similarity_score DESC;
END
GO

PRINT '存储过程 sp_get_similar_books 创建成功';
GO

-- 12. 创建相似度计算日志表
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'similarity_calculation_log')
BEGIN
    CREATE TABLE similarity_calculation_log (
        log_id BIGINT IDENTITY(1,1) PRIMARY KEY,
        similarity_type TINYINT NOT NULL,
        start_time DATETIME NOT NULL,
        end_time DATETIME NULL,
        total_books INT NULL,
        similarities_calculated INT NULL,
        success BIT NOT NULL DEFAULT 0,
        error_message NVARCHAR(MAX) NULL
    );
    
    PRINT '表 similarity_calculation_log 创建成功';
END
GO

PRINT '=============================================';
PRINT '图书推荐系统数据库扩展脚本执行完成！';
PRINT '=============================================';
PRINT '';
PRINT '新增内容：';
PRINT '  - sp_get_similarity_statistics: 获取相似度统计';
PRINT '  - sp_clear_similarity_by_type: 清除指定类型相似度';
PRINT '  - sp_get_similar_books: 获取相似书籍（使用预计算）';
PRINT '  - vw_bibliography_borrowers: 书目借阅者视图';
PRINT '  - similarity_calculation_log: 计算日志表';
PRINT '';
PRINT '表名对照说明：';
PRINT '  - readcard: 借书证表 (主键 cardID)';
PRINT '  - reader: 读者表 (主键 cardID)';
PRINT '  - BIBLIOGRAPHY: 书目表';
PRINT '  - BOOK_ITEM: 馆藏实体表 (主键 item_barcode)';
PRINT '  - bookborrow: 借阅明细表 (字段 cardID, bookID, borrowdate)';
PRINT '  - BOOK_CATEGORY: 图书分类表';
PRINT '  - BIBLIO_AUTHOR + AUTHOR: 作者关联表';
GO
