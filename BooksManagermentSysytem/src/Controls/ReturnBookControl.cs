using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Models;
using BooksManagermentSysytem.Services;
using BooksManagermentSysytem.Helpers;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 归还图书控件
    /// 功能：查询已借书籍、归还处理、自动计算罚款
    /// 完整处理：逾期计算、损坏赔偿、丢失赔偿、图书状态更新
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
            this.lblReaderInfo = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cboCardID = new System.Windows.Forms.ComboBox();
            this.lblCardID = new System.Windows.Forms.Label();
            this.dgvBorrowedBooks = new System.Windows.Forms.DataGridView();
            this.panelReturn = new System.Windows.Forms.Panel();
            this.lblFineInfo = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Button();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.cboCondition = new System.Windows.Forms.ComboBox();
            this.lblCondition = new System.Windows.Forms.Label();
            this.lblReturnTitle = new System.Windows.Forms.Label();
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
            this.panelSearch.Controls.Add(this.cboCardID);
            this.panelSearch.Controls.Add(this.lblCardID);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Margin = new System.Windows.Forms.Padding(4);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(1350, 90);
            this.panelSearch.TabIndex = 2;
            // 
            // lblReaderInfo
            // 
            this.lblReaderInfo.Location = new System.Drawing.Point(578, 27);
            this.lblReaderInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReaderInfo.Name = "lblReaderInfo";
            this.lblReaderInfo.Size = new System.Drawing.Size(750, 38);
            this.lblReaderInfo.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(428, 22);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(120, 42);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cboCardID
            // 
            this.cboCardID.Location = new System.Drawing.Point(135, 26);
            this.cboCardID.Margin = new System.Windows.Forms.Padding(4);
            this.cboCardID.Name = "cboCardID";
            this.cboCardID.Size = new System.Drawing.Size(285, 32);
            this.cboCardID.TabIndex = 2;
            // 
            // lblCardID
            // 
            this.lblCardID.AutoSize = true;
            this.lblCardID.Location = new System.Drawing.Point(30, 30);
            this.lblCardID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCardID.Name = "lblCardID";
            this.lblCardID.Size = new System.Drawing.Size(100, 24);
            this.lblCardID.TabIndex = 3;
            this.lblCardID.Text = "借书证号：";
            // 
            // dgvBorrowedBooks
            // 
            this.dgvBorrowedBooks.AllowUserToAddRows = false;
            this.dgvBorrowedBooks.AllowUserToDeleteRows = false;
            this.dgvBorrowedBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBorrowedBooks.BackgroundColor = System.Drawing.Color.White;
            this.dgvBorrowedBooks.ColumnHeadersHeight = 40;
            this.dgvBorrowedBooks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBorrowedBooks.Location = new System.Drawing.Point(0, 90);
            this.dgvBorrowedBooks.Margin = new System.Windows.Forms.Padding(4);
            this.dgvBorrowedBooks.Name = "dgvBorrowedBooks";
            this.dgvBorrowedBooks.ReadOnly = true;
            this.dgvBorrowedBooks.RowHeadersVisible = false;
            this.dgvBorrowedBooks.RowHeadersWidth = 62;
            this.dgvBorrowedBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBorrowedBooks.Size = new System.Drawing.Size(1350, 495);
            this.dgvBorrowedBooks.TabIndex = 0;
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
            this.panelReturn.Location = new System.Drawing.Point(0, 585);
            this.panelReturn.Margin = new System.Windows.Forms.Padding(4);
            this.panelReturn.Name = "panelReturn";
            this.panelReturn.Size = new System.Drawing.Size(1350, 195);
            this.panelReturn.TabIndex = 1;
            // 
            // lblFineInfo
            // 
            this.lblFineInfo.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFineInfo.ForeColor = System.Drawing.Color.Red;
            this.lblFineInfo.Location = new System.Drawing.Point(22, 128);
            this.lblFineInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFineInfo.Name = "lblFineInfo";
            this.lblFineInfo.Size = new System.Drawing.Size(1050, 45);
            this.lblFineInfo.TabIndex = 0;
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Location = new System.Drawing.Point(870, 52);
            this.btnReturn.Margin = new System.Windows.Forms.Padding(4);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(210, 60);
            this.btnReturn.TabIndex = 1;
            this.btnReturn.Text = "确认归还";
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(450, 63);
            this.txtNote.Margin = new System.Windows.Forms.Padding(4);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(373, 30);
            this.txtNote.TabIndex = 2;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(345, 68);
            this.lblNote.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(100, 24);
            this.lblNote.TabIndex = 3;
            this.lblNote.Text = "备注说明：";
            // 
            // cboCondition
            // 
            this.cboCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCondition.Items.AddRange(new object[] {
            "完好",
            "轻微破损",
            "严重破损",
            "丢失"});
            this.cboCondition.Location = new System.Drawing.Point(128, 63);
            this.cboCondition.Margin = new System.Windows.Forms.Padding(4);
            this.cboCondition.Name = "cboCondition";
            this.cboCondition.Size = new System.Drawing.Size(178, 32);
            this.cboCondition.TabIndex = 4;
            this.cboCondition.SelectedIndexChanged += new System.EventHandler(this.cboCondition_SelectedIndexChanged);
            // 
            // lblCondition
            // 
            this.lblCondition.AutoSize = true;
            this.lblCondition.Location = new System.Drawing.Point(22, 68);
            this.lblCondition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(100, 24);
            this.lblCondition.TabIndex = 5;
            this.lblCondition.Text = "归还状态：";
            // 
            // lblReturnTitle
            // 
            this.lblReturnTitle.AutoSize = true;
            this.lblReturnTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblReturnTitle.Location = new System.Drawing.Point(22, 15);
            this.lblReturnTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReturnTitle.Name = "lblReturnTitle";
            this.lblReturnTitle.Size = new System.Drawing.Size(92, 27);
            this.lblReturnTitle.TabIndex = 6;
            this.lblReturnTitle.Text = "归还操作";
            // 
            // ReturnBookControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvBorrowedBooks);
            this.Controls.Add(this.panelReturn);
            this.Controls.Add(this.panelSearch);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1200, 750);
            this.Name = "ReturnBookControl";
            this.Size = new System.Drawing.Size(1350, 780);
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
        private System.Windows.Forms.ComboBox cboCardID;
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

            // 初始化借书证选择框
            CardIDSelector.InitializeCardIDComboBox(cboCardID, onlyNormal: true, allowEmpty: true);

            // 如果是读者登录，自动填充
            var user = AuthenticationService.Instance.CurrentUser;
            if (user != null && user.IsReader && !string.IsNullOrEmpty(user.CardID))
            {
                CardIDSelector.SetSelectedCardID(cboCardID, user.CardID);
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
            
            string cardID = CardIDSelector.GetSelectedCardID(cboCardID);
            if (string.IsNullOrWhiteSpace(cardID))
            {
                lblReaderInfo.Text = "请选择或输入借书证号";
                lblReaderInfo.ForeColor = System.Drawing.Color.Red;
                return;
            }

            currentCardID = cardID;

            try
            {
                // 获取读者信息
                string readerSql = @"
                    SELECT r.readername, rc.state, rc.overdate
                    FROM reader r
                    INNER JOIN readcard rc ON r.cardID = rc.cardID
                    WHERE r.cardID = @cardID";
                
                DataTable readerDt = DatabaseHelper.ExecuteQuery(readerSql,
                    DatabaseHelper.CreateParameter("@cardID", currentCardID));

                if (readerDt.Rows.Count == 0)
                {
                    lblReaderInfo.Text = "未找到该读者";
                    lblReaderInfo.ForeColor = System.Drawing.Color.Red;
                    dgvBorrowedBooks.DataSource = null;
                    return;
                }

                DataRow readerRow = readerDt.Rows[0];
                string readerName = readerRow["readername"].ToString();
                string cardState = readerRow["state"].ToString();
                DateTime overDate = Convert.ToDateTime(readerRow["overdate"]);

                lblReaderInfo.Text = $"读者姓名：{readerName} | 借书证状态：{cardState}";
                
                // 检查是否过期
                if (overDate < DateTime.Today)
                {
                    lblReaderInfo.Text += $" | ⚠️ 借书证已过期（{overDate:yyyy-MM-dd}）";
                    lblReaderInfo.ForeColor = System.Drawing.Color.OrangeRed;
                }
                else
                {
                    lblReaderInfo.ForeColor = System.Drawing.Color.Black;
                }

                // 查询已借书籍
                string sql = @"
                    SELECT bb.bookborrow_id AS ID,
                           bb.bookID AS 馆藏码, 
                           bib.bibliography_name AS 书名,
                           bib.ISBN,
                           bc.category_code AS  分类,
                           bb.borrowdate AS 借阅日期,
                           DATEADD(DAY, @borrowDays, bb.borrowdate) AS 应还日期,
                           CASE 
                               WHEN GETDATE() > DATEADD(DAY, @borrowDays, bb.borrowdate) THEN N'逾期'
                               WHEN DATEDIFF(DAY, GETDATE(), DATEADD(DAY, @borrowDays, bb.borrowdate)) <= 2 THEN N'即将到期'
                               ELSE N'正常' 
                           END AS 状态,
                           CASE 
                               WHEN GETDATE() > DATEADD(DAY, @borrowDays, bb.borrowdate) 
                               THEN DATEDIFF(DAY, DATEADD(DAY, @borrowDays, bb.borrowdate), GETDATE()) 
                               ELSE 0 
                           END AS 逾期天数,
                           COALESCE(bi.price, bib.price, 0) AS 单价,
                           bi.physical_condition AS 当前状态
                    FROM bookborrow bb
                    INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                    INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                    INNER JOIN BOOK_CATEGORY bc ON bib.category_id = bc.category_id
                    WHERE bb.cardID = @cardID AND bb.overdate IS NULL
                    ORDER BY bb.borrowdate";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@cardID", currentCardID),
                    DatabaseHelper.CreateParameter("@borrowDays", BorrowRules.BorrowDays));

                dgvBorrowedBooks.DataSource = dt;

                // 隐藏ID列
                if (dgvBorrowedBooks.Columns["ID"] != null)
                {
                    dgvBorrowedBooks.Columns["ID"].Visible = false;
                }

                // 设置逾期行的颜色
                foreach (DataGridViewRow row in dgvBorrowedBooks.Rows)
                {
                    if (row.Cells["状态"].Value?.ToString() == "逾期")
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                        row.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkRed;
                    }
                    else if (row.Cells["状态"].Value?.ToString() == "即将到期")
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                    }
                }

                if (dt.Rows.Count == 0)
                {
                    lblReaderInfo.Text += " | 暂无待归还书籍";
                }
                else
                {
                    int overdueCount = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["状态"].ToString() == "逾期")
                            overdueCount++;
                    }

                    lblReaderInfo.Text += $" | 待归还：{dt.Rows.Count}本";
                    if (overdueCount > 0)
                    {
                        lblReaderInfo.Text += $" | ⚠️ 逾期：{overdueCount}本";
                        lblReaderInfo.ForeColor = System.Drawing.Color.Red;
                    }
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
            string fineDetails = string.Empty;

            switch (cboCondition.SelectedIndex)
            {
                case 0: // 完好
                    if (overdueDays > 0)
                    {
                        fineAmount = FineCalculator.CalculateOverdueFine(price, overdueDays);
                        fineDetails = $"逾期{overdueDays}天（书价×{FineCalculator.OverduePriceRate:P0} + {overdueDays}天×¥{FineCalculator.OverdueDayRate:F2}）";
                    }
                    else
                    {
                        lblFineInfo.Text = "无罚款";
                        lblFineInfo.ForeColor = System.Drawing.Color.Green;
                        return;
                    }
                    break;
                    
                case 1: // 轻微破损
                    decimal damageFine = FineCalculator.CalculateDamagedFine(price) * 0.5m;
                    fineAmount = damageFine;
                    fineDetails = $"轻微破损（书价×{FineCalculator.DamagedRate / 2:P0}）";
                    
                    if (overdueDays > 0)
                    {
                        decimal overdueFine = FineCalculator.CalculateOverdueFine(price, overdueDays);
                        fineAmount += overdueFine;
                        fineDetails += $" + 逾期{overdueDays}天";
                    }
                    break;
                    
                case 2: // 严重破损
                    fineAmount = FineCalculator.CalculateDamagedFine(price);
                    fineDetails = $"严重破损（书价×{FineCalculator.DamagedRate:P0}）";
                    
                    if (overdueDays > 0)
                    {
                        decimal overdueFine = FineCalculator.CalculateOverdueFine(price, overdueDays);
                        fineAmount += overdueFine;
                        fineDetails += $" + 逾期{overdueDays}天";
                    }
                    break;
                    
                case 3: // 丢失
                    fineAmount = FineCalculator.CalculateLostFine(price);
                    fineDetails = $"图书丢失（按原价赔偿：¥{price:F2}）";
                    break;
            }

            lblFineInfo.Text = $"预计罚款：¥{fineAmount:F2}  |  明细：{fineDetails}";
            lblFineInfo.ForeColor = System.Drawing.Color.Red;
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
            DateTime borrowDate = Convert.ToDateTime(row.Cells["借阅日期"].Value);
            int overdueDays = Convert.ToInt32(row.Cells["逾期天数"].Value);
            decimal price = Convert.ToDecimal(row.Cells["单价"].Value);

            string condition = cboCondition.SelectedItem.ToString();
            bool isLost = cboCondition.SelectedIndex == 3;
            bool isDamaged = cboCondition.SelectedIndex >= 2;

            // 计算罚款
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
                    fineReason = $"图书《{bookName}》轻微破损";
                    if (overdueDays > 0)
                    {
                        fineAmount += FineCalculator.CalculateOverdueFine(price, overdueDays);
                        fineReason += $" + 逾期{overdueDays}天";
                    }
                    break;
                    
                case 2: // 严重破损
                    fineAmount = FineCalculator.CalculateDamagedFine(price);
                    fineReason = FineCalculator.GetFineReason(FineType.Damaged, bookName);
                    if (overdueDays > 0)
                    {
                        fineAmount += FineCalculator.CalculateOverdueFine(price, overdueDays);
                        fineReason += $" + 逾期{overdueDays}天";
                    }
                    break;
                    
                case 3: // 丢失
                    fineAmount = FineCalculator.CalculateLostFine(price);
                    fineReason = FineCalculator.GetFineReason(FineType.Lost, bookName);
                    break;
            }

            // 构建确认消息
            string confirmMsg = $"确认归还书籍《{bookName}》？\n\n";
            confirmMsg += $"馆藏码：{bookID}\n";
            confirmMsg += $"借阅日期：{borrowDate:yyyy-MM-dd}\n";
            confirmMsg += $"归还状态：{condition}\n";
            
            if (!string.IsNullOrEmpty(txtNote.Text.Trim()))
            {
                confirmMsg += $"备注说明：{txtNote.Text.Trim()}\n";
            }

            if (fineAmount > 0)
            {
                confirmMsg += $"\n⚠️ 将产生罚款：¥{fineAmount:F2}\n";
                confirmMsg += $"罚款原因：{fineReason}\n";
                confirmMsg += "\n该罚款将记录到读者账户，需到图书馆前台缴纳。";
            }
            else
            {
                confirmMsg += "\n✓ 无罚款";
            }

            if (MessageBox.Show(confirmMsg, "确认归还", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                // 更新借阅明细
                string note = txtNote.Text.Trim();
                if (overdueDays > 0 && string.IsNullOrEmpty(note))
                {
                    note = $"逾期{overdueDays}天归还";
                }
                else if (!string.IsNullOrEmpty(note) && overdueDays > 0)
                {
                    note = $"逾期{overdueDays}天；{note}";
                }

                string updateBorrowSql = @"
                    UPDATE bookborrow 
                    SET overdate = GETDATE(), add_note = @note 
                    WHERE bookborrow_id = @id";

                DatabaseHelper.ExecuteNonQuery(updateBorrowSql,
                    DatabaseHelper.CreateParameter("@id", borrowId),
                    DatabaseHelper.CreateParameter("@note", note));

                // 更新借阅记录表（如果该批次所有书都还了，更新状态）
                string updateRecordSql = @"
                    UPDATE borrow_record
                    SET overdate = GETDATE(),
                        bcomplete = @condition,
                        add_note = @note
                    WHERE borrow_record_id = (
                        SELECT borrow_record_id FROM bookborrow WHERE bookborrow_id = @id
                    )
                    AND NOT EXISTS (
                        SELECT 1 FROM bookborrow 
                        WHERE borrow_record_id = (SELECT borrow_record_id FROM bookborrow WHERE bookborrow_id = @id)
                        AND overdate IS NULL
                        AND bookborrow_id != @id
                    )";

                DatabaseHelper.ExecuteNonQuery(updateRecordSql,
                    DatabaseHelper.CreateParameter("@id", borrowId),
                    DatabaseHelper.CreateParameter("@condition", condition),
                    DatabaseHelper.CreateParameter("@note", note));

                // 更新书籍状态
                string newStatus;
                string physicalCondition;

                if (isLost)
                {
                    newStatus = "OFF_SHELF";
                    physicalCondition = "DAMAGED";
                }
                else if (isDamaged)
                {
                    newStatus = "AVAILABLE";  // 严重破损的书也可能继续流通
                    physicalCondition = "DAMAGED";
                }
                else if (cboCondition.SelectedIndex == 1) // 轻微破损
                {
                    newStatus = "AVAILABLE";
                    physicalCondition = "GOOD";  // 轻微破损仍标记为完好
                }
                else
                {
                    newStatus = "AVAILABLE";
                    physicalCondition = "GOOD";
                }

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
                        INSERT INTO fine (cardID, readername, reason, amount, fine_status, created_time)
                        VALUES (@cardID, @name, @reason, @amount, N'未支付', GETDATE())";

                    DatabaseHelper.ExecuteNonQuery(insertFineSql,
                        DatabaseHelper.CreateParameter("@cardID", currentCardID),
                        DatabaseHelper.CreateParameter("@name", readerName),
                        DatabaseHelper.CreateParameter("@reason", fineReason),
                        DatabaseHelper.CreateParameter("@amount", fineAmount));
                }

                string successMsg = "归还成功！\n\n";
                successMsg += $"书名：《{bookName}》\n";
                successMsg += $"归还状态：{condition}\n";
                
                if (fineAmount > 0)
                {
                    successMsg += $"\n已生成罚款记录：¥{fineAmount:F2}\n";
                    successMsg += "请到图书馆前台缴纳罚款。";
                }
                else
                {
                    successMsg += "\n无罚款，感谢按时归还！";
                }

                MessageBox.Show(successMsg, "归还成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
