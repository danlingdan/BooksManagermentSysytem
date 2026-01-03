using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 系统日志控件 - 查看编目操作日志和系统审计
    /// </summary>
    public partial class SystemLogControl : UserControl
    {
        public SystemLogControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.btnClearFilter = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.txtOperator = new System.Windows.Forms.TextBox();
            this.lblOperator = new System.Windows.Forms.Label();
            this.cboActionType = new System.Windows.Forms.ComboBox();
            this.lblActionType = new System.Windows.Forms.Label();
            this.cboTargetType = new System.Windows.Forms.ComboBox();
            this.lblTargetType = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblDateRange = new System.Windows.Forms.Label();
            this.dgvLogs = new System.Windows.Forms.DataGridView();
            this.panelStats = new System.Windows.Forms.Panel();
            this.lblStats = new System.Windows.Forms.Label();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).BeginInit();
            this.panelStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.panelHeader.Controls.Add(this.btnExport);
            this.panelHeader.Controls.Add(this.btnRefresh);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1425, 75);
            this.panelHeader.TabIndex = 3;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.White;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Location = new System.Drawing.Point(1290, 15);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(105, 45);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(1170, 15);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(105, 45);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(30, 18);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(300, 42);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "📋 系统操作日志";
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelFilter.Controls.Add(this.btnClearFilter);
            this.panelFilter.Controls.Add(this.btnFilter);
            this.panelFilter.Controls.Add(this.txtOperator);
            this.panelFilter.Controls.Add(this.lblOperator);
            this.panelFilter.Controls.Add(this.cboActionType);
            this.panelFilter.Controls.Add(this.lblActionType);
            this.panelFilter.Controls.Add(this.cboTargetType);
            this.panelFilter.Controls.Add(this.lblTargetType);
            this.panelFilter.Controls.Add(this.dtpEndDate);
            this.panelFilter.Controls.Add(this.lblTo);
            this.panelFilter.Controls.Add(this.dtpStartDate);
            this.panelFilter.Controls.Add(this.lblDateRange);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 75);
            this.panelFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1425, 120);
            this.panelFilter.TabIndex = 2;
            // 
            // btnClearFilter
            // 
            this.btnClearFilter.Location = new System.Drawing.Point(495, 68);
            this.btnClearFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.Size = new System.Drawing.Size(135, 42);
            this.btnClearFilter.TabIndex = 0;
            this.btnClearFilter.Text = "清除筛选";
            this.btnClearFilter.Click += new System.EventHandler(this.btnClearFilter_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.Location = new System.Drawing.Point(375, 68);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(105, 42);
            this.btnFilter.TabIndex = 1;
            this.btnFilter.Text = "筛选";
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // txtOperator
            // 
            this.txtOperator.Location = new System.Drawing.Point(128, 70);
            this.txtOperator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtOperator.Name = "txtOperator";
            this.txtOperator.Size = new System.Drawing.Size(223, 30);
            this.txtOperator.TabIndex = 2;
            // 
            // lblOperator
            // 
            this.lblOperator.AutoSize = true;
            this.lblOperator.Location = new System.Drawing.Point(22, 75);
            this.lblOperator.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOperator.Name = "lblOperator";
            this.lblOperator.Size = new System.Drawing.Size(82, 24);
            this.lblOperator.TabIndex = 3;
            this.lblOperator.Text = "操作员：";
            // 
            // cboActionType
            // 
            this.cboActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboActionType.Location = new System.Drawing.Point(975, 18);
            this.cboActionType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboActionType.Name = "cboActionType";
            this.cboActionType.Size = new System.Drawing.Size(178, 32);
            this.cboActionType.TabIndex = 4;
            // 
            // lblActionType
            // 
            this.lblActionType.AutoSize = true;
            this.lblActionType.Location = new System.Drawing.Point(870, 22);
            this.lblActionType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblActionType.Name = "lblActionType";
            this.lblActionType.Size = new System.Drawing.Size(100, 24);
            this.lblActionType.TabIndex = 5;
            this.lblActionType.Text = "操作类型：";
            // 
            // cboTargetType
            // 
            this.cboTargetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTargetType.Location = new System.Drawing.Point(630, 18);
            this.cboTargetType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboTargetType.Name = "cboTargetType";
            this.cboTargetType.Size = new System.Drawing.Size(208, 32);
            this.cboTargetType.TabIndex = 6;
            // 
            // lblTargetType
            // 
            this.lblTargetType.AutoSize = true;
            this.lblTargetType.Location = new System.Drawing.Point(525, 22);
            this.lblTargetType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTargetType.Name = "lblTargetType";
            this.lblTargetType.Size = new System.Drawing.Size(100, 24);
            this.lblTargetType.TabIndex = 7;
            this.lblTargetType.Text = "对象类型：";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(330, 18);
            this.dtpEndDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(163, 30);
            this.dtpEndDate.TabIndex = 8;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(300, 22);
            this.lblTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(28, 24);
            this.lblTo.TabIndex = 9;
            this.lblTo.Text = "至";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(128, 18);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(163, 30);
            this.dtpStartDate.TabIndex = 10;
            // 
            // lblDateRange
            // 
            this.lblDateRange.AutoSize = true;
            this.lblDateRange.Location = new System.Drawing.Point(22, 22);
            this.lblDateRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDateRange.Name = "lblDateRange";
            this.lblDateRange.Size = new System.Drawing.Size(100, 24);
            this.lblDateRange.TabIndex = 11;
            this.lblDateRange.Text = "日期范围：";
            // 
            // dgvLogs
            // 
            this.dgvLogs.AllowUserToAddRows = false;
            this.dgvLogs.AllowUserToDeleteRows = false;
            this.dgvLogs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLogs.BackgroundColor = System.Drawing.Color.White;
            this.dgvLogs.ColumnHeadersHeight = 40;
            this.dgvLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLogs.Location = new System.Drawing.Point(0, 195);
            this.dgvLogs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvLogs.Name = "dgvLogs";
            this.dgvLogs.ReadOnly = true;
            this.dgvLogs.RowHeadersVisible = false;
            this.dgvLogs.RowHeadersWidth = 62;
            this.dgvLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLogs.Size = new System.Drawing.Size(1425, 570);
            this.dgvLogs.TabIndex = 0;
            // 
            // panelStats
            // 
            this.panelStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelStats.Controls.Add(this.lblStats);
            this.panelStats.Controls.Add(this.lblTotalCount);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStats.Location = new System.Drawing.Point(0, 765);
            this.panelStats.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelStats.Name = "panelStats";
            this.panelStats.Size = new System.Drawing.Size(1425, 60);
            this.panelStats.TabIndex = 1;
            // 
            // lblStats
            // 
            this.lblStats.AutoSize = true;
            this.lblStats.ForeColor = System.Drawing.Color.Gray;
            this.lblStats.Location = new System.Drawing.Point(225, 18);
            this.lblStats.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(223, 24);
            this.lblStats.TabIndex = 0;
            this.lblStats.Text = "提示：日志记录保留180天";
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalCount.Location = new System.Drawing.Point(22, 18);
            this.lblTotalCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(113, 25);
            this.lblTotalCount.TabIndex = 1;
            this.lblTotalCount.Text = "总记录数：0";
            // 
            // SystemLogControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvLogs);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1200, 750);
            this.Name = "SystemLogControl";
            this.Size = new System.Drawing.Size(1425, 825);
            this.Load += new System.EventHandler(this.SystemLogControl_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).EndInit();
            this.panelStats.ResumeLayout(false);
            this.panelStats.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Label lblDateRange;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblTargetType;
        private System.Windows.Forms.ComboBox cboTargetType;
        private System.Windows.Forms.Label lblActionType;
        private System.Windows.Forms.ComboBox cboActionType;
        private System.Windows.Forms.Label lblOperator;
        private System.Windows.Forms.TextBox txtOperator;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnClearFilter;
        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.Label lblStats;

        private void SystemLogControl_Load(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Now.AddDays(-7);
            dtpEndDate.Value = DateTime.Now;

            LoadTargetTypes();
            LoadActionTypes();
            LoadLogs();
        }

        private void LoadTargetTypes()
        {
            cboTargetType.Items.Clear();
            cboTargetType.Items.Add(new FilterItem { Value = "", Text = "全部对象" });
            cboTargetType.Items.Add(new FilterItem { Value = "BIBLIOGRAPHY", Text = "书目" });
            cboTargetType.Items.Add(new FilterItem { Value = "BOOK_ITEM", Text = "馆藏实体" });
            cboTargetType.Items.Add(new FilterItem { Value = "CATEGORY", Text = "图书分类" });
            cboTargetType.Items.Add(new FilterItem { Value = "LOCATION", Text = "库位" });
            cboTargetType.SelectedIndex = 0;
        }

        private void LoadActionTypes()
        {
            cboActionType.Items.Clear();
            cboActionType.Items.Add(new FilterItem { Value = "", Text = "全部操作" });
            cboActionType.Items.Add(new FilterItem { Value = "新增", Text = "新增" });
            cboActionType.Items.Add(new FilterItem { Value = "删除", Text = "删除" });
            cboActionType.Items.Add(new FilterItem { Value = "更新", Text = "更新" });
            cboActionType.Items.Add(new FilterItem { Value = "分类", Text = "分类" });
            cboActionType.Items.Add(new FilterItem { Value = "上架", Text = "上架" });
            cboActionType.Items.Add(new FilterItem { Value = "下架", Text = "下架" });
            cboActionType.Items.Add(new FilterItem { Value = "状态变更", Text = "状态变更" });
            cboActionType.SelectedIndex = 0;
        }

        private void LoadLogs()
        {
            try
            {
                string sql = @"
                    SELECT log_id AS ID, target_type AS 对象类型, target_id AS 对象标识,
                           action_type AS 操作类型, operator AS 操作员,
                           action_time AS 操作时间, note AS 备注
                    FROM catalog_log
                    WHERE action_time >= @startDate AND action_time <= @endDate";

                var parameters = new System.Collections.Generic.List<System.Data.SqlClient.SqlParameter>();
                parameters.Add(DatabaseHelper.CreateParameter("@startDate", dtpStartDate.Value.Date));
                parameters.Add(DatabaseHelper.CreateParameter("@endDate", dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1)));

                if (cboTargetType.SelectedItem != null)
                {
                    string targetType = ((FilterItem)cboTargetType.SelectedItem).Value;
                    if (!string.IsNullOrEmpty(targetType))
                    {
                        sql += " AND target_type = @targetType";
                        parameters.Add(DatabaseHelper.CreateParameter("@targetType", targetType));
                    }
                }

                if (cboActionType.SelectedItem != null)
                {
                    string actionType = ((FilterItem)cboActionType.SelectedItem).Value;
                    if (!string.IsNullOrEmpty(actionType))
                    {
                        sql += " AND action_type = @actionType";
                        parameters.Add(DatabaseHelper.CreateParameter("@actionType", actionType));
                    }
                }

                if (!string.IsNullOrWhiteSpace(txtOperator.Text))
                {
                    sql += " AND operator LIKE @operator";
                    parameters.Add(DatabaseHelper.CreateParameter("@operator", "%" + txtOperator.Text.Trim() + "%"));
                }

                sql += " ORDER BY action_time DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters.ToArray());
                dgvLogs.DataSource = dt;

                if (dgvLogs.Columns.Contains("ID"))
                {
                    dgvLogs.Columns["ID"].Visible = false;
                }

                lblTotalCount.Text = $"总记录数：{dt.Rows.Count}";

                dgvLogs.CellFormatting += (s, cellArgs) =>
                {
                    if (dgvLogs.Columns[cellArgs.ColumnIndex].HeaderText == "操作类型" && cellArgs.Value != null)
                    {
                        string action = cellArgs.Value.ToString();
                        if (action == "删除")
                        {
                            cellArgs.CellStyle.ForeColor = System.Drawing.Color.Red;
                            cellArgs.CellStyle.Font = new System.Drawing.Font(dgvLogs.Font, System.Drawing.FontStyle.Bold);
                        }
                        else if (action == "新增")
                        {
                            cellArgs.CellStyle.ForeColor = System.Drawing.Color.Green;
                        }
                        else if (action == "更新")
                        {
                            cellArgs.CellStyle.ForeColor = System.Drawing.Color.Blue;
                        }
                    }
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载日志失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadLogs();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadLogs();
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Now.AddDays(-7);
            dtpEndDate.Value = DateTime.Now;
            cboTargetType.SelectedIndex = 0;
            cboActionType.SelectedIndex = 0;
            txtOperator.Clear();
            LoadLogs();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvLogs.Rows.Count == 0)
            {
                MessageBox.Show("没有数据可导出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "CSV文件|*.csv|文本文件|*.txt";
                    sfd.FileName = $"系统日志_{DateTime.Now:yyyyMMddHHmmss}.csv";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        // 写入列头
                        for (int i = 0; i < dgvLogs.Columns.Count; i++)
                        {
                            if (dgvLogs.Columns[i].Visible)
                            {
                                sb.Append(dgvLogs.Columns[i].HeaderText);
                                if (i < dgvLogs.Columns.Count - 1) sb.Append(",");
                            }
                        }
                        sb.AppendLine();

                        // 写入数据行
                        foreach (DataGridViewRow row in dgvLogs.Rows)
                        {
                            for (int i = 0; i < dgvLogs.Columns.Count; i++)
                            {
                                if (dgvLogs.Columns[i].Visible)
                                {
                                    object cellValue = row.Cells[i].Value;
                                    string value = cellValue?.ToString() ?? "";
                                    if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
                                    {
                                        value = "\"" + value.Replace("\"", "\"\"") + "\"";
                                    }
                                    sb.Append(value);
                                    if (i < dgvLogs.Columns.Count - 1) sb.Append(",");
                                }
                            }
                            sb.AppendLine();
                        }

                        System.IO.File.WriteAllText(sfd.FileName, sb.ToString(), System.Text.Encoding.UTF8);
                        MessageBox.Show($"导出成功！\n文件保存至：{sfd.FileName}", "成功", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class FilterItem
        {
            public string Value { get; set; }
            public string Text { get; set; }
            public override string ToString() { return Text; }
        }
    }
}
