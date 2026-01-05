using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Utils;
using BooksManagermentSysytem.Helpers;

namespace BooksManagermentSysytem.Controls.Reports
{
    /// <summary>
    /// 读者借阅史查询控件
    /// 查询指定读者的完整借阅历史记录
    /// </summary>
    public partial class ReaderHistoryControl : UserControl
    {
        private DataTable historyData;
        private string currentCardID;

        public ReaderHistoryControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.lblCardID = new System.Windows.Forms.Label();
            this.cboCardID = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panelReaderInfo = new System.Windows.Forms.Panel();
            this.lblReaderInfo = new System.Windows.Forms.Label();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.panelStats = new System.Windows.Forms.Panel();
            this.lblStats = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.panelReaderInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.panelStats.SuspendLayout();
            this.SuspendLayout();
            
            // panelHeader
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.panelHeader.Controls.Add(this.btnPrint);
            this.panelHeader.Controls.Add(this.btnExport);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Size = new System.Drawing.Size(1200, 60);
            
            // lblTitle
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 18);
            this.lblTitle.Text = "👤 读者借阅史查询";
            this.lblTitle.AutoSize = true;
            
            // btnExport
            this.btnExport.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnExport.BackColor = System.Drawing.Color.White;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Location = new System.Drawing.Point(1070, 15);
            this.btnExport.Size = new System.Drawing.Size(100, 30);
            this.btnExport.Text = "导出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            
            // btnPrint
            this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnPrint.BackColor = System.Drawing.Color.White;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Location = new System.Drawing.Point(960, 15);
            this.btnPrint.Size = new System.Drawing.Size(100, 30);
            this.btnPrint.Text = "打印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            
            // panelSearch
            this.panelSearch.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.panelSearch.Controls.Add(this.btnSearch);
            this.panelSearch.Controls.Add(this.cboCardID);
            this.panelSearch.Controls.Add(this.lblCardID);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 60);
            this.panelSearch.Padding = new System.Windows.Forms.Padding(20);
            this.panelSearch.Size = new System.Drawing.Size(1200, 70);
            
            // lblCardID
            this.lblCardID.AutoSize = true;
            this.lblCardID.Location = new System.Drawing.Point(20, 25);
            this.lblCardID.Text = "借书证号：";
            
            // cboCardID
            this.cboCardID.Location = new System.Drawing.Point(100, 22);
            this.cboCardID.Size = new System.Drawing.Size(250, 28);
            this.cboCardID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboCardID_KeyDown);
            
            // btnSearch
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(370, 20);
            this.btnSearch.Size = new System.Drawing.Size(100, 30);
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            
            // panelReaderInfo
            this.panelReaderInfo.BackColor = System.Drawing.Color.FromArgb(232, 245, 233);
            this.panelReaderInfo.Controls.Add(this.lblReaderInfo);
            this.panelReaderInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelReaderInfo.Location = new System.Drawing.Point(0, 130);
            this.panelReaderInfo.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.panelReaderInfo.Size = new System.Drawing.Size(1200, 60);
            this.panelReaderInfo.Visible = false;
            
            // lblReaderInfo
            this.lblReaderInfo.AutoSize = true;
            this.lblReaderInfo.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblReaderInfo.Location = new System.Drawing.Point(20, 18);
            this.lblReaderInfo.Text = "读者信息：";
            
            // dgvHistory
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistory.ColumnHeadersHeight = 40;
            this.dgvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.RowHeadersVisible = false;
            this.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            
            // panelStats
            this.panelStats.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.panelStats.Controls.Add(this.lblStats);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStats.Size = new System.Drawing.Size(1200, 50);
            
            // lblStats
            this.lblStats.AutoSize = true;
            this.lblStats.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStats.Location = new System.Drawing.Point(20, 18);
            this.lblStats.Text = "借阅记录：0 条";
            
            // ReaderHistoryControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvHistory);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.panelReaderInfo);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Size = new System.Drawing.Size(1200, 690);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.panelReaderInfo.ResumeLayout(false);
            this.panelReaderInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.panelStats.ResumeLayout(false);
            this.panelStats.PerformLayout();
            this.ResumeLayout(false);
        }

        private Panel panelHeader;
        private Label lblTitle;
        private Button btnExport;
        private Button btnPrint;
        private Panel panelSearch;
        private Label lblCardID;
        private ComboBox cboCardID;
        private Button btnSearch;
        private Panel panelReaderInfo;
        private Label lblReaderInfo;
        private DataGridView dgvHistory;
        private Panel panelStats;
        private Label lblStats;

        private void ReaderHistoryControl_Load(object sender, EventArgs e)
        {
            // 初始化借书证选择框 - 显示所有状态的借书证
            CardIDSelector.InitializeCardIDComboBox(cboCardID, onlyNormal: false, allowEmpty: true);
        }

        private void cboCardID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string cardID = CardIDSelector.GetSelectedCardID(cboCardID);
            if (string.IsNullOrWhiteSpace(cardID))
            {
                MessageBox.Show("请选择或输入借书证号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // 查询读者信息
                string readerSql = @"
                    SELECT r.cardID, r.readername, r.readertype, r.unit, rc.state
                    FROM reader r
                    INNER JOIN readcard rc ON r.cardID = rc.cardID
                    WHERE r.cardID = @cardID";

                DataTable readerDt = DatabaseHelper.ExecuteQuery(readerSql,
                    DatabaseHelper.CreateParameter("@cardID", cardID));

                if (readerDt.Rows.Count == 0)
                {
                    MessageBox.Show("未找到该读者", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DataRow readerRow = readerDt.Rows[0];
                currentCardID = readerRow["cardID"].ToString();

                // 显示读者信息
                lblReaderInfo.Text = $"👤 {readerRow["readername"]} | 类型：{readerRow["readertype"]} | " +
                    $"单位：{readerRow["unit"]} | 状态：{readerRow["state"]}";
                panelReaderInfo.Visible = true;

                // 查询借阅历史
                LoadBorrowHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBorrowHistory()
        {
            try
            {
                string sql = @"
                    SELECT 
                        bb.bookID AS '馆藏码',
                        bib.bibliography_name AS '书名',
                        bib.ISBN AS 'ISBN',
                        bc.category_code AS '分类号',
                        bb.borrowdate AS '借阅日期',
                        DATEADD(DAY, 7, bb.borrowdate) AS '应还日期',
                        bb.overdate AS '实际归还日期',
                        CASE 
                            WHEN bb.overdate IS NULL THEN N'在借'
                            WHEN bb.overdate > DATEADD(DAY, 7, bb.borrowdate) THEN N'逾期归还'
                            ELSE N'按时归还'
                        END AS '归还状态',
                        CASE 
                            WHEN bb.overdate IS NOT NULL AND bb.overdate > DATEADD(DAY, 7, bb.borrowdate)
                                THEN DATEDIFF(DAY, DATEADD(DAY, 7, bb.borrowdate), bb.overdate)
                            ELSE 0
                        END AS '逾期天数',
                        bb.add_note AS '备注'
                    FROM bookborrow bb
                    INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                    INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                    INNER JOIN BOOK_CATEGORY bc ON bib.category_id = bc.category_id
                    WHERE bb.cardID = @cardID
                    ORDER BY bb.borrowdate DESC";

                historyData = DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@cardID", currentCardID));

                dgvHistory.DataSource = historyData;

                // 设置颜色
                dgvHistory.CellFormatting += (s, e) =>
                {
                    if (dgvHistory.Columns[e.ColumnIndex].HeaderText == "归还状态" && e.Value != null)
                    {
                        string status = e.Value.ToString();
                        if (status == "在借")
                        {
                            e.CellStyle.ForeColor = Color.Blue;
                            e.CellStyle.Font = new Font(dgvHistory.Font, FontStyle.Bold);
                        }
                        else if (status == "逾期归还")
                        {
                            e.CellStyle.ForeColor = Color.Red;
                        }
                        else if (status == "按时归还")
                        {
                            e.CellStyle.ForeColor = Color.Green;
                        }
                    }
                };

                int totalBorrows = historyData.Rows.Count;
                int currentBorrowed = historyData.AsEnumerable().Count(r => r["归还状态"].ToString() == "在借");
                int overdueCount = historyData.AsEnumerable().Count(r => r["归还状态"].ToString() == "逾期归还");

                lblStats.Text = $"借阅记录：共 {totalBorrows} 次 | 当前在借：{currentBorrowed} 本 | 历史逾期：{overdueCount} 次";
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载借阅历史失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (historyData == null || historyData.Rows.Count == 0)
            {
                MessageBox.Show("请先查询读者", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ExportHelper.ExportDataTableToCSV(historyData,
                $"读者借阅史_{currentCardID}_{DateTime.Now:yyyyMMddHHmmss}.csv",
                lblReaderInfo.Text);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (historyData == null || historyData.Rows.Count == 0)
            {
                MessageBox.Show("请先查询读者", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PrintHelper.PrintDataTable(historyData, lblReaderInfo.Text);
        }
    }
}
