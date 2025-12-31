using System;
using System.Data;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 图书检索控件 - 所有用户可用
    /// 功能：多条件检索图书、查看馆藏状态、查看详情
    /// </summary>
    public partial class BookSearchControl : UserControl
    {
        public BookSearchControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panelSearch = new System.Windows.Forms.Panel();
            this.lblSearchTitle = new System.Windows.Forms.Label();
            this.lblKeyword = new System.Windows.Forms.Label();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.lblSearchType = new System.Windows.Forms.Label();
            this.cboSearchType = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.chkAvailableOnly = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.lblDetailsTitle = new System.Windows.Forms.Label();
            this.lblBookName = new System.Windows.Forms.Label();
            this.lblISBN = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblPublisher = new System.Windows.Forms.Label();
            this.lblCategoryInfo = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblItemsTitle = new System.Windows.Forms.Label();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.lblResultCount = new System.Windows.Forms.Label();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.panelDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelSearch.Controls.Add(this.lblResultCount);
            this.panelSearch.Controls.Add(this.btnClear);
            this.panelSearch.Controls.Add(this.btnSearch);
            this.panelSearch.Controls.Add(this.chkAvailableOnly);
            this.panelSearch.Controls.Add(this.cboCategory);
            this.panelSearch.Controls.Add(this.lblCategory);
            this.panelSearch.Controls.Add(this.cboSearchType);
            this.panelSearch.Controls.Add(this.lblSearchType);
            this.panelSearch.Controls.Add(this.txtKeyword);
            this.panelSearch.Controls.Add(this.lblKeyword);
            this.panelSearch.Controls.Add(this.lblSearchTitle);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(950, 90);
            // 
            // lblSearchTitle
            // 
            this.lblSearchTitle.AutoSize = true;
            this.lblSearchTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSearchTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.lblSearchTitle.Location = new System.Drawing.Point(15, 10);
            this.lblSearchTitle.Text = "📚 图书检索";
            // 
            // lblKeyword
            // 
            this.lblKeyword.AutoSize = true;
            this.lblKeyword.Location = new System.Drawing.Point(15, 50);
            this.lblKeyword.Text = "关键字：";
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(75, 47);
            this.txtKeyword.Size = new System.Drawing.Size(200, 23);
            this.txtKeyword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKeyword_KeyDown);
            // 
            // lblSearchType
            // 
            this.lblSearchType.AutoSize = true;
            this.lblSearchType.Location = new System.Drawing.Point(290, 50);
            this.lblSearchType.Text = "检索方式：";
            // 
            // cboSearchType
            // 
            this.cboSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSearchType.Location = new System.Drawing.Point(360, 47);
            this.cboSearchType.Size = new System.Drawing.Size(100, 25);
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(480, 50);
            this.lblCategory.Text = "分类：";
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.Location = new System.Drawing.Point(525, 47);
            this.cboCategory.Size = new System.Drawing.Size(130, 25);
            // 
            // chkAvailableOnly
            // 
            this.chkAvailableOnly.AutoSize = true;
            this.chkAvailableOnly.Location = new System.Drawing.Point(670, 50);
            this.chkAvailableOnly.Text = "仅显示可借";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(775, 45);
            this.btnSearch.Size = new System.Drawing.Size(80, 30);
            this.btnSearch.Text = "搜索";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(865, 45);
            this.btnClear.Size = new System.Drawing.Size(65, 30);
            this.btnClear.Text = "清空";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblResultCount
            // 
            this.lblResultCount.ForeColor = System.Drawing.Color.Gray;
            this.lblResultCount.Location = new System.Drawing.Point(200, 10);
            this.lblResultCount.Size = new System.Drawing.Size(300, 20);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 90);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Size = new System.Drawing.Size(950, 460);
            this.splitContainer.SplitterDistance = 480;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dgvResults);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panelDetails);
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResults.BackgroundColor = System.Drawing.Color.White;
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowHeadersVisible = false;
            this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResults.SelectionChanged += new System.EventHandler(this.dgvResults_SelectionChanged);
            // 
            // panelDetails
            // 
            this.panelDetails.Controls.Add(this.dgvItems);
            this.panelDetails.Controls.Add(this.lblItemsTitle);
            this.panelDetails.Controls.Add(this.txtDescription);
            this.panelDetails.Controls.Add(this.lblDescription);
            this.panelDetails.Controls.Add(this.lblCategoryInfo);
            this.panelDetails.Controls.Add(this.lblPublisher);
            this.panelDetails.Controls.Add(this.lblAuthor);
            this.panelDetails.Controls.Add(this.lblISBN);
            this.panelDetails.Controls.Add(this.lblBookName);
            this.panelDetails.Controls.Add(this.lblDetailsTitle);
            this.panelDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // lblDetailsTitle
            // 
            this.lblDetailsTitle.AutoSize = true;
            this.lblDetailsTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetailsTitle.Location = new System.Drawing.Point(10, 10);
            this.lblDetailsTitle.Text = "图书详情";
            // 
            // lblBookName
            // 
            this.lblBookName.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblBookName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblBookName.Location = new System.Drawing.Point(10, 40);
            this.lblBookName.Size = new System.Drawing.Size(430, 25);
            this.lblBookName.Text = "请选择一本书";
            // 
            // lblISBN
            // 
            this.lblISBN.Location = new System.Drawing.Point(10, 70);
            this.lblISBN.Size = new System.Drawing.Size(200, 20);
            this.lblISBN.Text = "ISBN：";
            // 
            // lblAuthor
            // 
            this.lblAuthor.Location = new System.Drawing.Point(10, 95);
            this.lblAuthor.Size = new System.Drawing.Size(430, 20);
            this.lblAuthor.Text = "作者：";
            // 
            // lblPublisher
            // 
            this.lblPublisher.Location = new System.Drawing.Point(10, 120);
            this.lblPublisher.Size = new System.Drawing.Size(430, 20);
            this.lblPublisher.Text = "出版社：";
            // 
            // lblCategoryInfo
            // 
            this.lblCategoryInfo.Location = new System.Drawing.Point(10, 145);
            this.lblCategoryInfo.Size = new System.Drawing.Size(200, 20);
            this.lblCategoryInfo.Text = "分类：";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(10, 175);
            this.lblDescription.Text = "简介：";
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.White;
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescription.Location = new System.Drawing.Point(60, 175);
            this.txtDescription.Multiline = true;
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(380, 60);
            // 
            // lblItemsTitle
            // 
            this.lblItemsTitle.AutoSize = true;
            this.lblItemsTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblItemsTitle.Location = new System.Drawing.Point(10, 245);
            this.lblItemsTitle.Text = "馆藏信息：";
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvItems.Location = new System.Drawing.Point(10, 270);
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.Size = new System.Drawing.Size(430, 180);
            // 
            // BookSearchControl
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelSearch);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Size = new System.Drawing.Size(950, 550);
            this.Load += new System.EventHandler(this.BookSearchControl_Load);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.panelDetails.ResumeLayout(false);
            this.panelDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Label lblSearchTitle;
        private System.Windows.Forms.Label lblKeyword;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Label lblSearchType;
        private System.Windows.Forms.ComboBox cboSearchType;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.CheckBox chkAvailableOnly;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblResultCount;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.Label lblDetailsTitle;
        private System.Windows.Forms.Label lblBookName;
        private System.Windows.Forms.Label lblISBN;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblPublisher;
        private System.Windows.Forms.Label lblCategoryInfo;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblItemsTitle;
        private System.Windows.Forms.DataGridView dgvItems;

        private void BookSearchControl_Load(object sender, EventArgs e)
        {
            LoadSearchTypes();
            LoadCategories();
        }

        private void LoadSearchTypes()
        {
            cboSearchType.Items.Clear();
            cboSearchType.Items.Add(new ComboItem { Value = "ALL", Text = "全部" });
            cboSearchType.Items.Add(new ComboItem { Value = "TITLE", Text = "书名" });
            cboSearchType.Items.Add(new ComboItem { Value = "ISBN", Text = "ISBN" });
            cboSearchType.Items.Add(new ComboItem { Value = "AUTHOR", Text = "作者" });
            cboSearchType.Items.Add(new ComboItem { Value = "PUBLISHER", Text = "出版社" });
            cboSearchType.SelectedIndex = 0;
        }

        private void LoadCategories()
        {
            cboCategory.Items.Clear();
            cboCategory.Items.Add(new ComboItem { Value = "", Text = "全部分类" });

            try
            {
                string sql = "SELECT category_id, category_code, category_name FROM BOOK_CATEGORY ORDER BY category_code";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql);
                foreach (DataRow row in dt.Rows)
                {
                    cboCategory.Items.Add(new ComboItem
                    {
                        Value = row["category_id"].ToString(),
                        Text = $"[{row["category_code"]}] {row["category_name"]}"
                    });
                }
            }
            catch { }

            cboCategory.SelectedIndex = 0;
        }

        private void txtKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchBooks();
        }

        private void SearchBooks()
        {
            try
            {
                string searchType = ((ComboItem)cboSearchType.SelectedItem).Value;
                string categoryId = ((ComboItem)cboCategory.SelectedItem).Value;
                bool availableOnly = chkAvailableOnly.Checked;
                string keyword = txtKeyword.Text.Trim();

                string sql = @"
                    SELECT DISTINCT b.bibliography_id AS ID, b.bibliography_name AS 书名, 
                           b.ISBN, b.publish AS 出版社, bc.category_name AS 分类,
                           b.price AS 定价,
                           (SELECT COUNT(*) FROM BOOK_ITEM bi WHERE bi.bibliography_id = b.bibliography_id AND bi.current_status = N'AVAILABLE') AS 可借数量,
                           (SELECT COUNT(*) FROM BOOK_ITEM bi WHERE bi.bibliography_id = b.bibliography_id) AS 馆藏总数
                    FROM BIBLIOGRAPHY b
                    INNER JOIN BOOK_CATEGORY bc ON b.category_id = bc.category_id
                    LEFT JOIN BIBLIO_AUTHOR ba ON b.bibliography_id = ba.bibliography_id
                    LEFT JOIN AUTHOR a ON ba.author_id = a.author_id
                    WHERE 1=1";

                var parameters = new System.Collections.Generic.List<System.Data.SqlClient.SqlParameter>();

                if (!string.IsNullOrEmpty(keyword))
                {
                    switch (searchType)
                    {
                        case "TITLE":
                            sql += " AND b.bibliography_name LIKE @kw";
                            break;
                        case "ISBN":
                            sql += " AND b.ISBN LIKE @kw";
                            break;
                        case "AUTHOR":
                            sql += " AND a.author_name LIKE @kw";
                            break;
                        case "PUBLISHER":
                            sql += " AND b.publish LIKE @kw";
                            break;
                        default:
                            sql += " AND (b.bibliography_name LIKE @kw OR b.ISBN LIKE @kw OR b.publish LIKE @kw OR a.author_name LIKE @kw)";
                            break;
                    }
                    parameters.Add(DatabaseHelper.CreateParameter("@kw", "%" + keyword + "%"));
                }

                if (!string.IsNullOrEmpty(categoryId))
                {
                    sql += " AND b.category_id = @catId";
                    parameters.Add(DatabaseHelper.CreateParameter("@catId", Convert.ToInt32(categoryId)));
                }

                if (availableOnly)
                {
                    sql += " AND EXISTS (SELECT 1 FROM BOOK_ITEM bi WHERE bi.bibliography_id = b.bibliography_id AND bi.current_status = N'AVAILABLE')";
                }

                sql += " ORDER BY b.create_time DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters.ToArray());
                dgvResults.DataSource = dt;

                lblResultCount.Text = $"找到 {dt.Rows.Count} 条结果";

                // 清空详情
                ClearDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show("搜索失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearDetails()
        {
            lblBookName.Text = "请选择一本书";
            lblISBN.Text = "ISBN：";
            lblAuthor.Text = "作者：";
            lblPublisher.Text = "出版社：";
            lblCategoryInfo.Text = "分类：";
            txtDescription.Text = "";
            dgvItems.DataSource = null;
        }

        private void dgvResults_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvResults.SelectedRows.Count == 0) return;

            var idCell = dgvResults.SelectedRows[0].Cells["ID"];
            if (idCell?.Value == null) return;

            int bibId = Convert.ToInt32(idCell.Value);
            LoadBookDetails(bibId);
        }

        private void LoadBookDetails(int bibId)
        {
            try
            {
                // 加载书目信息
                string sql = @"
                    SELECT b.*, bc.category_code, bc.category_name
                    FROM BIBLIOGRAPHY b
                    INNER JOIN BOOK_CATEGORY bc ON b.category_id = bc.category_id
                    WHERE b.bibliography_id = @id";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.CreateParameter("@id", bibId));

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    lblBookName.Text = row["bibliography_name"].ToString();
                    lblISBN.Text = $"ISBN：{row["ISBN"]}";
                    lblPublisher.Text = $"出版社：{row["publish"]}";
                    lblCategoryInfo.Text = $"分类：[{row["category_code"]}] {row["category_name"]}";
                    txtDescription.Text = row["Description"]?.ToString() ?? "";

                    // 加载作者
                    LoadAuthors(bibId);

                    // 加载馆藏
                    LoadBookItems(bibId);
                }
            }
            catch { }
        }

        private void LoadAuthors(int bibId)
        {
            try
            {
                string sql = @"SELECT a.author_name 
                              FROM BIBLIO_AUTHOR ba
                              INNER JOIN AUTHOR a ON ba.author_id = a.author_id
                              WHERE ba.bibliography_id = @id
                              ORDER BY ba.author_order";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.CreateParameter("@id", bibId));

                var authors = new System.Collections.Generic.List<string>();
                foreach (DataRow row in dt.Rows)
                {
                    authors.Add(row["author_name"].ToString());
                }

                lblAuthor.Text = "作者：" + (authors.Count > 0 ? string.Join(", ", authors) : "未知");
            }
            catch
            {
                lblAuthor.Text = "作者：未知";
            }
        }

        private void LoadBookItems(int bibId)
        {
            try
            {
                string sql = @"
                    SELECT bi.item_barcode AS 馆藏码, 
                           CASE bi.current_status 
                               WHEN 'AVAILABLE' THEN '可借' 
                               WHEN 'BORROWED' THEN '已借出'
                               WHEN 'RESERVED' THEN '已预约'
                               WHEN 'PROCESSING' THEN '处理中'
                               WHEN 'DAMAGED' THEN '损坏'
                               WHEN 'LOST' THEN '丢失'
                               ELSE bi.current_status 
                           END AS 状态,
                           sl.location_name AS 位置,
                           bi.physical_condition AS 物理状态
                    FROM BOOK_ITEM bi
                    INNER JOIN STORAGE_LOCATION sl ON bi.location_id = sl.location_id
                    WHERE bi.bibliography_id = @id
                    ORDER BY bi.current_status";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.CreateParameter("@id", bibId));
                dgvItems.DataSource = dt;

                // 设置状态列颜色
                dgvItems.CellFormatting += (s, e) =>
                {
                    if (dgvItems.Columns[e.ColumnIndex].HeaderText == "状态" && e.Value != null)
                    {
                        string status = e.Value.ToString();
                        if (status == "可借")
                        {
                            e.CellStyle.ForeColor = System.Drawing.Color.Green;
                            e.CellStyle.Font = new System.Drawing.Font(dgvItems.Font, System.Drawing.FontStyle.Bold);
                        }
                        else if (status == "已借出" || status == "已预约")
                        {
                            e.CellStyle.ForeColor = System.Drawing.Color.Orange;
                        }
                        else if (status == "损坏" || status == "丢失")
                        {
                            e.CellStyle.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                };
            }
            catch { }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtKeyword.Clear();
            cboSearchType.SelectedIndex = 0;
            cboCategory.SelectedIndex = 0;
            chkAvailableOnly.Checked = false;
            dgvResults.DataSource = null;
            lblResultCount.Text = "";
            ClearDetails();
        }

        private class ComboItem
        {
            public string Value { get; set; }
            public string Text { get; set; }
            public override string ToString() { return Text; }
        }
    }
}
