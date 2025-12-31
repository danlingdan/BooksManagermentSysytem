using System;
using System.Data;
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblLastUpdate = new System.Windows.Forms.Label();
            this.panelStats = new System.Windows.Forms.Panel();
            this.panelStat1 = new System.Windows.Forms.Panel();
            this.lblStat1Value = new System.Windows.Forms.Label();
            this.lblStat1Title = new System.Windows.Forms.Label();
            this.panelStat2 = new System.Windows.Forms.Panel();
            this.lblStat2Value = new System.Windows.Forms.Label();
            this.lblStat2Title = new System.Windows.Forms.Label();
            this.panelStat3 = new System.Windows.Forms.Panel();
            this.lblStat3Value = new System.Windows.Forms.Label();
            this.lblStat3Title = new System.Windows.Forms.Label();
            this.panelStat4 = new System.Windows.Forms.Panel();
            this.lblStat4Value = new System.Windows.Forms.Label();
            this.lblStat4Title = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.panelOverdue = new System.Windows.Forms.Panel();
            this.lblOverdueTitle = new System.Windows.Forms.Label();
            this.dgvOverdue = new System.Windows.Forms.DataGridView();
            this.panelReservations = new System.Windows.Forms.Panel();
            this.lblReservationsTitle = new System.Windows.Forms.Label();
            this.dgvReservations = new System.Windows.Forms.DataGridView();
            this.panelHeader.SuspendLayout();
            this.panelStats.SuspendLayout();
            this.panelStat1.SuspendLayout();
            this.panelStat2.SuspendLayout();
            this.panelStat3.SuspendLayout();
            this.panelStat4.SuspendLayout();
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
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(950, 50);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Text = "📊 借阅统计仪表板";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(850, 10);
            this.btnRefresh.Size = new System.Drawing.Size(80, 30);
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblLastUpdate
            // 
            this.lblLastUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLastUpdate.ForeColor = System.Drawing.Color.White;
            this.lblLastUpdate.Location = new System.Drawing.Point(650, 15);
            this.lblLastUpdate.Size = new System.Drawing.Size(190, 20);
            this.lblLastUpdate.Text = "最后更新：";
            this.lblLastUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelStats
            // 
            this.panelStats.Controls.Add(this.panelStat4);
            this.panelStats.Controls.Add(this.panelStat3);
            this.panelStats.Controls.Add(this.panelStat2);
            this.panelStats.Controls.Add(this.panelStat1);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStats.Location = new System.Drawing.Point(0, 50);
            this.panelStats.Size = new System.Drawing.Size(950, 100);
            this.panelStats.Padding = new System.Windows.Forms.Padding(10);
            // 
            // panelStat1
            // 
            this.panelStat1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.panelStat1.Controls.Add(this.lblStat1Value);
            this.panelStat1.Controls.Add(this.lblStat1Title);
            this.panelStat1.Location = new System.Drawing.Point(20, 10);
            this.panelStat1.Size = new System.Drawing.Size(200, 80);
            // 
            // lblStat1Title
            // 
            this.lblStat1Title.ForeColor = System.Drawing.Color.White;
            this.lblStat1Title.Location = new System.Drawing.Point(10, 10);
            this.lblStat1Title.Size = new System.Drawing.Size(180, 25);
            this.lblStat1Title.Text = "今日借阅";
            this.lblStat1Title.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            // 
            // lblStat1Value
            // 
            this.lblStat1Value.Font = new System.Drawing.Font("Microsoft YaHei UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblStat1Value.ForeColor = System.Drawing.Color.White;
            this.lblStat1Value.Location = new System.Drawing.Point(10, 35);
            this.lblStat1Value.Size = new System.Drawing.Size(180, 40);
            this.lblStat1Value.Text = "0";
            // 
            // panelStat2
            // 
            this.panelStat2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.panelStat2.Controls.Add(this.lblStat2Value);
            this.panelStat2.Controls.Add(this.lblStat2Title);
            this.panelStat2.Location = new System.Drawing.Point(240, 10);
            this.panelStat2.Size = new System.Drawing.Size(200, 80);
            // 
            // lblStat2Title
            // 
            this.lblStat2Title.ForeColor = System.Drawing.Color.White;
            this.lblStat2Title.Location = new System.Drawing.Point(10, 10);
            this.lblStat2Title.Size = new System.Drawing.Size(180, 25);
            this.lblStat2Title.Text = "今日归还";
            this.lblStat2Title.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            // 
            // lblStat2Value
            // 
            this.lblStat2Value.Font = new System.Drawing.Font("Microsoft YaHei UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblStat2Value.ForeColor = System.Drawing.Color.White;
            this.lblStat2Value.Location = new System.Drawing.Point(10, 35);
            this.lblStat2Value.Size = new System.Drawing.Size(180, 40);
            this.lblStat2Value.Text = "0";
            // 
            // panelStat3
            // 
            this.panelStat3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.panelStat3.Controls.Add(this.lblStat3Value);
            this.panelStat3.Controls.Add(this.lblStat3Title);
            this.panelStat3.Location = new System.Drawing.Point(460, 10);
            this.panelStat3.Size = new System.Drawing.Size(200, 80);
            // 
            // lblStat3Title
            // 
            this.lblStat3Title.ForeColor = System.Drawing.Color.White;
            this.lblStat3Title.Location = new System.Drawing.Point(10, 10);
            this.lblStat3Title.Size = new System.Drawing.Size(180, 25);
            this.lblStat3Title.Text = "待处理预约";
            this.lblStat3Title.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            // 
            // lblStat3Value
            // 
            this.lblStat3Value.Font = new System.Drawing.Font("Microsoft YaHei UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblStat3Value.ForeColor = System.Drawing.Color.White;
            this.lblStat3Value.Location = new System.Drawing.Point(10, 35);
            this.lblStat3Value.Size = new System.Drawing.Size(180, 40);
            this.lblStat3Value.Text = "0";
            // 
            // panelStat4
            // 
            this.panelStat4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.panelStat4.Controls.Add(this.lblStat4Value);
            this.panelStat4.Controls.Add(this.lblStat4Title);
            this.panelStat4.Location = new System.Drawing.Point(680, 10);
            this.panelStat4.Size = new System.Drawing.Size(200, 80);
            // 
            // lblStat4Title
            // 
            this.lblStat4Title.ForeColor = System.Drawing.Color.White;
            this.lblStat4Title.Location = new System.Drawing.Point(10, 10);
            this.lblStat4Title.Size = new System.Drawing.Size(180, 25);
            this.lblStat4Title.Text = "逾期未还";
            this.lblStat4Title.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            // 
            // lblStat4Value
            // 
            this.lblStat4Value.Font = new System.Drawing.Font("Microsoft YaHei UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblStat4Value.ForeColor = System.Drawing.Color.White;
            this.lblStat4Value.Location = new System.Drawing.Point(10, 35);
            this.lblStat4Value.Size = new System.Drawing.Size(180, 40);
            this.lblStat4Value.Text = "0";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 150);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Size = new System.Drawing.Size(950, 400);
            this.splitContainer.SplitterDistance = 475;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.panelOverdue);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panelReservations);
            // 
            // panelOverdue
            // 
            this.panelOverdue.Controls.Add(this.dgvOverdue);
            this.panelOverdue.Controls.Add(this.lblOverdueTitle);
            this.panelOverdue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOverdue.Padding = new System.Windows.Forms.Padding(10);
            // 
            // lblOverdueTitle
            // 
            this.lblOverdueTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOverdueTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblOverdueTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.lblOverdueTitle.Location = new System.Drawing.Point(10, 10);
            this.lblOverdueTitle.Size = new System.Drawing.Size(455, 30);
            this.lblOverdueTitle.Text = "⚠️ 逾期未还书籍";
            // 
            // dgvOverdue
            // 
            this.dgvOverdue.AllowUserToAddRows = false;
            this.dgvOverdue.AllowUserToDeleteRows = false;
            this.dgvOverdue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOverdue.BackgroundColor = System.Drawing.Color.White;
            this.dgvOverdue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOverdue.Location = new System.Drawing.Point(10, 40);
            this.dgvOverdue.ReadOnly = true;
            this.dgvOverdue.RowHeadersVisible = false;
            this.dgvOverdue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // panelReservations
            // 
            this.panelReservations.Controls.Add(this.dgvReservations);
            this.panelReservations.Controls.Add(this.lblReservationsTitle);
            this.panelReservations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelReservations.Padding = new System.Windows.Forms.Padding(10);
            // 
            // lblReservationsTitle
            // 
            this.lblReservationsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReservationsTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblReservationsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.lblReservationsTitle.Location = new System.Drawing.Point(10, 10);
            this.lblReservationsTitle.Size = new System.Drawing.Size(440, 30);
            this.lblReservationsTitle.Text = "📋 待处理预约";
            // 
            // dgvReservations
            // 
            this.dgvReservations.AllowUserToAddRows = false;
            this.dgvReservations.AllowUserToDeleteRows = false;
            this.dgvReservations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReservations.BackgroundColor = System.Drawing.Color.White;
            this.dgvReservations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReservations.Location = new System.Drawing.Point(10, 40);
            this.dgvReservations.ReadOnly = true;
            this.dgvReservations.RowHeadersVisible = false;
            this.dgvReservations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // LibrarianDashboardControl
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Size = new System.Drawing.Size(950, 550);
            this.Load += new System.EventHandler(this.LibrarianDashboardControl_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelStats.ResumeLayout(false);
            this.panelStat1.ResumeLayout(false);
            this.panelStat2.ResumeLayout(false);
            this.panelStat3.ResumeLayout(false);
            this.panelStat4.ResumeLayout(false);
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
