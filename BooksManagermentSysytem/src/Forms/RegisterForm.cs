using System;
using System.Security.Principal;
using System.Windows.Forms;
using BooksManagermentSysytem.Models;
using BooksManagermentSysytem.Services;

namespace BooksManagermentSysytem.Forms
{
    /// <summary>
    /// 注册窗体
    /// </summary>
    public partial class RegisterForm : Form
    {
        /// <summary>
        /// 注册成功后的用户名
        /// </summary>
        public string RegisteredUsername { get; private set; }

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            cboRole.SelectedIndex = 0; // 默认选择读者
            UpdateWindowsAccountLabel();
        }

        private void cboRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 只有读者角色需要填写借书证号
            bool isReader = cboRole.SelectedIndex == 0;
            txtCardID.Enabled = isReader;
            lblCardID.Text = isReader ? "借书证号：*" : "借书证号：";

            if (!isReader)
            {
                txtCardID.Clear();
            }
        }

        private void chkBindWindows_CheckedChanged(object sender, EventArgs e)
        {
            UpdateWindowsAccountLabel();
        }

        private void UpdateWindowsAccountLabel()
        {
            if (chkBindWindows.Checked)
            {
                lblWindowsAccount.Text = WindowsIdentity.GetCurrent().Name;
            }
            else
            {
                lblWindowsAccount.Text = string.Empty;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;

            // 验证输入
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                ShowError("请输入用户名");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                ShowError("请输入密码");
                txtPassword.Focus();
                return;
            }

            if (txtPassword.Text.Length < 6)
            {
                ShowError("密码长度至少为6位");
                txtPassword.Focus();
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                ShowError("两次输入的密码不一致");
                txtConfirmPassword.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDisplayName.Text))
            {
                ShowError("请输入显示名称");
                txtDisplayName.Focus();
                return;
            }

            if (cboRole.SelectedIndex < 0)
            {
                ShowError("请选择用户角色");
                cboRole.Focus();
                return;
            }

            UserRole role = GetSelectedRole();
            string cardID = null;

            // 读者需要验证借书证号
            if (role == UserRole.Reader)
            {
                if (string.IsNullOrWhiteSpace(txtCardID.Text))
                {
                    ShowError("读者必须填写借书证号");
                    txtCardID.Focus();
                    return;
                }
                cardID = txtCardID.Text.Trim();
            }

            string windowsAccount = chkBindWindows.Checked ? WindowsIdentity.GetCurrent().Name : null;

            // 禁用按钮
            btnRegister.Enabled = false;

            try
            {
                string errorMessage;
                bool success = AuthenticationService.Instance.Register(
                    txtUsername.Text.Trim(),
                    txtPassword.Text,
                    txtDisplayName.Text.Trim(),
                    role,
                    cardID,
                    windowsAccount,
                    out errorMessage);

                if (success)
                {
                    RegisteredUsername = txtUsername.Text.Trim();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    ShowError(errorMessage);
                }
            }
            catch (Exception ex)
            {
                ShowError("注册失败：" + ex.Message);
            }
            finally
            {
                btnRegister.Enabled = true;
            }
        }

        private UserRole GetSelectedRole()
        {
            switch (cboRole.SelectedIndex)
            {
                case 0: return UserRole.Reader;
                case 1: return UserRole.Librarian;
                case 2: return UserRole.Cataloger;
                case 3: return UserRole.Admin;
                default: return UserRole.Reader;
            }
        }

        private void ShowError(string message)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = message;
        }
    }
}
