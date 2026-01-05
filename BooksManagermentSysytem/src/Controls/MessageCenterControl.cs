using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BooksManagermentSysytem.Models;
using BooksManagermentSysytem.Services;

namespace BooksManagermentSysytem.Controls
{
    /// <summary>
    /// 消息中心控件 - 读者查看和管理系统消息
    /// </summary>
    public partial class MessageCenterControl : UserControl
    {
        private string currentCardID;
        private DataTable currentMessages;
        private bool showUnreadOnly = false;

        public MessageCenterControl()
        {
            InitializeComponent();
        }

        private void MessageCenterControl_Load(object sender, EventArgs e)
        {
            var user = AuthenticationService.Instance.CurrentUser;
            if (user != null && !string.IsNullOrEmpty(user.CardID))
            {
                currentCardID = user.CardID;
                LoadMessages();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadMessages();
        }

        private void btnMarkRead_Click(object sender, EventArgs e)
        {
            if (dgvMessages.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要标记为已读的消息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                foreach (DataGridViewRow row in dgvMessages.SelectedRows)
                {
                    long messageId = Convert.ToInt64(row.Cells["消息ID"].Value);
                    NotificationService.Instance.MarkAsRead(messageId);
                }

                MessageBox.Show("已标记为已读", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMessages();
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMarkAllRead_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentCardID))
                return;

            if (MessageBox.Show("确定要将所有消息标记为已读吗？", "确认", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                int count = NotificationService.Instance.MarkAllAsRead(currentCardID);
                MessageBox.Show($"已标记 {count} 条消息为已读", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMessages();
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMessages.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要删除的消息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"确定要删除选中的 {dgvMessages.SelectedRows.Count} 条消息吗？", "确认", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                foreach (DataGridViewRow row in dgvMessages.SelectedRows)
                {
                    long messageId = Convert.ToInt64(row.Cells["消息ID"].Value);
                    NotificationService.Instance.DeleteMessage(messageId);
                }

                MessageBox.Show("删除成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMessages();
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkUnreadOnly_CheckedChanged(object sender, EventArgs e)
        {
            showUnreadOnly = chkUnreadOnly.Checked;
            LoadMessages();
        }

        private void LoadMessages()
        {
            if (string.IsNullOrEmpty(currentCardID))
            {
                lblMessageCount.Text = "请先登录";
                return;
            }

            try
            {
                currentMessages = NotificationService.Instance.GetMessages(currentCardID, !showUnreadOnly);

                // 格式化显示
                var displayData = new DataTable();
                displayData.Columns.Add("消息ID", typeof(long));
                displayData.Columns.Add("类型", typeof(string));
                displayData.Columns.Add("标题", typeof(string));
                displayData.Columns.Add("内容", typeof(string));
                displayData.Columns.Add("优先级", typeof(string));
                displayData.Columns.Add("状态", typeof(string));
                displayData.Columns.Add("时间", typeof(string));

                foreach (DataRow row in currentMessages.Rows)
                {
                    string messageType = GetMessageTypeText(row["message_type"].ToString());
                    string priority = GetPriorityText(row["priority"].ToString());
                    string status = row["status"].ToString() == "Unread" ? "未读" : "已读";
                    string time = Convert.ToDateTime(row["created_time"]).ToString("yyyy-MM-dd HH:mm");

                    displayData.Rows.Add(
                        row["message_id"],
                        messageType,
                        row["title"],
                        row["content"],
                        priority,
                        status,
                        time
                    );
                }

                dgvMessages.DataSource = displayData;

                // 隐藏消息ID列
                if (dgvMessages.Columns.Contains("消息ID"))
                {
                    dgvMessages.Columns["消息ID"].Visible = false;
                }

                // 设置列宽
                if (dgvMessages.Columns.Contains("类型"))
                    dgvMessages.Columns["类型"].Width = 100;
                if (dgvMessages.Columns.Contains("标题"))
                    dgvMessages.Columns["标题"].Width = 200;
                if (dgvMessages.Columns.Contains("内容"))
                    dgvMessages.Columns["内容"].Width = 400;
                if (dgvMessages.Columns.Contains("优先级"))
                    dgvMessages.Columns["优先级"].Width = 80;
                if (dgvMessages.Columns.Contains("状态"))
                    dgvMessages.Columns["状态"].Width = 80;
                if (dgvMessages.Columns.Contains("时间"))
                    dgvMessages.Columns["时间"].Width = 150;

                // 未读消息加粗显示
                dgvMessages.CellFormatting += (s, cellArgs) =>
                {
                    if (cellArgs.RowIndex >= 0)
                    {
                        var statusCell = dgvMessages.Rows[cellArgs.RowIndex].Cells["状态"];
                        if (statusCell.Value?.ToString() == "未读")
                        {
                            cellArgs.CellStyle.Font = new Font(dgvMessages.Font, FontStyle.Bold);
                            cellArgs.CellStyle.BackColor = Color.FromArgb(255, 255, 224); // 淡黄色
                        }

                        // 紧急消息标红
                        var priorityCell = dgvMessages.Rows[cellArgs.RowIndex].Cells["优先级"];
                        if (priorityCell.Value?.ToString() == "紧急")
                        {
                            cellArgs.CellStyle.ForeColor = Color.Red;
                        }
                    }
                };

                // 更新统计信息
                int totalCount = currentMessages.Rows.Count;
                int unreadCount = NotificationService.Instance.GetUnreadCount(currentCardID);
                lblMessageCount.Text = $"共 {totalCount} 条消息，未读 {unreadCount} 条";
                lblMessageCount.ForeColor = unreadCount > 0 ? Color.Red : Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载消息失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetMessageTypeText(string type)
        {
            switch (type)
            {
                case "OverdueReminder": return "逾期提醒";
                case "SoonDueReminder": return "即将到期";
                case "ReservationReady": return "预约到书";
                case "ReservationExpired": return "预约过期";
                case "FineNotice": return "罚款通知";
                case "CardExpireReminder": return "证件到期";
                case "Announcement": return "系统公告";
                default: return "系统消息";
            }
        }

        private string GetPriorityText(string priority)
        {
            switch (priority)
            {
                case "Urgent": return "紧急";
                case "High": return "高";
                case "Normal": return "普通";
                case "Low": return "低";
                default: return "普通";
            }
        }
    }
}
