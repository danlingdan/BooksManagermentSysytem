using System;
using System.Data;
using System.Drawing;
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
            this.searchLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblSearchTitle = new System.Windows.Forms.Label();
            this.lblResultCount = new System.Windows.Forms.Label();
            this.lblKeyword = new System.Windows.Forms.Label();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.lblSearchType = new System.Windows.Forms.Label();
            this.cboSearchType = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.chkAvailableOnly = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.detailsLayout = new System.Windows.Forms.TableLayoutPanel();
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
            this.panelSearch = new System.Windows.Forms.Panel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.searchLayout.SuspendLayout();
            this.detailsLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.panelDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchLayout
            // 
            this.searchLayout.AutoSize = true;
            this.searchLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.searchLayout.ColumnCount = 8;
            this.searchLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.searchLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.searchLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.searchLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.searchLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.searchLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.searchLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.searchLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.searchLayout.Controls.Add(this.lblSearchTitle, 0, 0);
            this.searchLayout.Controls.Add(this.lblResultCount, 1, 0);
            this.searchLayout.Controls.Add(this.lblKeyword, 0, 1);
            this.searchLayout.Controls.Add(this.txtKeyword, 0, 2);
            this.searchLayout.Controls.Add(this.lblSearchType, 1, 1);
            this.searchLayout.Controls.Add(this.cboSearchType, 1, 2);
            this.searchLayout.Controls.Add(this.lblCategory, 2, 1);
            this.searchLayout.Controls.Add(this.cboCategory, 2, 2);
            this.searchLayout.Controls.Add(this.chkAvailableOnly, 3, 2);
            this.searchLayout.Controls.Add(this.btnSearch, 5, 2);
            this.searchLayout.Controls.Add(this.btnClear, 6, 2);
            this.searchLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchLayout.Location = new System.Drawing.Point(22, 15);
            this.searchLayout.Margin = new System.Windows.Forms.Padding(4);
            this.searchLayout.Name = "searchLayout";
            this.searchLayout.RowCount = 3;
            this.searchLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.searchLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.searchLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.searchLayout.Size = new System.Drawing.Size(1381, 133);
            this.searchLayout.TabIndex = 0;
            // 
            // lblSearchTitle
            // 
            this.lblSearchTitle.AutoSize = true;
            this.lblSearchTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSearchTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.lblSearchTitle.Location = new System.Drawing.Point(0, 0);
            this.lblSearchTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.lblSearchTitle.Name = "lblSearchTitle";
            this.lblSearchTitle.Size = new System.Drawing.Size(139, 30);
            this.lblSearchTitle.TabIndex = 0;
            this.lblSearchTitle.Text = "📚 图书检索";
            // 
            // lblResultCount
            // 
            this.lblResultCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblResultCount.AutoSize = true;
            this.searchLayout.SetColumnSpan(this.lblResultCount, 7);
            this.lblResultCount.ForeColor = System.Drawing.Color.Gray;
            this.lblResultCount.Location = new System.Drawing.Point(192, 3);
            this.lblResultCount.Margin = new System.Windows.Forms.Padding(15, 0, 0, 15);
            this.lblResultCount.Name = "lblResultCount";
            this.lblResultCount.Size = new System.Drawing.Size(0, 24);
            this.lblResultCount.TabIndex = 1;
            // 
            // lblKeyword
            // 
            this.lblKeyword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblKeyword.AutoSize = true;
            this.lblKeyword.Location = new System.Drawing.Point(0, 45);
            this.lblKeyword.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.lblKeyword.Name = "lblKeyword";
            this.lblKeyword.Size = new System.Drawing.Size(82, 24);
            this.lblKeyword.TabIndex = 2;
            this.lblKeyword.Text = "关键字：";
            // 
            // txtKeyword
            // 
            this.txtKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKeyword.Location = new System.Drawing.Point(0, 88);
            this.txtKeyword.Margin = new System.Windows.Forms.Padding(0, 4, 15, 4);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(162, 30);
            this.txtKeyword.TabIndex = 3;
            this.txtKeyword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKeyword_KeyDown);
            // 
            // lblSearchType
            // 
            this.lblSearchType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSearchType.AutoSize = true;
            this.lblSearchType.Location = new System.Drawing.Point(177, 45);
            this.lblSearchType.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.lblSearchType.Name = "lblSearchType";
            this.lblSearchType.Size = new System.Drawing.Size(100, 24);
            this.lblSearchType.TabIndex = 4;
            this.lblSearchType.Text = "检索方式：";
            // 
            // cboSearchType
            // 
            this.cboSearchType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSearchType.Location = new System.Drawing.Point(177, 87);
            this.cboSearchType.Margin = new System.Windows.Forms.Padding(0, 4, 15, 4);
            this.cboSearchType.Name = "cboSearchType";
            this.cboSearchType.Size = new System.Drawing.Size(180, 32);
            this.cboSearchType.TabIndex = 5;
            // 
            // lblCategory
            // 
            this.lblCategory.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(372, 45);
            this.lblCategory.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(64, 24);
            this.lblCategory.TabIndex = 6;
            this.lblCategory.Text = "分类：";
            // 
            // cboCategory
            // 
            this.cboCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.Location = new System.Drawing.Point(372, 87);
            this.cboCategory.Margin = new System.Windows.Forms.Padding(0, 4, 15, 4);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(118, 32);
            this.cboCategory.TabIndex = 7;
            // 
            // chkAvailableOnly
            // 
            this.chkAvailableOnly.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkAvailableOnly.AutoSize = true;
            this.searchLayout.SetColumnSpan(this.chkAvailableOnly, 2);
            this.chkAvailableOnly.Location = new System.Drawing.Point(505, 89);
            this.chkAvailableOnly.Margin = new System.Windows.Forms.Padding(0, 4, 15, 4);
            this.chkAvailableOnly.Name = "chkAvailableOnly";
            this.chkAvailableOnly.Size = new System.Drawing.Size(126, 28);
            this.chkAvailableOnly.TabIndex = 8;
            this.chkAvailableOnly.Text = "仅显示可借";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSearch.AutoSize = true;
            this.btnSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(752, 77);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(0, 4, 8, 4);
            this.btnSearch.MinimumSize = new System.Drawing.Size(120, 45);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(22, 8, 22, 8);
            this.btnSearch.Size = new System.Drawing.Size(120, 52);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "搜索";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnClear.AutoSize = true;
            this.btnClear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClear.Location = new System.Drawing.Point(880, 78);
            this.btnClear.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.btnClear.MinimumSize = new System.Drawing.Size(98, 45);
            this.btnClear.Name = "btnClear";
            this.btnClear.Padding = new System.Windows.Forms.Padding(22, 8, 22, 8);
            this.btnClear.Size = new System.Drawing.Size(100, 50);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "清空";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // detailsLayout
            // 
            this.detailsLayout.AutoSize = true;
            this.detailsLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.detailsLayout.ColumnCount = 1;
            this.detailsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.detailsLayout.Controls.Add(this.lblDetailsTitle, 0, 0);
            this.detailsLayout.Controls.Add(this.lblBookName, 0, 1);
            this.detailsLayout.Controls.Add(this.lblISBN, 0, 2);
            this.detailsLayout.Controls.Add(this.lblAuthor, 0, 3);
            this.detailsLayout.Controls.Add(this.lblPublisher, 0, 4);
            this.detailsLayout.Controls.Add(this.lblCategoryInfo, 0, 5);
            this.detailsLayout.Controls.Add(this.lblDescription, 0, 6);
            this.detailsLayout.Controls.Add(this.txtDescription, 0, 7);
            this.detailsLayout.Controls.Add(this.lblItemsTitle, 0, 8);
            this.detailsLayout.Controls.Add(this.dgvItems, 0, 9);
            this.detailsLayout.Dock = System.Windows.Forms.DockStyle.Top;
            this.detailsLayout.Location = new System.Drawing.Point(15, 15);
            this.detailsLayout.Margin = new System.Windows.Forms.Padding(4);
            this.detailsLayout.Name = "detailsLayout";
            this.detailsLayout.RowCount = 10;
            this.detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.detailsLayout.Size = new System.Drawing.Size(0, 736);
            this.detailsLayout.TabIndex = 0;
            // 
            // lblDetailsTitle
            // 
            this.lblDetailsTitle.AutoSize = true;
            this.lblDetailsTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetailsTitle.Location = new System.Drawing.Point(0, 0);
            this.lblDetailsTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.lblDetailsTitle.Name = "lblDetailsTitle";
            this.lblDetailsTitle.Size = new System.Drawing.Size(1, 27);
            this.lblDetailsTitle.TabIndex = 0;
            this.lblDetailsTitle.Text = "图书详情";
            // 
            // lblBookName
            // 
            this.lblBookName.AutoSize = true;
            this.lblBookName.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblBookName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblBookName.Location = new System.Drawing.Point(0, 42);
            this.lblBookName.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblBookName.MaximumSize = new System.Drawing.Size(645, 0);
            this.lblBookName.Name = "lblBookName";
            this.lblBookName.Size = new System.Drawing.Size(1, 30);
            this.lblBookName.TabIndex = 1;
            this.lblBookName.Text = "请选择一本书";
            // 
            // lblISBN
            // 
            this.lblISBN.AutoSize = true;
            this.lblISBN.Location = new System.Drawing.Point(0, 88);
            this.lblISBN.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.lblISBN.Name = "lblISBN";
            this.lblISBN.Size = new System.Drawing.Size(1, 24);
            this.lblISBN.TabIndex = 2;
            this.lblISBN.Text = "ISBN：";
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Location = new System.Drawing.Point(0, 128);
            this.lblAuthor.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.lblAuthor.MaximumSize = new System.Drawing.Size(645, 0);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(1, 24);
            this.lblAuthor.TabIndex = 3;
            this.lblAuthor.Text = "作者：";
            // 
            // lblPublisher
            // 
            this.lblPublisher.AutoSize = true;
            this.lblPublisher.Location = new System.Drawing.Point(0, 168);
            this.lblPublisher.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.lblPublisher.MaximumSize = new System.Drawing.Size(645, 0);
            this.lblPublisher.Name = "lblPublisher";
            this.lblPublisher.Size = new System.Drawing.Size(1, 24);
            this.lblPublisher.TabIndex = 4;
            this.lblPublisher.Text = "出版社：";
            // 
            // lblCategoryInfo
            // 
            this.lblCategoryInfo.AutoSize = true;
            this.lblCategoryInfo.Location = new System.Drawing.Point(0, 208);
            this.lblCategoryInfo.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.lblCategoryInfo.Name = "lblCategoryInfo";
            this.lblCategoryInfo.Size = new System.Drawing.Size(1, 24);
            this.lblCategoryInfo.TabIndex = 5;
            this.lblCategoryInfo.Text = "分类：";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(0, 255);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(0, 15, 0, 4);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(1, 24);
            this.lblDescription.TabIndex = 6;
            this.lblDescription.Text = "简介：";
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.White;
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(0, 283);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.txtDescription.MinimumSize = new System.Drawing.Size(0, 90);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(1, 90);
            this.txtDescription.TabIndex = 7;
            // 
            // lblItemsTitle
            // 
            this.lblItemsTitle.AutoSize = true;
            this.lblItemsTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblItemsTitle.Location = new System.Drawing.Point(0, 403);
            this.lblItemsTitle.Margin = new System.Windows.Forms.Padding(0, 15, 0, 8);
            this.lblItemsTitle.Name = "lblItemsTitle";
            this.lblItemsTitle.Size = new System.Drawing.Size(1, 25);
            this.lblItemsTitle.TabIndex = 8;
            this.lblItemsTitle.Text = "馆藏信息：";
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvItems.ColumnHeadersHeight = 40;
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.Location = new System.Drawing.Point(0, 436);
            this.dgvItems.Margin = new System.Windows.Forms.Padding(0);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.RowHeadersWidth = 62;
            this.dgvItems.Size = new System.Drawing.Size(1, 300);
            this.dgvItems.TabIndex = 9;
            // 
            // panelSearch
            // 
            this.panelSearch.AutoSize = true;
            this.panelSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelSearch.Controls.Add(this.searchLayout);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Margin = new System.Windows.Forms.Padding(4);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Padding = new System.Windows.Forms.Padding(22, 15, 22, 15);
            this.panelSearch.Size = new System.Drawing.Size(1425, 163);
            this.panelSearch.TabIndex = 1;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(0, 163);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dgvResults);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panelDetails);
            this.splitContainer.Size = new System.Drawing.Size(1425, 662);
            this.splitContainer.SplitterDistance = 1376;
            this.splitContainer.SplitterWidth = 8;
            this.splitContainer.TabIndex = 0;
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResults.BackgroundColor = System.Drawing.Color.White;
            this.dgvResults.ColumnHeadersHeight = 40;
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.Location = new System.Drawing.Point(0, 0);
            this.dgvResults.Margin = new System.Windows.Forms.Padding(4);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowHeadersVisible = false;
            this.dgvResults.RowHeadersWidth = 62;
            this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResults.Size = new System.Drawing.Size(1376, 662);
            this.dgvResults.TabIndex = 0;
            this.dgvResults.SelectionChanged += new System.EventHandler(this.dgvResults_SelectionChanged);
            // 
            // panelDetails
            // 
            this.panelDetails.AutoScroll = true;
            this.panelDetails.BackColor = System.Drawing.Color.White;
            this.panelDetails.Controls.Add(this.detailsLayout);
            this.panelDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetails.Location = new System.Drawing.Point(0, 0);
            this.panelDetails.Margin = new System.Windows.Forms.Padding(4);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Padding = new System.Windows.Forms.Padding(15);
            this.panelDetails.Size = new System.Drawing.Size(41, 662);
            this.panelDetails.TabIndex = 0;
            // 
            // BookSearchControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelSearch);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1200, 750);
            this.Name = "BookSearchControl";
            this.Size = new System.Drawing.Size(1425, 825);
            this.Load += new System.EventHandler(this.BookSearchControl_Load);
            this.searchLayout.ResumeLayout(false);
            this.searchLayout.PerformLayout();
            this.detailsLayout.ResumeLayout(false);
            this.detailsLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.panelDetails.ResumeLayout(false);
            this.panelDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private TableLayoutPanel searchLayout;
        private TableLayoutPanel detailsLayout;
        private System.Windows.Forms.DataGridView dgvItems;

        private void BookSearchControl_Load(object sender, EventArgs e)
        {
            LoadSearchTypes();
            LoadCategories();
            SearchBooks();
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
                    SELECT b.bibliography_id AS ID, b.bibliography_name AS 书名, 
                           b.ISBN, b.publish AS 出版社, bc.category_name AS 分类,
                           b.price AS 定价,
                           (SELECT COUNT(*) FROM BOOK_ITEM bi WHERE bi.bibliography_id = b.bibliography_id AND bi.current_status = N'AVAILABLE') AS 可借数量,
                           (SELECT COUNT(*) FROM BOOK_ITEM bi WHERE bi.bibliography_id = b.bibliography_id) AS 馆藏总数,
                           b.create_time
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

                sql += " GROUP BY b.bibliography_id, b.bibliography_name, b.ISBN, b.publish, bc.category_name, b.price, b.create_time";
                sql += " ORDER BY b.create_time DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters.ToArray());
                
                // 移除 create_time 列,不显示给用户
                if (dt.Columns.Contains("create_time"))
                {
                    dt.Columns.Remove("create_time");
                }
                
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
