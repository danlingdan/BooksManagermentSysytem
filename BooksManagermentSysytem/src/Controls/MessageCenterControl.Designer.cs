using System.Windows.Forms;

namespace BooksManagermentSysytem.Controls
{
    partial class MessageCenterControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.chkUnreadOnly = new System.Windows.Forms.CheckBox();
            this.btnMarkRead = new System.Windows.Forms.Button();
            this.btnMarkAllRead = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgvMessages = new System.Windows.Forms.DataGridView();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblMessageCount = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMessages)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            
            // panelHeader
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.panelHeader.Controls.Add(this.btnRefresh);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.panelHeader.Size = new System.Drawing.Size(1350, 75);
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(177, 37);
            this.lblTitle.Text = "📬 消息中心";
            
            // btnRefresh
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(1210, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 45);
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            
            // panelToolbar
            this.panelToolbar.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.panelToolbar.Controls.Add(this.chkUnreadOnly);
            this.panelToolbar.Controls.Add(this.btnMarkRead);
            this.panelToolbar.Controls.Add(this.btnMarkAllRead);
            this.panelToolbar.Controls.Add(this.btnDelete);
            this.panelToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelToolbar.Location = new System.Drawing.Point(0, 75);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Padding = new System.Windows.Forms.Padding(20);
            this.panelToolbar.Size = new System.Drawing.Size(1350, 70);
            
            // chkUnreadOnly
            this.chkUnreadOnly.AutoSize = true;
            this.chkUnreadOnly.Location = new System.Drawing.Point(30, 25);
            this.chkUnreadOnly.Name = "chkUnreadOnly";
            this.chkUnreadOnly.Size = new System.Drawing.Size(138, 28);
            this.chkUnreadOnly.Text = "仅显示未读";
            this.chkUnreadOnly.CheckedChanged += new System.EventHandler(this.chkUnreadOnly_CheckedChanged);
            
            // btnMarkRead
            this.btnMarkRead.Location = new System.Drawing.Point(200, 20);
            this.btnMarkRead.Name = "btnMarkRead";
            this.btnMarkRead.Size = new System.Drawing.Size(150, 35);
            this.btnMarkRead.Text = "标记为已读";
            this.btnMarkRead.Click += new System.EventHandler(this.btnMarkRead_Click);
            
            // btnMarkAllRead
            this.btnMarkAllRead.Location = new System.Drawing.Point(365, 20);
            this.btnMarkAllRead.Name = "btnMarkAllRead";
            this.btnMarkAllRead.Size = new System.Drawing.Size(150, 35);
            this.btnMarkAllRead.Text = "全部已读";
            this.btnMarkAllRead.Click += new System.EventHandler(this.btnMarkAllRead_Click);
            
            // btnDelete
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(244, 67, 54);
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(530, 20);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(150, 35);
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            
            // dgvMessages
            this.dgvMessages.AllowUserToAddRows = false;
            this.dgvMessages.AllowUserToDeleteRows = false;
            this.dgvMessages.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMessages.BackgroundColor = System.Drawing.Color.White;
            this.dgvMessages.ColumnHeadersHeight = 40;
            this.dgvMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMessages.Location = new System.Drawing.Point(0, 145);
            this.dgvMessages.Name = "dgvMessages";
            this.dgvMessages.ReadOnly = true;
            this.dgvMessages.RowHeadersVisible = false;
            this.dgvMessages.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMessages.Size = new System.Drawing.Size(1350, 655);
            
            // panelFooter
            this.panelFooter.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.panelFooter.Controls.Add(this.lblMessageCount);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 800);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1350, 50);
            
            // lblMessageCount
            this.lblMessageCount.AutoSize = true;
            this.lblMessageCount.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.lblMessageCount.Location = new System.Drawing.Point(30, 15);
            this.lblMessageCount.Name = "lblMessageCount";
            this.lblMessageCount.Size = new System.Drawing.Size(150, 24);
            this.lblMessageCount.Text = "共 0 条消息";
            
            // MessageCenterControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvMessages);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelToolbar);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1200, 750);
            this.Name = "MessageCenterControl";
            this.Size = new System.Drawing.Size(1350, 850);
            this.Load += new System.EventHandler(this.MessageCenterControl_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelToolbar.ResumeLayout(false);
            this.panelToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMessages)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.panelFooter.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.CheckBox chkUnreadOnly;
        private System.Windows.Forms.Button btnMarkRead;
        private System.Windows.Forms.Button btnMarkAllRead;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dgvMessages;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Label lblMessageCount;
    }
}
