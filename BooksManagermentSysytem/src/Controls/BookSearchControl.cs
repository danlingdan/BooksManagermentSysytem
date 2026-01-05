using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
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
            this.advancedPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.lblAuthorFilter = new System.Windows.Forms.Label();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.lblPublisherFilter = new System.Windows.Forms.Label();
            this.txtPublisher = new System.Windows.Forms.TextBox();
            this.lblIsbnFilter = new System.Windows.Forms.Label();
            this.txtIsbn = new System.Windows.Forms.TextBox();
            this.lblPublishYear = new System.Windows.Forms.Label();
            this.numYearFrom = new System.Windows.Forms.NumericUpDown();
            this.lblYearSeparator = new System.Windows.Forms.Label();
            this.numYearTo = new System.Windows.Forms.NumericUpDown();
            this.lblSort = new System.Windows.Forms.Label();
            this.cboSort = new System.Windows.Forms.ComboBox();
            this.lblPageSize = new System.Windows.Forms.Label();
            this.numPageSize = new System.Windows.Forms.NumericUpDown();
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
            this.panelResultsContainer = new System.Windows.Forms.Panel();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.paginationPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPrevPage = new System.Windows.Forms.Button();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.searchLayout.SuspendLayout();
            this.advancedPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numYearFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYearTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPageSize)).BeginInit();
            this.detailsLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panelResultsContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.paginationPanel.SuspendLayout();
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
            this.searchLayout.Controls.Add(this.advancedPanel, 0, 3);
            this.searchLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchLayout.Location = new System.Drawing.Point(22, 15);
            this.searchLayout.Margin = new System.Windows.Forms.Padding(4);
            this.searchLayout.Name = "searchLayout";
            this.searchLayout.RowCount = 4;
            this.searchLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.searchLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.searchLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.searchLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.searchLayout.Size = new System.Drawing.Size(1381, 213);
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
            this.cboSearchType.Location = new System.Drawing.Point(177, 89);
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
            this.cboCategory.Location = new System.Drawing.Point(372, 89);
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
            // advancedPanel
            // 
            this.advancedPanel.AutoSize = true;
            this.advancedPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.searchLayout.SetColumnSpan(this.advancedPanel, 8);
            this.advancedPanel.Controls.Add(this.lblAuthorFilter);
            this.advancedPanel.Controls.Add(this.txtAuthor);
            this.advancedPanel.Controls.Add(this.lblPublisherFilter);
            this.advancedPanel.Controls.Add(this.txtPublisher);
            this.advancedPanel.Controls.Add(this.lblIsbnFilter);
            this.advancedPanel.Controls.Add(this.txtIsbn);
            this.advancedPanel.Controls.Add(this.lblPublishYear);
            this.advancedPanel.Controls.Add(this.numYearFrom);
            this.advancedPanel.Controls.Add(this.lblYearSeparator);
            this.advancedPanel.Controls.Add(this.numYearTo);
            this.advancedPanel.Controls.Add(this.lblSort);
            this.advancedPanel.Controls.Add(this.cboSort);
            this.advancedPanel.Controls.Add(this.lblPageSize);
            this.advancedPanel.Controls.Add(this.numPageSize);
            this.advancedPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.advancedPanel.Location = new System.Drawing.Point(0, 145);
            this.advancedPanel.Margin = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.advancedPanel.Name = "advancedPanel";
            this.advancedPanel.Size = new System.Drawing.Size(1381, 68);
            this.advancedPanel.TabIndex = 11;
            // 
            // lblAuthorFilter
            // 
            this.lblAuthorFilter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAuthorFilter.AutoSize = true;
            this.lblAuthorFilter.Location = new System.Drawing.Point(0, 5);
            this.lblAuthorFilter.Margin = new System.Windows.Forms.Padding(0, 4, 6, 4);
            this.lblAuthorFilter.Name = "lblAuthorFilter";
            this.lblAuthorFilter.Size = new System.Drawing.Size(64, 24);
            this.lblAuthorFilter.TabIndex = 11;
            this.lblAuthorFilter.Text = "作者：";
            // 
            // txtAuthor
            // 
            this.txtAuthor.Location = new System.Drawing.Point(70, 0);
            this.txtAuthor.Margin = new System.Windows.Forms.Padding(0, 0, 15, 4);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(150, 30);
            this.txtAuthor.TabIndex = 12;
            // 
            // lblPublisherFilter
            // 
            this.lblPublisherFilter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPublisherFilter.AutoSize = true;
            this.lblPublisherFilter.Location = new System.Drawing.Point(235, 5);
            this.lblPublisherFilter.Margin = new System.Windows.Forms.Padding(0, 4, 6, 4);
            this.lblPublisherFilter.Name = "lblPublisherFilter";
            this.lblPublisherFilter.Size = new System.Drawing.Size(82, 24);
            this.lblPublisherFilter.TabIndex = 13;
            this.lblPublisherFilter.Text = "出版社：";
            // 
            // txtPublisher
            // 
            this.txtPublisher.Location = new System.Drawing.Point(323, 0);
            this.txtPublisher.Margin = new System.Windows.Forms.Padding(0, 0, 15, 4);
            this.txtPublisher.Name = "txtPublisher";
            this.txtPublisher.Size = new System.Drawing.Size(160, 30);
            this.txtPublisher.TabIndex = 14;
            // 
            // lblIsbnFilter
            // 
            this.lblIsbnFilter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblIsbnFilter.AutoSize = true;
            this.lblIsbnFilter.Location = new System.Drawing.Point(498, 5);
            this.lblIsbnFilter.Margin = new System.Windows.Forms.Padding(0, 4, 6, 4);
            this.lblIsbnFilter.Name = "lblIsbnFilter";
            this.lblIsbnFilter.Size = new System.Drawing.Size(69, 24);
            this.lblIsbnFilter.TabIndex = 15;
            this.lblIsbnFilter.Text = "ISBN：";
            // 
            // txtIsbn
            // 
            this.txtIsbn.Location = new System.Drawing.Point(573, 0);
            this.txtIsbn.Margin = new System.Windows.Forms.Padding(0, 0, 15, 4);
            this.txtIsbn.Name = "txtIsbn";
            this.txtIsbn.Size = new System.Drawing.Size(140, 30);
            this.txtIsbn.TabIndex = 16;
            // 
            // lblPublishYear
            // 
            this.lblPublishYear.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPublishYear.AutoSize = true;
            this.lblPublishYear.Location = new System.Drawing.Point(728, 5);
            this.lblPublishYear.Margin = new System.Windows.Forms.Padding(0, 4, 6, 4);
            this.lblPublishYear.Name = "lblPublishYear";
            this.lblPublishYear.Size = new System.Drawing.Size(100, 24);
            this.lblPublishYear.TabIndex = 17;
            this.lblPublishYear.Text = "出版年份：";
            // 
            // numYearFrom
            // 
            this.numYearFrom.Location = new System.Drawing.Point(834, 0);
            this.numYearFrom.Margin = new System.Windows.Forms.Padding(0, 0, 6, 4);
            this.numYearFrom.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.numYearFrom.Name = "numYearFrom";
            this.numYearFrom.Size = new System.Drawing.Size(80, 30);
            this.numYearFrom.TabIndex = 18;
            // 
            // lblYearSeparator
            // 
            this.lblYearSeparator.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblYearSeparator.AutoSize = true;
            this.lblYearSeparator.Location = new System.Drawing.Point(920, 5);
            this.lblYearSeparator.Margin = new System.Windows.Forms.Padding(0, 4, 6, 4);
            this.lblYearSeparator.Name = "lblYearSeparator";
            this.lblYearSeparator.Size = new System.Drawing.Size(23, 24);
            this.lblYearSeparator.TabIndex = 19;
            this.lblYearSeparator.Text = "~";
            // 
            // numYearTo
            // 
            this.numYearTo.Location = new System.Drawing.Point(949, 0);
            this.numYearTo.Margin = new System.Windows.Forms.Padding(0, 0, 15, 4);
            this.numYearTo.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.numYearTo.Name = "numYearTo";
            this.numYearTo.Size = new System.Drawing.Size(80, 30);
            this.numYearTo.TabIndex = 20;
            // 
            // lblSort
            // 
            this.lblSort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSort.AutoSize = true;
            this.lblSort.Location = new System.Drawing.Point(1044, 5);
            this.lblSort.Margin = new System.Windows.Forms.Padding(0, 4, 6, 4);
            this.lblSort.Name = "lblSort";
            this.lblSort.Size = new System.Drawing.Size(100, 24);
            this.lblSort.TabIndex = 21;
            this.lblSort.Text = "排序方式：";
            // 
            // cboSort
            // 
            this.cboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSort.Location = new System.Drawing.Point(1150, 0);
            this.cboSort.Margin = new System.Windows.Forms.Padding(0, 0, 15, 4);
            this.cboSort.Name = "cboSort";
            this.cboSort.Size = new System.Drawing.Size(180, 32);
            this.cboSort.TabIndex = 22;
            this.cboSort.SelectedIndexChanged += new System.EventHandler(this.cboSort_SelectedIndexChanged);
            // 
            // lblPageSize
            // 
            this.lblPageSize.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPageSize.AutoSize = true;
            this.lblPageSize.Location = new System.Drawing.Point(0, 39);
            this.lblPageSize.Margin = new System.Windows.Forms.Padding(0, 4, 6, 4);
            this.lblPageSize.Name = "lblPageSize";
            this.lblPageSize.Size = new System.Drawing.Size(64, 24);
            this.lblPageSize.TabIndex = 23;
            this.lblPageSize.Text = "每页：";
            // 
            // numPageSize
            // 
            this.numPageSize.Location = new System.Drawing.Point(70, 34);
            this.numPageSize.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.numPageSize.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numPageSize.Name = "numPageSize";
            this.numPageSize.Size = new System.Drawing.Size(80, 30);
            this.numPageSize.TabIndex = 24;
            this.numPageSize.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numPageSize.ValueChanged += new System.EventHandler(this.numPageSize_ValueChanged);
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
            this.detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.detailsLayout.Size = new System.Drawing.Size(395, 632);
            this.detailsLayout.TabIndex = 0;
            // 
            // lblDetailsTitle
            // 
            this.lblDetailsTitle.AutoSize = true;
            this.lblDetailsTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetailsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.lblDetailsTitle.Location = new System.Drawing.Point(0, 0);
            this.lblDetailsTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.lblDetailsTitle.Name = "lblDetailsTitle";
            this.lblDetailsTitle.Size = new System.Drawing.Size(126, 27);
            this.lblDetailsTitle.TabIndex = 0;
            this.lblDetailsTitle.Text = "📖 图书详情";
            this.lblDetailsTitle.Visible = false;
            // 
            // lblBookName
            // 
            this.lblBookName.AutoSize = true;
            this.lblBookName.Location = new System.Drawing.Point(0, 42);
            this.lblBookName.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblBookName.Name = "lblBookName";
            this.lblBookName.Size = new System.Drawing.Size(64, 24);
            this.lblBookName.TabIndex = 1;
            this.lblBookName.Text = "书名：";
            this.lblBookName.Visible = false;
            // 
            // lblISBN
            // 
            this.lblISBN.AutoSize = true;
            this.lblISBN.Location = new System.Drawing.Point(0, 74);
            this.lblISBN.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblISBN.Name = "lblISBN";
            this.lblISBN.Size = new System.Drawing.Size(69, 24);
            this.lblISBN.TabIndex = 2;
            this.lblISBN.Text = "ISBN：";
            this.lblISBN.Visible = false;
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Location = new System.Drawing.Point(0, 106);
            this.lblAuthor.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(64, 24);
            this.lblAuthor.TabIndex = 3;
            this.lblAuthor.Text = "作者：";
            this.lblAuthor.Visible = false;
            // 
            // lblPublisher
            // 
            this.lblPublisher.AutoSize = true;
            this.lblPublisher.Location = new System.Drawing.Point(0, 138);
            this.lblPublisher.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblPublisher.Name = "lblPublisher";
            this.lblPublisher.Size = new System.Drawing.Size(82, 24);
            this.lblPublisher.TabIndex = 4;
            this.lblPublisher.Text = "出版社：";
            this.lblPublisher.Visible = false;
            // 
            // lblCategoryInfo
            // 
            this.lblCategoryInfo.AutoSize = true;
            this.lblCategoryInfo.Location = new System.Drawing.Point(0, 170);
            this.lblCategoryInfo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblCategoryInfo.Name = "lblCategoryInfo";
            this.lblCategoryInfo.Size = new System.Drawing.Size(64, 24);
            this.lblCategoryInfo.TabIndex = 5;
            this.lblCategoryInfo.Text = "分类：";
            this.lblCategoryInfo.Visible = false;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescription.Location = new System.Drawing.Point(0, 202);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(66, 25);
            this.lblDescription.TabIndex = 6;
            this.lblDescription.Text = "简介：";
            this.lblDescription.Visible = false;
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(0, 235);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(395, 100);
            this.txtDescription.TabIndex = 7;
            this.txtDescription.Visible = false;
            // 
            // lblItemsTitle
            // 
            this.lblItemsTitle.AutoSize = true;
            this.lblItemsTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblItemsTitle.Location = new System.Drawing.Point(0, 350);
            this.lblItemsTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblItemsTitle.Name = "lblItemsTitle";
            this.lblItemsTitle.Size = new System.Drawing.Size(102, 25);
            this.lblItemsTitle.TabIndex = 8;
            this.lblItemsTitle.Text = "馆藏情况：";
            this.lblItemsTitle.Visible = false;
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvItems.ColumnHeadersHeight = 35;
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.Location = new System.Drawing.Point(0, 383);
            this.dgvItems.Margin = new System.Windows.Forms.Padding(0);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.RowHeadersWidth = 62;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(395, 249);
            this.dgvItems.TabIndex = 9;
            this.dgvItems.Visible = false;
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
            this.panelSearch.Size = new System.Drawing.Size(1425, 243);
            this.panelSearch.TabIndex = 1;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(0, 243);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.panelResultsContainer);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panelDetails);
            this.splitContainer.Size = new System.Drawing.Size(1425, 582);
            this.splitContainer.SplitterDistance = 966;
            this.splitContainer.SplitterWidth = 8;
            this.splitContainer.TabIndex = 0;
            // 
            // panelResultsContainer
            // 
            this.panelResultsContainer.Controls.Add(this.dgvResults);
            this.panelResultsContainer.Controls.Add(this.paginationPanel);
            this.panelResultsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelResultsContainer.Location = new System.Drawing.Point(0, 0);
            this.panelResultsContainer.Margin = new System.Windows.Forms.Padding(0);
            this.panelResultsContainer.Name = "panelResultsContainer";
            this.panelResultsContainer.Size = new System.Drawing.Size(966, 582);
            this.panelResultsContainer.TabIndex = 1;
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
            this.dgvResults.Size = new System.Drawing.Size(966, 522);
            this.dgvResults.TabIndex = 0;
            this.dgvResults.SelectionChanged += new System.EventHandler(this.dgvResults_SelectionChanged);
            // 
            // paginationPanel
            // 
            this.paginationPanel.AutoSize = true;
            this.paginationPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.paginationPanel.Controls.Add(this.btnPrevPage);
            this.paginationPanel.Controls.Add(this.btnNextPage);
            this.paginationPanel.Controls.Add(this.lblPageInfo);
            this.paginationPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.paginationPanel.Location = new System.Drawing.Point(0, 522);
            this.paginationPanel.Margin = new System.Windows.Forms.Padding(0);
            this.paginationPanel.Name = "paginationPanel";
            this.paginationPanel.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.paginationPanel.Size = new System.Drawing.Size(966, 60);
            this.paginationPanel.TabIndex = 1;
            this.paginationPanel.WrapContents = false;
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.AutoSize = true;
            this.btnPrevPage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPrevPage.Location = new System.Drawing.Point(10, 8);
            this.btnPrevPage.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnPrevPage.MinimumSize = new System.Drawing.Size(100, 32);
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.btnPrevPage.Size = new System.Drawing.Size(100, 44);
            this.btnPrevPage.TabIndex = 0;
            this.btnPrevPage.Text = "上一页";
            this.btnPrevPage.UseVisualStyleBackColor = true;
            this.btnPrevPage.Click += new System.EventHandler(this.btnPrevPage_Click);
            // 
            // btnNextPage
            // 
            this.btnNextPage.AutoSize = true;
            this.btnNextPage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNextPage.Location = new System.Drawing.Point(120, 8);
            this.btnNextPage.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.btnNextPage.MinimumSize = new System.Drawing.Size(100, 32);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.btnNextPage.Size = new System.Drawing.Size(100, 44);
            this.btnNextPage.TabIndex = 1;
            this.btnNextPage.Text = "下一页";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblPageInfo.Location = new System.Drawing.Point(235, 18);
            this.lblPageInfo.Margin = new System.Windows.Forms.Padding(0);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(86, 24);
            this.lblPageInfo.TabIndex = 2;
            this.lblPageInfo.Text = "第 1/1 页";
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
            this.panelDetails.Size = new System.Drawing.Size(451, 582);
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
            this.advancedPanel.ResumeLayout(false);
            this.advancedPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numYearFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYearTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPageSize)).EndInit();
            this.detailsLayout.ResumeLayout(false);
            this.detailsLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panelResultsContainer.ResumeLayout(false);
            this.panelResultsContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.paginationPanel.ResumeLayout(false);
            this.paginationPanel.PerformLayout();
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
        private System.Windows.Forms.FlowLayoutPanel advancedPanel;
        private System.Windows.Forms.Label lblAuthorFilter;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.Label lblPublisherFilter;
        private System.Windows.Forms.TextBox txtPublisher;
        private System.Windows.Forms.Label lblIsbnFilter;
        private System.Windows.Forms.TextBox txtIsbn;
        private System.Windows.Forms.Label lblPublishYear;
        private System.Windows.Forms.NumericUpDown numYearFrom;
        private System.Windows.Forms.Label lblYearSeparator;
        private System.Windows.Forms.NumericUpDown numYearTo;
        private System.Windows.Forms.Label lblSort;
        private System.Windows.Forms.ComboBox cboSort;
        private System.Windows.Forms.Label lblPageSize;
        private System.Windows.Forms.NumericUpDown numPageSize;
        private System.Windows.Forms.Panel panelResultsContainer;
        private System.Windows.Forms.FlowLayoutPanel paginationPanel;
        private System.Windows.Forms.Button btnPrevPage;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Label lblPageInfo;

        private int currentPage = 1;
        private int totalRecords;
        private const int DefaultPageSize = 15;
        private bool suppressSearch;

        private void BookSearchControl_Load(object sender, EventArgs e)
        {
            suppressSearch = true;
             LoadSearchTypes();
            LoadCategories();
            LoadSortOptions();
            numPageSize.Value = DefaultPageSize;
            currentPage = 1;
            suppressSearch = false;
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

        private void LoadSortOptions()
        {
            cboSort.Items.Clear();
            cboSort.Items.Add(new ComboItem { Value = "CREATE_DESC", Text = "最新上架" });
            cboSort.Items.Add(new ComboItem { Value = "PUBLISH_DESC", Text = "出版时间（新→旧）" });
            cboSort.Items.Add(new ComboItem { Value = "PUBLISH_ASC", Text = "出版时间（旧→新）" });
            cboSort.Items.Add(new ComboItem { Value = "BORROW_DESC", Text = "借阅热度（高→低）" });
            cboSort.Items.Add(new ComboItem { Value = "TITLE_ASC", Text = "书名（A-Z）" });
            cboSort.Items.Add(new ComboItem { Value = "TITLE_DESC", Text = "书名（Z-A）" });

            cboSort.SelectedIndex = 0;
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

        private void SearchBooks(bool resetPage = true)
        {
            if (resetPage)
            {
                currentPage = 1;
            }

            try
            {
                string searchType = (cboSearchType.SelectedItem as ComboItem)?.Value ?? "ALL";
                string categoryId = (cboCategory.SelectedItem as ComboItem)?.Value ?? string.Empty;
                bool availableOnly = chkAvailableOnly.Checked;
                string keyword = txtKeyword.Text.Trim();
                string author = txtAuthor.Text.Trim();
                string publisher = txtPublisher.Text.Trim();
                string isbn = txtIsbn.Text.Trim();
                int yearFrom = (int)numYearFrom.Value;
                int yearTo = (int)numYearTo.Value;
                int pageSize = (int)numPageSize.Value;

                if (yearFrom > 0 && yearTo > 0 && yearFrom > yearTo)
                {
                    MessageBox.Show("出版年份起始不能大于结束年份", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                StringBuilder filters = new StringBuilder("WHERE 1=1");
                List<SqlParameter> parameters = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(keyword))
                {
                    switch (searchType)
                    {
                        case "TITLE":
                            filters.Append(" AND b.bibliography_name LIKE @kw");
                            break;
                        case "ISBN":
                            filters.Append(" AND b.ISBN LIKE @kw");
                            break;
                        case "AUTHOR":
                            filters.Append(" AND EXISTS (SELECT 1 FROM BIBLIO_AUTHOR ba INNER JOIN AUTHOR a ON ba.author_id = a.author_id WHERE ba.bibliography_id = b.bibliography_id AND a.author_name LIKE @kw)");
                            break;
                        case "PUBLISHER":
                            filters.Append(" AND b.publish LIKE @kw");
                            break;
                        default:
                            filters.Append(" AND (b.bibliography_name LIKE @kw OR b.ISBN LIKE @kw OR b.publish LIKE @kw OR EXISTS (SELECT 1 FROM BIBLIO_AUTHOR ba INNER JOIN AUTHOR a ON ba.author_id = a.author_id WHERE ba.bibliography_id = b.bibliography_id AND a.author_name LIKE @kw))");
                            break;
                    }

                    parameters.Add(DatabaseHelper.CreateParameter("@kw", "%" + keyword + "%"));
                }

                if (!string.IsNullOrEmpty(categoryId))
                {
                    filters.Append(" AND b.category_id = @catId");
                    parameters.Add(DatabaseHelper.CreateParameter("@catId", Convert.ToInt32(categoryId)));
                }

                if (availableOnly)
                {
                    filters.Append(" AND EXISTS (SELECT 1 FROM BOOK_ITEM bi WHERE bi.bibliography_id = b.bibliography_id AND bi.current_status = N'AVAILABLE')");
                }

                if (!string.IsNullOrWhiteSpace(author))
                {
                    filters.Append(" AND EXISTS (SELECT 1 FROM BIBLIO_AUTHOR ba INNER JOIN AUTHOR a ON ba.author_id = a.author_id WHERE ba.bibliography_id = b.bibliography_id AND a.author_name LIKE @author)");
                    parameters.Add(DatabaseHelper.CreateParameter("@author", "%" + author + "%"));
                }

                if (!string.IsNullOrWhiteSpace(publisher))
                {
                    filters.Append(" AND b.publish LIKE @publisher");
                    parameters.Add(DatabaseHelper.CreateParameter("@publisher", "%" + publisher + "%"));
                }

                if (!string.IsNullOrWhiteSpace(isbn))
                {
                    filters.Append(" AND b.ISBN LIKE @isbn");
                    parameters.Add(DatabaseHelper.CreateParameter("@isbn", "%" + isbn + "%"));
                }

                if (yearFrom > 0)
                {
                    filters.Append(" AND b.publish_date IS NOT NULL AND YEAR(b.publish_date) >= @yearFrom");
                    parameters.Add(DatabaseHelper.CreateParameter("@yearFrom", yearFrom));
                }

                if (yearTo > 0)
                {
                    filters.Append(" AND b.publish_date IS NOT NULL AND YEAR(b.publish_date) <= @yearTo");
                    parameters.Add(DatabaseHelper.CreateParameter("@yearTo", yearTo));
                }

                string orderClause = GetOrderClause();

                string countSql = $"SELECT COUNT(*) FROM BIBLIOGRAPHY b INNER JOIN BOOK_CATEGORY bc ON b.category_id = bc.category_id {filters}";
                totalRecords = Convert.ToInt32(DatabaseHelper.ExecuteScalar(countSql, parameters.ToArray()));

                int totalPages = GetTotalPages(pageSize);
                if (totalPages > 0 && currentPage > totalPages)
                {
                    currentPage = totalPages;
                }

                List<SqlParameter> dataParameters = new List<SqlParameter>(parameters)
                {
                    DatabaseHelper.CreateParameter("@offset", Math.Max(0, (currentPage - 1) * pageSize)),
                    DatabaseHelper.CreateParameter("@pageSize", pageSize)
                };

                string sql = $@"
                    SELECT b.bibliography_id AS ID,
                           b.bibliography_name AS 书名,
                           ISNULL(authors.authors, N'未知') AS 作者,
                           b.ISBN,
                           b.publish AS 出版社,
                           bc.category_name AS 分类,
                           CASE WHEN b.publish_date IS NOT NULL THEN CONVERT(varchar(10), b.publish_date, 120) ELSE N'' END AS 出版日期,
                           b.price AS 定价,
                           inv.available_count AS 可借数量,
                           inv.total_count AS 馆藏总数,
                           ISNULL(bor.borrow_count, 0) AS 借阅次数
                    FROM BIBLIOGRAPHY b
                    INNER JOIN BOOK_CATEGORY bc ON b.category_id = bc.category_id
                    OUTER APPLY (
                        SELECT COUNT(*) AS total_count,
                               SUM(CASE WHEN bi.current_status = N'AVAILABLE' THEN 1 ELSE 0 END) AS available_count
                        FROM BOOK_ITEM bi
                        WHERE bi.bibliography_id = b.bibliography_id
                    ) inv
                    OUTER APPLY (
                        SELECT COUNT(*) AS borrow_count
                        FROM bookborrow bb
                        INNER JOIN BOOK_ITEM bi2 ON bb.bookID = bi2.item_barcode
                        WHERE bi2.bibliography_id = b.bibliography_id
                    ) bor
                    OUTER APPLY (
                        SELECT STUFF((
                            SELECT N'、' + a.author_name
                            FROM BIBLIO_AUTHOR ba2
                            INNER JOIN AUTHOR a ON ba2.author_id = a.author_id
                            WHERE ba2.bibliography_id = b.bibliography_id
                            ORDER BY ba2.author_order
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS authors
                    ) authors
                    {filters}
                    {orderClause}
                    OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, dataParameters.ToArray());
                dgvResults.DataSource = dt;

                if (totalRecords == 0)
                {
                    lblResultCount.Text = "未找到符合条件的图书";
                }
                else
                {
                    lblResultCount.Text = string.Format("找到 {0} 条结果，当前第 {1}/{2} 页", totalRecords, currentPage, Math.Max(1, totalPages));
                }

                UpdatePaginationControls(pageSize);
                ClearDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show("搜索失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetOrderClause()
        {
            string sortValue = (cboSort.SelectedItem as ComboItem)?.Value ?? "CREATE_DESC";

            switch (sortValue)
            {
                case "PUBLISH_DESC":
                    return "ORDER BY CASE WHEN b.publish_date IS NULL THEN 1 ELSE 0 END, b.publish_date DESC, b.bibliography_id DESC";
                case "PUBLISH_ASC":
                    return "ORDER BY CASE WHEN b.publish_date IS NULL THEN 1 ELSE 0 END, b.publish_date ASC, b.bibliography_id DESC";
                case "BORROW_DESC":
                    return "ORDER BY ISNULL(bor.borrow_count, 0) DESC, b.create_time DESC, b.bibliography_id DESC";
                case "TITLE_ASC":
                    return "ORDER BY b.bibliography_name ASC, b.bibliography_id DESC";
                case "TITLE_DESC":
                    return "ORDER BY b.bibliography_name DESC, b.bibliography_id DESC";
                default:
                    return "ORDER BY b.create_time DESC, b.bibliography_id DESC";
            }
        }

        private int GetTotalPages(int pageSize)
        {
            if (pageSize <= 0 || totalRecords <= 0)
            {
                return 0;
            }

            return (int)Math.Ceiling(totalRecords / (double)pageSize);
        }

        private void UpdatePaginationControls(int pageSize)
        {
            int totalPages = GetTotalPages(pageSize);
            if (totalPages == 0)
            {
                currentPage = 1;
                btnPrevPage.Enabled = false;
                btnNextPage.Enabled = false;
                lblPageInfo.Text = "第 0/0 页（共 0 条）";
                return;
            }

            btnPrevPage.Enabled = currentPage > 1;
            btnNextPage.Enabled = currentPage < totalPages;
            lblPageInfo.Text = string.Format("第 {0}/{1} 页（共 {2} 条）", currentPage, totalPages, totalRecords);
        }

        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            if (currentPage <= 1)
            {
                return;
            }

            currentPage--;
            SearchBooks(false);
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            int totalPages = GetTotalPages((int)numPageSize.Value);
            if (totalPages == 0 || currentPage >= totalPages)
            {
                return;
            }

            currentPage++;
            SearchBooks(false);
        }

        private void numPageSize_ValueChanged(object sender, EventArgs e)
        {
            if (suppressSearch)
            {
                return;
            }

             currentPage = 1;
             SearchBooks(false);
        }

         private void cboSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (suppressSearch)
            {
                return;
            }

             SearchBooks();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            suppressSearch = true;
             txtKeyword.Clear();
             cboSearchType.SelectedIndex = 0;
             cboCategory.SelectedIndex = 0;
             chkAvailableOnly.Checked = false;
             txtAuthor.Clear();
             txtPublisher.Clear();
             txtIsbn.Clear();
             numYearFrom.Value = 0;
             numYearTo.Value = 0;
             cboSort.SelectedIndex = 0;
             numPageSize.Value = DefaultPageSize;
             currentPage = 1;
             totalRecords = 0;
             dgvResults.DataSource = null;
             lblResultCount.Text = "";

             UpdatePaginationControls((int)numPageSize.Value);
             ClearDetails();
            suppressSearch = false;
        }

        private void dgvResults_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvResults.SelectedRows.Count == 0)
            {
                ClearDetails();
                return;
            }

            try
            {
                int bibliographyId = Convert.ToInt32(dgvResults.SelectedRows[0].Cells["ID"].Value);
                LoadBookDetails(bibliographyId);
            }
            catch
            {
                ClearDetails();
            }
        }

        private void LoadBookDetails(int bibliographyId)
        {
            try
            {
                string sql = @"
                    SELECT b.bibliography_id, b.bibliography_name, b.ISBN, b.publish, 
                           b.Description, bc.category_name,
                           CASE WHEN b.publish_date IS NOT NULL THEN CONVERT(varchar(10), b.publish_date, 120) ELSE N'' END AS publish_date,
                           ISNULL(authors.authors, N'未知') AS authors
                    FROM BIBLIOGRAPHY b
                    INNER JOIN BOOK_CATEGORY bc ON b.category_id = bc.category_id
                    OUTER APPLY (
                        SELECT STUFF((
                            SELECT N'、' + a.author_name
                            FROM BIBLIO_AUTHOR ba2
                            INNER JOIN AUTHOR a ON ba2.author_id = a.author_id
                            WHERE ba2.bibliography_id = b.bibliography_id
                            ORDER BY ba2.author_order
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS authors
                    ) authors
                    WHERE b.bibliography_id = @id";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.CreateParameter("@id", bibliographyId));

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    lblDetailsTitle.Text = "📖 图书详情";
                    lblBookName.Text = "书名：" + row["bibliography_name"].ToString();
                    lblISBN.Text = "ISBN：" + row["ISBN"].ToString();
                    lblAuthor.Text = "作者：" + row["authors"].ToString();
                    lblPublisher.Text = "出版社：" + row["publish"].ToString();
                    lblCategoryInfo.Text = "分类：" + row["category_name"].ToString();
                    txtDescription.Text = row["Description"]?.ToString() ?? "";
                    
                    lblDetailsTitle.Visible = true;
                    lblBookName.Visible = true;
                    lblISBN.Visible = true;
                    lblAuthor.Visible = true;
                    lblPublisher.Visible = true;
                    lblCategoryInfo.Visible = true;
                    lblDescription.Visible = true;
                    txtDescription.Visible = true;
                    lblItemsTitle.Visible = true;
                    dgvItems.Visible = true;

                    LoadBookItems(bibliographyId);

                    if (splitContainer.Panel2Collapsed)
                    {
                        splitContainer.Panel2Collapsed = false;
                    }
                }
            }
            catch
            {
                ClearDetails();
            }
        }

        private void LoadBookItems(int bibliographyId)
        {
            try
            {
                string sql = @"
                    SELECT bi.item_barcode AS 馆藏码,
                           sl.location_name AS 库位,
                           bi.current_status AS 状态,
                           bi.physical_condition AS 物理状态
                    FROM BOOK_ITEM bi
                    INNER JOIN STORAGE_LOCATION sl ON bi.location_id = sl.location_id
                    WHERE bi.bibliography_id = @id
                    ORDER BY bi.item_barcode";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.CreateParameter("@id", bibliographyId));
                dgvItems.DataSource = dt;

                if (dgvItems.Columns.Count > 0)
                {
                    foreach (DataGridViewColumn col in dgvItems.Columns)
                    {
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
            }
            catch
            {
                dgvItems.DataSource = null;
            }
        }

        private void ClearDetails()
        {
            lblDetailsTitle.Text = "📖 图书详情";
            lblBookName.Text = "";
            lblISBN.Text = "";
            lblAuthor.Text = "";
            lblPublisher.Text = "";
            lblCategoryInfo.Text = "";
            txtDescription.Text = "";
            dgvItems.DataSource = null;

            lblDetailsTitle.Visible = false;
            lblBookName.Visible = false;
            lblISBN.Visible = false;
            lblAuthor.Visible = false;
            lblPublisher.Visible = false;
            lblCategoryInfo.Visible = false;
            lblDescription.Visible = false;
            txtDescription.Visible = false;
            lblItemsTitle.Visible = false;
            dgvItems.Visible = false;

            if (!splitContainer.Panel2Collapsed)
            {
                splitContainer.Panel2Collapsed = true;
            }
        }

        private class ComboItem
        {
            public string Value { get; set; }
            public string Text { get; set; }
            public override string ToString() { return Text; }
        }
    }
}
