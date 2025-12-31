using System;
using System.Data;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Models;
using BooksManagermentSysytem.Services;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 归还图书控件
    /// 功能：查询已借书籍、归还处理、自动计算罚款
    /// </summary>
    public partial class ReturnBookControl : UserControl
    {
        private string currentCardID;

        public ReturnBookControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panelSearch = new System.Windows.Forms.Panel();
            this.lblCardID = new System.Windows.Forms.Label();
            this.txtCardID = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblReaderInfo = new System.Windows.Forms.Label();
            this.dgvBorrowedBooks = new System.Windows.Forms.DataGridView();
            this.panelReturn = new System.Windows.Forms.Panel();
            this.lblReturnTitle = new System.Windows.Forms.Label();
            this.lblCondition = new System.Windows.Forms.Label();
            this.cboCondition = new System.Windows.Forms.ComboBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.btnReturn = new System.Windows.Forms.Button();
            this.lblFineInfo = new System.Windows.Forms.Label();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowedBooks)).BeginInit();
            this.panelReturn.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelSearch.Controls.Add(this.lblReaderInfo);
            this.panelSearch.Controls.Add(this.btnSearch);
            this.panelSearch.Controls.Add(this.txtCardID);
            this.panelSearch.Controls.Add(this.lblCardID);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Size = new System.Drawing.Size(900, 60);
            // 
            // lblCardID
            // 
            this.lblCardID.AutoSize = true;
            this.lblCardID.Location = new System.Drawing.Point(20, 20);
            this.lblCardID.Text = "借书证号：";
            // 
            // txtCardID
            // 
            this.txtCardID.Location = new System.Drawing.Point(90, 17);
            this.txtCardID.Size = new System.Drawing.Size(180, 23);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(285, 15);
            this.btnSearch.Size = new System.Drawing.Size(80, 28);
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblReaderInfo
            // 
            this.lblReaderInfo.Location = new System.Drawing.Point(385, 18);
            this.lblReaderInfo.Size = new System.Drawing.Size(500, 25);
            // 
            // dgvBorrowedBooks
            // 
            this.dgvBorrowedBooks.AllowUserToAddRows = false;
            this.dgvBorrowedBooks.AllowUserToDeleteRows = false;
            this.dgvBorrowedBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBorrowedBooks.BackgroundColor = System.Drawing.Color.White;
            this.dgvBorrowedBooks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBorrowedBooks.Location = new System.Drawing.Point(0, 60);
            this.dgvBorrowedBooks.ReadOnly = true;
            this.dgvBorrowedBooks.RowHeadersVisible = false;
            this.dgvBorrowedBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBorrowedBooks.Size = new System.Drawing.Size(900, 330);
            this.dgvBorrowedBooks.SelectionChanged += new System.EventHandler(this.dgvBorrowedBooks_SelectionChanged);
            // 
            // panelReturn
            // 
            this.panelReturn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelReturn.Controls.Add(this.lblFineInfo);
            this.panelReturn.Controls.Add(this.btnReturn);
            this.panelReturn.Controls.Add(this.txtNote);
            this.panelReturn.Controls.Add(this.lblNote);
            this.panelReturn.Controls.Add(this.cboCondition);
            this.panelReturn.Controls.Add(this.lblCondition);
            this.panelReturn.Controls.Add(this.lblReturnTitle);
            this.panelReturn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelReturn.Location = new System.Drawing.Point(0, 390);
            this.panelReturn.Size = new System.Drawing.Size(900, 130);
            // 
            // lblReturnTitle
            // 
            this.lblReturnTitle.AutoSize = true;
            this.lblReturnTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblReturnTitle.Location = new System.Drawing.Point(15, 10);
            this.lblReturnTitle.Text = "归还操作";
            // 
            // lblCondition
            // 
            this.lblCondition.AutoSize = true;
            this.lblCondition.Location = new System.Drawing.Point(15, 45);
            this.lblCondition.Text = "归还状态：";
            // 
            // cboCondition
            // 
            this.cboCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCondition.Items.AddRange(new object[] {
                "完好",
                "轻微破损",
                "严重破损",
                "丢失"});
            this.cboCondition.Location = new System.Drawing.Point(85, 42);
            this.cboCondition.Size = new System.Drawing.Size(120, 25);
            this.cboCondition.SelectedIndexChanged += new System.EventHandler(this.cboCondition_SelectedIndexChanged);
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(230, 45);
            this.lblNote.Text = "备注说明：";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(300, 42);
            this.txtNote.Size = new System.Drawing.Size(250, 23);
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Location = new System.Drawing.Point(580, 35);
            this.btnReturn.Size = new System.Drawing.Size(140, 40);
            this.btnReturn.Text = "确认归还";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // lblFineInfo
            // 
            this.lblFineInfo.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFineInfo.ForeColor = System.Drawing.Color.Red;
            this.lblFineInfo.Location = new System.Drawing.Point(15, 85);
            this.lblFineInfo.Size = new System.Drawing.Size(700, 30);
            // 
            // ReturnBookControl
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvBorrowedBooks);
            this.Controls.Add(this.panelReturn);
            this.Controls.Add(this.panelSearch);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Size = new System.Drawing.Size(900, 520);
            this.Load += new System.EventHandler(this.ReturnBookControl_Load);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowedBooks)).EndInit();
            this.panelReturn.ResumeLayout(false);
            this.panelReturn.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Label lblCardID;
        private System.Windows.Forms.TextBox txtCardID;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblReaderInfo;
        private System.Windows.Forms.DataGridView dgvBorrowedBooks;
        private System.Windows.Forms.Panel panelReturn;
        private System.Windows.Forms.Label lblReturnTitle;
        private System.Windows.Forms.Label lblCondition;
        private System.Windows.Forms.ComboBox cboCondition;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Label lblFineInfo;

        private void ReturnBookControl_Load(object sender, EventArgs e)
        {
            cboCondition.SelectedIndex = 0;

            // 如果是读者登录，自动填充
            var user = AuthenticationService.Instance.CurrentUser;
            if (user != null && user.IsReader && !string.IsNullOrEmpty(user.CardID))
            {
                txtCardID.Text = user.CardID;
                LoadBorrowedBooks();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadBorrowedBooks();
        }

        private void LoadBorrowedBooks()
        {
            lblFineInfo.Text = string.Empty;
            
            if (string.IsNullOrWhiteSpace(txtCardID.Text))
            {
                lblReaderInfo.Text = "请输入借书证号";
                lblReaderInfo.ForeColor = System.Drawing.Color.Red;
                return;
            }

            currentCardID = txtCardID.Text.Trim();

            try
            {
                // 获取读者信息
                string readerSql = "SELECT readername FROM reader WHERE cardID = @cardID";
                object nameObj = DatabaseHelper.ExecuteScalar(readerSql,
                    DatabaseHelper.CreateParameter("@cardID", currentCardID));

                if (nameObj == null)
                {
                    lblReaderInfo.Text = "未找到该读者";
                    lblReaderInfo.ForeColor = System.Drawing.Color.Red;
                    dgvBorrowedBooks.DataSource = null;
                    return;
                }

                lblReaderInfo.Text = $"读者姓名：{nameObj}";
                lblReaderInfo.ForeColor = System.Drawing.Color.Black;

                // 查询已借书籍
                string sql = @"
                    SELECT bb.bookborrow_id AS ID,
                           bb.bookID AS 馆藏码, 
                           bib.bibliography_name AS 书名,
                           bib.ISBN,
                           bc.category_code AS 分类,
                           bb.borrowdate AS 借阅日期,
                           DATEADD(DAY, 7, bb.borrowdate) AS 应还日期,
                           CASE WHEN GETDATE() > DATEADD(DAY, 7, bb.borrowdate) THEN N'逾期' ELSE N'正常' END AS 状态,
                           CASE WHEN GETDATE() > DATEADD(DAY, 7, bb.borrowdate) 
                                THEN DATEDIFF(DAY, DATEADD(DAY, 7, bb.borrowdate), GETDATE()) 
                                ELSE 0 END AS 逾期天数,
                           COALESCE(bi.price, bib.price, 0) AS 单价
                    FROM bookborrow bb
                    INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                    INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                    INNER JOIN BOOK_CATEGORY bc ON bib.category_id = bc.category_id
                    WHERE bb.cardID = @cardID AND bb.overdate IS NULL
                    ORDER BY bb.borrowdate";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@cardID", currentCardID));

                dgvBorrowedBooks.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    lblReaderInfo.Text += " | 暂无待归还书籍";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvBorrowedBooks_SelectionChanged(object sender, EventArgs e)
        {
            UpdateFineInfo();
        }

        private void cboCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFineInfo();
        }

        private void UpdateFineInfo()
        {
            lblFineInfo.Text = string.Empty;

            if (dgvBorrowedBooks.SelectedRows.Count == 0) return;

            DataGridViewRow row = dgvBorrowedBooks.SelectedRows[0];
            
            int overdueDays = Convert.ToInt32(row.Cells["逾期天数"].Value);
            decimal price = Convert.ToDecimal(row.Cells["单价"].Value);
            string bookName = row.Cells["书名"].Value.ToString();

            decimal fineAmount = 0;
            string fineReason = string.Empty;

            switch (cboCondition.SelectedIndex)
            {
                case 0: // 完好
                    if (overdueDays > 0)
                    {
                        fineAmount = FineCalculator.CalculateOverdueFine(price, overdueDays);
                        fineReason = FineCalculator.GetFineReason(FineType.Overdue, bookName, overdueDays);
                    }
                    break;
                case 1: // 轻微破损
                    fineAmount = FineCalculator.CalculateDamagedFine(price) * 0.5m;
                    if (overdueDays > 0)
                    {
                        fineAmount += FineCalculator.CalculateOverdueFine(price, overdueDays);
                    }
                    fineReason = $"轻微破损 + {(overdueDays > 0 ? $"逾期{overdueDays}天" : "")}";
                    break;
                case 2: // 严重破损
                    fineAmount = FineCalculator.CalculateDamagedFine(price);
                    if (overdueDays > 0)
                    {
                        fineAmount += FineCalculator.CalculateOverdueFine(price, overdueDays);
                    }
                    fineReason = FineCalculator.GetFineReason(FineType.Damaged, bookName);
                    break;
                case 3: // 丢失
                    fineAmount = FineCalculator.CalculateLostFine(price);
                    fineReason = FineCalculator.GetFineReason(FineType.Lost, bookName);
                    break;
            }

            if (fineAmount > 0)
            {
                lblFineInfo.Text = $"预计罚款：¥{fineAmount:F2}  |  原因：{fineReason}";
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (dgvBorrowedBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要归还的书籍", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dgvBorrowedBooks.SelectedRows[0];
            long borrowId = Convert.ToInt64(row.Cells["ID"].Value);
            string bookID = row.Cells["馆藏码"].Value.ToString();
            string bookName = row.Cells["书名"].Value.ToString();
            int overdueDays = Convert.ToInt32(row.Cells["逾期天数"].Value);
            decimal price = Convert.ToDecimal(row.Cells["单价"].Value);

            string condition = cboCondition.SelectedItem.ToString();
            bool isLost = cboCondition.SelectedIndex == 3;

            // 计算罚款
            decimal fineAmount = 0;
            string fineReason = string.Empty;

            switch (cboCondition.SelectedIndex)
            {
                case 0:
                    if (overdueDays > 0)
                    {
                        fineAmount = FineCalculator.CalculateOverdueFine(price, overdueDays);
                        fineReason = FineCalculator.GetFineReason(FineType.Overdue, bookName, overdueDays);
                    }
                    break;
                case 1:
                    fineAmount = FineCalculator.CalculateDamagedFine(price) * 0.5m;
                    if (overdueDays > 0)
                    {
                        fineAmount += FineCalculator.CalculateOverdueFine(price, overdueDays);
                    }
                    fineReason = $"图书《{bookName}》轻微破损";
                    break;
                case 2:
                    fineAmount = FineCalculator.CalculateDamagedFine(price);
                    if (overdueDays > 0)
                    {
                        fineAmount += FineCalculator.CalculateOverdueFine(price, overdueDays);
                    }
                    fineReason = FineCalculator.GetFineReason(FineType.Damaged, bookName);
                    break;
                case 3:
                    fineAmount = FineCalculator.CalculateLostFine(price);
                    fineReason = FineCalculator.GetFineReason(FineType.Lost, bookName);
                    break;
            }

            string confirmMsg = $"确认归还书籍《{bookName}》？\n状态：{condition}";
            if (fineAmount > 0)
            {
                confirmMsg += $"\n\n将产生罚款：¥{fineAmount:F2}\n原因：{fineReason}";
            }

            if (MessageBox.Show(confirmMsg, "确认归还", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                // 更新借阅明细
                string updateBorrowSql = @"
                    UPDATE bookborrow 
                    SET overdate = GETDATE(), add_note = @note 
                    WHERE bookborrow_id = @id";

                DatabaseHelper.ExecuteNonQuery(updateBorrowSql,
                    DatabaseHelper.CreateParameter("@id", borrowId),
                    DatabaseHelper.CreateParameter("@note", txtNote.Text.Trim()));

                // 更新书籍状态
                string newStatus = isLost ? "OFF_SHELF" : "AVAILABLE";
                string physicalCondition = cboCondition.SelectedIndex >= 2 ? "DAMAGED" : "GOOD";

                string updateBookSql = @"
                    UPDATE BOOK_ITEM 
                    SET current_status = @status, 
                        physical_condition = @condition,
                        status_changed_date = GETDATE()
                    WHERE item_barcode = @barcode";

                DatabaseHelper.ExecuteNonQuery(updateBookSql,
                    DatabaseHelper.CreateParameter("@status", newStatus),
                    DatabaseHelper.CreateParameter("@condition", physicalCondition),
                    DatabaseHelper.CreateParameter("@barcode", bookID));

                // 如果有罚款，创建罚款记录
                if (fineAmount > 0)
                {
                    string readerNameSql = "SELECT readername FROM reader WHERE cardID = @cardID";
                    string readerName = DatabaseHelper.ExecuteScalar(readerNameSql,
                        DatabaseHelper.CreateParameter("@cardID", currentCardID))?.ToString() ?? "";

                    string insertFineSql = @"
                        INSERT INTO fine (cardID, readername, reason, amount, fine_status)
                        VALUES (@cardID, @name, @reason, @amount, N'未支付')";

                    DatabaseHelper.ExecuteNonQuery(insertFineSql,
                        DatabaseHelper.CreateParameter("@cardID", currentCardID),
                        DatabaseHelper.CreateParameter("@name", readerName),
                        DatabaseHelper.CreateParameter("@reason", fineReason),
                        DatabaseHelper.CreateParameter("@amount", fineAmount));
                }

                string successMsg = "归还成功！";
                if (fineAmount > 0)
                {
                    successMsg += $"\n已生成罚款记录：¥{fineAmount:F2}";
                }

                MessageBox.Show(successMsg, "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 刷新列表
                LoadBorrowedBooks();
                cboCondition.SelectedIndex = 0;
                txtNote.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("归还失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
