using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Utils;

namespace BooksManagermentSysytem.Controls.Reports
{
    /// <summary>
    /// 超期图书汇总控件
    /// 按部门汇总统计超期图书情况,支持打印催还通知单
    /// </summary>
    public partial class OverdueReportControl : UserControl
    {
        private DataTable summaryData;
        private DataTable detailData;

        public OverdueReportControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnPrintNotice = new System.Windows.Forms.Button();
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
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(244, 67, 54);
            this.panelHeader.Controls.Add(this.btnPrintNotice);
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
            this.lblTitle.Text = "⚠️ 超期图书汇总";
            
            // btnRefresh
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(850, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            
            // btnExport
            this.btnExport.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnExport.BackColor = System.Drawing.Color.White;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Location = new System.Drawing.Point(960, 15);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 30);
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            
            // btnPrintNotice
            this.btnPrintNotice.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnPrintNotice.BackColor = System.Drawing.Color.White;
            this.btnPrintNotice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintNotice.Location = new System.Drawing.Point(1070, 15);
            this.btnPrintNotice.Name = "btnPrintNotice";
            this.btnPrintNotice.Size = new System.Drawing.Size(110, 30);
            this.btnPrintNotice.Text = "打印催还单";
            this.btnPrintNotice.UseVisualStyleBackColor = false;
            this.btnPrintNotice.Click += new System.EventHandler(this.btnPrintNotice_Click);
            
            // splitContainer
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 60);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer.Panel1.Controls.Add(this.panelSummary);
            this.splitContainer.Panel2.Controls.Add(this.panelDetail);
            this.splitContainer.Size = new System.Drawing.Size(1200, 580);
            this.splitContainer.SplitterDistance = 250;
            
            // panelSummary
            this.panelSummary.Controls.Add(this.dgvSummary);
            this.panelSummary.Controls.Add(this.lblSummaryTitle);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSummary.Location = new System.Drawing.Point(0, 0);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Padding = new System.Windows.Forms.Padding(10);
            this.panelSummary.Size = new System.Drawing.Size(1200, 250);
            
            // lblSummaryTitle
            this.lblSummaryTitle.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.lblSummaryTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSummaryTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSummaryTitle.Location = new System.Drawing.Point(10, 10);
            this.lblSummaryTitle.Name = "lblSummaryTitle";
            this.lblSummaryTitle.Padding = new System.Windows.Forms.Padding(10, 8, 0, 8);
            this.lblSummaryTitle.Size = new System.Drawing.Size(1180, 35);
            this.lblSummaryTitle.Text = "📊 按部门汇总";
            
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
            this.panelDetail.Size = new System.Drawing.Size(1200, 326);
            
            // lblDetailTitle
            this.lblDetailTitle.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.lblDetailTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetailTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetailTitle.Location = new System.Drawing.Point(10, 10);
            this.lblDetailTitle.Name = "lblDetailTitle";
            this.lblDetailTitle.Padding = new System.Windows.Forms.Padding(10, 8, 0, 8);
            this.lblDetailTitle.Size = new System.Drawing.Size(1180, 35);
            this.lblDetailTitle.Text = "📋 超期明细（选择部门查看详情）";
            
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
            this.panelStats.BackColor = System.Drawing.Color.FromArgb(255, 235, 235);
            this.panelStats.Controls.Add(this.lblStats);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStats.Location = new System.Drawing.Point(0, 640);
            this.panelStats.Name = "panelStats";
            this.panelStats.Size = new System.Drawing.Size(1200, 50);
            
            // lblStats
            this.lblStats.AutoSize = true;
            this.lblStats.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStats.ForeColor = System.Drawing.Color.FromArgb(183, 28, 28);
            this.lblStats.Location = new System.Drawing.Point(20, 18);
            this.lblStats.Text = "统计信息：";
            
            // OverdueReportControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Name = "OverdueReportControl";
            this.Size = new System.Drawing.Size(1200, 690);
            this.Load += new System.EventHandler(this.OverdueReportControl_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
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
        private System.Windows.Forms.Button btnPrintNotice;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.Label lblSummaryTitle;
        private System.Windows.Forms.DataGridView dgvSummary;
        private System.Windows.Forms.Panel panelDetail;
        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.DataGridView dgvDetail;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Label lblStats;

        private void OverdueReportControl_Load(object sender, EventArgs e)
        {
            LoadSummaryData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSummaryData();
        }

        private void LoadSummaryData()
        {
            try
            {
                string sql = @"
                    SELECT 
                        r.unit AS '单位/学院',
                        COUNT(DISTINCT bb.cardID) AS '超期读者数',
                        COUNT(*) AS '超期图书数',
                        SUM(DATEDIFF(DAY, DATEADD(DAY, 7, bb.borrowdate), GETDATE())) AS '总超期天数',
                        MAX(DATEDIFF(DAY, DATEADD(DAY, 7, bb.borrowdate), GETDATE())) AS '最大逾期天数',
                        AVG(DATEDIFF(DAY, DATEADD(DAY, 7, bb.borrowdate), GETDATE())) AS '平均逾期天数'
                    FROM bookborrow bb
                    INNER JOIN reader r ON bb.cardID = r.cardID
                    WHERE bb.overdate IS NULL
                      AND GETDATE() > DATEADD(DAY, 7, bb.borrowdate)
                    GROUP BY r.unit
                    ORDER BY COUNT(*) DESC";

                summaryData = DatabaseHelper.ExecuteQuery(sql);
                dgvSummary.DataSource = summaryData;

                // 设置列格式
                if (dgvSummary.Columns.Contains("平均逾期天数"))
                {
                    dgvSummary.Columns["平均逾期天数"].DefaultCellStyle.Format = "N1";
                }

                // 计算总计
                int totalReaders = 0;
                int totalBooks = 0;
                long totalDays = 0;

                foreach (DataRow row in summaryData.Rows)
                {
                    totalReaders += Convert.ToInt32(row["超期读者数"]);
                    totalBooks += Convert.ToInt32(row["超期图书数"]);
                    totalDays += Convert.ToInt64(row["总超期天数"]);
                }

                lblStats.Text = $"统计信息：共 {summaryData.Rows.Count} 个部门，{totalReaders} 名读者，{totalBooks} 本图书超期，累计 {totalDays} 天";
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
                lblDetailTitle.Text = "📋 超期明细（选择部门查看详情）";
                return;
            }

            string unit = dgvSummary.SelectedRows[0].Cells["单位/学院"].Value?.ToString();
            if (string.IsNullOrEmpty(unit))
                return;

            LoadDetailData(unit);
        }

        private void LoadDetailData(string unit)
        {
            try
            {
                string sql = @"
                    SELECT 
                        bb.cardID AS '借书证号',
                        r.readername AS '读者姓名',
                        r.readertype AS '读者类型',
                        bb.bookID AS '馆藏码',
                        bib.bibliography_name AS '书名',
                        bc.category_code AS '分类号',
                        bb.borrowdate AS '借阅日期',
                        DATEADD(DAY, 7, bb.borrowdate) AS '应还日期',
                        DATEDIFF(DAY, DATEADD(DAY, 7, bb.borrowdate), GETDATE()) AS '逾期天数',
                        bib.price AS '书价',
                        CAST((bib.price * 0.1 + DATEDIFF(DAY, DATEADD(DAY, 7, bb.borrowdate), GETDATE()) * 0.1) AS DECIMAL(10,2)) AS '预计罚款'
                    FROM bookborrow bb
                    INNER JOIN reader r ON bb.cardID = r.cardID
                    INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                    INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                    INNER JOIN BOOK_CATEGORY bc ON bib.category_id = bc.category_id
                    WHERE bb.overdate IS NULL
                      AND GETDATE() > DATEADD(DAY, 7, bb.borrowdate)
                      AND r.unit = @unit
                    ORDER BY DATEDIFF(DAY, DATEADD(DAY, 7, bb.borrowdate), GETDATE()) DESC";

                detailData = DatabaseHelper.ExecuteQuery(sql, 
                    DatabaseHelper.CreateParameter("@unit", unit));
                dgvDetail.DataSource = detailData;

                // 设置逾期天数和罚款列颜色
                dgvDetail.CellFormatting += (s, cellArgs) =>
                {
                    if (dgvDetail.Columns[cellArgs.ColumnIndex].HeaderText == "逾期天数" && cellArgs.Value != null)
                    {
                        int days = Convert.ToInt32(cellArgs.Value);
                        if (days > 30)
                        {
                            cellArgs.CellStyle.BackColor = Color.FromArgb(255, 200, 200);
                            cellArgs.CellStyle.ForeColor = Color.DarkRed;
                            cellArgs.CellStyle.Font = new Font(dgvDetail.Font, FontStyle.Bold);
                        }
                        else if (days > 14)
                        {
                            cellArgs.CellStyle.BackColor = Color.FromArgb(255, 230, 200);
                            cellArgs.CellStyle.ForeColor = Color.OrangeRed;
                        }
                    }
                };

                lblDetailTitle.Text = $"📋 {unit} - 超期明细（{detailData.Rows.Count} 本）";
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

            // 合并汇总和明细数据导出
            DataTable exportData = new DataTable();
            exportData.Columns.Add("类型");
            
            foreach (DataColumn col in summaryData.Columns)
            {
                exportData.Columns.Add(col.ColumnName);
            }

            // 添加汇总数据
            foreach (DataRow row in summaryData.Rows)
            {
                DataRow newRow = exportData.NewRow();
                newRow["类型"] = "汇总";
                for (int i = 0; i < summaryData.Columns.Count; i++)
                {
                    newRow[i + 1] = row[i];
                }
                exportData.Rows.Add(newRow);
            }

            ExportHelper.ExportDataTableToCSV(exportData, 
                $"超期图书汇总_{DateTime.Now:yyyyMMddHHmmss}.csv", 
                "超期图书汇总报表");
        }

        private void btnPrintNotice_Click(object sender, EventArgs e)
        {
            if (dgvSummary.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择一个部门", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (detailData == null || detailData.Rows.Count == 0)
            {
                MessageBox.Show("该部门没有超期图书", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string unit = dgvSummary.SelectedRows[0].Cells["单位/学院"].Value?.ToString();
            int overdueCount = Convert.ToInt32(dgvSummary.SelectedRows[0].Cells["超期图书数"].Value);
            long totalDays = Convert.ToInt64(dgvSummary.SelectedRows[0].Cells["总超期天数"].Value);

            // 生成催还通知单内容
            StringBuilder noticeContent = new StringBuilder();
            noticeContent.AppendLine("═══════════════════════════════════════");
            noticeContent.AppendLine("           图书馆超期图书催还通知单");
            noticeContent.AppendLine("═══════════════════════════════════════");
            noticeContent.AppendLine();
            noticeContent.AppendLine($"单位/学院：{unit}");
            noticeContent.AppendLine($"超期图书数量：{overdueCount} 本");
            noticeContent.AppendLine($"累计超期天数：{totalDays} 天");
            noticeContent.AppendLine($"通知日期：{DateTime.Now:yyyy年MM月dd日}");
            noticeContent.AppendLine();
            noticeContent.AppendLine("───────────────────────────────────────");
            noticeContent.AppendLine("超期图书明细：");
            noticeContent.AppendLine("───────────────────────────────────────");
            noticeContent.AppendLine();

            foreach (DataRow row in detailData.Rows)
            {
                noticeContent.AppendLine($"读者：{row["读者姓名"]} ({row["借书证号"]})");
                noticeContent.AppendLine($"书名：《{row["书名"]}》");
                noticeContent.AppendLine($"借阅日期：{Convert.ToDateTime(row["借阅日期"]):yyyy-MM-dd}");
                noticeContent.AppendLine($"应还日期：{Convert.ToDateTime(row["应还日期"]):yyyy-MM-dd}");
                noticeContent.AppendLine($"逾期天数：{row["逾期天数"]} 天");
                noticeContent.AppendLine($"预计罚款：¥{row["预计罚款"]}");
                noticeContent.AppendLine();
            }

            noticeContent.AppendLine("───────────────────────────────────────");
            noticeContent.AppendLine("请督促以上读者尽快归还超期图书。");
            noticeContent.AppendLine("逾期未还将影响借阅权限并产生罚款。");
            noticeContent.AppendLine();
            noticeContent.AppendLine("如有疑问，请联系图书馆服务台。");
            noticeContent.AppendLine("───────────────────────────────────────");
            noticeContent.AppendLine($"图书馆服务中心");
            noticeContent.AppendLine($"打印时间：{DateTime.Now:yyyy年MM月dd日 HH:mm}");
            noticeContent.AppendLine("═══════════════════════════════════════");

            PrintHelper.PrintText(noticeContent.ToString(), $"{unit} - 超期图书催还通知单");
        }
    }
}
