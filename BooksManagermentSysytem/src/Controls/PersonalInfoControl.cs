using System;
using System.Data;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Models;
using BooksManagermentSysytem.Services;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 个人信息控件 - 读者查看自己的借书证和借阅信息
    /// </summary>
    public partial class PersonalInfoControl : UserControl
    {
        private Reader currentReader;

        public PersonalInfoControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panelCardInfo = new System.Windows.Forms.Panel();
            this.lblCardInfoTitle = new System.Windows.Forms.Label();
            this.lblCardID = new System.Windows.Forms.Label();
            this.lblCardIDValue = new System.Windows.Forms.Label();
            this.lblReaderName = new System.Windows.Forms.Label();
            this.lblReaderNameValue = new System.Windows.Forms.Label();
            this.lblReaderType = new System.Windows.Forms.Label();
            this.lblReaderTypeValue = new System.Windows.Forms.Label();
            this.lblUnit = new System.Windows.Forms.Label();
            this.lblUnitValue = new System.Windows.Forms.Label();
            this.lblNumber = new System.Windows.Forms.Label();
            this.lblNumberValue = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblStartDateValue = new System.Windows.Forms.Label();
            this.lblOverDate = new System.Windows.Forms.Label();
            this.lblOverDateValue = new System.Windows.Forms.Label();
            this.lblCardState = new System.Windows.Forms.Label();
            this.lblCardStateValue = new System.Windows.Forms.Label();
            this.panelBorrowInfo = new System.Windows.Forms.Panel();
            this.lblBorrowInfoTitle = new System.Windows.Forms.Label();
            this.dgvCurrentBorrows = new System.Windows.Forms.DataGridView();
            this.lblBorrowSummary = new System.Windows.Forms.Label();
            this.panelHistory = new System.Windows.Forms.Panel();
            this.lblHistoryTitle = new System.Windows.Forms.Label();
            this.dgvBorrowHistory = new System.Windows.Forms.DataGridView();
            this.panelHeader.SuspendLayout();
            this.panelCardInfo.SuspendLayout();
            this.panelBorrowInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentBorrows)).BeginInit();
            this.panelHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.panelHeader.Controls.Add(this.btnRefresh);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Size = new System.Drawing.Size(900, 50);
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Size = new System.Drawing.Size(200, 28);
            this.lblTitle.Text = "👤 个人信息";
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(800, 10);
            this.btnRefresh.Size = new System.Drawing.Size(80, 30);
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panelCardInfo
            // 
            this.panelCardInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelCardInfo.Controls.Add(this.lblCardStateValue);
            this.panelCardInfo.Controls.Add(this.lblCardState);
            this.panelCardInfo.Controls.Add(this.lblOverDateValue);
            this.panelCardInfo.Controls.Add(this.lblOverDate);
            this.panelCardInfo.Controls.Add(this.lblStartDateValue);
            this.panelCardInfo.Controls.Add(this.lblStartDate);
            this.panelCardInfo.Controls.Add(this.lblNumberValue);
            this.panelCardInfo.Controls.Add(this.lblNumber);
            this.panelCardInfo.Controls.Add(this.lblUnitValue);
            this.panelCardInfo.Controls.Add(this.lblUnit);
            this.panelCardInfo.Controls.Add(this.lblReaderTypeValue);
            this.panelCardInfo.Controls.Add(this.lblReaderType);
            this.panelCardInfo.Controls.Add(this.lblReaderNameValue);
            this.panelCardInfo.Controls.Add(this.lblReaderName);
            this.panelCardInfo.Controls.Add(this.lblCardIDValue);
            this.panelCardInfo.Controls.Add(this.lblCardID);
            this.panelCardInfo.Controls.Add(this.lblCardInfoTitle);
            this.panelCardInfo.Location = new System.Drawing.Point(20, 70);
            this.panelCardInfo.Size = new System.Drawing.Size(860, 200);
            // 
            // lblCardInfoTitle
            // 
            this.lblCardInfoTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCardInfoTitle.Location = new System.Drawing.Point(15, 15);
            this.lblCardInfoTitle.Size = new System.Drawing.Size(200, 25);
            this.lblCardInfoTitle.Text = "借书证信息";
            // 
            // lblCardID
            // 
            this.lblCardID.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCardID.Location = new System.Drawing.Point(30, 50);
            this.lblCardID.Size = new System.Drawing.Size(100, 23);
            this.lblCardID.Text = "借书证号：";
            // 
            // lblCardIDValue
            // 
            this.lblCardIDValue.Location = new System.Drawing.Point(130, 50);
            this.lblCardIDValue.Size = new System.Drawing.Size(300, 23);
            this.lblCardIDValue.Text = "-";
            // 
            // lblReaderName
            // 
            this.lblReaderName.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblReaderName.Location = new System.Drawing.Point(30, 80);
            this.lblReaderName.Size = new System.Drawing.Size(100, 23);
            this.lblReaderName.Text = "读者姓名：";
            // 
            // lblReaderNameValue
            // 
            this.lblReaderNameValue.Location = new System.Drawing.Point(130, 80);
            this.lblReaderNameValue.Size = new System.Drawing.Size(200, 23);
            this.lblReaderNameValue.Text = "-";
            // 
            // lblReaderType
            // 
            this.lblReaderType.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblReaderType.Location = new System.Drawing.Point(30, 110);
            this.lblReaderType.Size = new System.Drawing.Size(100, 23);
            this.lblReaderType.Text = "读者类型：";
            // 
            // lblReaderTypeValue
            // 
            this.lblReaderTypeValue.Location = new System.Drawing.Point(130, 110);
            this.lblReaderTypeValue.Size = new System.Drawing.Size(200, 23);
            this.lblReaderTypeValue.Text = "-";
            // 
            // lblUnit
            // 
            this.lblUnit.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUnit.Location = new System.Drawing.Point(30, 140);
            this.lblUnit.Size = new System.Drawing.Size(100, 23);
            this.lblUnit.Text = "单位/学院：";
            // 
            // lblUnitValue
            // 
            this.lblUnitValue.Location = new System.Drawing.Point(130, 140);
            this.lblUnitValue.Size = new System.Drawing.Size(300, 23);
            this.lblUnitValue.Text = "-";
            // 
            // lblNumber
            // 
            this.lblNumber.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNumber.Location = new System.Drawing.Point(30, 170);
            this.lblNumber.Size = new System.Drawing.Size(100, 23);
            this.lblNumber.Text = "学号/工号：";
            // 
            // lblNumberValue
            // 
            this.lblNumberValue.Location = new System.Drawing.Point(130, 170);
            this.lblNumberValue.Size = new System.Drawing.Size(200, 23);
            this.lblNumberValue.Text = "-";
            // 
            // lblStartDate
            // 
            this.lblStartDate.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStartDate.Location = new System.Drawing.Point(450, 50);
            this.lblStartDate.Size = new System.Drawing.Size(100, 23);
            this.lblStartDate.Text = "开始日期：";
            // 
            // lblStartDateValue
            // 
            this.lblStartDateValue.Location = new System.Drawing.Point(550, 50);
            this.lblStartDateValue.Size = new System.Drawing.Size(200, 23);
            this.lblStartDateValue.Text = "-";
            // 
            // lblOverDate
            // 
            this.lblOverDate.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblOverDate.Location = new System.Drawing.Point(450, 80);
            this.lblOverDate.Size = new System.Drawing.Size(100, 23);
            this.lblOverDate.Text = "到期日期：";
            // 
            // lblOverDateValue
            // 
            this.lblOverDateValue.Location = new System.Drawing.Point(550, 80);
            this.lblOverDateValue.Size = new System.Drawing.Size(200, 23);
            this.lblOverDateValue.Text = "-";
            // 
            // lblCardState
            // 
            this.lblCardState.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCardState.Location = new System.Drawing.Point(450, 110);
            this.lblCardState.Size = new System.Drawing.Size(100, 23);
            this.lblCardState.Text = "证件状态：";
            // 
            // lblCardStateValue
            // 
            this.lblCardStateValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCardStateValue.Location = new System.Drawing.Point(550, 110);
            this.lblCardStateValue.Size = new System.Drawing.Size(200, 23);
            this.lblCardStateValue.Text = "-";
            // 
            // panelBorrowInfo
            // 
            this.panelBorrowInfo.Controls.Add(this.lblBorrowSummary);
            this.panelBorrowInfo.Controls.Add(this.dgvCurrentBorrows);
            this.panelBorrowInfo.Controls.Add(this.lblBorrowInfoTitle);
            this.panelBorrowInfo.Location = new System.Drawing.Point(20, 285);
            this.panelBorrowInfo.Size = new System.Drawing.Size(860, 200);
            // 
            // lblBorrowInfoTitle
            // 
            this.lblBorrowInfoTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblBorrowInfoTitle.Location = new System.Drawing.Point(15, 10);
            this.lblBorrowInfoTitle.Size = new System.Drawing.Size(200, 25);
            this.lblBorrowInfoTitle.Text = "当前借阅";
            // 
            // dgvCurrentBorrows
            // 
            this.dgvCurrentBorrows.AllowUserToAddRows = false;
            this.dgvCurrentBorrows.AllowUserToDeleteRows = false;
            this.dgvCurrentBorrows.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCurrentBorrows.BackgroundColor = System.Drawing.Color.White;
            this.dgvCurrentBorrows.Location = new System.Drawing.Point(15, 40);
            this.dgvCurrentBorrows.ReadOnly = true;
            this.dgvCurrentBorrows.RowHeadersVisible = false;
            this.dgvCurrentBorrows.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCurrentBorrows.Size = new System.Drawing.Size(830, 120);
            // 
            // lblBorrowSummary
            // 
            this.lblBorrowSummary.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.lblBorrowSummary.ForeColor = System.Drawing.Color.Gray;
            this.lblBorrowSummary.Location = new System.Drawing.Point(15, 165);
            this.lblBorrowSummary.Size = new System.Drawing.Size(830, 25);
            this.lblBorrowSummary.Text = "提示：每次最多借阅3本书，借期7天。";
            // 
            // panelHistory
            // 
            this.panelHistory.Controls.Add(this.dgvBorrowHistory);
            this.panelHistory.Controls.Add(this.lblHistoryTitle);
            this.panelHistory.Location = new System.Drawing.Point(20, 500);
            this.panelHistory.Size = new System.Drawing.Size(860, 180);
            // 
            // lblHistoryTitle
            // 
            this.lblHistoryTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblHistoryTitle.Location = new System.Drawing.Point(15, 10);
            this.lblHistoryTitle.Size = new System.Drawing.Size(200, 25);
            this.lblHistoryTitle.Text = "借阅历史（最近10条）";
            // 
            // dgvBorrowHistory
            // 
            this.dgvBorrowHistory.AllowUserToAddRows = false;
            this.dgvBorrowHistory.AllowUserToDeleteRows = false;
            this.dgvBorrowHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBorrowHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvBorrowHistory.Location = new System.Drawing.Point(15, 40);
            this.dgvBorrowHistory.ReadOnly = true;
            this.dgvBorrowHistory.RowHeadersVisible = false;
            this.dgvBorrowHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBorrowHistory.Size = new System.Drawing.Size(830, 125);
            // 
            // PersonalInfoControl
            // 
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelHistory);
            this.Controls.Add(this.panelBorrowInfo);
            this.Controls.Add(this.panelCardInfo);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Size = new System.Drawing.Size(900, 700);
            this.Load += new System.EventHandler(this.PersonalInfoControl_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelCardInfo.ResumeLayout(false);
            this.panelBorrowInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentBorrows)).EndInit();
            this.panelHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowHistory)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panelCardInfo;
        private System.Windows.Forms.Label lblCardInfoTitle;
        private System.Windows.Forms.Label lblCardID;
        private System.Windows.Forms.Label lblCardIDValue;
        private System.Windows.Forms.Label lblReaderName;
        private System.Windows.Forms.Label lblReaderNameValue;
        private System.Windows.Forms.Label lblReaderType;
        private System.Windows.Forms.Label lblReaderTypeValue;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Label lblUnitValue;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.Label lblNumberValue;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblStartDateValue;
        private System.Windows.Forms.Label lblOverDate;
        private System.Windows.Forms.Label lblOverDateValue;
        private System.Windows.Forms.Label lblCardState;
        private System.Windows.Forms.Label lblCardStateValue;
        private System.Windows.Forms.Panel panelBorrowInfo;
        private System.Windows.Forms.Label lblBorrowInfoTitle;
        private System.Windows.Forms.DataGridView dgvCurrentBorrows;
        private System.Windows.Forms.Label lblBorrowSummary;
        private System.Windows.Forms.Panel panelHistory;
        private System.Windows.Forms.Label lblHistoryTitle;
        private System.Windows.Forms.DataGridView dgvBorrowHistory;

        private void PersonalInfoControl_Load(object sender, EventArgs e)
        {
            LoadPersonalInfo();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPersonalInfo();
        }

        private void LoadPersonalInfo()
        {
            var user = AuthenticationService.Instance.CurrentUser;
            if (user == null || string.IsNullOrEmpty(user.CardID))
            {
                MessageBox.Show("无法获取当前用户的借书证信息", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                LoadCardInfo(user.CardID);
                LoadCurrentBorrows(user.CardID);
                LoadBorrowHistory(user.CardID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载个人信息失败：" + ex.Message, "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCardInfo(string cardID)
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
                MessageBox.Show("未找到借书证信息", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            lblCardIDValue.Text = currentReader.CardID;
            lblReaderNameValue.Text = currentReader.ReaderName;
            lblReaderTypeValue.Text = currentReader.ReaderType;
            lblUnitValue.Text = currentReader.Unit ?? "-";
            lblNumberValue.Text = currentReader.Number ?? "-";
            lblStartDateValue.Text = currentReader.StartDate.ToString("yyyy-MM-dd");
            lblOverDateValue.Text = currentReader.OverDate.ToString("yyyy-MM-dd");
            lblCardStateValue.Text = currentReader.CardState;

            if (currentReader.IsCardValid())
            {
                lblCardStateValue.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblCardStateValue.ForeColor = System.Drawing.Color.Red;
            }

            if (currentReader.OverDate < DateTime.Today.AddDays(30))
            {
                int daysLeft = (currentReader.OverDate - DateTime.Today).Days;
                if (daysLeft > 0)
                {
                    MessageBox.Show($"您的借书证将在 {daysLeft} 天后到期，请及时续期。", 
                        "到期提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (daysLeft <= 0)
                {
                    MessageBox.Show("您的借书证已过期，请及时续期。", 
                        "到期提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void LoadCurrentBorrows(string cardID)
        {
            string sql = @"
                SELECT bb.bookID AS 馆藏码, 
                       bib.bibliography_name AS 书名,
                       bb.borrowdate AS 借阅日期,
                       DATEADD(DAY, 7, bb.borrowdate) AS 应还日期,
                       DATEDIFF(DAY, GETDATE(), DATEADD(DAY, 7, bb.borrowdate)) AS 剩余天数,
                       CASE 
                           WHEN GETDATE() > DATEADD(DAY, 7, bb.borrowdate) THEN N'逾期'
                           WHEN DATEDIFF(DAY, GETDATE(), DATEADD(DAY, 7, bb.borrowdate)) <= 2 THEN N'即将到期'
                           ELSE N'正常'
                       END AS 状态
                FROM bookborrow bb
                INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                WHERE bb.cardID = @cardID AND bb.overdate IS NULL
                ORDER BY bb.borrowdate DESC";

            DataTable dt = DatabaseHelper.ExecuteQuery(sql, 
                DatabaseHelper.CreateParameter("@cardID", cardID));

            dgvCurrentBorrows.DataSource = dt;

            int currentCount = dt.Rows.Count;
            int maxBooks = BorrowRules.MaxBooksPerBorrow;
            lblBorrowSummary.Text = $"当前已借阅：{currentCount} 本 / 最多可借：{maxBooks} 本 | 借期：{BorrowRules.BorrowDays} 天";

            dgvCurrentBorrows.CellFormatting += (s, cellArgs) =>
            {
                if (dgvCurrentBorrows.Columns[cellArgs.ColumnIndex].HeaderText == "状态" && cellArgs.Value != null)
                {
                    string status = cellArgs.Value.ToString();
                    if (status == "逾期")
                    {
                        cellArgs.CellStyle.BackColor = System.Drawing.Color.FromArgb(255, 200, 200);
                        cellArgs.CellStyle.Font = new System.Drawing.Font(dgvCurrentBorrows.Font, System.Drawing.FontStyle.Bold);
                        cellArgs.CellStyle.ForeColor = System.Drawing.Color.Red;
                    }
                    else if (status == "即将到期")
                    {
                        cellArgs.CellStyle.BackColor = System.Drawing.Color.FromArgb(255, 255, 200);
                        cellArgs.CellStyle.ForeColor = System.Drawing.Color.Orange;
                    }
                }
            };

            foreach (DataRow row in dt.Rows)
            {
                if (row["状态"].ToString() == "逾期")
                {
                    string bookName = row["书名"].ToString();
                    DateTime dueDate = Convert.ToDateTime(row["应还日期"]);
                    int overdueDays = (DateTime.Now - dueDate).Days;
                    
                    MessageBox.Show($"您借阅的《{bookName}》已逾期 {overdueDays} 天，请尽快归还！\n逾期可能产生罚款。", 
                        "逾期提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
            }
        }

        private void LoadBorrowHistory(string cardID)
        {
            string sql = @"
                SELECT TOP 10
                       bb.bookID AS 馆藏码,
                       bib.bibliography_name AS 书名,
                       bb.borrowdate AS 借阅日期,
                       bb.overdate AS 归还日期,
                       CASE 
                           WHEN bb.overdate IS NULL THEN N'未归还'
                           WHEN bb.overdate > DATEADD(DAY, 7, bb.borrowdate) THEN N'逾期归还'
                           ELSE N'正常归还'
                       END AS 状态
                FROM bookborrow bb
                INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                WHERE bb.cardID = @cardID
                ORDER BY bb.borrowdate DESC";

            DataTable dt = DatabaseHelper.ExecuteQuery(sql, 
                DatabaseHelper.CreateParameter("@cardID", cardID));

            dgvBorrowHistory.DataSource = dt;
        }
    }
}
