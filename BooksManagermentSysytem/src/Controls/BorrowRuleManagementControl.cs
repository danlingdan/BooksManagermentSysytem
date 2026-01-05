using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Models;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 借阅规则管理控件
    /// 允许管理员为不同读者类型配置借阅规则
    /// </summary>
    public partial class BorrowRuleManagementControl : UserControl
    {
        private string currentReaderType;

        public BorrowRuleManagementControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvRules = new System.Windows.Forms.DataGridView();
            this.panelEdit = new System.Windows.Forms.Panel();
            this.lblEditTitle = new System.Windows.Forms.Label();
            this.lblReaderType = new System.Windows.Forms.Label();
            this.cboReaderType = new System.Windows.Forms.ComboBox();
            this.lblMaxBorrow = new System.Windows.Forms.Label();
            this.numMaxBorrow = new System.Windows.Forms.NumericUpDown();
            this.lblMaxCategory = new System.Windows.Forms.Label();
            this.numMaxCategory = new System.Windows.Forms.NumericUpDown();
            this.lblBorrowDays = new System.Windows.Forms.Label();
            this.numBorrowDays = new System.Windows.Forms.NumericUpDown();
            this.lblMaxRenew = new System.Windows.Forms.Label();
            this.numMaxRenew = new System.Windows.Forms.NumericUpDown();
            this.lblRenewDays = new System.Windows.Forms.Label();
            this.numRenewDays = new System.Windows.Forms.NumericUpDown();
            this.chkReference = new System.Windows.Forms.CheckBox();
            this.chkNewBooks = new System.Windows.Forms.CheckBox();
            this.chkHotBooks = new System.Windows.Forms.CheckBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRules)).BeginInit();
            this.panelEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxBorrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBorrowDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxRenew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRenewDays)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1200, 60);
            this.panelTop.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(1080, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 36);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(192, 31);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "📋 借阅规则管理";
            // 
            // dgvRules
            // 
            this.dgvRules.AllowUserToAddRows = false;
            this.dgvRules.AllowUserToDeleteRows = false;
            this.dgvRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRules.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRules.BackgroundColor = System.Drawing.Color.White;
            this.dgvRules.ColumnHeadersHeight = 40;
            this.dgvRules.Location = new System.Drawing.Point(20, 80);
            this.dgvRules.MultiSelect = false;
            this.dgvRules.Name = "dgvRules";
            this.dgvRules.ReadOnly = true;
            this.dgvRules.RowHeadersVisible = false;
            this.dgvRules.RowHeadersWidth = 62;
            this.dgvRules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRules.Size = new System.Drawing.Size(1160, 250);
            this.dgvRules.TabIndex = 0;
            this.dgvRules.SelectionChanged += new System.EventHandler(this.dgvRules_SelectionChanged);
            // 
            // panelEdit
            // 
            this.panelEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEdit.Controls.Add(this.lblEditTitle);
            this.panelEdit.Controls.Add(this.lblReaderType);
            this.panelEdit.Controls.Add(this.cboReaderType);
            this.panelEdit.Controls.Add(this.lblMaxBorrow);
            this.panelEdit.Controls.Add(this.numMaxBorrow);
            this.panelEdit.Controls.Add(this.lblMaxCategory);
            this.panelEdit.Controls.Add(this.numMaxCategory);
            this.panelEdit.Controls.Add(this.lblBorrowDays);
            this.panelEdit.Controls.Add(this.numBorrowDays);
            this.panelEdit.Controls.Add(this.lblMaxRenew);
            this.panelEdit.Controls.Add(this.numMaxRenew);
            this.panelEdit.Controls.Add(this.lblRenewDays);
            this.panelEdit.Controls.Add(this.numRenewDays);
            this.panelEdit.Controls.Add(this.chkReference);
            this.panelEdit.Controls.Add(this.chkNewBooks);
            this.panelEdit.Controls.Add(this.chkHotBooks);
            this.panelEdit.Controls.Add(this.lblRemark);
            this.panelEdit.Controls.Add(this.txtRemark);
            this.panelEdit.Controls.Add(this.btnSave);
            this.panelEdit.Controls.Add(this.btnCancel);
            this.panelEdit.Location = new System.Drawing.Point(20, 350);
            this.panelEdit.Name = "panelEdit";
            this.panelEdit.Size = new System.Drawing.Size(1160, 300);
            this.panelEdit.TabIndex = 1;
            // 
            // lblEditTitle
            // 
            this.lblEditTitle.AutoSize = true;
            this.lblEditTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEditTitle.Location = new System.Drawing.Point(15, 15);
            this.lblEditTitle.Name = "lblEditTitle";
            this.lblEditTitle.Size = new System.Drawing.Size(92, 27);
            this.lblEditTitle.TabIndex = 0;
            this.lblEditTitle.Text = "规则编辑";
            // 
            // lblReaderType
            // 
            this.lblReaderType.AutoSize = true;
            this.lblReaderType.Location = new System.Drawing.Point(16, 55);
            this.lblReaderType.Name = "lblReaderType";
            this.lblReaderType.Size = new System.Drawing.Size(100, 24);
            this.lblReaderType.TabIndex = 1;
            this.lblReaderType.Text = "读者类型：";
            // 
            // cboReaderType
            // 
            this.cboReaderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReaderType.Items.AddRange(new object[] {
            "本校学生",
            "本校教师",
            "校外人员"});
            this.cboReaderType.Location = new System.Drawing.Point(100, 52);
            this.cboReaderType.Name = "cboReaderType";
            this.cboReaderType.Size = new System.Drawing.Size(150, 32);
            this.cboReaderType.TabIndex = 2;
            // 
            // lblMaxBorrow
            // 
            this.lblMaxBorrow.AutoSize = true;
            this.lblMaxBorrow.Location = new System.Drawing.Point(256, 54);
            this.lblMaxBorrow.Name = "lblMaxBorrow";
            this.lblMaxBorrow.Size = new System.Drawing.Size(118, 24);
            this.lblMaxBorrow.TabIndex = 3;
            this.lblMaxBorrow.Text = "最大借阅数：";
            // 
            // numMaxBorrow
            // 
            this.numMaxBorrow.Location = new System.Drawing.Point(380, 52);
            this.numMaxBorrow.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numMaxBorrow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMaxBorrow.Name = "numMaxBorrow";
            this.numMaxBorrow.Size = new System.Drawing.Size(80, 30);
            this.numMaxBorrow.TabIndex = 4;
            this.numMaxBorrow.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // lblMaxCategory
            // 
            this.lblMaxCategory.AutoSize = true;
            this.lblMaxCategory.Location = new System.Drawing.Point(456, 55);
            this.lblMaxCategory.Name = "lblMaxCategory";
            this.lblMaxCategory.Size = new System.Drawing.Size(118, 24);
            this.lblMaxCategory.TabIndex = 5;
            this.lblMaxCategory.Text = "最大分类数：";
            // 
            // numMaxCategory
            // 
            this.numMaxCategory.Location = new System.Drawing.Point(580, 52);
            this.numMaxCategory.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numMaxCategory.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMaxCategory.Name = "numMaxCategory";
            this.numMaxCategory.Size = new System.Drawing.Size(80, 30);
            this.numMaxCategory.TabIndex = 6;
            this.numMaxCategory.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // lblBorrowDays
            // 
            this.lblBorrowDays.AutoSize = true;
            this.lblBorrowDays.Location = new System.Drawing.Point(7, 99);
            this.lblBorrowDays.Name = "lblBorrowDays";
            this.lblBorrowDays.Size = new System.Drawing.Size(100, 24);
            this.lblBorrowDays.TabIndex = 7;
            this.lblBorrowDays.Text = "借阅天数：";
            // 
            // numBorrowDays
            // 
            this.numBorrowDays.Location = new System.Drawing.Point(113, 95);
            this.numBorrowDays.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.numBorrowDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBorrowDays.Name = "numBorrowDays";
            this.numBorrowDays.Size = new System.Drawing.Size(80, 30);
            this.numBorrowDays.TabIndex = 8;
            this.numBorrowDays.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // lblMaxRenew
            // 
            this.lblMaxRenew.AutoSize = true;
            this.lblMaxRenew.Location = new System.Drawing.Point(192, 101);
            this.lblMaxRenew.Name = "lblMaxRenew";
            this.lblMaxRenew.Size = new System.Drawing.Size(136, 24);
            this.lblMaxRenew.TabIndex = 9;
            this.lblMaxRenew.Text = "最大续借次数：";
            // 
            // numMaxRenew
            // 
            this.numMaxRenew.Location = new System.Drawing.Point(334, 95);
            this.numMaxRenew.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numMaxRenew.Name = "numMaxRenew";
            this.numMaxRenew.Size = new System.Drawing.Size(80, 30);
            this.numMaxRenew.TabIndex = 10;
            this.numMaxRenew.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // lblRenewDays
            // 
            this.lblRenewDays.AutoSize = true;
            this.lblRenewDays.Location = new System.Drawing.Point(414, 99);
            this.lblRenewDays.Name = "lblRenewDays";
            this.lblRenewDays.Size = new System.Drawing.Size(100, 24);
            this.lblRenewDays.TabIndex = 11;
            this.lblRenewDays.Text = "续借天数：";
            // 
            // numRenewDays
            // 
            this.numRenewDays.Location = new System.Drawing.Point(520, 97);
            this.numRenewDays.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numRenewDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRenewDays.Name = "numRenewDays";
            this.numRenewDays.Size = new System.Drawing.Size(80, 30);
            this.numRenewDays.TabIndex = 12;
            this.numRenewDays.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // chkReference
            // 
            this.chkReference.AutoSize = true;
            this.chkReference.Location = new System.Drawing.Point(20, 145);
            this.chkReference.Name = "chkReference";
            this.chkReference.Size = new System.Drawing.Size(144, 28);
            this.chkReference.TabIndex = 13;
            this.chkReference.Text = "允许借工具书";
            // 
            // chkNewBooks
            // 
            this.chkNewBooks.AutoSize = true;
            this.chkNewBooks.Checked = true;
            this.chkNewBooks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNewBooks.Location = new System.Drawing.Point(170, 148);
            this.chkNewBooks.Name = "chkNewBooks";
            this.chkNewBooks.Size = new System.Drawing.Size(126, 28);
            this.chkNewBooks.TabIndex = 14;
            this.chkNewBooks.Text = "允许借新书";
            // 
            // chkHotBooks
            // 
            this.chkHotBooks.AutoSize = true;
            this.chkHotBooks.Checked = true;
            this.chkHotBooks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHotBooks.Location = new System.Drawing.Point(316, 145);
            this.chkHotBooks.Name = "chkHotBooks";
            this.chkHotBooks.Size = new System.Drawing.Size(144, 28);
            this.chkHotBooks.TabIndex = 15;
            this.chkHotBooks.Text = "允许借热门书";
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(20, 185);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(100, 24);
            this.lblRemark.TabIndex = 16;
            this.lblRemark.Text = "备注说明：";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(126, 182);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(500, 60);
            this.txtRemark.TabIndex = 17;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(450, 255);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 36);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(590, 255);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 36);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // BorrowRuleManagementControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvRules);
            this.Controls.Add(this.panelEdit);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Name = "BorrowRuleManagementControl";
            this.Size = new System.Drawing.Size(1200, 670);
            this.Load += new System.EventHandler(this.BorrowRuleManagementControl_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRules)).EndInit();
            this.panelEdit.ResumeLayout(false);
            this.panelEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxBorrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBorrowDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxRenew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRenewDays)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvRules;
        private System.Windows.Forms.Panel panelEdit;
        private System.Windows.Forms.Label lblEditTitle;
        private System.Windows.Forms.Label lblReaderType;
        private System.Windows.Forms.ComboBox cboReaderType;
        private System.Windows.Forms.Label lblMaxBorrow;
        private System.Windows.Forms.NumericUpDown numMaxBorrow;
        private System.Windows.Forms.Label lblMaxCategory;
        private System.Windows.Forms.NumericUpDown numMaxCategory;
        private System.Windows.Forms.Label lblBorrowDays;
        private System.Windows.Forms.NumericUpDown numBorrowDays;
        private System.Windows.Forms.Label lblMaxRenew;
        private System.Windows.Forms.NumericUpDown numMaxRenew;
        private System.Windows.Forms.Label lblRenewDays;
        private System.Windows.Forms.NumericUpDown numRenewDays;
        private System.Windows.Forms.CheckBox chkReference;
        private System.Windows.Forms.CheckBox chkNewBooks;
        private System.Windows.Forms.CheckBox chkHotBooks;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        private void BorrowRuleManagementControl_Load(object sender, EventArgs e)
        {
            LoadRules();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadRules();
        }

        private void LoadRules()
        {
            try
            {
                string sql = @"
                    SELECT 
                        reader_type AS 读者类型,
                        max_borrow_count AS 最大借阅数,
                        max_category_count AS 最大分类数,
                        borrow_days AS 借阅天数,
                        max_renew_count AS 最大续借次数,
                        renew_days AS 续借天数,
                        CASE WHEN allow_reference_books = 1 THEN N'是' ELSE N'否' END AS 可借工具书,
                        CASE WHEN allow_new_books = 1 THEN N'是' ELSE N'否' END AS 可借新书,
                        CASE WHEN allow_hot_books = 1 THEN N'是' ELSE N'否' END AS 可借热门书,
                        CASE WHEN is_active = 1 THEN N'启用' ELSE N'停用' END AS 状态,
                        remark AS 备注
                    FROM BORROW_RULES
                    ORDER BY 
                        CASE reader_type 
                            WHEN N'本校教师' THEN 1 
                            WHEN N'本校学生' THEN 2 
                            WHEN N'校外人员' THEN 3 
                        END";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql);
                dgvRules.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载规则失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvRules_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRules.SelectedRows.Count == 0) return;

            DataGridViewRow row = dgvRules.SelectedRows[0];
            currentReaderType = row.Cells["读者类型"].Value?.ToString();

            if (string.IsNullOrEmpty(currentReaderType)) return;

            try
            {
                string sql = @"
                    SELECT * FROM BORROW_RULES 
                    WHERE reader_type = @readerType";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@readerType", currentReaderType));

                if (dt.Rows.Count > 0)
                {
                    DataRow dataRow = dt.Rows[0];
                    
                    cboReaderType.SelectedItem = currentReaderType;
                    numMaxBorrow.Value = Convert.ToInt32(dataRow["max_borrow_count"]);
                    numMaxCategory.Value = Convert.ToInt32(dataRow["max_category_count"]);
                    numBorrowDays.Value = Convert.ToInt32(dataRow["borrow_days"]);
                    numMaxRenew.Value = Convert.ToInt32(dataRow["max_renew_count"]);
                    numRenewDays.Value = Convert.ToInt32(dataRow["renew_days"]);
                    chkReference.Checked = Convert.ToBoolean(dataRow["allow_reference_books"]);
                    chkNewBooks.Checked = Convert.ToBoolean(dataRow["allow_new_books"]);
                    chkHotBooks.Checked = Convert.ToBoolean(dataRow["allow_hot_books"]);
                    txtRemark.Text = dataRow["remark"]?.ToString() ?? "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载规则详情失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cboReaderType.SelectedIndex < 0)
            {
                MessageBox.Show("请选择读者类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string readerType = cboReaderType.SelectedItem.ToString();

            string confirmMsg = $"确认保存【{readerType}】的借阅规则？\n\n";
            confirmMsg += $"最大借阅数：{numMaxBorrow.Value}本\n";
            confirmMsg += $"最大分类数：{numMaxCategory.Value}个\n";
            confirmMsg += $"借阅天数：{numBorrowDays.Value}天\n";
            confirmMsg += $"最大续借次数：{numMaxRenew.Value}次\n";
            confirmMsg += $"续借天数：{numRenewDays.Value}天\n";
            confirmMsg += $"工具书：{(chkReference.Checked ? "允许" : "不允许")}\n";
            confirmMsg += $"新书：{(chkNewBooks.Checked ? "允许" : "不允许")}\n";
            confirmMsg += $"热门书：{(chkHotBooks.Checked ? "允许" : "不允许")}";

            if (MessageBox.Show(confirmMsg, "确认保存", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                string sql = @"
                    UPDATE BORROW_RULES
                    SET max_borrow_count = @maxBorrow,
                        max_category_count = @maxCategory,
                        borrow_days = @borrowDays,
                        max_renew_count = @maxRenew,
                        renew_days = @renewDays,
                        allow_reference_books = @allowRef,
                        allow_new_books = @allowNew,
                        allow_hot_books = @allowHot,
                        remark = @remark,
                        updated_time = GETDATE()
                    WHERE reader_type = @readerType";

                DatabaseHelper.ExecuteNonQuery(sql,
                    DatabaseHelper.CreateParameter("@maxBorrow", (int)numMaxBorrow.Value),
                    DatabaseHelper.CreateParameter("@maxCategory", (int)numMaxCategory.Value),
                    DatabaseHelper.CreateParameter("@borrowDays", (int)numBorrowDays.Value),
                    DatabaseHelper.CreateParameter("@maxRenew", (int)numMaxRenew.Value),
                    DatabaseHelper.CreateParameter("@renewDays", (int)numRenewDays.Value),
                    DatabaseHelper.CreateParameter("@allowRef", chkReference.Checked),
                    DatabaseHelper.CreateParameter("@allowNew", chkNewBooks.Checked),
                    DatabaseHelper.CreateParameter("@allowHot", chkHotBooks.Checked),
                    DatabaseHelper.CreateParameter("@remark", txtRemark.Text.Trim()),
                    DatabaseHelper.CreateParameter("@readerType", readerType));

                MessageBox.Show("保存成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // 清除业务规则缓存，使新规则立即生效
                BorrowRules.ClearCache();
                
                LoadRules();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (dgvRules.SelectedRows.Count > 0)
            {
                dgvRules_SelectionChanged(null, null);
            }
        }
    }
}
