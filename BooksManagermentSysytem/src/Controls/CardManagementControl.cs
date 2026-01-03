using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 借书证管理控件 - 仅管理员使用
    /// 功能：查看借书证、新办理、注销、挂失、补办
    /// </summary>
    public partial class CardManagementControl : UserControl
    {
        private string selectedCardID;

        public CardManagementControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabCardList = new System.Windows.Forms.TabPage();
            this.tabNewCard = new System.Windows.Forms.TabPage();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.lblSearchCardID = new System.Windows.Forms.Label();
            this.txtSearchCardID = new System.Windows.Forms.TextBox();
            this.lblSearchState = new System.Windows.Forms.Label();
            this.cboSearchState = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.dgvCards = new System.Windows.Forms.DataGridView();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnViewDetails = new System.Windows.Forms.Button();
            this.btnMarkLost = new System.Windows.Forms.Button();
            this.btnReissue = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelNewCard = new System.Windows.Forms.Panel();
            this.lblReaderName = new System.Windows.Forms.Label();
            this.txtReaderName = new System.Windows.Forms.TextBox();
            this.lblReaderType = new System.Windows.Forms.Label();
            this.cboReaderType = new System.Windows.Forms.ComboBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.lblNumber = new System.Windows.Forms.Label();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblNote = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.btnCreateCard = new System.Windows.Forms.Button();
            this.btnClearForm = new System.Windows.Forms.Button();
            this.lblCardIDPreview = new System.Windows.Forms.Label();
            this.lblCardIDValue = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabCardList.SuspendLayout();
            this.tabNewCard.SuspendLayout();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCards)).BeginInit();
            this.panelActions.SuspendLayout();
            this.panelNewCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabCardList);
            this.tabControl.Controls.Add(this.tabNewCard);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(900, 600);
            // 
            // tabCardList
            // 
            this.tabCardList.BackColor = System.Drawing.Color.White;
            this.tabCardList.Controls.Add(this.dgvCards);
            this.tabCardList.Controls.Add(this.panelActions);
            this.tabCardList.Controls.Add(this.panelSearch);
            this.tabCardList.Location = new System.Drawing.Point(4, 26);
            this.tabCardList.Name = "tabCardList";
            this.tabCardList.Padding = new System.Windows.Forms.Padding(3);
            this.tabCardList.Size = new System.Drawing.Size(892, 570);
            this.tabCardList.Text = "借书证列表";
            // 
            // tabNewCard
            // 
            this.tabNewCard.BackColor = System.Drawing.Color.White;
            this.tabNewCard.Controls.Add(this.panelNewCard);
            this.tabNewCard.Location = new System.Drawing.Point(4, 26);
            this.tabNewCard.Name = "tabNewCard";
            this.tabNewCard.Padding = new System.Windows.Forms.Padding(3);
            this.tabNewCard.Size = new System.Drawing.Size(892, 570);
            this.tabNewCard.Text = "新办理";
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelSearch.Controls.Add(this.lblSearchCardID);
            this.panelSearch.Controls.Add(this.txtSearchCardID);
            this.panelSearch.Controls.Add(this.lblSearchState);
            this.panelSearch.Controls.Add(this.cboSearchState);
            this.panelSearch.Controls.Add(this.btnSearch);
            this.panelSearch.Controls.Add(this.btnShowAll);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(3, 3);
            this.panelSearch.Size = new System.Drawing.Size(886, 50);
            // 
            // lblSearchCardID
            // 
            this.lblSearchCardID.AutoSize = true;
            this.lblSearchCardID.Location = new System.Drawing.Point(20, 15);
            this.lblSearchCardID.Text = "借书证号：";
            // 
            // txtSearchCardID
            // 
            this.txtSearchCardID.Location = new System.Drawing.Point(90, 12);
            this.txtSearchCardID.Size = new System.Drawing.Size(180, 23);
            // 
            // lblSearchState
            // 
            this.lblSearchState.AutoSize = true;
            this.lblSearchState.Location = new System.Drawing.Point(290, 15);
            this.lblSearchState.Text = "状态：";
            // 
            // cboSearchState
            // 
            this.cboSearchState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSearchState.FormattingEnabled = true;
            this.cboSearchState.Items.AddRange(new object[] {
            "全部",
            "正常",
            "注销",
            "挂失",
            "补办中"});
            this.cboSearchState.Location = new System.Drawing.Point(340, 12);
            this.cboSearchState.Size = new System.Drawing.Size(120, 25);
            this.cboSearchState.SelectedIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(480, 10);
            this.btnSearch.Size = new System.Drawing.Size(80, 28);
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnShowAll
            // 
            this.btnShowAll.Location = new System.Drawing.Point(570, 10);
            this.btnShowAll.Size = new System.Drawing.Size(100, 28);
            this.btnShowAll.Text = "显示全部";
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // dgvCards
            // 
            this.dgvCards.AllowUserToAddRows = false;
            this.dgvCards.AllowUserToDeleteRows = false;
            this.dgvCards.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCards.BackgroundColor = System.Drawing.Color.White;
            this.dgvCards.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCards.MultiSelect = false;
            this.dgvCards.ReadOnly = true;
            this.dgvCards.RowHeadersVisible = false;
            this.dgvCards.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCards.SelectionChanged += new System.EventHandler(this.dgvCards_SelectionChanged);
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelActions.Controls.Add(this.btnViewDetails);
            this.panelActions.Controls.Add(this.btnMarkLost);
            this.panelActions.Controls.Add(this.btnReissue);
            this.panelActions.Controls.Add(this.btnCancel);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelActions.Location = new System.Drawing.Point(3, 517);
            this.panelActions.Size = new System.Drawing.Size(886, 50);
            // 
            // btnViewDetails
            // 
            this.btnViewDetails.Location = new System.Drawing.Point(20, 11);
            this.btnViewDetails.Size = new System.Drawing.Size(120, 28);
            this.btnViewDetails.Text = "查看详情";
            this.btnViewDetails.Enabled = false;
            this.btnViewDetails.Click += new System.EventHandler(this.btnViewDetails_Click);
            // 
            // btnMarkLost
            // 
            this.btnMarkLost.Location = new System.Drawing.Point(150, 11);
            this.btnMarkLost.Size = new System.Drawing.Size(120, 28);
            this.btnMarkLost.Text = "标记挂失";
            this.btnMarkLost.Enabled = false;
            this.btnMarkLost.Click += new System.EventHandler(this.btnMarkLost_Click);
            // 
            // btnReissue
            // 
            this.btnReissue.Location = new System.Drawing.Point(280, 11);
            this.btnReissue.Size = new System.Drawing.Size(120, 28);
            this.btnReissue.Text = "补办";
            this.btnReissue.Enabled = false;
            this.btnReissue.Click += new System.EventHandler(this.btnReissue_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(410, 11);
            this.btnCancel.Size = new System.Drawing.Size(120, 28);
            this.btnCancel.Text = "注销";
            this.btnCancel.Enabled = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelNewCard
            // 
            this.panelNewCard.Controls.Add(this.lblReaderName);
            this.panelNewCard.Controls.Add(this.txtReaderName);
            this.panelNewCard.Controls.Add(this.lblReaderType);
            this.panelNewCard.Controls.Add(this.cboReaderType);
            this.panelNewCard.Controls.Add(this.lblUnit);
            this.panelNewCard.Controls.Add(this.txtUnit);
            this.panelNewCard.Controls.Add(this.lblNumber);
            this.panelNewCard.Controls.Add(this.txtNumber);
            this.panelNewCard.Controls.Add(this.lblStartDate);
            this.panelNewCard.Controls.Add(this.dtpStartDate);
            this.panelNewCard.Controls.Add(this.lblNote);
            this.panelNewCard.Controls.Add(this.txtNote);
            this.panelNewCard.Controls.Add(this.lblCardIDPreview);
            this.panelNewCard.Controls.Add(this.lblCardIDValue);
            this.panelNewCard.Controls.Add(this.btnCreateCard);
            this.panelNewCard.Controls.Add(this.btnClearForm);
            this.panelNewCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNewCard.Location = new System.Drawing.Point(3, 3);
            this.panelNewCard.Size = new System.Drawing.Size(886, 564);
            // 
            // lblReaderName
            // 
            this.lblReaderName.AutoSize = true;
            this.lblReaderName.Location = new System.Drawing.Point(50, 50);
            this.lblReaderName.Text = "读者姓名：";
            // 
            // txtReaderName
            // 
            this.txtReaderName.Location = new System.Drawing.Point(140, 47);
            this.txtReaderName.Size = new System.Drawing.Size(300, 23);
            // 
            // lblReaderType
            // 
            this.lblReaderType.AutoSize = true;
            this.lblReaderType.Location = new System.Drawing.Point(50, 90);
            this.lblReaderType.Text = "读者类型：";
            // 
            // cboReaderType
            // 
            this.cboReaderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReaderType.FormattingEnabled = true;
            this.cboReaderType.Items.AddRange(new object[] {
            "本校学生",
            "本校教师",
            "校外人员"});
            this.cboReaderType.Location = new System.Drawing.Point(140, 87);
            this.cboReaderType.Size = new System.Drawing.Size(300, 25);
            this.cboReaderType.SelectedIndex = 0;
            this.cboReaderType.SelectedIndexChanged += new System.EventHandler(this.cboReaderType_SelectedIndexChanged);
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Location = new System.Drawing.Point(50, 130);
            this.lblUnit.Text = "单位/学院：";
            // 
            // txtUnit
            // 
            this.txtUnit.Location = new System.Drawing.Point(140, 127);
            this.txtUnit.Size = new System.Drawing.Size(300, 23);
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Location = new System.Drawing.Point(50, 170);
            this.lblNumber.Text = "学号/工号：";
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(140, 167);
            this.txtNumber.Size = new System.Drawing.Size(300, 23);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(50, 210);
            this.lblStartDate.Text = "开始日期：";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(140, 207);
            this.dtpStartDate.Size = new System.Drawing.Size(300, 23);
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(50, 250);
            this.lblNote.Text = "备注：";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(140, 247);
            this.txtNote.Multiline = true;
            this.txtNote.Size = new System.Drawing.Size(300, 80);
            // 
            // lblCardIDPreview
            // 
            this.lblCardIDPreview.AutoSize = true;
            this.lblCardIDPreview.Location = new System.Drawing.Point(50, 350);
            this.lblCardIDPreview.Text = "借书证号预览：";
            // 
            // lblCardIDValue
            // 
            this.lblCardIDValue.AutoSize = true;
            this.lblCardIDValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblCardIDValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.lblCardIDValue.Location = new System.Drawing.Point(140, 348);
            this.lblCardIDValue.Size = new System.Drawing.Size(300, 20);
            this.lblCardIDValue.Text = "BRW-YYYY-X-XXXXXX";
            // 
            // btnCreateCard
            // 
            this.btnCreateCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnCreateCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateCard.ForeColor = System.Drawing.Color.White;
            this.btnCreateCard.Location = new System.Drawing.Point(140, 400);
            this.btnCreateCard.Size = new System.Drawing.Size(120, 35);
            this.btnCreateCard.Text = "办理";
            this.btnCreateCard.Click += new System.EventHandler(this.btnCreateCard_Click);
            // 
            // btnClearForm
            // 
            this.btnClearForm.Location = new System.Drawing.Point(270, 400);
            this.btnClearForm.Size = new System.Drawing.Size(120, 35);
            this.btnClearForm.Text = "清空";
            this.btnClearForm.Click += new System.EventHandler(this.btnClearForm_Click);
            // 
            // CardManagementControl
            // 
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.BackColor = Color.White;
            this.Controls.Add(this.tabControl);
            this.Font = new Font("Microsoft YaHei UI", 9F);
            this.MinimumSize = new Size(800, 500);
            this.Name = "CardManagementControl";
            this.Size = new Size(900, 600);
            this.Load += new EventHandler(this.CardManagementControl_Load);
            this.tabControl.ResumeLayout(false);
            this.tabCardList.ResumeLayout(false);
            this.tabNewCard.ResumeLayout(false);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCards)).EndInit();
            this.panelActions.ResumeLayout(false);
            this.panelNewCard.ResumeLayout(false);
            this.panelNewCard.PerformLayout();
            this.ResumeLayout(false);
        }

        #region Designer Fields

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabCardList;
        private System.Windows.Forms.TabPage tabNewCard;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Label lblSearchCardID;
        private System.Windows.Forms.TextBox txtSearchCardID;
        private System.Windows.Forms.Label lblSearchState;
        private System.Windows.Forms.ComboBox cboSearchState;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.DataGridView dgvCards;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnViewDetails;
        private System.Windows.Forms.Button btnMarkLost;
        private System.Windows.Forms.Button btnReissue;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panelNewCard;
        private System.Windows.Forms.Label lblReaderName;
        private System.Windows.Forms.TextBox txtReaderName;
        private System.Windows.Forms.Label lblReaderType;
        private System.Windows.Forms.ComboBox cboReaderType;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label lblCardIDPreview;
        private System.Windows.Forms.Label lblCardIDValue;
        private System.Windows.Forms.Button btnCreateCard;
        private System.Windows.Forms.Button btnClearForm;

        #endregion

        private void CardManagementControl_Load(object sender, EventArgs e)
        {
            LoadAllCards();
            UpdateCardIDPreview();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string cardID = txtSearchCardID.Text.Trim();
            string state = cboSearchState.SelectedItem.ToString();

            string sql = @"
                SELECT 
                    rc.cardID AS '借书证号',
                    r.readername AS '读者姓名',
                    r.readertype AS '读者类型',
                    r.unit AS '单位',
                    r.[number] AS '学号/工号',
                    rc.startdate AS '开始日期',
                    rc.overdate AS '到期日期',
                    rc.[state] AS '状态',
                    r.borrowed_books_info AS '借阅信息'
                FROM dbo.readcard rc
                INNER JOIN dbo.reader r ON rc.cardID = r.cardID
                WHERE 1=1";

            if (!string.IsNullOrEmpty(cardID))
            {
                sql += " AND rc.cardID LIKE @CardID";
            }

            if (state != "全部")
            {
                sql += " AND rc.[state] = @State";
            }

            sql += " ORDER BY rc.cardID DESC";

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    DatabaseHelper.CreateParameter("@CardID", "%" + cardID + "%"),
                    DatabaseHelper.CreateParameter("@State", state)
                };

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                dgvCards.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("未找到符合条件的借书证。", "提示", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询失败：" + ex.Message, "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            txtSearchCardID.Clear();
            cboSearchState.SelectedIndex = 0;
            LoadAllCards();
        }

        private void LoadAllCards()
        {
            string sql = @"
                SELECT 
                    rc.cardID AS '借书证号',
                    r.readername AS '读者姓名',
                    r.readertype AS '读者类型',
                    r.unit AS '单位',
                    r.[number] AS '学号/工号',
                    rc.startdate AS '开始日期',
                    rc.overdate AS '到期日期',
                    rc.[state] AS '状态',
                    r.borrowed_books_info AS '借阅信息'
                FROM dbo.readcard rc
                INNER JOIN dbo.reader r ON rc.cardID = r.cardID
                ORDER BY rc.cardID DESC";

            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(sql);
                dgvCards.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载借书证列表失败：" + ex.Message, "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCards_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCards.SelectedRows.Count > 0)
            {
                selectedCardID = dgvCards.SelectedRows[0].Cells["借书证号"].Value.ToString();
                string state = dgvCards.SelectedRows[0].Cells["状态"].Value.ToString();

                btnViewDetails.Enabled = true;
                btnMarkLost.Enabled = state == "正常";
                btnReissue.Enabled = state == "挂失";
                btnCancel.Enabled = state == "正常" || state == "挂失";
            }
            else
            {
                selectedCardID = null;
                btnViewDetails.Enabled = false;
                btnMarkLost.Enabled = false;
                btnReissue.Enabled = false;
                btnCancel.Enabled = false;
            }
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedCardID))
                return;

            string sql = @"
                SELECT 
                    rc.cardID AS '借书证号',
                    r.readername AS '读者姓名',
                    r.readertype AS '读者类型',
                    r.unit AS '单位',
                    r.[number] AS '学号/工号',
                    rc.startdate AS '开始日期',
                    rc.overdate AS '到期日期',
                    rc.[state] AS '状态',
                    r.borrowed_books_info AS '借阅信息',
                    r.borrow_note AS '备注'
                FROM dbo.readcard rc
                INNER JOIN dbo.reader r ON rc.cardID = r.cardID
                WHERE rc.cardID = @CardID";

            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, 
                    DatabaseHelper.CreateParameter("@CardID", selectedCardID));

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    string details = string.Format(
                        "借书证号：{0}\n读者姓名：{1}\n读者类型：{2}\n单位：{3}\n学号/工号：{4}\n" +
                        "开始日期：{5:yyyy-MM-dd}\n到期日期：{6:yyyy-MM-dd}\n状态：{7}\n" +
                        "借阅信息：{8}\n备注：{9}",
                        row["借书证号"], row["读者姓名"], row["读者类型"], row["单位"], 
                        row["学号/工号"], row["开始日期"], row["到期日期"], row["状态"], 
                        row["借阅信息"], row["备注"]);

                    MessageBox.Show(details, "借书证详情", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("查看详情失败：" + ex.Message, "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMarkLost_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedCardID))
                return;

            if (MessageBox.Show("确定要将此借书证标记为挂失吗？", "确认", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string sql = "UPDATE dbo.readcard SET [state] = N'挂失' WHERE cardID = @CardID";
                    int rows = DatabaseHelper.ExecuteNonQuery(sql, 
                        DatabaseHelper.CreateParameter("@CardID", selectedCardID));

                    if (rows > 0)
                    {
                        MessageBox.Show("挂失成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAllCards();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("挂失失败：" + ex.Message, "错误", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnReissue_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedCardID))
                return;

            if (MessageBox.Show("确定要补办此借书证吗？补办后将生成新的借书证号。", "确认", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string getReaderSql = @"
                        SELECT readername, readertype, unit, [number], borrow_note 
                        FROM dbo.reader 
                        WHERE cardID = @CardID";

                    DataTable readerDt = DatabaseHelper.ExecuteQuery(getReaderSql, 
                        DatabaseHelper.CreateParameter("@CardID", selectedCardID));

                    if (readerDt.Rows.Count == 0)
                    {
                        MessageBox.Show("读者信息不存在。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    DataRow readerRow = readerDt.Rows[0];
                    string readerName = readerRow["readername"].ToString();
                    string readerType = readerRow["readertype"].ToString();
                    string unit = readerRow["unit"].ToString();
                    string number = readerRow["number"] == DBNull.Value ? null : readerRow["number"].ToString();

                    DateTime startDate = DateTime.Today;
                    string newCardID = GenerateCardID(readerType, startDate);

                    string insertCardSql = @"
                        INSERT INTO dbo.readcard (cardID, startdate, overdate, [state])
                        VALUES (@CardID, @StartDate, @OverDate, N'正常')";

                    string insertReaderSql = @"
                        INSERT INTO dbo.reader (cardID, readername, readertype, unit, [number], 
                            borrowed_books_info, borrow_note)
                        VALUES (@CardID, @ReaderName, @ReaderType, @Unit, @Number, 
                            N'当前未归还：0 本', N'补办')";

                    string updateOldCardSql = @"
                        UPDATE dbo.readcard SET [state] = N'注销' WHERE cardID = @CardID";

                    Tuple<string, SqlParameter[]>[] commands = new Tuple<string, SqlParameter[]>[]
                    {
                        Tuple.Create(insertCardSql, new SqlParameter[] {
                            DatabaseHelper.CreateParameter("@CardID", newCardID),
                            DatabaseHelper.CreateParameter("@StartDate", startDate),
                            DatabaseHelper.CreateParameter("@OverDate", startDate.AddYears(1))
                        }),
                        Tuple.Create(insertReaderSql, new SqlParameter[] {
                            DatabaseHelper.CreateParameter("@CardID", newCardID),
                            DatabaseHelper.CreateParameter("@ReaderName", readerName),
                            DatabaseHelper.CreateParameter("@ReaderType", readerType),
                            DatabaseHelper.CreateParameter("@Unit", unit),
                            DatabaseHelper.CreateParameter("@Number", number)
                        }),
                        Tuple.Create(updateOldCardSql, new SqlParameter[] {
                            DatabaseHelper.CreateParameter("@CardID", selectedCardID)
                        })
                    };

                    if (DatabaseHelper.ExecuteTransaction(commands))
                    {
                        MessageBox.Show($"补办成功！新借书证号：{newCardID}", "成功", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAllCards();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("补办失败：" + ex.Message, "错误", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedCardID))
                return;

            if (MessageBox.Show("确定要注销此借书证吗？注销后将无法恢复。", "确认", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    string checkBorrowSql = @"
                        SELECT COUNT(*) 
                        FROM dbo.bookborrow 
                        WHERE cardID = @CardID AND overdate IS NULL";

                    int borrowCount = Convert.ToInt32(DatabaseHelper.ExecuteScalar(checkBorrowSql, 
                        DatabaseHelper.CreateParameter("@CardID", selectedCardID)));

                    if (borrowCount > 0)
                    {
                        MessageBox.Show("该借书证还有未归还的图书，无法注销。", "错误", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string sql = "UPDATE dbo.readcard SET [state] = N'注销' WHERE cardID = @CardID";
                    int rows = DatabaseHelper.ExecuteNonQuery(sql, 
                        DatabaseHelper.CreateParameter("@CardID", selectedCardID));

                    if (rows > 0)
                    {
                        MessageBox.Show("注销成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAllCards();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("注销失败：" + ex.Message, "错误", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cboReaderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string readerType = cboReaderType.SelectedItem.ToString();
            
            if (readerType == "校外人员")
            {
                lblNumber.Text = "证件号码：";
                txtNumber.Enabled = false;
                txtNumber.Clear();
            }
            else
            {
                lblNumber.Text = readerType == "本校学生" ? "学号：" : "工号：";
                txtNumber.Enabled = true;
            }

            UpdateCardIDPreview();
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateCardIDPreview();
        }

        private void UpdateCardIDPreview()
        {
            if (cboReaderType.SelectedItem == null)
                return;

            string readerType = cboReaderType.SelectedItem.ToString();
            DateTime startDate = dtpStartDate.Value;

            string preview = GenerateCardID(readerType, startDate, true);
            lblCardIDValue.Text = preview;
        }

        private string GenerateCardID(string readerType, DateTime startDate, bool isPreview = false)
        {
            string typeCode;
            switch (readerType)
            {
                case "本校学生":
                    typeCode = "1";
                    break;
                case "本校教师":
                    typeCode = "2";
                    break;
                case "校外人员":
                    typeCode = "3";
                    break;
                default:
                    typeCode = "1";
                    break;
            }

            string year = startDate.Year.ToString();

            if (isPreview)
            {
                return $"BRW-{year}-{typeCode}-XXXXXX";
            }

            string sql = @"
                SELECT MAX(CAST(SUBSTRING(cardID, 12, 6) AS INT)) 
                FROM dbo.readcard 
                WHERE SUBSTRING(cardID, 5, 4) = @Year 
                    AND SUBSTRING(cardID, 10, 1) = @TypeCode";

            object result = DatabaseHelper.ExecuteScalar(sql,
                DatabaseHelper.CreateParameter("@Year", year),
                DatabaseHelper.CreateParameter("@TypeCode", typeCode));

            int nextSeq = 1;
            if (result != null && result != DBNull.Value)
            {
                nextSeq = Convert.ToInt32(result) + 1;
            }

            return $"BRW-{year}-{typeCode}-{nextSeq:D6}";
        }

        private void btnCreateCard_Click(object sender, EventArgs e)
        {
            string readerName = txtReaderName.Text.Trim();
            string readerType = cboReaderType.SelectedItem.ToString();
            string unit = txtUnit.Text.Trim();
            string number = txtNumber.Text.Trim();
            DateTime startDate = dtpStartDate.Value;
            string note = txtNote.Text.Trim();

            if (string.IsNullOrEmpty(readerName))
            {
                MessageBox.Show("请输入读者姓名。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReaderName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(unit))
            {
                MessageBox.Show("请输入单位/学院。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUnit.Focus();
                return;
            }

            if (readerType != "校外人员" && string.IsNullOrEmpty(number))
            {
                MessageBox.Show("请输入学号/工号。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumber.Focus();
                return;
            }

            try
            {
                string cardID = GenerateCardID(readerType, startDate);
                DateTime overDate = startDate.AddYears(1);

                string insertCardSql = @"
                    INSERT INTO dbo.readcard (cardID, startdate, overdate, [state])
                    VALUES (@CardID, @StartDate, @OverDate, N'正常')";

                string insertReaderSql = @"
                    INSERT INTO dbo.reader (cardID, readername, readertype, unit, [number], 
                        borrowed_books_info, borrow_note)
                    VALUES (@CardID, @ReaderName, @ReaderType, @Unit, @Number, 
                        N'当前未归还：0 本', @Note)";

                Tuple<string, SqlParameter[]>[] commands = new Tuple<string, SqlParameter[]>[]
                {
                    Tuple.Create(insertCardSql, new SqlParameter[] {
                        DatabaseHelper.CreateParameter("@CardID", cardID),
                        DatabaseHelper.CreateParameter("@StartDate", startDate),
                        DatabaseHelper.CreateParameter("@OverDate", overDate)
                    }),
                    Tuple.Create(insertReaderSql, new SqlParameter[] {
                        DatabaseHelper.CreateParameter("@CardID", cardID),
                        DatabaseHelper.CreateParameter("@ReaderName", readerName),
                        DatabaseHelper.CreateParameter("@ReaderType", readerType),
                        DatabaseHelper.CreateParameter("@Unit", unit),
                        DatabaseHelper.CreateParameter("@Number", readerType == "校外人员" ? (object)DBNull.Value : number),
                        DatabaseHelper.CreateParameter("@Note", note)
                    })
                };

                if (DatabaseHelper.ExecuteTransaction(commands))
                {
                    MessageBox.Show($"办理成功！借书证号：{cardID}\n有效期：{startDate:yyyy-MM-dd} 至 {overDate:yyyy-MM-dd}", 
                        "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    ClearNewCardForm();
                    tabControl.SelectedTab = tabCardList;
                    LoadAllCards();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("办理失败：" + ex.Message, "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearForm_Click(object sender, EventArgs e)
        {
            ClearNewCardForm();
        }

        private void ClearNewCardForm()
        {
            txtReaderName.Clear();
            cboReaderType.SelectedIndex = 0;
            txtUnit.Clear();
            txtNumber.Clear();
            dtpStartDate.Value = DateTime.Today;
            txtNote.Clear();
            UpdateCardIDPreview();
        }
    }
}
