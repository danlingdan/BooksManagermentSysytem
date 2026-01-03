using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Services;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 书目管理控件 - 管理ISBN级书目信息
    /// </summary>
    public partial class BibliographyControl : UserControl
    {
        private int? currentBibId;
        private bool isNewMode;

        public BibliographyControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panelSearch = new System.Windows.Forms.Panel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.lblKeyword = new System.Windows.Forms.Label();
            this.dgvBibliography = new System.Windows.Forms.DataGridView();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.lblItems = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtAuthors = new System.Windows.Forms.TextBox();
            this.lblAuthors = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.lblPrice = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.dtpPublishDate = new System.Windows.Forms.DateTimePicker();
            this.lblPublishDate = new System.Windows.Forms.Label();
            this.txtPublish = new System.Windows.Forms.TextBox();
            this.lblPublish = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.btnLookupISBN = new System.Windows.Forms.Button();
            this.txtISBN = new System.Windows.Forms.TextBox();
            this.lblISBN = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBibliography)).BeginInit();
            this.panelDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelSearch.Controls.Add(this.btnNew);
            this.panelSearch.Controls.Add(this.btnSearch);
            this.panelSearch.Controls.Add(this.txtKeyword);
            this.panelSearch.Controls.Add(this.lblKeyword);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Margin = new System.Windows.Forms.Padding(4);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(1425, 68);
            this.panelSearch.TabIndex = 2;
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(570, 12);
            this.btnNew.Margin = new System.Windows.Forms.Padding(4);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(135, 42);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "新建书目";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(442, 12);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(105, 42);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "搜索";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(128, 15);
            this.txtKeyword.Margin = new System.Windows.Forms.Padding(4);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(298, 30);
            this.txtKeyword.TabIndex = 2;
            // 
            // lblKeyword
            // 
            this.lblKeyword.AutoSize = true;
            this.lblKeyword.Location = new System.Drawing.Point(22, 20);
            this.lblKeyword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblKeyword.Name = "lblKeyword";
            this.lblKeyword.Size = new System.Drawing.Size(100, 24);
            this.lblKeyword.TabIndex = 3;
            this.lblKeyword.Text = "搜索书目：";
            // 
            // dgvBibliography
            // 
            this.dgvBibliography.AllowUserToAddRows = false;
            this.dgvBibliography.AllowUserToDeleteRows = false;
            this.dgvBibliography.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBibliography.BackgroundColor = System.Drawing.Color.White;
            this.dgvBibliography.ColumnHeadersHeight = 40;
            this.dgvBibliography.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvBibliography.Location = new System.Drawing.Point(0, 68);
            this.dgvBibliography.Margin = new System.Windows.Forms.Padding(4);
            this.dgvBibliography.Name = "dgvBibliography";
            this.dgvBibliography.ReadOnly = true;
            this.dgvBibliography.RowHeadersVisible = false;
            this.dgvBibliography.RowHeadersWidth = 62;
            this.dgvBibliography.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBibliography.Size = new System.Drawing.Size(675, 757);
            this.dgvBibliography.TabIndex = 1;
            this.dgvBibliography.SelectionChanged += new System.EventHandler(this.dgvBibliography_SelectionChanged);
            // 
            // panelDetails
            // 
            this.panelDetails.Controls.Add(this.dgvItems);
            this.panelDetails.Controls.Add(this.lblItems);
            this.panelDetails.Controls.Add(this.panelButtons);
            this.panelDetails.Controls.Add(this.txtAuthors);
            this.panelDetails.Controls.Add(this.lblAuthors);
            this.panelDetails.Controls.Add(this.txtDescription);
            this.panelDetails.Controls.Add(this.lblDescription);
            this.panelDetails.Controls.Add(this.numPrice);
            this.panelDetails.Controls.Add(this.lblPrice);
            this.panelDetails.Controls.Add(this.cboCategory);
            this.panelDetails.Controls.Add(this.lblCategory);
            this.panelDetails.Controls.Add(this.dtpPublishDate);
            this.panelDetails.Controls.Add(this.lblPublishDate);
            this.panelDetails.Controls.Add(this.txtPublish);
            this.panelDetails.Controls.Add(this.lblPublish);
            this.panelDetails.Controls.Add(this.txtName);
            this.panelDetails.Controls.Add(this.lblName);
            this.panelDetails.Controls.Add(this.btnLookupISBN);
            this.panelDetails.Controls.Add(this.txtISBN);
            this.panelDetails.Controls.Add(this.lblISBN);
            this.panelDetails.Controls.Add(this.lblTitle);
            this.panelDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetails.Location = new System.Drawing.Point(675, 68);
            this.panelDetails.Margin = new System.Windows.Forms.Padding(4);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Size = new System.Drawing.Size(750, 757);
            this.panelDetails.TabIndex = 0;
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvItems.ColumnHeadersHeight = 40;
            this.dgvItems.Location = new System.Drawing.Point(15, 540);
            this.dgvItems.Margin = new System.Windows.Forms.Padding(4);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.RowHeadersWidth = 62;
            this.dgvItems.Size = new System.Drawing.Size(705, 195);
            this.dgvItems.TabIndex = 0;
            this.dgvItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellContentClick);
            // 
            // lblItems
            // 
            this.lblItems.AutoSize = true;
            this.lblItems.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblItems.Location = new System.Drawing.Point(15, 502);
            this.lblItems.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblItems.Name = "lblItems";
            this.lblItems.Size = new System.Drawing.Size(102, 25);
            this.lblItems.TabIndex = 1;
            this.lblItems.Text = "馆藏实体：";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Controls.Add(this.btnDelete);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Location = new System.Drawing.Point(15, 428);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(4);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(690, 60);
            this.panelButtons.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(405, 8);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(135, 45);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(255, 8);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(135, 45);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(105, 8);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(135, 45);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtAuthors
            // 
            this.txtAuthors.Location = new System.Drawing.Point(120, 378);
            this.txtAuthors.Margin = new System.Windows.Forms.Padding(4);
            this.txtAuthors.Name = "txtAuthors";
            this.txtAuthors.Size = new System.Drawing.Size(418, 30);
            this.txtAuthors.TabIndex = 3;
            // 
            // lblAuthors
            // 
            this.lblAuthors.AutoSize = true;
            this.lblAuthors.Location = new System.Drawing.Point(15, 382);
            this.lblAuthors.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAuthors.Name = "lblAuthors";
            this.lblAuthors.Size = new System.Drawing.Size(64, 24);
            this.lblAuthors.TabIndex = 4;
            this.lblAuthors.Text = "作者：";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(120, 273);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(583, 88);
            this.txtDescription.TabIndex = 5;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(15, 278);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(64, 24);
            this.lblDescription.TabIndex = 6;
            this.lblDescription.Text = "简介：";
            // 
            // numPrice
            // 
            this.numPrice.DecimalPlaces = 2;
            this.numPrice.Location = new System.Drawing.Point(510, 220);
            this.numPrice.Margin = new System.Windows.Forms.Padding(4);
            this.numPrice.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numPrice.Name = "numPrice";
            this.numPrice.Size = new System.Drawing.Size(150, 30);
            this.numPrice.TabIndex = 7;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(435, 225);
            this.lblPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(64, 24);
            this.lblPrice.TabIndex = 8;
            this.lblPrice.Text = "定价：";
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.Location = new System.Drawing.Point(120, 220);
            this.cboCategory.Margin = new System.Windows.Forms.Padding(4);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(298, 32);
            this.cboCategory.TabIndex = 9;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(15, 225);
            this.lblCategory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(64, 24);
            this.lblCategory.TabIndex = 10;
            this.lblCategory.Text = "分类：";
            // 
            // dtpPublishDate
            // 
            this.dtpPublishDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPublishDate.Location = new System.Drawing.Point(540, 168);
            this.dtpPublishDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpPublishDate.Name = "dtpPublishDate";
            this.dtpPublishDate.Size = new System.Drawing.Size(163, 30);
            this.dtpPublishDate.TabIndex = 11;
            // 
            // lblPublishDate
            // 
            this.lblPublishDate.AutoSize = true;
            this.lblPublishDate.Location = new System.Drawing.Point(435, 172);
            this.lblPublishDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPublishDate.Name = "lblPublishDate";
            this.lblPublishDate.Size = new System.Drawing.Size(100, 24);
            this.lblPublishDate.TabIndex = 12;
            this.lblPublishDate.Text = "出版日期：";
            // 
            // txtPublish
            // 
            this.txtPublish.Location = new System.Drawing.Point(120, 168);
            this.txtPublish.Margin = new System.Windows.Forms.Padding(4);
            this.txtPublish.Name = "txtPublish";
            this.txtPublish.Size = new System.Drawing.Size(298, 30);
            this.txtPublish.TabIndex = 13;
            // 
            // lblPublish
            // 
            this.lblPublish.AutoSize = true;
            this.lblPublish.Location = new System.Drawing.Point(15, 172);
            this.lblPublish.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPublish.Name = "lblPublish";
            this.lblPublish.Size = new System.Drawing.Size(82, 24);
            this.lblPublish.TabIndex = 14;
            this.lblPublish.Text = "出版社：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(120, 116);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(418, 30);
            this.txtName.TabIndex = 15;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(15, 120);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(64, 24);
            this.lblName.TabIndex = 16;
            this.lblName.Text = "书名：";
            // 
            // btnLookupISBN
            // 
            this.btnLookupISBN.Location = new System.Drawing.Point(405, 60);
            this.btnLookupISBN.Margin = new System.Windows.Forms.Padding(4);
            this.btnLookupISBN.Name = "btnLookupISBN";
            this.btnLookupISBN.Size = new System.Drawing.Size(135, 42);
            this.btnLookupISBN.TabIndex = 17;
            this.btnLookupISBN.Text = "ISBN查询";
            this.btnLookupISBN.Click += new System.EventHandler(this.btnLookupISBN_Click);
            // 
            // txtISBN
            // 
            this.txtISBN.Location = new System.Drawing.Point(120, 63);
            this.txtISBN.Margin = new System.Windows.Forms.Padding(4);
            this.txtISBN.Name = "txtISBN";
            this.txtISBN.Size = new System.Drawing.Size(268, 30);
            this.txtISBN.TabIndex = 18;
            // 
            // lblISBN
            // 
            this.lblISBN.AutoSize = true;
            this.lblISBN.Location = new System.Drawing.Point(15, 68);
            this.lblISBN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblISBN.Name = "lblISBN";
            this.lblISBN.Size = new System.Drawing.Size(69, 24);
            this.lblISBN.TabIndex = 19;
            this.lblISBN.Text = "ISBN：";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(92, 27);
            this.lblTitle.TabIndex = 20;
            this.lblTitle.Text = "书目详情";
            // 
            // BibliographyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelDetails);
            this.Controls.Add(this.dgvBibliography);
            this.Controls.Add(this.panelSearch);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1350, 750);
            this.Name = "BibliographyControl";
            this.Size = new System.Drawing.Size(1425, 825);
            this.Load += new System.EventHandler(this.BibliographyControl_Load);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBibliography)).EndInit();
            this.panelDetails.ResumeLayout(false);
            this.panelDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Label lblKeyword;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.DataGridView dgvBibliography;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblISBN;
        private System.Windows.Forms.TextBox txtISBN;
        private System.Windows.Forms.Button btnLookupISBN;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblPublish;
        private System.Windows.Forms.TextBox txtPublish;
        private System.Windows.Forms.Label lblPublishDate;
        private System.Windows.Forms.DateTimePicker dtpPublishDate;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblAuthors;
        private System.Windows.Forms.TextBox txtAuthors;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblItems;
        private System.Windows.Forms.DataGridView dgvItems;

        private void BibliographyControl_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadBibliography();
        }

        private void LoadCategories()
        {
            cboCategory.Items.Clear();
            try
            {
                string sql = "SELECT category_id, category_code, category_name FROM BOOK_CATEGORY ORDER BY category_code";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql);
                foreach (DataRow row in dt.Rows)
                {
                    cboCategory.Items.Add(new CategoryItem
                    {
                        Id = Convert.ToInt32(row["category_id"]),
                        Text = $"[{row["category_code"]}] {row["category_name"]}"
                    });
                }
                if (cboCategory.Items.Count > 0) cboCategory.SelectedIndex = 0;
            }
            catch { }
        }

        private void LoadBibliography()
        {
            try
            {
                string sql = @"SELECT b.bibliography_id AS ID, b.ISBN, b.bibliography_name AS 书名, 
                              b.publish AS 出版社, bc.category_code AS 分类, b.price AS 价格
                              FROM BIBLIOGRAPHY b
                              INNER JOIN BOOK_CATEGORY bc ON b.category_id = bc.category_id";

                if (!string.IsNullOrWhiteSpace(txtKeyword.Text))
                {
                    sql += " WHERE b.bibliography_name LIKE @kw OR b.ISBN LIKE @kw OR b.publish LIKE @kw";
                    DataTable dt = DatabaseHelper.ExecuteQuery(sql,
                        DatabaseHelper.CreateParameter("@kw", "%" + txtKeyword.Text.Trim() + "%"));
                    dgvBibliography.DataSource = dt;
                }
                else
                {
                    sql += " ORDER BY b.create_time DESC";
                    dgvBibliography.DataSource = DatabaseHelper.ExecuteQuery(sql);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadBibliography();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            isNewMode = true;
            currentBibId = null;
            ClearForm();
            txtISBN.Focus();
        }

        private void ClearForm()
        {
            txtISBN.Clear();
            txtName.Clear();
            txtPublish.Clear();
            dtpPublishDate.Value = DateTime.Now;
            if (cboCategory.Items.Count > 0) cboCategory.SelectedIndex = 0;
            numPrice.Value = 0;
            txtDescription.Clear();
            txtAuthors.Clear();
            dgvItems.DataSource = null;
        }

        private void dgvBibliography_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBibliography.SelectedRows.Count == 0) return;

            var idCell = dgvBibliography.SelectedRows[0].Cells["ID"];
            if (idCell?.Value == null) return;

            currentBibId = Convert.ToInt32(idCell.Value);
            isNewMode = false;
            LoadBibliographyDetails(currentBibId.Value);
            LoadBookItems(currentBibId.Value);
        }

        private void LoadBibliographyDetails(int bibId)
        {
            try
            {
                string sql = "SELECT * FROM BIBLIOGRAPHY WHERE bibliography_id = @id";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.CreateParameter("@id", bibId));

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtISBN.Text = row["ISBN"].ToString();
                    txtName.Text = row["bibliography_name"].ToString();
                    txtPublish.Text = row["publish"]?.ToString() ?? "";
                    if (row["publish_date"] != DBNull.Value)
                        dtpPublishDate.Value = Convert.ToDateTime(row["publish_date"]);
                    txtDescription.Text = row["Description"]?.ToString() ?? "";
                    if (row["price"] != DBNull.Value)
                        numPrice.Value = Convert.ToDecimal(row["price"]);

                    int categoryId = Convert.ToInt32(row["category_id"]);
                    for (int i = 0; i < cboCategory.Items.Count; i++)
                    {
                        if (((CategoryItem)cboCategory.Items[i]).Id == categoryId)
                        {
                            cboCategory.SelectedIndex = i;
                            break;
                        }
                    }

                    // 加载作者
                    LoadAuthors(bibId);
                }
            }
            catch { }
        }

        private void LoadAuthors(int bibId)
        {
            try
            {
                string sql = @"SELECT a.author_name FROM BIBLIO_AUTHOR ba
                              INNER JOIN AUTHOR a ON ba.author_id = a.author_id
                              WHERE ba.bibliography_id = @id ORDER BY ba.author_order";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.CreateParameter("@id", bibId));

                var authors = new System.Collections.Generic.List<string>();
                foreach (DataRow row in dt.Rows)
                {
                    authors.Add(row["author_name"].ToString());
                }
                txtAuthors.Text = string.Join(", ", authors);
            }
            catch { }
        }

        private void LoadBookItems(int bibId)
        {
            try
            {
                string sql = @"SELECT bi.item_barcode AS 馆藏码, bi.current_status AS 状态,
                              sl.location_name AS 位置, bi.physical_condition AS 物理状态
                              FROM BOOK_ITEM bi
                              INNER JOIN STORAGE_LOCATION sl ON bi.location_id = sl.location_id
                              WHERE bi.bibliography_id = @id";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.CreateParameter("@id", bibId));
                dgvItems.DataSource = dt;
            }
            catch { }
        }

        private void btnLookupISBN_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtISBN.Text))
            {
                MessageBox.Show("请输入ISBN", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 检查数据库中是否已存在
            string sql = "SELECT * FROM BIBLIOGRAPHY WHERE ISBN = @isbn";
            DataTable dt = DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.CreateParameter("@isbn", txtISBN.Text.Trim()));

            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("该ISBN已存在于系统中，将加载现有信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                currentBibId = Convert.ToInt32(dt.Rows[0]["bibliography_id"]);
                isNewMode = false;
                LoadBibliographyDetails(currentBibId.Value);
                LoadBookItems(currentBibId.Value);
            }
            else
            {
                MessageBox.Show("ISBN未在系统中找到。您可以手动填写书目信息或从外部数据源获取。\n\n提示：实际应用中可集成ISBN查询API自动填充。",
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtISBN.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("请填写ISBN和书名", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboCategory.SelectedItem == null)
            {
                MessageBox.Show("请选择分类", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int categoryId = ((CategoryItem)cboCategory.SelectedItem).Id;
                string operatorName = AuthenticationService.Instance.CurrentUser?.Username ?? "system";

                if (isNewMode)
                {
                    // 检查ISBN唯一性
                    string checkSql = "SELECT COUNT(*) FROM BIBLIOGRAPHY WHERE ISBN = @isbn";
                    int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(checkSql,
                        DatabaseHelper.CreateParameter("@isbn", txtISBN.Text.Trim())));

                    if (count > 0)
                    {
                        MessageBox.Show("该ISBN已存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string sql = @"INSERT INTO BIBLIOGRAPHY (ISBN, bibliography_name, publish, publish_date, Description, category_id, price)
                                  VALUES (@isbn, @name, @pub, @pubDate, @desc, @catId, @price);
                                  SELECT SCOPE_IDENTITY();";

                    object result = DatabaseHelper.ExecuteScalar(sql,
                        DatabaseHelper.CreateParameter("@isbn", txtISBN.Text.Trim()),
                        DatabaseHelper.CreateParameter("@name", txtName.Text.Trim()),
                        DatabaseHelper.CreateParameter("@pub", txtPublish.Text.Trim()),
                        DatabaseHelper.CreateParameter("@pubDate", dtpPublishDate.Value.Date),
                        DatabaseHelper.CreateParameter("@desc", txtDescription.Text.Trim()),
                        DatabaseHelper.CreateParameter("@catId", categoryId),
                        DatabaseHelper.CreateParameter("@price", numPrice.Value));

                    currentBibId = Convert.ToInt32(result);

                    // 保存作者
                    SaveAuthors(currentBibId.Value);

                    LogCatalogAction("BIBLIOGRAPHY", txtISBN.Text, "新增", operatorName, $"录入书目：{txtName.Text}");
                }
                else if (currentBibId.HasValue)
                {
                    string sql = @"UPDATE BIBLIOGRAPHY SET ISBN = @isbn, bibliography_name = @name, 
                                  publish = @pub, publish_date = @pubDate, Description = @desc, 
                                  category_id = @catId, price = @price
                                  WHERE bibliography_id = @id";

                    DatabaseHelper.ExecuteNonQuery(sql,
                        DatabaseHelper.CreateParameter("@isbn", txtISBN.Text.Trim()),
                        DatabaseHelper.CreateParameter("@name", txtName.Text.Trim()),
                        DatabaseHelper.CreateParameter("@pub", txtPublish.Text.Trim()),
                        DatabaseHelper.CreateParameter("@pubDate", dtpPublishDate.Value.Date),
                        DatabaseHelper.CreateParameter("@desc", txtDescription.Text.Trim()),
                        DatabaseHelper.CreateParameter("@catId", categoryId),
                        DatabaseHelper.CreateParameter("@price", numPrice.Value),
                        DatabaseHelper.CreateParameter("@id", currentBibId.Value));

                    SaveAuthors(currentBibId.Value);

                    LogCatalogAction("BIBLIOGRAPHY", txtISBN.Text, "更新", operatorName, $"更新书目：{txtName.Text}");
                }

                MessageBox.Show("保存成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isNewMode = false;
                LoadBibliography();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAuthors(int bibId)
        {
            if (string.IsNullOrWhiteSpace(txtAuthors.Text)) return;

            try
            {
                // 删除现有关联
                string delSql = "DELETE FROM BIBLIO_AUTHOR WHERE bibliography_id = @id";
                DatabaseHelper.ExecuteNonQuery(delSql, DatabaseHelper.CreateParameter("@id", bibId));

                // 解析作者（逗号分隔）
                string[] authors = txtAuthors.Text.Split(new[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
                int order = 1;

                foreach (string authorName in authors)
                {
                    string name = authorName.Trim();
                    if (string.IsNullOrEmpty(name)) continue;

                    // 查找或创建作者
                    string findSql = "SELECT author_id FROM AUTHOR WHERE author_name = @name";
                    object authorIdObj = DatabaseHelper.ExecuteScalar(findSql,
                        DatabaseHelper.CreateParameter("@name", name));

                    int authorId;
                    if (authorIdObj == null || authorIdObj == DBNull.Value)
                    {
                        string insertSql = "INSERT INTO AUTHOR (author_name) VALUES (@name); SELECT SCOPE_IDENTITY();";
                        authorId = Convert.ToInt32(DatabaseHelper.ExecuteScalar(insertSql,
                            DatabaseHelper.CreateParameter("@name", name)));
                    }
                    else
                    {
                        authorId = Convert.ToInt32(authorIdObj);
                    }

                    // 创建关联
                    string linkSql = "INSERT INTO BIBLIO_AUTHOR (bibliography_id, author_id, author_order) VALUES (@bibId, @authId, @order)";
                    DatabaseHelper.ExecuteNonQuery(linkSql,
                        DatabaseHelper.CreateParameter("@bibId", bibId),
                        DatabaseHelper.CreateParameter("@authId", authorId),
                        DatabaseHelper.CreateParameter("@order", order++));
                }
            }
            catch { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!currentBibId.HasValue || isNewMode)
            {
                MessageBox.Show("请选择要删除的书目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 检查是否有馆藏
            string checkSql = "SELECT COUNT(*) FROM BOOK_ITEM WHERE bibliography_id = @id";
            int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(checkSql,
                DatabaseHelper.CreateParameter("@id", currentBibId.Value)));

            if (count > 0)
            {
                MessageBox.Show("该书目有馆藏实体，无法删除", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("确定删除该书目？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                // 删除作者关联
                string delAuthorSql = "DELETE FROM BIBLIO_AUTHOR WHERE bibliography_id = @id";
                DatabaseHelper.ExecuteNonQuery(delAuthorSql, DatabaseHelper.CreateParameter("@id", currentBibId.Value));

                // 删除书目
                string sql = "DELETE FROM BIBLIOGRAPHY WHERE bibliography_id = @id";
                DatabaseHelper.ExecuteNonQuery(sql, DatabaseHelper.CreateParameter("@id", currentBibId.Value));

                string operatorName = AuthenticationService.Instance.CurrentUser?.Username ?? "system";
                LogCatalogAction("BIBLIOGRAPHY", txtISBN.Text, "删除", operatorName, $"删除书目：{txtName.Text}");

                MessageBox.Show("删除成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCancel_Click(sender, e);
                LoadBibliography();
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            isNewMode = false;
            currentBibId = null;
            ClearForm();
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

        private class CategoryItem
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public override string ToString() => Text;
        }

        private void dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
