using System;
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
    /// 图书续借管理控件
    /// </summary>
    public partial class RenewManagementControl : UserControl
    {
        private Reader currentReader;
        private DataTable renewableBooks;

        public RenewManagementControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panelReader = new System.Windows.Forms.Panel();
            this.lblReaderInfo = new System.Windows.Forms.Label();
            this.btnLoadReader = new System.Windows.Forms.Button();
            this.txtCardID = new System.Windows.Forms.TextBox();
            this.lblCardIDInput = new System.Windows.Forms.Label();
            this.lblReaderTitle = new System.Windows.Forms.Label();
            this.panelBooks = new System.Windows.Forms.Panel();
            this.lblBooksTitle = new System.Windows.Forms.Label();
            this.dgvRenewableBooks = new System.Windows.Forms.DataGridView();
            this.btnRenew = new System.Windows.Forms.Button();
            this.lblRenewInfo = new System.Windows.Forms.Label();
            this.lblRules = new System.Windows.Forms.Label();
            this.panelAction = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panelReader.SuspendLayout();
            this.panelBooks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRenewableBooks)).BeginInit();
            this.panelAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelReader
            // 
            this.panelReader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelReader.Controls.Add(this.lblReaderInfo);
            this.panelReader.Controls.Add(this.btnLoadReader);
            this.panelReader.Controls.Add(this.txtCardID);
            this.panelReader.Controls.Add(this.lblCardIDInput);
            this.panelReader.Controls.Add(this.lblReaderTitle);
            this.panelReader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelReader.Location = new System.Drawing.Point(0, 0);
            this.panelReader.Name = "panelReader";
            this.panelReader.Size = new System.Drawing.Size(1350, 120);
            this.panelReader.TabIndex = 0;
            // 
            // lblReaderInfo
            // 
            this.lblReaderInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReaderInfo.Location = new System.Drawing.Point(555, 52);
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
            this.btnLoadReader.Location = new System.Drawing.Point(412, 60);
            this.btnLoadReader.Name = "btnLoadReader";
            this.btnLoadReader.Size = new System.Drawing.Size(120, 42);
            this.btnLoadReader.TabIndex = 1;
            this.btnLoadReader.Text = "查询";
            this.btnLoadReader.UseVisualStyleBackColor = false;
            this.btnLoadReader.Click += new System.EventHandler(this.btnLoadReader_Click);
            // 
            // txtCardID
            // 
            this.txtCardID.Location = new System.Drawing.Point(128, 63);
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.Size = new System.Drawing.Size(268, 30);
            this.txtCardID.TabIndex = 0;
            this.txtCardID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardID_KeyDown);
            // 
            // lblCardIDInput
            // 
            this.lblCardIDInput.AutoSize = true;
            this.lblCardIDInput.Location = new System.Drawing.Point(22, 68);
            this.lblCardIDInput.Name = "lblCardIDInput";
            this.lblCardIDInput.Size = new System.Drawing.Size(100, 24);
            this.lblCardIDInput.TabIndex = 0;
            this.lblCardIDInput.Text = "借书证号：";
            // 
            // lblReaderTitle
            // 
            this.lblReaderTitle.AutoSize = true;
            this.lblReaderTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblReaderTitle.Location = new System.Drawing.Point(22, 15);
            this.lblReaderTitle.Name = "lblReaderTitle";
            this.lblReaderTitle.Size = new System.Drawing.Size(92, 27);
            this.lblReaderTitle.TabIndex = 0;
            this.lblReaderTitle.Text = "读者信息";
            // 
            // panelBooks
            // 
            this.panelBooks.Controls.Add(this.lblBooksTitle);
            this.panelBooks.Controls.Add(this.dgvRenewableBooks);
            this.panelBooks.Controls.Add(this.btnRenew);
            this.panelBooks.Controls.Add(this.lblRenewInfo);
            this.panelBooks.Controls.Add(this.lblRules);
            this.panelBooks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBooks.Location = new System.Drawing.Point(0, 120);
            this.panelBooks.Name = "panelBooks";
            this.panelBooks.Padding = new System.Windows.Forms.Padding(22);
            this.panelBooks.Size = new System.Drawing.Size(1350, 600);
            this.panelBooks.TabIndex = 1;
            // 
            // lblBooksTitle
            // 
            this.lblBooksTitle.AutoSize = true;
            this.lblBooksTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBooksTitle.Location = new System.Drawing.Point(22, 15);
            this.lblBooksTitle.Name = "lblBooksTitle";
            this.lblBooksTitle.Size = new System.Drawing.Size(132, 27);
            this.lblBooksTitle.TabIndex = 0;
            this.lblBooksTitle.Text = "可续借书籍列表";
            // 
            // dgvRenewableBooks
            // 
            this.dgvRenewableBooks.AllowUserToAddRows = false;
            this.dgvRenewableBooks.AllowUserToDeleteRows = false;
            this.dgvRenewableBooks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRenewableBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRenewableBooks.BackgroundColor = System.Drawing.Color.White;
            this.dgvRenewableBooks.ColumnHeadersHeight = 40;
            this.dgvRenewableBooks.Location = new System.Drawing.Point(22, 60);
            this.dgvRenewableBooks.MultiSelect = false;
            this.dgvRenewableBooks.Name = "dgvRenewableBooks";
            this.dgvRenewableBooks.ReadOnly = true;
            this.dgvRenewableBooks.RowHeadersVisible = false;
            this.dgvRenewableBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRenewableBooks.Size = new System.Drawing.Size(1100, 450);
            this.dgvRenewableBooks.TabIndex = 0;
            this.dgvRenewableBooks.SelectionChanged += new System.EventHandler(this.dgvRenewableBooks_SelectionChanged);
            // 
            // btnRenew
            // 
            this.btnRenew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRenew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnRenew.Enabled = false;
            this.btnRenew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRenew.ForeColor = System.Drawing.Color.White;
            this.btnRenew.Location = new System.Drawing.Point(1140, 60);
            this.btnRenew.Name = "btnRenew";
            this.btnRenew.Size = new System.Drawing.Size(150, 45);
            this.btnRenew.TabIndex = 1;
            this.btnRenew.Text = "办理续借";
            this.btnRenew.UseVisualStyleBackColor = false;
            this.btnRenew.Click += new System.EventHandler(this.btnRenew_Click);
            // 
            // lblRenewInfo
            // 
            this.lblRenewInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRenewInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(225)))));
            this.lblRenewInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRenewInfo.Location = new System.Drawing.Point(1140, 120);
            this.lblRenewInfo.Name = "lblRenewInfo";
            this.lblRenewInfo.Padding = new System.Windows.Forms.Padding(10);
            this.lblRenewInfo.Size = new System.Drawing.Size(188, 240);
            this.lblRenewInfo.TabIndex = 0;
            this.lblRenewInfo.Text = "请选择要续借的书籍";
            // 
            // lblRules
            // 
            this.lblRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRules.AutoSize = true;
            this.lblRules.ForeColor = System.Drawing.Color.Gray;
            this.lblRules.Location = new System.Drawing.Point(22, 525);
            this.lblRules.Name = "lblRules";
            this.lblRules.Size = new System.Drawing.Size(600, 24);
            this.lblRules.TabIndex = 0;
            this.lblRules.Text = "续借规则：每本书可续借次数根据读者类型不同而不同，续借后延长相应天数。";
            // 
            // panelAction
            // 
            this.panelAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelAction.Controls.Add(this.lblMessage);
            this.panelAction.Controls.Add(this.btnRefresh);
            this.panelAction.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelAction.Location = new System.Drawing.Point(0, 720);
            this.panelAction.Name = "panelAction";
            this.panelAction.Size = new System.Drawing.Size(1350, 90);
            this.panelAction.TabIndex = 2;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(22, 30);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 24);
            this.lblMessage.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(450, 22);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(150, 48);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "刷新列表";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // RenewManagementControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelBooks);
            this.Controls.Add(this.panelAction);
            this.Controls.Add(this.panelReader);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1200, 750);
            this.Name = "RenewManagementControl";
            this.Size = new System.Drawing.Size(1350, 810);
            this.Load += new System.EventHandler(this.RenewManagementControl_Load);
            this.panelReader.ResumeLayout(false);
            this.panelReader.PerformLayout();
            this.panelBooks.ResumeLayout(false);
            this.panelBooks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRenewableBooks)).EndInit();
            this.panelAction.ResumeLayout(false);
            this.panelAction.PerformLayout();
            this.ResumeLayout(false);
        }

        private Panel panelReader;
        private Label lblReaderTitle;
        private Label lblCardIDInput;
        private TextBox txtCardID;
        private Button btnLoadReader;
        private Label lblReaderInfo;
        private Panel panelBooks;
        private Label lblBooksTitle;
        private DataGridView dgvRenewableBooks;
        private Button btnRenew;
        private Label lblRenewInfo;
        private Label lblRules;
        private Panel panelAction;
        private Button btnRefresh;
        private Label lblMessage;

        private void RenewManagementControl_Load(object sender, EventArgs e)
        {
            // 如果是读者登录，自动填充借书证号
            var user = AuthenticationService.Instance.CurrentUser;
            if (user != null && user.IsReader && !string.IsNullOrEmpty(user.CardID))
            {
                txtCardID.Text = user.CardID;
                LoadReader();
            }
        }

        private void txtCardID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLoadReader_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void btnLoadReader_Click(object sender, EventArgs e)
        {
            LoadReader();
        }

        private void LoadReader()
        {
            lblMessage.Text = string.Empty;
            currentReader = null;
            renewableBooks = null;

            if (string.IsNullOrWhiteSpace(txtCardID.Text))
            {
                lblReaderInfo.Text = "请输入借书证号";
                lblReaderInfo.ForeColor = Color.Red;
                RefreshRenewableBooks();
                return;
            }

            try
            {
                string sql = @"
                    SELECT r.cardID, r.readername, r.readertype, r.unit,
                           rc.startdate, rc.overdate, rc.state
                    FROM reader r
                    INNER JOIN readcard rc ON r.cardID = rc.cardID
                    WHERE r.cardID = @cardID";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@cardID", txtCardID.Text.Trim()));

                if (dt.Rows.Count == 0)
                {
                    lblReaderInfo.Text = "未找到该借书证号对应的读者";
                    lblReaderInfo.ForeColor = Color.Red;
                    RefreshRenewableBooks();
                    return;
                }

                DataRow row = dt.Rows[0];
                currentReader = new Reader
                {
                    CardID = row["cardID"].ToString(),
                    ReaderName = row["readername"].ToString(),
                    ReaderType = row["readertype"].ToString(),
                    Unit = row["unit"]?.ToString(),
                    StartDate = Convert.ToDateTime(row["startdate"]),
                    OverDate = Convert.ToDateTime(row["overdate"]),
                    CardState = row["state"].ToString()
                };

                if (!currentReader.IsCardValid())
                {
                    lblReaderInfo.Text = $"姓名：{currentReader.ReaderName} | {CardStateHelper.GetStateDescription(currentReader.CardState, currentReader.OverDate)}";
                    lblReaderInfo.ForeColor = Color.Red;
                    RefreshRenewableBooks();
                    return;
                }

                // 获取可续借书籍
                renewableBooks = RenewService.GetRenewableBooks(currentReader.CardID);

                lblReaderInfo.Text = $"姓名：{currentReader.ReaderName} | 类型：{currentReader.ReaderType} | " +
                    $"单位：{currentReader.Unit} | 当前借阅：{renewableBooks.Rows.Count}本";
                lblReaderInfo.ForeColor = Color.Green;

                RefreshRenewableBooks();
            }
            catch (Exception ex)
            {
                lblReaderInfo.Text = "查询失败：" + ex.Message;
                lblReaderInfo.ForeColor = Color.Red;
                RefreshRenewableBooks();
            }
        }

        private void RefreshRenewableBooks()
        {
            if (renewableBooks == null || renewableBooks.Rows.Count == 0)
            {
                dgvRenewableBooks.DataSource = null;
                btnRenew.Enabled = false;
                lblRenewInfo.Text = "暂无可续借书籍";
                return;
            }

            var displayData = renewableBooks.AsEnumerable().Select(row => new
            {
                借阅ID = row["bookborrow_id"],
                馆藏码 = row["bookID"],
                书名 = row["bibliography_name"],
                分类 = row["category_code"],
                借阅日期 = Convert.ToDateTime(row["borrowdate"]).ToString("yyyy-MM-dd"),
                最后续借日期 = row["last_renew_time"] != DBNull.Value ?
                    Convert.ToDateTime(row["last_renew_time"]).ToString("yyyy-MM-dd") : "",
                已续借次数 = row["renew_count"],
                当前到期日 = BorrowRules.CalculateDueDate(
                    row["last_renew_time"] != DBNull.Value ?
                        Convert.ToDateTime(row["last_renew_time"]) :
                        Convert.ToDateTime(row["borrowdate"]),
                    row["readertype"].ToString()).ToString("yyyy-MM-dd"),
                状态 = BorrowRules.IsOverdue(
                    row["last_renew_time"] != DBNull.Value ?
                        Convert.ToDateTime(row["last_renew_time"]) :
                        Convert.ToDateTime(row["borrowdate"]),
                    row["readertype"].ToString()) ? "已逾期" : "正常"
            }).ToList();

            dgvRenewableBooks.DataSource = displayData;

            // 隐藏借阅ID列
            if (dgvRenewableBooks.Columns.Contains("借阅ID"))
            {
                dgvRenewableBooks.Columns["借阅ID"].Visible = false;
            }

            // 设置状态列颜色
            if (dgvRenewableBooks.Columns.Contains("状态"))
            {
                dgvRenewableBooks.CellFormatting += (s, e) =>
                {
                    if (e.ColumnIndex == dgvRenewableBooks.Columns["状态"].Index && e.Value != null)
                    {
                        if (e.Value.ToString() == "已逾期")
                        {
                            e.CellStyle.ForeColor = Color.Red;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                };
            }
        }

        private void dgvRenewableBooks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRenewableBooks.SelectedRows.Count == 0)
            {
                btnRenew.Enabled = false;
                lblRenewInfo.Text = "请选择要续借的书籍";
                return;
            }

            try
            {
                long bookborrowId = Convert.ToInt64(dgvRenewableBooks.SelectedRows[0].Cells["借阅ID"].Value);
                string summary = RenewService.GetRenewSummary(bookborrowId);
                lblRenewInfo.Text = summary;

                // 检查是否可以续借
                string errorMessage;
                bool canRenew = RenewService.ValidateRenewEligibility(bookborrowId, out errorMessage);
                btnRenew.Enabled = canRenew;

                if (!canRenew)
                {
                    lblRenewInfo.Text += "\n\n❌ 无法续借\n" + errorMessage;
                    lblRenewInfo.ForeColor = Color.Red;
                }
                else
                {
                    lblRenewInfo.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                lblRenewInfo.Text = "获取续借信息失败：" + ex.Message;
                btnRenew.Enabled = false;
            }
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;

            if (dgvRenewableBooks.SelectedRows.Count == 0)
            {
                lblMessage.Text = "请选择要续借的书籍";
                return;
            }

            try
            {
                long bookborrowId = Convert.ToInt64(dgvRenewableBooks.SelectedRows[0].Cells["借阅ID"].Value);
                string bookName = dgvRenewableBooks.SelectedRows[0].Cells["书名"].Value.ToString();

                string confirmMsg = $"确认续借《{bookName}》？\n\n" +
                    RenewService.GetRenewSummary(bookborrowId);

                if (MessageBox.Show(confirmMsg, "确认续借", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                string errorMessage;
                if (RenewService.ProcessRenew(bookborrowId, out errorMessage))
                {
                    MessageBox.Show($"续借成功！\n\n《{bookName}》已续借。\n\n请在新的到期日前归还。",
                        "续借成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 刷新列表
                    LoadReader();
                }
                else
                {
                    MessageBox.Show("续借失败：" + errorMessage, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("续借失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (currentReader != null)
            {
                LoadReader();
            }
        }
    }
}
