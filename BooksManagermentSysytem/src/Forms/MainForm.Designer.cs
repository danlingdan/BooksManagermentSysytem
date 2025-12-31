namespace BooksManagermentSysytem.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuReader = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReaderQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBorrowBook = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReturnBook = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReservation = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMyFines = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLibrarian = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReaderManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFineManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBorrowStats = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCatalog = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCategoryManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLocationManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuBibliography = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBookItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBookSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAdmin = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCardManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUserManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSystemLog = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUser = new System.Windows.Forms.ToolStripMenuItem();
            this.menuChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBindWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblUserInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblRole = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.timerClock = new System.Windows.Forms.Timer();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.5F);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuReader,
            this.menuLibrarian,
            this.menuCatalog,
            this.menuSearch,
            this.menuAdmin,
            this.menuUser});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1200, 28);
            this.menuStrip.TabIndex = 0;
            // 
            // menuReader
            // 
            this.menuReader.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuReaderQuery,
            this.menuBorrowBook,
            this.menuReturnBook,
            this.menuReservation,
            this.menuMyFines});
            this.menuReader.Name = "menuReader";
            this.menuReader.Size = new System.Drawing.Size(76, 24);
            this.menuReader.Text = "读者服务";
            // 
            // menuReaderQuery
            // 
            this.menuReaderQuery.Name = "menuReaderQuery";
            this.menuReaderQuery.Size = new System.Drawing.Size(148, 24);
            this.menuReaderQuery.Text = "个人信息";
            this.menuReaderQuery.Click += new System.EventHandler(this.menuReaderQuery_Click);
            // 
            // menuBorrowBook
            // 
            this.menuBorrowBook.Name = "menuBorrowBook";
            this.menuBorrowBook.Size = new System.Drawing.Size(148, 24);
            this.menuBorrowBook.Text = "借阅图书";
            this.menuBorrowBook.Click += new System.EventHandler(this.menuBorrowBook_Click);
            // 
            // menuReturnBook
            // 
            this.menuReturnBook.Name = "menuReturnBook";
            this.menuReturnBook.Size = new System.Drawing.Size(148, 24);
            this.menuReturnBook.Text = "归还图书";
            this.menuReturnBook.Click += new System.EventHandler(this.menuReturnBook_Click);
            // 
            // menuReservation
            // 
            this.menuReservation.Name = "menuReservation";
            this.menuReservation.Size = new System.Drawing.Size(148, 24);
            this.menuReservation.Text = "预约图书";
            this.menuReservation.Click += new System.EventHandler(this.menuReservation_Click);
            // 
            // menuMyFines
            // 
            this.menuMyFines.Name = "menuMyFines";
            this.menuMyFines.Size = new System.Drawing.Size(148, 24);
            this.menuMyFines.Text = "我的罚款";
            this.menuMyFines.Click += new System.EventHandler(this.menuMyFines_Click);
            // 
            // menuLibrarian
            // 
            this.menuLibrarian.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuReaderManagement,
            this.menuFineManagement,
            this.menuBorrowStats});
            this.menuLibrarian.Name = "menuLibrarian";
            this.menuLibrarian.Size = new System.Drawing.Size(93, 24);
            this.menuLibrarian.Text = "图书管理员";
            // 
            // menuReaderManagement
            // 
            this.menuReaderManagement.Name = "menuReaderManagement";
            this.menuReaderManagement.Size = new System.Drawing.Size(148, 24);
            this.menuReaderManagement.Text = "读者管理";
            this.menuReaderManagement.Click += new System.EventHandler(this.menuReaderManagement_Click);
            // 
            // menuFineManagement
            // 
            this.menuFineManagement.Name = "menuFineManagement";
            this.menuFineManagement.Size = new System.Drawing.Size(148, 24);
            this.menuFineManagement.Text = "罚款管理";
            this.menuFineManagement.Click += new System.EventHandler(this.menuFineManagement_Click);
            // 
            // menuBorrowStats
            // 
            this.menuBorrowStats.Name = "menuBorrowStats";
            this.menuBorrowStats.Size = new System.Drawing.Size(148, 24);
            this.menuBorrowStats.Text = "借阅统计";
            this.menuBorrowStats.Click += new System.EventHandler(this.menuBorrowStats_Click);
            // 
            // menuCatalog
            // 
            this.menuCatalog.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCategoryManagement,
            this.menuLocationManagement,
            this.toolStripSeparator1,
            this.menuBibliography,
            this.menuBookItem});
            this.menuCatalog.Name = "menuCatalog";
            this.menuCatalog.Size = new System.Drawing.Size(76, 24);
            this.menuCatalog.Text = "编目管理";
            // 
            // menuCategoryManagement
            // 
            this.menuCategoryManagement.Name = "menuCategoryManagement";
            this.menuCategoryManagement.Size = new System.Drawing.Size(148, 24);
            this.menuCategoryManagement.Text = "分类管理";
            this.menuCategoryManagement.Click += new System.EventHandler(this.menuCategoryManagement_Click);
            // 
            // menuLocationManagement
            // 
            this.menuLocationManagement.Name = "menuLocationManagement";
            this.menuLocationManagement.Size = new System.Drawing.Size(148, 24);
            this.menuLocationManagement.Text = "库位管理";
            this.menuLocationManagement.Click += new System.EventHandler(this.menuLocationManagement_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // menuBibliography
            // 
            this.menuBibliography.Name = "menuBibliography";
            this.menuBibliography.Size = new System.Drawing.Size(148, 24);
            this.menuBibliography.Text = "书目管理";
            this.menuBibliography.Click += new System.EventHandler(this.menuBibliography_Click);
            // 
            // menuBookItem
            // 
            this.menuBookItem.Name = "menuBookItem";
            this.menuBookItem.Size = new System.Drawing.Size(148, 24);
            this.menuBookItem.Text = "馆藏管理";
            this.menuBookItem.Click += new System.EventHandler(this.menuBookItem_Click);
            // 
            // menuSearch
            // 
            this.menuSearch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuBookSearch});
            this.menuSearch.Name = "menuSearch";
            this.menuSearch.Size = new System.Drawing.Size(76, 24);
            this.menuSearch.Text = "图书检索";
            // 
            // menuBookSearch
            // 
            this.menuBookSearch.Name = "menuBookSearch";
            this.menuBookSearch.Size = new System.Drawing.Size(148, 24);
            this.menuBookSearch.Text = "图书查询";
            this.menuBookSearch.Click += new System.EventHandler(this.menuBookSearch_Click);
            // 
            // menuAdmin
            // 
            this.menuAdmin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCardManagement,
            this.menuUserManagement,
            this.menuSystemLog});
            this.menuAdmin.Name = "menuAdmin";
            this.menuAdmin.Size = new System.Drawing.Size(76, 24);
            this.menuAdmin.Text = "系统管理";
            // 
            // menuCardManagement
            // 
            this.menuCardManagement.Name = "menuCardManagement";
            this.menuCardManagement.Size = new System.Drawing.Size(148, 24);
            this.menuCardManagement.Text = "借书证管理";
            this.menuCardManagement.Click += new System.EventHandler(this.menuCardManagement_Click);
            // 
            // menuUserManagement
            // 
            this.menuUserManagement.Name = "menuUserManagement";
            this.menuUserManagement.Size = new System.Drawing.Size(148, 24);
            this.menuUserManagement.Text = "用户管理";
            this.menuUserManagement.Click += new System.EventHandler(this.menuUserManagement_Click);
            // 
            // menuSystemLog
            // 
            this.menuSystemLog.Name = "menuSystemLog";
            this.menuSystemLog.Size = new System.Drawing.Size(148, 24);
            this.menuSystemLog.Text = "系统日志";
            this.menuSystemLog.Click += new System.EventHandler(this.menuSystemLog_Click);
            // 
            // menuUser
            // 
            this.menuUser.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.menuUser.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuChangePassword,
            this.menuBindWindows,
            this.toolStripSeparator2,
            this.menuLogout});
            this.menuUser.Name = "menuUser";
            this.menuUser.Size = new System.Drawing.Size(59, 24);
            this.menuUser.Text = "用户 ▼";
            // 
            // menuChangePassword
            // 
            this.menuChangePassword.Name = "menuChangePassword";
            this.menuChangePassword.Size = new System.Drawing.Size(184, 24);
            this.menuChangePassword.Text = "修改密码";
            this.menuChangePassword.Click += new System.EventHandler(this.menuChangePassword_Click);
            // 
            // menuBindWindows
            // 
            this.menuBindWindows.Name = "menuBindWindows";
            this.menuBindWindows.Size = new System.Drawing.Size(184, 24);
            this.menuBindWindows.Text = "绑定 Windows 账户";
            this.menuBindWindows.Click += new System.EventHandler(this.menuBindWindows_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(181, 6);
            // 
            // menuLogout
            // 
            this.menuLogout.Name = "menuLogout";
            this.menuLogout.Size = new System.Drawing.Size(184, 24);
            this.menuLogout.Text = "退出登录";
            this.menuLogout.Click += new System.EventHandler(this.menuLogout_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblUserInfo,
            this.lblRole,
            this.lblTime});
            this.statusStrip.Location = new System.Drawing.Point(0, 678);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1200, 22);
            this.statusStrip.TabIndex = 1;
            // 
            // lblUserInfo
            // 
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Size = new System.Drawing.Size(68, 17);
            this.lblUserInfo.Text = "当前用户：";
            // 
            // lblRole
            // 
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(44, 17);
            this.lblRole.Text = "角色：";
            // 
            // lblTime
            // 
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(1073, 17);
            this.lblTime.Spring = true;
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.White;
            this.panelContent.Controls.Add(this.lblWelcome);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 28);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1200, 650);
            this.panelContent.TabIndex = 2;
            // 
            // lblWelcome
            // 
            this.lblWelcome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft YaHei UI", 24F);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblWelcome.Location = new System.Drawing.Point(0, 0);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(1200, 650);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "欢迎使用图书馆管理系统";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerClock
            // 
            this.timerClock.Enabled = true;
            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图书馆管理系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuReader;
        private System.Windows.Forms.ToolStripMenuItem menuReaderQuery;
        private System.Windows.Forms.ToolStripMenuItem menuBorrowBook;
        private System.Windows.Forms.ToolStripMenuItem menuReturnBook;
        private System.Windows.Forms.ToolStripMenuItem menuReservation;
        private System.Windows.Forms.ToolStripMenuItem menuMyFines;
        private System.Windows.Forms.ToolStripMenuItem menuLibrarian;
        private System.Windows.Forms.ToolStripMenuItem menuReaderManagement;
        private System.Windows.Forms.ToolStripMenuItem menuFineManagement;
        private System.Windows.Forms.ToolStripMenuItem menuBorrowStats;
        private System.Windows.Forms.ToolStripMenuItem menuCatalog;
        private System.Windows.Forms.ToolStripMenuItem menuCategoryManagement;
        private System.Windows.Forms.ToolStripMenuItem menuLocationManagement;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuBibliography;
        private System.Windows.Forms.ToolStripMenuItem menuBookItem;
        private System.Windows.Forms.ToolStripMenuItem menuSearch;
        private System.Windows.Forms.ToolStripMenuItem menuBookSearch;
        private System.Windows.Forms.ToolStripMenuItem menuAdmin;
        private System.Windows.Forms.ToolStripMenuItem menuCardManagement;
        private System.Windows.Forms.ToolStripMenuItem menuUserManagement;
        private System.Windows.Forms.ToolStripMenuItem menuSystemLog;
        private System.Windows.Forms.ToolStripMenuItem menuUser;
        private System.Windows.Forms.ToolStripMenuItem menuChangePassword;
        private System.Windows.Forms.ToolStripMenuItem menuBindWindows;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuLogout;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblUserInfo;
        private System.Windows.Forms.ToolStripStatusLabel lblRole;
        private System.Windows.Forms.ToolStripStatusLabel lblTime;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Timer timerClock;
    }
}
