using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Utils;

namespace BooksManagermentSysytem.Controls.Reports
{
    /// <summary>
    /// 罚款统计报表控件
    /// 提供罚款的统计分析功能,包括按时间、部门、读者类型等维度的统计
    /// </summary>
    public partial class FineReportControl : UserControl
    {
        private DataTable summaryData;
        private DataTable detailData;

        public FineReportControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.panelQuery = new System.Windows.Forms.Panel();
            this.lblQueryTitle = new System.Windows.Forms.Label();
            this.lblDateRange = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.lblGroupBy = new System.Windows.Forms.Label();
            this.cboGroupBy = new System.Windows.Forms.ComboBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.lblSummaryTitle = new System.Windows.Forms.Label();
            this.dgvSummary = new System.Windows.Forms.DataGridView();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.lblDetailTitle = new System.Windows.Forms.Label();
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.panelStats = new System.Windows.Forms.Panel();
            this.lblStats = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panelSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSummary)).BeginInit();
            this.panelDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.panelStats.SuspendLayout();
            this.SuspendLayout();
            
            // panelHeader
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(156, 39, 176);
            this.panelHeader.Controls.Add(this.btnExport);
            this.panelHeader.Controls.Add(this.btnRefresh);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1200, 60);
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 18);
            this.lblTitle.Text = "💰 罚款统计报表";
            
            // btnRefresh
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(960, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            
            // btnExport
            this.btnExport.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnExport.BackColor = System.Drawing.Color.White;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Location = new System.Drawing.Point(1070, 15);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 30);
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            
            // panelQuery
            this.panelQuery.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.panelQuery.Controls.Add(this.btnQuery);
            this.panelQuery.Controls.Add(this.cboGroupBy);
            this.panelQuery.Controls.Add(this.lblGroupBy);
            this.panelQuery.Controls.Add(this.dtpEnd);
            this.panelQuery.Controls.Add(this.lblTo);
            this.panelQuery.Controls.Add(this.dtpStart);
            this.panelQuery.Controls.Add(this.lblDateRange);
            this.panelQuery.Controls.Add(this.lblQueryTitle);
            this.panelQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelQuery.Location = new System.Drawing.Point(0, 60);
            this.panelQuery.Name = "panelQuery";
            this.panelQuery.Padding = new System.Windows.Forms.Padding(20);
            this.panelQuery.Size = new System.Drawing.Size(1200, 80);
            
            // lblQueryTitle
            this.lblQueryTitle.AutoSize = true;
            this.lblQueryTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblQueryTitle.Location = new System.Drawing.Point(20, 15);
            this.lblQueryTitle.Text = "统计条件";
            
            // lblDateRange
            this.lblDateRange.AutoSize = true;
            this.lblDateRange.Location = new System.Drawing.Point(20, 47);
            this.lblDateRange.Text = "日期范围：";
            
            // dtpStart
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(100, 44);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(120, 25);
            
            // lblTo
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(225, 47);
            this.lblTo.Text = "至";
            
            // dtpEnd
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(245, 44);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(120, 25);
            
            // lblGroupBy
            this.lblGroupBy.AutoSize = true;
            this.lblGroupBy.Location = new System.Drawing.Point(385, 47);
            this.lblGroupBy.Text = "分组统计：";
            
            // cboGroupBy
            this.cboGroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGroupBy.Items.AddRange(new object[] { "按部门", "按读者类型", "按罚款原因", "按月份" });
            this.cboGroupBy.Location = new System.Drawing.Point(465, 44);
            this.cboGroupBy.Name = "cboGroupBy";
            this.cboGroupBy.Size = new System.Drawing.Size(150, 25);
            this.cboGroupBy.SelectedIndex = 0;
            
            // btnQuery
            this.btnQuery.BackColor = System.Drawing.Color.FromArgb(156, 39, 176);
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.ForeColor = System.Drawing.Color.White;
            this.btnQuery.Location = new System.Drawing.Point(635, 42);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(100, 30);
            this.btnQuery.Text = "统计";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            
            // splitContainer
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 140);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer.Panel1.Controls.Add(this.panelSummary);
            this.splitContainer.Panel2.Controls.Add(this.panelDetail);
            this.splitContainer.Size = new System.Drawing.Size(1200, 500);
            this.splitContainer.SplitterDistance = 220;
            
            // panelSummary
            this.panelSummary.Controls.Add(this.dgvSummary);
            this.panelSummary.Controls.Add(this.lblSummaryTitle);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSummary.Location = new System.Drawing.Point(0, 0);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Padding = new System.Windows.Forms.Padding(10);
            this.panelSummary.Size = new System.Drawing.Size(1200, 220);
            
            // lblSummaryTitle
            this.lblSummaryTitle.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.lblSummaryTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSummaryTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSummaryTitle.Location = new System.Drawing.Point(10, 10);
            this.lblSummaryTitle.Name = "lblSummaryTitle";
            this.lblSummaryTitle.Padding = new System.Windows.Forms.Padding(10, 8, 0, 8);
            this.lblSummaryTitle.Size = new System.Drawing.Size(1180, 35);
            this.lblSummaryTitle.Text = "📊 统计汇总";
            
            // dgvSummary
            this.dgvSummary.AllowUserToAddRows = false;
            this.dgvSummary.AllowUserToDeleteRows = false;
            this.dgvSummary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSummary.BackgroundColor = System.Drawing.Color.White;
            this.dgvSummary.ColumnHeadersHeight = 40;
            this.dgvSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSummary.Location = new System.Drawing.Point(10, 45);
            this.dgvSummary.Name = "dgvSummary";
            this.dgvSummary.ReadOnly = true;
            this.dgvSummary.RowHeadersVisible = false;
            this.dgvSummary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSummary.SelectionChanged += new System.EventHandler(this.dgvSummary_SelectionChanged);
            
            // panelDetail
            this.panelDetail.Controls.Add(this.dgvDetail);
            this.panelDetail.Controls.Add(this.lblDetailTitle);
            this.panelDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetail.Location = new System.Drawing.Point(0, 0);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Padding = new System.Windows.Forms.Padding(10);
            this.panelDetail.Size = new System.Drawing.Size(1200, 276);
            
            // lblDetailTitle
            this.lblDetailTitle.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.lblDetailTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetailTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetailTitle.Location = new System.Drawing.Point(10, 10);
            this.lblDetailTitle.Name = "lblDetailTitle";
            this.lblDetailTitle.Padding = new System.Windows.Forms.Padding(10, 8, 0, 8);
            this.lblDetailTitle.Size = new System.Drawing.Size(1180, 35);
            this.lblDetailTitle.Text = "📋 罚款明细（选择类别查看详情）";
            
            // dgvDetail
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.AllowUserToDeleteRows = false;
            this.dgvDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetail.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetail.ColumnHeadersHeight = 40;
            this.dgvDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetail.Location = new System.Drawing.Point(10, 45);
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.ReadOnly = true;
            this.dgvDetail.RowHeadersVisible = false;
            this.dgvDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            
            // panelStats
            this.panelStats.BackColor = System.Drawing.Color.FromArgb(243, 229, 245);
            this.panelStats.Controls.Add(this.lblStats);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStats.Location = new System.Drawing.Point(0, 640);
            this.panelStats.Name = "panelStats";
            this.panelStats.Size = new System.Drawing.Size(1200, 50);
            
            // lblStats
            this.lblStats.AutoSize = true;
            this.lblStats.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStats.ForeColor = System.Drawing.Color.FromArgb(74, 20, 140);
            this.lblStats.Location = new System.Drawing.Point(20, 18);
            this.lblStats.Text = "统计信息：";
            
            // FineReportControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.panelQuery);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Name = "FineReportControl";
            this.Size = new System.Drawing.Size(1200, 690);
            this.Load += new System.EventHandler(this.FineReportControl_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelQuery.ResumeLayout(false);
            this.panelQuery.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panelSummary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSummary)).EndInit();
            this.panelDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.panelStats.ResumeLayout(false);
            this.panelStats.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Panel panelQuery;
        private System.Windows.Forms.Label lblQueryTitle;
        private System.Windows.Forms.Label lblDateRange;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label lblGroupBy;
        private System.Windows.Forms.ComboBox cboGroupBy;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.Label lblSummaryTitle;
        private System.Windows.Forms.DataGridView dgvSummary;
        private System.Windows.Forms.Panel panelDetail;
        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.DataGridView dgvDetail;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Label lblStats;

        private void FineReportControl_Load(object sender, EventArgs e)
        {
            dtpStart.Value = DateTime.Now.AddMonths(-6);
            dtpEnd.Value = DateTime.Now;
            LoadSummaryData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSummaryData();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            LoadSummaryData();
        }

        private void LoadSummaryData()
        {
            try
            {
                string sql = "";
                string groupByField = "";
                string groupByLabel = "";

                switch (cboGroupBy.SelectedIndex)
                {
                    case 0: // 按部门
                        groupByField = "r.unit";
                        groupByLabel = "部门/单位";
                        sql = @"
                            SELECT 
                                r.unit AS '部门/单位',
                                COUNT(DISTINCT f.cardID) AS '罚款人数',
                                COUNT(*) AS '罚款笔数',
                                SUM(CASE WHEN f.fine_status = N'未支付' THEN 1 ELSE 0 END) AS '未支付笔数',
                                SUM(CASE WHEN f.fine_status = N'已支付' THEN 1 ELSE 0 END) AS '已支付笔数',
                                SUM(CASE WHEN f.fine_status = N'未支付' THEN f.amount ELSE 0 END) AS '未支付金额',
                                SUM(CASE WHEN f.fine_status = N'已支付' THEN f.amount ELSE 0 END) AS '已支付金额',
                                SUM(f.amount) AS '总金额'
                            FROM fine f
                            INNER JOIN reader r ON f.cardID = r.cardID
                            WHERE f.created_time >= @startDate AND f.created_time <= @endDate
                            GROUP BY r.unit
                            ORDER BY SUM(f.amount) DESC";
                        break;

                    case 1: // 按读者类型
                        groupByField = "r.readertype";
                        groupByLabel = "读者类型";
                        sql = @"
                            SELECT 
                                r.readertype AS '读者类型',
                                COUNT(DISTINCT f.cardID) AS '罚款人数',
                                COUNT(*) AS '罚款笔数',
                                SUM(CASE WHEN f.fine_status = N'未支付' THEN 1 ELSE 0 END) AS '未支付笔数',
                                SUM(CASE WHEN f.fine_status = N'已支付' THEN 1 ELSE 0 END) AS '已支付笔数',
                                SUM(CASE WHEN f.fine_status = N'未支付' THEN f.amount ELSE 0 END) AS '未支付金额',
                                SUM(CASE WHEN f.fine_status = N'已支付' THEN f.amount ELSE 0 END) AS '已支付金额',
                                SUM(f.amount) AS '总金额'
                            FROM fine f
                            INNER JOIN reader r ON f.cardID = r.cardID
                            WHERE f.created_time >= @startDate AND f.created_time <= @endDate
                            GROUP BY r.readertype
                            ORDER BY SUM(f.amount) DESC";
                        break;

                    case 2: // 按罚款原因
                        groupByLabel = "罚款原因";
                        sql = @"
                            SELECT 
                                f.reason AS '罚款原因',
                                COUNT(DISTINCT f.cardID) AS '罚款人数',
                                COUNT(*) AS '罚款笔数',
                                SUM(CASE WHEN f.fine_status = N'未支付' THEN 1 ELSE 0 END) AS '未支付笔数',
                                SUM(CASE WHEN f.fine_status = N'已支付' THEN 1 ELSE 0 END) AS '已支付笔数',
                                SUM(CASE WHEN f.fine_status = N'未支付' THEN f.amount ELSE 0 END) AS '未支付金额',
                                SUM(CASE WHEN f.fine_status = N'已支付' THEN f.amount ELSE 0 END) AS '已支付金额',
                                SUM(f.amount) AS '总金额'
                            FROM fine f
                            WHERE f.created_time >= @startDate AND f.created_time <= @endDate
                            GROUP BY f.reason
                            ORDER BY SUM(f.amount) DESC";
                        break;

                    case 3: // 按月份
                        groupByLabel = "月份";
                        sql = @"
                            SELECT 
                                FORMAT(f.created_time, 'yyyy-MM') AS '月份',
                                COUNT(DISTINCT f.cardID) AS '罚款人数',
                                COUNT(*) AS '罚款笔数',
                                SUM(CASE WHEN f.fine_status = N'未支付' THEN 1 ELSE 0 END) AS '未支付笔数',
                                SUM(CASE WHEN f.fine_status = N'已支付' THEN 1 ELSE 0 END) AS '已支付笔数',
                                SUM(CASE WHEN f.fine_status = N'未支付' THEN f.amount ELSE 0 END) AS '未支付金额',
                                SUM(CASE WHEN f.fine_status = N'已支付' THEN f.amount ELSE 0 END) AS '已支付金额',
                                SUM(f.amount) AS '总金额'
                            FROM fine f
                            WHERE f.created_time >= @startDate AND f.created_time <= @endDate
                            GROUP BY FORMAT(f.created_time, 'yyyy-MM')
                            ORDER BY FORMAT(f.created_time, 'yyyy-MM') DESC";
                        break;
                }

                summaryData = DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@startDate", dtpStart.Value.Date),
                    DatabaseHelper.CreateParameter("@endDate", dtpEnd.Value.Date.AddDays(1).AddSeconds(-1)));

                dgvSummary.DataSource = summaryData;

                // 设置金额列格式
                if (dgvSummary.Columns.Contains("未支付金额"))
                {
                    dgvSummary.Columns["未支付金额"].DefaultCellStyle.Format = "C2";
                }
                if (dgvSummary.Columns.Contains("已支付金额"))
                {
                    dgvSummary.Columns["已支付金额"].DefaultCellStyle.Format = "C2";
                }
                if (dgvSummary.Columns.Contains("总金额"))
                {
                    dgvSummary.Columns["总金额"].DefaultCellStyle.Format = "C2";
                }

                // 计算统计信息
                decimal totalUnpaid = 0;
                decimal totalPaid = 0;
                int totalCount = 0;

                foreach (DataRow row in summaryData.Rows)
                {
                    totalUnpaid += Convert.ToDecimal(row["未支付金额"]);
                    totalPaid += Convert.ToDecimal(row["已支付金额"]);
                    totalCount += Convert.ToInt32(row["罚款笔数"]);
                }

                decimal totalAmount = totalUnpaid + totalPaid;
                lblStats.Text = string.Format("统计信息：共 {0} 笔罚款，总金额 {1:C2}（未支付 {2:C2}，已支付 {3:C2}）",
                    totalCount, totalAmount, totalUnpaid, totalPaid);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载数据失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSummary_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSummary.SelectedRows.Count == 0)
            {
                detailData = null;
                dgvDetail.DataSource = null;
                lblDetailTitle.Text = "📋 罚款明细（选择类别查看详情）";
                return;
            }

            string category = dgvSummary.SelectedRows[0].Cells[0].Value?.ToString();
            if (string.IsNullOrEmpty(category))
                return;

            LoadDetailData(category);
        }

        private void LoadDetailData(string category)
        {
            try
            {
                string sql = "";
                string categoryLabel = "";

                switch (cboGroupBy.SelectedIndex)
                {
                    case 0: // 按部门
                        categoryLabel = category;
                        sql = @"
                            SELECT 
                                f.fine_id AS 'ID',
                                f.cardID AS '借书证号',
                                f.readername AS '读者姓名',
                                r.readertype AS '读者类型',
                                f.reason AS '罚款原因',
                                f.amount AS '金额',
                                f.fine_status AS '状态',
                                f.created_time AS '创建时间'
                            FROM fine f
                            INNER JOIN reader r ON f.cardID = r.cardID
                            WHERE r.unit = @category
                              AND f.created_time >= @startDate 
                              AND f.created_time <= @endDate
                            ORDER BY f.created_time DESC";
                        break;

                    case 1: // 按读者类型
                        categoryLabel = category;
                        sql = @"
                            SELECT 
                                f.fine_id AS 'ID',
                                f.cardID AS '借书证号',
                                f.readername AS '读者姓名',
                                r.unit AS '部门/单位',
                                f.reason AS '罚款原因',
                                f.amount AS '金额',
                                f.fine_status AS '状态',
                                f.created_time AS '创建时间'
                            FROM fine f
                            INNER JOIN reader r ON f.cardID = r.cardID
                            WHERE r.readertype = @category
                              AND f.created_time >= @startDate 
                              AND f.created_time <= @endDate
                            ORDER BY f.created_time DESC";
                        break;

                    case 2: // 按罚款原因
                        categoryLabel = category;
                        sql = @"
                            SELECT 
                                f.fine_id AS 'ID',
                                f.cardID AS '借书证号',
                                f.readername AS '读者姓名',
                                r.readertype AS '读者类型',
                                r.unit AS '部门/单位',
                                f.amount AS '金额',
                                f.fine_status AS '状态',
                                f.created_time AS '创建时间'
                            FROM fine f
                            INNER JOIN reader r ON f.cardID = r.cardID
                            WHERE f.reason = @category
                              AND f.created_time >= @startDate 
                              AND f.created_time <= @endDate
                            ORDER BY f.created_time DESC";
                        break;

                    case 3: // 按月份
                        categoryLabel = category;
                        sql = @"
                            SELECT 
                                f.fine_id AS 'ID',
                                f.cardID AS '借书证号',
                                f.readername AS '读者姓名',
                                r.readertype AS '读者类型',
                                r.unit AS '部门/单位',
                                f.reason AS '罚款原因',
                                f.amount AS '金额',
                                f.fine_status AS '状态',
                                f.created_time AS '创建时间'
                            FROM fine f
                            INNER JOIN reader r ON f.cardID = r.cardID
                            WHERE FORMAT(f.created_time, 'yyyy-MM') = @category
                              AND f.created_time >= @startDate 
                              AND f.created_time <= @endDate
                            ORDER BY f.created_time DESC";
                        break;
                }

                detailData = DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@category", category),
                    DatabaseHelper.CreateParameter("@startDate", dtpStart.Value.Date),
                    DatabaseHelper.CreateParameter("@endDate", dtpEnd.Value.Date.AddDays(1).AddSeconds(-1)));

                dgvDetail.DataSource = detailData;

                // 隐藏ID列
                if (dgvDetail.Columns.Contains("ID"))
                {
                    dgvDetail.Columns["ID"].Visible = false;
                }

                // 设置金额列格式
                if (dgvDetail.Columns.Contains("金额"))
                {
                    dgvDetail.Columns["金额"].DefaultCellStyle.Format = "C2";
                }

                // 设置状态颜色
                dgvDetail.CellFormatting += (s, cellArgs) =>
                {
                    if (dgvDetail.Columns[cellArgs.ColumnIndex].HeaderText == "状态" && cellArgs.Value != null)
                    {
                        if (cellArgs.Value.ToString() == "未支付")
                        {
                            cellArgs.CellStyle.BackColor = Color.FromArgb(255, 235, 235);
                            cellArgs.CellStyle.ForeColor = Color.Red;
                        }
                        else
                        {
                            cellArgs.CellStyle.BackColor = Color.FromArgb(232, 245, 233);
                            cellArgs.CellStyle.ForeColor = Color.Green;
                        }
                    }
                };

                lblDetailTitle.Text = string.Format("📋 {0} - 罚款明细（{1} 笔）", categoryLabel, detailData.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载明细失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (summaryData == null || summaryData.Rows.Count == 0)
            {
                MessageBox.Show("没有数据可导出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ExportHelper.ExportDataTableToCSV(summaryData,
                string.Format("罚款统计报表_{0}.csv", DateTime.Now.ToString("yyyyMMddHHmmss")),
                string.Format("罚款统计报表 - {0}", cboGroupBy.SelectedItem));
        }
    }
}
