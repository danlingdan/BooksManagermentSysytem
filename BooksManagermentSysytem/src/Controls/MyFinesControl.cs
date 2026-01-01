using System;
using System.Data;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Services;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 我的罚款控件 - 读者查看自己的罚款记录
    /// </summary>
    public partial class MyFinesControl : UserControl
    {
        private DataTable currentFines;
        private string currentCardID;

        public MyFinesControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.lblUnpaidAmount = new System.Windows.Forms.Label();
            this.lblPaidAmount = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblWarning = new System.Windows.Forms.Label();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.dgvFines = new System.Windows.Forms.DataGridView();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelSummary.SuspendLayout();
            this.panelFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFines)).BeginInit();
            this.panelInfo.SuspendLayout();
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
            this.lblTitle.Text = "💰 我的罚款";
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
            // panelSummary
            // 
            this.panelSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelSummary.Controls.Add(this.lblWarning);
            this.panelSummary.Controls.Add(this.lblTotalAmount);
            this.panelSummary.Controls.Add(this.lblPaidAmount);
            this.panelSummary.Controls.Add(this.lblUnpaidAmount);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSummary.Location = new System.Drawing.Point(0, 50);
            this.panelSummary.Size = new System.Drawing.Size(900, 90);
            // 
            // lblUnpaidAmount
            // 
            this.lblUnpaidAmount.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblUnpaidAmount.ForeColor = System.Drawing.Color.Red;
            this.lblUnpaidAmount.Location = new System.Drawing.Point(30, 15);
            this.lblUnpaidAmount.Size = new System.Drawing.Size(250, 28);
            this.lblUnpaidAmount.Text = "未支付：¥0.00";
            // 
            // lblPaidAmount
            // 
            this.lblPaidAmount.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.lblPaidAmount.ForeColor = System.Drawing.Color.Green;
            this.lblPaidAmount.Location = new System.Drawing.Point(290, 18);
            this.lblPaidAmount.Size = new System.Drawing.Size(200, 25);
            this.lblPaidAmount.Text = "已支付：¥0.00";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblTotalAmount.Location = new System.Drawing.Point(500, 18);
            this.lblTotalAmount.Size = new System.Drawing.Size(200, 25);
            this.lblTotalAmount.Text = "总计：¥0.00";
            // 
            // lblWarning
            // 
            this.lblWarning.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.lblWarning.ForeColor = System.Drawing.Color.Orange;
            this.lblWarning.Location = new System.Drawing.Point(30, 50);
            this.lblWarning.Size = new System.Drawing.Size(850, 30);
            this.lblWarning.Text = "ℹ️ 提示：如有未支付罚款，请到图书馆服务台缴纳。未支付罚款可能影响您的借阅权限。";
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelFilter.Controls.Add(this.btnFilter);
            this.panelFilter.Controls.Add(this.cboStatus);
            this.panelFilter.Controls.Add(this.lblStatus);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 140);
            this.panelFilter.Size = new System.Drawing.Size(900, 45);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(20, 13);
            this.lblStatus.Size = new System.Drawing.Size(80, 23);
            this.lblStatus.Text = "罚款状态：";
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Items.AddRange(new object[] { "全部", "未支付", "已支付" });
            this.cboStatus.Location = new System.Drawing.Point(100, 10);
            this.cboStatus.Size = new System.Drawing.Size(120, 25);
            this.cboStatus.SelectedIndex = 0;
            this.cboStatus.SelectedIndexChanged += new System.EventHandler(this.cboStatus_SelectedIndexChanged);
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.Location = new System.Drawing.Point(230, 8);
            this.btnFilter.Size = new System.Drawing.Size(80, 28);
            this.btnFilter.Text = "筛选";
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // dgvFines
            // 
            this.dgvFines.AllowUserToAddRows = false;
            this.dgvFines.AllowUserToDeleteRows = false;
            this.dgvFines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFines.BackgroundColor = System.Drawing.Color.White;
            this.dgvFines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFines.Location = new System.Drawing.Point(0, 185);
            this.dgvFines.ReadOnly = true;
            this.dgvFines.RowHeadersVisible = false;
            this.dgvFines.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFines.Size = new System.Drawing.Size(900, 345);
            // 
            // panelInfo
            // 
            this.panelInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelInfo.Controls.Add(this.lblInfo);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelInfo.Location = new System.Drawing.Point(0, 530);
            this.panelInfo.Size = new System.Drawing.Size(900, 40);
            // 
            // lblInfo
            // 
            this.lblInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblInfo.Location = new System.Drawing.Point(20, 10);
            this.lblInfo.Size = new System.Drawing.Size(860, 23);
            this.lblInfo.Text = "罚款说明：逾期罚款 = 书价×0.1 + 逾期天数×0.1 | 损坏赔偿 = 书价×50% | 丢失赔偿 = 书价×100%";
            // 
            // MyFinesControl
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvFines);
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelSummary);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Size = new System.Drawing.Size(900, 570);
            this.Load += new System.EventHandler(this.MyFinesControl_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelSummary.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFines)).EndInit();
            this.panelInfo.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.Label lblUnpaidAmount;
        private System.Windows.Forms.Label lblPaidAmount;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.DataGridView dgvFines;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label lblInfo;

        private void MyFinesControl_Load(object sender, EventArgs e)
        {
            var user = AuthenticationService.Instance.CurrentUser;
            if (user == null || string.IsNullOrEmpty(user.CardID))
            {
                MessageBox.Show("无法获取当前用户的借书证信息", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            currentCardID = user.CardID;
            LoadFines();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadFines();
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFines();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadFines();
        }

        private void LoadFines()
        {
            if (string.IsNullOrEmpty(currentCardID))
                return;

            try
            {
                string sql = @"
                    SELECT fine_id AS ID,
                           reason AS 罚款原因,
                           amount AS 金额,
                           fine_status AS 状态,
                           created_time AS 创建时间
                    FROM fine
                    WHERE cardID = @cardID";

                if (cboStatus.SelectedIndex == 1)
                {
                    sql += " AND fine_status = N'未支付'";
                }
                else if (cboStatus.SelectedIndex == 2)
                {
                    sql += " AND fine_status = N'已支付'";
                }

                sql += " ORDER BY created_time DESC";

                currentFines = DatabaseHelper.ExecuteQuery(sql, 
                    DatabaseHelper.CreateParameter("@cardID", currentCardID));

                dgvFines.DataSource = currentFines;

                if (dgvFines.Columns.Contains("ID"))
                {
                    dgvFines.Columns["ID"].Visible = false;
                }

                UpdateSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载罚款记录失败：" + ex.Message, "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSummary()
        {
            decimal unpaid = 0, paid = 0;

            if (currentFines != null)
            {
                foreach (DataRow row in currentFines.Rows)
                {
                    decimal amount = Convert.ToDecimal(row["金额"]);
                    string status = row["状态"].ToString();

                    if (status == "未支付")
                    {
                        unpaid += amount;
                    }
                    else if (status == "已支付")
                    {
                        paid += amount;
                    }
                }
            }

            decimal total = unpaid + paid;

            lblUnpaidAmount.Text = $"未支付：¥{unpaid:F2}";
            lblPaidAmount.Text = $"已支付：¥{paid:F2}";
            lblTotalAmount.Text = $"总计：¥{total:F2}";

            if (unpaid > 0)
            {
                lblWarning.ForeColor = System.Drawing.Color.Red;
                lblWarning.Text = $"⚠️ 您有未支付罚款 ¥{unpaid:F2}，请尽快到图书馆服务台缴纳。未支付罚款可能影响您的借阅权限。";
            }
            else
            {
                lblWarning.ForeColor = System.Drawing.Color.Green;
                lblWarning.Text = "✅ 您没有未支付的罚款，借阅状态良好。";
            }
        }
    }
}
