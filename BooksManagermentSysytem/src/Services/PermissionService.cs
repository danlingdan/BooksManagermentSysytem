using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Models;

namespace BooksManagermentSysytem.Services
{
    /// <summary>
    /// 权限管理服务 - 功能权限精细化管理
    /// </summary>
    public class PermissionService
    {
        private static PermissionService _instance;
        private static readonly object _lock = new object();

        public static PermissionService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new PermissionService();
                        }
                    }
                }
                return _instance;
            }
        }

        private PermissionService()
        {
        }

        /// <summary>
        /// 获取所有权限列表
        /// </summary>
        public List<Permission> GetAllPermissions()
        {
            string sql = @"
                SELECT permission_id, permission_code, permission_name, 
                       permission_group, description, is_active, 
                       created_time, updated_time
                FROM sys_permission
                WHERE is_active = 1
                ORDER BY permission_group, permission_id";

            DataTable dt = DatabaseHelper.ExecuteQuery(sql);
            return ConvertToPermissionList(dt);
        }

        /// <summary>
        /// 获取指定角色的权限列表
        /// </summary>
        public List<Permission> GetRolePermissions(string roleName)
        {
            string sql = @"
                SELECT p.permission_id, p.permission_code, p.permission_name, 
                       p.permission_group, p.description, p.is_active, 
                       p.created_time, p.updated_time
                FROM sys_permission p
                INNER JOIN sys_role_permission rp ON p.permission_code = rp.permission_code
                WHERE rp.role_name = @roleName AND p.is_active = 1
                ORDER BY p.permission_group, p.permission_id";

            DataTable dt = DatabaseHelper.ExecuteQuery(sql,
                DatabaseHelper.CreateParameter("@roleName", roleName));

            return ConvertToPermissionList(dt);
        }

        /// <summary>
        /// 获取角色权限配置（包含已授权和未授权的权限）
        /// </summary>
        public List<RolePermissionConfig> GetRolePermissionConfig(string roleName)
        {
            string sql = @"
                SELECT 
                    p.permission_id,
                    p.permission_code,
                    p.permission_name,
                    p.permission_group,
                    p.description,
                    CASE WHEN rp.role_permission_id IS NOT NULL THEN 1 ELSE 0 END AS is_granted
                FROM sys_permission p
                LEFT JOIN sys_role_permission rp ON p.permission_code = rp.permission_code 
                    AND rp.role_name = @roleName
                WHERE p.is_active = 1
                ORDER BY p.permission_group, p.permission_id";

            DataTable dt = DatabaseHelper.ExecuteQuery(sql,
                DatabaseHelper.CreateParameter("@roleName", roleName));

            List<RolePermissionConfig> configs = new List<RolePermissionConfig>();
            foreach (DataRow row in dt.Rows)
            {
                configs.Add(new RolePermissionConfig
                {
                    PermissionId = Convert.ToInt32(row["permission_id"]),
                    PermissionCode = row["permission_code"].ToString(),
                    PermissionName = row["permission_name"].ToString(),
                    PermissionGroup = row["permission_group"].ToString(),
                    Description = row["description"] == DBNull.Value ? null : row["description"].ToString(),
                    IsGranted = Convert.ToInt32(row["is_granted"]) == 1
                });
            }

            return configs;
        }

        /// <summary>
        /// 设置角色权限
        /// </summary>
        public bool SetRolePermissions(string roleName, List<string> permissionCodes, string grantedBy, out string errorMessage)
        {
            errorMessage = null;

            try
            {
                string deleteSql = "DELETE FROM sys_role_permission WHERE role_name = @roleName";
                DatabaseHelper.ExecuteNonQuery(deleteSql,
                    DatabaseHelper.CreateParameter("@roleName", roleName));

                if (permissionCodes != null && permissionCodes.Count > 0)
                {
                    string insertSql = @"
                        INSERT INTO sys_role_permission (role_name, permission_code, granted_by)
                        VALUES (@roleName, @permissionCode, @grantedBy)";

                    foreach (string permissionCode in permissionCodes)
                    {
                        DatabaseHelper.ExecuteNonQuery(insertSql,
                            DatabaseHelper.CreateParameter("@roleName", roleName),
                            DatabaseHelper.CreateParameter("@permissionCode", permissionCode),
                            DatabaseHelper.CreateParameter("@grantedBy", grantedBy));
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "设置角色权限失败：" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 授予角色单个权限
        /// </summary>
        public bool GrantPermission(string roleName, string permissionCode, string grantedBy, out string errorMessage)
        {
            errorMessage = null;

            try
            {
                string checkSql = @"
                    SELECT COUNT(*) 
                    FROM sys_role_permission 
                    WHERE role_name = @roleName AND permission_code = @permissionCode";

                int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(checkSql,
                    DatabaseHelper.CreateParameter("@roleName", roleName),
                    DatabaseHelper.CreateParameter("@permissionCode", permissionCode)));

                if (count > 0)
                {
                    errorMessage = "该角色已拥有此权限";
                    return false;
                }

                string insertSql = @"
                    INSERT INTO sys_role_permission (role_name, permission_code, granted_by)
                    VALUES (@roleName, @permissionCode, @grantedBy)";

                DatabaseHelper.ExecuteNonQuery(insertSql,
                    DatabaseHelper.CreateParameter("@roleName", roleName),
                    DatabaseHelper.CreateParameter("@permissionCode", permissionCode),
                    DatabaseHelper.CreateParameter("@grantedBy", grantedBy));

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "授予权限失败：" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 撤销角色权限
        /// </summary>
        public bool RevokePermission(string roleName, string permissionCode, out string errorMessage)
        {
            errorMessage = null;

            try
            {
                string deleteSql = @"
                    DELETE FROM sys_role_permission 
                    WHERE role_name = @roleName AND permission_code = @permissionCode";

                int affected = DatabaseHelper.ExecuteNonQuery(deleteSql,
                    DatabaseHelper.CreateParameter("@roleName", roleName),
                    DatabaseHelper.CreateParameter("@permissionCode", permissionCode));

                if (affected == 0)
                {
                    errorMessage = "该角色不拥有此权限";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "撤销权限失败：" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 检查用户是否有指定权限
        /// </summary>
        public bool CheckUserPermission(int userId, string permissionCode)
        {
            try
            {
                string sql = @"
                    SELECT COUNT(*) 
                    FROM app_user u
                    INNER JOIN sys_role_permission rp ON u.user_role = rp.role_name
                    WHERE u.user_id = @userId 
                        AND rp.permission_code = @permissionCode
                        AND u.is_active = 1";

                int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(sql,
                    DatabaseHelper.CreateParameter("@userId", userId),
                    DatabaseHelper.CreateParameter("@permissionCode", permissionCode)));

                return count > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取用户权限列表
        /// </summary>
        public List<string> GetUserPermissions(int userId)
        {
            try
            {
                string sql = @"
                    SELECT DISTINCT rp.permission_code
                    FROM app_user u
                    INNER JOIN sys_role_permission rp ON u.user_role = rp.role_name
                    INNER JOIN sys_permission p ON rp.permission_code = p.permission_code
                    WHERE u.user_id = @userId 
                        AND u.is_active = 1
                        AND p.is_active = 1";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@userId", userId));

                List<string> permissions = new List<string>();
                foreach (DataRow row in dt.Rows)
                {
                    permissions.Add(row["permission_code"].ToString());
                }

                return permissions;
            }
            catch
            {
                return new List<string>();
            }
        }

        /// <summary>
        /// 获取权限分组列表
        /// </summary>
        public List<string> GetPermissionGroups()
        {
            string sql = @"
                SELECT DISTINCT permission_group 
                FROM sys_permission 
                WHERE is_active = 1
                ORDER BY permission_group";

            DataTable dt = DatabaseHelper.ExecuteQuery(sql);

            List<string> groups = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                groups.Add(row["permission_group"].ToString());
            }

            return groups;
        }

        private List<Permission> ConvertToPermissionList(DataTable dt)
        {
            List<Permission> permissions = new List<Permission>();
            foreach (DataRow row in dt.Rows)
            {
                permissions.Add(new Permission
                {
                    PermissionId = Convert.ToInt32(row["permission_id"]),
                    PermissionCode = row["permission_code"].ToString(),
                    PermissionName = row["permission_name"].ToString(),
                    PermissionGroup = row["permission_group"].ToString(),
                    Description = row["description"] == DBNull.Value ? null : row["description"].ToString(),
                    IsActive = Convert.ToBoolean(row["is_active"]),
                    CreatedTime = Convert.ToDateTime(row["created_time"]),
                    UpdatedTime = row["updated_time"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["updated_time"])
                });
            }

            return permissions;
        }
    }

    /// <summary>
    /// 角色权限配置辅助类
    /// </summary>
    public class RolePermissionConfig
    {
        public int PermissionId { get; set; }
        public string PermissionCode { get; set; }
        public string PermissionName { get; set; }
        public string PermissionGroup { get; set; }
        public string Description { get; set; }
        public bool IsGranted { get; set; }
    }
}
