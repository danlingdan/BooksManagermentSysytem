-- =============================================
-- 功能权限精细化管理 - 数据库架构
-- 创建时间: 2025
-- 说明: 实现细粒度权限控制和角色权限管理
-- =============================================

USE LibraryDB;
GO

-- 1. 创建权限表
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'sys_permission')
BEGIN
    CREATE TABLE sys_permission (
        permission_id INT IDENTITY(1,1) PRIMARY KEY,
        permission_code NVARCHAR(50) NOT NULL UNIQUE,  -- 权限代码，如 BOOK_CATALOG
        permission_name NVARCHAR(100) NOT NULL,        -- 权限名称，如 "图书编目"
        permission_group NVARCHAR(50) NOT NULL,        -- 权限分组，如 "图书管理"
        description NVARCHAR(500) NULL,                -- 权限说明
        is_active BIT NOT NULL DEFAULT(1),             -- 是否启用
        created_time DATETIME2(0) NOT NULL DEFAULT(SYSDATETIME()),
        updated_time DATETIME2(0) NULL
    );

    CREATE INDEX idx_permission_code ON sys_permission(permission_code);
    CREATE INDEX idx_permission_group ON sys_permission(permission_group);
END
GO

-- 2. 创建角色权限关联表
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'sys_role_permission')
BEGIN
    CREATE TABLE sys_role_permission (
        role_permission_id INT IDENTITY(1,1) PRIMARY KEY,
        role_name NVARCHAR(20) NOT NULL,              -- 角色名称：Reader/Librarian/Cataloger/Admin
        permission_code NVARCHAR(50) NOT NULL,        -- 权限代码
        granted_by NVARCHAR(50) NULL,                 -- 授权人
        granted_time DATETIME2(0) NOT NULL DEFAULT(SYSDATETIME()),
        
        CONSTRAINT uq_role_permission UNIQUE(role_name, permission_code),
        CONSTRAINT fk_role_permission_code FOREIGN KEY(permission_code) 
            REFERENCES sys_permission(permission_code) ON DELETE CASCADE
    );

    CREATE INDEX idx_role_name ON sys_role_permission(role_name);
END
GO

-- 3. 初始化基础权限数据
-- 删除现有数据（如果存在）
DELETE FROM sys_role_permission;
DELETE FROM sys_permission;
GO

-- 插入权限项
SET IDENTITY_INSERT sys_permission ON;

-- 图书管理类权限
INSERT INTO sys_permission (permission_id, permission_code, permission_name, permission_group, description) VALUES
(1, 'BOOK_CATALOG', N'图书编目', N'图书管理', N'创建和编辑书目信息'),
(2, 'BOOK_CATALOG_DELETE', N'删除书目', N'图书管理', N'删除书目记录'),
(3, 'BOOK_ITEM_MANAGE', N'馆藏管理', N'图书管理', N'管理馆藏实体，包括新增、下架等'),
(4, 'BOOK_CATEGORY_MANAGE', N'分类管理', N'图书管理', N'管理图书分类体系'),
(5, 'BOOK_LOCATION_MANAGE', N'库位管理', N'图书管理', N'管理图书库位信息');

-- 读者服务类权限
INSERT INTO sys_permission (permission_id, permission_code, permission_name, permission_group, description) VALUES
(10, 'READER_INFO_VIEW', N'读者信息查看', N'读者服务', N'查看读者基本信息'),
(11, 'READER_INFO_MODIFY', N'读者信息修改', N'读者服务', N'修改读者基本信息'),
(12, 'READER_CARD_MANAGE', N'借书证管理', N'读者服务', N'管理借书证的办理、挂失、补办等'),
(13, 'BORROW_OPERATION', N'借阅操作', N'读者服务', N'办理图书借阅和归还'),
(14, 'BORROW_RENEW', N'续借操作', N'读者服务', N'办理图书续借'),
(15, 'RESERVATION_MANAGE', N'预约管理', N'读者服务', N'管理图书预约');

-- 规则配置类权限
INSERT INTO sys_permission (permission_id, permission_code, permission_name, permission_group, description) VALUES
(20, 'BORROW_RULE_SETTING', N'借阅规则设置', N'规则配置', N'配置借阅规则参数'),
(21, 'FINE_RULE_SETTING', N'处罚规则设置', N'规则配置', N'配置处罚规则参数'),
(22, 'FINE_MANAGE', N'罚款管理', N'规则配置', N'管理罚款记录和缴费');

-- 系统管理类权限
INSERT INTO sys_permission (permission_id, permission_code, permission_name, permission_group, description) VALUES
(30, 'USER_MANAGE', N'用户管理', N'系统管理', N'管理系统用户账户'),
(31, 'ROLE_PERMISSION_MANAGE', N'角色权限管理', N'系统管理', N'配置角色权限关系'),
(32, 'SYSTEM_LOG_VIEW', N'系统日志查看', N'系统管理', N'查看系统操作日志'),
(33, 'SYSTEM_CONFIG', N'系统配置', N'系统管理', N'修改系统配置参数');

-- 统计分析类权限
INSERT INTO sys_permission (permission_id, permission_code, permission_name, permission_group, description) VALUES
(40, 'REPORT_BORROW_STATS', N'借阅统计报表', N'统计分析', N'查看借阅统计数据'),
(41, 'REPORT_READER_STATS', N'读者统计报表', N'统计分析', N'查看读者统计数据'),
(42, 'REPORT_BOOK_STATS', N'图书统计报表', N'统计分析', N'查看图书统计数据');

SET IDENTITY_INSERT sys_permission OFF;
GO

-- 4. 初始化默认角色权限关系
-- Admin - 拥有所有权限
INSERT INTO sys_role_permission (role_name, permission_code, granted_by) 
SELECT N'Admin', permission_code, N'SYSTEM' FROM sys_permission WHERE is_active = 1;

-- Cataloger (图书采编员) - 图书管理相关权限
INSERT INTO sys_role_permission (role_name, permission_code, granted_by) VALUES
(N'Cataloger', N'BOOK_CATALOG', N'SYSTEM'),
(N'Cataloger', N'BOOK_CATALOG_DELETE', N'SYSTEM'),
(N'Cataloger', N'BOOK_ITEM_MANAGE', N'SYSTEM'),
(N'Cataloger', N'BOOK_CATEGORY_MANAGE', N'SYSTEM'),
(N'Cataloger', N'BOOK_LOCATION_MANAGE', N'SYSTEM'),
(N'Cataloger', N'READER_INFO_VIEW', N'SYSTEM'),
(N'Cataloger', N'REPORT_BOOK_STATS', N'SYSTEM');

-- Librarian (图书管理员) - 读者服务相关权限
INSERT INTO sys_role_permission (role_name, permission_code, granted_by) VALUES
(N'Librarian', N'READER_INFO_VIEW', N'SYSTEM'),
(N'Librarian', N'READER_INFO_MODIFY', N'SYSTEM'),
(N'Librarian', N'READER_CARD_MANAGE', N'SYSTEM'),
(N'Librarian', N'BORROW_OPERATION', N'SYSTEM'),
(N'Librarian', N'BORROW_RENEW', N'SYSTEM'),
(N'Librarian', N'RESERVATION_MANAGE', N'SYSTEM'),
(N'Librarian', N'FINE_MANAGE', N'SYSTEM'),
(N'Librarian', N'REPORT_BORROW_STATS', N'SYSTEM'),
(N'Librarian', N'REPORT_READER_STATS', N'SYSTEM');

-- Reader (读者) - 基础查看权限
INSERT INTO sys_role_permission (role_name, permission_code, granted_by) VALUES
(N'Reader', N'READER_INFO_VIEW', N'SYSTEM');

GO

-- 5. 创建权限检查存储过程
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_check_user_permission')
    DROP PROCEDURE sp_check_user_permission;
GO

CREATE PROCEDURE sp_check_user_permission
    @user_id INT,
    @permission_code NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- 检查用户角色是否拥有指定权限
    SELECT COUNT(*) AS has_permission
    FROM app_user u
    INNER JOIN sys_role_permission rp ON u.user_role = rp.role_name
    WHERE u.user_id = @user_id 
        AND rp.permission_code = @permission_code
        AND u.is_active = 1;
END
GO

-- 6. 创建获取角色权限列表的存储过程
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_get_role_permissions')
    DROP PROCEDURE sp_get_role_permissions;
GO

CREATE PROCEDURE sp_get_role_permissions
    @role_name NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        p.permission_id,
        p.permission_code,
        p.permission_name,
        p.permission_group,
        p.description,
        CASE WHEN rp.role_permission_id IS NOT NULL THEN 1 ELSE 0 END AS is_granted
    FROM sys_permission p
    LEFT JOIN sys_role_permission rp ON p.permission_code = rp.permission_code 
        AND rp.role_name = @role_name
    WHERE p.is_active = 1
    ORDER BY p.permission_group, p.permission_id;
END
GO

-- 7. 创建获取用户权限列表的存储过程
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_get_user_permissions')
    DROP PROCEDURE sp_get_user_permissions;
GO

CREATE PROCEDURE sp_get_user_permissions
    @user_id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT DISTINCT
        p.permission_code,
        p.permission_name,
        p.permission_group
    FROM app_user u
    INNER JOIN sys_role_permission rp ON u.user_role = rp.role_name
    INNER JOIN sys_permission p ON rp.permission_code = p.permission_code
    WHERE u.user_id = @user_id 
        AND u.is_active = 1
        AND p.is_active = 1
    ORDER BY p.permission_group, p.permission_code;
END
GO

-- 8. 完成提示
DECLARE @permission_count INT;
SELECT @permission_count = COUNT(*) FROM sys_permission;

PRINT N'=============================================';
PRINT N'功能权限精细化管理数据库架构创建完成';
PRINT N'- 已创建 sys_permission 表（权限表）';
PRINT N'- 已创建 sys_role_permission 表（角色权限关联表）';
PRINT N'- 已初始化 ' + CAST(@permission_count AS NVARCHAR) + N' 个权限项';
PRINT N'- 已配置默认角色权限关系';
PRINT N'- 已创建相关存储过程';
PRINT N'=============================================';
GO
