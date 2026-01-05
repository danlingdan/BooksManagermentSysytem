using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Utils;

namespace BooksManagermentSysytem.Controls.Reports
{
    /// <summary>
    /// 借阅综合查询控件
    /// 支持按读者类别、部门、班级、个人、借还日期、借阅天数等多条件组合查询
    /// </summary>
    public partial class BorrowQueryControl : UserControl
    {
        private DataTable queryResult;

        public BorrowQueryControl()
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
            this.lblReaderType = new System.Windows.Forms.Label();
            this.cboReaderType = new System.Windows.Forms.ComboBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.lblCardID = new System.Windows.Forms.Label();
            this.txtCardID = new System.Windows.Forms.TextBox();
            this.lblBorrowDateRange = new System.Windows.Forms.Label();
            this.dtpBorrowStart = new System.Windows.Forms.DateTimePicker();
            this.lblTo1 = new System.Windows.Forms.Label();
            this.dtpBorrowEnd = new System.Windows.Forms.DateTimePicker();
            this.lblReturnDateRange = new System.Windows.Forms.Label();
            this.dtpReturnStart = new System.Windows.Forms.DateTimePicker();
            this.lblTo2 = new System.Windows.Forms.Label();
            this.dtpReturnEnd = new System.Windows.Forms.DateTimePicker();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.chkOverdueOnly = new System.Windows.Forms.CheckBox();
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
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
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
            this.lblTitle.Text = "📋 借阅综合查询";
            
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
            this.panelQuery.Controls.Add(this.chkOverdueOnly);
            this.panelQuery.Controls.Add(this.cboStatus);
            this.panelQuery.Controls.Add(this.lblStatus);
            this.panelQuery.Controls.Add(this.dtpReturnEnd);
            this.panelQuery.Controls.Add(this.lblTo2);
            this.panelQuery.Controls.Add(this.dtpReturnStart);
            this.panelQuery.Controls.Add(this.lblReturnDateRange);
            this.panelQuery.Controls.Add(this.dtpBorrowEnd);
            this.panelQuery.Controls.Add(this.lblTo1);
            this.panelQuery.Controls.Add(this.dtpBorrowStart);
            this.panelQuery.Controls.Add(this.lblBorrowDateRange);
            this.panelQuery.Controls.Add(this.txtCardID);
            this.panelQuery.Controls.Add(this.lblCardID);
            this.panelQuery.Controls.Add(this.txtUnit);
            this.panelQuery.Controls.Add(this.lblUnit);
            this.panelQuery.Controls.Add(this.cboReaderType);
            this.panelQuery.Controls.Add(this.lblReaderType);
            this.panelQuery.Controls.Add(this.lblQueryTitle);
            this.panelQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelQuery.Location = new System.Drawing.Point(0, 60);
            this.panelQuery.Name = "panelQuery";
            this.panelQuery.Padding = new System.Windows.Forms.Padding(20);
            this.panelQuery.Size = new System.Drawing.Size(1200, 180);
            
            // lblQueryTitle
            this.lblQueryTitle.AutoSize = true;
            this.lblQueryTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblQueryTitle.Location = new System.Drawing.Point(20, 15);
            this.lblQueryTitle.Text = "查询条件";
            
            // lblReaderType
            this.lblReaderType.AutoSize = true;
            this.lblReaderType.Location = new System.Drawing.Point(20, 50);
            this.lblReaderType.Text = "读者类型：";
            
            // cboReaderType
            this.cboReaderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReaderType.Items.AddRange(new object[] { "全部", "本校学生", "本校教师", "校外人员" });
            this.cboReaderType.Location = new System.Drawing.Point(100, 47);
            this.cboReaderType.Name = "cboReaderType";
            this.cboReaderType.Size = new System.Drawing.Size(120, 25);
            this.cboReaderType.SelectedIndex = 0;
            
            // lblUnit
            this.lblUnit.AutoSize = true;
            this.lblUnit.Location = new System.Drawing.Point(240, 50);
            this.lblUnit.Text = "单位/学院：";
            
            // txtUnit
            this.txtUnit.Location = new System.Drawing.Point(330, 47);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(150, 25);
            
            // lblCardID
            this.lblCardID.AutoSize = true;
            this.lblCardID.Location = new System.Drawing.Point(500, 50);
            this.lblCardID.Text = "借书证号：";
            
            // txtCardID
            this.txtCardID.Location = new System.Drawing.Point(580, 47);
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.Size = new System.Drawing.Size(150, 25);
            
            // lblBorrowDateRange
            this.lblBorrowDateRange.AutoSize = true;
            this.lblBorrowDateRange.Location = new System.Drawing.Point(20, 90);
            this.lblBorrowDateRange.Text = "借阅日期：";
            
            // dtpBorrowStart
            this.dtpBorrowStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBorrowStart.Location = new System.Drawing.Point(100, 87);
            this.dtpBorrowStart.Name = "dtpBorrowStart";
            this.dtpBorrowStart.Size = new System.Drawing.Size(120, 25);
            
            // lblTo1
            this.lblTo1.AutoSize = true;
            this.lblTo1.Location = new System.Drawing.Point(225, 90);
            this.lblTo1.Text = "至";
            
            // dtpBorrowEnd
            this.dtpBorrowEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBorrowEnd.Location = new System.Drawing.Point(245, 87);
            this.dtpBorrowEnd.Name = "dtpBorrowEnd";
            this.dtpBorrowEnd.Size = new System.Drawing.Size(120, 25);
            
            // lblReturnDateRange
            this.lblReturnDateRange.AutoSize = true;
            this.lblReturnDateRange.Location = new System.Drawing.Point(385, 90);
            this.lblReturnDateRange.Text = "归还日期：";
            
            // dtpReturnStart
            this.dtpReturnStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReturnStart.Location = new System.Drawing.Point(465, 87);
            this.dtpReturnStart.Name = "dtpReturnStart";
            this.dtpReturnStart.Size = new System.Drawing.Size(120, 25);
            
            // lblTo2
            this.lblTo2.AutoSize = true;
            this.lblTo2.Location = new System.Drawing.Point(590, 90);
            this.lblTo2.Text = "至";
            
            // dtpReturnEnd
            this.dtpReturnEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReturnEnd.Location = new System.Drawing.Point(610, 87);
            this.dtpReturnEnd.Name = "dtpReturnEnd";
            this.dtpReturnEnd.Size = new System.Drawing.Size(120, 25);
            
            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(20, 130);
            this.lblStatus.Text = "借阅状态：";
            
            // cboStatus
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Items.AddRange(new object[] { "全部", "在借", "已还" });
            this.cboStatus.Location = new System.Drawing.Point(100, 127);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(120, 25);
            this.cboStatus.SelectedIndex = 0;
            
            // chkOverdueOnly
            this.chkOverdueOnly.AutoSize = true;
            this.chkOverdueOnly.Location = new System.Drawing.Point(240, 130);
            this.chkOverdueOnly.Text = "仅显示逾期记录";
            
            // btnQuery
            this.btnQuery.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.ForeColor = System.Drawing.Color.White;
            this.btnQuery.Location = new System.Drawing.Point(400, 125);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(100, 32);
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            
            // btnReset
            this.btnReset.Location = new System.Drawing.Point(510, 125);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(100, 32);
            this.btnReset.Text = "重置";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            
            // dgvResult
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResult.BackgroundColor = System.Drawing.Color.White;
            this.dgvResult.ColumnHeadersHeight = 40;
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.Location = new System.Drawing.Point(0, 240);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowHeadersVisible = false;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            
            // panelStats
            this.panelStats.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.panelStats.Controls.Add(this.lblStats);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStats.Location = new System.Drawing.Point(0, 640);
            this.panelStats.Name = "panelStats";
            this.panelStats.Size = new System.Drawing.Size(1200, 50);
            
            // lblStats
            this.lblStats.AutoSize = true;
            this.lblStats.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStats.Location = new System.Drawing.Point(20, 18);
            this.lblStats.Text = "查询结果：0 条记录";
            
            // BorrowQueryControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvResult);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.panelQuery);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1200, 600);
            this.Name = "BorrowQueryControl";
            this.Load += new System.EventHandler(this.BorrowQueryControl_Load);
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
        private System.Windows.Forms.Label lblReaderType;
        private System.Windows.Forms.ComboBox cboReaderType;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label lblCardID;
        private System.Windows.Forms.TextBox txtCardID;
        private System.Windows.Forms.Label lblBorrowDateRange;
        private System.Windows.Forms.DateTimePicker dtpBorrowStart;
        private System.Windows.Forms.Label lblTo1;
        private System.Windows.Forms.DateTimePicker dtpBorrowEnd;
        private System.Windows.Forms.Label lblReturnDateRange;
        private System.Windows.Forms.DateTimePicker dtpReturnStart;
        private System.Windows.Forms.Label lblTo2;
        private System.Windows.Forms.DateTimePicker dtpReturnEnd;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.CheckBox chkOverdueOnly;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Label lblStats;

        private void BorrowQueryControl_Load(object sender, EventArgs e)
        {
            dtpBorrowStart.Value = DateTime.Now.AddMonths(-3);
            dtpBorrowEnd.Value = DateTime.Now;
            dtpReturnStart.Value = DateTime.Now.AddMonths(-3);
            dtpReturnEnd.Value = DateTime.Now;
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
                        bb.bookborrow_id AS '借阅ID',
                        bb.cardID AS '借书证号',
                        r.readername AS '读者姓名',
                        r.readertype AS '读者类型',
                        r.unit AS '单位/学院',
                        bb.bookID AS '馆藏码',
                        bib.bibliography_name AS '书名',
                        bib.ISBN AS 'ISBN',
                        bc.category_code AS '分类号',
                        bb.borrowdate AS '借阅日期',
                        DATEADD(DAY, 7, bb.borrowdate) AS '应还日期',
                        bb.overdate AS '实际归还日期',
                        CASE 
                            WHEN bb.overdate IS NULL THEN N'在借'
                            ELSE N'已还'
                        END AS '状态',
                        CASE 
                            WHEN bb.overdate IS NULL AND GETDATE() > DATEADD(DAY, 7, bb.borrowdate) 
                                THEN DATEDIFF(DAY, DATEADD(DAY, 7, bb.borrowdate), GETDATE())
                            WHEN bb.overdate IS NOT NULL AND bb.overdate > DATEADD(DAY, 7, bb.borrowdate)
                                THEN DATEDIFF(DAY, DATEADD(DAY, 7, bb.borrowdate), bb.overdate)
                            ELSE 0
                        END AS '逾期天数',
                        bb.add_note AS '备注'
                    FROM bookborrow bb
                    INNER JOIN reader r ON bb.cardID = r.cardID
                    INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                    INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                    INNER JOIN BOOK_CATEGORY bc ON bib.category_id = bc.category_id
                    WHERE 1=1";

                var parameters = new System.Collections.Generic.List<System.Data.SqlClient.SqlParameter>();

                // 读者类型
                if (cboReaderType.SelectedIndex > 0)
                {
                    sql += " AND r.readertype = @readerType";
                    parameters.Add(DatabaseHelper.CreateParameter("@readerType", cboReaderType.SelectedItem.ToString()));
                }

                // 单位
                if (!string.IsNullOrWhiteSpace(txtUnit.Text))
                {
                    sql += " AND r.unit LIKE @unit";
                    parameters.Add(DatabaseHelper.CreateParameter("@unit", "%" + txtUnit.Text.Trim() + "%"));
                }

                // 借书证号
                if (!string.IsNullOrWhiteSpace(txtCardID.Text))
                {
                    sql += " AND bb.cardID LIKE @cardID";
                    parameters.Add(DatabaseHelper.CreateParameter("@cardID", "%" + txtCardID.Text.Trim() + "%"));
                }

                // 借阅日期范围
                sql += " AND bb.borrowdate >= @borrowStart AND bb.borrowdate <= @borrowEnd";
                parameters.Add(DatabaseHelper.CreateParameter("@borrowStart", dtpBorrowStart.Value.Date));
                parameters.Add(DatabaseHelper.CreateParameter("@borrowEnd", dtpBorrowEnd.Value.Date.AddDays(1).AddSeconds(-1)));

                // 归还状态
                if (cboStatus.SelectedIndex == 1) // 在借
                {
                    sql += " AND bb.overdate IS NULL";
                }
                else if (cboStatus.SelectedIndex == 2) // 已还
                {
                    sql += " AND bb.overdate IS NOT NULL";
                    sql += " AND bb.overdate >= @returnStart AND bb.overdate <= @returnEnd";
                    parameters.Add(DatabaseHelper.CreateParameter("@returnStart", dtpReturnStart.Value.Date));
                    parameters.Add(DatabaseHelper.CreateParameter("@returnEnd", dtpReturnEnd.Value.Date.AddDays(1).AddSeconds(-1)));
                }

                // 仅逾期
                if (chkOverdueOnly.Checked)
                {
                    sql += @" AND (
                        (bb.overdate IS NULL AND GETDATE() > DATEADD(DAY, 7, bb.borrowdate))
                        OR
                        (bb.overdate IS NOT NULL AND bb.overdate > DATEADD(DAY, 7, bb.borrowdate))
                    )";
                }

                sql += " ORDER BY bb.borrowdate DESC";

                queryResult = DatabaseHelper.ExecuteQuery(sql, parameters.ToArray());
                dgvResult.DataSource = queryResult;

                // 隐藏ID列
                if (dgvResult.Columns.Contains("借阅ID"))
                {
                    dgvResult.Columns["借阅ID"].Visible = false;
                }

                // 设置逾期行颜色
                dgvResult.CellFormatting += (s, cellArgs) =>
                {
                    if (dgvResult.Columns[cellArgs.ColumnIndex].HeaderText == "逾期天数" && cellArgs.Value != null)
                    {
                        int days = Convert.ToInt32(cellArgs.Value);
                        if (days > 0)
                        {
                            cellArgs.CellStyle.BackColor = Color.FromArgb(255, 230, 230);
                            cellArgs.CellStyle.ForeColor = Color.Red;
                            cellArgs.CellStyle.Font = new Font(dgvResult.Font, FontStyle.Bold);
                        }
                    }
                };

                lblStats.Text = $"查询结果：{queryResult.Rows.Count} 条记录";
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cboReaderType.SelectedIndex = 0;
            txtUnit.Clear();
            txtCardID.Clear();
            dtpBorrowStart.Value = DateTime.Now.AddMonths(-3);
            dtpBorrowEnd.Value = DateTime.Now;
            dtpReturnStart.Value = DateTime.Now.AddMonths(-3);
            dtpReturnEnd.Value = DateTime.Now;
            cboStatus.SelectedIndex = 0;
            chkOverdueOnly.Checked = false;
            
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

            ExportHelper.ExportDataTableToCSV(queryResult, $"借阅综合查询_{DateTime.Now:yyyyMMddHHmmss}.csv", "借阅综合查询");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (queryResult == null || queryResult.Rows.Count == 0)
            {
                MessageBox.Show("请先执行查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PrintHelper.PrintDataTable(queryResult, "借阅综合查询报表");
        }
    }
}
