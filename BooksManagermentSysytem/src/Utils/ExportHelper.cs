using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BooksManagermentSysytem.Utils
{
    /// <summary>
    /// 数据导出辅助类
    /// 支持导出为 Excel、CSV、TXT 等格式
    /// </summary>
    public static class ExportHelper
    {
        /// <summary>
        /// 导出DataGridView数据到CSV文件
        /// </summary>
        public static bool ExportDataGridViewToCSV(DataGridView dgv, string defaultFileName = null)
        {
            if (dgv == null || dgv.Rows.Count == 0)
            {
                MessageBox.Show("没有数据可导出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV文件|*.csv|文本文件|*.txt|所有文件|*.*";
                sfd.FileName = defaultFileName ?? $"导出数据_{DateTime.Now:yyyyMMddHHmmss}.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string content = DataGridViewToCSV(dgv);
                        File.WriteAllText(sfd.FileName, content, Encoding.UTF8);
                        
                        MessageBox.Show($"导出成功！\n文件保存至：{sfd.FileName}", "成功",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出失败：" + ex.Message, "错误",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 导出DataTable数据到CSV文件
        /// </summary>
        public static bool ExportDataTableToCSV(DataTable dt, string defaultFileName = null, string title = null)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("没有数据可导出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV文件|*.csv|文本文件|*.txt|所有文件|*.*";
                sfd.FileName = defaultFileName ?? $"导出数据_{DateTime.Now:yyyyMMddHHmmss}.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();

                        // 添加标题（可选）
                        if (!string.IsNullOrEmpty(title))
                        {
                            sb.AppendLine(title);
                            sb.AppendLine($"导出时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                            sb.AppendLine();
                        }

                        // 写入列头
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            sb.Append(EscapeCSVField(dt.Columns[i].ColumnName));
                            if (i < dt.Columns.Count - 1)
                                sb.Append(",");
                        }
                        sb.AppendLine();

                        // 写入数据行
                        foreach (DataRow row in dt.Rows)
                        {
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                sb.Append(EscapeCSVField(row[i]?.ToString() ?? ""));
                                if (i < dt.Columns.Count - 1)
                                    sb.Append(",");
                            }
                            sb.AppendLine();
                        }

                        File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);

                        MessageBox.Show($"导出成功！\n文件保存至：{sfd.FileName}\n共导出 {dt.Rows.Count} 条记录", 
                            "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出失败：" + ex.Message, "错误",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 将DataGridView转换为CSV字符串
        /// </summary>
        private static string DataGridViewToCSV(DataGridView dgv)
        {
            StringBuilder sb = new StringBuilder();

            // 写入列头
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (dgv.Columns[i].Visible)
                {
                    sb.Append(EscapeCSVField(dgv.Columns[i].HeaderText));
                    if (i < dgv.Columns.Count - 1)
                        sb.Append(",");
                }
            }
            sb.AppendLine();

            // 写入数据行
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;

                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    if (dgv.Columns[i].Visible)
                    {
                        object cellValue = row.Cells[i].Value;
                        string value = cellValue?.ToString() ?? "";
                        sb.Append(EscapeCSVField(value));
                        if (i < dgv.Columns.Count - 1)
                            sb.Append(",");
                    }
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        /// <summary>
        /// 转义CSV字段（处理逗号、引号、换行符）
        /// </summary>
        private static string EscapeCSVField(string field)
        {
            if (string.IsNullOrEmpty(field))
                return "";

            // 如果包含逗号、引号或换行符，需要用引号包围，并将引号转义为双引号
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
            {
                return "\"" + field.Replace("\"", "\"\"") + "\"";
            }

            return field;
        }

        /// <summary>
        /// 导出为HTML格式（可在Excel中打开）
        /// </summary>
        public static bool ExportDataTableToHTML(DataTable dt, string defaultFileName = null, string title = null)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("没有数据可导出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "HTML文件|*.html|所有文件|*.*";
                sfd.FileName = defaultFileName ?? $"导出数据_{DateTime.Now:yyyyMMddHHmmss}.html";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("<!DOCTYPE html>");
                        sb.AppendLine("<html>");
                        sb.AppendLine("<head>");
                        sb.AppendLine("<meta charset='utf-8'>");
                        sb.AppendLine($"<title>{title ?? "数据导出"}</title>");
                        sb.AppendLine("<style>");
                        sb.AppendLine("table { border-collapse: collapse; width: 100%; font-family: 'Microsoft YaHei'; }");
                        sb.AppendLine("th { background-color: #0078d4; color: white; padding: 10px; border: 1px solid #ddd; }");
                        sb.AppendLine("td { padding: 8px; border: 1px solid #ddd; }");
                        sb.AppendLine("tr:nth-child(even) { background-color: #f2f2f2; }");
                        sb.AppendLine("h2 { font-family: 'Microsoft YaHei'; color: #333; }");
                        sb.AppendLine("</style>");
                        sb.AppendLine("</head>");
                        sb.AppendLine("<body>");

                        if (!string.IsNullOrEmpty(title))
                        {
                            sb.AppendLine($"<h2>{title}</h2>");
                            sb.AppendLine($"<p>导出时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss}</p>");
                        }

                        sb.AppendLine("<table>");
                        sb.AppendLine("<thead><tr>");
                        foreach (DataColumn col in dt.Columns)
                        {
                            sb.AppendLine($"<th>{HtmlEncode(col.ColumnName)}</th>");
                        }
                        sb.AppendLine("</tr></thead>");

                        sb.AppendLine("<tbody>");
                        foreach (DataRow row in dt.Rows)
                        {
                            sb.AppendLine("<tr>");
                            foreach (DataColumn col in dt.Columns)
                            {
                                string value = row[col]?.ToString() ?? "";
                                sb.AppendLine($"<td>{HtmlEncode(value)}</td>");
                            }
                            sb.AppendLine("</tr>");
                        }
                        sb.AppendLine("</tbody>");
                        sb.AppendLine("</table>");

                        sb.AppendLine("</body>");
                        sb.AppendLine("</html>");

                        File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);

                        MessageBox.Show($"导出成功！\n文件保存至：{sfd.FileName}", "成功",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出失败：" + ex.Message, "错误",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// HTML编码字符串
        /// </summary>
        private static string HtmlEncode(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";

            return text.Replace("&", "&amp;")
                       .Replace("<", "&lt;")
                       .Replace(">", "&gt;")
                       .Replace("\"", "&quot;")
                       .Replace("'", "&#39;");
        }
    }
}
