using System;
using System.Windows.Forms;

namespace BooksManagermentSysytem.Controls.Reports
{
    /// <summary>
    /// 统计报表管理主控件
    /// 提供所有统计查询和报表功能的统一入口
    /// </summary>
    public partial class ReportManagementControl : UserControl
    {
        public ReportManagementControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabBorrowQuery = new System.Windows.Forms.TabPage();
            this.borrowQueryControl = new BooksManagermentSysytem.Controls.Reports.BorrowQueryControl();
            this.tabOverdueReport = new System.Windows.Forms.TabPage();
            this.overdueReportControl = new BooksManagermentSysytem.Controls.Reports.OverdueReportControl();
            this.tabBookHistory = new System.Windows.Forms.TabPage();
            this.bookHistoryControl = new BooksManagermentSysytem.Controls.Reports.BookHistoryControl();
            this.tabReaderHistory = new System.Windows.Forms.TabPage();
            this.readerHistoryControl = new BooksManagermentSysytem.Controls.Reports.ReaderHistoryControl();
            this.tabFineReport = new System.Windows.Forms.TabPage();
            this.fineReportControl = new BooksManagermentSysytem.Controls.Reports.FineReportControl();
            this.tabDamagedBooks = new System.Windows.Forms.TabPage();
            this.damagedBooksQueryControl = new BooksManagermentSysytem.Controls.Reports.DamagedBooksQueryControl();
            this.tabCirculationStats = new System.Windows.Forms.TabPage();
            this.circulationStatisticsControl = new BooksManagermentSysytem.Controls.Reports.CirculationStatisticsControl();
            this.tabCollectionStats = new System.Windows.Forms.TabPage();
            this.collectionStatisticsControl = new BooksManagermentSysytem.Controls.Reports.CollectionStatisticsControl();
            this.tabControl.SuspendLayout();
            this.tabBorrowQuery.SuspendLayout();
            this.tabOverdueReport.SuspendLayout();
            this.tabBookHistory.SuspendLayout();
            this.tabReaderHistory.SuspendLayout();
            this.tabFineReport.SuspendLayout();
            this.tabDamagedBooks.SuspendLayout();
            this.tabCirculationStats.SuspendLayout();
            this.tabCollectionStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabBorrowQuery);
            this.tabControl.Controls.Add(this.tabOverdueReport);
            this.tabControl.Controls.Add(this.tabBookHistory);
            this.tabControl.Controls.Add(this.tabReaderHistory);
            this.tabControl.Controls.Add(this.tabFineReport);
            this.tabControl.Controls.Add(this.tabDamagedBooks);
            this.tabControl.Controls.Add(this.tabCirculationStats);
            this.tabControl.Controls.Add(this.tabCollectionStats);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1200, 600);
            this.tabControl.TabIndex = 0;
            // 
            // tabBorrowQuery
            // 
            this.tabBorrowQuery.Controls.Add(this.borrowQueryControl);
            this.tabBorrowQuery.Location = new System.Drawing.Point(4, 33);
            this.tabBorrowQuery.Name = "tabBorrowQuery";
            this.tabBorrowQuery.Padding = new System.Windows.Forms.Padding(3);
            this.tabBorrowQuery.Size = new System.Drawing.Size(1192, 563);
            this.tabBorrowQuery.TabIndex = 0;
            this.tabBorrowQuery.Text = "借阅综合查询";
            this.tabBorrowQuery.UseVisualStyleBackColor = true;
            // 
            // borrowQueryControl
            // 
            this.borrowQueryControl.BackColor = System.Drawing.Color.White;
            this.borrowQueryControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.borrowQueryControl.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.borrowQueryControl.Location = new System.Drawing.Point(3, 3);
            this.borrowQueryControl.MinimumSize = new System.Drawing.Size(1200, 600);
            this.borrowQueryControl.Name = "borrowQueryControl";
            this.borrowQueryControl.Size = new System.Drawing.Size(1200, 600);
            this.borrowQueryControl.TabIndex = 0;
            // 
            // tabOverdueReport
            // 
            this.tabOverdueReport.Controls.Add(this.overdueReportControl);
            this.tabOverdueReport.Location = new System.Drawing.Point(4, 33);
            this.tabOverdueReport.Name = "tabOverdueReport";
            this.tabOverdueReport.Padding = new System.Windows.Forms.Padding(3);
            this.tabOverdueReport.Size = new System.Drawing.Size(1192, 683);
            this.tabOverdueReport.TabIndex = 1;
            this.tabOverdueReport.Text = "超期图书汇总";
            this.tabOverdueReport.UseVisualStyleBackColor = true;
            // 
            // overdueReportControl
            // 
            this.overdueReportControl.BackColor = System.Drawing.Color.White;
            this.overdueReportControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.overdueReportControl.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.overdueReportControl.Location = new System.Drawing.Point(3, 3);
            this.overdueReportControl.Name = "overdueReportControl";
            this.overdueReportControl.Size = new System.Drawing.Size(1186, 677);
            this.overdueReportControl.TabIndex = 0;
            // 
            // tabBookHistory
            // 
            this.tabBookHistory.Controls.Add(this.bookHistoryControl);
            this.tabBookHistory.Location = new System.Drawing.Point(4, 33);
            this.tabBookHistory.Name = "tabBookHistory";
            this.tabBookHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabBookHistory.Size = new System.Drawing.Size(1192, 683);
            this.tabBookHistory.TabIndex = 2;
            this.tabBookHistory.Text = "图书借阅史";
            this.tabBookHistory.UseVisualStyleBackColor = true;
            // 
            // bookHistoryControl
            // 
            this.bookHistoryControl.BackColor = System.Drawing.Color.White;
            this.bookHistoryControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bookHistoryControl.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.bookHistoryControl.Location = new System.Drawing.Point(3, 3);
            this.bookHistoryControl.Name = "bookHistoryControl";
            this.bookHistoryControl.Size = new System.Drawing.Size(1186, 677);
            this.bookHistoryControl.TabIndex = 0;
            // 
            // tabReaderHistory
            // 
            this.tabReaderHistory.Controls.Add(this.readerHistoryControl);
            this.tabReaderHistory.Location = new System.Drawing.Point(4, 33);
            this.tabReaderHistory.Name = "tabReaderHistory";
            this.tabReaderHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabReaderHistory.Size = new System.Drawing.Size(1192, 683);
            this.tabReaderHistory.TabIndex = 3;
            this.tabReaderHistory.Text = "读者借阅史";
            this.tabReaderHistory.UseVisualStyleBackColor = true;
            // 
            // readerHistoryControl
            // 
            this.readerHistoryControl.BackColor = System.Drawing.Color.White;
            this.readerHistoryControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.readerHistoryControl.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.readerHistoryControl.Location = new System.Drawing.Point(3, 3);
            this.readerHistoryControl.Name = "readerHistoryControl";
            this.readerHistoryControl.Size = new System.Drawing.Size(1186, 677);
            this.readerHistoryControl.TabIndex = 0;
            // 
            // tabFineReport
            // 
            this.tabFineReport.Controls.Add(this.fineReportControl);
            this.tabFineReport.Location = new System.Drawing.Point(4, 33);
            this.tabFineReport.Name = "tabFineReport";
            this.tabFineReport.Padding = new System.Windows.Forms.Padding(3);
            this.tabFineReport.Size = new System.Drawing.Size(1192, 683);
            this.tabFineReport.TabIndex = 4;
            this.tabFineReport.Text = "罚款统计";
            this.tabFineReport.UseVisualStyleBackColor = true;
            // 
            // fineReportControl
            // 
            this.fineReportControl.BackColor = System.Drawing.Color.White;
            this.fineReportControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fineReportControl.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.fineReportControl.Location = new System.Drawing.Point(3, 3);
            this.fineReportControl.Name = "fineReportControl";
            this.fineReportControl.Size = new System.Drawing.Size(1186, 677);
            this.fineReportControl.TabIndex = 0;
            // 
            // tabDamagedBooks
            // 
            this.tabDamagedBooks.Controls.Add(this.damagedBooksQueryControl);
            this.tabDamagedBooks.Location = new System.Drawing.Point(4, 33);
            this.tabDamagedBooks.Name = "tabDamagedBooks";
            this.tabDamagedBooks.Padding = new System.Windows.Forms.Padding(3);
            this.tabDamagedBooks.Size = new System.Drawing.Size(1192, 683);
            this.tabDamagedBooks.TabIndex = 5;
            this.tabDamagedBooks.Text = "损坏图书查询";
            this.tabDamagedBooks.UseVisualStyleBackColor = true;
            // 
            // damagedBooksQueryControl
            // 
            this.damagedBooksQueryControl.BackColor = System.Drawing.Color.White;
            this.damagedBooksQueryControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.damagedBooksQueryControl.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.damagedBooksQueryControl.Location = new System.Drawing.Point(3, 3);
            this.damagedBooksQueryControl.Name = "damagedBooksQueryControl";
            this.damagedBooksQueryControl.Size = new System.Drawing.Size(1186, 677);
            this.damagedBooksQueryControl.TabIndex = 0;
            // 
            // tabCirculationStats
            // 
            this.tabCirculationStats.Controls.Add(this.circulationStatisticsControl);
            this.tabCirculationStats.Location = new System.Drawing.Point(4, 33);
            this.tabCirculationStats.Name = "tabCirculationStats";
            this.tabCirculationStats.Padding = new System.Windows.Forms.Padding(3);
            this.tabCirculationStats.Size = new System.Drawing.Size(1192, 683);
            this.tabCirculationStats.TabIndex = 6;
            this.tabCirculationStats.Text = "流通统计分析";
            this.tabCirculationStats.UseVisualStyleBackColor = true;
            // 
            // circulationStatisticsControl
            // 
            this.circulationStatisticsControl.BackColor = System.Drawing.Color.White;
            this.circulationStatisticsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.circulationStatisticsControl.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.circulationStatisticsControl.Location = new System.Drawing.Point(3, 3);
            this.circulationStatisticsControl.Name = "circulationStatisticsControl";
            this.circulationStatisticsControl.Size = new System.Drawing.Size(1186, 677);
            this.circulationStatisticsControl.TabIndex = 0;
            // 
            // tabCollectionStats
            // 
            this.tabCollectionStats.Controls.Add(this.collectionStatisticsControl);
            this.tabCollectionStats.Location = new System.Drawing.Point(4, 33);
            this.tabCollectionStats.Name = "tabCollectionStats";
            this.tabCollectionStats.Padding = new System.Windows.Forms.Padding(3);
            this.tabCollectionStats.Size = new System.Drawing.Size(1192, 683);
            this.tabCollectionStats.TabIndex = 7;
            this.tabCollectionStats.Text = "馆藏统计分析";
            this.tabCollectionStats.UseVisualStyleBackColor = true;
            // 
            // collectionStatisticsControl
            // 
            this.collectionStatisticsControl.BackColor = System.Drawing.Color.White;
            this.collectionStatisticsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.collectionStatisticsControl.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.collectionStatisticsControl.Location = new System.Drawing.Point(3, 3);
            this.collectionStatisticsControl.Name = "collectionStatisticsControl";
            this.collectionStatisticsControl.Size = new System.Drawing.Size(1186, 677);
            this.collectionStatisticsControl.TabIndex = 0;
            // 
            // ReportManagementControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1200, 600);
            this.Name = "ReportManagementControl";
            this.Size = new System.Drawing.Size(1200, 600);
            this.tabControl.ResumeLayout(false);
            this.tabBorrowQuery.ResumeLayout(false);
            this.tabOverdueReport.ResumeLayout(false);
            this.tabBookHistory.ResumeLayout(false);
            this.tabReaderHistory.ResumeLayout(false);
            this.tabFineReport.ResumeLayout(false);
            this.tabDamagedBooks.ResumeLayout(false);
            this.tabCirculationStats.ResumeLayout(false);
            this.tabCollectionStats.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabBorrowQuery;
        private System.Windows.Forms.TabPage tabOverdueReport;
        private System.Windows.Forms.TabPage tabBookHistory;
        private System.Windows.Forms.TabPage tabReaderHistory;
        private System.Windows.Forms.TabPage tabFineReport;
        private System.Windows.Forms.TabPage tabDamagedBooks;
        private System.Windows.Forms.TabPage tabCirculationStats;
        private System.Windows.Forms.TabPage tabCollectionStats;
        private BorrowQueryControl borrowQueryControl;
        private OverdueReportControl overdueReportControl;
        private BookHistoryControl bookHistoryControl;
        private ReaderHistoryControl readerHistoryControl;
        private FineReportControl fineReportControl;
        private DamagedBooksQueryControl damagedBooksQueryControl;
        private CirculationStatisticsControl circulationStatisticsControl;
        private CollectionStatisticsControl collectionStatisticsControl;
    }
}
