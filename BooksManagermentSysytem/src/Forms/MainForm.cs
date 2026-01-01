using System;
using System.Windows.Forms;
using BooksManagermentSysytem.Models;
using BooksManagermentSysytem.Services;

namespace BooksManagermentSysytem.Forms
{
    /// <summary>
    /// 主窗体 - 根据用户角色显示不同的功能菜单
    /// </summary>
    public partial class MainForm : Form
    {
        private UserControl currentControl;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 设置用户信息
            UpdateUserInfo();
            
            // 根据角色设置菜单可见性
            SetupMenuByRole();
            
            // 更新时间
            UpdateTime();
        }

        private void UpdateUserInfo()
        {
            var user = AuthenticationService.Instance.CurrentUser;
            if (user != null)
            {
                lblUserInfo.Text = $"当前用户：{user.DisplayName}";
                lblRole.Text = $"角色：{GetRoleDisplayName(user.Role)}";
                menuUser.Text = $"{user.DisplayName} ▼";
            }
        }

        private string GetRoleDisplayName(UserRole role)
        {
            switch (role)
            {
                case UserRole.Reader: return "读者";
                case UserRole.Librarian: return "图书管理员";
                case UserRole.Cataloger: return "图书采编员";
                case UserRole.Admin: return "系统管理员";
                default: return "未知";
            }
        }

        private void SetupMenuByRole()
        {
            var user = AuthenticationService.Instance.CurrentUser;
            if (user == null) return;

            // 读者服务菜单 - 所有用户可见
            menuReader.Visible = true;

            // 图书管理员菜单 - 管理员和图书管理员可见
            menuLibrarian.Visible = user.IsLibrarian;

            // 编目管理菜单 - 管理员和采编员可见
            menuCatalog.Visible = user.IsCataloger;

            // 图书检索 - 所有用户可见
            menuSearch.Visible = true;

            // 系统管理 - 仅管理员可见
            menuAdmin.Visible = user.IsAdmin;

            // 如果是纯读者，调整欢迎信息
            if (user.IsReader && !user.IsAdmin)
            {
                lblWelcome.Text = $"欢迎，{user.DisplayName}！\n\n您可以使用图书检索查找图书，\n或通过读者服务菜单进行借阅操作。";
            }
        }

        private void UpdateTime()
        {
            lblTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss dddd");
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            UpdateTime();
        }

        #region 菜单事件处理

        // 读者服务菜单
        private void menuReaderQuery_Click(object sender, EventArgs e)
        {
            ShowContent("个人信息", "PersonalInfo");
        }

        private void menuBorrowBook_Click(object sender, EventArgs e)
        {
            ShowContent("借阅图书", "BorrowBook");
        }

        private void menuReturnBook_Click(object sender, EventArgs e)
        {
            ShowContent("归还图书", "ReturnBook");
        }

        private void menuReservation_Click(object sender, EventArgs e)
        {
            ShowContent("预约图书", "Reservation");
        }

        private void menuMyFines_Click(object sender, EventArgs e)
        {
            ShowContent("我的罚款", "MyFines");
        }

        // 图书管理员菜单
        private void menuReaderManagement_Click(object sender, EventArgs e)
        {
            ShowContent("读者管理", "ReaderManagement");
        }

        private void menuFineManagement_Click(object sender, EventArgs e)
        {
            ShowContent("罚款管理", "FineManagement");
        }

        private void menuBorrowStats_Click(object sender, EventArgs e)
        {
            ShowContent("借阅统计", "BorrowStats");
        }

        // 编目管理菜单
        private void menuCategoryManagement_Click(object sender, EventArgs e)
        {
            ShowContent("分类管理", "CategoryManagement");
        }

        private void menuLocationManagement_Click(object sender, EventArgs e)
        {
            ShowContent("库位管理", "LocationManagement");
        }

        private void menuBibliography_Click(object sender, EventArgs e)
        {
            ShowContent("书目管理", "Bibliography");
        }

        private void menuBookItem_Click(object sender, EventArgs e)
        {
            ShowContent("馆藏管理", "BookItem");
        }

        // 图书检索菜单
        private void menuBookSearch_Click(object sender, EventArgs e)
        {
            ShowContent("图书查询", "BookSearch");
        }

        // 系统管理菜单
        private void menuCardManagement_Click(object sender, EventArgs e)
        {
            ShowContent("借书证管理", "CardManagement");
        }

        private void menuUserManagement_Click(object sender, EventArgs e)
        {
            ShowContent("用户管理", "UserManagement");
        }

        private void menuSystemLog_Click(object sender, EventArgs e)
        {
            ShowContent("系统日志", "SystemLog");
        }

        // 用户菜单
        private void menuChangePassword_Click(object sender, EventArgs e)
        {
            using (var form = new ChangePasswordForm())
            {
                form.ShowDialog(this);
            }
        }

        private void menuBindWindows_Click(object sender, EventArgs e)
        {
            string errorMessage;
            if (AuthenticationService.Instance.BindWindowsAccount(out errorMessage))
            {
                MessageBox.Show("Windows 账户绑定成功！下次可以使用 Windows 账户直接登录。", 
                    "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(errorMessage, "绑定失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void menuLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出登录吗？", "确认", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        #endregion

        private void ShowContent(string title, string controlType)
        {
            // 清除当前内容
            if (currentControl != null)
            {
                panelContent.Controls.Remove(currentControl);
                currentControl.Dispose();
                currentControl = null;
            }

            lblWelcome.Visible = false;

            // 根据类型创建对应的用户控件
            UserControl control = CreateControl(controlType);
            
            if (control != null)
            {
                control.Dock = DockStyle.Fill;
                panelContent.Controls.Add(control);
                currentControl = control;
            }
            else
            {
                // 如果控件尚未实现，显示提示
                lblWelcome.Text = $"功能模块：{title}\n\n该功能正在开发中...";
                lblWelcome.Visible = true;
            }

            this.Text = $"图书馆管理系统 - {title}";
        }

        private UserControl CreateControl(string controlType)
        {
            switch (controlType)
            {
                case "PersonalInfo":
                    return new Controls.PersonalInfoControl();
                case "MyFines":
                    return new Controls.MyFinesControl();
                case "BookSearch":
                    return new Controls.BookSearchControl();
                case "BorrowBook":
                    return new Controls.BorrowBookControl();
                case "ReturnBook":
                    return new Controls.ReturnBookControl();
                case "Reservation":
                    return new Controls.ReservationControl();
                case "ReaderManagement":
                    return new Controls.ReaderManagementControl();
                case "FineManagement":
                    return new Controls.FineManagementControl();
                case "BorrowStats":
                    return new Controls.LibrarianDashboardControl();
                case "CategoryManagement":
                    return new Controls.CategoryManagementControl();
                case "LocationManagement":
                    return new Controls.LocationManagementControl();
                case "Bibliography":
                    return new Controls.BibliographyControl();
                case "BookItem":
                    return new Controls.BookItemControl();
                case "CardManagement":
                    return new Controls.CardManagementControl();
                case "UserManagement":
                    return new Controls.UserManagementControl();
                case "SystemLog":
                    return new Controls.SystemLogControl();
                default:
                    return null;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 清理资源
            if (currentControl != null)
            {
                currentControl.Dispose();
            }
        }
    }
}
