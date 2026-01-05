using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Utils;

namespace BooksManagermentSysytem.Controls.Reports
{
    /// <summary>
    /// 损坏图书查询控件
    /// 查询并统计损坏、丢失等异常状态的图书
    /// </summary>
    public partial class DamagedBooksQueryControl : UserControl
    {
        private DataTable queryResult;

        public DamagedBooksQueryControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panelQuery = new System.Windows.Forms.Panel();
            this.lblQueryTitle = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.lblBookName = new System.Windows.Forms.Label();
            this.txtBookName = new System.Windows.Forms.TextBox();
            this.lblDateRange = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.panelStats = new System.Windows.Forms.Panel();
            this.lblStats = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.panelStats.SuspendLayout();
            this.SuspendLayout();
            
            // panelHeader
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(255, 152, 0);
            this.panelHeader.Controls.Add(this.btnPrint);
            this.panelHeader.Controls.Add(this.btnExport);
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
            this.lblTitle.Text = "🔧 损坏图书查询";
            
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
            
            // btnPrint
            this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnPrint.BackColor = System.Drawing.Color.White;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Location = new System.Drawing.Point(960, 15);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 30);
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            
            // panelQuery
            this.panelQuery.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.panelQuery.Controls.Add(this.btnReset);
            this.panelQuery.Controls.Add(this.btnQuery);
            this.panelQuery.Controls.Add(this.dtpEnd);
            this.panelQuery.Controls.Add(this.lblTo);
            this.panelQuery.Controls.Add(this.dtpStart);
            this.panelQuery.Controls.Add(this.lblDateRange);
            this.panelQuery.Controls.Add(this.txtBookName);
            this.panelQuery.Controls.Add(this.lblBookName);
            this.panelQuery.Controls.Add(this.txtCategory);
            this.panelQuery.Controls.Add(this.lblCategory);
            this.panelQuery.Controls.Add(this.cboStatus);
            this.panelQuery.Controls.Add(this.lblStatus);
            this.panelQuery.Controls.Add(this.lblQueryTitle);
            this.panelQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelQuery.Location = new System.Drawing.Point(0, 60);
            this.panelQuery.Name = "panelQuery";
            this.panelQuery.Padding = new System.Windows.Forms.Padding(20);
            this.panelQuery.Size = new System.Drawing.Size(1200, 140);
            
            // lblQueryTitle
            this.lblQueryTitle.AutoSize = true;
            this.lblQueryTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblQueryTitle.Location = new System.Drawing.Point(20, 15);
            this.lblQueryTitle.Text = "查询条件";
            
            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(20, 50);
            this.lblStatus.Text = "图书状态：";
            
            // cboStatus
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Items.AddRange(new object[] { "全部异常", "损坏", "丢失", "注销" });
            this.cboStatus.Location = new System.Drawing.Point(100, 47);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(120, 25);
            this.cboStatus.SelectedIndex = 0;
            
            // lblCategory
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(240, 50);
            this.lblCategory.Text = "分类号：";
            
            // txtCategory
            this.txtCategory.Location = new System.Drawing.Point(305, 47);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(120, 25);
            
            // lblBookName
            this.lblBookName.AutoSize = true;
            this.lblBookName.Location = new System.Drawing.Point(445, 50);
            this.lblBookName.Text = "书名：";
            
            // txtBookName
            this.txtBookName.Location = new System.Drawing.Point(495, 47);
            this.txtBookName.Name = "txtBookName";
            this.txtBookName.Size = new System.Drawing.Size(200, 25);
            
            // lblDateRange
            this.lblDateRange.AutoSize = true;
            this.lblDateRange.Location = new System.Drawing.Point(20, 90);
            this.lblDateRange.Text = "登记日期：";
            
            // dtpStart
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(100, 87);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(120, 25);
            
            // lblTo
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(225, 90);
            this.lblTo.Text = "至";
            
            // dtpEnd
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(245, 87);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(120, 25);
            
            // btnQuery
            this.btnQuery.BackColor = System.Drawing.Color.FromArgb(255, 152, 0);
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.ForeColor = System.Drawing.Color.White;
            this.btnQuery.Location = new System.Drawing.Point(385, 85);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(100, 30);
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            
            // btnReset
            this.btnReset.Location = new System.Drawing.Point(495, 85);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(100, 30);
            this.btnReset.Text = "重置";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            
            // dgvResult
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResult.BackgroundColor = System.Drawing.Color.White;
            this.dgvResult.ColumnHeadersHeight = 40;
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.Location = new System.Drawing.Point(0, 200);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowHeadersVisible = false;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            
            // panelStats
            this.panelStats.BackColor = System.Drawing.Color.FromArgb(255, 243, 224);
            this.panelStats.Controls.Add(this.lblStats);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStats.Location = new System.Drawing.Point(0, 640);
            this.panelStats.Name = "panelStats";
            this.panelStats.Size = new System.Drawing.Size(1200, 50);
            
            // lblStats
            this.lblStats.AutoSize = true;
            this.lblStats.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStats.ForeColor = System.Drawing.Color.FromArgb(230, 81, 0);
            this.lblStats.Location = new System.Drawing.Point(20, 18);
            this.lblStats.Text = "查询结果：0 条记录";
            
            // DamagedBooksQueryControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvResult);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.panelQuery);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Name = "DamagedBooksQueryControl";
            this.Size = new System.Drawing.Size(1200, 690);
            this.Load += new System.EventHandler(this.DamagedBooksQueryControl_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelQuery.ResumeLayout(false);
            this.panelQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.panelStats.ResumeLayout(false);
            this.panelStats.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel panelQuery;
        private System.Windows.Forms.Label lblQueryTitle;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.Label lblBookName;
        private System.Windows.Forms.TextBox txtBookName;
        private System.Windows.Forms.Label lblDateRange;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Label lblStats;

        private void DamagedBooksQueryControl_Load(object sender, EventArgs e)
        {
            dtpStart.Value = DateTime.Now.AddYears(-1);
            dtpEnd.Value = DateTime.Now;
            ExecuteQuery();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            ExecuteQuery();
        }

        private void ExecuteQuery()
        {
            try
            {
                string sql = @"
                    SELECT 
                        bi.item_barcode AS '馆藏码',
                        bib.bibliography_name AS '书名',
                        bib.author AS '作者',
                        bc.category_code AS '分类号',
                        bib.ISBN AS 'ISBN',
                        bib.publisher AS '出版社',
                        bib.price AS '价格',
                        bi.item_status AS '状态',
                        bi.add_time AS '入藏时间',
                        bi.update_time AS '状态更新时间',
                        bi.add_note AS '备注'
                    FROM BOOK_ITEM bi
                    INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                    INNER JOIN BOOK_CATEGORY bc ON bib.category_id = bc.category_id
                    WHERE 1=1";

                var parameters = new System.Collections.Generic.List<System.Data.SqlClient.SqlParameter>();

                // 图书状态筛选
                if (cboStatus.SelectedIndex == 0)
                {
                    // 全部异常
                    sql += " AND bi.item_status IN (N'损坏', N'丢失', N'注销')";
                }
                else
                {
                    sql += " AND bi.item_status = @status";
                    parameters.Add(DatabaseHelper.CreateParameter("@status", cboStatus.SelectedItem.ToString()));
                }

                // 分类号筛选
                if (!string.IsNullOrWhiteSpace(txtCategory.Text))
                {
                    sql += " AND bc.category_code LIKE @category";
                    parameters.Add(DatabaseHelper.CreateParameter("@category", txtCategory.Text.Trim() + "%"));
                }

                // 书名筛选
                if (!string.IsNullOrWhiteSpace(txtBookName.Text))
                {
                    sql += " AND bib.bibliography_name LIKE @bookName";
                    parameters.Add(DatabaseHelper.CreateParameter("@bookName", "%" + txtBookName.Text.Trim() + "%"));
                }

                // 日期范围筛选（使用更新时间）
                sql += " AND bi.update_time >= @startDate AND bi.update_time <= @endDate";
                parameters.Add(DatabaseHelper.CreateParameter("@startDate", dtpStart.Value.Date));
                parameters.Add(DatabaseHelper.CreateParameter("@endDate", dtpEnd.Value.Date.AddDays(1).AddSeconds(-1)));

                sql += " ORDER BY bi.update_time DESC";

                queryResult = DatabaseHelper.ExecuteQuery(sql, parameters.ToArray());
                dgvResult.DataSource = queryResult;

                // 设置价格列格式
                if (dgvResult.Columns.Contains("价格"))
                {
                    dgvResult.Columns["价格"].DefaultCellStyle.Format = "C2";
                }

                // 设置状态颜色
                dgvResult.CellFormatting += (s, cellArgs) =>
                {
                    if (dgvResult.Columns[cellArgs.ColumnIndex].HeaderText == "状态" && cellArgs.Value != null)
                    {
                        string status = cellArgs.Value.ToString();
                        if (status == "损坏")
                        {
                            cellArgs.CellStyle.BackColor = Color.FromArgb(255, 243, 224);
                            cellArgs.CellStyle.ForeColor = Color.FromArgb(230, 81, 0);
                        }
                        else if (status == "丢失")
                        {
                            cellArgs.CellStyle.BackColor = Color.FromArgb(255, 235, 238);
                            cellArgs.CellStyle.ForeColor = Color.FromArgb(198, 40, 40);
                        }
                        else if (status == "注销")
                        {
                            cellArgs.CellStyle.BackColor = Color.FromArgb(224, 224, 224);
                            cellArgs.CellStyle.ForeColor = Color.FromArgb(97, 97, 97);
                        }
                    }
                };

                // 统计各状态数量和总价值
                int damagedCount = 0;
                int lostCount = 0;
                int canceledCount = 0;
                decimal totalValue = 0;

                foreach (DataRow row in queryResult.Rows)
                {
                    string status = row["状态"].ToString();
                    decimal price = row["价格"] != DBNull.Value ? Convert.ToDecimal(row["价格"]) : 0;
                    totalValue += price;

                    if (status == "损坏") damagedCount++;
                    else if (status == "丢失") lostCount++;
                    else if (status == "注销") canceledCount++;
                }

                lblStats.Text = string.Format("查询结果：共 {0} 本（损坏 {1} 本，丢失 {2} 本，注销 {3} 本） - 总价值：{4:C2}",
                    queryResult.Rows.Count, damagedCount, lostCount, canceledCount, totalValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cboStatus.SelectedIndex = 0;
            txtCategory.Clear();
            txtBookName.Clear();
            dtpStart.Value = DateTime.Now.AddYears(-1);
            dtpEnd.Value = DateTime.Now;
            
            queryResult = null;
            dgvResult.DataSource = null;
            lblStats.Text = "查询结果：0 条记录";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (queryResult == null || queryResult.Rows.Count == 0)
            {
                MessageBox.Show("请先执行查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ExportHelper.ExportDataTableToCSV(queryResult,
                string.Format("损坏图书查询_{0}.csv", DateTime.Now.ToString("yyyyMMddHHmmss")),
                "损坏图书查询报表");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (queryResult == null || queryResult.Rows.Count == 0)
            {
                MessageBox.Show("请先执行查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PrintHelper.PrintDataTable(queryResult, "损坏图书查询报表");
        }
    }
}
