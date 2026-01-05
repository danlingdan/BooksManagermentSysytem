using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Utils;

namespace BooksManagermentSysytem.Controls.Reports
{
    /// <summary>
    /// 图书借阅史查询控件
    /// 查询指定图书的完整借阅历史
    /// </summary>
    public partial class BookHistoryControl : UserControl
    {
        private DataTable historyData;
        private int currentBibliographyId;

        public BookHistoryControl()
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
            this.lblISBN = new System.Windows.Forms.Label();
            this.txtISBN = new System.Windows.Forms.TextBox();
            this.lblBookName = new System.Windows.Forms.Label();
            this.txtBookName = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panelBookInfo = new System.Windows.Forms.Panel();
            this.lblBookInfo = new System.Windows.Forms.Label();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.panelStats = new System.Windows.Forms.Panel();
            this.lblStats = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.panelBookInfo.SuspendLayout();
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
            this.lblTitle.Text = "📚 图书借阅史查询";
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
            this.panelSearch.Controls.Add(this.txtBookName);
            this.panelSearch.Controls.Add(this.lblBookName);
            this.panelSearch.Controls.Add(this.txtISBN);
            this.panelSearch.Controls.Add(this.lblISBN);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 60);
            this.panelSearch.Padding = new System.Windows.Forms.Padding(20);
            this.panelSearch.Size = new System.Drawing.Size(1200, 70);
            
            // lblISBN
            this.lblISBN.AutoSize = true;
            this.lblISBN.Location = new System.Drawing.Point(20, 25);
            this.lblISBN.Text = "ISBN：";
            
            // txtISBN
            this.txtISBN.Location = new System.Drawing.Point(80, 22);
            this.txtISBN.Size = new System.Drawing.Size(180, 25);
            
            // lblBookName
            this.lblBookName.AutoSize = true;
            this.lblBookName.Location = new System.Drawing.Point(280, 25);
            this.lblBookName.Text = "书名：";
            
            // txtBookName
            this.txtBookName.Location = new System.Drawing.Point(330, 22);
            this.txtBookName.Size = new System.Drawing.Size(250, 25);
            
            // btnSearch
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(600, 20);
            this.btnSearch.Size = new System.Drawing.Size(100, 30);
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            
            // panelBookInfo
            this.panelBookInfo.BackColor = System.Drawing.Color.FromArgb(232, 245, 233);
            this.panelBookInfo.Controls.Add(this.lblBookInfo);
            this.panelBookInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBookInfo.Location = new System.Drawing.Point(0, 130);
            this.panelBookInfo.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.panelBookInfo.Size = new System.Drawing.Size(1200, 60);
            this.panelBookInfo.Visible = false;
            
            // lblBookInfo
            this.lblBookInfo.AutoSize = true;
            this.lblBookInfo.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBookInfo.Location = new System.Drawing.Point(20, 18);
            this.lblBookInfo.Text = "图书信息：";
            
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
            
            // BookHistoryControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvHistory);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.panelBookInfo);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Size = new System.Drawing.Size(1200, 690);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.panelBookInfo.ResumeLayout(false);
            this.panelBookInfo.PerformLayout();
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
        private Label lblISBN;
        private TextBox txtISBN;
        private Label lblBookName;
        private TextBox txtBookName;
        private Button btnSearch;
        private Panel panelBookInfo;
        private Label lblBookInfo;
        private DataGridView dgvHistory;
        private Panel panelStats;
        private Label lblStats;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtISBN.Text) && string.IsNullOrWhiteSpace(txtBookName.Text))
            {
                MessageBox.Show("请输入ISBN或书名", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // 先查找图书
                string bookSql = @"
                    SELECT bibliography_id, ISBN, bibliography_name, publish, category_id 
                    FROM BIBLIOGRAPHY 
                    WHERE 1=1";

                var parameters = new System.Collections.Generic.List<System.Data.SqlClient.SqlParameter>();

                if (!string.IsNullOrWhiteSpace(txtISBN.Text))
                {
                    bookSql += " AND ISBN LIKE @isbn";
                    parameters.Add(DatabaseHelper.CreateParameter("@isbn", "%" + txtISBN.Text.Trim() + "%"));
                }

                if (!string.IsNullOrWhiteSpace(txtBookName.Text))
                {
                    bookSql += " AND bibliography_name LIKE @name";
                    parameters.Add(DatabaseHelper.CreateParameter("@name", "%" + txtBookName.Text.Trim() + "%"));
                }

                DataTable bookDt = DatabaseHelper.ExecuteQuery(bookSql, parameters.ToArray());

                if (bookDt.Rows.Count == 0)
                {
                    MessageBox.Show("未找到匹配的图书", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (bookDt.Rows.Count > 1)
                {
                    MessageBox.Show($"找到 {bookDt.Rows.Count} 本匹配的图书，请输入更精确的条件", 
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DataRow bookRow = bookDt.Rows[0];
                currentBibliographyId = Convert.ToInt32(bookRow["bibliography_id"]);
                
                // 显示图书信息
                lblBookInfo.Text = $"📖 《{bookRow["bibliography_name"]}》 | ISBN: {bookRow["ISBN"]} | 出版社: {bookRow["publish"]}";
                panelBookInfo.Visible = true;

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
                        bb.cardID AS '借书证号',
                        r.readername AS '读者姓名',
                        r.readertype AS '读者类型',
                        r.unit AS '单位/学院',
                        bb.borrowdate AS '借阅日期',
                        bb.overdate AS '归还日期',
                        CASE 
                            WHEN bb.overdate IS NULL THEN N'在借'
                            WHEN bb.overdate > DATEADD(DAY, 7, bb.borrowdate) THEN N'逾期归还'
                            ELSE N'正常归还'
                        END AS '归还状态',
                        CASE 
                            WHEN bb.overdate IS NULL THEN DATEDIFF(DAY, bb.borrowdate, GETDATE())
                            ELSE DATEDIFF(DAY, bb.borrowdate, bb.overdate)
                        END AS '借阅天数',
                        bb.add_note AS '备注'
                    FROM bookborrow bb
                    INNER JOIN reader r ON bb.cardID = r.cardID
                    INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                    WHERE bi.bibliography_id = @bibId
                    ORDER BY bb.borrowdate DESC";

                historyData = DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@bibId", currentBibliographyId));

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
                        }
                        else if (status == "逾期归还")
                        {
                            e.CellStyle.ForeColor = Color.Red;
                        }
                    }
                };

                int totalBorrows = historyData.Rows.Count;
                int currentBorrowed = historyData.AsEnumerable().Count(r => r["归还状态"].ToString() == "在借");
                lblStats.Text = $"借阅记录：共 {totalBorrows} 次 | 当前在借：{currentBorrowed} 本";
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
                MessageBox.Show("请先查询图书", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ExportHelper.ExportDataTableToCSV(historyData, 
                $"图书借阅史_{DateTime.Now:yyyyMMddHHmmss}.csv",
                lblBookInfo.Text);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (historyData == null || historyData.Rows.Count == 0)
            {
                MessageBox.Show("请先查询图书", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PrintHelper.PrintDataTable(historyData, lblBookInfo.Text);
        }
    }
}
