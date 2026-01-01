using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Models;
using BooksManagermentSysytem.Services;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 用户管理控件 - 系统管理员管理用户账户
    /// </summary>
    public partial class UserManagementControl : UserControl
    {
        private int currentUserId;
        private bool isNewMode;

        public UserManagementControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panelSearch = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblRoleFilter = new System.Windows.Forms.Label();
            this.cboRoleFilter = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblDisplayName = new System.Windows.Forms.Label();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.cboRole = new System.Windows.Forms.ComboBox();
            this.lblCardID = new System.Windows.Forms.Label();
            this.txtCardID = new System.Windows.Forms.TextBox();
            this.btnSelectCard = new System.Windows.Forms.Button();
            this.lblCardInfo = new System.Windows.Forms.Label();
            this.lblWindowsAccount = new System.Windows.Forms.Label();
            this.txtWindowsAccount = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPasswordNote = new System.Windows.Forms.Label();
            this.lblIsActive = new System.Windows.Forms.Label();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.lblCreatedTime = new System.Windows.Forms.Label();
            this.lblCreatedTimeValue = new System.Windows.Forms.Label();
            this.lblLastLogin = new System.Windows.Forms.Label();
            this.lblLastLoginValue = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnResetPassword = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.panelDetails.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelSearch.Controls.Add(this.btnNew);
            this.panelSearch.Controls.Add(this.btnSearch);
            this.panelSearch.Controls.Add(this.cboRoleFilter);
            this.panelSearch.Controls.Add(this.lblRoleFilter);
            this.panelSearch.Controls.Add(this.txtSearch);
            this.panelSearch.Controls.Add(this.lblSearch);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Size = new System.Drawing.Size(950, 45);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(15, 13);
            this.lblSearch.Text = "搜索用户：";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(85, 10);
            this.txtSearch.Size = new System.Drawing.Size(180, 23);
            // 
            // lblRoleFilter
            // 
            this.lblRoleFilter.AutoSize = true;
            this.lblRoleFilter.Location = new System.Drawing.Point(280, 13);
            this.lblRoleFilter.Text = "角色：";
            // 
            // cboRoleFilter
            // 
            this.cboRoleFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRoleFilter.Location = new System.Drawing.Point(320, 10);
            this.cboRoleFilter.Size = new System.Drawing.Size(120, 25);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(455, 8);
            this.btnSearch.Size = new System.Drawing.Size(70, 28);
            this.btnSearch.Text = "搜索";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(540, 8);
            this.btnNew.Size = new System.Drawing.Size(90, 28);
            this.btnNew.Text = "新建用户";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 45);
            this.splitContainer.Size = new System.Drawing.Size(950, 505);
            this.splitContainer.SplitterDistance = 500;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dgvUsers);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panelDetails);
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsers.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowHeadersVisible = false;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.SelectionChanged += new System.EventHandler(this.dgvUsers_SelectionChanged);
            // 
            // panelDetails
            // 
            this.panelDetails.Controls.Add(this.panelButtons);
            this.panelDetails.Controls.Add(this.lblLastLoginValue);
            this.panelDetails.Controls.Add(this.lblLastLogin);
            this.panelDetails.Controls.Add(this.lblCreatedTimeValue);
            this.panelDetails.Controls.Add(this.lblCreatedTime);
            this.panelDetails.Controls.Add(this.chkIsActive);
            this.panelDetails.Controls.Add(this.lblIsActive);
            this.panelDetails.Controls.Add(this.lblPasswordNote);
            this.panelDetails.Controls.Add(this.txtPassword);
            this.panelDetails.Controls.Add(this.lblPassword);
            this.panelDetails.Controls.Add(this.txtWindowsAccount);
            this.panelDetails.Controls.Add(this.lblWindowsAccount);
            this.panelDetails.Controls.Add(this.lblCardInfo);
            this.panelDetails.Controls.Add(this.btnSelectCard);
            this.panelDetails.Controls.Add(this.txtCardID);
            this.panelDetails.Controls.Add(this.lblCardID);
            this.panelDetails.Controls.Add(this.cboRole);
            this.panelDetails.Controls.Add(this.lblRole);
            this.panelDetails.Controls.Add(this.txtDisplayName);
            this.panelDetails.Controls.Add(this.lblDisplayName);
            this.panelDetails.Controls.Add(this.txtUsername);
            this.panelDetails.Controls.Add(this.lblUsername);
            this.panelDetails.Controls.Add(this.lblTitle);
            this.panelDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTitle.Text = "用户详情";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(10, 45);
            this.lblUsername.Text = "用户名：";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(100, 42);
            this.txtUsername.Size = new System.Drawing.Size(150, 23);
            // 
            // lblDisplayName
            // 
            this.lblDisplayName.AutoSize = true;
            this.lblDisplayName.Location = new System.Drawing.Point(10, 80);
            this.lblDisplayName.Text = "显示名称：";
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Location = new System.Drawing.Point(100, 77);
            this.txtDisplayName.Size = new System.Drawing.Size(200, 23);
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(10, 115);
            this.lblRole.Text = "用户角色：";
            // 
            // cboRole
            // 
            this.cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRole.Location = new System.Drawing.Point(100, 112);
            this.cboRole.Size = new System.Drawing.Size(150, 25);
            this.cboRole.SelectedIndexChanged += new System.EventHandler(this.cboRole_SelectedIndexChanged);
            // 
            // lblCardID
            // 
            this.lblCardID.AutoSize = true;
            this.lblCardID.Location = new System.Drawing.Point(10, 150);
            this.lblCardID.Text = "借书证号：";
            // 
            // txtCardID
            // 
            this.txtCardID.Location = new System.Drawing.Point(100, 147);
            this.txtCardID.Size = new System.Drawing.Size(150, 23);
            // 
            // btnSelectCard
            // 
            this.btnSelectCard.Location = new System.Drawing.Point(260, 145);
            this.btnSelectCard.Size = new System.Drawing.Size(70, 28);
            this.btnSelectCard.Text = "选择...";
            this.btnSelectCard.Click += new System.EventHandler(this.btnSelectCard_Click);
            // 
            // lblCardInfo
            // 
            this.lblCardInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblCardInfo.Location = new System.Drawing.Point(100, 175);
            this.lblCardInfo.Size = new System.Drawing.Size(330, 20);
            this.lblCardInfo.Text = "（读者角色需绑定借书证）";
            // 
            // lblWindowsAccount
            // 
            this.lblWindowsAccount.AutoSize = true;
            this.lblWindowsAccount.Location = new System.Drawing.Point(10, 205);
            this.lblWindowsAccount.Text = "Windows账户：";
            // 
            // txtWindowsAccount
            // 
            this.txtWindowsAccount.Location = new System.Drawing.Point(100, 202);
            this.txtWindowsAccount.Size = new System.Drawing.Size(300, 23);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(10, 240);
            this.lblPassword.Text = "密码：";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(100, 237);
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(200, 23);
            // 
            // lblPasswordNote
            // 
            this.lblPasswordNote.ForeColor = System.Drawing.Color.Gray;
            this.lblPasswordNote.Location = new System.Drawing.Point(100, 265);
            this.lblPasswordNote.Size = new System.Drawing.Size(330, 20);
            this.lblPasswordNote.Text = "（新建时必填，编辑时留空表示不修改）";
            // 
            // lblIsActive
            // 
            this.lblIsActive.AutoSize = true;
            this.lblIsActive.Location = new System.Drawing.Point(10, 295);
            this.lblIsActive.Text = "账户状态：";
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Location = new System.Drawing.Point(100, 295);
            this.chkIsActive.Text = "启用";
            // 
            // lblCreatedTime
            // 
            this.lblCreatedTime.AutoSize = true;
            this.lblCreatedTime.Location = new System.Drawing.Point(10, 330);
            this.lblCreatedTime.Text = "创建时间：";
            // 
            // lblCreatedTimeValue
            // 
            this.lblCreatedTimeValue.AutoSize = true;
            this.lblCreatedTimeValue.Location = new System.Drawing.Point(100, 330);
            this.lblCreatedTimeValue.Text = "-";
            // 
            // lblLastLogin
            // 
            this.lblLastLogin.AutoSize = true;
            this.lblLastLogin.Location = new System.Drawing.Point(10, 355);
            this.lblLastLogin.Text = "最后登录：";
            // 
            // lblLastLoginValue
            // 
            this.lblLastLoginValue.AutoSize = true;
            this.lblLastLoginValue.Location = new System.Drawing.Point(100, 355);
            this.lblLastLoginValue.Text = "-";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Controls.Add(this.btnDelete);
            this.panelButtons.Controls.Add(this.btnResetPassword);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Location = new System.Drawing.Point(10, 390);
            this.panelButtons.Size = new System.Drawing.Size(430, 40);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(0, 5);
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnResetPassword
            // 
            this.btnResetPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnResetPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetPassword.ForeColor = System.Drawing.Color.White;
            this.btnResetPassword.Location = new System.Drawing.Point(100, 5);
            this.btnResetPassword.Size = new System.Drawing.Size(100, 30);
            this.btnResetPassword.Text = "重置密码";
            this.btnResetPassword.Click += new System.EventHandler(this.btnResetPassword_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(210, 5);
            this.btnDelete.Size = new System.Drawing.Size(90, 30);
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(310, 5);
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // UserManagementControl
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelSearch);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Size = new System.Drawing.Size(950, 550);
            this.Load += new System.EventHandler(this.UserManagementControl_Load);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.panelDetails.ResumeLayout(false);
            this.panelDetails.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblRoleFilter;
        private System.Windows.Forms.ComboBox cboRoleFilter;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblDisplayName;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.ComboBox cboRole;
        private System.Windows.Forms.Label lblCardID;
        private System.Windows.Forms.TextBox txtCardID;
        private System.Windows.Forms.Button btnSelectCard;
        private System.Windows.Forms.Label lblCardInfo;
        private System.Windows.Forms.Label lblWindowsAccount;
        private System.Windows.Forms.TextBox txtWindowsAccount;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPasswordNote;
        private System.Windows.Forms.Label lblIsActive;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Label lblCreatedTime;
        private System.Windows.Forms.Label lblCreatedTimeValue;
        private System.Windows.Forms.Label lblLastLogin;
        private System.Windows.Forms.Label lblLastLoginValue;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnResetPassword;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;

        private void UserManagementControl_Load(object sender, EventArgs e)
        {
            LoadRoleFilters();
            LoadRoles();
            LoadUsers();
        }

        private void LoadRoleFilters()
        {
            cboRoleFilter.Items.Clear();
            cboRoleFilter.Items.Add(new RoleItem { Value = "", Text = "全部角色" });
            cboRoleFilter.Items.Add(new RoleItem { Value = "Reader", Text = "读者" });
            cboRoleFilter.Items.Add(new RoleItem { Value = "Librarian", Text = "图书管理员" });
            cboRoleFilter.Items.Add(new RoleItem { Value = "Cataloger", Text = "图书采编员" });
            cboRoleFilter.Items.Add(new RoleItem { Value = "Admin", Text = "系统管理员" });
            cboRoleFilter.SelectedIndex = 0;
        }

        private void LoadRoles()
        {
            cboRole.Items.Clear();
            cboRole.Items.Add(new RoleItem { Value = "Reader", Text = "读者" });
            cboRole.Items.Add(new RoleItem { Value = "Librarian", Text = "图书管理员" });
            cboRole.Items.Add(new RoleItem { Value = "Cataloger", Text = "图书采编员" });
            cboRole.Items.Add(new RoleItem { Value = "Admin", Text = "系统管理员" });
            cboRole.SelectedIndex = 0;
        }

        private void LoadUsers()
        {
            try
            {
                string sql = @"
                    SELECT user_id AS ID, username AS 用户名, display_name AS 显示名称,
                           user_role AS 角色, cardID AS 借书证号, 
                           CASE WHEN is_active = 1 THEN N'启用' ELSE N'禁用' END AS 状态,
                           last_login_time AS 最后登录
                    FROM [system_user]
                    WHERE 1=1";

                var parameters = new System.Collections.Generic.List<System.Data.SqlClient.SqlParameter>();

                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    sql += " AND (username LIKE @kw OR display_name LIKE @kw)";
                    parameters.Add(DatabaseHelper.CreateParameter("@kw", "%" + txtSearch.Text.Trim() + "%"));
                }

                if (cboRoleFilter.SelectedItem != null)
                {
                    string role = ((RoleItem)cboRoleFilter.SelectedItem).Value;
                    if (!string.IsNullOrEmpty(role))
                    {
                        sql += " AND user_role = @role";
                        parameters.Add(DatabaseHelper.CreateParameter("@role", role));
                    }
                }

                sql += " ORDER BY user_id";

                dgvUsers.DataSource = DatabaseHelper.ExecuteQuery(sql, parameters.ToArray());

                if (dgvUsers.Columns.Contains("ID"))
                {
                    dgvUsers.Columns["ID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载用户列表失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            isNewMode = true;
            currentUserId = 0;
            ClearForm();
            txtUsername.Enabled = true;
            txtUsername.Focus();
        }

        private void ClearForm()
        {
            txtUsername.Clear();
            txtDisplayName.Clear();
            cboRole.SelectedIndex = 0;
            txtCardID.Clear();
            lblCardInfo.Text = "（读者角色需绑定借书证）";
            txtWindowsAccount.Clear();
            txtPassword.Clear();
            chkIsActive.Checked = true;
            lblCreatedTimeValue.Text = "-";
            lblLastLoginValue.Text = "-";
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0) return;

            var idCell = dgvUsers.SelectedRows[0].Cells["ID"];
            if (idCell?.Value == null) return;

            currentUserId = Convert.ToInt32(idCell.Value);
            isNewMode = false;
            LoadUserDetails(currentUserId);
        }

        private void LoadUserDetails(int userId)
        {
            try
            {
                string sql = "SELECT * FROM [system_user] WHERE user_id = @id";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.CreateParameter("@id", userId));

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    txtUsername.Text = row["username"].ToString();
                    txtUsername.Enabled = false;
                    txtDisplayName.Text = row["display_name"].ToString();

                    string role = row["user_role"].ToString();
                    for (int i = 0; i < cboRole.Items.Count; i++)
                    {
                        if (((RoleItem)cboRole.Items[i]).Value == role)
                        {
                            cboRole.SelectedIndex = i;
                            break;
                        }
                    }

                    txtCardID.Text = row["cardID"] == DBNull.Value ? "" : row["cardID"].ToString();
                    UpdateCardInfo();

                    txtWindowsAccount.Text = row["windows_account"] == DBNull.Value ? "" : row["windows_account"].ToString();
                    txtPassword.Clear();
                    chkIsActive.Checked = Convert.ToBoolean(row["is_active"]);

                    lblCreatedTimeValue.Text = Convert.ToDateTime(row["created_time"]).ToString("yyyy-MM-dd HH:mm");
                    lblLastLoginValue.Text = row["last_login_time"] == DBNull.Value ? "从未登录" : 
                        Convert.ToDateTime(row["last_login_time"]).ToString("yyyy-MM-dd HH:mm");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载用户详情失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isReader = ((RoleItem)cboRole.SelectedItem).Value == "Reader";
            lblCardID.Visible = isReader;
            txtCardID.Visible = isReader;
            btnSelectCard.Visible = isReader;
            lblCardInfo.Visible = isReader;
        }

        private void btnSelectCard_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = @"
                    SELECT r.cardID, r.readername, r.readertype, rc.state
                    FROM reader r
                    INNER JOIN readcard rc ON r.cardID = rc.cardID
                    WHERE rc.state = N'正常' AND rc.overdate >= GETDATE()
                    AND NOT EXISTS (SELECT 1 FROM [system_user] WHERE cardID = r.cardID" +
                    (isNewMode ? "" : " AND user_id <> @userId") + @")
                    ORDER BY r.cardID";

                var parameters = new System.Collections.Generic.List<System.Data.SqlClient.SqlParameter>();
                if (!isNewMode)
                {
                    parameters.Add(DatabaseHelper.CreateParameter("@userId", currentUserId));
                }

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters.ToArray());

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("没有可用的借书证（需要正常状态且未绑定）", "提示", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var selectForm = new Form())
                {
                    selectForm.Text = "选择借书证";
                    selectForm.Size = new System.Drawing.Size(600, 400);
                    selectForm.StartPosition = FormStartPosition.CenterParent;

                    var dgv = new DataGridView
                    {
                        Dock = DockStyle.Fill,
                        DataSource = dt,
                        ReadOnly = true,
                        AllowUserToAddRows = false,
                        AllowUserToDeleteRows = false,
                        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                    };

                    var panel = new Panel { Dock = DockStyle.Bottom, Height = 40 };
                    var btnOK = new Button
                    {
                        Text = "确定",
                        DialogResult = DialogResult.OK,
                        Location = new System.Drawing.Point(200, 5),
                        Size = new System.Drawing.Size(80, 30)
                    };
                    var btnCancelSel = new Button
                    {
                        Text = "取消",
                        DialogResult = DialogResult.Cancel,
                        Location = new System.Drawing.Point(300, 5),
                        Size = new System.Drawing.Size(80, 30)
                    };

                    panel.Controls.Add(btnOK);
                    panel.Controls.Add(btnCancelSel);
                    selectForm.Controls.Add(dgv);
                    selectForm.Controls.Add(panel);
                    selectForm.AcceptButton = btnOK;
                    selectForm.CancelButton = btnCancelSel;

                    if (selectForm.ShowDialog() == DialogResult.OK && dgv.SelectedRows.Count > 0)
                    {
                        txtCardID.Text = dgv.SelectedRows[0].Cells["cardID"].Value.ToString();
                        UpdateCardInfo();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("选择借书证失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCardInfo()
        {
            if (string.IsNullOrWhiteSpace(txtCardID.Text))
            {
                lblCardInfo.Text = "（读者角色需绑定借书证）";
                return;
            }

            try
            {
                string sql = "SELECT readername, readertype FROM reader WHERE cardID = @cardID";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.CreateParameter("@cardID", txtCardID.Text));

                if (dt.Rows.Count > 0)
                {
                    lblCardInfo.Text = $"读者：{dt.Rows[0]["readername"]} ({dt.Rows[0]["readertype"]})";
                }
            }
            catch
            {
                lblCardInfo.Text = "（未找到该借书证）";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtDisplayName.Text))
            {
                MessageBox.Show("请填写用户名和显示名称", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string role = ((RoleItem)cboRole.SelectedItem).Value;
            if (role == "Reader" && string.IsNullOrWhiteSpace(txtCardID.Text))
            {
                MessageBox.Show("读者角色必须绑定借书证", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (isNewMode && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("新建用户时密码不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (isNewMode)
                {
                    string checkSql = "SELECT COUNT(*) FROM [system_user] WHERE username = @username";
                    int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(checkSql,
                        DatabaseHelper.CreateParameter("@username", txtUsername.Text.Trim())));

                    if (count > 0)
                    {
                        MessageBox.Show("用户名已存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string errorMessage;
                    if (!AuthenticationService.Instance.Register(
                        txtUsername.Text.Trim(),
                        txtPassword.Text,
                        txtDisplayName.Text.Trim(),
                        ParseRole(role),
                        string.IsNullOrWhiteSpace(txtCardID.Text) ? null : txtCardID.Text.Trim(),
                        string.IsNullOrWhiteSpace(txtWindowsAccount.Text) ? null : txtWindowsAccount.Text.Trim(),
                        out errorMessage))
                    {
                        MessageBox.Show(errorMessage, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    string sql = @"UPDATE [system_user] SET 
                                  display_name = @displayName, user_role = @role, 
                                  cardID = @cardID, windows_account = @windowsAccount, 
                                  is_active = @isActive
                                  WHERE user_id = @userId";

                    DatabaseHelper.ExecuteNonQuery(sql,
                        DatabaseHelper.CreateParameter("@displayName", txtDisplayName.Text.Trim()),
                        DatabaseHelper.CreateParameter("@role", role),
                        DatabaseHelper.CreateParameter("@cardID", 
                            string.IsNullOrWhiteSpace(txtCardID.Text) ? (object)DBNull.Value : txtCardID.Text.Trim()),
                        DatabaseHelper.CreateParameter("@windowsAccount",
                            string.IsNullOrWhiteSpace(txtWindowsAccount.Text) ? (object)DBNull.Value : txtWindowsAccount.Text.Trim()),
                        DatabaseHelper.CreateParameter("@isActive", chkIsActive.Checked),
                        DatabaseHelper.CreateParameter("@userId", currentUserId));

                    if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                    {
                        string salt = GenerateSalt();
                        string passwordHash = ComputeHash(txtPassword.Text, salt);

                        string pwdSql = "UPDATE [system_user] SET password_hash = @hash, salt = @salt WHERE user_id = @userId";
                        DatabaseHelper.ExecuteNonQuery(pwdSql,
                            DatabaseHelper.CreateParameter("@hash", passwordHash),
                            DatabaseHelper.CreateParameter("@salt", salt),
                            DatabaseHelper.CreateParameter("@userId", currentUserId));
                    }
                }

                MessageBox.Show("保存成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsers();
                isNewMode = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (isNewMode || currentUserId == 0)
            {
                MessageBox.Show("请先选择要重置密码的用户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newPassword = "123456";
            if (MessageBox.Show($"确定将用户【{txtUsername.Text}】的密码重置为：{newPassword}？",
                "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                string salt = GenerateSalt();
                string passwordHash = ComputeHash(newPassword, salt);

                string sql = "UPDATE [system_user] SET password_hash = @hash, salt = @salt WHERE user_id = @userId";
                DatabaseHelper.ExecuteNonQuery(sql,
                    DatabaseHelper.CreateParameter("@hash", passwordHash),
                    DatabaseHelper.CreateParameter("@salt", salt),
                    DatabaseHelper.CreateParameter("@userId", currentUserId));

                MessageBox.Show("密码已重置为：" + newPassword, "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("重置密码失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (isNewMode || currentUserId == 0)
            {
                MessageBox.Show("请先选择要删除的用户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var currentUser = AuthenticationService.Instance.CurrentUser;
            if (currentUser != null && currentUser.UserId == currentUserId)
            {
                MessageBox.Show("不能删除当前登录用户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"确定删除用户【{txtUsername.Text}】？此操作不可恢复。",
                "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                string sql = "DELETE FROM [system_user] WHERE user_id = @userId";
                DatabaseHelper.ExecuteNonQuery(sql, DatabaseHelper.CreateParameter("@userId", currentUserId));

                MessageBox.Show("删除成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCancel_Click(sender, e);
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            isNewMode = false;
            currentUserId = 0;
            ClearForm();
            txtUsername.Enabled = true;
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

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private string ComputeHash(string password, string salt)
        {
            string combined = password + salt;
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(combined));
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private class RoleItem
        {
            public string Value { get; set; }
            public string Text { get; set; }
            public override string ToString() { return Text; }
        }
    }
}
