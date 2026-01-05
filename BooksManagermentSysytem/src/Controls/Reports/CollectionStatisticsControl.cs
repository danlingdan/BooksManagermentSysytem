using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Utils;

namespace BooksManagermentSysytem.Controls.Reports
{
    /// <summary>
    /// 馆藏统计分析控件
    /// 提供馆藏图书的统计分析,包括分类分布、状态统计等
    /// </summary>
    public partial class CollectionStatisticsControl : UserControl
    {
        private DataTable statisticsData;

        public CollectionStatisticsControl()
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
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
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
            this.lblTitle.Text = "📚 馆藏统计分析";
            
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
                "图书状态分布", 
                "分类藏书统计",
                "出版年份分布",
                "价格区间分布",
                "出版社TOP N",
                "作者TOP N"
            });
            this.cboStatType.Location = new System.Drawing.Point(100, 44);
            this.cboStatType.Name = "cboStatType";
            this.cboStatType.Size = new System.Drawing.Size(150, 25);
            this.cboStatType.SelectedIndex = 0;
            this.cboStatType.SelectedIndexChanged += new System.EventHandler(this.cboStatType_SelectedIndexChanged);
            
            // lblTopN
            this.lblTopN.AutoSize = true;
            this.lblTopN.Location = new System.Drawing.Point(270, 47);
            this.lblTopN.Text = "显示前：";
            this.lblTopN.Visible = false;
            
            // numTopN
            this.numTopN.Location = new System.Drawing.Point(330, 45);
            this.numTopN.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.numTopN.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numTopN.Name = "numTopN";
            this.numTopN.Size = new System.Drawing.Size(60, 25);
            this.numTopN.Value = new decimal(new int[] { 10, 0, 0, 0 });
            this.numTopN.Visible = false;
            
            // btnAnalyze
            this.btnAnalyze.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.btnAnalyze.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnalyze.ForeColor = System.Drawing.Color.White;
            this.btnAnalyze.Location = new System.Drawing.Point(410, 42);
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
            this.panelStats.BackColor = System.Drawing.Color.FromArgb(232, 245, 233);
            this.panelStats.Controls.Add(this.lblStats);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStats.Location = new System.Drawing.Point(0, 640);
            this.panelStats.Name = "panelStats";
            this.panelStats.Size = new System.Drawing.Size(1200, 50);
            
            // lblStats
            this.lblStats.AutoSize = true;
            this.lblStats.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStats.ForeColor = System.Drawing.Color.FromArgb(27, 94, 32);
            this.lblStats.Location = new System.Drawing.Point(20, 18);
            this.lblStats.Text = "分析结果：";
            
            // CollectionStatisticsControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvStatistics);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.panelQuery);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Name = "CollectionStatisticsControl";
            this.Size = new System.Drawing.Size(1200, 690);
            this.Load += new System.EventHandler(this.CollectionStatisticsControl_Load);
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
        private System.Windows.Forms.Label lblTopN;
        private System.Windows.Forms.NumericUpDown numTopN;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.DataGridView dgvStatistics;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Label lblStats;

        private void CollectionStatisticsControl_Load(object sender, EventArgs e)
        {
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
            bool showTopN = cboStatType.SelectedIndex >= 4;
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
                    case 0: // 图书状态分布
                        sql = @"
                            SELECT 
                                item_status AS '状态',
                                COUNT(*) AS '数量',
                                SUM(CASE WHEN bib.price IS NOT NULL THEN bib.price ELSE 0 END) AS '总价值'
                            FROM BOOK_ITEM bi
                            LEFT JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                            GROUP BY item_status
                            ORDER BY COUNT(*) DESC";
                        break;

                    case 1: // 分类藏书统计
                        sql = @"
                            SELECT 
                                bc.category_name AS '分类名称',
                                bc.category_code AS '分类号',
                                COUNT(DISTINCT bib.bibliography_id) AS '书目数',
                                COUNT(bi.item_barcode) AS '馆藏数',
                                SUM(CASE WHEN bib.price IS NOT NULL THEN bib.price ELSE 0 END) AS '总价值'
                            FROM BOOK_CATEGORY bc
                            LEFT JOIN BIBLIOGRAPHY bib ON bc.category_id = bib.category_id
                            LEFT JOIN BOOK_ITEM bi ON bib.bibliography_id = bi.bibliography_id
                            GROUP BY bc.category_name, bc.category_code
                            ORDER BY COUNT(bi.item_barcode) DESC";
                        break;

                    case 2: // 出版年份分布
                        sql = @"
                            SELECT 
                                YEAR(publish_date) AS '出版年份',
                                COUNT(DISTINCT bib.bibliography_id) AS '书目数',
                                COUNT(bi.item_barcode) AS '馆藏数'
                            FROM BIBLIOGRAPHY bib
                            LEFT JOIN BOOK_ITEM bi ON bib.bibliography_id = bi.bibliography_id
                            WHERE publish_date IS NOT NULL
                            GROUP BY YEAR(publish_date)
                            ORDER BY YEAR(publish_date) DESC";
                        break;

                    case 3: // 价格区间分布
                        sql = @"
                            SELECT 
                                CASE 
                                    WHEN bib.price < 20 THEN '0-20元'
                                    WHEN bib.price < 50 THEN '20-50元'
                                    WHEN bib.price < 100 THEN '50-100元'
                                    WHEN bib.price < 200 THEN '100-200元'
                                    ELSE '200元以上'
                                END AS '价格区间',
                                COUNT(DISTINCT bib.bibliography_id) AS '书目数',
                                COUNT(bi.item_barcode) AS '馆藏数',
                                SUM(bib.price) AS '总价值'
                            FROM BIBLIOGRAPHY bib
                            LEFT JOIN BOOK_ITEM bi ON bib.bibliography_id = bi.bibliography_id
                            WHERE bib.price IS NOT NULL
                            GROUP BY CASE 
                                    WHEN bib.price < 20 THEN '0-20元'
                                    WHEN bib.price < 50 THEN '20-50元'
                                    WHEN bib.price < 100 THEN '50-100元'
                                    WHEN bib.price < 200 THEN '100-200元'
                                    ELSE '200元以上'
                                END
                            ORDER BY MIN(bib.price)";
                        break;

                    case 4: // 出版社TOP N
                        sql = string.Format(@"
                            SELECT TOP {0}
                                bib.publisher AS '出版社',
                                COUNT(DISTINCT bib.bibliography_id) AS '书目数',
                                COUNT(bi.item_barcode) AS '馆藏数',
                                SUM(CASE WHEN bib.price IS NOT NULL THEN bib.price ELSE 0 END) AS '总价值'
                            FROM BIBLIOGRAPHY bib
                            LEFT JOIN BOOK_ITEM bi ON bib.bibliography_id = bi.bibliography_id
                            WHERE bib.publisher IS NOT NULL AND bib.publisher <> ''
                            GROUP BY bib.publisher
                            ORDER BY COUNT(bi.item_barcode) DESC", numTopN.Value);
                        break;

                    case 5: // 作者TOP N
                        sql = string.Format(@"
                            SELECT TOP {0}
                                bib.author AS '作者',
                                COUNT(DISTINCT bib.bibliography_id) AS '书目数',
                                COUNT(bi.item_barcode) AS '馆藏数',
                                SUM(CASE WHEN bib.price IS NOT NULL THEN bib.price ELSE 0 END) AS '总价值'
                            FROM BIBLIOGRAPHY bib
                            LEFT JOIN BOOK_ITEM bi ON bib.bibliography_id = bi.bibliography_id
                            WHERE bib.author IS NOT NULL AND bib.author <> ''
                            GROUP BY bib.author
                            ORDER BY COUNT(bi.item_barcode) DESC", numTopN.Value);
                        break;
                }

                statisticsData = DatabaseHelper.ExecuteQuery(sql);

                // 绑定到数据视图
                dgvStatistics.DataSource = statisticsData;

                // 设置价格列格式
                if (dgvStatistics.Columns.Contains("总价值"))
                {
                    dgvStatistics.Columns["总价值"].DefaultCellStyle.Format = "C2";
                }

                // 更新统计信息
                int totalCount = 0;
                decimal totalValue = 0;
                string countColumn = statisticsData.Columns.Contains("馆藏数") ? "馆藏数" : "数量";
                
                foreach (DataRow row in statisticsData.Rows)
                {
                    if (row[countColumn] != DBNull.Value)
                    {
                        totalCount += Convert.ToInt32(row[countColumn]);
                    }
                    if (statisticsData.Columns.Contains("总价值") && row["总价值"] != DBNull.Value)
                    {
                        totalValue += Convert.ToDecimal(row["总价值"]);
                    }
                }

                lblStats.Text = string.Format("分析结果：共 {0} 项，馆藏总数 {1} 册，总价值 {2:C2}",
                    statisticsData.Rows.Count, totalCount, totalValue);
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
                string.Format("馆藏统计分析_{0}.csv", DateTime.Now.ToString("yyyyMMddHHmmss")),
                string.Format("馆藏统计分析 - {0}", cboStatType.SelectedItem));
        }
    }
}
