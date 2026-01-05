using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 图书管理员仪表板控件 - 显示借阅统计和待处理事项
    /// </summary>
    public partial class LibrarianDashboardControl : UserControl
    {
        public LibrarianDashboardControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblLastUpdate = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelStats = new System.Windows.Forms.Panel();
            this.panelStat4 = new System.Windows.Forms.Panel();
            this.lblStat4Value = new System.Windows.Forms.Label();
            this.lblStat4Title = new System.Windows.Forms.Label();
            this.panelStat3 = new System.Windows.Forms.Panel();
            this.lblStat3Value = new System.Windows.Forms.Label();
            this.lblStat3Title = new System.Windows.Forms.Label();
            this.panelStat2 = new System.Windows.Forms.Panel();
            this.lblStat2Value = new System.Windows.Forms.Label();
            this.lblStat2Title = new System.Windows.Forms.Label();
            this.panelStat1 = new System.Windows.Forms.Panel();
            this.lblStat1Value = new System.Windows.Forms.Label();
            this.lblStat1Title = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.panelOverdue = new System.Windows.Forms.Panel();
            this.dgvOverdue = new System.Windows.Forms.DataGridView();
            this.lblOverdueTitle = new System.Windows.Forms.Label();
            this.panelReservations = new System.Windows.Forms.Panel();
            this.dgvReservations = new System.Windows.Forms.DataGridView();
            this.lblReservationsTitle = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelStats.SuspendLayout();
            this.panelStat4.SuspendLayout();
            this.panelStat3.SuspendLayout();
            this.panelStat2.SuspendLayout();
            this.panelStat1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panelOverdue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOverdue)).BeginInit();
            this.panelReservations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservations)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.panelHeader.Controls.Add(this.lblLastUpdate);
            this.panelHeader.Controls.Add(this.btnRefresh);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1425, 75);
            this.panelHeader.TabIndex = 2;
            // 
            // lblLastUpdate
            // 
            this.lblLastUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLastUpdate.ForeColor = System.Drawing.Color.White;
            this.lblLastUpdate.Location = new System.Drawing.Point(975, 22);
            this.lblLastUpdate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLastUpdate.Name = "lblLastUpdate";
            this.lblLastUpdate.Size = new System.Drawing.Size(285, 30);
            this.lblLastUpdate.TabIndex = 0;
            this.lblLastUpdate.Text = "最后更新：";
            this.lblLastUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(1275, 15);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 45);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(30, 18);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(260, 37);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "📊 借阅统计仪表板";
            // 
            // panelStats
            // 
            this.panelStats.Controls.Add(this.panelStat4);
            this.panelStats.Controls.Add(this.panelStat3);
            this.panelStats.Controls.Add(this.panelStat2);
            this.panelStats.Controls.Add(this.panelStat1);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStats.Location = new System.Drawing.Point(0, 75);
            this.panelStats.Margin = new System.Windows.Forms.Padding(4);
            this.panelStats.Name = "panelStats";
            this.panelStats.Padding = new System.Windows.Forms.Padding(15);
            this.panelStats.Size = new System.Drawing.Size(1425, 150);
            this.panelStats.TabIndex = 1;
            // 
            // panelStat4
            // 
            this.panelStat4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.panelStat4.Controls.Add(this.lblStat4Value);
            this.panelStat4.Controls.Add(this.lblStat4Title);
            this.panelStat4.Location = new System.Drawing.Point(1020, 15);
            this.panelStat4.Margin = new System.Windows.Forms.Padding(4);
            this.panelStat4.Name = "panelStat4";
            this.panelStat4.Size = new System.Drawing.Size(300, 120);
            this.panelStat4.TabIndex = 0;
            // 
            // lblStat4Value
            // 
            this.lblStat4Value.Font = new System.Drawing.Font("Microsoft YaHei UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblStat4Value.ForeColor = System.Drawing.Color.White;
            this.lblStat4Value.Location = new System.Drawing.Point(15, 52);
            this.lblStat4Value.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStat4Value.Name = "lblStat4Value";
            this.lblStat4Value.Size = new System.Drawing.Size(270, 60);
            this.lblStat4Value.TabIndex = 0;
            this.lblStat4Value.Text = "0";
            // 
            // lblStat4Title
            // 
            this.lblStat4Title.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.lblStat4Title.ForeColor = System.Drawing.Color.White;
            this.lblStat4Title.Location = new System.Drawing.Point(15, 15);
            this.lblStat4Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStat4Title.Name = "lblStat4Title";
            this.lblStat4Title.Size = new System.Drawing.Size(270, 38);
            this.lblStat4Title.TabIndex = 1;
            this.lblStat4Title.Text = "逾期未还";
            // 
            // panelStat3
            // 
            this.panelStat3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.panelStat3.Controls.Add(this.lblStat3Value);
            this.panelStat3.Controls.Add(this.lblStat3Title);
            this.panelStat3.Location = new System.Drawing.Point(690, 15);
            this.panelStat3.Margin = new System.Windows.Forms.Padding(4);
            this.panelStat3.Name = "panelStat3";
            this.panelStat3.Size = new System.Drawing.Size(300, 120);
            this.panelStat3.TabIndex = 1;
            // 
            // lblStat3Value
            // 
            this.lblStat3Value.Font = new System.Drawing.Font("Microsoft YaHei UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblStat3Value.ForeColor = System.Drawing.Color.White;
            this.lblStat3Value.Location = new System.Drawing.Point(15, 52);
            this.lblStat3Value.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStat3Value.Name = "lblStat3Value";
            this.lblStat3Value.Size = new System.Drawing.Size(270, 60);
            this.lblStat3Value.TabIndex = 0;
            this.lblStat3Value.Text = "0";
            // 
            // lblStat3Title
            // 
            this.lblStat3Title.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.lblStat3Title.ForeColor = System.Drawing.Color.White;
            this.lblStat3Title.Location = new System.Drawing.Point(15, 15);
            this.lblStat3Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStat3Title.Name = "lblStat3Title";
            this.lblStat3Title.Size = new System.Drawing.Size(270, 38);
            this.lblStat3Title.TabIndex = 1;
            this.lblStat3Title.Text = "待处理预约";
            // 
            // panelStat2
            // 
            this.panelStat2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.panelStat2.Controls.Add(this.lblStat2Value);
            this.panelStat2.Controls.Add(this.lblStat2Title);
            this.panelStat2.Location = new System.Drawing.Point(360, 15);
            this.panelStat2.Margin = new System.Windows.Forms.Padding(4);
            this.panelStat2.Name = "panelStat2";
            this.panelStat2.Size = new System.Drawing.Size(300, 120);
            this.panelStat2.TabIndex = 2;
            // 
            // lblStat2Value
            // 
            this.lblStat2Value.Font = new System.Drawing.Font("Microsoft YaHei UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblStat2Value.ForeColor = System.Drawing.Color.White;
            this.lblStat2Value.Location = new System.Drawing.Point(15, 52);
            this.lblStat2Value.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStat2Value.Name = "lblStat2Value";
            this.lblStat2Value.Size = new System.Drawing.Size(270, 60);
            this.lblStat2Value.TabIndex = 0;
            this.lblStat2Value.Text = "0";
            // 
            // lblStat2Title
            // 
            this.lblStat2Title.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.lblStat2Title.ForeColor = System.Drawing.Color.White;
            this.lblStat2Title.Location = new System.Drawing.Point(15, 15);
            this.lblStat2Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStat2Title.Name = "lblStat2Title";
            this.lblStat2Title.Size = new System.Drawing.Size(270, 38);
            this.lblStat2Title.TabIndex = 1;
            this.lblStat2Title.Text = "今日归还";
            // 
            // panelStat1
            // 
            this.panelStat1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.panelStat1.Controls.Add(this.lblStat1Value);
            this.panelStat1.Controls.Add(this.lblStat1Title);
            this.panelStat1.Location = new System.Drawing.Point(30, 15);
            this.panelStat1.Margin = new System.Windows.Forms.Padding(4);
            this.panelStat1.Name = "panelStat1";
            this.panelStat1.Size = new System.Drawing.Size(300, 120);
            this.panelStat1.TabIndex = 3;
            // 
            // lblStat1Value
            // 
            this.lblStat1Value.Font = new System.Drawing.Font("Microsoft YaHei UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblStat1Value.ForeColor = System.Drawing.Color.White;
            this.lblStat1Value.Location = new System.Drawing.Point(15, 52);
            this.lblStat1Value.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStat1Value.Name = "lblStat1Value";
            this.lblStat1Value.Size = new System.Drawing.Size(270, 60);
            this.lblStat1Value.TabIndex = 0;
            this.lblStat1Value.Text = "0";
            // 
            // lblStat1Title
            // 
            this.lblStat1Title.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.lblStat1Title.ForeColor = System.Drawing.Color.White;
            this.lblStat1Title.Location = new System.Drawing.Point(15, 15);
            this.lblStat1Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStat1Title.Name = "lblStat1Title";
            this.lblStat1Title.Size = new System.Drawing.Size(270, 38);
            this.lblStat1Title.TabIndex = 1;
            this.lblStat1Title.Text = "今日借阅";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 225);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.panelOverdue);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panelReservations);
            this.splitContainer.Size = new System.Drawing.Size(1425, 600);
            this.splitContainer.SplitterDistance = 712;
            this.splitContainer.SplitterWidth = 6;
            this.splitContainer.TabIndex = 0;
            // 
            // panelOverdue
            // 
            this.panelOverdue.Controls.Add(this.dgvOverdue);
            this.panelOverdue.Controls.Add(this.lblOverdueTitle);
            this.panelOverdue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOverdue.Location = new System.Drawing.Point(0, 0);
            this.panelOverdue.Margin = new System.Windows.Forms.Padding(4);
            this.panelOverdue.Name = "panelOverdue";
            this.panelOverdue.Padding = new System.Windows.Forms.Padding(15);
            this.panelOverdue.Size = new System.Drawing.Size(712, 600);
            this.panelOverdue.TabIndex = 0;
            // 
            // dgvOverdue
            // 
            this.dgvOverdue.AllowUserToAddRows = false;
            this.dgvOverdue.AllowUserToDeleteRows = false;
            this.dgvOverdue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOverdue.BackgroundColor = System.Drawing.Color.White;
            this.dgvOverdue.ColumnHeadersHeight = 40;
            this.dgvOverdue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOverdue.Location = new System.Drawing.Point(15, 60);
            this.dgvOverdue.Margin = new System.Windows.Forms.Padding(4);
            this.dgvOverdue.Name = "dgvOverdue";
            this.dgvOverdue.ReadOnly = true;
            this.dgvOverdue.RowHeadersVisible = false;
            this.dgvOverdue.RowHeadersWidth = 62;
            this.dgvOverdue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOverdue.Size = new System.Drawing.Size(682, 525);
            this.dgvOverdue.TabIndex = 0;
            // 
            // lblOverdueTitle
            // 
            this.lblOverdueTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOverdueTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblOverdueTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.lblOverdueTitle.Location = new System.Drawing.Point(15, 15);
            this.lblOverdueTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOverdueTitle.Name = "lblOverdueTitle";
            this.lblOverdueTitle.Size = new System.Drawing.Size(682, 45);
            this.lblOverdueTitle.TabIndex = 1;
            this.lblOverdueTitle.Text = "⚠️ 逾期未还书籍";
            // 
            // panelReservations
            // 
            this.panelReservations.Controls.Add(this.dgvReservations);
            this.panelReservations.Controls.Add(this.lblReservationsTitle);
            this.panelReservations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelReservations.Location = new System.Drawing.Point(0, 0);
            this.panelReservations.Margin = new System.Windows.Forms.Padding(4);
            this.panelReservations.Name = "panelReservations";
            this.panelReservations.Padding = new System.Windows.Forms.Padding(15);
            this.panelReservations.Size = new System.Drawing.Size(707, 600);
            this.panelReservations.TabIndex = 0;
            // 
            // dgvReservations
            // 
            this.dgvReservations.AllowUserToAddRows = false;
            this.dgvReservations.AllowUserToDeleteRows = false;
            this.dgvReservations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReservations.BackgroundColor = System.Drawing.Color.White;
            this.dgvReservations.ColumnHeadersHeight = 40;
            this.dgvReservations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReservations.Location = new System.Drawing.Point(15, 60);
            this.dgvReservations.Margin = new System.Windows.Forms.Padding(4);
            this.dgvReservations.Name = "dgvReservations";
            this.dgvReservations.ReadOnly = true;
            this.dgvReservations.RowHeadersVisible = false;
            this.dgvReservations.RowHeadersWidth = 62;
            this.dgvReservations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReservations.Size = new System.Drawing.Size(677, 525);
            this.dgvReservations.TabIndex = 0;
            // 
            // lblReservationsTitle
            // 
            this.lblReservationsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReservationsTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblReservationsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.lblReservationsTitle.Location = new System.Drawing.Point(15, 15);
            this.lblReservationsTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReservationsTitle.Name = "lblReservationsTitle";
            this.lblReservationsTitle.Size = new System.Drawing.Size(677, 45);
            this.lblReservationsTitle.TabIndex = 1;
            this.lblReservationsTitle.Text = "📋 待处理预约";
            // 
            // LibrarianDashboardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1200, 750);
            this.Name = "LibrarianDashboardControl";
            this.Size = new System.Drawing.Size(1425, 825);
            this.Load += new System.EventHandler(this.LibrarianDashboardControl_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelStats.ResumeLayout(false);
            this.panelStat4.ResumeLayout(false);
            this.panelStat3.ResumeLayout(false);
            this.panelStat2.ResumeLayout(false);
            this.panelStat1.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panelOverdue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOverdue)).EndInit();
            this.panelReservations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservations)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblLastUpdate;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Panel panelStat1;
        private System.Windows.Forms.Label lblStat1Title;
        private System.Windows.Forms.Label lblStat1Value;
        private System.Windows.Forms.Panel panelStat2;
        private System.Windows.Forms.Label lblStat2Title;
        private System.Windows.Forms.Label lblStat2Value;
        private System.Windows.Forms.Panel panelStat3;
        private System.Windows.Forms.Label lblStat3Title;
        private System.Windows.Forms.Label lblStat3Value;
        private System.Windows.Forms.Panel panelStat4;
        private System.Windows.Forms.Label lblStat4Title;
        private System.Windows.Forms.Label lblStat4Value;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel panelOverdue;
        private System.Windows.Forms.Label lblOverdueTitle;
        private System.Windows.Forms.DataGridView dgvOverdue;
        private System.Windows.Forms.Panel panelReservations;
        private System.Windows.Forms.Label lblReservationsTitle;
        private System.Windows.Forms.DataGridView dgvReservations;

        private void LibrarianDashboardControl_Load(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            try
            {
                // 今日借阅数
                string todayBorrowSql = "SELECT COUNT(*) FROM bookborrow WHERE CAST(borrowdate AS DATE) = CAST(GETDATE() AS DATE)";
                lblStat1Value.Text = DatabaseHelper.ExecuteScalar(todayBorrowSql)?.ToString() ?? "0";

                // 今日归还数
                string todayReturnSql = "SELECT COUNT(*) FROM bookborrow WHERE CAST(overdate AS DATE) = CAST(GETDATE() AS DATE)";
                lblStat2Value.Text = DatabaseHelper.ExecuteScalar(todayReturnSql)?.ToString() ?? "0";

                // 待处理预约数
                string pendingReservationSql = "SELECT COUNT(*) FROM book_reservation WHERE reservation_status = N'PENDING'";
                try
                {
                    lblStat3Value.Text = DatabaseHelper.ExecuteScalar(pendingReservationSql)?.ToString() ?? "0";
                }
                catch
                {
                    lblStat3Value.Text = "0";
                }

                // 逾期未还数（借期7天）
                string overdueSql = @"SELECT COUNT(*) FROM bookborrow 
                                     WHERE overdate IS NULL 
                                     AND DATEADD(DAY, 7, borrowdate) < GETDATE()";
                lblStat4Value.Text = DatabaseHelper.ExecuteScalar(overdueSql)?.ToString() ?? "0";

                // 加载逾期列表
                LoadOverdueBooks();

                // 加载待处理预约
                LoadPendingReservations();

                lblLastUpdate.Text = $"最后更新：{DateTime.Now:HH:mm:ss}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载数据失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadOverdueBooks()
        {
            try
            {
                string sql = @"
                    SELECT TOP 50 
                           bb.cardID AS 借书证号,
                           r.readername AS 读者姓名,
                           bb.bookID AS 馆藏码,
                           bib.bibliography_name AS 书名,
                           bb.borrowdate AS 借阅日期,
                           DATEADD(DAY, 7, bb.borrowdate) AS 应还日期,
                           DATEDIFF(DAY, DATEADD(DAY, 7, bb.borrowdate), GETDATE()) AS 逾期天数
                    FROM bookborrow bb
                    INNER JOIN reader r ON bb.cardID = r.cardID
                    INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                    INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                    WHERE bb.overdate IS NULL 
                    AND DATEADD(DAY, 7, bb.borrowdate) < GETDATE()
                    ORDER BY bb.borrowdate ASC";

                dgvOverdue.DataSource = DatabaseHelper.ExecuteQuery(sql);

                // 设置逾期天数列颜色
                dgvOverdue.CellFormatting += (s, e) =>
                {
                    if (dgvOverdue.Columns[e.ColumnIndex].HeaderText == "逾期天数" && e.Value != null)
                    {
                        int days;
                        if (int.TryParse(e.Value.ToString(), out days))
                        {
                            if (days > 14)
                            {
                                e.CellStyle.BackColor = System.Drawing.Color.FromArgb(255, 200, 200);
                                e.CellStyle.Font = new System.Drawing.Font(dgvOverdue.Font, System.Drawing.FontStyle.Bold);
                            }
                            else if (days > 7)
                            {
                                e.CellStyle.BackColor = System.Drawing.Color.FromArgb(255, 230, 200);
                            }
                        }
                    }
                };
            }
            catch
            {
                dgvOverdue.DataSource = null;
            }
        }

        private void LoadPendingReservations()
        {
            try
            {
                string sql = @"
                    SELECT TOP 50
                           br.cardID AS 借书证号,
                           r.readername AS 读者姓名,
                           br.bookID AS 馆藏码,
                           bib.bibliography_name AS 书名,
                           br.reservation_time AS 预约时间,
                           br.expire_time AS 过期时间
                    FROM book_reservation br
                    INNER JOIN reader r ON br.cardID = r.cardID
                    INNER JOIN BOOK_ITEM bi ON br.bookID = bi.item_barcode
                    INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                    WHERE br.reservation_status = N'PENDING'
                    ORDER BY br.reservation_time ASC";

                dgvReservations.DataSource = DatabaseHelper.ExecuteQuery(sql);
            }
            catch
            {
                // 如果表不存在，显示空
                dgvReservations.DataSource = null;
            }
        }
    }
}
