using System;
using System.Data;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Services;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 分类管理控件 - 管理中图法图书分类
    /// </summary>
    public partial class CategoryManagementControl : UserControl
    {
        public CategoryManagementControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.lblTreeTitle = new System.Windows.Forms.Label();
            this.treeCategories = new System.Windows.Forms.TreeView();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.lblDetailsTitle = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblParent = new System.Windows.Forms.Label();
            this.cboParent = new System.Windows.Forms.ComboBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblBooks = new System.Windows.Forms.Label();
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panelDetails.SuspendLayout();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Size = new System.Drawing.Size(900, 550);
            this.splitContainer.SplitterDistance = 280;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeCategories);
            this.splitContainer.Panel1.Controls.Add(this.lblTreeTitle);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.dgvBooks);
            this.splitContainer.Panel2.Controls.Add(this.lblBooks);
            this.splitContainer.Panel2.Controls.Add(this.panelButtons);
            this.splitContainer.Panel2.Controls.Add(this.panelDetails);
            // 
            // lblTreeTitle
            // 
            this.lblTreeTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.lblTreeTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTreeTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTreeTitle.ForeColor = System.Drawing.Color.White;
            this.lblTreeTitle.Size = new System.Drawing.Size(280, 30);
            this.lblTreeTitle.Text = "  图书分类（中图法）";
            this.lblTreeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // treeCategories
            // 
            this.treeCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeCategories.Location = new System.Drawing.Point(0, 30);
            this.treeCategories.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeCategories_AfterSelect);
            // 
            // panelDetails
            // 
            this.panelDetails.Controls.Add(this.txtDescription);
            this.panelDetails.Controls.Add(this.lblDescription);
            this.panelDetails.Controls.Add(this.cboParent);
            this.panelDetails.Controls.Add(this.lblParent);
            this.panelDetails.Controls.Add(this.txtName);
            this.panelDetails.Controls.Add(this.lblName);
            this.panelDetails.Controls.Add(this.txtCode);
            this.panelDetails.Controls.Add(this.lblCode);
            this.panelDetails.Controls.Add(this.lblDetailsTitle);
            this.panelDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDetails.Size = new System.Drawing.Size(616, 180);
            // 
            // lblDetailsTitle
            // 
            this.lblDetailsTitle.AutoSize = true;
            this.lblDetailsTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetailsTitle.Location = new System.Drawing.Point(10, 10);
            this.lblDetailsTitle.Text = "分类详情";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(10, 45);
            this.lblCode.Text = "分类号：";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(80, 42);
            this.txtCode.Size = new System.Drawing.Size(150, 23);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(250, 45);
            this.lblName.Text = "分类名称：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(320, 42);
            this.txtName.Size = new System.Drawing.Size(200, 23);
            // 
            // lblParent
            // 
            this.lblParent.AutoSize = true;
            this.lblParent.Location = new System.Drawing.Point(10, 80);
            this.lblParent.Text = "上级分类：";
            // 
            // cboParent
            // 
            this.cboParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboParent.Location = new System.Drawing.Point(80, 77);
            this.cboParent.Size = new System.Drawing.Size(250, 25);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(10, 115);
            this.lblDescription.Text = "分类说明：";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(80, 112);
            this.txtDescription.Multiline = true;
            this.txtDescription.Size = new System.Drawing.Size(440, 55);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Controls.Add(this.btnDelete);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Controls.Add(this.btnNew);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelButtons.Location = new System.Drawing.Point(0, 180);
            this.panelButtons.Size = new System.Drawing.Size(616, 45);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(10, 8);
            this.btnNew.Size = new System.Drawing.Size(80, 28);
            this.btnNew.Text = "新建";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(100, 8);
            this.btnSave.Size = new System.Drawing.Size(80, 28);
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(190, 8);
            this.btnDelete.Size = new System.Drawing.Size(80, 28);
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(280, 8);
            this.btnCancel.Size = new System.Drawing.Size(80, 28);
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblBooks
            // 
            this.lblBooks.AutoSize = true;
            this.lblBooks.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBooks.Location = new System.Drawing.Point(10, 230);
            this.lblBooks.Text = "该分类下的书目：";
            // 
            // dgvBooks
            // 
            this.dgvBooks.AllowUserToAddRows = false;
            this.dgvBooks.AllowUserToDeleteRows = false;
            this.dgvBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBooks.BackgroundColor = System.Drawing.Color.White;
            this.dgvBooks.Location = new System.Drawing.Point(10, 255);
            this.dgvBooks.ReadOnly = true;
            this.dgvBooks.RowHeadersVisible = false;
            this.dgvBooks.Size = new System.Drawing.Size(590, 280);
            // 
            // CategoryManagementControl
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Size = new System.Drawing.Size(900, 550);
            this.Load += new System.EventHandler(this.CategoryManagementControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            this.splitContainer.ResumeLayout(false);
            this.panelDetails.ResumeLayout(false);
            this.panelDetails.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label lblTreeTitle;
        private System.Windows.Forms.TreeView treeCategories;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.Label lblDetailsTitle;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblParent;
        private System.Windows.Forms.ComboBox cboParent;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblBooks;
        private System.Windows.Forms.DataGridView dgvBooks;

        private int? currentCategoryId;
        private bool isNewMode;

        private void CategoryManagementControl_Load(object sender, EventArgs e)
        {
            LoadCategoryTree();
            LoadParentCombo();
        }

        private void LoadCategoryTree()
        {
            treeCategories.Nodes.Clear();

            try
            {
                string sql = @"SELECT category_id, category_code, category_name, parent_category_id 
                              FROM BOOK_CATEGORY ORDER BY category_code";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql);

                // 构建树
                foreach (DataRow row in dt.Rows)
                {
                    if (row["parent_category_id"] == DBNull.Value)
                    {
                        TreeNode node = new TreeNode($"[{row["category_code"]}] {row["category_name"]}");
                        node.Tag = Convert.ToInt32(row["category_id"]);
                        AddChildNodes(node, dt, Convert.ToInt32(row["category_id"]));
                        treeCategories.Nodes.Add(node);
                    }
                }
                treeCategories.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载分类失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddChildNodes(TreeNode parentNode, DataTable dt, int parentId)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row["parent_category_id"] != DBNull.Value && 
                    Convert.ToInt32(row["parent_category_id"]) == parentId)
                {
                    TreeNode node = new TreeNode($"[{row["category_code"]}] {row["category_name"]}");
                    node.Tag = Convert.ToInt32(row["category_id"]);
                    AddChildNodes(node, dt, Convert.ToInt32(row["category_id"]));
                    parentNode.Nodes.Add(node);
                }
            }
        }

        private void LoadParentCombo()
        {
            cboParent.Items.Clear();
            cboParent.Items.Add(new ComboItem { Id = null, Text = "(无上级分类)" });

            try
            {
                string sql = "SELECT category_id, category_code, category_name FROM BOOK_CATEGORY ORDER BY category_code";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql);
                foreach (DataRow row in dt.Rows)
                {
                    cboParent.Items.Add(new ComboItem
                    {
                        Id = Convert.ToInt32(row["category_id"]),
                        Text = $"[{row["category_code"]}] {row["category_name"]}"
                    });
                }
            }
            catch { }

            cboParent.SelectedIndex = 0;
        }

        private void treeCategories_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag == null) return;

            currentCategoryId = (int)e.Node.Tag;
            isNewMode = false;
            LoadCategoryDetails(currentCategoryId.Value);
            LoadCategoryBooks(currentCategoryId.Value);
        }

        private void LoadCategoryDetails(int categoryId)
        {
            try
            {
                string sql = "SELECT * FROM BOOK_CATEGORY WHERE category_id = @id";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.CreateParameter("@id", categoryId));

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtCode.Text = row["category_code"].ToString();
                    txtName.Text = row["category_name"].ToString();
                    txtDescription.Text = row["Description"]?.ToString() ?? "";

                    int? parentId = row["parent_category_id"] == DBNull.Value ? null : (int?)Convert.ToInt32(row["parent_category_id"]);
                    for (int i = 0; i < cboParent.Items.Count; i++)
                    {
                        if (((ComboItem)cboParent.Items[i]).Id == parentId)
                        {
                            cboParent.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            catch { }
        }

        private void LoadCategoryBooks(int categoryId)
        {
            try
            {
                string sql = @"SELECT bibliography_name AS 书名, ISBN, publish AS 出版社, 
                              price AS 价格, create_time AS 录入时间
                              FROM BIBLIOGRAPHY WHERE category_id = @id ORDER BY bibliography_name";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.CreateParameter("@id", categoryId));
                dgvBooks.DataSource = dt;
            }
            catch { }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            isNewMode = true;
            currentCategoryId = null;
            txtCode.Clear();
            txtName.Clear();
            txtDescription.Clear();
            cboParent.SelectedIndex = 0;
            txtCode.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("请填写分类号和分类名称", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int? parentId = ((ComboItem)cboParent.SelectedItem).Id;
                string operatorName = AuthenticationService.Instance.CurrentUser?.Username ?? "system";

                if (isNewMode)
                {
                    string sql = @"INSERT INTO BOOK_CATEGORY (category_code, category_name, parent_category_id, Description)
                                  VALUES (@code, @name, @parent, @desc)";
                    DatabaseHelper.ExecuteNonQuery(sql,
                        DatabaseHelper.CreateParameter("@code", txtCode.Text.Trim()),
                        DatabaseHelper.CreateParameter("@name", txtName.Text.Trim()),
                        DatabaseHelper.CreateParameter("@parent", parentId.HasValue ? (object)parentId.Value : DBNull.Value),
                        DatabaseHelper.CreateParameter("@desc", txtDescription.Text.Trim()));

                    // 记录日志
                    LogCatalogAction("CATEGORY", txtCode.Text.Trim(), "新增", operatorName, $"新增分类：{txtName.Text}");
                }
                else if (currentCategoryId.HasValue)
                {
                    string sql = @"UPDATE BOOK_CATEGORY SET category_code = @code, category_name = @name, 
                                  parent_category_id = @parent, Description = @desc, update_time = GETDATE()
                                  WHERE category_id = @id";
                    DatabaseHelper.ExecuteNonQuery(sql,
                        DatabaseHelper.CreateParameter("@code", txtCode.Text.Trim()),
                        DatabaseHelper.CreateParameter("@name", txtName.Text.Trim()),
                        DatabaseHelper.CreateParameter("@parent", parentId.HasValue ? (object)parentId.Value : DBNull.Value),
                        DatabaseHelper.CreateParameter("@desc", txtDescription.Text.Trim()),
                        DatabaseHelper.CreateParameter("@id", currentCategoryId.Value));

                    LogCatalogAction("CATEGORY", txtCode.Text.Trim(), "更新", operatorName, $"更新分类：{txtName.Text}");
                }

                MessageBox.Show("保存成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCategoryTree();
                LoadParentCombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!currentCategoryId.HasValue || isNewMode)
            {
                MessageBox.Show("请选择要删除的分类", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 检查是否有子分类
            string checkChildSql = "SELECT COUNT(*) FROM BOOK_CATEGORY WHERE parent_category_id = @id";
            int childCount = Convert.ToInt32(DatabaseHelper.ExecuteScalar(checkChildSql,
                DatabaseHelper.CreateParameter("@id", currentCategoryId.Value)));

            if (childCount > 0)
            {
                MessageBox.Show("该分类下有子分类，无法删除", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 检查是否有书目
            string checkBookSql = "SELECT COUNT(*) FROM BIBLIOGRAPHY WHERE category_id = @id";
            int bookCount = Convert.ToInt32(DatabaseHelper.ExecuteScalar(checkBookSql,
                DatabaseHelper.CreateParameter("@id", currentCategoryId.Value)));

            if (bookCount > 0)
            {
                MessageBox.Show("该分类下有书目，无法删除", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("确定删除该分类？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                string sql = "DELETE FROM BOOK_CATEGORY WHERE category_id = @id";
                DatabaseHelper.ExecuteNonQuery(sql, DatabaseHelper.CreateParameter("@id", currentCategoryId.Value));

                string operatorName = AuthenticationService.Instance.CurrentUser?.Username ?? "system";
                LogCatalogAction("CATEGORY", txtCode.Text, "删除", operatorName, $"删除分类：{txtName.Text}");

                MessageBox.Show("删除成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCancel_Click(sender, e);
                LoadCategoryTree();
                LoadParentCombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            isNewMode = false;
            currentCategoryId = null;
            txtCode.Clear();
            txtName.Clear();
            txtDescription.Clear();
            cboParent.SelectedIndex = 0;
            dgvBooks.DataSource = null;
        }

        private void LogCatalogAction(string targetType, string targetId, string actionType, string operatorName, string note)
        {
            try
            {
                string sql = @"INSERT INTO catalog_log (target_type, target_id, action_type, operator, note)
                              VALUES (@type, @targetId, @action, @operator, @note)";
                DatabaseHelper.ExecuteNonQuery(sql,
                    DatabaseHelper.CreateParameter("@type", targetType),
                    DatabaseHelper.CreateParameter("@targetId", targetId),
                    DatabaseHelper.CreateParameter("@action", actionType),
                    DatabaseHelper.CreateParameter("@operator", operatorName),
                    DatabaseHelper.CreateParameter("@note", note));
            }
            catch { }
        }

        private class ComboItem
        {
            public int? Id { get; set; }
            public string Text { get; set; }
            public override string ToString() => Text;
        }
    }
}
