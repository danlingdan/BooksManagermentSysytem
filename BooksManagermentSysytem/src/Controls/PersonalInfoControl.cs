using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Models;
using BooksManagermentSysytem.Services;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 个人信息控件 - 读者查看自己的借书证和借阅信息
    /// </summary>
    public partial class PersonalInfoControl : UserControl
    {
        private Reader currentReader;

        public PersonalInfoControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.panelCardInfo = new System.Windows.Forms.Panel();
            this.cardInfoLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblCardID = new System.Windows.Forms.Label();
            this.lblCardIDValue = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblStartDateValue = new System.Windows.Forms.Label();
            this.lblReaderName = new System.Windows.Forms.Label();
            this.lblReaderNameValue = new System.Windows.Forms.Label();
            this.lblOverDate = new System.Windows.Forms.Label();
            this.lblOverDateValue = new System.Windows.Forms.Label();
            this.lblReaderType = new System.Windows.Forms.Label();
            this.lblReaderTypeValue = new System.Windows.Forms.Label();
            this.lblCardState = new System.Windows.Forms.Label();
            this.lblCardStateValue = new System.Windows.Forms.Label();
            this.lblUnit = new System.Windows.Forms.Label();
            this.lblUnitValue = new System.Windows.Forms.Label();
            this.lblNumber = new System.Windows.Forms.Label();
            this.lblNumberValue = new System.Windows.Forms.Label();
            this.lblCardInfoTitle = new System.Windows.Forms.Label();
            this.panelBorrowInfo = new System.Windows.Forms.Panel();
            this.borrowInfoLayout = new System.Windows.Forms.TableLayoutPanel();
            this.dgvCurrentBorrows = new System.Windows.Forms.DataGridView();
            this.lblBorrowSummary = new System.Windows.Forms.Label();
            this.lblBorrowInfoTitle = new System.Windows.Forms.Label();
            this.panelHistory = new System.Windows.Forms.Panel();
            this.historyLayout = new System.Windows.Forms.TableLayoutPanel();
            this.dgvBorrowHistory = new System.Windows.Forms.DataGridView();
            this.lblHistoryTitle = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.mainLayout.SuspendLayout();
            this.panelCardInfo.SuspendLayout();
            this.cardInfoLayout.SuspendLayout();
            this.panelBorrowInfo.SuspendLayout();
            this.borrowInfoLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentBorrows)).BeginInit();
            this.panelHistory.SuspendLayout();
            this.historyLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowHistory)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Controls.Add(this.panelCardInfo, 0, 0);
            this.mainLayout.Controls.Add(this.panelBorrowInfo, 0, 1);
            this.mainLayout.Controls.Add(this.panelHistory, 0, 2);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 75);
            this.mainLayout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.Padding = new System.Windows.Forms.Padding(30, 30, 30, 30);
            this.mainLayout.RowCount = 3;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainLayout.Size = new System.Drawing.Size(1350, 975);
            this.mainLayout.TabIndex = 0;
            // 
            // panelCardInfo
            // 
            this.panelCardInfo.AutoSize = true;
            this.panelCardInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelCardInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelCardInfo.Controls.Add(this.cardInfoLayout);
            this.panelCardInfo.Controls.Add(this.lblCardInfoTitle);
            this.panelCardInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCardInfo.Location = new System.Drawing.Point(34, 34);
            this.panelCardInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelCardInfo.Name = "panelCardInfo";
            this.panelCardInfo.Padding = new System.Windows.Forms.Padding(22, 22, 22, 22);
            this.panelCardInfo.Size = new System.Drawing.Size(1282, 334);
            this.panelCardInfo.TabIndex = 0;
            // 
            // cardInfoLayout
            // 
            this.cardInfoLayout.AutoSize = true;
            this.cardInfoLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cardInfoLayout.ColumnCount = 4;
            this.cardInfoLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.cardInfoLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.cardInfoLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.cardInfoLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.cardInfoLayout.Controls.Add(this.lblCardID, 0, 0);
            this.cardInfoLayout.Controls.Add(this.lblCardIDValue, 1, 0);
            this.cardInfoLayout.Controls.Add(this.lblStartDate, 2, 0);
            this.cardInfoLayout.Controls.Add(this.lblStartDateValue, 3, 0);
            this.cardInfoLayout.Controls.Add(this.lblReaderName, 0, 1);
            this.cardInfoLayout.Controls.Add(this.lblReaderNameValue, 1, 1);
            this.cardInfoLayout.Controls.Add(this.lblOverDate, 2, 1);
            this.cardInfoLayout.Controls.Add(this.lblOverDateValue, 3, 1);
            this.cardInfoLayout.Controls.Add(this.lblReaderType, 0, 2);
            this.cardInfoLayout.Controls.Add(this.lblReaderTypeValue, 1, 2);
            this.cardInfoLayout.Controls.Add(this.lblCardState, 2, 2);
            this.cardInfoLayout.Controls.Add(this.lblCardStateValue, 3, 2);
            this.cardInfoLayout.Controls.Add(this.lblUnit, 0, 3);
            this.cardInfoLayout.Controls.Add(this.lblUnitValue, 1, 3);
            this.cardInfoLayout.Controls.Add(this.lblNumber, 0, 4);
            this.cardInfoLayout.Controls.Add(this.lblNumberValue, 1, 4);
            this.cardInfoLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardInfoLayout.Location = new System.Drawing.Point(22, 67);
            this.cardInfoLayout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cardInfoLayout.Name = "cardInfoLayout";
            this.cardInfoLayout.RowCount = 5;
            this.cardInfoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.cardInfoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.cardInfoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.cardInfoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.cardInfoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.cardInfoLayout.Size = new System.Drawing.Size(1238, 245);
            this.cardInfoLayout.TabIndex = 0;
            // 
            // lblCardID
            // 
            this.lblCardID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCardID.AutoSize = true;
            this.lblCardID.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCardID.Location = new System.Drawing.Point(4, 12);
            this.lblCardID.Margin = new System.Windows.Forms.Padding(4, 12, 4, 12);
            this.lblCardID.Name = "lblCardID";
            this.lblCardID.Size = new System.Drawing.Size(102, 25);
            this.lblCardID.TabIndex = 0;
            this.lblCardID.Text = "借书证号：";
            // 
            // lblCardIDValue
            // 
            this.lblCardIDValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCardIDValue.AutoSize = true;
            this.lblCardIDValue.Location = new System.Drawing.Point(123, 12);
            this.lblCardIDValue.Margin = new System.Windows.Forms.Padding(4, 12, 4, 12);
            this.lblCardIDValue.Name = "lblCardIDValue";
            this.lblCardIDValue.Size = new System.Drawing.Size(18, 24);
            this.lblCardIDValue.TabIndex = 1;
            this.lblCardIDValue.Text = "-";
            // 
            // lblStartDate
            // 
            this.lblStartDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStartDate.Location = new System.Drawing.Point(640, 12);
            this.lblStartDate.Margin = new System.Windows.Forms.Padding(30, 12, 4, 12);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(102, 25);
            this.lblStartDate.TabIndex = 2;
            this.lblStartDate.Text = "开始日期：";
            // 
            // lblStartDateValue
            // 
            this.lblStartDateValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStartDateValue.AutoSize = true;
            this.lblStartDateValue.Location = new System.Drawing.Point(750, 12);
            this.lblStartDateValue.Margin = new System.Windows.Forms.Padding(4, 12, 4, 12);
            this.lblStartDateValue.Name = "lblStartDateValue";
            this.lblStartDateValue.Size = new System.Drawing.Size(18, 24);
            this.lblStartDateValue.TabIndex = 3;
            this.lblStartDateValue.Text = "-";
            // 
            // lblReaderName
            // 
            this.lblReaderName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblReaderName.AutoSize = true;
            this.lblReaderName.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblReaderName.Location = new System.Drawing.Point(4, 61);
            this.lblReaderName.Margin = new System.Windows.Forms.Padding(4, 12, 4, 12);
            this.lblReaderName.Name = "lblReaderName";
            this.lblReaderName.Size = new System.Drawing.Size(102, 25);
            this.lblReaderName.TabIndex = 4;
            this.lblReaderName.Text = "读者姓名：";
            // 
            // lblReaderNameValue
            // 
            this.lblReaderNameValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblReaderNameValue.AutoSize = true;
            this.lblReaderNameValue.Location = new System.Drawing.Point(123, 61);
            this.lblReaderNameValue.Margin = new System.Windows.Forms.Padding(4, 12, 4, 12);
            this.lblReaderNameValue.Name = "lblReaderNameValue";
            this.lblReaderNameValue.Size = new System.Drawing.Size(18, 24);
            this.lblReaderNameValue.TabIndex = 5;
            this.lblReaderNameValue.Text = "-";
            // 
            // lblOverDate
            // 
            this.lblOverDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblOverDate.AutoSize = true;
            this.lblOverDate.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblOverDate.Location = new System.Drawing.Point(640, 61);
            this.lblOverDate.Margin = new System.Windows.Forms.Padding(30, 12, 4, 12);
            this.lblOverDate.Name = "lblOverDate";
            this.lblOverDate.Size = new System.Drawing.Size(102, 25);
            this.lblOverDate.TabIndex = 6;
            this.lblOverDate.Text = "到期日期：";
            // 
            // lblOverDateValue
            // 
            this.lblOverDateValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblOverDateValue.AutoSize = true;
            this.lblOverDateValue.Location = new System.Drawing.Point(750, 61);
            this.lblOverDateValue.Margin = new System.Windows.Forms.Padding(4, 12, 4, 12);
            this.lblOverDateValue.Name = "lblOverDateValue";
            this.lblOverDateValue.Size = new System.Drawing.Size(18, 24);
            this.lblOverDateValue.TabIndex = 7;
            this.lblOverDateValue.Text = "-";
            // 
            // lblReaderType
            // 
            this.lblReaderType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblReaderType.AutoSize = true;
            this.lblReaderType.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblReaderType.Location = new System.Drawing.Point(4, 110);
            this.lblReaderType.Margin = new System.Windows.Forms.Padding(4, 12, 4, 12);
            this.lblReaderType.Name = "lblReaderType";
            this.lblReaderType.Size = new System.Drawing.Size(102, 25);
            this.lblReaderType.TabIndex = 8;
            this.lblReaderType.Text = "读者类型：";
            // 
            // lblReaderTypeValue
            // 
            this.lblReaderTypeValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblReaderTypeValue.AutoSize = true;
            this.lblReaderTypeValue.Location = new System.Drawing.Point(123, 110);
            this.lblReaderTypeValue.Margin = new System.Windows.Forms.Padding(4, 12, 4, 12);
            this.lblReaderTypeValue.Name = "lblReaderTypeValue";
            this.lblReaderTypeValue.Size = new System.Drawing.Size(18, 24);
            this.lblReaderTypeValue.TabIndex = 9;
            this.lblReaderTypeValue.Text = "-";
            // 
            // lblCardState
            // 
            this.lblCardState.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCardState.AutoSize = true;
            this.lblCardState.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCardState.Location = new System.Drawing.Point(640, 110);
            this.lblCardState.Margin = new System.Windows.Forms.Padding(30, 12, 4, 12);
            this.lblCardState.Name = "lblCardState";
            this.lblCardState.Size = new System.Drawing.Size(102, 25);
            this.lblCardState.TabIndex = 10;
            this.lblCardState.Text = "证件状态：";
            // 
            // lblCardStateValue
            // 
            this.lblCardStateValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCardStateValue.AutoSize = true;
            this.lblCardStateValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCardStateValue.Location = new System.Drawing.Point(750, 110);
            this.lblCardStateValue.Margin = new System.Windows.Forms.Padding(4, 12, 4, 12);
            this.lblCardStateValue.Name = "lblCardStateValue";
            this.lblCardStateValue.Size = new System.Drawing.Size(20, 25);
            this.lblCardStateValue.TabIndex = 11;
            this.lblCardStateValue.Text = "-";
            // 
            // lblUnit
            // 
            this.lblUnit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUnit.AutoSize = true;
            this.lblUnit.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUnit.Location = new System.Drawing.Point(4, 159);
            this.lblUnit.Margin = new System.Windows.Forms.Padding(4, 12, 4, 12);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(111, 25);
            this.lblUnit.TabIndex = 12;
            this.lblUnit.Text = "单位/学院：";
            // 
            // lblUnitValue
            // 
            this.lblUnitValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUnitValue.AutoSize = true;
            this.lblUnitValue.Location = new System.Drawing.Point(123, 159);
            this.lblUnitValue.Margin = new System.Windows.Forms.Padding(4, 12, 4, 12);
            this.lblUnitValue.Name = "lblUnitValue";
            this.lblUnitValue.Size = new System.Drawing.Size(18, 24);
            this.lblUnitValue.TabIndex = 13;
            this.lblUnitValue.Text = "-";
            // 
            // lblNumber
            // 
            this.lblNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNumber.AutoSize = true;
            this.lblNumber.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNumber.Location = new System.Drawing.Point(4, 208);
            this.lblNumber.Margin = new System.Windows.Forms.Padding(4, 12, 4, 12);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(111, 25);
            this.lblNumber.TabIndex = 14;
            this.lblNumber.Text = "学号/工号：";
            // 
            // lblNumberValue
            // 
            this.lblNumberValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNumberValue.AutoSize = true;
            this.lblNumberValue.Location = new System.Drawing.Point(123, 208);
            this.lblNumberValue.Margin = new System.Windows.Forms.Padding(4, 12, 4, 12);
            this.lblNumberValue.Name = "lblNumberValue";
            this.lblNumberValue.Size = new System.Drawing.Size(18, 24);
            this.lblNumberValue.TabIndex = 15;
            this.lblNumberValue.Text = "-";
            // 
            // lblCardInfoTitle
            // 
            this.lblCardInfoTitle.AutoSize = true;
            this.lblCardInfoTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCardInfoTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCardInfoTitle.Location = new System.Drawing.Point(22, 22);
            this.lblCardInfoTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCardInfoTitle.Name = "lblCardInfoTitle";
            this.lblCardInfoTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.lblCardInfoTitle.Size = new System.Drawing.Size(123, 45);
            this.lblCardInfoTitle.TabIndex = 1;
            this.lblCardInfoTitle.Text = "借书证信息";
            // 
            // panelBorrowInfo
            // 
            this.panelBorrowInfo.AutoSize = true;
            this.panelBorrowInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelBorrowInfo.Controls.Add(this.borrowInfoLayout);
            this.panelBorrowInfo.Controls.Add(this.lblBorrowInfoTitle);
            this.panelBorrowInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBorrowInfo.Location = new System.Drawing.Point(30, 394);
            this.panelBorrowInfo.Margin = new System.Windows.Forms.Padding(0, 22, 0, 0);
            this.panelBorrowInfo.Name = "panelBorrowInfo";
            this.panelBorrowInfo.Padding = new System.Windows.Forms.Padding(22, 22, 22, 22);
            this.panelBorrowInfo.Size = new System.Drawing.Size(1290, 350);
            this.panelBorrowInfo.TabIndex = 1;
            // 
            // borrowInfoLayout
            // 
            this.borrowInfoLayout.AutoSize = true;
            this.borrowInfoLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.borrowInfoLayout.ColumnCount = 1;
            this.borrowInfoLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.borrowInfoLayout.Controls.Add(this.dgvCurrentBorrows, 0, 0);
            this.borrowInfoLayout.Controls.Add(this.lblBorrowSummary, 0, 1);
            this.borrowInfoLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.borrowInfoLayout.Location = new System.Drawing.Point(22, 67);
            this.borrowInfoLayout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.borrowInfoLayout.Name = "borrowInfoLayout";
            this.borrowInfoLayout.RowCount = 2;
            this.borrowInfoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 225F));
            this.borrowInfoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.borrowInfoLayout.Size = new System.Drawing.Size(1246, 261);
            this.borrowInfoLayout.TabIndex = 0;
            // 
            // dgvCurrentBorrows
            // 
            this.dgvCurrentBorrows.AllowUserToAddRows = false;
            this.dgvCurrentBorrows.AllowUserToDeleteRows = false;
            this.dgvCurrentBorrows.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCurrentBorrows.BackgroundColor = System.Drawing.Color.White;
            this.dgvCurrentBorrows.ColumnHeadersHeight = 40;
            this.dgvCurrentBorrows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCurrentBorrows.Location = new System.Drawing.Point(4, 4);
            this.dgvCurrentBorrows.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvCurrentBorrows.Name = "dgvCurrentBorrows";
            this.dgvCurrentBorrows.ReadOnly = true;
            this.dgvCurrentBorrows.RowHeadersVisible = false;
            this.dgvCurrentBorrows.RowHeadersWidth = 62;
            this.dgvCurrentBorrows.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCurrentBorrows.Size = new System.Drawing.Size(1238, 217);
            this.dgvCurrentBorrows.TabIndex = 0;
            // 
            // lblBorrowSummary
            // 
            this.lblBorrowSummary.AutoSize = true;
            this.lblBorrowSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBorrowSummary.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.lblBorrowSummary.ForeColor = System.Drawing.Color.Gray;
            this.lblBorrowSummary.Location = new System.Drawing.Point(4, 233);
            this.lblBorrowSummary.Margin = new System.Windows.Forms.Padding(4, 8, 4, 4);
            this.lblBorrowSummary.Name = "lblBorrowSummary";
            this.lblBorrowSummary.Size = new System.Drawing.Size(1238, 24);
            this.lblBorrowSummary.TabIndex = 1;
            this.lblBorrowSummary.Text = "提示：每次最多借阅3本书，借期7天。";
            // 
            // lblBorrowInfoTitle
            // 
            this.lblBorrowInfoTitle.AutoSize = true;
            this.lblBorrowInfoTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBorrowInfoTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblBorrowInfoTitle.Location = new System.Drawing.Point(22, 22);
            this.lblBorrowInfoTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBorrowInfoTitle.Name = "lblBorrowInfoTitle";
            this.lblBorrowInfoTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.lblBorrowInfoTitle.Size = new System.Drawing.Size(101, 45);
            this.lblBorrowInfoTitle.TabIndex = 1;
            this.lblBorrowInfoTitle.Text = "当前借阅";
            // 
            // panelHistory
            // 
            this.panelHistory.AutoSize = true;
            this.panelHistory.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelHistory.Controls.Add(this.historyLayout);
            this.panelHistory.Controls.Add(this.lblHistoryTitle);
            this.panelHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHistory.Location = new System.Drawing.Point(30, 766);
            this.panelHistory.Margin = new System.Windows.Forms.Padding(0, 22, 0, 0);
            this.panelHistory.Name = "panelHistory";
            this.panelHistory.Padding = new System.Windows.Forms.Padding(22, 22, 22, 22);
            this.panelHistory.Size = new System.Drawing.Size(1290, 314);
            this.panelHistory.TabIndex = 2;
            // 
            // historyLayout
            // 
            this.historyLayout.AutoSize = true;
            this.historyLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.historyLayout.ColumnCount = 1;
            this.historyLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.historyLayout.Controls.Add(this.dgvBorrowHistory, 0, 0);
            this.historyLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.historyLayout.Location = new System.Drawing.Point(22, 67);
            this.historyLayout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.historyLayout.Name = "historyLayout";
            this.historyLayout.RowCount = 1;
            this.historyLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 225F));
            this.historyLayout.Size = new System.Drawing.Size(1246, 225);
            this.historyLayout.TabIndex = 0;
            // 
            // dgvBorrowHistory
            // 
            this.dgvBorrowHistory.AllowUserToAddRows = false;
            this.dgvBorrowHistory.AllowUserToDeleteRows = false;
            this.dgvBorrowHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBorrowHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvBorrowHistory.ColumnHeadersHeight = 40;
            this.dgvBorrowHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBorrowHistory.Location = new System.Drawing.Point(4, 4);
            this.dgvBorrowHistory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvBorrowHistory.Name = "dgvBorrowHistory";
            this.dgvBorrowHistory.ReadOnly = true;
            this.dgvBorrowHistory.RowHeadersVisible = false;
            this.dgvBorrowHistory.RowHeadersWidth = 62;
            this.dgvBorrowHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBorrowHistory.Size = new System.Drawing.Size(1238, 217);
            this.dgvBorrowHistory.TabIndex = 0;
            // 
            // lblHistoryTitle
            // 
            this.lblHistoryTitle.AutoSize = true;
            this.lblHistoryTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHistoryTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblHistoryTitle.Location = new System.Drawing.Point(22, 22);
            this.lblHistoryTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHistoryTitle.Name = "lblHistoryTitle";
            this.lblHistoryTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.lblHistoryTitle.Size = new System.Drawing.Size(239, 45);
            this.lblHistoryTitle.TabIndex = 1;
            this.lblHistoryTitle.Text = "借阅历史（最近10条）";
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.panelHeader.Controls.Add(this.btnRefresh);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.panelHeader.Size = new System.Drawing.Size(1350, 75);
            this.panelHeader.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(1200, 15);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 45);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(30, 15);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(176, 37);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "👤 个人信息";
            // 
            // PersonalInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.mainLayout);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1200, 900);
            this.Name = "PersonalInfoControl";
            this.Size = new System.Drawing.Size(1350, 1050);
            this.Load += new System.EventHandler(this.PersonalInfoControl_Load);
            this.mainLayout.ResumeLayout(false);
            this.mainLayout.PerformLayout();
            this.panelCardInfo.ResumeLayout(false);
            this.panelCardInfo.PerformLayout();
            this.cardInfoLayout.ResumeLayout(false);
            this.cardInfoLayout.PerformLayout();
            this.panelBorrowInfo.ResumeLayout(false);
            this.panelBorrowInfo.PerformLayout();
            this.borrowInfoLayout.ResumeLayout(false);
            this.borrowInfoLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentBorrows)).EndInit();
            this.panelHistory.ResumeLayout(false);
            this.panelHistory.PerformLayout();
            this.historyLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowHistory)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panelCardInfo;
        private System.Windows.Forms.Label lblCardInfoTitle;
        private System.Windows.Forms.Label lblCardID;
        private System.Windows.Forms.Label lblCardIDValue;
        private System.Windows.Forms.Label lblReaderName;
        private System.Windows.Forms.Label lblReaderNameValue;
        private System.Windows.Forms.Label lblReaderType;
        private System.Windows.Forms.Label lblReaderTypeValue;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Label lblUnitValue;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.Label lblNumberValue;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblStartDateValue;
        private System.Windows.Forms.Label lblOverDate;
        private System.Windows.Forms.Label lblOverDateValue;
        private System.Windows.Forms.Label lblCardState;
        private System.Windows.Forms.Label lblCardStateValue;
        private System.Windows.Forms.Panel panelBorrowInfo;
        private System.Windows.Forms.Label lblBorrowInfoTitle;
        private System.Windows.Forms.DataGridView dgvCurrentBorrows;
        private System.Windows.Forms.Label lblBorrowSummary;
        private System.Windows.Forms.Panel panelHistory;
        private System.Windows.Forms.Label lblHistoryTitle;
        private TableLayoutPanel mainLayout;
        private TableLayoutPanel cardInfoLayout;
        private TableLayoutPanel borrowInfoLayout;
        private TableLayoutPanel historyLayout;
        private System.Windows.Forms.DataGridView dgvBorrowHistory;

        private void PersonalInfoControl_Load(object sender, EventArgs e)
        {
            LoadPersonalInfo();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPersonalInfo();
        }

        private void LoadPersonalInfo()
        {
            var user = AuthenticationService.Instance.CurrentUser;
            if (user == null || string.IsNullOrEmpty(user.CardID))
            {
                MessageBox.Show("无法获取当前用户的借书证信息", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                LoadCardInfo(user.CardID);
                LoadCurrentBorrows(user.CardID);
                LoadBorrowHistory(user.CardID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载个人信息失败：" + ex.Message, "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCardInfo(string cardID)
        {
            string sql = @"
                SELECT r.cardID, r.readername, r.readertype, r.unit, r.number,
                       rc.startdate, rc.overdate, rc.state
                FROM reader r
                INNER JOIN readcard rc ON r.cardID = rc.cardID
                WHERE r.cardID = @cardID";

            DataTable dt = DatabaseHelper.ExecuteQuery(sql, 
                DatabaseHelper.CreateParameter("@cardID", cardID));

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("未找到借书证信息", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataRow row = dt.Rows[0];
            currentReader = new Reader
            {
                CardID = row["cardID"].ToString(),
                ReaderName = row["readername"].ToString(),
                ReaderType = row["readertype"].ToString(),
                Unit = row["unit"]?.ToString(),
                Number = row["number"]?.ToString(),
                StartDate = Convert.ToDateTime(row["startdate"]),
                OverDate = Convert.ToDateTime(row["overdate"]),
                CardState = row["state"].ToString()
            };

            lblCardIDValue.Text = currentReader.CardID;
            lblReaderNameValue.Text = currentReader.ReaderName;
            lblReaderTypeValue.Text = currentReader.ReaderType;
            lblUnitValue.Text = currentReader.Unit ?? "-";
            lblNumberValue.Text = currentReader.Number ?? "-";
            lblStartDateValue.Text = currentReader.StartDate.ToString("yyyy-MM-dd");
            lblOverDateValue.Text = currentReader.OverDate.ToString("yyyy-MM-dd");
            lblCardStateValue.Text = currentReader.CardState;

            if (currentReader.IsCardValid())
            {
                lblCardStateValue.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblCardStateValue.ForeColor = System.Drawing.Color.Red;
            }

            if (currentReader.OverDate < DateTime.Today.AddDays(30))
            {
                int daysLeft = (currentReader.OverDate - DateTime.Today).Days;
                if (daysLeft > 0)
                {
                    MessageBox.Show($"您的借书证将在 {daysLeft} 天后到期，请及时续期。", 
                        "到期提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (daysLeft <= 0)
                {
                    MessageBox.Show("您的借书证已过期，请及时续期。", 
                        "到期提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void LoadCurrentBorrows(string cardID)
        {
            string sql = @"
                SELECT bb.bookID AS 馆藏码, 
                       bib.bibliography_name AS 书名,
                       bb.borrowdate AS 借阅日期,
                       DATEADD(DAY, 7, bb.borrowdate) AS 应还日期,
                       DATEDIFF(DAY, GETDATE(), DATEADD(DAY, 7, bb.borrowdate)) AS 剩余天数,
                       CASE 
                           WHEN GETDATE() > DATEADD(DAY, 7, bb.borrowdate) THEN N'逾期'
                           WHEN DATEDIFF(DAY, GETDATE(), DATEADD(DAY, 7, bb.borrowdate)) <= 2 THEN N'即将到期'
                           ELSE N'正常'
                       END AS 状态
                FROM bookborrow bb
                INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                WHERE bb.cardID = @cardID AND bb.overdate IS NULL
                ORDER BY bb.borrowdate DESC";

            DataTable dt = DatabaseHelper.ExecuteQuery(sql, 
                DatabaseHelper.CreateParameter("@cardID", cardID));

            dgvCurrentBorrows.DataSource = dt;

            int currentCount = dt.Rows.Count;
            int maxBooks = BorrowRules.MaxBooksPerBorrow;
            lblBorrowSummary.Text = $"当前已借阅：{currentCount} 本 / 最多可借：{maxBooks} 本 | 借期：{BorrowRules.BorrowDays} 天";

            dgvCurrentBorrows.CellFormatting += (s, cellArgs) =>
            {
                if (dgvCurrentBorrows.Columns[cellArgs.ColumnIndex].HeaderText == "状态" && cellArgs.Value != null)
                {
                    string status = cellArgs.Value.ToString();
                    if (status == "逾期")
                    {
                        cellArgs.CellStyle.BackColor = System.Drawing.Color.FromArgb(255, 200, 200);
                        cellArgs.CellStyle.Font = new System.Drawing.Font(dgvCurrentBorrows.Font, System.Drawing.FontStyle.Bold);
                        cellArgs.CellStyle.ForeColor = System.Drawing.Color.Red;
                    }
                    else if (status == "即将到期")
                    {
                        cellArgs.CellStyle.BackColor = System.Drawing.Color.FromArgb(255, 255, 200);
                        cellArgs.CellStyle.ForeColor = System.Drawing.Color.Orange;
                    }
                }
            };

            foreach (DataRow row in dt.Rows)
            {
                if (row["状态"].ToString() == "逾期")
                {
                    string bookName = row["书名"].ToString();
                    DateTime dueDate = Convert.ToDateTime(row["应还日期"]);
                    int overdueDays = (DateTime.Now - dueDate).Days;
                    
                    MessageBox.Show($"您借阅的《{bookName}》已逾期 {overdueDays} 天，请尽快归还！\n逾期可能产生罚款。", 
                        "逾期提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
            }
        }

        private void LoadBorrowHistory(string cardID)
        {
            string sql = @"
                SELECT TOP 10
                       bb.bookID AS 馆藏码,
                       bib.bibliography_name AS 书名,
                       bb.borrowdate AS 借阅日期,
                       bb.overdate AS 归还日期,
                       CASE 
                           WHEN bb.overdate IS NULL THEN N'未归还'
                           WHEN bb.overdate > DATEADD(DAY, 7, bb.borrowdate) THEN N'逾期归还'
                           ELSE N'正常归还'
                       END AS 状态
                FROM bookborrow bb
                INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                INNER JOIN BIBLIOGRAPHY bib ON bi.bibliography_id = bib.bibliography_id
                WHERE bb.cardID = @cardID
                ORDER BY bb.borrowdate DESC";

            DataTable dt = DatabaseHelper.ExecuteQuery(sql, 
                DatabaseHelper.CreateParameter("@cardID", cardID));

            dgvBorrowHistory.DataSource = dt;
        }
    }
}
