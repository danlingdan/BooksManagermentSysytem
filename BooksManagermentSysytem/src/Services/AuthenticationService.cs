using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Models;

namespace BooksManagermentSysytem.Services
{
    /// <summary>
    /// 认证服务类 - 处理用户登录、注册和会话管理
    /// </summary>
    public class AuthenticationService
    {
        private static AuthenticationService _instance;
        private static readonly object _lock = new object();

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public SystemUser CurrentUser { get; private set; }

        /// <summary>
        /// 是否已登录
        /// </summary>
        public bool IsLoggedIn => CurrentUser != null;

        /// <summary>
        /// 获取单例实例
        /// </summary>
        public static AuthenticationService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new AuthenticationService();
                        }
                    }
                }
                return _instance;
            }
        }

        private AuthenticationService() { }

        /// <summary>
        /// 用户名密码登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="errorMessage">错误信息</param>
        /// <returns>是否登录成功</returns>
        public bool Login(string username, string password, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                errorMessage = "用户名和密码不能为空";
                return false;
            }

            try
            {
                string sql = @"SELECT user_id, username, password_hash, salt, user_role, cardID, 
                              windows_account, display_name, is_active, created_time, last_login_time
                              FROM app_user WHERE username = @username";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@username", username));

                if (dt.Rows.Count == 0)
                {
                    errorMessage = "用户名或密码错误";
                    return false;
                }

                DataRow row = dt.Rows[0];

                if (!(bool)row["is_active"])
                {
                    errorMessage = "账户已被禁用，请联系管理员";
                    return false;
                }

                string storedHash = row["password_hash"].ToString();
                string salt = row["salt"].ToString();
                string computedHash = ComputeHash(password, salt);

                if (storedHash != computedHash)
                {
                    errorMessage = "用户名或密码错误";
                    return false;
                }

                // 登录成功，创建用户对象
                CurrentUser = MapToSystemUser(row);

                // 更新最后登录时间
                UpdateLastLoginTime(CurrentUser.UserId);

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "登录失败：" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Windows 凭证登录
        /// </summary>
        /// <param name="errorMessage">错误信息</param>
        /// <returns>是否登录成功</returns>
        public bool LoginWithWindows(out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                string windowsAccount = WindowsIdentity.GetCurrent().Name;

                string sql = @"SELECT user_id, username, password_hash, salt, user_role, cardID, 
                              windows_account, display_name, is_active, created_time, last_login_time
                              FROM app_user WHERE windows_account = @windowsAccount AND is_active = 1";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@windowsAccount", windowsAccount));

                if (dt.Rows.Count == 0)
                {
                    errorMessage = $"Windows 账户 {windowsAccount} 未绑定系统用户";
                    return false;
                }

                DataRow row = dt.Rows[0];
                CurrentUser = MapToSystemUser(row);

                // 更新最后登录时间
                UpdateLastLoginTime(CurrentUser.UserId);

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Windows 登录失败：" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 注册新用户
        /// </summary>
        public bool Register(string username, string password, string displayName, 
            UserRole role, string cardID, string windowsAccount, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                errorMessage = "用户名和密码不能为空";
                return false;
            }

            if (password.Length < 6)
            {
                errorMessage = "密码长度至少为6位";
                return false;
            }

            try
            {
                // 检查用户名是否已存在
                string checkSql = "SELECT COUNT(*) FROM app_user WHERE username = @username";
                int count = (int)DatabaseHelper.ExecuteScalar(checkSql,
                    DatabaseHelper.CreateParameter("@username", username));

                if (count > 0)
                {
                    errorMessage = "用户名已存在";
                    return false;
                }

                // 如果是读者，检查借书证是否存在
                if (role == UserRole.Reader && !string.IsNullOrEmpty(cardID))
                {
                    string checkCardSql = "SELECT COUNT(*) FROM reader WHERE cardID = @cardID";
                    int cardCount = (int)DatabaseHelper.ExecuteScalar(checkCardSql,
                        DatabaseHelper.CreateParameter("@cardID", cardID));

                    if (cardCount == 0)
                    {
                        errorMessage = "借书证号不存在";
                        return false;
                    }

                    // 检查借书证是否已绑定
                    string checkBindSql = "SELECT COUNT(*) FROM app_user WHERE cardID = @cardID";
                    int bindCount = (int)DatabaseHelper.ExecuteScalar(checkBindSql,
                        DatabaseHelper.CreateParameter("@cardID", cardID));

                    if (bindCount > 0)
                    {
                        errorMessage = "该借书证已绑定其他账户";
                        return false;
                    }
                }

                // 生成盐值和哈希密码
                string salt = GenerateSalt();
                string passwordHash = ComputeHash(password, salt);

                string insertSql = @"INSERT INTO app_user 
                    (username, password_hash, salt, user_role, cardID, windows_account, display_name, is_active)
                    VALUES (@username, @passwordHash, @salt, @role, @cardID, @windowsAccount, @displayName, 1)";

                DatabaseHelper.ExecuteNonQuery(insertSql,
                    DatabaseHelper.CreateParameter("@username", username),
                    DatabaseHelper.CreateParameter("@passwordHash", passwordHash),
                    DatabaseHelper.CreateParameter("@salt", salt),
                    DatabaseHelper.CreateParameter("@role", role.ToString()),
                    DatabaseHelper.CreateParameter("@cardID", string.IsNullOrEmpty(cardID) ? (object)DBNull.Value : cardID),
                    DatabaseHelper.CreateParameter("@windowsAccount", string.IsNullOrEmpty(windowsAccount) ? (object)DBNull.Value : windowsAccount),
                    DatabaseHelper.CreateParameter("@displayName", displayName));

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "注册失败：" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        public bool ChangePassword(string oldPassword, string newPassword, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!IsLoggedIn)
            {
                errorMessage = "请先登录";
                return false;
            }

            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
            {
                errorMessage = "新密码长度至少为6位";
                return false;
            }

            try
            {
                // 验证旧密码
                string sql = "SELECT password_hash, salt FROM app_user WHERE user_id = @userId";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@userId", CurrentUser.UserId));

                if (dt.Rows.Count == 0)
                {
                    errorMessage = "用户不存在";
                    return false;
                }

                string storedHash = dt.Rows[0]["password_hash"].ToString();
                string salt = dt.Rows[0]["salt"].ToString();
                string computedHash = ComputeHash(oldPassword, salt);

                if (storedHash != computedHash)
                {
                    errorMessage = "旧密码错误";
                    return false;
                }

                // 生成新的盐值和哈希
                string newSalt = GenerateSalt();
                string newHash = ComputeHash(newPassword, newSalt);

                string updateSql = "UPDATE app_user SET password_hash = @hash, salt = @salt WHERE user_id = @userId";
                DatabaseHelper.ExecuteNonQuery(updateSql,
                    DatabaseHelper.CreateParameter("@hash", newHash),
                    DatabaseHelper.CreateParameter("@salt", newSalt),
                    DatabaseHelper.CreateParameter("@userId", CurrentUser.UserId));

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "修改密码失败：" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 注销登录
        /// </summary>
        public void Logout()
        {
            CurrentUser = null;
        }

        /// <summary>
        /// 绑定 Windows 账户
        /// </summary>
        public bool BindWindowsAccount(out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!IsLoggedIn)
            {
                errorMessage = "请先登录";
                return false;
            }

            try
            {
                string windowsAccount = WindowsIdentity.GetCurrent().Name;

                // 检查是否已被其他用户绑定
                string checkSql = "SELECT COUNT(*) FROM app_user WHERE windows_account = @account AND user_id <> @userId";
                int count = (int)DatabaseHelper.ExecuteScalar(checkSql,
                    DatabaseHelper.CreateParameter("@account", windowsAccount),
                    DatabaseHelper.CreateParameter("@userId", CurrentUser.UserId));

                if (count > 0)
                {
                    errorMessage = "该 Windows 账户已绑定其他用户";
                    return false;
                }

                string updateSql = "UPDATE app_user SET windows_account = @account WHERE user_id = @userId";
                DatabaseHelper.ExecuteNonQuery(updateSql,
                    DatabaseHelper.CreateParameter("@account", windowsAccount),
                    DatabaseHelper.CreateParameter("@userId", CurrentUser.UserId));

                CurrentUser.WindowsAccount = windowsAccount;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "绑定失败：" + ex.Message;
                return false;
            }
        }

        #region 私有方法

        private SystemUser MapToSystemUser(DataRow row)
        {
            return new SystemUser
            {
                UserId = Convert.ToInt32(row["user_id"]),
                Username = row["username"].ToString(),
                PasswordHash = row["password_hash"].ToString(),
                Salt = row["salt"].ToString(),
                Role = ParseRole(row["user_role"].ToString()),
                CardID = row["cardID"] == DBNull.Value ? null : row["cardID"].ToString(),
                WindowsAccount = row["windows_account"] == DBNull.Value ? null : row["windows_account"].ToString(),
                DisplayName = row["display_name"].ToString(),
                IsActive = (bool)row["is_active"],
                CreatedTime = (DateTime)row["created_time"],
                LastLoginTime = row["last_login_time"] == DBNull.Value ? (DateTime?)null : (DateTime)row["last_login_time"]
            };
        }

        private UserRole ParseRole(string roleString)
        {
            switch (roleString)
            {
                case "Reader": return UserRole.Reader;
                case "Librarian": return UserRole.Librarian;
                case "Cataloger": return UserRole.Cataloger;
                case "Admin": return UserRole.Admin;
                default: return UserRole.Reader;
            }
        }

        private void UpdateLastLoginTime(int userId)
        {
            try
            {
                string sql = "UPDATE app_user SET last_login_time = GETDATE() WHERE user_id = @userId";
                DatabaseHelper.ExecuteNonQuery(sql, DatabaseHelper.CreateParameter("@userId", userId));
            }
            catch
            {
                // 忽略更新登录时间的错误
            }
        }

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private string ComputeHash(string password, string salt)
        {
            string combined = password + salt;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        #endregion
    }
}
