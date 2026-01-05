using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Utils;

namespace BooksManagermentSysytem.Controls.Reports
{
    /// <summary>
    /// 流通统计分析控件
    /// 提供图书流通情况的统计分析,包括借阅趋势、热门图书等
    /// </summary>
    public partial class CirculationStatisticsControl : UserControl
    {
        private DataTable statisticsData;

        public CirculationStatisticsControl()
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
            this.lblStatType = new System.Windows.Forms.Label();
            this.cboStatType = new System.Windows.Forms.ComboBox();
            this.lblDateRange = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.lblTopN = new System.Windows.Forms.Label();
            this.numTopN = new System.Windows.Forms.NumericUpDown();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.dgvStatistics = new System.Windows.Forms.DataGridView();
            this.panelStats = new System.Windows.Forms.Panel();
            this.lblStats = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTopN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatistics)).BeginInit();
            this.panelStats.SuspendLayout();
            this.SuspendLayout();
            
            // panelHeader
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(33, 150, 243);
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
            this.lblTitle.Text = "📈 流通统计分析";
            
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
            this.panelQuery.Controls.Add(this.btnAnalyze);
            this.panelQuery.Controls.Add(this.numTopN);
            this.panelQuery.Controls.Add(this.lblTopN);
            this.panelQuery.Controls.Add(this.dtpEnd);
            this.panelQuery.Controls.Add(this.lblTo);
            this.panelQuery.Controls.Add(this.dtpStart);
            this.panelQuery.Controls.Add(this.lblDateRange);
            this.panelQuery.Controls.Add(this.cboStatType);
            this.panelQuery.Controls.Add(this.lblStatType);
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
            this.lblQueryTitle.Text = "分析设置";
            
            // lblStatType
            this.lblStatType.AutoSize = true;
            this.lblStatType.Location = new System.Drawing.Point(20, 47);
            this.lblStatType.Text = "统计类型：";
            
            // cboStatType
            this.cboStatType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatType.Items.AddRange(new object[] { 
                "借阅趋势（按月）", 
                "借阅趋势（按周）", 
                "热门图书TOP N", 
                "热门分类TOP N",
                "读者类型分布",
                "部门借阅排名"
            });
            this.cboStatType.Location = new System.Drawing.Point(100, 44);
            this.cboStatType.Name = "cboStatType";
            this.cboStatType.Size = new System.Drawing.Size(150, 25);
            this.cboStatType.SelectedIndex = 0;
            this.cboStatType.SelectedIndexChanged += new System.EventHandler(this.cboStatType_SelectedIndexChanged);
            
            // lblDateRange
            this.lblDateRange.AutoSize = true;
            this.lblDateRange.Location = new System.Drawing.Point(270, 47);
            this.lblDateRange.Text = "时间范围：";
            
            // dtpStart
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(345, 44);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(120, 25);
            
            // lblTo
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(470, 47);
            this.lblTo.Text = "至";
            
            // dtpEnd
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(490, 44);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(120, 25);
            
            // lblTopN
            this.lblTopN.AutoSize = true;
            this.lblTopN.Location = new System.Drawing.Point(630, 47);
            this.lblTopN.Text = "显示前：";
            
            // numTopN
            this.numTopN.Location = new System.Drawing.Point(690, 45);
            this.numTopN.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.numTopN.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numTopN.Name = "numTopN";
            this.numTopN.Size = new System.Drawing.Size(60, 25);
            this.numTopN.Value = new decimal(new int[] { 10, 0, 0, 0 });
            
            // btnAnalyze
            this.btnAnalyze.BackColor = System.Drawing.Color.FromArgb(33, 150, 243);
            this.btnAnalyze.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnalyze.ForeColor = System.Drawing.Color.White;
            this.btnAnalyze.Location = new System.Drawing.Point(770, 42);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(100, 30);
            this.btnAnalyze.Text = "分析";
            this.btnAnalyze.UseVisualStyleBackColor = false;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            
            // dgvStatistics
            this.dgvStatistics.AllowUserToAddRows = false;
            this.dgvStatistics.AllowUserToDeleteRows = false;
            this.dgvStatistics.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStatistics.BackgroundColor = System.Drawing.Color.White;
            this.dgvStatistics.ColumnHeadersHeight = 40;
            this.dgvStatistics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStatistics.Location = new System.Drawing.Point(0, 140);
            this.dgvStatistics.Name = "dgvStatistics";
            this.dgvStatistics.ReadOnly = true;
            this.dgvStatistics.RowHeadersVisible = false;
            this.dgvStatistics.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStatistics.Size = new System.Drawing.Size(1200, 500);
            
            // panelStats
            this.panelStats.BackColor = System.Drawing.Color.FromArgb(227, 242, 253);
            this.panelStats.Controls.Add(this.lblStats);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStats.Location = new System.Drawing.Point(0, 640);
            this.panelStats.Name = "panelStats";
            this.panelStats.Size = new System.Drawing.Size(1200, 50);
            
            // lblStats
            this.lblStats.AutoSize = true;
            this.lblStats.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStats.ForeColor = System.Drawing.Color.FromArgb(1, 87, 155);
            this.lblStats.Location = new System.Drawing.Point(20, 18);
            this.lblStats.Text = "分析结果：";
            
            // CirculationStatisticsControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvStatistics);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.panelQuery);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Name = "CirculationStatisticsControl";
            this.Size = new System.Drawing.Size(1200, 690);
            this.Load += new System.EventHandler(this.CirculationStatisticsControl_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelQuery.ResumeLayout(false);
            this.panelQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTopN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatistics)).EndInit();
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
        private System.Windows.Forms.Label lblStatType;
        private System.Windows.Forms.ComboBox cboStatType;
        private System.Windows.Forms.Label lblDateRange;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label lblTopN;
        private System.Windows.Forms.NumericUpDown numTopN;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.DataGridView dgvStatistics;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Label lblStats;

        private void CirculationStatisticsControl_Load(object sender, EventArgs e)
        {
            dtpStart.Value = DateTime.Now.AddMonths(-6);
            dtpEnd.Value = DateTime.Now;
            UpdateTopNVisibility();
            LoadStatistics();
        }

        private void cboStatType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTopNVisibility();
        }

        private void UpdateTopNVisibility()
        {
            // 只有TOP N类型的统计才显示TOP N选择器
            bool showTopN = cboStatType.SelectedIndex >= 2 && cboStatType.SelectedIndex <= 5;
            lblTopN.Visible = showTopN;
            numTopN.Visible = showTopN;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStatistics();
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            LoadStatistics();
        }

        private void LoadStatistics()
        {
            try
            {
                string sql = "";

                switch (cboStatType.SelectedIndex)
                {
                    case 0: // 借阅趋势（按月）
                        sql = @"
                            SELECT 
                                FORMAT(borrowdate, 'yyyy-MM') AS '月份',
                                COUNT(*) AS '借阅次数',
                                COUNT(DISTINCT cardID) AS '借阅人数'
                            FROM bookborrow
                            WHERE borrowdate >= @startDate AND borrowdate <= @endDate
                            GROUP BY FORMAT(borrowdate, 'yyyy-MM')
                            ORDER BY FORMAT(borrowdate, 'yyyy-MM')";
                        break;

                    case 1: // 借阅趋势（按周）
                        sql = @"
                            SELECT 
                                FORMAT(borrowdate, 'yyyy-MM-dd') AS '日期',
                                DATEPART(WEEK, borrowdate) AS '周数',
                                COUNT(*) AS '借阅次数',
                                COUNT(DISTINCT cardID) AS '借阅人数'
                            FROM bookborrow
                            WHERE borrowdate >= @startDate AND borrowdate <= @endDate
                            GROUP BY FORMAT(borrowdate, 'yyyy-MM-dd'), DATEPART(WEEK, borrowdate)
                            ORDER BY FORMAT(borrowdate, 'yyyy-MM-dd')";
                        break;

                    case 2: // 热门图书TOP N
                        sql = string.Format(@"
                            SELECT TOP {0}
                                bib.bibliography_name AS '书名',
                                COUNT(*) AS '借阅次数',
                                COUNT(DISTINCT bb.cardID) AS '借阅人数'
                            FROM bookborrow bb
                            INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                            INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                            WHERE bb.borrowdate >= @startDate AND bb.borrowdate <= @endDate
                            GROUP BY bib.bibliography_name
                            ORDER BY COUNT(*) DESC", numTopN.Value);
                        break;

                    case 3: // 热门分类TOP N
                        sql = string.Format(@"
                            SELECT TOP {0}
                                bc.category_name AS '分类名称',
                                bc.category_code AS '分类号',
                                COUNT(*) AS '借阅次数',
                                COUNT(DISTINCT bb.cardID) AS '借阅人数'
                            FROM bookborrow bb
                            INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                            INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                            INNER JOIN BOOK_CATEGORY bc ON bib.category_id = bc.category_id
                            WHERE bb.borrowdate >= @startDate AND bb.borrowdate <= @endDate
                            GROUP BY bc.category_name, bc.category_code
                            ORDER BY COUNT(*) DESC", numTopN.Value);
                        break;

                    case 4: // 读者类型分布
                        sql = @"
                            SELECT 
                                r.readertype AS '读者类型',
                                COUNT(*) AS '借阅次数',
                                COUNT(DISTINCT bb.cardID) AS '借阅人数'
                            FROM bookborrow bb
                            INNER JOIN reader r ON bb.cardID = r.cardID
                            WHERE bb.borrowdate >= @startDate AND bb.borrowdate <= @endDate
                            GROUP BY r.readertype
                            ORDER BY COUNT(*) DESC";
                        break;

                    case 5: // 部门借阅排名
                        sql = string.Format(@"
                            SELECT TOP {0}
                                r.unit AS '部门/单位',
                                COUNT(*) AS '借阅次数',
                                COUNT(DISTINCT bb.cardID) AS '借阅人数'
                            FROM bookborrow bb
                            INNER JOIN reader r ON bb.cardID = r.cardID
                            WHERE bb.borrowdate >= @startDate AND bb.borrowdate <= @endDate
                            GROUP BY r.unit
                            ORDER BY COUNT(*) DESC", numTopN.Value);
                        break;
                }

                statisticsData = DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@startDate", dtpStart.Value.Date),
                    DatabaseHelper.CreateParameter("@endDate", dtpEnd.Value.Date.AddDays(1).AddSeconds(-1)));

                // 绑定到数据视图
                dgvStatistics.DataSource = statisticsData;

                // 更新统计信息
                int totalCount = 0;
                foreach (DataRow row in statisticsData.Rows)
                {
                    totalCount += Convert.ToInt32(row["借阅次数"]);
                }

                lblStats.Text = string.Format("分析结果：共 {0} 项，总借阅次数 {1} 次",
                    statisticsData.Rows.Count, totalCount);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载统计数据失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (statisticsData == null || statisticsData.Rows.Count == 0)
            {
                MessageBox.Show("请先执行统计分析", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ExportHelper.ExportDataTableToCSV(statisticsData,
                string.Format("流通统计分析_{0}.csv", DateTime.Now.ToString("yyyyMMddHHmmss")),
                string.Format("流通统计分析 - {0}", cboStatType.SelectedItem));
        }
    }
}
