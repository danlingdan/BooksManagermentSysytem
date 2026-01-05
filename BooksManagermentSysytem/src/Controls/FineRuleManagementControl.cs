using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Models;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 处罚规则管理控件
    /// 允许管理员为不同读者类型配置处罚规则
    /// </summary>
    public partial class FineRuleManagementControl : UserControl
    {
        private string currentReaderType;

        public FineRuleManagementControl()
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
            this.lblOverduePriceRate = new System.Windows.Forms.Label();
            this.numOverduePriceRate = new System.Windows.Forms.NumericUpDown();
            this.lblOverdueDayRate = new System.Windows.Forms.Label();
            this.numOverdueDayRate = new System.Windows.Forms.NumericUpDown();
            this.lblLostRate = new System.Windows.Forms.Label();
            this.numLostRate = new System.Windows.Forms.NumericUpDown();
            this.lblDamagedRate = new System.Windows.Forms.Label();
            this.numDamagedRate = new System.Windows.Forms.NumericUpDown();
            this.lblMinorDamagedRate = new System.Windows.Forms.Label();
            this.numMinorDamagedRate = new System.Windows.Forms.NumericUpDown();
            this.lblMaxOverdueFine = new System.Windows.Forms.Label();
            this.numMaxOverdueFine = new System.Windows.Forms.NumericUpDown();
            this.lblFreeDays = new System.Windows.Forms.Label();
            this.numFreeDays = new System.Windows.Forms.NumericUpDown();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblPreview = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRules)).BeginInit();
            this.panelEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOverduePriceRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOverdueDayRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLostRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDamagedRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinorDamagedRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxOverdueFine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFreeDays)).BeginInit();
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
            this.panelTop.TabIndex = 2;
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
            this.lblTitle.Text = "💰 处罚规则管理";
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
            this.dgvRules.Size = new System.Drawing.Size(1160, 200);
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
            this.panelEdit.Controls.Add(this.lblOverduePriceRate);
            this.panelEdit.Controls.Add(this.numOverduePriceRate);
            this.panelEdit.Controls.Add(this.lblOverdueDayRate);
            this.panelEdit.Controls.Add(this.numOverdueDayRate);
            this.panelEdit.Controls.Add(this.lblLostRate);
            this.panelEdit.Controls.Add(this.numLostRate);
            this.panelEdit.Controls.Add(this.lblDamagedRate);
            this.panelEdit.Controls.Add(this.numDamagedRate);
            this.panelEdit.Controls.Add(this.lblMinorDamagedRate);
            this.panelEdit.Controls.Add(this.numMinorDamagedRate);
            this.panelEdit.Controls.Add(this.lblMaxOverdueFine);
            this.panelEdit.Controls.Add(this.numMaxOverdueFine);
            this.panelEdit.Controls.Add(this.lblFreeDays);
            this.panelEdit.Controls.Add(this.numFreeDays);
            this.panelEdit.Controls.Add(this.lblRemark);
            this.panelEdit.Controls.Add(this.txtRemark);
            this.panelEdit.Controls.Add(this.btnSave);
            this.panelEdit.Controls.Add(this.btnCancel);
            this.panelEdit.Controls.Add(this.lblPreview);
            this.panelEdit.Location = new System.Drawing.Point(20, 300);
            this.panelEdit.Name = "panelEdit";
            this.panelEdit.Size = new System.Drawing.Size(1160, 360);
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
            this.lblReaderType.Location = new System.Drawing.Point(20, 55);
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
            this.cboReaderType.Location = new System.Drawing.Point(110, 52);
            this.cboReaderType.Name = "cboReaderType";
            this.cboReaderType.Size = new System.Drawing.Size(140, 32);
            this.cboReaderType.TabIndex = 2;
            // 
            // lblOverduePriceRate
            // 
            this.lblOverduePriceRate.AutoSize = true;
            this.lblOverduePriceRate.Location = new System.Drawing.Point(264, 55);
            this.lblOverduePriceRate.Name = "lblOverduePriceRate";
            this.lblOverduePriceRate.Size = new System.Drawing.Size(116, 24);
            this.lblOverduePriceRate.TabIndex = 3;
            this.lblOverduePriceRate.Text = "逾期书价%：";
            // 
            // numOverduePriceRate
            // 
            this.numOverduePriceRate.DecimalPlaces = 2;
            this.numOverduePriceRate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numOverduePriceRate.Location = new System.Drawing.Point(370, 52);
            this.numOverduePriceRate.Name = "numOverduePriceRate";
            this.numOverduePriceRate.Size = new System.Drawing.Size(80, 30);
            this.numOverduePriceRate.TabIndex = 4;
            this.numOverduePriceRate.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numOverduePriceRate.ValueChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // lblOverdueDayRate
            // 
            this.lblOverdueDayRate.AutoSize = true;
            this.lblOverdueDayRate.Location = new System.Drawing.Point(456, 55);
            this.lblOverdueDayRate.Name = "lblOverdueDayRate";
            this.lblOverdueDayRate.Size = new System.Drawing.Size(100, 24);
            this.lblOverdueDayRate.TabIndex = 5;
            this.lblOverdueDayRate.Text = "每天罚款：";
            // 
            // numOverdueDayRate
            // 
            this.numOverdueDayRate.DecimalPlaces = 2;
            this.numOverdueDayRate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numOverdueDayRate.Location = new System.Drawing.Point(550, 52);
            this.numOverdueDayRate.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numOverdueDayRate.Name = "numOverdueDayRate";
            this.numOverdueDayRate.Size = new System.Drawing.Size(80, 30);
            this.numOverdueDayRate.TabIndex = 6;
            this.numOverdueDayRate.Value = new decimal(new int[] {
            10,
            0,
            0,
            131072});
            this.numOverdueDayRate.ValueChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // lblLostRate
            // 
            this.lblLostRate.AutoSize = true;
            this.lblLostRate.Location = new System.Drawing.Point(16, 100);
            this.lblLostRate.Name = "lblLostRate";
            this.lblLostRate.Size = new System.Drawing.Size(116, 24);
            this.lblLostRate.TabIndex = 7;
            this.lblLostRate.Text = "丢失赔偿%：";
            // 
            // numLostRate
            // 
            this.numLostRate.DecimalPlaces = 2;
            this.numLostRate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numLostRate.Location = new System.Drawing.Point(110, 97);
            this.numLostRate.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numLostRate.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numLostRate.Name = "numLostRate";
            this.numLostRate.Size = new System.Drawing.Size(80, 30);
            this.numLostRate.TabIndex = 8;
            this.numLostRate.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblDamagedRate
            // 
            this.lblDamagedRate.AutoSize = true;
            this.lblDamagedRate.Location = new System.Drawing.Point(196, 103);
            this.lblDamagedRate.Name = "lblDamagedRate";
            this.lblDamagedRate.Size = new System.Drawing.Size(116, 24);
            this.lblDamagedRate.TabIndex = 9;
            this.lblDamagedRate.Text = "严重破损%：";
            // 
            // numDamagedRate
            // 
            this.numDamagedRate.DecimalPlaces = 2;
            this.numDamagedRate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numDamagedRate.Location = new System.Drawing.Point(300, 97);
            this.numDamagedRate.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numDamagedRate.Name = "numDamagedRate";
            this.numDamagedRate.Size = new System.Drawing.Size(80, 30);
            this.numDamagedRate.TabIndex = 10;
            this.numDamagedRate.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // lblMinorDamagedRate
            // 
            this.lblMinorDamagedRate.AutoSize = true;
            this.lblMinorDamagedRate.Location = new System.Drawing.Point(386, 99);
            this.lblMinorDamagedRate.Name = "lblMinorDamagedRate";
            this.lblMinorDamagedRate.Size = new System.Drawing.Size(116, 24);
            this.lblMinorDamagedRate.TabIndex = 11;
            this.lblMinorDamagedRate.Text = "轻微破损%：";
            // 
            // numMinorDamagedRate
            // 
            this.numMinorDamagedRate.DecimalPlaces = 2;
            this.numMinorDamagedRate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numMinorDamagedRate.Location = new System.Drawing.Point(490, 97);
            this.numMinorDamagedRate.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numMinorDamagedRate.Name = "numMinorDamagedRate";
            this.numMinorDamagedRate.Size = new System.Drawing.Size(80, 30);
            this.numMinorDamagedRate.TabIndex = 12;
            this.numMinorDamagedRate.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // lblMaxOverdueFine
            // 
            this.lblMaxOverdueFine.AutoSize = true;
            this.lblMaxOverdueFine.Location = new System.Drawing.Point(3, 142);
            this.lblMaxOverdueFine.Name = "lblMaxOverdueFine";
            this.lblMaxOverdueFine.Size = new System.Drawing.Size(136, 24);
            this.lblMaxOverdueFine.TabIndex = 13;
            this.lblMaxOverdueFine.Text = "最大逾期罚款：";
            // 
            // numMaxOverdueFine
            // 
            this.numMaxOverdueFine.DecimalPlaces = 2;
            this.numMaxOverdueFine.Location = new System.Drawing.Point(145, 140);
            this.numMaxOverdueFine.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMaxOverdueFine.Name = "numMaxOverdueFine";
            this.numMaxOverdueFine.Size = new System.Drawing.Size(100, 30);
            this.numMaxOverdueFine.TabIndex = 14;
            this.numMaxOverdueFine.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // lblFreeDays
            // 
            this.lblFreeDays.AutoSize = true;
            this.lblFreeDays.Location = new System.Drawing.Point(254, 142);
            this.lblFreeDays.Name = "lblFreeDays";
            this.lblFreeDays.Size = new System.Drawing.Size(100, 24);
            this.lblFreeDays.TabIndex = 15;
            this.lblFreeDays.Text = "宽限天数：";
            // 
            // numFreeDays
            // 
            this.numFreeDays.Location = new System.Drawing.Point(351, 142);
            this.numFreeDays.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numFreeDays.Name = "numFreeDays";
            this.numFreeDays.Size = new System.Drawing.Size(80, 30);
            this.numFreeDays.TabIndex = 16;
            this.numFreeDays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFreeDays.ValueChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(20, 185);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(100, 24);
            this.lblRemark.TabIndex = 17;
            this.lblRemark.Text = "备注说明：";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(110, 182);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(490, 60);
            this.txtRemark.TabIndex = 18;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(400, 310);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 36);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(540, 310);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 36);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblPreview
            // 
            this.lblPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(220)))));
            this.lblPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPreview.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblPreview.Location = new System.Drawing.Point(640, 52);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Padding = new System.Windows.Forms.Padding(10);
            this.lblPreview.Size = new System.Drawing.Size(500, 190);
            this.lblPreview.TabIndex = 21;
            this.lblPreview.Text = "罚款预览计算";
            // 
            // FineRuleManagementControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvRules);
            this.Controls.Add(this.panelEdit);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Name = "FineRuleManagementControl";
            this.Size = new System.Drawing.Size(1200, 680);
            this.Load += new System.EventHandler(this.FineRuleManagementControl_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRules)).EndInit();
            this.panelEdit.ResumeLayout(false);
            this.panelEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOverduePriceRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOverdueDayRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLostRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDamagedRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinorDamagedRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxOverdueFine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFreeDays)).EndInit();
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
        private System.Windows.Forms.Label lblOverduePriceRate;
        private System.Windows.Forms.NumericUpDown numOverduePriceRate;
        private System.Windows.Forms.Label lblOverdueDayRate;
        private System.Windows.Forms.NumericUpDown numOverdueDayRate;
        private System.Windows.Forms.Label lblLostRate;
        private System.Windows.Forms.NumericUpDown numLostRate;
        private System.Windows.Forms.Label lblDamagedRate;
        private System.Windows.Forms.NumericUpDown numDamagedRate;
        private System.Windows.Forms.Label lblMinorDamagedRate;
        private System.Windows.Forms.NumericUpDown numMinorDamagedRate;
        private System.Windows.Forms.Label lblMaxOverdueFine;
        private System.Windows.Forms.NumericUpDown numMaxOverdueFine;
        private System.Windows.Forms.Label lblFreeDays;
        private System.Windows.Forms.NumericUpDown numFreeDays;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        private void FineRuleManagementControl_Load(object sender, EventArgs e)
        {
            LoadRules();
            UpdatePreview(null, null);
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
                        CAST(overdue_price_rate * 100 AS DECIMAL(5,2)) AS '逾期书价%',
                        overdue_day_rate AS 每天罚款,
                        CAST(lost_rate * 100 AS DECIMAL(5,2)) AS '丢失赔偿%',
                        CAST(damaged_rate * 100 AS DECIMAL(5,2)) AS '严重破损%',
                        CAST(minor_damaged_rate * 100 AS DECIMAL(5,2)) AS '轻微破损%',
                        max_overdue_fine AS 最大逾期罚款,
                        free_overdue_days AS 宽限天数,
                        CASE WHEN is_active = 1 THEN N'启用' ELSE N'停用' END AS 状态,
                        remark AS 备注
                    FROM FINE_RULES
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
                string sql = "SELECT * FROM FINE_RULES WHERE reader_type = @readerType";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@readerType", currentReaderType));

                if (dt.Rows.Count > 0)
                {
                    DataRow dataRow = dt.Rows[0];
                    
                    cboReaderType.SelectedItem = currentReaderType;
                    numOverduePriceRate.Value = Convert.ToDecimal(dataRow["overdue_price_rate"]) * 100;
                    numOverdueDayRate.Value = Convert.ToDecimal(dataRow["overdue_day_rate"]);
                    numLostRate.Value = Convert.ToDecimal(dataRow["lost_rate"]) * 100;
                    numDamagedRate.Value = Convert.ToDecimal(dataRow["damaged_rate"]) * 100;
                    numMinorDamagedRate.Value = Convert.ToDecimal(dataRow["minor_damaged_rate"]) * 100;
                    numMaxOverdueFine.Value = dataRow["max_overdue_fine"] != DBNull.Value ? 
                        Convert.ToDecimal(dataRow["max_overdue_fine"]) : 0;
                    numFreeDays.Value = Convert.ToInt32(dataRow["free_overdue_days"]);
                    txtRemark.Text = dataRow["remark"]?.ToString() ?? "";
                    
                    UpdatePreview(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载规则详情失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePreview(object sender, EventArgs e)
        {
            decimal bookPrice = 50m; 
            int overdueDays = 5;
            
            decimal overdueFine = bookPrice * (numOverduePriceRate.Value / 100) + 
                                 (overdueDays - (int)numFreeDays.Value) * numOverdueDayRate.Value;
            if (overdueFine < 0) overdueFine = 0;
            
            decimal minorDamagedFine = bookPrice * (numMinorDamagedRate.Value / 100);
            decimal damagedFine = bookPrice * (numDamagedRate.Value / 100);
            decimal lostFine = bookPrice * (numLostRate.Value / 100);
            
            string preview = $"💡 罚款预览（示例：书价¥{bookPrice:F2}）\n\n";
            preview += $"• 逾期{overdueDays}天：¥{overdueFine:F2}\n";
            preview += $"  （书价×{numOverduePriceRate.Value}% + ({overdueDays}-{numFreeDays.Value})天×¥{numOverdueDayRate.Value}）\n\n";
            preview += $"• 轻微破损：¥{minorDamagedFine:F2}（书价×{numMinorDamagedRate.Value}%）\n\n";
            preview += $"• 严重破损：¥{damagedFine:F2}（书价×{numDamagedRate.Value}%）\n\n";
            preview += $"• 丢失：¥{lostFine:F2}（书价×{numLostRate.Value}%）";
            
            lblPreview.Text = preview;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cboReaderType.SelectedIndex < 0)
            {
                MessageBox.Show("请选择读者类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string readerType = cboReaderType.SelectedItem.ToString();

            string confirmMsg = $"确认保存【{readerType}】的处罚规则？\n\n";
            confirmMsg += $"逾期：书价×{numOverduePriceRate.Value}% + 每天¥{numOverdueDayRate.Value}\n";
            confirmMsg += $"丢失：书价×{numLostRate.Value}%\n";
            confirmMsg += $"严重破损：书价×{numDamagedRate.Value}%\n";
            confirmMsg += $"轻微破损：书价×{numMinorDamagedRate.Value}%\n";
            confirmMsg += $"宽限天数：{numFreeDays.Value}天\n";
            confirmMsg += $"最大逾期罚款：¥{numMaxOverdueFine.Value}";

            if (MessageBox.Show(confirmMsg, "确认保存", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                string sql = @"
                    UPDATE FINE_RULES
                    SET overdue_price_rate = @overduePriceRate,
                        overdue_day_rate = @overdueDayRate,
                        lost_rate = @lostRate,
                        damaged_rate = @damagedRate,
                        minor_damaged_rate = @minorDamagedRate,
                        max_overdue_fine = @maxOverdueFine,
                        free_overdue_days = @freeDays,
                        remark = @remark,
                        updated_time = GETDATE()
                    WHERE reader_type = @readerType";

                DatabaseHelper.ExecuteNonQuery(sql,
                    DatabaseHelper.CreateParameter("@overduePriceRate", numOverduePriceRate.Value / 100),
                    DatabaseHelper.CreateParameter("@overdueDayRate", numOverdueDayRate.Value),
                    DatabaseHelper.CreateParameter("@lostRate", numLostRate.Value / 100),
                    DatabaseHelper.CreateParameter("@damagedRate", numDamagedRate.Value / 100),
                    DatabaseHelper.CreateParameter("@minorDamagedRate", numMinorDamagedRate.Value / 100),
                    DatabaseHelper.CreateParameter("@maxOverdueFine", numMaxOverdueFine.Value > 0 ? (object)numMaxOverdueFine.Value : DBNull.Value),
                    DatabaseHelper.CreateParameter("@freeDays", (int)numFreeDays.Value),
                    DatabaseHelper.CreateParameter("@remark", txtRemark.Text.Trim()),
                    DatabaseHelper.CreateParameter("@readerType", readerType));

                MessageBox.Show("保存成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // 清除业务规则缓存，使新规则立即生效
                FineCalculator.ClearCache();
                
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
