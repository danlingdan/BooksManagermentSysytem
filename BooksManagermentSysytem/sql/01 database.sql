/* =========================================================
   0) 创建数据库 + 切换
   ========================================================= */
USE master;
GO

IF DB_ID(N'LibraryDB') IS NULL
BEGIN
    CREATE DATABASE LibraryDB;
END
GO

USE LibraryDB;
GO

SET NOCOUNT ON;

------------------------------------------------------------
-- 1) 先删表（按依赖逆序），方便反复执行
------------------------------------------------------------
IF OBJECT_ID('dbo.BIBLIO_AUTHOR','U') IS NOT NULL DROP TABLE dbo.BIBLIO_AUTHOR;
IF OBJECT_ID('dbo.catalog_log','U')    IS NOT NULL DROP TABLE dbo.catalog_log;
IF OBJECT_ID('dbo.BOOK_ITEM','U')      IS NOT NULL DROP TABLE dbo.BOOK_ITEM;
IF OBJECT_ID('dbo.BIBLIOGRAPHY','U')   IS NOT NULL DROP TABLE dbo.BIBLIOGRAPHY;
IF OBJECT_ID('dbo.AUTHOR','U')         IS NOT NULL DROP TABLE dbo.AUTHOR;
IF OBJECT_ID('dbo.STORAGE_LOCATION','U') IS NOT NULL DROP TABLE dbo.STORAGE_LOCATION;
IF OBJECT_ID('dbo.BOOK_CATEGORY','U')  IS NOT NULL DROP TABLE dbo.BOOK_CATEGORY;

IF OBJECT_ID('dbo.fine','U')           IS NOT NULL DROP TABLE dbo.fine;
IF OBJECT_ID('dbo.bookborrow','U')     IS NOT NULL DROP TABLE dbo.bookborrow;
IF OBJECT_ID('dbo.borrow_record','U')  IS NOT NULL DROP TABLE dbo.borrow_record;
IF OBJECT_ID('dbo.reader','U')         IS NOT NULL DROP TABLE dbo.reader;
IF OBJECT_ID('dbo.readcard','U')       IS NOT NULL DROP TABLE dbo.readcard;
GO

/* =========================================================
   2) 图书分类表 BOOK_CATEGORY
   ========================================================= */
CREATE TABLE dbo.BOOK_CATEGORY(
    category_id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    category_code NVARCHAR(20) NOT NULL UNIQUE,
    category_name NVARCHAR(100) NOT NULL,
    parent_category_id INT NULL,
    [Description] NVARCHAR(500) NULL,
    create_time DATETIME2(0) NOT NULL CONSTRAINT DF_BOOK_CATEGORY_create DEFAULT (SYSDATETIME()),
    update_time DATETIME2(0) NULL
);
ALTER TABLE dbo.BOOK_CATEGORY
ADD CONSTRAINT FK_BOOK_CATEGORY_parent
FOREIGN KEY(parent_category_id) REFERENCES dbo.BOOK_CATEGORY(category_id);
GO

/* =========================================================
   3) 库存位置表 STORAGE_LOCATION
   ========================================================= */
CREATE TABLE dbo.STORAGE_LOCATION(
    location_id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    location_code NVARCHAR(30) NOT NULL UNIQUE,
    location_name NVARCHAR(100) NOT NULL,
    parent_location_id INT NULL,
    location_type NVARCHAR(30) NOT NULL,
    max_capacity INT NOT NULL,
    current_quantity INT NOT NULL CONSTRAINT DF_STORAGE_LOCATION_current DEFAULT(0),
    [status] NVARCHAR(30) NOT NULL CONSTRAINT DF_STORAGE_LOCATION_status DEFAULT(N'ACTIVE'),
    CONSTRAINT CK_STORAGE_LOCATION_type CHECK (location_type IN
        (N'REGULAR_SHELF',N'HOT_ZONE',N'NEW_BOOK',N'REFERENCE',N'JOURNAL',
         N'RESERVATION_SHELF',N'TOOL_ONLY',N'REPAIR_AREA')),
    CONSTRAINT CK_STORAGE_LOCATION_status CHECK ([status] IN (N'ACTIVE',N'INACTIVE',N'MAINTENANCE',N'FULL',N'ORGANIZING')),
    CONSTRAINT CK_STORAGE_LOCATION_capacity CHECK (max_capacity > 0 AND current_quantity >= 0 AND current_quantity <= max_capacity)
);
ALTER TABLE dbo.STORAGE_LOCATION
ADD CONSTRAINT FK_STORAGE_LOCATION_parent
FOREIGN KEY(parent_location_id) REFERENCES dbo.STORAGE_LOCATION(location_id);
GO

/* =========================================================
   4) 作者表 AUTHOR
   ========================================================= */
CREATE TABLE dbo.AUTHOR(
    author_id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    author_name NVARCHAR(100) NOT NULL,
    nationality NVARCHAR(50) NULL,
    birth_year SMALLINT NULL,
    biography NVARCHAR(MAX) NULL,
    CONSTRAINT CK_AUTHOR_birthyear CHECK (birth_year IS NULL OR (birth_year BETWEEN 1000 AND 2100))
);
GO

/* =========================================================
   5) 书目表 BIBLIOGRAPHY
   ========================================================= */
CREATE TABLE dbo.BIBLIOGRAPHY(
    bibliography_id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ISBN NVARCHAR(20) NOT NULL UNIQUE,
    bibliography_name NVARCHAR(200) NOT NULL,
    publish NVARCHAR(200) NULL,
    publish_date DATE NULL,
    [Description] NVARCHAR(MAX) NULL,
    category_id INT NOT NULL,
    price DECIMAL(10,2) NULL,
    create_time DATETIME2(0) NOT NULL CONSTRAINT DF_BIBLIOGRAPHY_create DEFAULT (SYSDATETIME()),
    CONSTRAINT CK_BIBLIOGRAPHY_price CHECK (price IS NULL OR price >= 0)
);
ALTER TABLE dbo.BIBLIOGRAPHY
ADD CONSTRAINT FK_BIBLIOGRAPHY_category
FOREIGN KEY(category_id) REFERENCES dbo.BOOK_CATEGORY(category_id);
GO

/* =========================================================
   6) 书目-作者关联表 BIBLIO_AUTHOR（同书目作者不重复）
   ========================================================= */
CREATE TABLE dbo.BIBLIO_AUTHOR(
    relation_id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    bibliography_id INT NOT NULL,
    author_id INT NOT NULL,
    author_order INT NOT NULL,
    CONSTRAINT CK_BIBLIO_AUTHOR_order CHECK (author_order >= 1),
    CONSTRAINT UQ_BIBLIO_AUTHOR_noDup UNIQUE (bibliography_id, author_id),
    CONSTRAINT UQ_BIBLIO_AUTHOR_order UNIQUE (bibliography_id, author_order)
);
ALTER TABLE dbo.BIBLIO_AUTHOR
ADD CONSTRAINT FK_BIBLIO_AUTHOR_bib
FOREIGN KEY(bibliography_id) REFERENCES dbo.BIBLIOGRAPHY(bibliography_id);

ALTER TABLE dbo.BIBLIO_AUTHOR
ADD CONSTRAINT FK_BIBLIO_AUTHOR_author
FOREIGN KEY(author_id) REFERENCES dbo.AUTHOR(author_id);
GO

/* =========================================================
   7) 馆藏实体表 BOOK_ITEM
   ========================================================= */
CREATE TABLE dbo.BOOK_ITEM(
    item_barcode NVARCHAR(30) NOT NULL PRIMARY KEY,        -- 书籍编号/馆藏条码
    bibliography_id INT NOT NULL,
    current_status NVARCHAR(20) NOT NULL,
    location_id INT NOT NULL,
    acquisition_date DATE NOT NULL,
    price DECIMAL(10,2) NULL,
    physical_condition NVARCHAR(20) NOT NULL,
    status_changed_date DATETIME2(0) NOT NULL CONSTRAINT DF_BOOK_ITEM_statuschg DEFAULT (SYSDATETIME()),
    CONSTRAINT CK_BOOK_ITEM_status CHECK (current_status IN (N'AVAILABLE',N'BORROWED',N'OFF_SHELF',N'RESERVED')),
    CONSTRAINT CK_BOOK_ITEM_condition CHECK (physical_condition IN (N'GOOD',N'DAMAGED',N'REPAIR')),
    CONSTRAINT CK_BOOK_ITEM_price CHECK (price IS NULL OR price >= 0)
);
ALTER TABLE dbo.BOOK_ITEM
ADD CONSTRAINT FK_BOOK_ITEM_bib
FOREIGN KEY(bibliography_id) REFERENCES dbo.BIBLIOGRAPHY(bibliography_id);

ALTER TABLE dbo.BOOK_ITEM
ADD CONSTRAINT FK_BOOK_ITEM_location
FOREIGN KEY(location_id) REFERENCES dbo.STORAGE_LOCATION(location_id);
GO

/* =========================================================
   8) 编目日志 catalog_log
   ========================================================= */
CREATE TABLE dbo.catalog_log(
    log_id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    target_type NVARCHAR(30) NOT NULL,
    target_id NVARCHAR(50) NOT NULL,
    action_type NVARCHAR(30) NOT NULL,
    operator NVARCHAR(50) NOT NULL,
    action_time DATETIME2(0) NOT NULL CONSTRAINT DF_catalog_log_time DEFAULT(SYSDATETIME()),
    note NVARCHAR(500) NULL,
    CONSTRAINT CK_catalog_log_target CHECK (target_type IN (N'BIBLIOGRAPHY',N'BOOK_ITEM',N'CATEGORY',N'LOCATION')),
    CONSTRAINT CK_catalog_log_action CHECK (action_type IN (N'新增',N'删除',N'更新',N'分类',N'上架',N'下架',N'状态变更'))
);
GO

/* =========================================================
   9) 借书证表 readcard
   cardID: BRW-年份-类别码-顺序号(6位)
   类别码：1学生 2教师 3校外
   overdate = startdate + 1年
   ========================================================= */
CREATE TABLE dbo.readcard(
    cardID NVARCHAR(20) NOT NULL PRIMARY KEY,
    startdate DATE NOT NULL,
    overdate DATE NOT NULL,
    [state] NVARCHAR(10) NOT NULL,
    CONSTRAINT CK_readcard_state CHECK ([state] IN (N'正常',N'注销',N'挂失',N'补办中')),
    CONSTRAINT CK_readcard_format CHECK (
        cardID LIKE N'BRW-[0-9][0-9][0-9][0-9]-[1-3]-[0-9][0-9][0-9][0-9][0-9][0-9]'
    ),
    CONSTRAINT CK_readcard_overdate_1y CHECK (overdate = DATEADD(YEAR, 1, startdate)),
    CONSTRAINT CK_readcard_year_match CHECK (SUBSTRING(cardID, 5, 4) = CONVERT(CHAR(4), YEAR(startdate)))
);
GO

/* =========================================================
   10) 读者表 reader（cardID PK + FK）
   ========================================================= */
CREATE TABLE dbo.reader(
    cardID NVARCHAR(20) NOT NULL PRIMARY KEY,
    readername NVARCHAR(50) NOT NULL,
    readertype NVARCHAR(10) NOT NULL,
    unit NVARCHAR(100) NULL,
    [number] NVARCHAR(30) NULL,                 -- 学号/工号（校外 NULL）
    borrowed_books_info NVARCHAR(400) NULL,     -- 冗余摘要
    borroweddate DATE NULL,                     -- 冗余字段（最近借书日）
    borrow_note NVARCHAR(200) NULL,             -- 备注
    CONSTRAINT CK_reader_type CHECK (readertype IN (N'本校学生',N'本校教师',N'校外人员')),
    CONSTRAINT CK_reader_number_rule CHECK (
        (readertype IN (N'本校学生',N'本校教师') AND [number] IS NOT NULL)
        OR
        (readertype = N'校外人员' AND [number] IS NULL)
    ),
    CONSTRAINT CK_reader_cardid_type_match CHECK (
        (readertype = N'本校学生' AND SUBSTRING(cardID,10,1)=N'1')
        OR (readertype = N'本校教师' AND SUBSTRING(cardID,10,1)=N'2')
        OR (readertype = N'校外人员' AND SUBSTRING(cardID,10,1)=N'3')
    )
);
ALTER TABLE dbo.reader
ADD CONSTRAINT FK_reader_readcard
FOREIGN KEY(cardID) REFERENCES dbo.readcard(cardID);
GO

/* =========================================================
   11) 借阅单头 borrow_record
   ========================================================= */
CREATE TABLE dbo.borrow_record(
    borrow_record_id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    cardID NVARCHAR(20) NOT NULL,
    borrowdate DATETIME2(0) NOT NULL,
    overdate DATETIME2(0) NULL,                 -- 实际还书时间；未还 NULL
    bcomplete NVARCHAR(20) NOT NULL CONSTRAINT DF_borrow_record_complete DEFAULT(N'完好'),
    add_note NVARCHAR(200) NULL,
    CONSTRAINT CK_borrow_record_complete CHECK (bcomplete IN (N'完好',N'轻微破损',N'严重破损'))
);
ALTER TABLE dbo.borrow_record
ADD CONSTRAINT FK_borrow_record_reader
FOREIGN KEY(cardID) REFERENCES dbo.reader(cardID);
GO

/* =========================================================
   12) 借阅明细 bookborrow（借了哪本）
   ========================================================= */
CREATE TABLE dbo.bookborrow(
    bookborrow_id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    borrow_record_id BIGINT NULL,
    cardID NVARCHAR(20) NOT NULL,
    bookID NVARCHAR(30) NOT NULL,               -- 对应 BOOK_ITEM.item_barcode
    borrowdate DATETIME2(0) NOT NULL,
    overdate DATETIME2(0) NULL,
    add_note NVARCHAR(200) NULL,
    CONSTRAINT CK_bookborrow_time CHECK (overdate IS NULL OR overdate >= borrowdate)
);
ALTER TABLE dbo.bookborrow
ADD CONSTRAINT FK_bookborrow_reader
FOREIGN KEY(cardID) REFERENCES dbo.reader(cardID);

ALTER TABLE dbo.bookborrow
ADD CONSTRAINT FK_bookborrow_item
FOREIGN KEY(bookID) REFERENCES dbo.BOOK_ITEM(item_barcode);

ALTER TABLE dbo.bookborrow
ADD CONSTRAINT FK_bookborrow_record
FOREIGN KEY(borrow_record_id) REFERENCES dbo.borrow_record(borrow_record_id);
GO

-- 同一本实体书：未归还时不允许再次借出（过滤唯一索引）
CREATE UNIQUE INDEX UX_bookborrow_bookID_open
ON dbo.bookborrow(bookID)
WHERE overdate IS NULL;
GO

/* =========================================================
   13) 罚款记录 fine
   ========================================================= */
CREATE TABLE dbo.fine(
    fine_id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    cardID NVARCHAR(20) NOT NULL,
    readername NVARCHAR(50) NOT NULL,           -- 历史快照
    reason NVARCHAR(200) NOT NULL,
    amount DECIMAL(10,2) NOT NULL,
    fine_status NVARCHAR(10) NOT NULL,
    created_time DATETIME2(0) NOT NULL CONSTRAINT DF_fine_time DEFAULT(SYSDATETIME()),
    CONSTRAINT CK_fine_amount CHECK (amount > 0),
    CONSTRAINT CK_fine_status CHECK (fine_status IN (N'已支付',N'未支付'))
);
ALTER TABLE dbo.fine
ADD CONSTRAINT FK_fine_reader
FOREIGN KEY(cardID) REFERENCES dbo.reader(cardID);
GO

/* =========================================================
   14) 插入示例数据（多一些）
   ========================================================= */

------------------------------------------------------------
-- 14.1 图书分类（含树结构）
------------------------------------------------------------
INSERT INTO dbo.BOOK_CATEGORY(category_code, category_name, parent_category_id, [Description]) VALUES
(N'I',     N'文学', NULL, N'中图法 I 类'),
(N'I2',    N'中国文学', 1, N'中国文学总论'),
(N'I24',   N'小说', 2, N'小说类'),
(N'I247',  N'现代作品', 3, N'中国现代小说作品'),
(N'I247.5',N'新体长篇、中篇小说', 4, N'现代长/中篇'),
(N'I247.7',N'新体短篇小说', 4, N'现代短篇'),
(N'I242',  N'古代作品', 3, N'中国古代小说作品'),

(N'T',     N'工业技术', NULL, N'中图法 T 类'),
(N'TP',    N'自动化技术、计算技术', 8, N'计算机与自动化'),
(N'TP31',  N'计算机软件', 9, N'软件工程与程序设计'),
(N'TP312', N'程序设计、软件工程', 10, N'软件工程/编程'),

(N'F',     N'经济', NULL, N'中图法 F 类'),
(N'F2',    N'经济计划与管理', 12, N'管理与计划'),

(N'B',     N'哲学、宗教', NULL, N'中图法 B 类'),
(N'B0',    N'哲学理论', 14, N'哲学基础理论'),

(N'K',     N'历史、地理', NULL, N'中图法 K 类'),
(N'K2',    N'中国史', 16, N'中国历史'),

(N'R',     N'医药、卫生', NULL, N'中图法 R 类'),
(N'R1',    N'预防医学、卫生学', 18, N'公共卫生');

GO

------------------------------------------------------------
-- 14.2 库位（多一些）
------------------------------------------------------------
INSERT INTO dbo.STORAGE_LOCATION(location_code, location_name, parent_location_id, location_type, max_capacity, current_quantity, [status]) VALUES
(N'1F-A-01-01-01', N'一楼A区01排01架第1层', NULL, N'REGULAR_SHELF', 50, 32, N'ACTIVE'),
(N'1F-A-01-01-02', N'一楼A区01排01架第2层', 1,    N'REGULAR_SHELF', 50, 41, N'ACTIVE'),
(N'1F-A-02-03-01', N'一楼A区02排03架第1层', NULL, N'REGULAR_SHELF', 50, 20, N'ACTIVE'),
(N'HOT-01',        N'热门图书区01号架',     1,    N'HOT_ZONE',      50, 36, N'ACTIVE'),
(N'HOT-02',        N'热门图书区02号架',     1,    N'HOT_ZONE',      50, 50, N'FULL'),
(N'NEW-01',        N'新书展示区01号架',     1,    N'NEW_BOOK',      30, 25, N'ACTIVE'),
(N'REF-01',        N'工具书区01号架',       1,    N'REFERENCE',     20, 10, N'ACTIVE'),
(N'JRN-01',        N'期刊区01号架',         1,    N'JOURNAL',       80, 70, N'ACTIVE'),
(N'RES-01',        N'预约书架01',           1,    N'RESERVATION_SHELF', 40, 12, N'ACTIVE'),
(N'REP-01',        N'待修复书籍区01',       1,    N'REPAIR_AREA',   60, 6,  N'ACTIVE'),
(N'2F-B-03-02',    N'二楼B区03排02架',      NULL, N'REGULAR_SHELF', 50, 22, N'ACTIVE'),
(N'2F-B-04-01',    N'二楼B区04排01架',      11,   N'REGULAR_SHELF', 50, 48, N'ACTIVE');
GO

------------------------------------------------------------
-- 14.3 作者（多一些）
------------------------------------------------------------
INSERT INTO dbo.AUTHOR(author_name, nationality, birth_year, biography) VALUES
(N'余华', N'中国', 1960, N'中国当代作家'),
(N'曹雪芹', N'中国', 1715, N'《红楼梦》作者（传统归属）'),
(N'鲁迅', N'中国', 1881, N'现代文学奠基人之一'),
(N'金庸', N'中国', 1924, N'武侠小说作家'),
(N'刘慈欣', N'中国', 1963, N'科幻作家'),
(N'Robert C. Martin', N'美国', 1952, N'Clean Code 作者'),
(N'Martin Fowler', N'英国', 1963, N'重构与架构领域作者'),
(N'Andrew Hunt', N'美国', 1964, N'程序员修炼之道作者'),
(N'David Thomas', N'美国', 1956, N'程序员修炼之道作者'),
(N'亚当·斯密', N'英国', 1723, N'经济学经典作者'),
(N'卡尔·马克思', N'德国', 1818, N'马克思主义理论家'),
(N'弗洛伊德', N'奥地利', 1856, N'心理学相关作者');
GO

------------------------------------------------------------
-- 14.4 书目（多一些）
-- 注意：ISBN 做 UNIQUE，这里用不同的字符串模拟即可
------------------------------------------------------------
INSERT INTO dbo.BIBLIOGRAPHY(ISBN, bibliography_name, publish, publish_date, [Description], category_id, price) VALUES
(N'9787506365437', N'活着', N'作家出版社', '2012-08-01', N'关于生命与命运的小说。', 5, 39.80),
(N'9787020002207', N'红楼梦', N'人民文学出版社', '1996-01-01', N'古典长篇小说。', 7, 59.00),
(N'9787506360000', N'许三观卖血记', N'作家出版社', '2005-05-01', N'余华作品。', 5, 36.00),
(N'9787020020001', N'呐喊', N'人民文学出版社', '2006-01-01', N'鲁迅小说集。', 6, 28.00),
(N'9787020020002', N'彷徨', N'人民文学出版社', '2006-02-01', N'鲁迅小说集。', 6, 29.00),
(N'9787806570003', N'三体', N'重庆出版社', '2008-01-01', N'科幻小说。', 5, 45.00),
(N'9787806570004', N'球状闪电', N'重庆出版社', '2005-01-01', N'科幻小说。', 5, 38.00),
(N'9787115216878', N'代码整洁之道', N'人民邮电出版社', '2010-01-01', N'Clean Code 中文版。', 11, 79.00),
(N'9787115000001', N'重构：改善既有代码的设计', N'人民邮电出版社', '2013-06-01', N'重构方法与实践。', 11, 88.00),
(N'9787115000002', N'程序员修炼之道', N'电子工业出版社', '2010-10-01', N'软件匠艺与工程实践。', 11, 69.00),
(N'9787100000001', N'国富论', N'商务印书馆', '2012-01-01', N'经济学经典。', 13, 98.00),
(N'9787010000001', N'资本论（第一卷）', N'人民出版社', '2004-01-01', N'政治经济学经典。', 12, 120.00);
GO

------------------------------------------------------------
-- 14.5 书目-作者关联（含多作者示例）
------------------------------------------------------------
-- 活着/许三观/三体/球状/呐喊/彷徨/红楼梦
INSERT INTO dbo.BIBLIO_AUTHOR(bibliography_id, author_id, author_order) VALUES
(1, 1, 1),
(2, 2, 1),
(3, 1, 1),
(4, 3, 1),
(5, 3, 1),
(6, 5, 1),
(7, 5, 1);

-- 代码整洁之道（单作者）
INSERT INTO dbo.BIBLIO_AUTHOR(bibliography_id, author_id, author_order) VALUES
(8, 6, 1);

-- 重构（单作者）
INSERT INTO dbo.BIBLIO_AUTHOR(bibliography_id, author_id, author_order) VALUES
(9, 7, 1);

-- 程序员修炼之道（双作者）
INSERT INTO dbo.BIBLIO_AUTHOR(bibliography_id, author_id, author_order) VALUES
(10, 8, 1),
(10, 9, 2);

-- 国富论、资本论
INSERT INTO dbo.BIBLIO_AUTHOR(bibliography_id, author_id, author_order) VALUES
(11, 10, 1),
(12, 11, 1);
GO

------------------------------------------------------------
-- 14.6 馆藏实体 BOOK_ITEM（每个书目多册）
------------------------------------------------------------
-- 规则：barcode 唯一；初始都 AVAILABLE
INSERT INTO dbo.BOOK_ITEM(item_barcode, bibliography_id, current_status, location_id, acquisition_date, price, physical_condition) VALUES
-- 活着（3册）
(N'BK-000001', 1, N'AVAILABLE', 4, '2024-09-01', 39.80, N'GOOD'),
(N'BK-000002', 1, N'AVAILABLE', 1, '2024-09-01', 39.80, N'GOOD'),
(N'BK-000003', 1, N'AVAILABLE', 2, '2025-03-10', 39.80, N'GOOD'),

-- 红楼梦（2册）
(N'BK-000004', 2, N'AVAILABLE', 1, '2023-03-12', 59.00, N'GOOD'),
(N'BK-000005', 2, N'AVAILABLE', 7, '2023-03-12', 59.00, N'GOOD'),

-- 许三观（2册）
(N'BK-000006', 3, N'AVAILABLE', 2, '2025-01-15', 36.00, N'GOOD'),
(N'BK-000007', 3, N'AVAILABLE', 3, '2025-01-15', 36.00, N'GOOD'),

-- 呐喊（2册）
(N'BK-000008', 4, N'AVAILABLE', 1, '2022-05-01', 28.00, N'GOOD'),
(N'BK-000009', 4, N'AVAILABLE', 8, '2022-05-01', 28.00, N'GOOD'),

-- 彷徨（1册）
(N'BK-000010', 5, N'AVAILABLE', 1, '2022-05-02', 29.00, N'GOOD'),

-- 三体（4册）
(N'BK-000011', 6, N'AVAILABLE', 4, '2024-10-01', 45.00, N'GOOD'),
(N'BK-000012', 6, N'AVAILABLE', 4, '2024-10-01', 45.00, N'GOOD'),
(N'BK-000013', 6, N'AVAILABLE', 6, '2025-11-01', 45.00, N'GOOD'),
(N'BK-000014', 6, N'AVAILABLE', 11,'2025-11-01', 45.00, N'GOOD'),

-- 球状闪电（2册）
(N'BK-000015', 7, N'AVAILABLE', 6, '2025-11-02', 38.00, N'GOOD'),
(N'BK-000016', 7, N'AVAILABLE', 11,'2025-11-02', 38.00, N'GOOD'),

-- 代码整洁之道（3册）
(N'BK-000017', 8, N'AVAILABLE', 11,'2025-10-10', 79.00, N'GOOD'),
(N'BK-000018', 8, N'AVAILABLE', 12,'2025-10-10', 79.00, N'GOOD'),
(N'BK-000019', 8, N'AVAILABLE', 9, '2025-10-10', 79.00, N'GOOD'),

-- 重构（2册）
(N'BK-000020', 9, N'AVAILABLE', 12,'2024-06-01', 88.00, N'GOOD'),
(N'BK-000021', 9, N'AVAILABLE', 9, '2024-06-01', 88.00, N'GOOD'),

-- 程序员修炼之道（2册）
(N'BK-000022',10, N'AVAILABLE', 12,'2024-06-15', 69.00, N'GOOD'),
(N'BK-000023',10, N'AVAILABLE', 9, '2024-06-15', 69.00, N'GOOD'),

-- 国富论（1册）
(N'BK-000024',11, N'AVAILABLE', 3, '2023-09-09', 98.00, N'GOOD'),

-- 资本论（2册）
(N'BK-000025',12, N'AVAILABLE', 3, '2023-09-10',120.00, N'GOOD'),
(N'BK-000026',12, N'AVAILABLE', 7, '2023-09-10',120.00, N'GOOD');
GO

------------------------------------------------------------
-- 14.7 借书证 readcard（多一些）
-- 注意：overdate 必须 = startdate + 1年；且 cardID 年份必须和 startdate 年匹配
------------------------------------------------------------
INSERT INTO dbo.readcard(cardID, startdate, overdate, [state]) VALUES
(N'BRW-2025-1-000001', '2025-02-01', DATEADD(YEAR,1,'2025-02-01'), N'正常'),
(N'BRW-2025-1-000002', '2025-03-05', DATEADD(YEAR,1,'2025-03-05'), N'正常'),
(N'BRW-2025-1-000003', '2025-09-01', DATEADD(YEAR,1,'2025-09-01'), N'正常'),
(N'BRW-2025-1-000004', '2025-10-12', DATEADD(YEAR,1,'2025-10-12'), N'补办中'),
(N'BRW-2025-2-000101', '2025-01-15', DATEADD(YEAR,1,'2025-01-15'), N'正常'),
(N'BRW-2025-2-000102', '2025-03-15', DATEADD(YEAR,1,'2025-03-15'), N'正常'),
(N'BRW-2025-2-000103', '2025-06-20', DATEADD(YEAR,1,'2025-06-20'), N'注销'),
(N'BRW-2025-2-000104', '2025-11-01', DATEADD(YEAR,1,'2025-11-01'), N'正常'),
(N'BRW-2025-3-000201', '2025-04-10', DATEADD(YEAR,1,'2025-04-10'), N'正常'),
(N'BRW-2025-3-000202', '2025-07-08', DATEADD(YEAR,1,'2025-07-08'), N'挂失'),
(N'BRW-2025-3-000203', '2025-11-20', DATEADD(YEAR,1,'2025-11-20'), N'正常'),
(N'BRW-2025-3-000204', '2025-12-05', DATEADD(YEAR,1,'2025-12-05'), N'正常');
GO

------------------------------------------------------------
-- 14.8 读者 reader（多一些）
------------------------------------------------------------
INSERT INTO dbo.reader(cardID, readername, readertype, unit, [number], borrowed_books_info, borroweddate, borrow_note) VALUES
(N'BRW-2025-1-000001', N'张三',   N'本校学生', N'电气工程学院', N'2023123456', N'（摘要）当前0本', NULL, NULL),
(N'BRW-2025-1-000002', N'李四',   N'本校学生', N'自动化学院',   N'2023111122', N'（摘要）当前0本', NULL, NULL),
(N'BRW-2025-1-000003', N'王小明', N'本校学生', N'计算机学院',   N'2023555566', N'（摘要）当前0本', NULL, NULL),
(N'BRW-2025-1-000004', N'陈晨',   N'本校学生', N'外国语学院',   N'2023777788', N'补办中，暂停借阅', NULL, N'证件补办中'),

(N'BRW-2025-2-000101', N'李老师', N'本校教师', N'计算机学院',   N'T20200088',  N'（摘要）当前0本', NULL, NULL),
(N'BRW-2025-2-000102', N'赵老师', N'本校教师', N'经济管理学院', N'T20190123',  N'（摘要）当前0本', NULL, NULL),
(N'BRW-2025-2-000103', N'孙老师', N'本校教师', N'图书馆',       N'T20180001',  N'注销状态，仅保留历史', NULL, N'已注销'),
(N'BRW-2025-2-000104', N'周老师', N'本校教师', N'医学院',       N'T20210077',  N'（摘要）当前0本', NULL, NULL),

(N'BRW-2025-3-000201', N'王五',   N'校外人员', N'社会读者',     NULL,          N'（摘要）当前0本', NULL, NULL),
(N'BRW-2025-3-000202', N'刘六',   N'校外人员', N'社会读者',     NULL,          N'挂失状态，暂停借阅', NULL, N'需补办'),
(N'BRW-2025-3-000203', N'钱七',   N'校外人员', N'企业访客',     NULL,          N'（摘要）当前0本', NULL, NULL),
(N'BRW-2025-3-000204', N'郑八',   N'校外人员', N'校友',         NULL,          N'（摘要）当前0本', NULL, NULL);
GO

------------------------------------------------------------
-- 14.9 借阅单头 borrow_record（用 OUTPUT 建映射，便于插入明细）
------------------------------------------------------------
/* =========================
   14.9 借阅单头 borrow_record（修正版：#BorrowMap + MERGE OUTPUT）
   ========================= */

IF OBJECT_ID('tempdb..#BorrowMap') IS NOT NULL DROP TABLE #BorrowMap;
IF OBJECT_ID('tempdb..#BorrowSrc') IS NOT NULL DROP TABLE #BorrowSrc;

CREATE TABLE #BorrowMap(
    keyname NVARCHAR(50) NOT NULL PRIMARY KEY,
    borrow_record_id BIGINT NOT NULL
);

CREATE TABLE #BorrowSrc(
    keyname NVARCHAR(50) NOT NULL PRIMARY KEY,
    cardID NVARCHAR(20) NOT NULL,
    borrowdate DATETIME2(0) NOT NULL,
    overdate DATETIME2(0) NULL,
    bcomplete NVARCHAR(20) NOT NULL,
    add_note NVARCHAR(200) NULL
);

INSERT INTO #BorrowSrc(keyname, cardID, borrowdate, overdate, bcomplete, add_note) VALUES
(N'BR1',  N'BRW-2025-1-000001', '2025-12-20 10:00:00', NULL,                  N'完好',      NULL),
(N'BR2',  N'BRW-2025-1-000001', '2025-11-10 09:30:00', '2025-11-20 16:10:00', N'完好',      NULL),
(N'BR3',  N'BRW-2025-1-000002', '2025-12-25 15:20:00', NULL,                  N'完好',      NULL),
(N'BR4',  N'BRW-2025-1-000003', '2025-12-01 14:00:00', '2025-12-08 10:00:00', N'轻微破损',  N'书页有轻微折角'),
(N'BR5',  N'BRW-2025-2-000101', '2025-12-01 14:30:00', '2025-12-10 09:15:00', N'轻微破损',  N'封面折痕'),
(N'BR6',  N'BRW-2025-2-000102', '2025-11-05 11:00:00', '2025-11-30 18:00:00', N'完好',      N'逾期归还（已记录）'),
(N'BR7',  N'BRW-2025-2-000104', '2025-12-28 09:10:00', NULL,                  N'完好',      NULL),
(N'BR8',  N'BRW-2025-3-000201', '2025-10-02 13:30:00', '2025-10-20 10:00:00', N'完好',      NULL),
(N'BR9',  N'BRW-2025-3-000203', '2025-12-15 16:40:00', '2025-12-29 09:00:00', N'严重破损',  N'书脊脱胶，需维修'),
(N'BR10', N'BRW-2025-3-000204', '2025-12-18 10:20:00', NULL,                  N'完好',      NULL);

-- 用 MERGE 强制插入，并在 OUTPUT 中安全引用 src.keyname
MERGE dbo.borrow_record AS tgt
USING #BorrowSrc AS src
ON 1 = 0
WHEN NOT MATCHED THEN
    INSERT(cardID, borrowdate, overdate, bcomplete, add_note)
    VALUES(src.cardID, src.borrowdate, src.overdate, src.bcomplete, src.add_note)
OUTPUT src.keyname, inserted.borrow_record_id
INTO #BorrowMap(keyname, borrow_record_id);


/* =========================
   14.10 借阅明细 bookborrow（修正版：JOIN #BorrowMap）
   ========================= */
INSERT INTO dbo.bookborrow(borrow_record_id, cardID, bookID, borrowdate, overdate, add_note)
SELECT bm.borrow_record_id, x.cardID, x.bookID, x.borrowdate, x.overdate, x.add_note
FROM (VALUES
 (N'BR1',  N'BRW-2025-1-000001', N'BK-000001', '2025-12-20 10:00:00', NULL,                  NULL),
 (N'BR2',  N'BRW-2025-1-000001', N'BK-000004', '2025-11-10 09:30:00', '2025-11-20 16:10:00', NULL),
 (N'BR3',  N'BRW-2025-1-000002', N'BK-000011', '2025-12-25 15:20:00', NULL,                  NULL),
 (N'BR4',  N'BRW-2025-1-000003', N'BK-000009', '2025-12-01 14:00:00', '2025-12-08 10:00:00', N'轻微折角'),
 (N'BR5',  N'BRW-2025-2-000101', N'BK-000017', '2025-12-01 14:30:00', '2025-12-10 09:15:00', N'封面折痕'),
 (N'BR6',  N'BRW-2025-2-000102', N'BK-000024', '2025-11-05 11:00:00', '2025-11-30 18:00:00', N'逾期归还'),
 (N'BR7',  N'BRW-2025-2-000104', N'BK-000020', '2025-12-28 09:10:00', NULL,                  NULL),
 (N'BR8',  N'BRW-2025-3-000201', N'BK-000006', '2025-10-02 13:30:00', '2025-10-20 10:00:00', NULL),
 (N'BR9',  N'BRW-2025-3-000203', N'BK-000025', '2025-12-15 16:40:00', '2025-12-29 09:00:00', N'书脊脱胶'),
 (N'BR10', N'BRW-2025-3-000204', N'BK-000015', '2025-12-18 10:20:00', NULL,                  NULL)
) AS x(keyname, cardID, bookID, borrowdate, overdate, add_note)
JOIN #BorrowMap bm ON bm.keyname = x.keyname;

-- 可选：用完就清理
DROP TABLE #BorrowSrc;
DROP TABLE #BorrowMap;

------------------------------------------------------------
-- 14.11 根据未归还记录更新 BOOK_ITEM 状态（示例）
------------------------------------------------------------
UPDATE bi
SET bi.current_status = N'BORROWED',
    bi.status_changed_date = SYSDATETIME()
FROM dbo.BOOK_ITEM bi
WHERE EXISTS (
    SELECT 1
    FROM dbo.bookborrow bb
    WHERE bb.bookID = bi.item_barcode AND bb.overdate IS NULL
);

-- 严重破损示例：把 BK-000025 标记为 DAMAGED（已归还但需要维修）
UPDATE dbo.BOOK_ITEM
SET physical_condition = N'DAMAGED',
    status_changed_date = SYSDATETIME()
WHERE item_barcode = N'BK-000025';
GO

------------------------------------------------------------
-- 14.12 罚款记录 fine（多一些）
------------------------------------------------------------
INSERT INTO dbo.fine(cardID, readername, reason, amount, fine_status) VALUES
(N'BRW-2025-2-000101', N'李老师', N'图书轻微破损（封面折痕），收取修补费', 5.00,  N'未支付'),
(N'BRW-2025-2-000102', N'赵老师', N'逾期归还（超期），按规定计费',         8.00,  N'已支付'),
(N'BRW-2025-1-000003', N'王小明', N'图书轻微破损（书页折角），收取修补费', 3.00,  N'已支付'),
(N'BRW-2025-3-000203', N'钱七',   N'严重破损（书脊脱胶），需维修处理费',   20.00, N'未支付');
GO

------------------------------------------------------------
-- 14.13 编目日志 catalog_log（多一些）
------------------------------------------------------------
INSERT INTO dbo.catalog_log(target_type, target_id, action_type, operator, note) VALUES
(N'CATEGORY',     N'I247.5',     N'新增',     N'admin001', N'新增分类：新体长篇、中篇小说'),
(N'CATEGORY',     N'TP312',      N'新增',     N'admin001', N'新增分类：程序设计、软件工程'),
(N'LOCATION',     N'HOT-02',     N'更新',     N'admin001', N'热门区02库存已满'),
(N'LOCATION',     N'REP-01',     N'更新',     N'lib002',   N'待修复区新增入库'),
(N'BIBLIOGRAPHY', N'9787115216878', N'新增',  N'lib002',   N'录入书目《代码整洁之道》'),
(N'BIBLIOGRAPHY', N'9787115000002', N'新增',  N'lib002',   N'录入书目《程序员修炼之道》'),
(N'BOOK_ITEM',    N'BK-000001',  N'状态变更', N'lib002',   N'借出：张三'),
(N'BOOK_ITEM',    N'BK-000011',  N'状态变更', N'lib002',   N'借出：李四'),
(N'BOOK_ITEM',    N'BK-000025',  N'状态变更', N'lib002',   N'归还：严重破损，转入待修复'),
(N'BOOK_ITEM',    N'BK-000020',  N'状态变更', N'lib002',   N'借出：周老师');
GO

------------------------------------------------------------
-- 14.14 可选：给 reader 的冗余摘要字段更新一下（示例演示）
------------------------------------------------------------
UPDATE r
SET borrowed_books_info = CONCAT(N'当前未归还：', COALESCE(t.cnt,0), N' 本'),
    borroweddate = t.last_borrow_date
FROM dbo.reader r
OUTER APPLY (
    SELECT
        COUNT(*) AS cnt,
        CONVERT(date, MAX(bb.borrowdate)) AS last_borrow_date
    FROM dbo.bookborrow bb
    WHERE bb.cardID = r.cardID AND bb.overdate IS NULL
) t;
GO

PRINT N'✅ LibraryDB 已创建（如不存在），所有表已建立，且已插入较多示例数据。';
GO

/* =========================================================
   15) 快速查看（可选）
   ========================================================= */
-- SELECT TOP 50 * FROM dbo.BOOK_CATEGORY ORDER BY category_id;
-- SELECT TOP 50 * FROM dbo.STORAGE_LOCATION ORDER BY location_id;
-- SELECT TOP 50 * FROM dbo.BIBLIOGRAPHY ORDER BY bibliography_id;
-- SELECT TOP 50 * FROM dbo.BOOK_ITEM ORDER BY item_barcode;
-- SELECT TOP 50 * FROM dbo.reader ORDER BY cardID;
-- SELECT TOP 50 * FROM dbo.bookborrow ORDER BY bookborrow_id;
-- SELECT TOP 50 * FROM dbo.fine ORDER BY fine_id;
