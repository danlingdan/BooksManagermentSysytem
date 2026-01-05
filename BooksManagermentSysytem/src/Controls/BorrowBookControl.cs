using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Models;
using BooksManagermentSysytem.Services;
using BooksManagermentSysytem.Helpers;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 借阅图书控件
    /// 规则：最多借3本，最多2个分类，借期7天
    /// 完整校验：借书证状态、未付罚款、逾期书籍、借阅数量限制
    /// </summary>
    public partial class BorrowBookControl : UserControl
    {
        private List<BookItem> selectedBooks = new List<BookItem>();
        private Reader currentReader;

        public BorrowBookControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panelReader = new System.Windows.Forms.Panel();
            this.lblReaderInfo = new System.Windows.Forms.Label();
            this.btnLoadReader = new System.Windows.Forms.Button();
            this.cboCardID = new System.Windows.Forms.ComboBox();
            this.lblCardIDInput = new System.Windows.Forms.Label();
            this.lblReaderTitle = new System.Windows.Forms.Label();
            this.panelBooks = new System.Windows.Forms.Panel();
            this.lblBooksTitle = new System.Windows.Forms.Label();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.btnAddBook = new System.Windows.Forms.Button();
            this.dgvSelectedBooks = new System.Windows.Forms.DataGridView();
            this.btnRemoveBook = new System.Windows.Forms.Button();
            this.lblRules = new System.Windows.Forms.Label();
            this.panelAction = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnBorrow = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.panelReader.SuspendLayout();
            this.panelBooks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedBooks)).BeginInit();
            this.panelAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelReader
            // 
            this.panelReader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelReader.Controls.Add(this.lblReaderInfo);
            this.panelReader.Controls.Add(this.btnLoadReader);
            this.panelReader.Controls.Add(this.cboCardID);
            this.panelReader.Controls.Add(this.lblCardIDInput);
            this.panelReader.Controls.Add(this.lblReaderTitle);
            this.panelReader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelReader.Location = new System.Drawing.Point(0, 0);
            this.panelReader.Margin = new System.Windows.Forms.Padding(4);
            this.panelReader.Name = "panelReader";
            this.panelReader.Size = new System.Drawing.Size(1350, 120);
            this.panelReader.TabIndex = 2;
            // 
            // lblReaderInfo
            // 
            this.lblReaderInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReaderInfo.Location = new System.Drawing.Point(600, 56);
            this.lblReaderInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReaderInfo.Name = "lblReaderInfo";
            this.lblReaderInfo.Size = new System.Drawing.Size(750, 60);
            this.lblReaderInfo.TabIndex = 0;
            this.lblReaderInfo.Text = "请输入借书证号并点击查询";
            // 
            // btnLoadReader
            // 
            this.btnLoadReader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnLoadReader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadReader.ForeColor = System.Drawing.Color.White;
            this.btnLoadReader.Location = new System.Drawing.Point(463, 59);
            this.btnLoadReader.Margin = new System.Windows.Forms.Padding(4);
            this.btnLoadReader.Name = "btnLoadReader";
            this.btnLoadReader.Size = new System.Drawing.Size(120, 42);
            this.btnLoadReader.TabIndex = 1;
            this.btnLoadReader.Text = "查询";
            this.btnLoadReader.UseVisualStyleBackColor = false;
            this.btnLoadReader.Click += new System.EventHandler(this.btnLoadReader_Click);
            // 
            // cboCardID
            // 
            this.cboCardID.Location = new System.Drawing.Point(128, 63);
            this.cboCardID.Margin = new System.Windows.Forms.Padding(4);
            this.cboCardID.Name = "cboCardID";
            this.cboCardID.Size = new System.Drawing.Size(327, 32);
            this.cboCardID.TabIndex = 2;
            // 
            // lblCardIDInput
            // 
            this.lblCardIDInput.AutoSize = true;
            this.lblCardIDInput.Location = new System.Drawing.Point(22, 68);
            this.lblCardIDInput.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCardIDInput.Name = "lblCardIDInput";
            this.lblCardIDInput.Size = new System.Drawing.Size(100, 24);
            this.lblCardIDInput.TabIndex = 3;
            this.lblCardIDInput.Text = "借书证号：";
            // 
            // lblReaderTitle
            // 
            this.lblReaderTitle.AutoSize = true;
            this.lblReaderTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblReaderTitle.Location = new System.Drawing.Point(22, 15);
            this.lblReaderTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReaderTitle.Name = "lblReaderTitle";
            this.lblReaderTitle.Size = new System.Drawing.Size(92, 27);
            this.lblReaderTitle.TabIndex = 4;
            this.lblReaderTitle.Text = "读者信息";
            // 
            // panelBooks
            // 
            this.panelBooks.Controls.Add(this.lblBooksTitle);
            this.panelBooks.Controls.Add(this.lblBarcode);
            this.panelBooks.Controls.Add(this.txtBarcode);
            this.panelBooks.Controls.Add(this.btnAddBook);
            this.panelBooks.Controls.Add(this.dgvSelectedBooks);
            this.panelBooks.Controls.Add(this.btnRemoveBook);
            this.panelBooks.Controls.Add(this.lblRules);
            this.panelBooks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBooks.Location = new System.Drawing.Point(0, 120);
            this.panelBooks.Margin = new System.Windows.Forms.Padding(4);
            this.panelBooks.Name = "panelBooks";
            this.panelBooks.Padding = new System.Windows.Forms.Padding(22);
            this.panelBooks.Size = new System.Drawing.Size(1350, 600);
            this.panelBooks.TabIndex = 0;
            // 
            // lblBooksTitle
            // 
            this.lblBooksTitle.AutoSize = true;
            this.lblBooksTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBooksTitle.Location = new System.Drawing.Point(22, 15);
            this.lblBooksTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBooksTitle.Name = "lblBooksTitle";
            this.lblBooksTitle.Size = new System.Drawing.Size(132, 27);
            this.lblBooksTitle.TabIndex = 0;
            this.lblBooksTitle.Text = "添加借阅书籍";
            // 
            // lblBarcode
            // 
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.Location = new System.Drawing.Point(22, 68);
            this.lblBarcode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(100, 24);
            this.lblBarcode.TabIndex = 1;
            this.lblBarcode.Text = "馆藏条码：";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(128, 63);
            this.txtBarcode.Margin = new System.Windows.Forms.Padding(4);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(268, 30);
            this.txtBarcode.TabIndex = 2;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            // 
            // btnAddBook
            // 
            this.btnAddBook.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnAddBook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddBook.ForeColor = System.Drawing.Color.White;
            this.btnAddBook.Location = new System.Drawing.Point(412, 60);
            this.btnAddBook.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddBook.Name = "btnAddBook";
            this.btnAddBook.Size = new System.Drawing.Size(120, 42);
            this.btnAddBook.TabIndex = 3;
            this.btnAddBook.Text = "添加";
            this.btnAddBook.UseVisualStyleBackColor = false;
            this.btnAddBook.Click += new System.EventHandler(this.btnAddBook_Click);
            // 
            // dgvSelectedBooks
            // 
            this.dgvSelectedBooks.AllowUserToAddRows = false;
            this.dgvSelectedBooks.AllowUserToDeleteRows = false;
            this.dgvSelectedBooks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSelectedBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSelectedBooks.BackgroundColor = System.Drawing.Color.White;
            this.dgvSelectedBooks.ColumnHeadersHeight = 40;
            this.dgvSelectedBooks.Location = new System.Drawing.Point(22, 120);
            this.dgvSelectedBooks.Margin = new System.Windows.Forms.Padding(4);
            this.dgvSelectedBooks.Name = "dgvSelectedBooks";
            this.dgvSelectedBooks.ReadOnly = true;
            this.dgvSelectedBooks.RowHeadersVisible = false;
            this.dgvSelectedBooks.RowHeadersWidth = 62;
            this.dgvSelectedBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSelectedBooks.Size = new System.Drawing.Size(2190, 825);
            this.dgvSelectedBooks.TabIndex = 4;
            // 
            // btnRemoveBook
            // 
            this.btnRemoveBook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveBook.Location = new System.Drawing.Point(2242, 120);
            this.btnRemoveBook.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemoveBook.Name = "btnRemoveBook";
            this.btnRemoveBook.Size = new System.Drawing.Size(120, 45);
            this.btnRemoveBook.TabIndex = 5;
            this.btnRemoveBook.Text = "移除";
            this.btnRemoveBook.Click += new System.EventHandler(this.btnRemoveBook_Click);
            // 
            // lblRules
            // 
            this.lblRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRules.AutoSize = true;
            this.lblRules.ForeColor = System.Drawing.Color.Gray;
            this.lblRules.Location = new System.Drawing.Point(22, 968);
            this.lblRules.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRules.Name = "lblRules";
            this.lblRules.Size = new System.Drawing.Size(505, 24);
            this.lblRules.TabIndex = 6;
            this.lblRules.Text = "借阅规则：每次最多借阅 3 本书，最多 2 个分类，借期 7 天。";
            // 
            // panelAction
            // 
            this.panelAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelAction.Controls.Add(this.lblMessage);
            this.panelAction.Controls.Add(this.btnBorrow);
            this.panelAction.Controls.Add(this.btnClear);
            this.panelAction.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelAction.Location = new System.Drawing.Point(0, 720);
            this.panelAction.Margin = new System.Windows.Forms.Padding(4);
            this.panelAction.Name = "panelAction";
            this.panelAction.Size = new System.Drawing.Size(1350, 90);
            this.panelAction.TabIndex = 1;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(22, 30);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 24);
            this.lblMessage.TabIndex = 0;
            // 
            // btnBorrow
            // 
            this.btnBorrow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnBorrow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorrow.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnBorrow.ForeColor = System.Drawing.Color.White;
            this.btnBorrow.Location = new System.Drawing.Point(450, 18);
            this.btnBorrow.Margin = new System.Windows.Forms.Padding(4);
            this.btnBorrow.Name = "btnBorrow";
            this.btnBorrow.Size = new System.Drawing.Size(225, 57);
            this.btnBorrow.TabIndex = 1;
            this.btnBorrow.Text = "确认借阅";
            this.btnBorrow.UseVisualStyleBackColor = false;
            this.btnBorrow.Click += new System.EventHandler(this.btnBorrow_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(705, 22);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(150, 48);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "清空重来";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // BorrowBookControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelBooks);
            this.Controls.Add(this.panelAction);
            this.Controls.Add(this.panelReader);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1200, 750);
            this.Name = "BorrowBookControl";
            this.Size = new System.Drawing.Size(1350, 810);
            this.Load += new System.EventHandler(this.BorrowBookControl_Load);
            this.panelReader.ResumeLayout(false);
            this.panelReader.PerformLayout();
            this.panelBooks.ResumeLayout(false);
            this.panelBooks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedBooks)).EndInit();
            this.panelAction.ResumeLayout(false);
            this.panelAction.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelReader;
        private System.Windows.Forms.Label lblReaderTitle;
        private System.Windows.Forms.Label lblCardIDInput;
        private System.Windows.Forms.ComboBox cboCardID;
        private System.Windows.Forms.Button btnLoadReader;
        private System.Windows.Forms.Label lblReaderInfo;
        private System.Windows.Forms.Panel panelBooks;
        private System.Windows.Forms.Label lblBooksTitle;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Button btnAddBook;
        private System.Windows.Forms.DataGridView dgvSelectedBooks;
        private System.Windows.Forms.Button btnRemoveBook;
        private System.Windows.Forms.Label lblRules;
        private System.Windows.Forms.Panel panelAction;
        private System.Windows.Forms.Button btnBorrow;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblMessage;

        private void BorrowBookControl_Load(object sender, EventArgs e)
        {
            // 初始化借书证选择框
            CardIDSelector.InitializeCardIDComboBox(cboCardID, onlyNormal: true, allowEmpty: true);
            
            // 如果是读者登录，自动填充借书证号
            var user = AuthenticationService.Instance.CurrentUser;
            if (user != null && user.IsReader && !string.IsNullOrEmpty(user.CardID))
            {
                CardIDSelector.SetSelectedCardID(cboCardID, user.CardID);
                LoadReader();
            }

            RefreshSelectedBooksGrid();
        }

        private void btnLoadReader_Click(object sender, EventArgs e)
        {
            LoadReader();
        }

        private void LoadReader()
        {
            lblMessage.Text = string.Empty;
            currentReader = null;

            string cardID = CardIDSelector.GetSelectedCardID(cboCardID);
            if (string.IsNullOrWhiteSpace(cardID))
            {
                lblReaderInfo.Text = "请选择或输入借书证号";
                lblReaderInfo.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                string sql = @"
                    SELECT r.cardID, r.readername, r.readertype, r.unit, r.number,
                           rc.startdate, rc.overdate, rc.state
                    FROM reader r
                    INNER JOIN readcard rc ON r.cardID = rc.cardID
                    WHERE r.cardID = @cardID";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, 
                    DatabaseHelper.CreateParameter("@cardID", cardID));

                if (dt.Rows.Count == 0)
                {
                    lblReaderInfo.Text = "未找到该借书证号对应的读者";
                    lblReaderInfo.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                DataRow row = dt.Rows[0];
                currentReader = new Reader
                {
                    CardID = row["cardID"].ToString(),
                    ReaderName = row["readername"].ToString(),
                    ReaderType = row["readertype"].ToString(),
                    Unit = row["unit"]?.ToString(),
                    Number = row["number"]?.ToString(),
                    StartDate = Convert.ToDateTime(row["startdate"]),
                    OverDate = Convert.ToDateTime(row["overdate"]),
                    CardState = row["state"].ToString()
                };

                // 检查借书证状态
                if (!currentReader.IsCardValid())
                {
                    lblReaderInfo.Text = $"姓名：{currentReader.ReaderName} | {CardStateHelper.GetStateDescription(currentReader.CardState, currentReader.OverDate)}";
                    lblReaderInfo.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // 检查是否有未支付罚款
                string fineSql = "SELECT SUM(amount) FROM fine WHERE cardID = @cardID AND fine_status = N'未支付'";
                object unpaidObj = DatabaseHelper.ExecuteScalar(fineSql,
                    DatabaseHelper.CreateParameter("@cardID", currentReader.CardID));
                decimal unpaidFines = unpaidObj != null && unpaidObj != DBNull.Value ? Convert.ToDecimal(unpaidObj) : 0;

                if (unpaidFines > 0)
                {
                    lblReaderInfo.Text = $"姓名：{currentReader.ReaderName} | 有未支付罚款 ¥{unpaidFines:F2}，请先缴纳罚款后再借阅";
                    lblReaderInfo.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // 检查是否有逾期书籍
                string overdueSql = @"
                    SELECT COUNT(*) 
                    FROM bookborrow 
                    WHERE cardID = @cardID 
                      AND overdate IS NULL 
                      AND GETDATE() > DATEADD(DAY, 7, borrowdate)";
                int overdueCount = Convert.ToInt32(DatabaseHelper.ExecuteScalar(overdueSql,
                    DatabaseHelper.CreateParameter("@cardID", currentReader.CardID)));

                if (overdueCount > 0)
                {
                    lblReaderInfo.Text = $"姓名：{currentReader.ReaderName} | 有{overdueCount}本逾期未还书籍，请先归还逾期书籍后再借阅";
                    lblReaderInfo.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // 获取当前借阅数量
                string countSql = "SELECT COUNT(*) FROM bookborrow WHERE cardID = @cardID AND overdate IS NULL";
                int borrowedCount = Convert.ToInt32(DatabaseHelper.ExecuteScalar(countSql,
                    DatabaseHelper.CreateParameter("@cardID", currentReader.CardID)));

                lblReaderInfo.Text = $"姓名：{currentReader.ReaderName} | 类型：{currentReader.ReaderType} | " +
                    $"单位：{currentReader.Unit} | 当前已借：{borrowedCount}本 | 可借阅：{BorrowRules.MaxBooksPerBorrow - borrowedCount}本 | 状态：可借阅";
                lblReaderInfo.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblReaderInfo.Text = "查询失败：" + ex.Message;
                lblReaderInfo.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddBook_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;

            if (currentReader == null || !currentReader.IsCardValid())
            {
                lblMessage.Text = "请先查询有效的读者信息";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                lblMessage.Text = "请输入馆藏条码";
                return;
            }

            string barcode = txtBarcode.Text.Trim();

            // 检查是否已添加
            if (selectedBooks.Any(b => b.ItemBarcode == barcode))
            {
                lblMessage.Text = "该书籍已在列表中";
                txtBarcode.Clear();
                txtBarcode.Focus();
                return;
            }

            // 检查数量限制
            if (selectedBooks.Count >= BorrowRules.MaxBooksPerBorrow)
            {
                lblMessage.Text = $"最多只能借阅{BorrowRules.MaxBooksPerBorrow}本书";
                return;
            }

            try
            {
                // 查询书籍信息
                string sql = @"
                    SELECT bi.item_barcode, bi.bibliography_id, bi.current_status, bi.price,
                           bib.bibliography_name, bib.ISBN, bib.price AS bib_price,
                           bc.category_code, bc.category_name, sl.location_type
                    FROM BOOK_ITEM bi
                    INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                    INNER JOIN BOOK_CATEGORY bc ON bib.category_id = bc.category_id
                    INNER JOIN STORAGE_LOCATION sl ON bi.location_id = sl.location_id
                    WHERE bi.item_barcode = @barcode";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.CreateParameter("@barcode", barcode));

                if (dt.Rows.Count == 0)
                {
                    lblMessage.Text = "未找到该馆藏条码对应的书籍";
                    return;
                }

                DataRow row = dt.Rows[0];

                // 检查状态
                string status = row["current_status"].ToString();
                if (status != "AVAILABLE")
                {
                    string statusText = status == "BORROWED" ? "已借出" :
                                       status == "RESERVED" ? "已预约" : 
                                       status == "OFF_SHELF" ? "已下架" : status;
                    lblMessage.Text = $"该书籍当前状态为 {statusText}，无法借阅";
                    return;
                }

                // 检查是否是工具书区（不可外借）
                string locationType = row["location_type"].ToString();
                if (locationType == "REFERENCE" || locationType == "TOOL_ONLY")
                {
                    lblMessage.Text = "工具书区/仅供查阅书籍不可外借";
                    return;
                }

                BookItem book = new BookItem
                {
                    ItemBarcode = row["item_barcode"].ToString(),
                    BibliographyId = Convert.ToInt32(row["bibliography_id"]),
                    CurrentStatus = status,
                    BookName = row["bibliography_name"].ToString(),
                    ISBN = row["ISBN"].ToString(),
                    CategoryCode = row["category_code"].ToString(),
                    Price = row["price"] != DBNull.Value ? Convert.ToDecimal(row["price"]) : 
                            (row["bib_price"] != DBNull.Value ? Convert.ToDecimal(row["bib_price"]) : 0)
                };

                // 检查分类限制
                var currentCategories = selectedBooks.Select(b => b.CategoryCode).Distinct().ToList();
                if (!currentCategories.Contains(book.CategoryCode) && 
                    currentCategories.Count >= BorrowRules.MaxCategoriesPerBorrow)
                {
                    lblMessage.Text = $"最多只能借阅{BorrowRules.MaxCategoriesPerBorrow}个分类的书籍，当前已选分类：{string.Join("、", currentCategories)}";
                    return;
                }

                selectedBooks.Add(book);
                RefreshSelectedBooksGrid();
                txtBarcode.Clear();
                txtBarcode.Focus();

                lblMessage.Text = $"已添加：《{book.BookName}》";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "添加失败：" + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void btnRemoveBook_Click(object sender, EventArgs e)
        {
            if (dgvSelectedBooks.SelectedRows.Count == 0) return;

            string barcode = dgvSelectedBooks.SelectedRows[0].Cells["馆藏码"].Value?.ToString();
            selectedBooks.RemoveAll(b => b.ItemBarcode == barcode);
            RefreshSelectedBooksGrid();
        }

        private void RefreshSelectedBooksGrid()
        {
            var displayData = selectedBooks.Select(b => new
            {
                馆藏码 = b.ItemBarcode,
                书名 = b.BookName,
                ISBN = b.ISBN,
                分类 = b.CategoryCode,
                单价 = b.Price?.ToString("F2") ?? "0.00"
            }).ToList();

            dgvSelectedBooks.DataSource = null;
            dgvSelectedBooks.DataSource = displayData;
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;

            if (currentReader == null || !currentReader.IsCardValid())
            {
                lblMessage.Text = "请先查询有效的读者信息";
                return;
            }

            if (selectedBooks.Count == 0)
            {
                lblMessage.Text = "请添加要借阅的书籍";
                return;
            }

            // 获取当前借阅数量
            string countSql = "SELECT COUNT(*) FROM bookborrow WHERE cardID = @cardID AND overdate IS NULL";
            int currentBorrowed = Convert.ToInt32(DatabaseHelper.ExecuteScalar(countSql,
                DatabaseHelper.CreateParameter("@cardID", currentReader.CardID)));

            string errorMessage;
            if (!BorrowRules.ValidateBorrowRequest(currentBorrowed, selectedBooks, out errorMessage))
            {
                lblMessage.Text = errorMessage;
                return;
            }

            DateTime dueDate = BorrowRules.CalculateDueDate(DateTime.Now);
            
            string confirmMsg = $"确认借阅以下 {selectedBooks.Count} 本书籍？\n\n";
            confirmMsg += "书籍列表：\n";
            foreach (var book in selectedBooks)
            {
                confirmMsg += $"  • 《{book.BookName}》 ({book.CategoryCode})\n";
            }
            confirmMsg += $"\n借期：{BorrowRules.BorrowDays}天\n";
            confirmMsg += $"应还日期：{dueDate:yyyy-MM-dd}\n";
            confirmMsg += $"\n逾期罚款规则：书价×{FineCalculator.OverduePriceRate:P0} + 每天¥{FineCalculator.OverdueDayRate:F2}";

            if (MessageBox.Show(confirmMsg, "确认借阅", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                // 创建借阅记录
                string insertRecordSql = @"
                    INSERT INTO borrow_record (cardID, borrowdate, bcomplete)
                    VALUES (@cardID, GETDATE(), N'完好');
                    SELECT SCOPE_IDENTITY();";

                object recordIdObj = DatabaseHelper.ExecuteScalar(insertRecordSql,
                    DatabaseHelper.CreateParameter("@cardID", currentReader.CardID));
                long recordId = Convert.ToInt64(recordIdObj);

                // 插入借阅明细并更新书籍状态
                foreach (var book in selectedBooks)
                {
                    string insertDetailSql = @"
                        INSERT INTO bookborrow (borrow_record_id, cardID, bookID, borrowdate)
                        VALUES (@recordId, @cardID, @bookID, GETDATE())";

                    DatabaseHelper.ExecuteNonQuery(insertDetailSql,
                        DatabaseHelper.CreateParameter("@recordId", recordId),
                        DatabaseHelper.CreateParameter("@cardID", currentReader.CardID),
                        DatabaseHelper.CreateParameter("@bookID", book.ItemBarcode));

                    // 更新书籍状态
                    string updateStatusSql = @"
                        UPDATE BOOK_ITEM 
                        SET current_status = N'BORROWED', status_changed_date = GETDATE()
                        WHERE item_barcode = @barcode";

                    DatabaseHelper.ExecuteNonQuery(updateStatusSql,
                        DatabaseHelper.CreateParameter("@barcode", book.ItemBarcode));
                }

                MessageBox.Show($"借阅成功！\n\n共借阅 {selectedBooks.Count} 本书\n应还日期：{dueDate:yyyy-MM-dd}\n\n请按时归还，避免产生罚款。",
                    "借阅成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 清空
                ClearAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("借阅失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            selectedBooks.Clear();
            RefreshSelectedBooksGrid();
            txtBarcode.Clear();
            lblMessage.Text = string.Empty;
            
            // 如果不是读者登录，也清空读者信息
            var user = AuthenticationService.Instance.CurrentUser;
            if (user == null || !user.IsReader)
            {
                CardIDSelector.SetSelectedCardID(cboCardID, "");
                currentReader = null;
                lblReaderInfo.Text = "请选择或输入借书证号并点击查询";
                lblReaderInfo.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                // 重新加载读者信息
                LoadReader();
            }
        }
    }
}
