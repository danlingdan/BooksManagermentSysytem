using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using BooksManagermentSysytem.Data;
using BooksManagermentSysytem.Models;

namespace BooksManagermentSysytem.Services
{
    /// <summary>
    /// 消息通知服务
    /// 统一的消息中心，支持系统消息、逾期提醒、预约通知等多种消息类型
    /// </summary>
    public class NotificationService
    {
        private static NotificationService _instance;
        private static readonly object _lock = new object();

        /// <summary>
        /// 获取单例实例
        /// </summary>
        public static NotificationService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new NotificationService();
                        }
                    }
                }
                return _instance;
            }
        }

        private NotificationService() { }

        /// <summary>
        /// 创建系统消息
        /// </summary>
        /// <param name="cardID">接收者借书证号</param>
        /// <param name="messageType">消息类型</param>
        /// <param name="title">消息标题</param>
        /// <param name="content">消息内容</param>
        /// <param name="priority">优先级</param>
        /// <param name="relatedId">关联ID</param>
        /// <param name="relatedType">关联类型</param>
        /// <returns>消息ID，失败返回0</returns>
        public long CreateMessage(string cardID, MessageType messageType, string title, string content,
            MessagePriority priority = MessagePriority.Normal, string relatedId = null, string relatedType = null)
        {
            try
            {
                string sql = @"
                    INSERT INTO system_message (cardID, message_type, title, content, priority, related_id, related_type)
                    VALUES (@cardID, @messageType, @title, @content, @priority, @relatedId, @relatedType);
                    SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";

                object result = DatabaseHelper.ExecuteScalar(sql,
                    DatabaseHelper.CreateParameter("@cardID", cardID),
                    DatabaseHelper.CreateParameter("@messageType", messageType.ToString()),
                    DatabaseHelper.CreateParameter("@title", title),
                    DatabaseHelper.CreateParameter("@content", content),
                    DatabaseHelper.CreateParameter("@priority", priority.ToString()),
                    DatabaseHelper.CreateParameter("@relatedId", relatedId ?? (object)DBNull.Value),
                    DatabaseHelper.CreateParameter("@relatedType", relatedType ?? (object)DBNull.Value));

                return result != null ? Convert.ToInt64(result) : 0;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 根据模板创建消息
        /// </summary>
        /// <param name="cardID">接收者借书证号</param>
        /// <param name="templateCode">模板代码</param>
        /// <param name="parameters">模板参数</param>
        /// <param name="relatedId">关联ID</param>
        /// <param name="relatedType">关联类型</param>
        /// <returns>消息ID</returns>
        public long CreateMessageFromTemplate(string cardID, string templateCode, 
            Dictionary<string, string> parameters, string relatedId = null, string relatedType = null)
        {
            try
            {
                string sql = @"
                    SELECT message_type, title_template, content_template, priority 
                    FROM message_template 
                    WHERE template_code = @templateCode AND is_active = 1";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@templateCode", templateCode));

                if (dt.Rows.Count == 0)
                    return 0;

                DataRow row = dt.Rows[0];
                string title = ReplaceTemplate(row["title_template"].ToString(), parameters);
                string content = ReplaceTemplate(row["content_template"].ToString(), parameters);
                MessageType messageType = ParseMessageType(row["message_type"].ToString());
                MessagePriority priority = ParsePriority(row["priority"].ToString());

                return CreateMessage(cardID, messageType, title, content, priority, relatedId, relatedType);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取读者的消息列表
        /// </summary>
        /// <param name="cardID">借书证号</param>
        /// <param name="includeRead">是否包含已读消息</param>
        /// <param name="maxCount">最大数量，0表示不限制</param>
        /// <returns>消息列表</returns>
        public DataTable GetMessages(string cardID, bool includeRead = true, int maxCount = 0)
        {
            try
            {
                string sql = @"
                    SELECT " + (maxCount > 0 ? $"TOP {maxCount} " : "") + @"
                        message_id, cardID, message_type, title, content, priority, 
                        status, created_time, read_time, related_id, related_type
                    FROM system_message
                    WHERE cardID = @cardID AND status <> N'Deleted'";

                if (!includeRead)
                {
                    sql += " AND status = N'Unread'";
                }

                sql += " ORDER BY created_time DESC";

                return DatabaseHelper.ExecuteQuery(sql,
                    DatabaseHelper.CreateParameter("@cardID", cardID));
            }
            catch
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// 获取未读消息数量
        /// </summary>
        /// <param name="cardID">借书证号</param>
        /// <returns>未读消息数量</returns>
        public int GetUnreadCount(string cardID)
        {
            try
            {
                string sql = @"
                    SELECT COUNT(*) 
                    FROM system_message 
                    WHERE cardID = @cardID AND status = N'Unread'";

                object result = DatabaseHelper.ExecuteScalar(sql,
                    DatabaseHelper.CreateParameter("@cardID", cardID));

                return result != null ? Convert.ToInt32(result) : 0;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 标记消息为已读
        /// </summary>
        /// <param name="messageId">消息ID</param>
        /// <returns>是否成功</returns>
        public bool MarkAsRead(long messageId)
        {
            try
            {
                string sql = @"
                    UPDATE system_message 
                    SET status = N'Read', read_time = SYSDATETIME() 
                    WHERE message_id = @messageId AND status = N'Unread'";

                int affected = DatabaseHelper.ExecuteNonQuery(sql,
                    DatabaseHelper.CreateParameter("@messageId", messageId));

                return affected > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 标记所有消息为已读
        /// </summary>
        /// <param name="cardID">借书证号</param>
        /// <returns>标记的消息数量</returns>
        public int MarkAllAsRead(string cardID)
        {
            try
            {
                string sql = @"
                    UPDATE system_message 
                    SET status = N'Read', read_time = SYSDATETIME() 
                    WHERE cardID = @cardID AND status = N'Unread'";

                return DatabaseHelper.ExecuteNonQuery(sql,
                    DatabaseHelper.CreateParameter("@cardID", cardID));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="messageId">消息ID</param>
        /// <returns>是否成功</returns>
        public bool DeleteMessage(long messageId)
        {
            try
            {
                string sql = @"
                    UPDATE system_message 
                    SET status = N'Deleted' 
                    WHERE message_id = @messageId";

                int affected = DatabaseHelper.ExecuteNonQuery(sql,
                    DatabaseHelper.CreateParameter("@messageId", messageId));

                return affected > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 批量删除消息
        /// </summary>
        /// <param name="messageIds">消息ID列表</param>
        /// <returns>删除的消息数量</returns>
        public int DeleteMessages(List<long> messageIds)
        {
            if (messageIds == null || messageIds.Count == 0)
                return 0;

            try
            {
                string ids = string.Join(",", messageIds);
                string sql = $@"
                    UPDATE system_message 
                    SET status = N'Deleted' 
                    WHERE message_id IN ({ids})";

                return DatabaseHelper.ExecuteNonQuery(sql);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 创建系统公告（发送给所有读者）
        /// </summary>
        /// <param name="title">公告标题</param>
        /// <param name="content">公告内容</param>
        /// <param name="priority">优先级</param>
        /// <returns>创建的消息数量</returns>
        public int CreateAnnouncement(string title, string content, MessagePriority priority = MessagePriority.Normal)
        {
            try
            {
                // 获取所有正常状态的读者
                string getReadersSql = @"
                    SELECT r.cardID 
                    FROM reader r
                    INNER JOIN readcard rc ON r.cardID = rc.cardID
                    WHERE rc.state = N'正常'";

                DataTable readers = DatabaseHelper.ExecuteQuery(getReadersSql);

                int count = 0;
                foreach (DataRow row in readers.Rows)
                {
                    string cardID = row["cardID"].ToString();
                    if (CreateMessage(cardID, MessageType.Announcement, title, content, priority) > 0)
                    {
                        count++;
                    }
                }

                return count;
            }
            catch
            {
                return 0;
            }
        }

        #region 私有辅助方法

        private string ReplaceTemplate(string template, Dictionary<string, string> parameters)
        {
            if (string.IsNullOrEmpty(template) || parameters == null)
                return template;

            string result = template;
            
            // 替换 {参数名} 格式的占位符
            foreach (var param in parameters)
            {
                result = result.Replace("{" + param.Key + "}", param.Value);
            }

            // 替换 {0}, {1} 格式的占位符（如果有）
            var matches = Regex.Matches(result, @"\{(\d+)\}");
            if (matches.Count > 0 && parameters.Count > 0)
            {
                var values = parameters.Values.ToArray();
                for (int i = 0; i < matches.Count && i < values.Length; i++)
                {
                    result = result.Replace("{" + i + "}", values[i]);
                }
            }

            return result;
        }

        private MessageType ParseMessageType(string typeString)
        {
            try
            {
                return (MessageType)Enum.Parse(typeof(MessageType), typeString);
            }
            catch
            {
                return MessageType.System;
            }
        }

        private MessagePriority ParsePriority(string priorityString)
        {
            try
            {
                return (MessagePriority)Enum.Parse(typeof(MessagePriority), priorityString);
            }
            catch
            {
                return MessagePriority.Normal;
            }
        }

        #endregion
    }
}
