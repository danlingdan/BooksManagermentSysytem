using System;
using System.Data;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Helpers;
using BooksManagermentSysytem.Services;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 馆藏管理控件 - 管理图书馆藏实体（BOOK_ITEM）
    /// 功能：添加/编辑/删除馆藏、设置状态、关联书目
    /// </summary>
    public partial class BookItemControl : UserControl
    {
        private string currentBarcode;
        private bool isNewMode;

        public BookItemControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panelSearch = new System.Windows.Forms.Panel();
            this.lblKeyword = new System.Windows.Forms.Label();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.lblStatusFilter = new System.Windows.Forms.Label();
            this.cboStatusFilter = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dgvBookItems = new System.Windows.Forms.DataGridView();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.lblBibliography = new System.Windows.Forms.Label();
            this.txtBibliography = new System.Windows.Forms.TextBox();
            this.btnSelectBibliography = new System.Windows.Forms.Button();
            this.lblBibInfo = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.cboLocation = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.lblAcquisitionDate = new System.Windows.Forms.Label();
            this.dtpAcquisitionDate = new System.Windows.Forms.DateTimePicker();
            this.lblPrice = new System.Windows.Forms.Label();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.lblCondition = new System.Windows.Forms.Label();
            this.cboCondition = new System.Windows.Forms.ComboBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookItems)).BeginInit();
            this.panelDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelSearch.Controls.Add(this.btnNew);
            this.panelSearch.Controls.Add(this.btnSearch);
            this.panelSearch.Controls.Add(this.cboStatusFilter);
            this.panelSearch.Controls.Add(this.lblStatusFilter);
            this.panelSearch.Controls.Add(this.txtKeyword);
            this.panelSearch.Controls.Add(this.lblKeyword);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(950, 45);
            // 
            // lblKeyword
            // 
            this.lblKeyword.AutoSize = true;
            this.lblKeyword.Location = new System.Drawing.Point(15, 13);
            this.lblKeyword.Text = "搜索：";
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(60, 10);
            this.txtKeyword.Size = new System.Drawing.Size(180, 23);
            // 
            // lblStatusFilter
            // 
            this.lblStatusFilter.AutoSize = true;
            this.lblStatusFilter.Location = new System.Drawing.Point(255, 13);
            this.lblStatusFilter.Text = "状态：";
            // 
            // cboStatusFilter
            // 
            this.cboStatusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatusFilter.Location = new System.Drawing.Point(300, 10);
            this.cboStatusFilter.Size = new System.Drawing.Size(120, 25);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(435, 8);
            this.btnSearch.Size = new System.Drawing.Size(70, 28);
            this.btnSearch.Text = "搜索";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(520, 8);
            this.btnNew.Size = new System.Drawing.Size(90, 28);
            this.btnNew.Text = "新增馆藏";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 45);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Size = new System.Drawing.Size(950, 505);
            this.splitContainer.SplitterDistance = 500;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dgvBookItems);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panelDetails);
            // 
            // dgvBookItems
            // 
            this.dgvBookItems.AllowUserToAddRows = false;
            this.dgvBookItems.AllowUserToDeleteRows = false;
            this.dgvBookItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBookItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvBookItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBookItems.ReadOnly = true;
            this.dgvBookItems.RowHeadersVisible = false;
            this.dgvBookItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBookItems.SelectionChanged += new System.EventHandler(this.dgvBookItems_SelectionChanged);
            // 
            // panelDetails
            // 
            this.panelDetails.Controls.Add(this.panelButtons);
            this.panelDetails.Controls.Add(this.txtNote);
            this.panelDetails.Controls.Add(this.lblNote);
            this.panelDetails.Controls.Add(this.cboCondition);
            this.panelDetails.Controls.Add(this.lblCondition);
            this.panelDetails.Controls.Add(this.numPrice);
            this.panelDetails.Controls.Add(this.lblPrice);
            this.panelDetails.Controls.Add(this.dtpAcquisitionDate);
            this.panelDetails.Controls.Add(this.lblAcquisitionDate);
            this.panelDetails.Controls.Add(this.cboStatus);
            this.panelDetails.Controls.Add(this.lblStatus);
            this.panelDetails.Controls.Add(this.cboLocation);
            this.panelDetails.Controls.Add(this.lblLocation);
            this.panelDetails.Controls.Add(this.lblBibInfo);
            this.panelDetails.Controls.Add(this.btnSelectBibliography);
            this.panelDetails.Controls.Add(this.txtBibliography);
            this.panelDetails.Controls.Add(this.lblBibliography);
            this.panelDetails.Controls.Add(this.txtBarcode);
            this.panelDetails.Controls.Add(this.lblBarcode);
            this.panelDetails.Controls.Add(this.lblTitle);
            this.panelDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTitle.Text = "馆藏详情";
            // 
            // lblBarcode
            // 
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.Location = new System.Drawing.Point(10, 45);
            this.lblBarcode.Text = "馆藏条码：";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(80, 42);
            this.txtBarcode.Size = new System.Drawing.Size(180, 23);
            // 
            // lblBibliography
            // 
            this.lblBibliography.AutoSize = true;
            this.lblBibliography.Location = new System.Drawing.Point(10, 80);
            this.lblBibliography.Text = "书目ID：";
            // 
            // txtBibliography
            // 
            this.txtBibliography.Location = new System.Drawing.Point(80, 77);
            this.txtBibliography.Size = new System.Drawing.Size(100, 23);
            // 
            // btnSelectBibliography
            // 
            this.btnSelectBibliography.Location = new System.Drawing.Point(190, 75);
            this.btnSelectBibliography.Size = new System.Drawing.Size(70, 28);
            this.btnSelectBibliography.Text = "选择...";
            this.btnSelectBibliography.Click += new System.EventHandler(this.btnSelectBibliography_Click);
            // 
            // lblBibInfo
            // 
            this.lblBibInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblBibInfo.Location = new System.Drawing.Point(10, 110);
            this.lblBibInfo.Size = new System.Drawing.Size(400, 20);
            this.lblBibInfo.Text = "书目信息：未选择";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(10, 140);
            this.lblLocation.Text = "存放位置：";
            // 
            // cboLocation
            // 
            this.cboLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocation.Location = new System.Drawing.Point(80, 137);
            this.cboLocation.Size = new System.Drawing.Size(180, 25);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(10, 175);
            this.lblStatus.Text = "当前状态：";
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Location = new System.Drawing.Point(80, 172);
            this.cboStatus.Size = new System.Drawing.Size(120, 25);
            // 
            // lblAcquisitionDate
            // 
            this.lblAcquisitionDate.AutoSize = true;
            this.lblAcquisitionDate.Location = new System.Drawing.Point(210, 175);
            this.lblAcquisitionDate.Text = "入库日期：";
            // 
            // dtpAcquisitionDate
            // 
            this.dtpAcquisitionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpAcquisitionDate.Location = new System.Drawing.Point(280, 172);
            this.dtpAcquisitionDate.Size = new System.Drawing.Size(110, 23);
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(10, 210);
            this.lblPrice.Text = "实际价格：";
            // 
            // numPrice
            // 
            this.numPrice.DecimalPlaces = 2;
            this.numPrice.Location = new System.Drawing.Point(80, 207);
            this.numPrice.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            this.numPrice.Size = new System.Drawing.Size(100, 23);
            // 
            // lblCondition
            // 
            this.lblCondition.AutoSize = true;
            this.lblCondition.Location = new System.Drawing.Point(200, 210);
            this.lblCondition.Text = "物理状态：";
            // 
            // cboCondition
            // 
            this.cboCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCondition.Location = new System.Drawing.Point(270, 207);
            this.cboCondition.Size = new System.Drawing.Size(120, 25);
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(10, 250);
            this.lblNote.Text = "备注：";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(80, 247);
            this.txtNote.Multiline = true;
            this.txtNote.Size = new System.Drawing.Size(310, 60);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Controls.Add(this.btnDelete);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Location = new System.Drawing.Point(10, 320);
            this.panelButtons.Size = new System.Drawing.Size(400, 40);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(70, 5);
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(170, 5);
            this.btnDelete.Size = new System.Drawing.Size(90, 30);
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(270, 5);
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // BookItemControl
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelSearch);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Size = new System.Drawing.Size(950, 550);
            this.Load += new System.EventHandler(this.BookItemControl_Load);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookItems)).EndInit();
            this.panelDetails.ResumeLayout(false);
            this.panelDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Label lblKeyword;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Label lblStatusFilter;
        private System.Windows.Forms.ComboBox cboStatusFilter;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView dgvBookItems;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label lblBibliography;
        private System.Windows.Forms.TextBox txtBibliography;
        private System.Windows.Forms.Button btnSelectBibliography;
        private System.Windows.Forms.Label lblBibInfo;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.ComboBox cboLocation;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label lblAcquisitionDate;
        private System.Windows.Forms.DateTimePicker dtpAcquisitionDate;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.Label lblCondition;
        private System.Windows.Forms.ComboBox cboCondition;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;

        private void BookItemControl_Load(object sender, EventArgs e)
        {
            LoadStatusFilter();
            LoadLocations();
            LoadStatusOptions();
            LoadConditions();
            LoadBookItems();
        }

        private void LoadStatusFilter()
        {
            cboStatusFilter.Items.Clear();
            cboStatusFilter.Items.Add(new ComboItem { Value = "", Text = "全部状态" });
            cboStatusFilter.Items.Add(new ComboItem { Value = "AVAILABLE", Text = "可借" });
            cboStatusFilter.Items.Add(new ComboItem { Value = "BORROWED", Text = "已借出" });
            cboStatusFilter.Items.Add(new ComboItem { Value = "RESERVED", Text = "已预约" });
            cboStatusFilter.Items.Add(new ComboItem { Value = "PROCESSING", Text = "处理中" });
            cboStatusFilter.Items.Add(new ComboItem { Value = "DAMAGED", Text = "损坏" });
            cboStatusFilter.Items.Add(new ComboItem { Value = "LOST", Text = "丢失" });
            cboStatusFilter.SelectedIndex = 0;
        }

        private void LoadLocations()
        {
            cboLocation.Items.Clear();
            try
            {
                string sql = "SELECT location_id, location_code, location_name FROM STORAGE_LOCATION WHERE status = N'ACTIVE' ORDER BY location_code";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql);
                foreach (DataRow row in dt.Rows)
                {
                    cboLocation.Items.Add(new ComboItem
                    {
                        Value = row["location_id"].ToString(),
                        Text = $"[{row["location_code"]}] {row["location_name"]}"
                    });
                }
                if (cboLocation.Items.Count > 0) cboLocation.SelectedIndex = 0;
            }
            catch { }
        }

        private void LoadStatusOptions()
        {
            cboStatus.Items.Clear();
            cboStatus.Items.Add(new ComboItem { Value = "AVAILABLE", Text = "可借" });
            cboStatus.Items.Add(new ComboItem { Value = "BORROWED", Text = "已借出" });
            cboStatus.Items.Add(new ComboItem { Value = "RESERVED", Text = "已预约" });
            cboStatus.Items.Add(new ComboItem { Value = "PROCESSING", Text = "处理中" });
            cboStatus.Items.Add(new ComboItem { Value = "DAMAGED", Text = "损坏" });
            cboStatus.Items.Add(new ComboItem { Value = "LOST", Text = "丢失" });
            cboStatus.SelectedIndex = 0;
        }

        private void LoadConditions()
        {
            cboCondition.Items.Clear();
            cboCondition.Items.Add("完好");
            cboCondition.Items.Add("轻微磨损");
            cboCondition.Items.Add("中度磨损");
            cboCondition.Items.Add("严重磨损");
            cboCondition.Items.Add("损坏");
            cboCondition.SelectedIndex = 0;
        }

        private void LoadBookItems()
        {
            try
            {
                string sql = @"
                    SELECT bi.item_barcode AS 馆藏码, bib.bibliography_name AS 书名, bib.ISBN,
                           bc.category_code AS 分类, sl.location_name AS 位置,
                           bi.current_status AS 状态, bi.price AS 价格
                    FROM BOOK_ITEM bi
                    INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                    INNER JOIN BOOK_CATEGORY bc ON bib.category_id = bc.category_id
                    INNER JOIN STORAGE_LOCATION sl ON bi.location_id = sl.location_id
                    WHERE 1=1";

                var parameters = new System.Collections.Generic.List<System.Data.SqlClient.SqlParameter>();

                if (!string.IsNullOrWhiteSpace(txtKeyword.Text))
                {
                    sql += " AND (bi.item_barcode LIKE @kw OR bib.bibliography_name LIKE @kw OR bib.ISBN LIKE @kw)";
                    parameters.Add(DatabaseHelper.CreateParameter("@kw", "%" + txtKeyword.Text.Trim() + "%"));
                }

                if (cboStatusFilter.SelectedItem != null)
                {
                    string status = ((ComboItem)cboStatusFilter.SelectedItem).Value;
                    if (!string.IsNullOrEmpty(status))
                    {
                        sql += " AND bi.current_status = @status";
                        parameters.Add(DatabaseHelper.CreateParameter("@status", status));
                    }
                }

                sql += " ORDER BY bi.status_changed_date DESC";

                dgvBookItems.DataSource = DatabaseHelper.ExecuteQuery(sql, parameters.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载馆藏失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadBookItems();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            isNewMode = true;
            currentBarcode = null;
            ClearForm();
            txtBarcode.Enabled = true;
            txtBarcode.Focus();
        }

        private void ClearForm()
        {
            txtBarcode.Clear();
            txtBibliography.Clear();
            lblBibInfo.Text = "书目信息：未选择";
            if (cboLocation.Items.Count > 0) cboLocation.SelectedIndex = 0;
            if (cboStatus.Items.Count > 0) cboStatus.SelectedIndex = 0;
            dtpAcquisitionDate.Value = DateTime.Now;
            numPrice.Value = 0;
            if (cboCondition.Items.Count > 0) cboCondition.SelectedIndex = 0;
            txtNote.Clear();
        }

        private void dgvBookItems_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBookItems.SelectedRows.Count == 0) return;

            var barcodeCell = dgvBookItems.SelectedRows[0].Cells["馆藏码"];
            if (barcodeCell?.Value == null) return;

            currentBarcode = barcodeCell.Value.ToString();
            isNewMode = false;
            LoadBookItemDetails(currentBarcode);
        }

        private void LoadBookItemDetails(string barcode)
        {
            try
            {
                string sql = @"
                    SELECT bi.*, bib.bibliography_name, bib.ISBN, bib.price AS bib_price
                    FROM BOOK_ITEM bi
                    INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                    WHERE bi.item_barcode = @barcode";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.CreateParameter("@barcode", barcode));

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    txtBarcode.Text = row["item_barcode"].ToString();
                    txtBarcode.Enabled = false;

                    txtBibliography.Text = row["bibliography_id"].ToString();
                    lblBibInfo.Text = $"书目：{row["bibliography_name"]} (ISBN: {row["ISBN"]})";

                    // 选择位置
                    int locationId = Convert.ToInt32(row["location_id"]);
                    for (int i = 0; i < cboLocation.Items.Count; i++)
                    {
                        if (((ComboItem)cboLocation.Items[i]).Value == locationId.ToString())
                        {
                            cboLocation.SelectedIndex = i;
                            break;
                        }
                    }

                    // 选择状态
                    string status = row["current_status"].ToString();
                    for (int i = 0; i < cboStatus.Items.Count; i++)
                    {
                        if (((ComboItem)cboStatus.Items[i]).Value == status)
                        {
                            cboStatus.SelectedIndex = i;
                            break;
                        }
                    }

                    if (row["acquisition_date"] != DBNull.Value)
                        dtpAcquisitionDate.Value = Convert.ToDateTime(row["acquisition_date"]);

                    if (row["price"] != DBNull.Value)
                        numPrice.Value = Convert.ToDecimal(row["price"]);

                    string condition = row["physical_condition"]?.ToString() ?? "完好";
                    int condIndex = cboCondition.Items.IndexOf(condition);
                    if (condIndex >= 0) cboCondition.SelectedIndex = condIndex;

                    txtNote.Text = row["note"]?.ToString() ?? "";
                }
            }
            catch { }
        }

        private void btnSelectBibliography_Click(object sender, EventArgs e)
        {
            // 简化版：通过ISBN或书名搜索书目
            string input = InputDialog.Show(
                "请输入ISBN或书名关键字搜索书目：", "选择书目", "");

            if (string.IsNullOrWhiteSpace(input)) return;

            try
            {
                string sql = @"SELECT TOP 10 bibliography_id, ISBN, bibliography_name, price 
                              FROM BIBLIOGRAPHY 
                              WHERE ISBN LIKE @kw OR bibliography_name LIKE @kw
                              ORDER BY create_time DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@kw", "%" + input.Trim() + "%"));

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("未找到匹配的书目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (dt.Rows.Count == 1)
                {
                    // 直接选择唯一结果
                    SelectBibliography(dt.Rows[0]);
                }
                else
                {
                    // 显示选择对话框
                    string[] options = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        options[i] = $"[{dt.Rows[i]["ISBN"]}] {dt.Rows[i]["bibliography_name"]}";
                    }

                    string message = "找到多个书目，请选择：\n\n";
                    for (int i = 0; i < options.Length; i++)
                    {
                        message += $"{i + 1}. {options[i]}\n";
                    }
                    message += "\n请输入序号(1-" + options.Length + ")：";

                    string choice = InputDialog.Show(message, "选择书目", "1");
                    int index;
                    if (int.TryParse(choice, out index) && index >= 1 && index <= dt.Rows.Count)
                    {
                        SelectBibliography(dt.Rows[index - 1]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("搜索书目失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectBibliography(DataRow row)
        {
            txtBibliography.Text = row["bibliography_id"].ToString();
            lblBibInfo.Text = $"书目：{row["bibliography_name"]} (ISBN: {row["ISBN"]})";

            if (row["price"] != DBNull.Value && numPrice.Value == 0)
            {
                numPrice.Value = Convert.ToDecimal(row["price"]);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                MessageBox.Show("请输入馆藏条码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtBibliography.Text))
            {
                MessageBox.Show("请选择书目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboLocation.SelectedItem == null)
            {
                MessageBox.Show("请选择存放位置", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int bibId = Convert.ToInt32(txtBibliography.Text);
                int locationId = Convert.ToInt32(((ComboItem)cboLocation.SelectedItem).Value);
                string status = ((ComboItem)cboStatus.SelectedItem).Value;
                string condition = cboCondition.SelectedItem?.ToString() ?? "完好";
                string operatorName = AuthenticationService.Instance.CurrentUser?.Username ?? "system";

                if (isNewMode)
                {
                    // 检查条码唯一性
                    string checkSql = "SELECT COUNT(*) FROM BOOK_ITEM WHERE item_barcode = @barcode";
                    int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(checkSql,
                        DatabaseHelper.CreateParameter("@barcode", txtBarcode.Text.Trim())));

                    if (count > 0)
                    {
                        MessageBox.Show("该馆藏条码已存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string sql = @"INSERT INTO BOOK_ITEM 
                                  (item_barcode, bibliography_id, location_id, current_status, 
                                   acquisition_date, price, physical_condition, note, status_changed_date)
                                  VALUES (@barcode, @bibId, @locId, @status, 
                                          @acqDate, @price, @condition, @note, GETDATE())";

                    DatabaseHelper.ExecuteNonQuery(sql,
                        DatabaseHelper.CreateParameter("@barcode", txtBarcode.Text.Trim()),
                        DatabaseHelper.CreateParameter("@bibId", bibId),
                        DatabaseHelper.CreateParameter("@locId", locationId),
                        DatabaseHelper.CreateParameter("@status", status),
                        DatabaseHelper.CreateParameter("@acqDate", dtpAcquisitionDate.Value.Date),
                        DatabaseHelper.CreateParameter("@price", numPrice.Value),
                        DatabaseHelper.CreateParameter("@condition", condition),
                        DatabaseHelper.CreateParameter("@note", txtNote.Text.Trim()));

                    LogCatalogAction("BOOK_ITEM", txtBarcode.Text, "新增", operatorName, $"新增馆藏");
                }
                else
                {
                    string sql = @"UPDATE BOOK_ITEM SET 
                                  bibliography_id = @bibId, location_id = @locId, current_status = @status,
                                  acquisition_date = @acqDate, price = @price, physical_condition = @condition,
                                  note = @note, status_changed_date = GETDATE()
                                  WHERE item_barcode = @barcode";

                    DatabaseHelper.ExecuteNonQuery(sql,
                        DatabaseHelper.CreateParameter("@bibId", bibId),
                        DatabaseHelper.CreateParameter("@locId", locationId),
                        DatabaseHelper.CreateParameter("@status", status),
                        DatabaseHelper.CreateParameter("@acqDate", dtpAcquisitionDate.Value.Date),
                        DatabaseHelper.CreateParameter("@price", numPrice.Value),
                        DatabaseHelper.CreateParameter("@condition", condition),
                        DatabaseHelper.CreateParameter("@note", txtNote.Text.Trim()),
                        DatabaseHelper.CreateParameter("@barcode", currentBarcode));

                    LogCatalogAction("BOOK_ITEM", currentBarcode, "更新", operatorName, $"更新馆藏信息");
                }

                MessageBox.Show("保存成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isNewMode = false;
                LoadBookItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentBarcode) || isNewMode)
            {
                MessageBox.Show("请选择要删除的馆藏", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 检查是否有借阅记录
            string checkSql = "SELECT COUNT(*) FROM bookborrow WHERE bookID = @barcode AND overdate IS NULL";
            int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(checkSql,
                DatabaseHelper.CreateParameter("@barcode", currentBarcode)));

            if (count > 0)
            {
                MessageBox.Show("该馆藏有未归还的借阅记录，无法删除", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("确定删除该馆藏？此操作不可恢复。", "确认", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                string sql = "DELETE FROM BOOK_ITEM WHERE item_barcode = @barcode";
                DatabaseHelper.ExecuteNonQuery(sql, DatabaseHelper.CreateParameter("@barcode", currentBarcode));

                string operatorName = AuthenticationService.Instance.CurrentUser?.Username ?? "system";
                LogCatalogAction("BOOK_ITEM", currentBarcode, "删除", operatorName, "删除馆藏");

                MessageBox.Show("删除成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCancel_Click(sender, e);
                LoadBookItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            isNewMode = false;
            currentBarcode = null;
            ClearForm();
            txtBarcode.Enabled = true;
        }

        private void LogCatalogAction(string targetType, string targetId, string actionType, string operatorName, string note)
        {
            try
            {
                string sql = @"INSERT INTO catalog_log (target_type, target_id, action_type, operator, note)
                              VALUES (@type, @targetId, @action, @operator, @note)";
                DatabaseHelper.ExecuteNonQuery(sql,
                    DatabaseHelper.CreateParameter("@type", targetType),
                    DatabaseHelper.CreateParameter("@targetId", targetId),
                    DatabaseHelper.CreateParameter("@action", actionType),
                    DatabaseHelper.CreateParameter("@operator", operatorName),
                    DatabaseHelper.CreateParameter("@note", note));
            }
            catch { }
        }

        private class ComboItem
        {
            public string Value { get; set; }
            public string Text { get; set; }
            public override string ToString() { return Text; }
        }
    }
}
