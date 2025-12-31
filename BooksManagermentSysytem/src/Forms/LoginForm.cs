using System;
using System.Windows.Forms;
using BooksManagermentSysytem.Services;

namespace BooksManagermentSysytem.Forms
{
    /// <summary>
    /// 登录窗体
    /// </summary>
    public partial class LoginForm : Form
    {
        private const string RememberUsernameKey = "RememberUsername";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // 加载记住的用户名
            LoadRememberedUsername();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username))
            {
                ShowMessage("请输入用户名");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                ShowMessage("请输入密码");
                txtPassword.Focus();
                return;
            }

            // 禁用按钮，防止重复点击
            SetButtonsEnabled(false);

            try
            {
                string errorMessage;
                if (AuthenticationService.Instance.Login(username, password, out errorMessage))
                {
                    // 保存用户名
                    if (chkRememberUsername.Checked)
                    {
                        SaveUsername(username);
                    }
                    else
                    {
                        ClearSavedUsername();
                    }

                    // 登录成功，打开主窗体
                    OpenMainForm();
                }
                else
                {
                    ShowMessage(errorMessage);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                ShowMessage("登录失败：" + ex.Message);
            }
            finally
            {
                SetButtonsEnabled(true);
            }
        }

        private void btnWindowsLogin_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            SetButtonsEnabled(false);

            try
            {
                string errorMessage;
                if (AuthenticationService.Instance.LoginWithWindows(out errorMessage))
                {
                    // 登录成功，打开主窗体
                    OpenMainForm();
                }
                else
                {
                    ShowMessage(errorMessage);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Windows 登录失败：" + ex.Message);
            }
            finally
            {
                SetButtonsEnabled(true);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            using (var registerForm = new RegisterForm())
            {
                if (registerForm.ShowDialog() == DialogResult.OK)
                {
                    // 注册成功，填充用户名
                    txtUsername.Text = registerForm.RegisteredUsername;
                    txtPassword.Focus();
                    ShowMessage("注册成功，请登录", false);
                }
            }
        }

        private void OpenMainForm()
        {
            this.Hide();
            using (var mainForm = new MainForm())
            {
                mainForm.ShowDialog();
            }
            
            // 主窗体关闭后，检查是否需要重新登录
            if (AuthenticationService.Instance.IsLoggedIn)
            {
                AuthenticationService.Instance.Logout();
            }
            
            txtPassword.Clear();
            lblMessage.Text = string.Empty;
            this.Show();
        }

        private void ShowMessage(string message, bool isError = true)
        {
            lblMessage.ForeColor = isError ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            lblMessage.Text = message;
        }

        private void SetButtonsEnabled(bool enabled)
        {
            btnLogin.Enabled = enabled;
            btnWindowsLogin.Enabled = enabled;
            btnRegister.Enabled = enabled;
            txtUsername.Enabled = enabled;
            txtPassword.Enabled = enabled;

            if (!enabled)
            {
                Application.DoEvents();
            }
        }

        private void LoadRememberedUsername()
        {
            try
            {
                string savedUsername = Properties.Settings.Default["LastUsername"]?.ToString();
                if (!string.IsNullOrEmpty(savedUsername))
                {
                    txtUsername.Text = savedUsername;
                    chkRememberUsername.Checked = true;
                    txtPassword.Focus();
                }
            }
            catch
            {
                // 忽略设置读取错误
            }
        }

        private void SaveUsername(string username)
        {
            try
            {
                Properties.Settings.Default["LastUsername"] = username;
                Properties.Settings.Default.Save();
            }
            catch
            {
                // 忽略设置保存错误
            }
        }

        private void ClearSavedUsername()
        {
            try
            {
                Properties.Settings.Default["LastUsername"] = string.Empty;
                Properties.Settings.Default.Save();
            }
            catch
            {
                // 忽略设置保存错误
            }
        }
    }
}
