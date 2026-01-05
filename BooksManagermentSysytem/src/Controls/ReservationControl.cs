using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Models;
using BooksManagermentSysytem.Services;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 预约图书控件 - 完整版
    /// 规则：最多预约3本，最多2个分类，预约后3天内取书，未完成预约前不能再次预约
    /// 支持预约取书、预约取消、预约过期自动处理
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
            this.panelSelected = new System.Windows.Forms.Panel();
            this.lblRules = new System.Windows.Forms.Label();
            this.btnConfirmReservation = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.dgvSelectedBooks = new System.Windows.Forms.DataGridView();
            this.lblSelectedTitle = new System.Windows.Forms.Label();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.btnAddToReservation = new System.Windows.Forms.Button();
            this.dgvSearchResults = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.lblKeyword = new System.Windows.Forms.Label();
            this.lblSearchTitle = new System.Windows.Forms.Label();
            this.panelReader = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblReaderInfo = new System.Windows.Forms.Label();
            this.btnLoadReader = new System.Windows.Forms.Button();
            this.txtCardID = new System.Windows.Forms.TextBox();
            this.lblCardID = new System.Windows.Forms.Label();
            this.tabMyReservations = new System.Windows.Forms.TabPage();
            this.btnRefreshReservations = new System.Windows.Forms.Button();
            this.btnCancelReservation = new System.Windows.Forms.Button();
            this.dgvMyReservations = new System.Windows.Forms.DataGridView();
            this.tabControl.SuspendLayout();
            this.tabNewReservation.SuspendLayout();
            this.panelSelected.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedBooks)).BeginInit();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchResults)).BeginInit();
            this.panelReader.SuspendLayout();
            this.tabMyReservations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyReservations)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabNewReservation);
            this.tabControl.Controls.Add(this.tabMyReservations);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1350, 825);
            this.tabControl.TabIndex = 0;
            // 
            // tabNewReservation
            // 
            this.tabNewReservation.Controls.Add(this.panelSelected);
            this.tabNewReservation.Controls.Add(this.panelSearch);
            this.tabNewReservation.Controls.Add(this.panelReader);
            this.tabNewReservation.Location = new System.Drawing.Point(4, 33);
            this.tabNewReservation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabNewReservation.Name = "tabNewReservation";
            this.tabNewReservation.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabNewReservation.Size = new System.Drawing.Size(1342, 788);
            this.tabNewReservation.TabIndex = 0;
            this.tabNewReservation.Text = "新建预约";
            // 
            // panelSelected
            // 
            this.panelSelected.Controls.Add(this.lblRules);
            this.panelSelected.Controls.Add(this.btnConfirmReservation);
            this.panelSelected.Controls.Add(this.btnRemove);
            this.panelSelected.Controls.Add(this.dgvSelectedBooks);
            this.panelSelected.Controls.Add(this.lblSelectedTitle);
            this.panelSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSelected.Location = new System.Drawing.Point(4, 394);
            this.panelSelected.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelSelected.Name = "panelSelected";
            this.panelSelected.Size = new System.Drawing.Size(1334, 390);
            this.panelSelected.TabIndex = 0;
            // 
            // lblRules
            // 
            this.lblRules.ForeColor = System.Drawing.Color.Gray;
            this.lblRules.Location = new System.Drawing.Point(15, 330);
            this.lblRules.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRules.Name = "lblRules";
            this.lblRules.Size = new System.Drawing.Size(1050, 38);
            this.lblRules.TabIndex = 0;
            this.lblRules.Text = "预约规则：最多预约3本，最多2个分类，预约后需在3天内取书，否则自动取消。有未完成预约时不能再次预约。";
            // 
            // btnConfirmReservation
            // 
            this.btnConfirmReservation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnConfirmReservation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmReservation.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnConfirmReservation.ForeColor = System.Drawing.Color.White;
            this.btnConfirmReservation.Location = new System.Drawing.Point(525, 255);
            this.btnConfirmReservation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnConfirmReservation.Name = "btnConfirmReservation";
            this.btnConfirmReservation.Size = new System.Drawing.Size(225, 57);
            this.btnConfirmReservation.TabIndex = 1;
            this.btnConfirmReservation.Text = "确认预约";
            this.btnConfirmReservation.UseVisualStyleBackColor = false;
            this.btnConfirmReservation.Click += new System.EventHandler(this.btnConfirmReservation_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(1200, 52);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(112, 42);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "移除";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // dgvSelectedBooks
            // 
            this.dgvSelectedBooks.AllowUserToAddRows = false;
            this.dgvSelectedBooks.AllowUserToDeleteRows = false;
            this.dgvSelectedBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSelectedBooks.BackgroundColor = System.Drawing.Color.White;
            this.dgvSelectedBooks.ColumnHeadersHeight = 40;
            this.dgvSelectedBooks.Location = new System.Drawing.Point(15, 52);
            this.dgvSelectedBooks.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvSelectedBooks.Name = "dgvSelectedBooks";
            this.dgvSelectedBooks.ReadOnly = true;
            this.dgvSelectedBooks.RowHeadersVisible = false;
            this.dgvSelectedBooks.RowHeadersWidth = 62;
            this.dgvSelectedBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSelectedBooks.Size = new System.Drawing.Size(1170, 180);
            this.dgvSelectedBooks.TabIndex = 3;
            // 
            // lblSelectedTitle
            // 
            this.lblSelectedTitle.AutoSize = true;
            this.lblSelectedTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSelectedTitle.Location = new System.Drawing.Point(15, 12);
            this.lblSelectedTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedTitle.Name = "lblSelectedTitle";
            this.lblSelectedTitle.Size = new System.Drawing.Size(156, 25);
            this.lblSelectedTitle.TabIndex = 4;
            this.lblSelectedTitle.Text = "已选择的预约书籍";
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
            this.panelSearch.Location = new System.Drawing.Point(4, 94);
            this.panelSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(1334, 300);
            this.panelSearch.TabIndex = 1;
            // 
            // btnAddToReservation
            // 
            this.btnAddToReservation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnAddToReservation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddToReservation.ForeColor = System.Drawing.Color.White;
            this.btnAddToReservation.Location = new System.Drawing.Point(1200, 98);
            this.btnAddToReservation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddToReservation.Name = "btnAddToReservation";
            this.btnAddToReservation.Size = new System.Drawing.Size(112, 45);
            this.btnAddToReservation.TabIndex = 0;
            this.btnAddToReservation.Text = "添加";
            this.btnAddToReservation.UseVisualStyleBackColor = false;
            this.btnAddToReservation.Click += new System.EventHandler(this.btnAddToReservation_Click);
            // 
            // dgvSearchResults
            // 
            this.dgvSearchResults.AllowUserToAddRows = false;
            this.dgvSearchResults.AllowUserToDeleteRows = false;
            this.dgvSearchResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSearchResults.BackgroundColor = System.Drawing.Color.White;
            this.dgvSearchResults.ColumnHeadersHeight = 40;
            this.dgvSearchResults.Location = new System.Drawing.Point(15, 98);
            this.dgvSearchResults.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvSearchResults.Name = "dgvSearchResults";
            this.dgvSearchResults.ReadOnly = true;
            this.dgvSearchResults.RowHeadersVisible = false;
            this.dgvSearchResults.RowHeadersWidth = 62;
            this.dgvSearchResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSearchResults.Size = new System.Drawing.Size(1170, 188);
            this.dgvSearchResults.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(420, 50);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(105, 42);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "搜索";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(105, 52);
            this.txtKeyword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(298, 30);
            this.txtKeyword.TabIndex = 3;
            // 
            // lblKeyword
            // 
            this.lblKeyword.AutoSize = true;
            this.lblKeyword.Location = new System.Drawing.Point(15, 57);
            this.lblKeyword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblKeyword.Name = "lblKeyword";
            this.lblKeyword.Size = new System.Drawing.Size(82, 24);
            this.lblKeyword.TabIndex = 4;
            this.lblKeyword.Text = "关键词：";
            // 
            // lblSearchTitle
            // 
            this.lblSearchTitle.AutoSize = true;
            this.lblSearchTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSearchTitle.Location = new System.Drawing.Point(15, 12);
            this.lblSearchTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchTitle.Name = "lblSearchTitle";
            this.lblSearchTitle.Size = new System.Drawing.Size(84, 25);
            this.lblSearchTitle.TabIndex = 5;
            this.lblSearchTitle.Text = "搜索图书";
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
            this.panelReader.Location = new System.Drawing.Point(4, 4);
            this.panelReader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelReader.Name = "panelReader";
            this.panelReader.Size = new System.Drawing.Size(1334, 90);
            this.panelReader.TabIndex = 2;
            // 
            // lblMessage
            // 
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(1050, 27);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(270, 38);
            this.lblMessage.TabIndex = 0;
            // 
            // lblReaderInfo
            // 
            this.lblReaderInfo.Location = new System.Drawing.Point(510, 27);
            this.lblReaderInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReaderInfo.Name = "lblReaderInfo";
            this.lblReaderInfo.Size = new System.Drawing.Size(525, 38);
            this.lblReaderInfo.TabIndex = 1;
            // 
            // btnLoadReader
            // 
            this.btnLoadReader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnLoadReader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadReader.ForeColor = System.Drawing.Color.White;
            this.btnLoadReader.Location = new System.Drawing.Point(382, 22);
            this.btnLoadReader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLoadReader.Name = "btnLoadReader";
            this.btnLoadReader.Size = new System.Drawing.Size(105, 42);
            this.btnLoadReader.TabIndex = 2;
            this.btnLoadReader.Text = "查询";
            this.btnLoadReader.UseVisualStyleBackColor = false;
            this.btnLoadReader.Click += new System.EventHandler(this.btnLoadReader_Click);
            // 
            // txtCardID
            // 
            this.txtCardID.Location = new System.Drawing.Point(128, 26);
            this.txtCardID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.Size = new System.Drawing.Size(238, 30);
            this.txtCardID.TabIndex = 3;
            // 
            // lblCardID
            // 
            this.lblCardID.AutoSize = true;
            this.lblCardID.Location = new System.Drawing.Point(22, 30);
            this.lblCardID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCardID.Name = "lblCardID";
            this.lblCardID.Size = new System.Drawing.Size(100, 24);
            this.lblCardID.TabIndex = 4;
            this.lblCardID.Text = "借书证号：";
            // 
            // tabMyReservations
            // 
            this.tabMyReservations.Controls.Add(this.btnRefreshReservations);
            this.tabMyReservations.Controls.Add(this.btnCancelReservation);
            this.tabMyReservations.Controls.Add(this.dgvMyReservations);
            this.tabMyReservations.Location = new System.Drawing.Point(4, 33);
            this.tabMyReservations.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabMyReservations.Name = "tabMyReservations";
            this.tabMyReservations.Size = new System.Drawing.Size(1342, 788);
            this.tabMyReservations.TabIndex = 1;
            this.tabMyReservations.Text = "我的预约";
            // 
            // btnRefreshReservations
            // 
            this.btnRefreshReservations.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRefreshReservations.Location = new System.Drawing.Point(705, 720);
            this.btnRefreshReservations.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRefreshReservations.Name = "btnRefreshReservations";
            this.btnRefreshReservations.Size = new System.Drawing.Size(150, 45);
            this.btnRefreshReservations.TabIndex = 0;
            this.btnRefreshReservations.Text = "刷新";
            this.btnRefreshReservations.Click += new System.EventHandler(this.btnRefreshReservations_Click);
            // 
            // btnCancelReservation
            // 
            this.btnCancelReservation.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancelReservation.Location = new System.Drawing.Point(525, 720);
            this.btnCancelReservation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelReservation.Name = "btnCancelReservation";
            this.btnCancelReservation.Size = new System.Drawing.Size(150, 45);
            this.btnCancelReservation.TabIndex = 1;
            this.btnCancelReservation.Text = "取消预约";
            this.btnCancelReservation.Click += new System.EventHandler(this.btnCancelReservation_Click);
            // 
            // dgvMyReservations
            // 
            this.dgvMyReservations.AllowUserToAddRows = false;
            this.dgvMyReservations.AllowUserToDeleteRows = false;
            this.dgvMyReservations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMyReservations.BackgroundColor = System.Drawing.Color.White;
            this.dgvMyReservations.ColumnHeadersHeight = 40;
            this.dgvMyReservations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMyReservations.Location = new System.Drawing.Point(0, 0);
            this.dgvMyReservations.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvMyReservations.Name = "dgvMyReservations";
            this.dgvMyReservations.ReadOnly = true;
            this.dgvMyReservations.RowHeadersVisible = false;
            this.dgvMyReservations.RowHeadersWidth = 62;
            this.dgvMyReservations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMyReservations.Size = new System.Drawing.Size(1342, 788);
            this.dgvMyReservations.TabIndex = 2;
            // 
            // ReservationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1200, 750);
            this.Name = "ReservationControl";
            this.Size = new System.Drawing.Size(1350, 825);
            this.Load += new System.EventHandler(this.ReservationControl_Load);
            this.tabControl.ResumeLayout(false);
            this.tabNewReservation.ResumeLayout(false);
            this.panelSelected.ResumeLayout(false);
            this.panelSelected.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedBooks)).EndInit();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchResults)).EndInit();
            this.panelReader.ResumeLayout(false);
            this.panelReader.PerformLayout();
            this.tabMyReservations.ResumeLayout(false);
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
                string sql = @"SELECT r.readername, r.readertype, rc.state, rc.overdate 
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

                // 使用ReservationService获取预约列表
                DataTable reservations = ReservationService.GetReaderReservations(currentCardID, false);
                hasPendingReservation = reservations.Rows.Count > 0;

                lblReaderInfo.Text = $"姓名：{row["readername"]} | 类型：{row["readertype"]}";
                if (hasPendingReservation)
                {
                    lblReaderInfo.Text += $" | 有{reservations.Rows.Count}个待处理预约";
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
                           sl.location_name AS 位置, sl.location_type
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

                // 隐藏location_type列
                if (dgvSearchResults.Columns.Contains("location_type"))
                {
                    dgvSearchResults.Columns["location_type"].Visible = false;
                }
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
                CurrentStatus = row.Cells["状态"].Value.ToString(),
                LocationType = row.Cells["location_type"].Value.ToString()
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
            var data = selectedBooks.Select(b => new 
            { 
                馆藏码 = b.ItemBarcode, 
                书名 = b.BookName, 
                ISBN = b.ISBN, 
                分类 = b.CategoryCode,
                状态 = b.CurrentStatus == "BORROWED" ? "已借出（预约）" : "可借（预约）"
            }).ToList();
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

            DateTime expireTime = BorrowRules.CalculateReservationExpireTime(DateTime.Now);

            if (MessageBox.Show($"确认预约 {selectedBooks.Count} 本书籍？\n请在 {expireTime:yyyy-MM-dd HH:mm} 前取书。",
                "确认预约", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                int successCount = 0;
                string firstError = string.Empty;

                foreach (var book in selectedBooks)
                {
                    // 判断预约类型
                    string reservationType = book.LocationType == "NEW_BOOK" ? "NEW_BOOK" : "BORROW_RESERVE";

                    string errorMessage;
                    long reservationId = ReservationService.CreateReservation(
                        currentCardID, 
                        book.ItemBarcode, 
                        reservationType, 
                        out errorMessage);

                    if (reservationId > 0)
                    {
                        successCount++;
                    }
                    else if (string.IsNullOrEmpty(firstError))
                    {
                        firstError = $"《{book.BookName}》：{errorMessage}";
                    }
                }

                if (successCount > 0)
                {
                    string msg = $"成功预约 {successCount} 本书籍！\n请在 {expireTime:yyyy-MM-dd HH:mm} 前到馆取书。";
                    if (successCount < selectedBooks.Count)
                    {
                        msg += $"\n\n部分预约失败：\n{firstError}";
                    }

                    MessageBox.Show(msg, "预约完成", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    selectedBooks.Clear();
                    RefreshSelectedBooksGrid();
                    LoadReaderAndCheck();
                }
                else
                {
                    MessageBox.Show("预约失败：" + firstError, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                // 使用ReservationService获取完整预约信息
                DataTable dt = ReservationService.GetReaderReservations(currentCardID, true);

                // 格式化显示数据
                var displayData = dt.AsEnumerable().Select(row => new
                {
                    ID = row["reservation_id"],
                    馆藏码 = row["bookID"],
                    书名 = row["bibliography_name"],
                    分类 = row["category_code"],
                    类型 = row["reservation_type"].ToString() == "BORROW_RESERVE" ? "借阅预约" : "新书预约",
                    预约时间 = Convert.ToDateTime(row["reservation_time"]).ToString("yyyy-MM-dd HH:mm"),
                    过期时间 = Convert.ToDateTime(row["expire_time"]).ToString("yyyy-MM-dd HH:mm"),
                    状态 = GetReservationStatusText(row["reservation_status"].ToString()),
                    取书时间 = row["pickup_time"] != DBNull.Value ? 
                        Convert.ToDateTime(row["pickup_time"]).ToString("yyyy-MM-dd HH:mm") : "",
                    是否过期 = Convert.ToInt32(row["is_expired"])
                }).ToList();

                dgvMyReservations.DataSource = displayData;

                // 隐藏某些列
                if (dgvMyReservations.Columns.Contains("ID"))
                    dgvMyReservations.Columns["ID"].Visible = false;
                if (dgvMyReservations.Columns.Contains("是否过期"))
                    dgvMyReservations.Columns["是否过期"].Visible = false;

                // 设置过期行的颜色
                dgvMyReservations.CellFormatting += (s, e) =>
                {
                    if (e.RowIndex >= 0 && dgvMyReservations.Rows[e.RowIndex].Cells["是否过期"].Value != null)
                    {
                        int isExpired = Convert.ToInt32(dgvMyReservations.Rows[e.RowIndex].Cells["是否过期"].Value);
                        if (isExpired == 1)
                        {
                            e.CellStyle.BackColor = Color.LightPink;
                        }
                    }
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载预约失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetReservationStatusText(string status)
        {
            switch (status)
            {
                case "PENDING": return "待处理";
                case "FULFILLED": return "已完成";
                case "EXPIRED": return "已过期";
                case "CANCELLED": return "已取消";
                default: return status;
            }
        }

        private void btnRefreshReservations_Click(object sender, EventArgs e)
        {
            // 先检查并处理过期预约
            int expiredCount = ReservationService.CheckAndExpireReservations();
            if (expiredCount > 0)
            {
                MessageBox.Show($"已自动处理 {expiredCount} 个过期预约", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

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
            if (status != "待处理")
            {
                MessageBox.Show("只能取消待处理的预约", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("确定取消该预约？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                long reservationId = Convert.ToInt64(dgvMyReservations.SelectedRows[0].Cells["ID"].Value);
                string bookName = dgvMyReservations.SelectedRows[0].Cells["书名"].Value.ToString();

                string errorMessage;
                if (ReservationService.CancelReservation(reservationId, "用户手动取消", out errorMessage))
                {
                    MessageBox.Show($"已成功取消《{bookName}》的预约", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadReaderAndCheck();
                }
                else
                {
                    MessageBox.Show("取消失败：" + errorMessage, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("取消失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
