/* =========================================================
   推荐系统示例数据脚本
   用于生成大量借阅数据以测试推荐算法
   ========================================================= */

USE LibraryDB;
GO

SET NOCOUNT ON;

PRINT N'开始生成推荐系统测试数据...';
GO

/* =========================================================
   1) 扩展图书分类
   ========================================================= */
PRINT N'1. 扩展图书分类...';

-- 插入更多分类（如果不存在）
IF NOT EXISTS (SELECT 1 FROM BOOK_CATEGORY WHERE category_code = N'O')
BEGIN
    INSERT INTO BOOK_CATEGORY(category_code, category_name, parent_category_id, [Description]) VALUES
    (N'O',     N'数理科学和化学', NULL, N'中图法 O 类'),
    (N'O1',    N'数学', (SELECT category_id FROM BOOK_CATEGORY WHERE category_code = N'O'), N'数学'),
    (N'O3',    N'力学', (SELECT category_id FROM BOOK_CATEGORY WHERE category_code = N'O'), N'力学'),
    (N'O4',    N'物理学', (SELECT category_id FROM BOOK_CATEGORY WHERE category_code = N'O'), N'物理学'),
    
    (N'H',     N'语言、文字', NULL, N'中图法 H 类'),
    (N'H3',    N'常用外国语', (SELECT category_id FROM BOOK_CATEGORY WHERE category_code = N'H'), N'外语'),
    
    (N'J',     N'艺术', NULL, N'中图法 J 类'),
    (N'J2',    N'绘画', (SELECT category_id FROM BOOK_CATEGORY WHERE category_code = N'J'), N'绘画艺术'),
    
    (N'G',     N'文化、科学、教育、体育', NULL, N'中图法 G 类'),
    (N'G4',    N'教育', (SELECT category_id FROM BOOK_CATEGORY WHERE category_code = N'G'), N'教育学'),
    
    (N'D',     N'政治、法律', NULL, N'中图法 D 类'),
    (N'D9',    N'法律', (SELECT category_id FROM BOOK_CATEGORY WHERE category_code = N'D'), N'法律');
    
    PRINT N'   已添加新分类';
END
GO

/* =========================================================
   2) 扩展作者表
   ========================================================= */
PRINT N'2. 扩展作者表...';

-- 插入更多作者
INSERT INTO AUTHOR(author_name, nationality, birth_year, biography) 
SELECT * FROM (VALUES
    (N'莫言', N'中国', 1955, N'诺贝尔文学奖得主'),
    (N'路遥', N'中国', 1949, N'《平凡的世界》作者'),
    (N'陈忠实', N'中国', 1942, N'《白鹿原》作者'),
    (N'贾平凹', N'中国', 1952, N'中国当代作家'),
    (N'王安忆', N'中国', 1954, N'上海女作家'),
    (N'严歌苓', N'中国', 1958, N'旅美作家'),
    (N'麦家', N'中国', 1964, N'谍战小说作家'),
    (N'阿来', N'中国', 1959, N'藏族作家'),
    (N'迟子建', N'中国', 1964, N'东北女作家'),
    (N'毕飞宇', N'中国', 1964, N'茅盾文学奖得主'),
    (N'东野圭吾', N'日本', 1958, N'推理小说作家'),
    (N'村上春树', N'日本', 1949, N'日本当代作家'),
    (N'太宰治', N'日本', 1909, N'日本作家'),
    (N'川端康成', N'日本', 1899, N'诺贝尔文学奖得主'),
    (N'加西亚·马尔克斯', N'哥伦比亚', 1927, N'魔幻现实主义作家'),
    (N'海明威', N'美国', 1899, N'美国作家'),
    (N'菲茨杰拉德', N'美国', 1896, N'美国作家'),
    (N'乔治·奥威尔', N'英国', 1903, N'《1984》作者'),
    (N'托尔斯泰', N'俄国', 1828, N'俄国文学巨匠'),
    (N'陀思妥耶夫斯基', N'俄国', 1821, N'俄国作家'),
    (N'Eric Evans', N'美国', 1960, N'DDD领域驱动设计作者'),
    (N'Gang of Four', N'美国', 1960, N'设计模式作者团队'),
    (N'Joshua Bloch', N'美国', 1961, N'Effective Java作者'),
    (N'Brian Kernighan', N'加拿大', 1942, N'C语言作者'),
    (N'Dennis Ritchie', N'美国', 1941, N'C语言作者'),
    (N'Bjarne Stroustrup', N'丹麦', 1950, N'C++之父'),
    (N'Guido van Rossum', N'荷兰', 1956, N'Python之父'),
    (N'James Gosling', N'加拿大', 1955, N'Java之父'),
    (N'Anders Hejlsberg', N'丹麦', 1960, N'C#之父'),
    (N'Donald Knuth', N'美国', 1938, N'计算机科学家')
) AS t(author_name, nationality, birth_year, biography)
WHERE NOT EXISTS (SELECT 1 FROM AUTHOR a WHERE a.author_name = t.author_name);
GO

/* =========================================================
   3) 扩展书目表（大量书籍）
   ========================================================= */
PRINT N'3. 扩展书目表...';

-- 获取分类ID
DECLARE @catLiterature INT = (SELECT category_id FROM BOOK_CATEGORY WHERE category_code = N'I247.5');
DECLARE @catAncient INT = (SELECT category_id FROM BOOK_CATEGORY WHERE category_code = N'I242');
DECLARE @catShort INT = (SELECT category_id FROM BOOK_CATEGORY WHERE category_code = N'I247.7');
DECLARE @catComputer INT = (SELECT category_id FROM BOOK_CATEGORY WHERE category_code = N'TP312');
DECLARE @catEcon INT = (SELECT category_id FROM BOOK_CATEGORY WHERE category_code = N'F2');
DECLARE @catPhilo INT = (SELECT category_id FROM BOOK_CATEGORY WHERE category_code = N'B0');
DECLARE @catHistory INT = (SELECT category_id FROM BOOK_CATEGORY WHERE category_code = N'K2');

-- 插入更多书目
INSERT INTO BIBLIOGRAPHY(ISBN, bibliography_name, publish, publish_date, [Description], category_id, price)
SELECT * FROM (VALUES
    -- 中国现代文学
    (N'9787020099528', N'平凡的世界', N'北京十月文艺出版社', '2017-01-01', N'路遥长篇小说代表作', @catLiterature, 68.00),
    (N'9787020104529', N'白鹿原', N'人民文学出版社', '2012-09-01', N'陈忠实长篇小说', @catLiterature, 45.00),
    (N'9787506365123', N'蛙', N'作家出版社', '2012-10-01', N'莫言长篇小说', @catLiterature, 42.00),
    (N'9787506365456', N'丰乳肥臀', N'作家出版社', '2012-10-01', N'莫言长篇小说', @catLiterature, 48.00),
    (N'9787506365789', N'红高粱家族', N'作家出版社', '2012-05-01', N'莫言中篇小说集', @catLiterature, 35.00),
    (N'9787020100001', N'长恨歌', N'人民文学出版社', '2010-01-01', N'王安忆长篇小说', @catLiterature, 38.00),
    (N'9787020100002', N'秦腔', N'人民文学出版社', '2008-01-01', N'贾平凹长篇小说', @catLiterature, 42.00),
    (N'9787506390001', N'芳华', N'人民文学出版社', '2017-04-01', N'严歌苓长篇小说', @catLiterature, 39.00),
    (N'9787506390002', N'陆犯焉识', N'作家出版社', '2014-01-01', N'严歌苓长篇小说', @catLiterature, 45.00),
    (N'9787506380001', N'暗算', N'作家出版社', '2010-01-01', N'麦家谍战小说', @catLiterature, 36.00),
    (N'9787506380002', N'风声', N'作家出版社', '2010-02-01', N'麦家谍战小说', @catLiterature, 32.00),
    (N'9787506380003', N'解密', N'作家出版社', '2010-03-01', N'麦家谍战小说', @catLiterature, 35.00),
    (N'9787020110001', N'尘埃落定', N'人民文学出版社', '2005-01-01', N'阿来长篇小说', @catLiterature, 38.00),
    (N'9787020110002', N'额尔古纳河右岸', N'人民文学出版社', '2008-01-01', N'迟子建长篇小说', @catLiterature, 36.00),
    (N'9787020110003', N'推拿', N'人民文学出版社', '2011-01-01', N'毕飞宇长篇小说', @catLiterature, 35.00),
    
    -- 日本文学
    (N'9787544270878', N'白夜行', N'南海出版公司', '2013-01-01', N'东野圭吾推理小说', @catLiterature, 39.50),
    (N'9787544270879', N'嫌疑人X的献身', N'南海出版公司', '2014-01-01', N'东野圭吾推理小说', @catLiterature, 35.00),
    (N'9787544270880', N'解忧杂货店', N'南海出版公司', '2014-05-01', N'东野圭吾治愈小说', @catLiterature, 39.50),
    (N'9787544270881', N'恶意', N'南海出版公司', '2016-01-01', N'东野圭吾推理小说', @catLiterature, 35.00),
    (N'9787544270882', N'幻夜', N'南海出版公司', '2017-01-01', N'东野圭吾推理小说', @catLiterature, 42.00),
    (N'9787532725069', N'挪威的森林', N'上海译文出版社', '2007-01-01', N'村上春树长篇小说', @catLiterature, 32.00),
    (N'9787532725070', N'1Q84', N'南海出版公司', '2010-01-01', N'村上春树长篇小说', @catLiterature, 88.00),
    (N'9787532725071', N'海边的卡夫卡', N'上海译文出版社', '2007-01-01', N'村上春树长篇小说', @catLiterature, 35.00),
    (N'9787020120001', N'人间失格', N'作家出版社', '2015-01-01', N'太宰治代表作', @catLiterature, 28.00),
    (N'9787020120002', N'雪国', N'人民文学出版社', '2008-01-01', N'川端康成代表作', @catLiterature, 25.00),
    
    -- 世界文学
    (N'9787544253994', N'百年孤独', N'南海出版公司', '2011-06-01', N'马尔克斯代表作', @catLiterature, 55.00),
    (N'9787544291001', N'霍乱时期的爱情', N'南海出版公司', '2015-01-01', N'马尔克斯长篇', @catLiterature, 48.00),
    (N'9787532765010', N'老人与海', N'上海译文出版社', '2010-01-01', N'海明威中篇', @catLiterature, 22.00),
    (N'9787532765011', N'了不起的盖茨比', N'上海译文出版社', '2013-01-01', N'菲茨杰拉德代表作', @catLiterature, 28.00),
    (N'9787532765012', N'1984', N'上海译文出版社', '2010-01-01', N'乔治·奥威尔反乌托邦', @catLiterature, 32.00),
    (N'9787532765013', N'动物庄园', N'上海译文出版社', '2010-01-01', N'乔治·奥威尔寓言', @catLiterature, 25.00),
    (N'9787020130001', N'战争与和平', N'人民文学出版社', '2010-01-01', N'托尔斯泰代表作', @catLiterature, 98.00),
    (N'9787020130002', N'安娜·卡列尼娜', N'人民文学出版社', '2010-01-01', N'托尔斯泰代表作', @catLiterature, 68.00),
    (N'9787020130003', N'罪与罚', N'人民文学出版社', '2010-01-01', N'陀思妥耶夫斯基', @catLiterature, 45.00),
    (N'9787020130004', N'卡拉马佐夫兄弟', N'人民文学出版社', '2010-01-01', N'陀思妥耶夫斯基', @catLiterature, 68.00),
    
    -- 计算机类书籍
    (N'9787115428028', N'领域驱动设计', N'人民邮电出版社', '2016-06-01', N'Eric Evans DDD经典', @catComputer, 99.00),
    (N'9787115428029', N'设计模式', N'机械工业出版社', '2007-03-01', N'Gang of Four设计模式', @catComputer, 79.00),
    (N'9787115428030', N'Effective Java', N'机械工业出版社', '2018-12-01', N'Joshua Bloch Java实践', @catComputer, 129.00),
    (N'9787115428031', N'C程序设计语言', N'机械工业出版社', '2004-01-01', N'K&R C语言经典', @catComputer, 45.00),
    (N'9787115428032', N'C++ Primer', N'电子工业出版社', '2013-09-01', N'C++入门经典', @catComputer, 128.00),
    (N'9787115428033', N'深入理解计算机系统', N'机械工业出版社', '2016-11-01', N'CSAPP经典', @catComputer, 139.00),
    (N'9787115428034', N'算法导论', N'机械工业出版社', '2013-01-01', N'算法圣经', @catComputer, 128.00),
    (N'9787115428035', N'计算机程序的构造和解释', N'机械工业出版社', '2015-01-01', N'SICP经典', @catComputer, 89.00),
    (N'9787115428036', N'编译原理', N'机械工业出版社', '2008-12-01', N'龙书', @catComputer, 89.00),
    (N'9787115428037', N'数据库系统概念', N'机械工业出版社', '2012-03-01', N'数据库经典教材', @catComputer, 99.00),
    (N'9787115428038', N'计算机网络', N'机械工业出版社', '2017-01-01', N'谢希仁计算机网络', @catComputer, 59.00),
    (N'9787115428039', N'操作系统概念', N'机械工业出版社', '2018-06-01', N'恐龙书', @catComputer, 99.00),
    (N'9787115428040', N'Python编程从入门到实践', N'人民邮电出版社', '2016-07-01', N'Python入门', @catComputer, 89.00),
    (N'9787115428041', N'流畅的Python', N'人民邮电出版社', '2017-05-01', N'Python进阶', @catComputer, 139.00),
    (N'9787115428042', N'Java核心技术 卷I', N'机械工业出版社', '2019-12-01', N'Java经典', @catComputer, 149.00),
    (N'9787115428043', N'Spring实战', N'人民邮电出版社', '2020-01-01', N'Spring框架', @catComputer, 89.00),
    (N'9787115428044', N'微服务架构设计模式', N'机械工业出版社', '2019-05-01', N'微服务架构', @catComputer, 139.00),
    (N'9787115428045', N'Kubernetes权威指南', N'电子工业出版社', '2019-10-01', N'K8s实践', @catComputer, 158.00),
    (N'9787115428046', N'Docker技术入门与实战', N'机械工业出版社', '2018-09-01', N'Docker入门', @catComputer, 79.00),
    (N'9787115428047', N'Redis设计与实现', N'机械工业出版社', '2014-06-01', N'Redis原理', @catComputer, 79.00)
) AS t(ISBN, bibliography_name, publish, publish_date, [Description], category_id, price)
WHERE NOT EXISTS (SELECT 1 FROM BIBLIOGRAPHY b WHERE b.ISBN = t.ISBN);
GO

/* =========================================================
   4) 扩展书目-作者关联
   ========================================================= */
PRINT N'4. 扩展书目-作者关联...';

-- 为新书目添加作者关联
INSERT INTO BIBLIO_AUTHOR(bibliography_id, author_id, author_order)
SELECT b.bibliography_id, a.author_id, 1
FROM BIBLIOGRAPHY b
CROSS APPLY (
    SELECT TOP 1 author_id FROM AUTHOR 
    WHERE author_name = CASE 
        WHEN b.bibliography_name LIKE N'%平凡的世界%' THEN N'路遥'
        WHEN b.bibliography_name LIKE N'%白鹿原%' THEN N'陈忠实'
        WHEN b.bibliography_name IN (N'蛙', N'丰乳肥臀', N'红高粱家族') THEN N'莫言'
        WHEN b.bibliography_name = N'长恨歌' THEN N'王安忆'
        WHEN b.bibliography_name = N'秦腔' THEN N'贾平凹'
        WHEN b.bibliography_name IN (N'芳华', N'陆犯焉识') THEN N'严歌苓'
        WHEN b.bibliography_name IN (N'暗算', N'风声', N'解密') THEN N'麦家'
        WHEN b.bibliography_name = N'尘埃落定' THEN N'阿来'
        WHEN b.bibliography_name = N'额尔古纳河右岸' THEN N'迟子建'
        WHEN b.bibliography_name = N'推拿' THEN N'毕飞宇'
        WHEN b.bibliography_name IN (N'白夜行', N'嫌疑人X的献身', N'解忧杂货店', N'恶意', N'幻夜') THEN N'东野圭吾'
        WHEN b.bibliography_name IN (N'挪威的森林', N'1Q84', N'海边的卡夫卡') THEN N'村上春树'
        WHEN b.bibliography_name = N'人间失格' THEN N'太宰治'
        WHEN b.bibliography_name = N'雪国' THEN N'川端康成'
        WHEN b.bibliography_name IN (N'百年孤独', N'霍乱时期的爱情') THEN N'加西亚·马尔克斯'
        WHEN b.bibliography_name = N'老人与海' THEN N'海明威'
        WHEN b.bibliography_name = N'了不起的盖茨比' THEN N'菲茨杰拉德'
        WHEN b.bibliography_name IN (N'1984', N'动物庄园') THEN N'乔治·奥威尔'
        WHEN b.bibliography_name IN (N'战争与和平', N'安娜·卡列尼娜') THEN N'托尔斯泰'
        WHEN b.bibliography_name IN (N'罪与罚', N'卡拉马佐夫兄弟') THEN N'陀思妥耶夫斯基'
        WHEN b.bibliography_name = N'领域驱动设计' THEN N'Eric Evans'
        WHEN b.bibliography_name = N'设计模式' THEN N'Gang of Four'
        WHEN b.bibliography_name = N'Effective Java' THEN N'Joshua Bloch'
        ELSE NULL
    END
) a
WHERE NOT EXISTS (
    SELECT 1 FROM BIBLIO_AUTHOR ba 
    WHERE ba.bibliography_id = b.bibliography_id AND ba.author_id = a.author_id
)
AND a.author_id IS NOT NULL;
GO

/* =========================================================
   5) 扩展馆藏实体（每本书2-4册）
   ========================================================= */
PRINT N'5. 扩展馆藏实体...';

-- 为所有书目添加馆藏（如果不存在）
DECLARE @bookCounter INT = 100;
DECLARE @bibId INT;
DECLARE @copies INT;
DECLARE @locId INT;

DECLARE cur CURSOR FOR 
SELECT bibliography_id FROM BIBLIOGRAPHY 
WHERE NOT EXISTS (
    SELECT 1 FROM BOOK_ITEM bi WHERE bi.bibliography_id = BIBLIOGRAPHY.bibliography_id
);

OPEN cur;
FETCH NEXT FROM cur INTO @bibId;

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @copies = 2 + ABS(CHECKSUM(NEWID())) % 3; -- 2-4册
    
    WHILE @copies > 0
    BEGIN
        SET @locId = 1 + ABS(CHECKSUM(NEWID())) % 12; -- 随机库位
        
        INSERT INTO BOOK_ITEM(item_barcode, bibliography_id, current_status, location_id, acquisition_date, price, physical_condition)
        SELECT 
            N'BK-' + RIGHT(N'000000' + CAST(@bookCounter AS NVARCHAR(10)), 6),
            @bibId,
            N'AVAILABLE',
            @locId,
            DATEADD(DAY, -ABS(CHECKSUM(NEWID())) % 365, GETDATE()),
            b.price,
            N'GOOD'
        FROM BIBLIOGRAPHY b WHERE b.bibliography_id = @bibId;
        
        SET @bookCounter = @bookCounter + 1;
        SET @copies = @copies - 1;
    END
    
    FETCH NEXT FROM cur INTO @bibId;
END

CLOSE cur;
DEALLOCATE cur;

PRINT N'   馆藏实体扩展完成';
GO

/* =========================================================
   6) 扩展借书证和读者（大量用户）
   ========================================================= */
PRINT N'6. 扩展借书证和读者...';

-- 生成更多学生读者
-- 注意：cardID年份必须与startdate年份匹配（CK_readcard_year_match约束）
DECLARE @i INT = 5;
DECLARE @cardId NVARCHAR(20);
DECLARE @startDate DATE;
DECLARE @cardYear NVARCHAR(4);

WHILE @i <= 100
BEGIN
    -- 使用固定的2025年作为startdate，确保与cardID年份匹配
    SET @startDate = DATEADD(DAY, @i, '2025-01-01');
    SET @cardYear = N'2025';
    SET @cardId = N'BRW-' + @cardYear + N'-1-' + RIGHT(N'000000' + CAST(@i AS NVARCHAR(10)), 6);
    
    IF NOT EXISTS (SELECT 1 FROM readcard WHERE cardID = @cardId)
    BEGIN
        INSERT INTO readcard(cardID, startdate, overdate, [state])
        VALUES (@cardId, @startDate, DATEADD(YEAR, 1, @startDate), N'正常');
        
        INSERT INTO reader(cardID, readername, readertype, unit, [number])
        VALUES (@cardId, 
                N'学生' + CAST(@i AS NVARCHAR(10)), 
                N'本校学生', 
                CASE @i % 5 
                    WHEN 0 THEN N'计算机学院'
                    WHEN 1 THEN N'电气工程学院'
                    WHEN 2 THEN N'机械工程学院'
                    WHEN 3 THEN N'文学院'
                    ELSE N'经济管理学院'
                END,
                N'2023' + RIGHT(N'000000' + CAST(@i * 100 AS NVARCHAR(10)), 6));
    END
    
    SET @i = @i + 1;
END

-- 生成更多教师读者
SET @i = 105;
WHILE @i <= 130
BEGIN
    SET @startDate = DATEADD(DAY, @i - 100, '2025-01-01');
    SET @cardYear = N'2025';
    SET @cardId = N'BRW-' + @cardYear + N'-2-' + RIGHT(N'000000' + CAST(@i AS NVARCHAR(10)), 6);
    
    IF NOT EXISTS (SELECT 1 FROM readcard WHERE cardID = @cardId)
    BEGIN
        INSERT INTO readcard(cardID, startdate, overdate, [state])
        VALUES (@cardId, @startDate, DATEADD(YEAR, 1, @startDate), N'正常');
        
        INSERT INTO reader(cardID, readername, readertype, unit, [number])
        VALUES (@cardId, 
                N'教师' + CAST(@i AS NVARCHAR(10)), 
                N'本校教师',
                CASE @i % 4
                    WHEN 0 THEN N'计算机学院'
                    WHEN 1 THEN N'图书馆'
                    WHEN 2 THEN N'文学院'
                    ELSE N'外国语学院'
                END,
                N'T2020' + RIGHT(N'0000' + CAST(@i AS NVARCHAR(10)), 4));
    END
    
    SET @i = @i + 1;
END

-- 生成更多校外读者
SET @i = 205;
WHILE @i <= 230
BEGIN
    SET @startDate = DATEADD(DAY, @i - 200, '2025-01-01');
    SET @cardYear = N'2025';
    SET @cardId = N'BRW-' + @cardYear + N'-3-' + RIGHT(N'000000' + CAST(@i AS NVARCHAR(10)), 6);
    
    IF NOT EXISTS (SELECT 1 FROM readcard WHERE cardID = @cardId)
    BEGIN
        INSERT INTO readcard(cardID, startdate, overdate, [state])
        VALUES (@cardId, @startDate, DATEADD(YEAR, 1, @startDate), N'正常');
        
        INSERT INTO reader(cardID, readername, readertype, unit, [number])
        VALUES (@cardId, 
                N'访客' + CAST(@i AS NVARCHAR(10)), 
                N'校外人员',
                N'社会读者',
                NULL);
    END
    
    SET @i = @i + 1;
END

PRINT N'   读者扩展完成';
GO

/* =========================================================
   7) 生成大量借阅记录（推荐系统核心数据）
   ========================================================= */
PRINT N'7. 生成大量借阅记录...';

-- 创建临时表存储可用的书籍和读者
IF OBJECT_ID('tempdb..#AvailableBooks') IS NOT NULL DROP TABLE #AvailableBooks;
IF OBJECT_ID('tempdb..#ActiveReaders') IS NOT NULL DROP TABLE #ActiveReaders;

SELECT bi.item_barcode, bi.bibliography_id, b.category_id
INTO #AvailableBooks
FROM BOOK_ITEM bi
INNER JOIN BIBLIOGRAPHY b ON b.bibliography_id = bi.bibliography_id;

SELECT r.cardID, r.readertype
INTO #ActiveReaders
FROM reader r
INNER JOIN readcard rc ON rc.cardID = r.cardID
WHERE rc.[state] = N'正常';

-- 生成借阅记录
-- 策略：模拟不同用户偏好
-- 1. 计算机专业学生偏好计算机类书籍
-- 2. 文学院学生偏好文学类书籍
-- 3. 教师借阅更广泛
-- 4. 同一用户借过的书，其他相似用户也可能借

DECLARE @totalRecords INT = 0;
DECLARE @targetRecords INT = 2000; -- 目标生成2000条借阅记录
DECLARE @readerId NVARCHAR(20);
DECLARE @bookBarcode NVARCHAR(30);
DECLARE @borrowBibId INT;
DECLARE @borrowDate DATETIME2;
DECLARE @returnDate DATETIME2;
DECLARE @recordId BIGINT;
DECLARE @attempts INT = 0;
DECLARE @maxAttempts INT = 5000; -- 防止无限循环

WHILE @totalRecords < @targetRecords AND @attempts < @maxAttempts
BEGIN
    SET @attempts = @attempts + 1;
    
    -- 随机选择读者
    SELECT TOP 1 @readerId = cardID 
    FROM #ActiveReaders 
    ORDER BY NEWID();
    
    -- 生成借阅时间（过去365天内）
    SET @borrowDate = DATEADD(DAY, -ABS(CHECKSUM(NEWID())) % 365, GETDATE());
    SET @borrowDate = DATEADD(HOUR, ABS(CHECKSUM(NEWID())) % 12 + 8, CAST(CAST(@borrowDate AS DATE) AS DATETIME2));
    
    -- 80%的书已归还
    IF ABS(CHECKSUM(NEWID())) % 10 < 8
        SET @returnDate = DATEADD(DAY, 7 + ABS(CHECKSUM(NEWID())) % 21, @borrowDate);
    ELSE
        SET @returnDate = NULL;
    
    -- 确保归还时间不超过当前时间
    IF @returnDate > GETDATE()
        SET @returnDate = NULL;
    
    -- 根据读者类型选择书籍（模拟偏好）
    -- 计算机相关学生偏好计算机书籍（70%概率）
    IF @readerId LIKE N'%1-00000[5-9]%' OR @readerId LIKE N'%1-0000[1-3]%'
    BEGIN
        IF ABS(CHECKSUM(NEWID())) % 10 < 7
        BEGIN
            -- 如果是未归还借阅，确保书籍当前可借
            IF @returnDate IS NULL
                SELECT TOP 1 @bookBarcode = ab.item_barcode, @borrowBibId = ab.bibliography_id
                FROM #AvailableBooks ab
                WHERE ab.category_id IN (SELECT category_id FROM BOOK_CATEGORY WHERE category_code LIKE N'TP%')
                AND NOT EXISTS (
                    SELECT 1 FROM bookborrow bb 
                    WHERE bb.bookID = ab.item_barcode AND bb.overdate IS NULL
                )
                ORDER BY NEWID();
            ELSE
                SELECT TOP 1 @bookBarcode = item_barcode, @borrowBibId = bibliography_id
                FROM #AvailableBooks 
                WHERE category_id IN (SELECT category_id FROM BOOK_CATEGORY WHERE category_code LIKE N'TP%')
                ORDER BY NEWID();
        END
        ELSE
        BEGIN
            IF @returnDate IS NULL
                SELECT TOP 1 @bookBarcode = ab.item_barcode, @borrowBibId = ab.bibliography_id
                FROM #AvailableBooks ab
                WHERE NOT EXISTS (
                    SELECT 1 FROM bookborrow bb 
                    WHERE bb.bookID = ab.item_barcode AND bb.overdate IS NULL
                )
                ORDER BY NEWID();
            ELSE
                SELECT TOP 1 @bookBarcode = item_barcode, @borrowBibId = bibliography_id
                FROM #AvailableBooks ORDER BY NEWID();
        END
    END
    -- 文学院学生偏好文学书籍（70%概率）
    ELSE IF @readerId LIKE N'%1-0000[4-6]%'
    BEGIN
        IF ABS(CHECKSUM(NEWID())) % 10 < 7
        BEGIN
            IF @returnDate IS NULL
                SELECT TOP 1 @bookBarcode = ab.item_barcode, @borrowBibId = ab.bibliography_id
                FROM #AvailableBooks ab
                WHERE ab.category_id IN (SELECT category_id FROM BOOK_CATEGORY WHERE category_code LIKE N'I%')
                AND NOT EXISTS (
                    SELECT 1 FROM bookborrow bb 
                    WHERE bb.bookID = ab.item_barcode AND bb.overdate IS NULL
                )
                ORDER BY NEWID();
            ELSE
                SELECT TOP 1 @bookBarcode = item_barcode, @borrowBibId = bibliography_id
                FROM #AvailableBooks 
                WHERE category_id IN (SELECT category_id FROM BOOK_CATEGORY WHERE category_code LIKE N'I%')
                ORDER BY NEWID();
        END
        ELSE
        BEGIN
            IF @returnDate IS NULL
                SELECT TOP 1 @bookBarcode = ab.item_barcode, @borrowBibId = ab.bibliography_id
                FROM #AvailableBooks ab
                WHERE NOT EXISTS (
                    SELECT 1 FROM bookborrow bb 
                    WHERE bb.bookID = ab.item_barcode AND bb.overdate IS NULL
                )
                ORDER BY NEWID();
            ELSE
                SELECT TOP 1 @bookBarcode = item_barcode, @borrowBibId = bibliography_id
                FROM #AvailableBooks ORDER BY NEWID();
        END
    END
    -- 其他用户随机选择
    ELSE
    BEGIN
        IF @returnDate IS NULL
            SELECT TOP 1 @bookBarcode = ab.item_barcode, @borrowBibId = ab.bibliography_id
            FROM #AvailableBooks ab
            WHERE NOT EXISTS (
                SELECT 1 FROM bookborrow bb 
                WHERE bb.bookID = ab.item_barcode AND bb.overdate IS NULL
            )
            ORDER BY NEWID();
        ELSE
            SELECT TOP 1 @bookBarcode = item_barcode, @borrowBibId = bibliography_id
            FROM #AvailableBooks ORDER BY NEWID();
    END
    
    -- 检查是否已存在相同的借阅记录（同一用户同一书同一天）
    IF NOT EXISTS (
        SELECT 1 FROM bookborrow 
        WHERE cardID = @readerId 
        AND bookID = @bookBarcode 
        AND CAST(borrowdate AS DATE) = CAST(@borrowDate AS DATE)
    )
    AND @bookBarcode IS NOT NULL
    BEGIN
        -- 先插入借阅单头
        INSERT INTO borrow_record(cardID, borrowdate, overdate, bcomplete)
        VALUES (@readerId, @borrowDate, @returnDate, N'完好');
        
        SET @recordId = SCOPE_IDENTITY();
        
        -- 插入借阅明细
        INSERT INTO bookborrow(borrow_record_id, cardID, bookID, borrowdate, overdate)
        VALUES (@recordId, @readerId, @bookBarcode, @borrowDate, @returnDate);
        
        SET @totalRecords = @totalRecords + 1;
        
        -- 每100条输出一次进度
        IF @totalRecords % 100 = 0
            PRINT N'   已生成 ' + CAST(@totalRecords AS NVARCHAR(10)) + N' 条借阅记录...';
    END
END

DROP TABLE #AvailableBooks;
DROP TABLE #ActiveReaders;

PRINT N'   借阅记录生成完成，共 ' + CAST(@totalRecords AS NVARCHAR(10)) + N' 条';
GO

/* =========================================================
   8) 生成协同过滤模式数据（模拟"借了A也借了B"）
   ========================================================= */
PRINT N'8. 生成协同过滤模式数据...';

-- 为某些热门书籍创建关联借阅
-- 例如：借了《三体》的人也借了《球状闪电》
DECLARE @patternCount INT = 0;

-- 获取《三体》读者，让他们也借《球状闪电》
INSERT INTO borrow_record(cardID, borrowdate, overdate, bcomplete)
SELECT DISTINCT bb.cardID, 
       DATEADD(DAY, 3, bb.borrowdate),
       DATEADD(DAY, 20, bb.borrowdate),
       N'完好'
FROM bookborrow bb
INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
INNER JOIN BIBLIOGRAPHY b ON b.bibliography_id = bi.bibliography_id
WHERE b.bibliography_name = N'三体'
AND NOT EXISTS (
    SELECT 1 FROM bookborrow bb2
    INNER JOIN BOOK_ITEM bi2 ON bb2.bookID = bi2.item_barcode
    INNER JOIN BIBLIOGRAPHY b2 ON b2.bibliography_id = bi2.bibliography_id
    WHERE bb2.cardID = bb.cardID AND b2.bibliography_name = N'球状闪电'
);

-- 插入对应的借阅明细
INSERT INTO bookborrow(borrow_record_id, cardID, bookID, borrowdate, overdate)
SELECT br.borrow_record_id, br.cardID, bi.item_barcode, br.borrowdate, br.overdate
FROM borrow_record br
CROSS APPLY (
    SELECT TOP 1 item_barcode 
    FROM BOOK_ITEM bi 
    INNER JOIN BIBLIOGRAPHY b ON b.bibliography_id = bi.bibliography_id
    WHERE b.bibliography_name = N'球状闪电'
    ORDER BY NEWID()
) bi
WHERE NOT EXISTS (
    SELECT 1 FROM bookborrow bb WHERE bb.borrow_record_id = br.borrow_record_id
)
AND br.borrowdate > DATEADD(DAY, -365, GETDATE());

SET @patternCount = @patternCount + @@ROWCOUNT;

-- 借了《白夜行》的人也借了《嫌疑人X的献身》
INSERT INTO borrow_record(cardID, borrowdate, overdate, bcomplete)
SELECT DISTINCT bb.cardID, 
       DATEADD(DAY, 5, bb.borrowdate),
       DATEADD(DAY, 25, bb.borrowdate),
       N'完好'
FROM bookborrow bb
INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
INNER JOIN BIBLIOGRAPHY b ON b.bibliography_id = bi.bibliography_id
WHERE b.bibliography_name = N'白夜行'
AND NOT EXISTS (
    SELECT 1 FROM bookborrow bb2
    INNER JOIN BOOK_ITEM bi2 ON bb2.bookID = bi2.item_barcode
    INNER JOIN BIBLIOGRAPHY b2 ON b2.bibliography_id = bi2.bibliography_id
    WHERE bb2.cardID = bb.cardID AND b2.bibliography_name = N'嫌疑人X的献身'
);

INSERT INTO bookborrow(borrow_record_id, cardID, bookID, borrowdate, overdate)
SELECT br.borrow_record_id, br.cardID, bi.item_barcode, br.borrowdate, br.overdate
FROM borrow_record br
CROSS APPLY (
    SELECT TOP 1 item_barcode 
    FROM BOOK_ITEM bi 
    INNER JOIN BIBLIOGRAPHY b ON b.bibliography_id = bi.bibliography_id
    WHERE b.bibliography_name = N'嫌疑人X的献身'
    ORDER BY NEWID()
) bi
WHERE NOT EXISTS (
    SELECT 1 FROM bookborrow bb WHERE bb.borrow_record_id = br.borrow_record_id
)
AND br.borrowdate > DATEADD(DAY, -365, GETDATE());

SET @patternCount = @patternCount + @@ROWCOUNT;

-- 借了《代码整洁之道》的人也借了《重构》
INSERT INTO borrow_record(cardID, borrowdate, overdate, bcomplete)
SELECT DISTINCT bb.cardID, 
       DATEADD(DAY, 7, bb.borrowdate),
       DATEADD(DAY, 30, bb.borrowdate),
       N'完好'
FROM bookborrow bb
INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
INNER JOIN BIBLIOGRAPHY b ON b.bibliography_id = bi.bibliography_id
WHERE b.bibliography_name = N'代码整洁之道'
AND NOT EXISTS (
    SELECT 1 FROM bookborrow bb2
    INNER JOIN BOOK_ITEM bi2 ON bb2.bookID = bi2.item_barcode
    INNER JOIN BIBLIOGRAPHY b2 ON b2.bibliography_id = bi2.bibliography_id
    WHERE bb2.cardID = bb.cardID AND b2.bibliography_name LIKE N'重构%'
);

INSERT INTO bookborrow(borrow_record_id, cardID, bookID, borrowdate, overdate)
SELECT br.borrow_record_id, br.cardID, bi.item_barcode, br.borrowdate, br.overdate
FROM borrow_record br
CROSS APPLY (
    SELECT TOP 1 item_barcode 
    FROM BOOK_ITEM bi 
    INNER JOIN BIBLIOGRAPHY b ON b.bibliography_id = bi.bibliography_id
    WHERE b.bibliography_name LIKE N'重构%'
    ORDER BY NEWID()
) bi
WHERE NOT EXISTS (
    SELECT 1 FROM bookborrow bb WHERE bb.borrow_record_id = br.borrow_record_id
)
AND br.borrowdate > DATEADD(DAY, -365, GETDATE());

SET @patternCount = @patternCount + @@ROWCOUNT;

PRINT N'   协同过滤模式数据生成完成，共 ' + CAST(@patternCount AS NVARCHAR(10)) + N' 条关联借阅';
GO

/* =========================================================
   9) 更新统计数据
   ========================================================= */
PRINT N'9. 更新统计数据...';

-- 更新馆藏状态
UPDATE bi
SET bi.current_status = CASE 
    WHEN EXISTS (SELECT 1 FROM bookborrow bb WHERE bb.bookID = bi.item_barcode AND bb.overdate IS NULL) 
    THEN N'BORROWED' 
    ELSE N'AVAILABLE' 
END,
bi.status_changed_date = GETDATE()
FROM BOOK_ITEM bi;

-- 更新读者借阅信息摘要
UPDATE r
SET borrowed_books_info = N'当前借阅：' + CAST(ISNULL(t.cnt, 0) AS NVARCHAR(10)) + N' 本',
    borroweddate = t.last_borrow
FROM reader r
OUTER APPLY (
    SELECT COUNT(*) AS cnt, MAX(CAST(bb.borrowdate AS DATE)) AS last_borrow
    FROM bookborrow bb
    WHERE bb.cardID = r.cardID AND bb.overdate IS NULL
) t;

PRINT N'   统计数据更新完成';
GO

/* =========================================================
   10) 输出统计摘要
   ========================================================= */
PRINT N'========================================';
PRINT N'推荐系统测试数据生成完成！';
PRINT N'========================================';

SELECT N'书目总数' AS [统计项], COUNT(*) AS [数量] FROM BIBLIOGRAPHY
UNION ALL
SELECT N'馆藏总数', COUNT(*) FROM BOOK_ITEM
UNION ALL
SELECT N'读者总数', COUNT(*) FROM reader
UNION ALL
SELECT N'借阅记录总数', COUNT(*) FROM bookborrow
UNION ALL
SELECT N'已归还记录', COUNT(*) FROM bookborrow WHERE overdate IS NOT NULL
UNION ALL
SELECT N'未归还记录', COUNT(*) FROM bookborrow WHERE overdate IS NULL
UNION ALL
SELECT N'作者总数', COUNT(*) FROM AUTHOR
UNION ALL
SELECT N'分类总数', COUNT(*) FROM BOOK_CATEGORY;

-- 热门书籍TOP10
PRINT N'';
PRINT N'热门书籍 TOP 10：';
SELECT TOP 10 
    b.bibliography_name AS [书名],
    COUNT(bb.bookborrow_id) AS [借阅次数]
FROM bookborrow bb
INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
INNER JOIN BIBLIOGRAPHY b ON b.bibliography_id = bi.bibliography_id
GROUP BY b.bibliography_id, b.bibliography_name
ORDER BY COUNT(bb.bookborrow_id) DESC;

-- 活跃读者TOP10
PRINT N'';
PRINT N'活跃读者 TOP 10：';
SELECT TOP 10 
    r.readername AS [读者姓名],
    r.readertype AS [读者类型],
    COUNT(bb.bookborrow_id) AS [借阅次数]
FROM bookborrow bb
INNER JOIN reader r ON bb.cardID = r.cardID
GROUP BY r.cardID, r.readername, r.readertype
ORDER BY COUNT(bb.bookborrow_id) DESC;

GO

PRINT N'';
PRINT N'✅ 数据生成完成！现在可以运行推荐系统进行测试。';
GO
