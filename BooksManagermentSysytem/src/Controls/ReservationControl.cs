using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Models;
using BooksManagermentSysytem.Services;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 预约图书控件
    /// 规则：最多预约3本，最多2个分类，预约后3天内取书，未完成预约前不能再次预约
    /// </summary>
    public partial class ReservationControl : UserControl
    {
        private List<BookItem> selectedBooks = new List<BookItem>();
        private string currentCardID;
        private bool hasPendingReservation;

        public ReservationControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabNewReservation = new System.Windows.Forms.TabPage();
            this.tabMyReservations = new System.Windows.Forms.TabPage();
            this.panelReader = new System.Windows.Forms.Panel();
            this.lblCardID = new System.Windows.Forms.Label();
            this.txtCardID = new System.Windows.Forms.TextBox();
            this.btnLoadReader = new System.Windows.Forms.Button();
            this.lblReaderInfo = new System.Windows.Forms.Label();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.lblSearchTitle = new System.Windows.Forms.Label();
            this.lblKeyword = new System.Windows.Forms.Label();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvSearchResults = new System.Windows.Forms.DataGridView();
            this.btnAddToReservation = new System.Windows.Forms.Button();
            this.panelSelected = new System.Windows.Forms.Panel();
            this.lblSelectedTitle = new System.Windows.Forms.Label();
            this.dgvSelectedBooks = new System.Windows.Forms.DataGridView();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnConfirmReservation = new System.Windows.Forms.Button();
            this.lblRules = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.dgvMyReservations = new System.Windows.Forms.DataGridView();
            this.btnCancelReservation = new System.Windows.Forms.Button();
            this.btnRefreshReservations = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabNewReservation.SuspendLayout();
            this.tabMyReservations.SuspendLayout();
            this.panelReader.SuspendLayout();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchResults)).BeginInit();
            this.panelSelected.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedBooks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyReservations)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabNewReservation);
            this.tabControl.Controls.Add(this.tabMyReservations);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Size = new System.Drawing.Size(900, 550);
            // 
            // tabNewReservation
            // 
            this.tabNewReservation.Controls.Add(this.panelSelected);
            this.tabNewReservation.Controls.Add(this.panelSearch);
            this.tabNewReservation.Controls.Add(this.panelReader);
            this.tabNewReservation.Location = new System.Drawing.Point(4, 26);
            this.tabNewReservation.Name = "tabNewReservation";
            this.tabNewReservation.Padding = new System.Windows.Forms.Padding(3);
            this.tabNewReservation.Size = new System.Drawing.Size(892, 520);
            this.tabNewReservation.Text = "新建预约";
            // 
            // tabMyReservations
            // 
            this.tabMyReservations.Controls.Add(this.btnRefreshReservations);
            this.tabMyReservations.Controls.Add(this.btnCancelReservation);
            this.tabMyReservations.Controls.Add(this.dgvMyReservations);
            this.tabMyReservations.Location = new System.Drawing.Point(4, 26);
            this.tabMyReservations.Name = "tabMyReservations";
            this.tabMyReservations.Size = new System.Drawing.Size(892, 520);
            this.tabMyReservations.Text = "我的预约";
            // 
            // panelReader
            // 
            this.panelReader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelReader.Controls.Add(this.lblMessage);
            this.panelReader.Controls.Add(this.lblReaderInfo);
            this.panelReader.Controls.Add(this.btnLoadReader);
            this.panelReader.Controls.Add(this.txtCardID);
            this.panelReader.Controls.Add(this.lblCardID);
            this.panelReader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelReader.Location = new System.Drawing.Point(3, 3);
            this.panelReader.Size = new System.Drawing.Size(886, 60);
            // 
            // lblCardID
            // 
            this.lblCardID.AutoSize = true;
            this.lblCardID.Location = new System.Drawing.Point(15, 20);
            this.lblCardID.Text = "借书证号：";
            // 
            // txtCardID
            // 
            this.txtCardID.Location = new System.Drawing.Point(85, 17);
            this.txtCardID.Size = new System.Drawing.Size(160, 23);
            // 
            // btnLoadReader
            // 
            this.btnLoadReader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnLoadReader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadReader.ForeColor = System.Drawing.Color.White;
            this.btnLoadReader.Location = new System.Drawing.Point(255, 15);
            this.btnLoadReader.Size = new System.Drawing.Size(70, 28);
            this.btnLoadReader.Text = "查询";
            this.btnLoadReader.Click += new System.EventHandler(this.btnLoadReader_Click);
            // 
            // lblReaderInfo
            // 
            this.lblReaderInfo.Location = new System.Drawing.Point(340, 18);
            this.lblReaderInfo.Size = new System.Drawing.Size(350, 25);
            // 
            // lblMessage
            // 
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(700, 18);
            this.lblMessage.Size = new System.Drawing.Size(180, 25);
            // 
            // panelSearch
            // 
            this.panelSearch.Controls.Add(this.btnAddToReservation);
            this.panelSearch.Controls.Add(this.dgvSearchResults);
            this.panelSearch.Controls.Add(this.btnSearch);
            this.panelSearch.Controls.Add(this.txtKeyword);
            this.panelSearch.Controls.Add(this.lblKeyword);
            this.panelSearch.Controls.Add(this.lblSearchTitle);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(3, 63);
            this.panelSearch.Size = new System.Drawing.Size(886, 200);
            // 
            // lblSearchTitle
            // 
            this.lblSearchTitle.AutoSize = true;
            this.lblSearchTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSearchTitle.Location = new System.Drawing.Point(10, 8);
            this.lblSearchTitle.Text = "搜索图书";
            // 
            // lblKeyword
            // 
            this.lblKeyword.AutoSize = true;
            this.lblKeyword.Location = new System.Drawing.Point(10, 38);
            this.lblKeyword.Text = "关键词：";
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(70, 35);
            this.txtKeyword.Size = new System.Drawing.Size(200, 23);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(280, 33);
            this.btnSearch.Size = new System.Drawing.Size(70, 28);
            this.btnSearch.Text = "搜索";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgvSearchResults
            // 
            this.dgvSearchResults.AllowUserToAddRows = false;
            this.dgvSearchResults.AllowUserToDeleteRows = false;
            this.dgvSearchResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSearchResults.BackgroundColor = System.Drawing.Color.White;
            this.dgvSearchResults.Location = new System.Drawing.Point(10, 65);
            this.dgvSearchResults.ReadOnly = true;
            this.dgvSearchResults.RowHeadersVisible = false;
            this.dgvSearchResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSearchResults.Size = new System.Drawing.Size(780, 125);
            // 
            // btnAddToReservation
            // 
            this.btnAddToReservation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnAddToReservation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddToReservation.ForeColor = System.Drawing.Color.White;
            this.btnAddToReservation.Location = new System.Drawing.Point(800, 65);
            this.btnAddToReservation.Size = new System.Drawing.Size(75, 30);
            this.btnAddToReservation.Text = "添加";
            this.btnAddToReservation.Click += new System.EventHandler(this.btnAddToReservation_Click);
            // 
            // panelSelected
            // 
            this.panelSelected.Controls.Add(this.lblRules);
            this.panelSelected.Controls.Add(this.btnConfirmReservation);
            this.panelSelected.Controls.Add(this.btnRemove);
            this.panelSelected.Controls.Add(this.dgvSelectedBooks);
            this.panelSelected.Controls.Add(this.lblSelectedTitle);
            this.panelSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSelected.Location = new System.Drawing.Point(3, 263);
            this.panelSelected.Size = new System.Drawing.Size(886, 254);
            // 
            // lblSelectedTitle
            // 
            this.lblSelectedTitle.AutoSize = true;
            this.lblSelectedTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSelectedTitle.Location = new System.Drawing.Point(10, 8);
            this.lblSelectedTitle.Text = "已选择的预约书籍";
            // 
            // dgvSelectedBooks
            // 
            this.dgvSelectedBooks.AllowUserToAddRows = false;
            this.dgvSelectedBooks.AllowUserToDeleteRows = false;
            this.dgvSelectedBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSelectedBooks.BackgroundColor = System.Drawing.Color.White;
            this.dgvSelectedBooks.Location = new System.Drawing.Point(10, 35);
            this.dgvSelectedBooks.ReadOnly = true;
            this.dgvSelectedBooks.RowHeadersVisible = false;
            this.dgvSelectedBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSelectedBooks.Size = new System.Drawing.Size(780, 120);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(800, 35);
            this.btnRemove.Size = new System.Drawing.Size(75, 28);
            this.btnRemove.Text = "移除";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnConfirmReservation
            // 
            this.btnConfirmReservation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnConfirmReservation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmReservation.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnConfirmReservation.ForeColor = System.Drawing.Color.White;
            this.btnConfirmReservation.Location = new System.Drawing.Point(350, 170);
            this.btnConfirmReservation.Size = new System.Drawing.Size(150, 38);
            this.btnConfirmReservation.Text = "确认预约";
            this.btnConfirmReservation.Click += new System.EventHandler(this.btnConfirmReservation_Click);
            // 
            // lblRules
            // 
            this.lblRules.ForeColor = System.Drawing.Color.Gray;
            this.lblRules.Location = new System.Drawing.Point(10, 220);
            this.lblRules.Size = new System.Drawing.Size(700, 25);
            this.lblRules.Text = "预约规则：最多预约3本，最多2个分类，预约后需在3天内取书，否则自动取消。有未完成预约时不能再次预约。";
            // 
            // dgvMyReservations
            // 
            this.dgvMyReservations.AllowUserToAddRows = false;
            this.dgvMyReservations.AllowUserToDeleteRows = false;
            this.dgvMyReservations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMyReservations.BackgroundColor = System.Drawing.Color.White;
            this.dgvMyReservations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMyReservations.ReadOnly = true;
            this.dgvMyReservations.RowHeadersVisible = false;
            this.dgvMyReservations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // btnCancelReservation
            // 
            this.btnCancelReservation.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancelReservation.Location = new System.Drawing.Point(350, 480);
            this.btnCancelReservation.Size = new System.Drawing.Size(100, 30);
            this.btnCancelReservation.Text = "取消预约";
            this.btnCancelReservation.Click += new System.EventHandler(this.btnCancelReservation_Click);
            // 
            // btnRefreshReservations
            // 
            this.btnRefreshReservations.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRefreshReservations.Location = new System.Drawing.Point(470, 480);
            this.btnRefreshReservations.Size = new System.Drawing.Size(100, 30);
            this.btnRefreshReservations.Text = "刷新";
            this.btnRefreshReservations.Click += new System.EventHandler(this.btnRefreshReservations_Click);
            // 
            // ReservationControl
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Size = new System.Drawing.Size(900, 550);
            this.Load += new System.EventHandler(this.ReservationControl_Load);
            this.tabControl.ResumeLayout(false);
            this.tabNewReservation.ResumeLayout(false);
            this.tabMyReservations.ResumeLayout(false);
            this.panelReader.ResumeLayout(false);
            this.panelReader.PerformLayout();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchResults)).EndInit();
            this.panelSelected.ResumeLayout(false);
            this.panelSelected.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedBooks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyReservations)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabNewReservation;
        private System.Windows.Forms.TabPage tabMyReservations;
        private System.Windows.Forms.Panel panelReader;
        private System.Windows.Forms.Label lblCardID;
        private System.Windows.Forms.TextBox txtCardID;
        private System.Windows.Forms.Button btnLoadReader;
        private System.Windows.Forms.Label lblReaderInfo;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Label lblSearchTitle;
        private System.Windows.Forms.Label lblKeyword;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvSearchResults;
        private System.Windows.Forms.Button btnAddToReservation;
        private System.Windows.Forms.Panel panelSelected;
        private System.Windows.Forms.Label lblSelectedTitle;
        private System.Windows.Forms.DataGridView dgvSelectedBooks;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnConfirmReservation;
        private System.Windows.Forms.Label lblRules;
        private System.Windows.Forms.DataGridView dgvMyReservations;
        private System.Windows.Forms.Button btnCancelReservation;
        private System.Windows.Forms.Button btnRefreshReservations;

        private void ReservationControl_Load(object sender, EventArgs e)
        {
            var user = AuthenticationService.Instance.CurrentUser;
            if (user != null && user.IsReader && !string.IsNullOrEmpty(user.CardID))
            {
                txtCardID.Text = user.CardID;
                LoadReaderAndCheck();
            }
            RefreshSelectedBooksGrid();
        }

        private void btnLoadReader_Click(object sender, EventArgs e)
        {
            LoadReaderAndCheck();
        }

        private void LoadReaderAndCheck()
        {
            lblMessage.Text = string.Empty;
            currentCardID = null;
            hasPendingReservation = false;

            if (string.IsNullOrWhiteSpace(txtCardID.Text))
            {
                lblReaderInfo.Text = "请输入借书证号";
                lblReaderInfo.ForeColor = System.Drawing.Color.Red;
                return;
            }

            currentCardID = txtCardID.Text.Trim();

            try
            {
                string sql = @"SELECT r.readername, rc.state, rc.overdate 
                              FROM reader r INNER JOIN readcard rc ON r.cardID = rc.cardID 
                              WHERE r.cardID = @cardID";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.CreateParameter("@cardID", currentCardID));

                if (dt.Rows.Count == 0)
                {
                    lblReaderInfo.Text = "未找到该读者";
                    lblReaderInfo.ForeColor = System.Drawing.Color.Red;
                    currentCardID = null;
                    return;
                }

                DataRow row = dt.Rows[0];
                string state = row["state"].ToString();
                DateTime overdate = Convert.ToDateTime(row["overdate"]);

                if (!CardStateHelper.CanBorrow(state, overdate))
                {
                    lblReaderInfo.Text = $"{row["readername"]} - {CardStateHelper.GetStateDescription(state, overdate)}";
                    lblReaderInfo.ForeColor = System.Drawing.Color.Red;
                    currentCardID = null;
                    return;
                }

                // 检查是否有未完成的预约
                string checkSql = @"SELECT COUNT(*) FROM book_reservation 
                                   WHERE cardID = @cardID AND reservation_status = N'PENDING'";
                int pendingCount = Convert.ToInt32(DatabaseHelper.ExecuteScalar(checkSql,
                    DatabaseHelper.CreateParameter("@cardID", currentCardID)));

                hasPendingReservation = pendingCount > 0;

                lblReaderInfo.Text = $"姓名：{row["readername"]}";
                if (hasPendingReservation)
                {
                    lblReaderInfo.Text += " | 有未完成预约";
                    lblReaderInfo.ForeColor = System.Drawing.Color.Orange;
                }
                else
                {
                    lblReaderInfo.Text += " | 可预约";
                    lblReaderInfo.ForeColor = System.Drawing.Color.Green;
                }

                LoadMyReservations();
            }
            catch (Exception ex)
            {
                lblReaderInfo.Text = "查询失败";
                lblReaderInfo.ForeColor = System.Drawing.Color.Red;
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKeyword.Text))
            {
                MessageBox.Show("请输入搜索关键词", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string sql = @"
                    SELECT bi.item_barcode AS 馆藏码, bib.bibliography_name AS 书名, bib.ISBN,
                           bc.category_code AS 分类, bi.current_status AS 状态,
                           sl.location_name AS 位置
                    FROM BOOK_ITEM bi
                    INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                    INNER JOIN BOOK_CATEGORY bc ON bib.category_id = bc.category_id
                    INNER JOIN STORAGE_LOCATION sl ON bi.location_id = sl.location_id
                    WHERE (bib.bibliography_name LIKE @keyword OR bib.ISBN LIKE @keyword 
                           OR bi.item_barcode LIKE @keyword)
                      AND bi.current_status IN (N'AVAILABLE', N'BORROWED')
                      AND sl.location_type NOT IN (N'REFERENCE', N'TOOL_ONLY')
                    ORDER BY bib.bibliography_name";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@keyword", "%" + txtKeyword.Text.Trim() + "%"));

                dgvSearchResults.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("搜索失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddToReservation_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;

            if (currentCardID == null)
            {
                lblMessage.Text = "请先查询读者";
                return;
            }

            if (hasPendingReservation)
            {
                lblMessage.Text = "有未完成预约";
                return;
            }

            if (dgvSearchResults.SelectedRows.Count == 0)
            {
                lblMessage.Text = "请选择书籍";
                return;
            }

            if (selectedBooks.Count >= BorrowRules.MaxReservations)
            {
                lblMessage.Text = $"最多预约{BorrowRules.MaxReservations}本";
                return;
            }

            DataGridViewRow row = dgvSearchResults.SelectedRows[0];
            string barcode = row.Cells["馆藏码"].Value.ToString();

            if (selectedBooks.Any(b => b.ItemBarcode == barcode))
            {
                lblMessage.Text = "已添加";
                return;
            }

            string categoryCode = row.Cells["分类"].Value.ToString();
            var currentCategories = selectedBooks.Select(b => b.CategoryCode).Distinct().ToList();
            if (!currentCategories.Contains(categoryCode) && currentCategories.Count >= BorrowRules.MaxReservationCategories)
            {
                lblMessage.Text = $"最多{BorrowRules.MaxReservationCategories}个分类";
                return;
            }

            selectedBooks.Add(new BookItem
            {
                ItemBarcode = barcode,
                BookName = row.Cells["书名"].Value.ToString(),
                ISBN = row.Cells["ISBN"].Value.ToString(),
                CategoryCode = categoryCode,
                CurrentStatus = row.Cells["状态"].Value.ToString()
            });

            RefreshSelectedBooksGrid();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvSelectedBooks.SelectedRows.Count == 0) return;
            string barcode = dgvSelectedBooks.SelectedRows[0].Cells["馆藏码"].Value?.ToString();
            selectedBooks.RemoveAll(b => b.ItemBarcode == barcode);
            RefreshSelectedBooksGrid();
        }

        private void RefreshSelectedBooksGrid()
        {
            var data = selectedBooks.Select(b => new { 馆藏码 = b.ItemBarcode, 书名 = b.BookName, ISBN = b.ISBN, 分类 = b.CategoryCode }).ToList();
            dgvSelectedBooks.DataSource = null;
            dgvSelectedBooks.DataSource = data;
        }

        private void btnConfirmReservation_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;

            if (currentCardID == null)
            {
                lblMessage.Text = "请先查询读者";
                return;
            }

            if (hasPendingReservation)
            {
                MessageBox.Show("您有未完成的预约，请先完成或取消后再进行新的预约。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedBooks.Count == 0)
            {
                lblMessage.Text = "请添加预约书籍";
                return;
            }

            DateTime expireTime = DateTime.Now.AddDays(BorrowRules.ReservationDays);

            if (MessageBox.Show($"确认预约 {selectedBooks.Count} 本书籍？\n请在 {expireTime:yyyy-MM-dd HH:mm} 前取书。",
                "确认预约", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                foreach (var book in selectedBooks)
                {
                    string sql = @"INSERT INTO book_reservation 
                        (cardID, bookID, reservation_type, expire_time, reservation_status)
                        VALUES (@cardID, @bookID, N'BORROW_RESERVE', @expire, N'PENDING')";

                    DatabaseHelper.ExecuteNonQuery(sql,
                        DatabaseHelper.CreateParameter("@cardID", currentCardID),
                        DatabaseHelper.CreateParameter("@bookID", book.ItemBarcode),
                        DatabaseHelper.CreateParameter("@expire", expireTime));

                    // 更新书籍状态为已预约
                    string updateSql = "UPDATE BOOK_ITEM SET current_status = N'RESERVED' WHERE item_barcode = @barcode";
                    DatabaseHelper.ExecuteNonQuery(updateSql, DatabaseHelper.CreateParameter("@barcode", book.ItemBarcode));
                }

                MessageBox.Show($"预约成功！请在 {expireTime:yyyy-MM-dd HH:mm} 前到馆取书。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                selectedBooks.Clear();
                RefreshSelectedBooksGrid();
                LoadReaderAndCheck();
            }
            catch (Exception ex)
            {
                MessageBox.Show("预约失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMyReservations()
        {
            if (string.IsNullOrEmpty(currentCardID)) return;

            try
            {
                string sql = @"
                    SELECT br.reservation_id AS ID, br.bookID AS 馆藏码, 
                           bib.bibliography_name AS 书名, br.reservation_type AS 类型,
                           br.reservation_time AS 预约时间, br.expire_time AS 过期时间,
                           br.reservation_status AS 状态
                    FROM book_reservation br
                    INNER JOIN BOOK_ITEM bi ON br.bookID = bi.item_barcode
                    INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                    WHERE br.cardID = @cardID
                    ORDER BY br.reservation_time DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.CreateParameter("@cardID", currentCardID));
                dgvMyReservations.DataSource = dt;
            }
            catch { }
        }

        private void btnRefreshReservations_Click(object sender, EventArgs e)
        {
            LoadMyReservations();
        }

        private void btnCancelReservation_Click(object sender, EventArgs e)
        {
            if (dgvMyReservations.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要取消的预约", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string status = dgvMyReservations.SelectedRows[0].Cells["状态"].Value?.ToString();
            if (status != "PENDING")
            {
                MessageBox.Show("只能取消待处理的预约", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("确定取消该预约？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                long reservationId = Convert.ToInt64(dgvMyReservations.SelectedRows[0].Cells["ID"].Value);
                string bookID = dgvMyReservations.SelectedRows[0].Cells["馆藏码"].Value.ToString();

                string sql = "UPDATE book_reservation SET reservation_status = N'CANCELLED' WHERE reservation_id = @id";
                DatabaseHelper.ExecuteNonQuery(sql, DatabaseHelper.CreateParameter("@id", reservationId));

                string updateSql = "UPDATE BOOK_ITEM SET current_status = N'AVAILABLE' WHERE item_barcode = @barcode";
                DatabaseHelper.ExecuteNonQuery(updateSql, DatabaseHelper.CreateParameter("@barcode", bookID));

                MessageBox.Show("预约已取消", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadReaderAndCheck();
            }
            catch (Exception ex)
            {
                MessageBox.Show("取消失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
