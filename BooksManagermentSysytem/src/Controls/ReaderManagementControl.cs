using System;
using System.Data;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Models;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 读者管理控件 - 用于图书管理员查看和管理读者信息
    /// </summary>
    public partial class ReaderManagementControl : UserControl
    {
        public ReaderManagementControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panelSearch = new System.Windows.Forms.Panel();
            this.lblCardID = new System.Windows.Forms.Label();
            this.txtCardID = new System.Windows.Forms.TextBox();
            this.lblReaderName = new System.Windows.Forms.Label();
            this.txtReaderName = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dgvReaders = new System.Windows.Forms.DataGridView();
            this.panelReaderInfo = new System.Windows.Forms.Panel();
            this.lblReaderInfoTitle = new System.Windows.Forms.Label();
            this.dgvBorrowedBooks = new System.Windows.Forms.DataGridView();
            this.lblBorrowedTitle = new System.Windows.Forms.Label();
            this.dgvFines = new System.Windows.Forms.DataGridView();
            this.lblFinesTitle = new System.Windows.Forms.Label();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReaders)).BeginInit();
            this.panelReaderInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowedBooks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFines)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelSearch.Controls.Add(this.btnClear);
            this.panelSearch.Controls.Add(this.btnSearch);
            this.panelSearch.Controls.Add(this.txtReaderName);
            this.panelSearch.Controls.Add(this.lblReaderName);
            this.panelSearch.Controls.Add(this.txtCardID);
            this.panelSearch.Controls.Add(this.lblCardID);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(900, 50);
            // 
            // lblCardID
            // 
            this.lblCardID.AutoSize = true;
            this.lblCardID.Location = new System.Drawing.Point(20, 15);
            this.lblCardID.Text = "借书证号：";
            // 
            // txtCardID
            // 
            this.txtCardID.Location = new System.Drawing.Point(90, 12);
            this.txtCardID.Size = new System.Drawing.Size(150, 23);
            // 
            // lblReaderName
            // 
            this.lblReaderName.AutoSize = true;
            this.lblReaderName.Location = new System.Drawing.Point(260, 15);
            this.lblReaderName.Text = "读者姓名：";
            // 
            // txtReaderName
            // 
            this.txtReaderName.Location = new System.Drawing.Point(330, 12);
            this.txtReaderName.Size = new System.Drawing.Size(120, 23);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(480, 10);
            this.btnSearch.Size = new System.Drawing.Size(80, 28);
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(570, 10);
            this.btnClear.Size = new System.Drawing.Size(80, 28);
            this.btnClear.Text = "清空";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 50);
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dgvReaders);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panelReaderInfo);
            this.splitContainer.Size = new System.Drawing.Size(900, 500);
            this.splitContainer.SplitterDistance = 200;
            // 
            // dgvReaders
            // 
            this.dgvReaders.AllowUserToAddRows = false;
            this.dgvReaders.AllowUserToDeleteRows = false;
            this.dgvReaders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReaders.BackgroundColor = System.Drawing.Color.White;
            this.dgvReaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReaders.ReadOnly = true;
            this.dgvReaders.RowHeadersVisible = false;
            this.dgvReaders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReaders.SelectionChanged += new System.EventHandler(this.dgvReaders_SelectionChanged);
            // 
            // panelReaderInfo
            // 
            this.panelReaderInfo.Controls.Add(this.dgvFines);
            this.panelReaderInfo.Controls.Add(this.lblFinesTitle);
            this.panelReaderInfo.Controls.Add(this.dgvBorrowedBooks);
            this.panelReaderInfo.Controls.Add(this.lblBorrowedTitle);
            this.panelReaderInfo.Controls.Add(this.lblReaderInfoTitle);
            this.panelReaderInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // lblReaderInfoTitle
            // 
            this.lblReaderInfoTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.lblReaderInfoTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReaderInfoTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblReaderInfoTitle.ForeColor = System.Drawing.Color.White;
            this.lblReaderInfoTitle.Size = new System.Drawing.Size(900, 28);
            this.lblReaderInfoTitle.Text = "  读者详情";
            this.lblReaderInfoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBorrowedTitle
            // 
            this.lblBorrowedTitle.AutoSize = true;
            this.lblBorrowedTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBorrowedTitle.Location = new System.Drawing.Point(10, 35);
            this.lblBorrowedTitle.Text = "当前借阅：";
            // 
            // dgvBorrowedBooks
            // 
            this.dgvBorrowedBooks.AllowUserToAddRows = false;
            this.dgvBorrowedBooks.AllowUserToDeleteRows = false;
            this.dgvBorrowedBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBorrowedBooks.BackgroundColor = System.Drawing.Color.White;
            this.dgvBorrowedBooks.Location = new System.Drawing.Point(10, 55);
            this.dgvBorrowedBooks.ReadOnly = true;
            this.dgvBorrowedBooks.RowHeadersVisible = false;
            this.dgvBorrowedBooks.Size = new System.Drawing.Size(430, 120);
            // 
            // lblFinesTitle
            // 
            this.lblFinesTitle.AutoSize = true;
            this.lblFinesTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFinesTitle.Location = new System.Drawing.Point(460, 35);
            this.lblFinesTitle.Text = "未支付罚款：";
            // 
            // dgvFines
            // 
            this.dgvFines.AllowUserToAddRows = false;
            this.dgvFines.AllowUserToDeleteRows = false;
            this.dgvFines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFines.BackgroundColor = System.Drawing.Color.White;
            this.dgvFines.Location = new System.Drawing.Point(460, 55);
            this.dgvFines.ReadOnly = true;
            this.dgvFines.RowHeadersVisible = false;
            this.dgvFines.Size = new System.Drawing.Size(420, 120);
            // 
            // ReaderManagementControl
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelSearch);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Size = new System.Drawing.Size(900, 550);
            this.Load += new System.EventHandler(this.ReaderManagementControl_Load);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReaders)).EndInit();
            this.panelReaderInfo.ResumeLayout(false);
            this.panelReaderInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowedBooks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFines)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Label lblCardID;
        private System.Windows.Forms.TextBox txtCardID;
        private System.Windows.Forms.Label lblReaderName;
        private System.Windows.Forms.TextBox txtReaderName;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView dgvReaders;
        private System.Windows.Forms.Panel panelReaderInfo;
        private System.Windows.Forms.Label lblReaderInfoTitle;
        private System.Windows.Forms.DataGridView dgvBorrowedBooks;
        private System.Windows.Forms.Label lblBorrowedTitle;
        private System.Windows.Forms.DataGridView dgvFines;
        private System.Windows.Forms.Label lblFinesTitle;

        private void ReaderManagementControl_Load(object sender, EventArgs e)
        {
            LoadReaders();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadReaders();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCardID.Clear();
            txtReaderName.Clear();
            LoadReaders();
        }

        private void LoadReaders()
        {
            try
            {
                string sql = @"
                    SELECT r.cardID AS 借书证号, r.readername AS 姓名, r.readertype AS 类型,
                           r.unit AS 单位, r.number AS 学号工号, 
                           rc.state AS 证件状态, rc.overdate AS 有效期至,
                           CASE WHEN rc.state = N'正常' AND rc.overdate >= GETDATE() THEN N'可借阅' ELSE N'不可借阅' END AS 借阅状态
                    FROM reader r
                    INNER JOIN readcard rc ON r.cardID = rc.cardID
                    WHERE 1=1";

                var parameters = new System.Collections.Generic.List<System.Data.SqlClient.SqlParameter>();

                if (!string.IsNullOrWhiteSpace(txtCardID.Text))
                {
                    sql += " AND r.cardID LIKE @cardID";
                    parameters.Add(DatabaseHelper.CreateParameter("@cardID", "%" + txtCardID.Text.Trim() + "%"));
                }

                if (!string.IsNullOrWhiteSpace(txtReaderName.Text))
                {
                    sql += " AND r.readername LIKE @name";
                    parameters.Add(DatabaseHelper.CreateParameter("@name", "%" + txtReaderName.Text.Trim() + "%"));
                }

                sql += " ORDER BY r.cardID";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters.ToArray());
                dgvReaders.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载读者列表失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvReaders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReaders.SelectedRows.Count == 0) return;

            string cardID = dgvReaders.SelectedRows[0].Cells["借书证号"].Value?.ToString();
            if (string.IsNullOrEmpty(cardID)) return;

            LoadReaderDetails(cardID);
        }

        private void LoadReaderDetails(string cardID)
        {
            // 加载借阅信息
            LoadBorrowedBooks(cardID);
            
            // 加载罚款信息
            LoadFines(cardID);

            // 检查是否有未支付罚款，弹出提示
            CheckUnpaidFines(cardID);
        }

        private void LoadBorrowedBooks(string cardID)
        {
            try
            {
                string sql = @"
                    SELECT bb.bookID AS 馆藏码, bib.bibliography_name AS 书名, 
                           bb.borrowdate AS 借阅日期, 
                           DATEADD(DAY, 7, bb.borrowdate) AS 应还日期,
                           CASE WHEN GETDATE() > DATEADD(DAY, 7, bb.borrowdate) THEN N'逾期' ELSE N'正常' END AS 状态
                    FROM bookborrow bb
                    INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                    INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                    WHERE bb.cardID = @cardID AND bb.overdate IS NULL
                    ORDER BY bb.borrowdate";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.CreateParameter("@cardID", cardID));
                dgvBorrowedBooks.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载借阅信息失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFines(string cardID)
        {
            try
            {
                string sql = @"
                    SELECT reason AS 罚款原因, amount AS 金额, 
                           fine_status AS 状态, created_time AS 创建时间
                    FROM fine
                    WHERE cardID = @cardID AND fine_status = N'未支付'
                    ORDER BY created_time DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.CreateParameter("@cardID", cardID));
                dgvFines.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载罚款信息失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckUnpaidFines(string cardID)
        {
            try
            {
                string sql = @"
                    SELECT SUM(amount) AS total FROM fine 
                    WHERE cardID = @cardID AND fine_status = N'未支付'";

                object result = DatabaseHelper.ExecuteScalar(sql, DatabaseHelper.CreateParameter("@cardID", cardID));
                
                if (result != null && result != DBNull.Value)
                {
                    decimal total = Convert.ToDecimal(result);
                    if (total > 0)
                    {
                        // 获取读者姓名
                        string nameSql = "SELECT readername FROM reader WHERE cardID = @cardID";
                        object nameResult = DatabaseHelper.ExecuteScalar(nameSql, DatabaseHelper.CreateParameter("@cardID", cardID));
                        string readerName = nameResult?.ToString() ?? cardID;

                        MessageBox.Show($"读者【{readerName}】有未支付罚款 ¥{total:F2}，请提醒缴纳。", 
                            "罚款提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch
            {
                // 忽略检查错误
            }
        }
    }
}
