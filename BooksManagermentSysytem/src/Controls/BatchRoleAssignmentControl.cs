using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Models;
using BooksManagermentSysytem.Services;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 批量角色分配控件
    /// </summary>
    public partial class BatchRoleAssignmentControl : UserControl
    {
        public BatchRoleAssignmentControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panelTop = new Panel();
            this.lblTitle = new Label();
            this.lblTargetRole = new Label();
            this.cboTargetRole = new ComboBox();
            this.btnAssign = new Button();
            this.splitContainer = new SplitContainer();
            this.panelLeft = new Panel();
            this.dgvAvailableUsers = new DataGridView();
            this.lblAvailableUsers = new Label();
            this.panelLeftSearch = new Panel();
            this.btnSearch = new Button();
            this.cboCurrentRole = new ComboBox();
            this.lblCurrentRole = new Label();
            this.txtSearch = new TextBox();
            this.lblSearch = new Label();
            this.panelMiddle = new Panel();
            this.btnRemoveAll = new Button();
            this.btnRemove = new Button();
            this.btnAdd = new Button();
            this.btnAddAll = new Button();
            this.panelRight = new Panel();
            this.dgvSelectedUsers = new DataGridView();
            this.lblSelectedUsers = new Label();
            this.panelBottom = new Panel();
            this.lblStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedUsers)).BeginInit();
            this.panelTop.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelLeftSearch.SuspendLayout();
            this.panelMiddle.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            
            this.panelTop.BackColor = Color.FromArgb(245, 245, 245);
            this.panelTop.Controls.Add(this.btnAssign);
            this.panelTop.Controls.Add(this.cboTargetRole);
            this.panelTop.Controls.Add(this.lblTargetRole);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Location = new Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new Size(1400, 100);
            this.panelTop.TabIndex = 0;
            
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold);
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(154, 27);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "批量角色分配";
            
            this.lblTargetRole.AutoSize = true;
            this.lblTargetRole.Location = new Point(20, 63);
            this.lblTargetRole.Name = "lblTargetRole";
            this.lblTargetRole.Size = new Size(100, 24);
            this.lblTargetRole.TabIndex = 1;
            this.lblTargetRole.Text = "目标角色：";
            
            this.cboTargetRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboTargetRole.Location = new Point(120, 60);
            this.cboTargetRole.Name = "cboTargetRole";
            this.cboTargetRole.Size = new Size(200, 32);
            this.cboTargetRole.TabIndex = 2;
            
            this.btnAssign.BackColor = Color.FromArgb(76, 175, 80);
            this.btnAssign.FlatStyle = FlatStyle.Flat;
            this.btnAssign.ForeColor = Color.White;
            this.btnAssign.Location = new Point(350, 55);
            this.btnAssign.Name = "btnAssign";
            this.btnAssign.Size = new Size(150, 40);
            this.btnAssign.TabIndex = 3;
            this.btnAssign.Text = "批量分配角色";
            this.btnAssign.UseVisualStyleBackColor = false;
            this.btnAssign.Click += new EventHandler(this.btnAssign_Click);
            
            this.splitContainer.Dock = DockStyle.Fill;
            this.splitContainer.Location = new Point(0, 100);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Panel1.Controls.Add(this.panelLeft);
            this.splitContainer.Panel2.Controls.Add(this.panelRight);
            this.splitContainer.Size = new Size(1400, 580);
            this.splitContainer.SplitterDistance = 550;
            this.splitContainer.TabIndex = 1;
            
            this.panelLeft.Controls.Add(this.dgvAvailableUsers);
            this.panelLeft.Controls.Add(this.lblAvailableUsers);
            this.panelLeft.Controls.Add(this.panelLeftSearch);
            this.panelLeft.Dock = DockStyle.Fill;
            this.panelLeft.Location = new Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new Padding(10);
            this.panelLeft.Size = new Size(550, 580);
            this.panelLeft.TabIndex = 0;
            
            this.panelLeftSearch.Controls.Add(this.btnSearch);
            this.panelLeftSearch.Controls.Add(this.cboCurrentRole);
            this.panelLeftSearch.Controls.Add(this.lblCurrentRole);
            this.panelLeftSearch.Controls.Add(this.txtSearch);
            this.panelLeftSearch.Controls.Add(this.lblSearch);
            this.panelLeftSearch.Dock = DockStyle.Top;
            this.panelLeftSearch.Location = new Point(10, 10);
            this.panelLeftSearch.Name = "panelLeftSearch";
            this.panelLeftSearch.Size = new Size(530, 80);
            this.panelLeftSearch.TabIndex = 0;
            
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new Point(5, 10);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new Size(64, 24);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "搜索：";
            
            this.txtSearch.Location = new Point(75, 7);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new Size(250, 30);
            this.txtSearch.TabIndex = 1;
            
            this.lblCurrentRole.AutoSize = true;
            this.lblCurrentRole.Location = new Point(5, 50);
            this.lblCurrentRole.Name = "lblCurrentRole";
            this.lblCurrentRole.Size = new Size(64, 24);
            this.lblCurrentRole.TabIndex = 2;
            this.lblCurrentRole.Text = "角色：";
            
            this.cboCurrentRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboCurrentRole.Location = new Point(75, 47);
            this.cboCurrentRole.Name = "cboCurrentRole";
            this.cboCurrentRole.Size = new Size(180, 32);
            this.cboCurrentRole.TabIndex = 3;
            
            this.btnSearch.BackColor = Color.FromArgb(0, 122, 204);
            this.btnSearch.FlatStyle = FlatStyle.Flat;
            this.btnSearch.ForeColor = Color.White;
            this.btnSearch.Location = new Point(270, 45);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new Size(100, 35);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "搜索";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new EventHandler(this.btnSearch_Click);
            
            this.lblAvailableUsers.AutoSize = true;
            this.lblAvailableUsers.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            this.lblAvailableUsers.Location = new Point(10, 90);
            this.lblAvailableUsers.Name = "lblAvailableUsers";
            this.lblAvailableUsers.Size = new Size(92, 27);
            this.lblAvailableUsers.TabIndex = 1;
            this.lblAvailableUsers.Text = "可选用户";
            
            this.dgvAvailableUsers.AllowUserToAddRows = false;
            this.dgvAvailableUsers.AllowUserToDeleteRows = false;
            this.dgvAvailableUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAvailableUsers.BackgroundColor = Color.White;
            this.dgvAvailableUsers.ColumnHeadersHeight = 40;
            this.dgvAvailableUsers.Dock = DockStyle.Fill;
            this.dgvAvailableUsers.Location = new Point(10, 117);
            this.dgvAvailableUsers.Margin = new Padding(10, 117, 10, 10);
            this.dgvAvailableUsers.Name = "dgvAvailableUsers";
            this.dgvAvailableUsers.ReadOnly = true;
            this.dgvAvailableUsers.RowHeadersVisible = false;
            this.dgvAvailableUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvAvailableUsers.Size = new Size(530, 453);
            this.dgvAvailableUsers.TabIndex = 2;
            
            this.panelMiddle.BackColor = Color.FromArgb(240, 240, 240);
            this.panelMiddle.Controls.Add(this.btnRemoveAll);
            this.panelMiddle.Controls.Add(this.btnRemove);
            this.panelMiddle.Controls.Add(this.btnAdd);
            this.panelMiddle.Controls.Add(this.btnAddAll);
            this.panelMiddle.Dock = DockStyle.Left;
            this.panelMiddle.Location = new Point(0, 0);
            this.panelMiddle.Name = "panelMiddle";
            this.panelMiddle.Size = new Size(120, 580);
            this.panelMiddle.TabIndex = 1;
            
            this.btnAddAll.Location = new Point(10, 180);
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new Size(100, 40);
            this.btnAddAll.TabIndex = 0;
            this.btnAddAll.Text = "全部添加 >>";
            this.btnAddAll.Click += new EventHandler(this.btnAddAll_Click);
            
            this.btnAdd.Location = new Point(10, 240);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(100, 40);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "添加 >";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            
            this.btnRemove.Location = new Point(10, 300);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new Size(100, 40);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "< 移除";
            this.btnRemove.Click += new EventHandler(this.btnRemove_Click);
            
            this.btnRemoveAll.Location = new Point(10, 360);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new Size(100, 40);
            this.btnRemoveAll.TabIndex = 3;
            this.btnRemoveAll.Text = "<< 全部移除";
            this.btnRemoveAll.Click += new EventHandler(this.btnRemoveAll_Click);
            
            this.panelRight.Controls.Add(this.dgvSelectedUsers);
            this.panelRight.Controls.Add(this.lblSelectedUsers);
            this.panelRight.Controls.Add(this.panelMiddle);
            this.panelRight.Dock = DockStyle.Fill;
            this.panelRight.Location = new Point(0, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Padding = new Padding(0, 10, 10, 10);
            this.panelRight.Size = new Size(846, 580);
            this.panelRight.TabIndex = 0;
            
            this.lblSelectedUsers.AutoSize = true;
            this.lblSelectedUsers.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            this.lblSelectedUsers.Location = new Point(130, 10);
            this.lblSelectedUsers.Name = "lblSelectedUsers";
            this.lblSelectedUsers.Size = new Size(92, 27);
            this.lblSelectedUsers.TabIndex = 0;
            this.lblSelectedUsers.Text = "已选用户";
            
            this.dgvSelectedUsers.AllowUserToAddRows = false;
            this.dgvSelectedUsers.AllowUserToDeleteRows = false;
            this.dgvSelectedUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSelectedUsers.BackgroundColor = Color.White;
            this.dgvSelectedUsers.ColumnHeadersHeight = 40;
            this.dgvSelectedUsers.Dock = DockStyle.Fill;
            this.dgvSelectedUsers.Location = new Point(120, 37);
            this.dgvSelectedUsers.Margin = new Padding(120, 37, 10, 10);
            this.dgvSelectedUsers.Name = "dgvSelectedUsers";
            this.dgvSelectedUsers.ReadOnly = true;
            this.dgvSelectedUsers.RowHeadersVisible = false;
            this.dgvSelectedUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvSelectedUsers.Size = new Size(716, 533);
            this.dgvSelectedUsers.TabIndex = 1;
            
            this.panelBottom.Controls.Add(this.lblStatus);
            this.panelBottom.Dock = DockStyle.Bottom;
            this.panelBottom.Location = new Point(0, 680);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new Size(1400, 40);
            this.panelBottom.TabIndex = 2;
            
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = Color.Gray;
            this.lblStatus.Location = new Point(20, 10);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(550, 24);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "提示：从左侧选择用户，添加到右侧列表，然后点击\"批量分配角色\"按钮";
            
            this.AutoScaleDimensions = new SizeF(144F, 144F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.BackColor = Color.White;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Font = new Font("Microsoft YaHei UI", 9F);
            this.MinimumSize = new Size(1400, 720);
            this.Name = "BatchRoleAssignmentControl";
            this.Size = new Size(1400, 720);
            this.Load += new EventHandler(this.BatchRoleAssignmentControl_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedUsers)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.panelLeftSearch.ResumeLayout(false);
            this.panelLeftSearch.PerformLayout();
            this.panelMiddle.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);
        }

        private Panel panelTop;
        private Label lblTitle;
        private Label lblTargetRole;
        private ComboBox cboTargetRole;
        private Button btnAssign;
        private SplitContainer splitContainer;
        private Panel panelLeft;
        private Panel panelLeftSearch;
        private Label lblSearch;
        private TextBox txtSearch;
        private Label lblCurrentRole;
        private ComboBox cboCurrentRole;
        private Button btnSearch;
        private Label lblAvailableUsers;
        private DataGridView dgvAvailableUsers;
        private Panel panelMiddle;
        private Button btnAddAll;
        private Button btnAdd;
        private Button btnRemove;
        private Button btnRemoveAll;
        private Panel panelRight;
        private Label lblSelectedUsers;
        private DataGridView dgvSelectedUsers;
        private Panel panelBottom;
        private Label lblStatus;

        private DataTable availableUsersTable;
        private DataTable selectedUsersTable;

        private void BatchRoleAssignmentControl_Load(object sender, EventArgs e)
        {
            LoadRoles();
            InitializeDataTables();
            LoadAvailableUsers();
        }

        private void LoadRoles()
        {
            cboCurrentRole.Items.Clear();
            cboCurrentRole.Items.Add(new RoleItem { Value = "", Text = "全部角色" });
            cboCurrentRole.Items.Add(new RoleItem { Value = "Reader", Text = "读者" });
            cboCurrentRole.Items.Add(new RoleItem { Value = "Librarian", Text = "图书管理员" });
            cboCurrentRole.Items.Add(new RoleItem { Value = "Cataloger", Text = "图书采编员" });
            cboCurrentRole.Items.Add(new RoleItem { Value = "Admin", Text = "系统管理员" });
            cboCurrentRole.SelectedIndex = 0;

            cboTargetRole.Items.Clear();
            cboTargetRole.Items.Add(new RoleItem { Value = "Reader", Text = "读者" });
            cboTargetRole.Items.Add(new RoleItem { Value = "Librarian", Text = "图书管理员" });
            cboTargetRole.Items.Add(new RoleItem { Value = "Cataloger", Text = "图书采编员" });
            cboTargetRole.Items.Add(new RoleItem { Value = "Admin", Text = "系统管理员" });
            cboTargetRole.SelectedIndex = 0;
        }

        private void InitializeDataTables()
        {
            availableUsersTable = new DataTable();
            availableUsersTable.Columns.Add("user_id", typeof(int));
            availableUsersTable.Columns.Add("用户名", typeof(string));
            availableUsersTable.Columns.Add("显示名称", typeof(string));
            availableUsersTable.Columns.Add("当前角色", typeof(string));
            availableUsersTable.Columns.Add("借书证号", typeof(string));

            selectedUsersTable = availableUsersTable.Clone();

            dgvAvailableUsers.DataSource = availableUsersTable;
            dgvSelectedUsers.DataSource = selectedUsersTable;

            if (dgvAvailableUsers.Columns.Contains("user_id"))
            {
                dgvAvailableUsers.Columns["user_id"].Visible = false;
            }
            if (dgvSelectedUsers.Columns.Contains("user_id"))
            {
                dgvSelectedUsers.Columns["user_id"].Visible = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadAvailableUsers();
        }

        private void LoadAvailableUsers()
        {
            try
            {
                string sql = @"
                    SELECT user_id, username AS 用户名, display_name AS 显示名称,
                           CASE user_role
                               WHEN 'Reader' THEN N'读者'
                               WHEN 'Librarian' THEN N'图书管理员'
                               WHEN 'Cataloger' THEN N'图书采编员'
                               WHEN 'Admin' THEN N'系统管理员'
                           END AS 当前角色,
                           ISNULL(cardID, '') AS 借书证号
                    FROM app_user
                    WHERE is_active = 1";

                List<System.Data.SqlClient.SqlParameter> parameters = new List<System.Data.SqlClient.SqlParameter>();

                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    sql += " AND (username LIKE @kw OR display_name LIKE @kw)";
                    parameters.Add(DatabaseHelper.CreateParameter("@kw", "%" + txtSearch.Text.Trim() + "%"));
                }

                if (cboCurrentRole.SelectedItem != null)
                {
                    string role = ((RoleItem)cboCurrentRole.SelectedItem).Value;
                    if (!string.IsNullOrEmpty(role))
                    {
                        sql += " AND user_role = @role";
                        parameters.Add(DatabaseHelper.CreateParameter("@role", role));
                    }
                }

                sql += " ORDER BY user_id";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters.ToArray());

                availableUsersTable.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    int userId = Convert.ToInt32(row["user_id"]);
                    bool alreadySelected = false;

                    foreach (DataRow selectedRow in selectedUsersTable.Rows)
                    {
                        if (Convert.ToInt32(selectedRow["user_id"]) == userId)
                        {
                            alreadySelected = true;
                            break;
                        }
                    }

                    if (!alreadySelected)
                    {
                        availableUsersTable.ImportRow(row);
                    }
                }

                lblAvailableUsers.Text = $"可选用户 (共 {availableUsersTable.Rows.Count} 人)";
                lblSelectedUsers.Text = $"已选用户 (共 {selectedUsersTable.Rows.Count} 人)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载用户列表失败：" + ex.Message, "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddAll_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in availableUsersTable.Rows)
            {
                selectedUsersTable.ImportRow(row);
            }
            availableUsersTable.Clear();

            lblAvailableUsers.Text = $"可选用户 (共 {availableUsersTable.Rows.Count} 人)";
            lblSelectedUsers.Text = $"已选用户 (共 {selectedUsersTable.Rows.Count} 人)";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgvAvailableUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要添加的用户", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (DataGridViewRow row in dgvAvailableUsers.SelectedRows)
            {
                DataRow dataRow = ((DataRowView)row.DataBoundItem).Row;
                selectedUsersTable.ImportRow(dataRow);
                availableUsersTable.Rows.Remove(dataRow);
            }

            lblAvailableUsers.Text = $"可选用户 (共 {availableUsersTable.Rows.Count} 人)";
            lblSelectedUsers.Text = $"已选用户 (共 {selectedUsersTable.Rows.Count} 人)";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvSelectedUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要移除的用户", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (DataGridViewRow row in dgvSelectedUsers.SelectedRows)
            {
                DataRow dataRow = ((DataRowView)row.DataBoundItem).Row;
                availableUsersTable.ImportRow(dataRow);
                selectedUsersTable.Rows.Remove(dataRow);
            }

            lblAvailableUsers.Text = $"可选用户 (共 {availableUsersTable.Rows.Count} 人)";
            lblSelectedUsers.Text = $"已选用户 (共 {selectedUsersTable.Rows.Count} 人)";
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in selectedUsersTable.Rows)
            {
                availableUsersTable.ImportRow(row);
            }
            selectedUsersTable.Clear();

            lblAvailableUsers.Text = $"可选用户 (共 {availableUsersTable.Rows.Count} 人)";
            lblSelectedUsers.Text = $"已选用户 (共 {selectedUsersTable.Rows.Count} 人)";
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            if (selectedUsersTable.Rows.Count == 0)
            {
                MessageBox.Show("请先选择要分配角色的用户", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboTargetRole.SelectedItem == null)
            {
                MessageBox.Show("请选择目标角色", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string targetRole = ((RoleItem)cboTargetRole.SelectedItem).Value;
            string targetRoleName = ((RoleItem)cboTargetRole.SelectedItem).Text;

            if (MessageBox.Show($"确定将选中的 {selectedUsersTable.Rows.Count} 名用户的角色批量设置为【{targetRoleName}】？", 
                "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                int successCount = 0;
                int failCount = 0;

                foreach (DataRow row in selectedUsersTable.Rows)
                {
                    int userId = Convert.ToInt32(row["user_id"]);

                    try
                    {
                        string sql = "UPDATE app_user SET user_role = @role WHERE user_id = @userId";
                        DatabaseHelper.ExecuteNonQuery(sql,
                            DatabaseHelper.CreateParameter("@role", targetRole),
                            DatabaseHelper.CreateParameter("@userId", userId));

                        successCount++;
                    }
                    catch
                    {
                        failCount++;
                    }
                }

                if (failCount == 0)
                {
                    MessageBox.Show($"批量分配成功！共处理 {successCount} 名用户", "成功", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"批量分配完成：成功 {successCount} 人，失败 {failCount} 人", "完成", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                selectedUsersTable.Clear();
                LoadAvailableUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("批量分配失败：" + ex.Message, "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
