using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BooksManagermentSysytem.Models;
using BooksManagermentSysytem.Services;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 角色权限管理控件 - 功能权限精细化管理
    /// </summary>
    public partial class PermissionManagementControl : UserControl
    {
        private string currentRoleName;

        public PermissionManagementControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panelTop = new Panel();
            this.lblTitle = new Label();
            this.lblRoleLabel = new Label();
            this.cboRole = new ComboBox();
            this.btnSave = new Button();
            this.btnReset = new Button();
            this.splitContainer = new SplitContainer();
            this.panelGroups = new Panel();
            this.lstGroups = new ListBox();
            this.lblGroups = new Label();
            this.panelPermissions = new Panel();
            this.dgvPermissions = new DataGridView();
            this.colPermissionName = new DataGridViewTextBoxColumn();
            this.colPermissionCode = new DataGridViewTextBoxColumn();
            this.colDescription = new DataGridViewTextBoxColumn();
            this.colIsGranted = new DataGridViewCheckBoxColumn();
            this.lblPermissions = new Label();
            this.panelBottom = new Panel();
            this.lblStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPermissions)).BeginInit();
            this.panelTop.SuspendLayout();
            this.panelGroups.SuspendLayout();
            this.panelPermissions.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            
            this.panelTop.BackColor = Color.FromArgb(245, 245, 245);
            this.panelTop.Controls.Add(this.btnReset);
            this.panelTop.Controls.Add(this.btnSave);
            this.panelTop.Controls.Add(this.cboRole);
            this.panelTop.Controls.Add(this.lblRoleLabel);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Location = new Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new Size(1200, 100);
            this.panelTop.TabIndex = 0;
            
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold);
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(154, 27);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "角色权限管理";
            
            this.lblRoleLabel.AutoSize = true;
            this.lblRoleLabel.Location = new Point(20, 63);
            this.lblRoleLabel.Name = "lblRoleLabel";
            this.lblRoleLabel.Size = new Size(100, 24);
            this.lblRoleLabel.TabIndex = 1;
            this.lblRoleLabel.Text = "选择角色：";
            
            this.cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboRole.Location = new Point(120, 60);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new Size(200, 32);
            this.cboRole.TabIndex = 2;
            this.cboRole.SelectedIndexChanged += new EventHandler(this.cboRole_SelectedIndexChanged);
            
            this.btnSave.BackColor = Color.FromArgb(0, 122, 204);
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Location = new Point(350, 55);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(120, 40);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存设置";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);
            
            this.btnReset.Location = new Point(490, 55);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new Size(120, 40);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "重置";
            this.btnReset.Click += new EventHandler(this.btnReset_Click);
            
            this.splitContainer.Dock = DockStyle.Fill;
            this.splitContainer.Location = new Point(0, 100);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Panel1.Controls.Add(this.panelGroups);
            this.splitContainer.Panel2.Controls.Add(this.panelPermissions);
            this.splitContainer.Size = new Size(1200, 600);
            this.splitContainer.SplitterDistance = 300;
            this.splitContainer.TabIndex = 1;
            
            this.panelGroups.Controls.Add(this.lstGroups);
            this.panelGroups.Controls.Add(this.lblGroups);
            this.panelGroups.Dock = DockStyle.Fill;
            this.panelGroups.Location = new Point(0, 0);
            this.panelGroups.Name = "panelGroups";
            this.panelGroups.Padding = new Padding(10);
            this.panelGroups.Size = new Size(300, 600);
            this.panelGroups.TabIndex = 0;
            
            this.lblGroups.AutoSize = true;
            this.lblGroups.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            this.lblGroups.Location = new Point(10, 10);
            this.lblGroups.Name = "lblGroups";
            this.lblGroups.Size = new Size(92, 27);
            this.lblGroups.TabIndex = 0;
            this.lblGroups.Text = "权限分组";
            
            this.lstGroups.Dock = DockStyle.Fill;
            this.lstGroups.Font = new Font("Microsoft YaHei UI", 9F);
            this.lstGroups.ItemHeight = 24;
            this.lstGroups.Location = new Point(10, 37);
            this.lstGroups.Margin = new Padding(10, 37, 10, 10);
            this.lstGroups.Name = "lstGroups";
            this.lstGroups.Size = new Size(280, 553);
            this.lstGroups.TabIndex = 1;
            this.lstGroups.SelectedIndexChanged += new EventHandler(this.lstGroups_SelectedIndexChanged);
            
            this.panelPermissions.Controls.Add(this.dgvPermissions);
            this.panelPermissions.Controls.Add(this.lblPermissions);
            this.panelPermissions.Dock = DockStyle.Fill;
            this.panelPermissions.Location = new Point(0, 0);
            this.panelPermissions.Name = "panelPermissions";
            this.panelPermissions.Padding = new Padding(10);
            this.panelPermissions.Size = new Size(896, 600);
            this.panelPermissions.TabIndex = 0;
            
            this.lblPermissions.AutoSize = true;
            this.lblPermissions.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            this.lblPermissions.Location = new Point(10, 10);
            this.lblPermissions.Name = "lblPermissions";
            this.lblPermissions.Size = new Size(92, 27);
            this.lblPermissions.TabIndex = 0;
            this.lblPermissions.Text = "权限列表";
            
            this.dgvPermissions.AllowUserToAddRows = false;
            this.dgvPermissions.AllowUserToDeleteRows = false;
            this.dgvPermissions.AllowUserToResizeRows = false;
            this.dgvPermissions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPermissions.BackgroundColor = Color.White;
            this.dgvPermissions.ColumnHeadersHeight = 40;
            this.dgvPermissions.Columns.AddRange(new DataGridViewColumn[] {
                this.colPermissionName,
                this.colPermissionCode,
                this.colDescription,
                this.colIsGranted
            });
            this.dgvPermissions.Dock = DockStyle.Fill;
            this.dgvPermissions.Location = new Point(10, 37);
            this.dgvPermissions.Margin = new Padding(10, 37, 10, 10);
            this.dgvPermissions.Name = "dgvPermissions";
            this.dgvPermissions.RowHeadersVisible = false;
            this.dgvPermissions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvPermissions.Size = new Size(876, 553);
            this.dgvPermissions.TabIndex = 1;
            
            this.colPermissionName.DataPropertyName = "PermissionName";
            this.colPermissionName.FillWeight = 30F;
            this.colPermissionName.HeaderText = "权限名称";
            this.colPermissionName.Name = "colPermissionName";
            this.colPermissionName.ReadOnly = true;
            
            this.colPermissionCode.DataPropertyName = "PermissionCode";
            this.colPermissionCode.FillWeight = 25F;
            this.colPermissionCode.HeaderText = "权限代码";
            this.colPermissionCode.Name = "colPermissionCode";
            this.colPermissionCode.ReadOnly = true;
            
            this.colDescription.DataPropertyName = "Description";
            this.colDescription.FillWeight = 35F;
            this.colDescription.HeaderText = "说明";
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            
            this.colIsGranted.DataPropertyName = "IsGranted";
            this.colIsGranted.FillWeight = 10F;
            this.colIsGranted.HeaderText = "已授权";
            this.colIsGranted.Name = "colIsGranted";
            
            this.panelBottom.Controls.Add(this.lblStatus);
            this.panelBottom.Dock = DockStyle.Bottom;
            this.panelBottom.Location = new Point(0, 700);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new Size(1200, 40);
            this.panelBottom.TabIndex = 2;
            
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = Color.Gray;
            this.lblStatus.Location = new Point(20, 10);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(450, 24);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "提示：勾选权限项表示授予该角色该权限，取消勾选表示撤销权限";
            
            this.AutoScaleDimensions = new SizeF(144F, 144F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.BackColor = Color.White;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Font = new Font("Microsoft YaHei UI", 9F);
            this.MinimumSize = new Size(1200, 740);
            this.Name = "PermissionManagementControl";
            this.Size = new Size(1200, 740);
            this.Load += new EventHandler(this.PermissionManagementControl_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPermissions)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelGroups.ResumeLayout(false);
            this.panelGroups.PerformLayout();
            this.panelPermissions.ResumeLayout(false);
            this.panelPermissions.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);
        }

        private Panel panelTop;
        private Label lblTitle;
        private Label lblRoleLabel;
        private ComboBox cboRole;
        private Button btnSave;
        private Button btnReset;
        private SplitContainer splitContainer;
        private Panel panelGroups;
        private Label lblGroups;
        private ListBox lstGroups;
        private Panel panelPermissions;
        private Label lblPermissions;
        private DataGridView dgvPermissions;
        private DataGridViewTextBoxColumn colPermissionName;
        private DataGridViewTextBoxColumn colPermissionCode;
        private DataGridViewTextBoxColumn colDescription;
        private DataGridViewCheckBoxColumn colIsGranted;
        private Panel panelBottom;
        private Label lblStatus;

        private void PermissionManagementControl_Load(object sender, EventArgs e)
        {
            LoadRoles();
            LoadPermissionGroups();
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

        private void LoadPermissionGroups()
        {
            try
            {
                List<string> groups = PermissionService.Instance.GetPermissionGroups();
                lstGroups.Items.Clear();
                lstGroups.Items.Add("全部权限");
                foreach (string group in groups)
                {
                    lstGroups.Items.Add(group);
                }

                if (lstGroups.Items.Count > 0)
                {
                    lstGroups.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载权限分组失败：" + ex.Message, "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboRole.SelectedItem == null) return;

            currentRoleName = ((RoleItem)cboRole.SelectedItem).Value;
            LoadPermissions();
        }

        private void lstGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPermissions();
        }

        private void LoadPermissions()
        {
            if (string.IsNullOrEmpty(currentRoleName)) return;

            try
            {
                List<RolePermissionConfig> configs = PermissionService.Instance.GetRolePermissionConfig(currentRoleName);

                string selectedGroup = lstGroups.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedGroup) && selectedGroup != "全部权限")
                {
                    configs = configs.Where(c => c.PermissionGroup == selectedGroup).ToList();
                }

                dgvPermissions.DataSource = configs;

                lblPermissions.Text = $"权限列表 (共 {configs.Count} 项)";

                if (currentRoleName == "Admin")
                {
                    dgvPermissions.Columns["colIsGranted"].ReadOnly = true;
                    lblStatus.Text = "提示：系统管理员拥有所有权限，不可修改";
                    lblStatus.ForeColor = Color.Red;
                }
                else
                {
                    dgvPermissions.Columns["colIsGranted"].ReadOnly = false;
                    lblStatus.Text = "提示：勾选权限项表示授予该角色该权限，取消勾选表示撤销权限";
                    lblStatus.ForeColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载权限列表失败：" + ex.Message, "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentRoleName))
            {
                MessageBox.Show("请先选择角色", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (currentRoleName == "Admin")
            {
                MessageBox.Show("系统管理员权限不可修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"确定保存【{cboRole.Text}】的权限设置？", "确认", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                List<string> grantedPermissions = new List<string>();
                List<RolePermissionConfig> configs = (List<RolePermissionConfig>)dgvPermissions.DataSource;

                if (configs != null)
                {
                    foreach (var config in configs)
                    {
                        if (config.IsGranted)
                        {
                            grantedPermissions.Add(config.PermissionCode);
                        }
                    }
                }

                string grantedBy = AuthenticationService.Instance.CurrentUser?.Username ?? "SYSTEM";

                string errorMessage;
                if (PermissionService.Instance.SetRolePermissions(currentRoleName, grantedPermissions, grantedBy, out errorMessage))
                {
                    MessageBox.Show("权限设置保存成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPermissions();
                }
                else
                {
                    MessageBox.Show(errorMessage, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadPermissions();
        }

        private class RoleItem
        {
            public string Value { get; set; }
            public string Text { get; set; }
            public override string ToString() { return Text; }
        }
    }
}
