using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 罚款管理控件 - 图书管理员查看和处理罚款
    /// </summary>
    public partial class FineManagementControl : UserControl
    {
        private DataTable currentFines;
        private string printContent;

        public FineManagementControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panelSearch = new System.Windows.Forms.Panel();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtCardID = new System.Windows.Forms.TextBox();
            this.lblCardID = new System.Windows.Forms.Label();
            this.dgvFines = new System.Windows.Forms.DataGridView();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.lblTotalPaid = new System.Windows.Forms.Label();
            this.lblTotalUnpaid = new System.Windows.Forms.Label();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnPrintNotice = new System.Windows.Forms.Button();
            this.btnMarkPaid = new System.Windows.Forms.Button();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFines)).BeginInit();
            this.panelSummary.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelSearch.Controls.Add(this.btnShowAll);
            this.panelSearch.Controls.Add(this.btnSearch);
            this.panelSearch.Controls.Add(this.cboStatus);
            this.panelSearch.Controls.Add(this.lblStatus);
            this.panelSearch.Controls.Add(this.txtCardID);
            this.panelSearch.Controls.Add(this.lblCardID);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(1350, 75);
            this.panelSearch.TabIndex = 3;
            // 
            // btnShowAll
            // 
            this.btnShowAll.Location = new System.Drawing.Point(765, 15);
            this.btnShowAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(150, 42);
            this.btnShowAll.TabIndex = 0;
            this.btnShowAll.Text = "显示全部未付";
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(630, 15);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(120, 42);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Items.AddRange(new object[] {
            "全部",
            "未支付",
            "已支付"});
            this.cboStatus.Location = new System.Drawing.Point(458, 18);
            this.cboStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(148, 32);
            this.cboStatus.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(390, 22);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(64, 24);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "状态：";
            // 
            // txtCardID
            // 
            this.txtCardID.Location = new System.Drawing.Point(135, 18);
            this.txtCardID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.Size = new System.Drawing.Size(223, 30);
            this.txtCardID.TabIndex = 4;
            // 
            // lblCardID
            // 
            this.lblCardID.AutoSize = true;
            this.lblCardID.Location = new System.Drawing.Point(30, 22);
            this.lblCardID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCardID.Name = "lblCardID";
            this.lblCardID.Size = new System.Drawing.Size(100, 24);
            this.lblCardID.TabIndex = 5;
            this.lblCardID.Text = "借书证号：";
            // 
            // dgvFines
            // 
            this.dgvFines.AllowUserToAddRows = false;
            this.dgvFines.AllowUserToDeleteRows = false;
            this.dgvFines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFines.BackgroundColor = System.Drawing.Color.White;
            this.dgvFines.ColumnHeadersHeight = 40;
            this.dgvFines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFines.Location = new System.Drawing.Point(0, 75);
            this.dgvFines.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvFines.Name = "dgvFines";
            this.dgvFines.ReadOnly = true;
            this.dgvFines.RowHeadersVisible = false;
            this.dgvFines.RowHeadersWidth = 62;
            this.dgvFines.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFines.Size = new System.Drawing.Size(1350, 585);
            this.dgvFines.TabIndex = 0;
            // 
            // panelSummary
            // 
            this.panelSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panelSummary.Controls.Add(this.lblTotalPaid);
            this.panelSummary.Controls.Add(this.lblTotalUnpaid);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSummary.Location = new System.Drawing.Point(0, 660);
            this.panelSummary.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Size = new System.Drawing.Size(1350, 60);
            this.panelSummary.TabIndex = 1;
            // 
            // lblTotalPaid
            // 
            this.lblTotalPaid.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.lblTotalPaid.ForeColor = System.Drawing.Color.Green;
            this.lblTotalPaid.Location = new System.Drawing.Point(450, 15);
            this.lblTotalPaid.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalPaid.Name = "lblTotalPaid";
            this.lblTotalPaid.Size = new System.Drawing.Size(375, 33);
            this.lblTotalPaid.TabIndex = 0;
            this.lblTotalPaid.Text = "已支付总额：¥0.00";
            // 
            // lblTotalUnpaid
            // 
            this.lblTotalUnpaid.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalUnpaid.ForeColor = System.Drawing.Color.Red;
            this.lblTotalUnpaid.Location = new System.Drawing.Point(30, 15);
            this.lblTotalUnpaid.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalUnpaid.Name = "lblTotalUnpaid";
            this.lblTotalUnpaid.Size = new System.Drawing.Size(375, 33);
            this.lblTotalUnpaid.TabIndex = 1;
            this.lblTotalUnpaid.Text = "未支付总额：¥0.00";
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelActions.Controls.Add(this.btnPrintNotice);
            this.panelActions.Controls.Add(this.btnMarkPaid);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelActions.Location = new System.Drawing.Point(0, 720);
            this.panelActions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(1350, 75);
            this.panelActions.TabIndex = 2;
            // 
            // btnPrintNotice
            // 
            this.btnPrintNotice.Location = new System.Drawing.Point(690, 15);
            this.btnPrintNotice.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPrintNotice.Name = "btnPrintNotice";
            this.btnPrintNotice.Size = new System.Drawing.Size(180, 48);
            this.btnPrintNotice.TabIndex = 0;
            this.btnPrintNotice.Text = "打印催缴通知";
            this.btnPrintNotice.Click += new System.EventHandler(this.btnPrintNotice_Click);
            // 
            // btnMarkPaid
            // 
            this.btnMarkPaid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnMarkPaid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarkPaid.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnMarkPaid.ForeColor = System.Drawing.Color.White;
            this.btnMarkPaid.Location = new System.Drawing.Point(450, 12);
            this.btnMarkPaid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMarkPaid.Name = "btnMarkPaid";
            this.btnMarkPaid.Size = new System.Drawing.Size(210, 52);
            this.btnMarkPaid.TabIndex = 1;
            this.btnMarkPaid.Text = "标记为已支付";
            this.btnMarkPaid.UseVisualStyleBackColor = false;
            this.btnMarkPaid.Click += new System.EventHandler(this.btnMarkPaid_Click);
            // 
            // FineManagementControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.panelSummary);
            this.Controls.Add(this.dgvFines);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1200, 750);
            this.Name = "FineManagementControl";
            this.Size = new System.Drawing.Size(1350, 795);
            this.Load += new System.EventHandler(this.FineManagementControl_Load);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFines)).EndInit();
            this.panelSummary.ResumeLayout(false);
            this.panelActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Label lblCardID;
        private System.Windows.Forms.TextBox txtCardID;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.DataGridView dgvFines;
        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.Label lblTotalUnpaid;
        private System.Windows.Forms.Label lblTotalPaid;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnMarkPaid;
        private System.Windows.Forms.Button btnPrintNotice;

        private void FineManagementControl_Load(object sender, EventArgs e)
        {
            cboStatus.SelectedIndex = 1; // 默认显示未支付
            LoadFines();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadFines();
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            txtCardID.Clear();
            cboStatus.SelectedIndex = 1;
            LoadFines();
        }

        private void LoadFines()
        {
            try
            {
                string sql = @"
                    SELECT f.fine_id AS ID, f.cardID AS 借书证号, f.readername AS 读者姓名,
                           f.reason AS 罚款原因, f.amount AS 金额, f.fine_status AS 状态,
                           f.created_time AS 创建时间
                    FROM fine f
                    WHERE 1=1";

                var parameters = new System.Collections.Generic.List<System.Data.SqlClient.SqlParameter>();

                if (!string.IsNullOrWhiteSpace(txtCardID.Text))
                {
                    sql += " AND f.cardID LIKE @cardID";
                    parameters.Add(DatabaseHelper.CreateParameter("@cardID", "%" + txtCardID.Text.Trim() + "%"));
                }

                if (cboStatus.SelectedIndex == 1)
                {
                    sql += " AND f.fine_status = N'未支付'";
                }
                else if (cboStatus.SelectedIndex == 2)
                {
                    sql += " AND f.fine_status = N'已支付'";
                }

                sql += " ORDER BY f.created_time DESC";

                currentFines = DatabaseHelper.ExecuteQuery(sql, parameters.ToArray());
                dgvFines.DataSource = currentFines;

                UpdateSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (row["状态"].ToString() == "未支付")
                        unpaid += amount;
                    else
                        paid += amount;
                }
            }
            lblTotalUnpaid.Text = $"未支付总额：¥{unpaid:F2}";
            lblTotalPaid.Text = $"已支付总额：¥{paid:F2}";
        }

        private void btnMarkPaid_Click(object sender, EventArgs e)
        {
            if (dgvFines.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要处理的罚款记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dgvFines.SelectedRows[0];
            if (row.Cells["状态"].Value.ToString() == "已支付")
            {
                MessageBox.Show("该罚款已支付", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal amount = Convert.ToDecimal(row.Cells["金额"].Value);
            string readerName = row.Cells["读者姓名"].Value.ToString();

            if (MessageBox.Show($"确认读者【{readerName}】已支付罚款 ¥{amount:F2}？",
                "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                long fineId = Convert.ToInt64(row.Cells["ID"].Value);
                string sql = "UPDATE fine SET fine_status = N'已支付' WHERE fine_id = @id";
                DatabaseHelper.ExecuteNonQuery(sql, DatabaseHelper.CreateParameter("@id", fineId));

                MessageBox.Show("已标记为已支付", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadFines();
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrintNotice_Click(object sender, EventArgs e)
        {
            if (dgvFines.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择罚款记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dgvFines.SelectedRows[0];
            if (row.Cells["状态"].Value.ToString() == "已支付")
            {
                MessageBox.Show("该罚款已支付，无需催缴", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string cardID = row.Cells["借书证号"].Value.ToString();
            string readerName = row.Cells["读者姓名"].Value.ToString();
            string reason = row.Cells["罚款原因"].Value.ToString();
            decimal amount = Convert.ToDecimal(row.Cells["金额"].Value);
            DateTime createdTime = Convert.ToDateTime(row.Cells["创建时间"].Value);

            printContent = $@"
═══════════════════════════════════════
           图书馆罚款催缴通知单
═══════════════════════════════════════

借书证号：{cardID}
读者姓名：{readerName}

罚款原因：{reason}
罚款金额：¥{amount:F2}
产生时间：{createdTime:yyyy年MM月dd日}

───────────────────────────────────────

请您于收到本通知后 7 日内到图书馆
服务台缴纳上述罚款。

逾期未缴将影响您的借阅权限。

───────────────────────────────────────
打印日期：{DateTime.Now:yyyy年MM月dd日}
图书馆服务中心
═══════════════════════════════════════
";

            // 显示预览对话框
            var result = MessageBox.Show(printContent + "\n\n是否打印？", 
                "催缴通知预览", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                try
                {
                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += PrintDocument_PrintPage;
                    
                    PrintPreviewDialog preview = new PrintPreviewDialog();
                    preview.Document = pd;
                    preview.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("打印失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Microsoft YaHei UI", 11);
            e.Graphics.DrawString(printContent, font, Brushes.Black, 50, 50);
        }
    }
}
