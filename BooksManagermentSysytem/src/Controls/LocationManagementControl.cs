using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Services;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 库位管理控件 - 管理图书馆库存位置
    /// </summary>
    public partial class LocationManagementControl : UserControl
    {
        private int? currentLocationId;
        private bool isNewMode;

        public LocationManagementControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.treeLocations = new System.Windows.Forms.TreeView();
            this.lblTreeTitle = new System.Windows.Forms.Label();
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            this.lblBooks = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblCurrentQtyValue = new System.Windows.Forms.Label();
            this.lblCurrentQty = new System.Windows.Forms.Label();
            this.numMaxCapacity = new System.Windows.Forms.NumericUpDown();
            this.lblMaxCapacity = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.lblDetailsTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.panelDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxCapacity)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeLocations);
            this.splitContainer.Panel1.Controls.Add(this.lblTreeTitle);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.dgvBooks);
            this.splitContainer.Panel2.Controls.Add(this.lblBooks);
            this.splitContainer.Panel2.Controls.Add(this.panelButtons);
            this.splitContainer.Panel2.Controls.Add(this.panelDetails);
            this.splitContainer.Size = new System.Drawing.Size(1350, 825);
            this.splitContainer.SplitterDistance = 420;
            this.splitContainer.SplitterWidth = 6;
            this.splitContainer.TabIndex = 0;
            // 
            // treeLocations
            // 
            this.treeLocations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeLocations.Location = new System.Drawing.Point(0, 45);
            this.treeLocations.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.treeLocations.Name = "treeLocations";
            this.treeLocations.Size = new System.Drawing.Size(420, 780);
            this.treeLocations.TabIndex = 0;
            this.treeLocations.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeLocations_AfterSelect);
            // 
            // lblTreeTitle
            // 
            this.lblTreeTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.lblTreeTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTreeTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTreeTitle.ForeColor = System.Drawing.Color.White;
            this.lblTreeTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTreeTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTreeTitle.Name = "lblTreeTitle";
            this.lblTreeTitle.Size = new System.Drawing.Size(420, 45);
            this.lblTreeTitle.TabIndex = 1;
            this.lblTreeTitle.Text = "  库存位置";
            this.lblTreeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvBooks
            // 
            this.dgvBooks.AllowUserToAddRows = false;
            this.dgvBooks.AllowUserToDeleteRows = false;
            this.dgvBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBooks.BackgroundColor = System.Drawing.Color.White;
            this.dgvBooks.ColumnHeadersHeight = 40;
            this.dgvBooks.Location = new System.Drawing.Point(15, 352);
            this.dgvBooks.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.ReadOnly = true;
            this.dgvBooks.RowHeadersVisible = false;
            this.dgvBooks.RowHeadersWidth = 62;
            this.dgvBooks.Size = new System.Drawing.Size(885, 450);
            this.dgvBooks.TabIndex = 0;
            // 
            // lblBooks
            // 
            this.lblBooks.AutoSize = true;
            this.lblBooks.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBooks.Location = new System.Drawing.Point(15, 315);
            this.lblBooks.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBooks.Name = "lblBooks";
            this.lblBooks.Size = new System.Drawing.Size(138, 25);
            this.lblBooks.TabIndex = 1;
            this.lblBooks.Text = "该库位的馆藏：";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnDelete);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Controls.Add(this.btnNew);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelButtons.Location = new System.Drawing.Point(0, 240);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(924, 68);
            this.panelButtons.TabIndex = 2;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(285, 12);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 42);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(150, 12);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 42);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(15, 12);
            this.btnNew.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(120, 42);
            this.btnNew.TabIndex = 2;
            this.btnNew.Text = "新建";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // panelDetails
            // 
            this.panelDetails.Controls.Add(this.cboStatus);
            this.panelDetails.Controls.Add(this.lblStatus);
            this.panelDetails.Controls.Add(this.lblCurrentQtyValue);
            this.panelDetails.Controls.Add(this.lblCurrentQty);
            this.panelDetails.Controls.Add(this.numMaxCapacity);
            this.panelDetails.Controls.Add(this.lblMaxCapacity);
            this.panelDetails.Controls.Add(this.cboType);
            this.panelDetails.Controls.Add(this.lblType);
            this.panelDetails.Controls.Add(this.txtName);
            this.panelDetails.Controls.Add(this.lblName);
            this.panelDetails.Controls.Add(this.txtCode);
            this.panelDetails.Controls.Add(this.lblCode);
            this.panelDetails.Controls.Add(this.lblDetailsTitle);
            this.panelDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDetails.Location = new System.Drawing.Point(0, 0);
            this.panelDetails.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Size = new System.Drawing.Size(924, 240);
            this.panelDetails.TabIndex = 3;
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Items.AddRange(new object[] {
            "ACTIVE",
            "INACTIVE",
            "MAINTENANCE",
            "FULL",
            "ORGANIZING"});
            this.cboStatus.Location = new System.Drawing.Point(120, 168);
            this.cboStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(223, 32);
            this.cboStatus.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(15, 172);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(100, 24);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "库位状态：";
            // 
            // lblCurrentQtyValue
            // 
            this.lblCurrentQtyValue.AutoSize = true;
            this.lblCurrentQtyValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCurrentQtyValue.Location = new System.Drawing.Point(735, 120);
            this.lblCurrentQtyValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentQtyValue.Name = "lblCurrentQtyValue";
            this.lblCurrentQtyValue.Size = new System.Drawing.Size(23, 25);
            this.lblCurrentQtyValue.TabIndex = 2;
            this.lblCurrentQtyValue.Text = "0";
            // 
            // lblCurrentQty
            // 
            this.lblCurrentQty.AutoSize = true;
            this.lblCurrentQty.Location = new System.Drawing.Point(630, 120);
            this.lblCurrentQty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentQty.Name = "lblCurrentQty";
            this.lblCurrentQty.Size = new System.Drawing.Size(100, 24);
            this.lblCurrentQty.TabIndex = 3;
            this.lblCurrentQty.Text = "当前数量：";
            // 
            // numMaxCapacity
            // 
            this.numMaxCapacity.Location = new System.Drawing.Point(480, 116);
            this.numMaxCapacity.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numMaxCapacity.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMaxCapacity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMaxCapacity.Name = "numMaxCapacity";
            this.numMaxCapacity.Size = new System.Drawing.Size(120, 30);
            this.numMaxCapacity.TabIndex = 4;
            this.numMaxCapacity.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // lblMaxCapacity
            // 
            this.lblMaxCapacity.AutoSize = true;
            this.lblMaxCapacity.Location = new System.Drawing.Point(375, 120);
            this.lblMaxCapacity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaxCapacity.Name = "lblMaxCapacity";
            this.lblMaxCapacity.Size = new System.Drawing.Size(100, 24);
            this.lblMaxCapacity.TabIndex = 5;
            this.lblMaxCapacity.Text = "最大容量：";
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.Items.AddRange(new object[] {
            "REGULAR_SHELF",
            "HOT_ZONE",
            "NEW_BOOK",
            "REFERENCE",
            "JOURNAL",
            "RESERVATION_SHELF",
            "TOOL_ONLY",
            "REPAIR_AREA"});
            this.cboType.Location = new System.Drawing.Point(120, 116);
            this.cboType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(223, 32);
            this.cboType.TabIndex = 6;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(15, 120);
            this.lblType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(100, 24);
            this.lblType.TabIndex = 7;
            this.lblType.Text = "库位类型：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(480, 63);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(298, 30);
            this.txtName.TabIndex = 8;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(375, 68);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(100, 24);
            this.lblName.TabIndex = 9;
            this.lblName.Text = "库位名称：";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(120, 63);
            this.txtCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(223, 30);
            this.txtCode.TabIndex = 10;
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(15, 68);
            this.lblCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(100, 24);
            this.lblCode.TabIndex = 11;
            this.lblCode.Text = "库位编码：";
            // 
            // lblDetailsTitle
            // 
            this.lblDetailsTitle.AutoSize = true;
            this.lblDetailsTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetailsTitle.Location = new System.Drawing.Point(15, 15);
            this.lblDetailsTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDetailsTitle.Name = "lblDetailsTitle";
            this.lblDetailsTitle.Size = new System.Drawing.Size(92, 27);
            this.lblDetailsTitle.TabIndex = 12;
            this.lblDetailsTitle.Text = "库位详情";
            // 
            // LocationManagementControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1200, 750);
            this.Name = "LocationManagementControl";
            this.Size = new System.Drawing.Size(1350, 825);
            this.Load += new System.EventHandler(this.LocationManagementControl_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.panelDetails.ResumeLayout(false);
            this.panelDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxCapacity)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label lblTreeTitle;
        private System.Windows.Forms.TreeView treeLocations;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.Label lblDetailsTitle;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label lblMaxCapacity;
        private System.Windows.Forms.NumericUpDown numMaxCapacity;
        private System.Windows.Forms.Label lblCurrentQty;
        private System.Windows.Forms.Label lblCurrentQtyValue;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblBooks;
        private System.Windows.Forms.DataGridView dgvBooks;

        private void LocationManagementControl_Load(object sender, EventArgs e)
        {
            cboType.SelectedIndex = 0;
            cboStatus.SelectedIndex = 0;
            LoadLocationTree();
        }

        private void LoadLocationTree()
        {
            treeLocations.Nodes.Clear();
            try
            {
                string sql = @"SELECT location_id, location_code, location_name, location_type, current_quantity, max_capacity, status
                              FROM STORAGE_LOCATION ORDER BY location_code";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql);

                foreach (DataRow row in dt.Rows)
                {
                    string nodeText = $"[{row["location_code"]}] {row["location_name"]} ({row["current_quantity"]}/{row["max_capacity"]})";
                    TreeNode node = new TreeNode(nodeText);
                    node.Tag = Convert.ToInt32(row["location_id"]);

                    // 根据状态设置颜色
                    string status = row["status"].ToString();
                    if (status == "FULL") node.ForeColor = System.Drawing.Color.Red;
                    else if (status == "MAINTENANCE") node.ForeColor = System.Drawing.Color.Orange;
                    else if (status == "INACTIVE") node.ForeColor = System.Drawing.Color.Gray;

                    treeLocations.Nodes.Add(node);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void treeLocations_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag == null) return;
            currentLocationId = (int)e.Node.Tag;
            isNewMode = false;
            LoadLocationDetails(currentLocationId.Value);
            LoadLocationBooks(currentLocationId.Value);
        }

        private void LoadLocationDetails(int locationId)
        {
            try
            {
                string sql = "SELECT * FROM STORAGE_LOCATION WHERE location_id = @id";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.CreateParameter("@id", locationId));

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtCode.Text = row["location_code"].ToString();
                    txtName.Text = row["location_name"].ToString();
                    cboType.SelectedItem = row["location_type"].ToString();
                    numMaxCapacity.Value = Convert.ToInt32(row["max_capacity"]);
                    lblCurrentQtyValue.Text = row["current_quantity"].ToString();
                    cboStatus.SelectedItem = row["status"].ToString();
                }
            }
            catch { }
        }

        private void LoadLocationBooks(int locationId)
        {
            try
            {
                string sql = @"SELECT bi.item_barcode AS 馆藏码, bib.bibliography_name AS 书名, 
                              bi.current_status AS 状态, bi.physical_condition AS 物理状态
                              FROM BOOK_ITEM bi
                              INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                              WHERE bi.location_id = @id ORDER BY bi.item_barcode";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.CreateParameter("@id", locationId));
                dgvBooks.DataSource = dt;
            }
            catch { }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            isNewMode = true;
            currentLocationId = null;
            txtCode.Clear();
            txtName.Clear();
            cboType.SelectedIndex = 0;
            numMaxCapacity.Value = 50;
            lblCurrentQtyValue.Text = "0";
            cboStatus.SelectedIndex = 0;
            txtCode.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("请填写库位编码和名称", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string operatorName = AuthenticationService.Instance.CurrentUser?.Username ?? "system";

                if (isNewMode)
                {
                    string sql = @"INSERT INTO STORAGE_LOCATION (location_code, location_name, location_type, max_capacity, status)
                                  VALUES (@code, @name, @type, @max, @status)";
                    DatabaseHelper.ExecuteNonQuery(sql,
                        DatabaseHelper.CreateParameter("@code", txtCode.Text.Trim()),
                        DatabaseHelper.CreateParameter("@name", txtName.Text.Trim()),
                        DatabaseHelper.CreateParameter("@type", cboType.SelectedItem.ToString()),
                        DatabaseHelper.CreateParameter("@max", (int)numMaxCapacity.Value),
                        DatabaseHelper.CreateParameter("@status", cboStatus.SelectedItem.ToString()));

                    LogCatalogAction("LOCATION", txtCode.Text.Trim(), "新增", operatorName, $"新增库位：{txtName.Text}");
                }
                else if (currentLocationId.HasValue)
                {
                    string sql = @"UPDATE STORAGE_LOCATION SET location_code = @code, location_name = @name, 
                                  location_type = @type, max_capacity = @max, status = @status
                                  WHERE location_id = @id";
                    DatabaseHelper.ExecuteNonQuery(sql,
                        DatabaseHelper.CreateParameter("@code", txtCode.Text.Trim()),
                        DatabaseHelper.CreateParameter("@name", txtName.Text.Trim()),
                        DatabaseHelper.CreateParameter("@type", cboType.SelectedItem.ToString()),
                        DatabaseHelper.CreateParameter("@max", (int)numMaxCapacity.Value),
                        DatabaseHelper.CreateParameter("@status", cboStatus.SelectedItem.ToString()),
                        DatabaseHelper.CreateParameter("@id", currentLocationId.Value));

                    LogCatalogAction("LOCATION", txtCode.Text.Trim(), "更新", operatorName, $"更新库位：{txtName.Text}");
                }

                MessageBox.Show("保存成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLocationTree();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!currentLocationId.HasValue || isNewMode) return;

            string checkSql = "SELECT COUNT(*) FROM BOOK_ITEM WHERE location_id = @id";
            int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(checkSql,
                DatabaseHelper.CreateParameter("@id", currentLocationId.Value)));

            if (count > 0)
            {
                MessageBox.Show("该库位有馆藏图书，无法删除", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("确定删除该库位？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                string sql = "DELETE FROM STORAGE_LOCATION WHERE location_id = @id";
                DatabaseHelper.ExecuteNonQuery(sql, DatabaseHelper.CreateParameter("@id", currentLocationId.Value));

                string operatorName = AuthenticationService.Instance.CurrentUser?.Username ?? "system";
                LogCatalogAction("LOCATION", txtCode.Text, "删除", operatorName, $"删除库位：{txtName.Text}");

                MessageBox.Show("删除成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnNew_Click(sender, e);
                LoadLocationTree();
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}
