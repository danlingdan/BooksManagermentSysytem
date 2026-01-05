using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace BooksManagermentSysytem.Utils
{
    /// <summary>
    /// 打印辅助类
    /// 支持打印DataGridView、DataTable和自定义内容
    /// </summary>
    public static class PrintHelper
    {
        /// <summary>
        /// 打印DataGridView内容
        /// </summary>
        public static void PrintDataGridView(DataGridView dgv, string title = null)
        {
            if (dgv == null || dgv.Rows.Count == 0)
            {
                MessageBox.Show("没有数据可打印", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                DataTable dt = new DataTable();
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (col.Visible)
                    {
                        dt.Columns.Add(col.HeaderText);
                    }
                }

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        DataRow dr = dt.NewRow();
                        int colIndex = 0;
                        foreach (DataGridViewColumn col in dgv.Columns)
                        {
                            if (col.Visible)
                            {
                                dr[colIndex++] = row.Cells[col.Index].Value?.ToString() ?? "";
                            }
                        }
                        dt.Rows.Add(dr);
                    }
                }

                PrintDataTable(dt, title);
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印失败：" + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 打印DataTable内容
        /// </summary>
        public static void PrintDataTable(DataTable dt, string title = null)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("没有数据可打印", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                DataTablePrinter printer = new DataTablePrinter(dt, title);
                
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += printer.PrintPage;

                PrintPreviewDialog preview = new PrintPreviewDialog();
                preview.Document = pd;
                preview.Width = 800;
                preview.Height = 600;
                preview.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印失败：" + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 打印文本内容
        /// </summary>
        public static void PrintText(string content, string title = null)
        {
            if (string.IsNullOrEmpty(content))
            {
                MessageBox.Show("没有内容可打印", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                TextPrinter printer = new TextPrinter(content, title);
                
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += printer.PrintPage;

                PrintPreviewDialog preview = new PrintPreviewDialog();
                preview.Document = pd;
                preview.Width = 800;
                preview.Height = 600;
                preview.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印失败：" + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region 内部打印器类

        /// <summary>
        /// DataTable打印器
        /// </summary>
        private class DataTablePrinter
        {
            private DataTable dataTable;
            private string title;
            private int currentRow;
            private Font headerFont = new Font("Microsoft YaHei", 14, FontStyle.Bold);
            private Font titleFont = new Font("Microsoft YaHei", 12, FontStyle.Bold);
            private Font normalFont = new Font("Microsoft YaHei", 10);
            private Font smallFont = new Font("Microsoft YaHei", 8);

            public DataTablePrinter(DataTable dt, string title)
            {
                this.dataTable = dt;
                this.title = title ?? "数据报表";
                this.currentRow = 0;
            }

            public void PrintPage(object sender, PrintPageEventArgs e)
            {
                float y = 50;
                float x = 50;
                float leftMargin = 50;
                float pageWidth = e.PageBounds.Width - 100;

                // 打印标题
                SizeF titleSize = e.Graphics.MeasureString(title, headerFont);
                e.Graphics.DrawString(title, headerFont, Brushes.Black,
                    (e.PageBounds.Width - titleSize.Width) / 2, y);
                y += titleSize.Height + 10;

                // 打印日期
                string dateStr = $"打印时间：{DateTime.Now:yyyy年MM月dd日 HH:mm}";
                e.Graphics.DrawString(dateStr, smallFont, Brushes.Gray, leftMargin, y);
                y += 30;

                // 计算列宽
                int colCount = dataTable.Columns.Count;
                float colWidth = pageWidth / colCount;

                // 打印表头
                x = leftMargin;
                foreach (DataColumn col in dataTable.Columns)
                {
                    RectangleF headerRect = new RectangleF(x, y, colWidth, 30);
                    e.Graphics.FillRectangle(Brushes.LightGray, headerRect);
                    e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(headerRect));
                    
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    e.Graphics.DrawString(col.ColumnName, titleFont, Brushes.Black, headerRect, sf);
                    
                    x += colWidth;
                }
                y += 30;

                // 打印数据行
                int rowsPerPage = (int)((e.PageBounds.Height - y - 50) / 25);
                int endRow = Math.Min(currentRow + rowsPerPage, dataTable.Rows.Count);

                for (int i = currentRow; i < endRow; i++)
                {
                    x = leftMargin;
                    DataRow row = dataTable.Rows[i];

                    foreach (DataColumn col in dataTable.Columns)
                    {
                        RectangleF cellRect = new RectangleF(x, y, colWidth, 25);
                        e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(cellRect));
                        
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        sf.Trimming = StringTrimming.EllipsisCharacter;
                        
                        string cellValue = row[col]?.ToString() ?? "";
                        e.Graphics.DrawString(cellValue, normalFont, Brushes.Black, cellRect, sf);
                        
                        x += colWidth;
                    }
                    y += 25;
                }

                // 打印页脚
                string footer = $"第 {(currentRow / rowsPerPage) + 1} 页    共 {dataTable.Rows.Count} 条记录";
                SizeF footerSize = e.Graphics.MeasureString(footer, smallFont);
                e.Graphics.DrawString(footer, smallFont, Brushes.Gray,
                    (e.PageBounds.Width - footerSize.Width) / 2, e.PageBounds.Height - 50);

                currentRow = endRow;
                e.HasMorePages = currentRow < dataTable.Rows.Count;

                if (!e.HasMorePages)
                {
                    currentRow = 0; // 重置以便再次打印
                }
            }
        }

        /// <summary>
        /// 文本打印器
        /// </summary>
        private class TextPrinter
        {
            private string content;
            private string title;
            private int currentChar;
            private Font titleFont = new Font("Microsoft YaHei", 14, FontStyle.Bold);
            private Font normalFont = new Font("Microsoft YaHei", 11);

            public TextPrinter(string content, string title)
            {
                this.content = content;
                this.title = title;
                this.currentChar = 0;
            }

            public void PrintPage(object sender, PrintPageEventArgs e)
            {
                float y = 50;
                float leftMargin = 50;

                // 打印标题（如果有）
                if (!string.IsNullOrEmpty(title))
                {
                    SizeF titleSize = e.Graphics.MeasureString(title, titleFont);
                    e.Graphics.DrawString(title, titleFont, Brushes.Black,
                        (e.PageBounds.Width - titleSize.Width) / 2, y);
                    y += titleSize.Height + 20;
                }

                // 打印内容
                RectangleF printArea = new RectangleF(leftMargin, y, e.PageBounds.Width - 100, e.PageBounds.Height - y - 50);
                int charsFitted;
                int linesFilled;
                
                SizeF layoutArea = new SizeF(printArea.Width, printArea.Height);
                SizeF stringSize = e.Graphics.MeasureString(content.Substring(currentChar), 
                    normalFont, layoutArea, StringFormat.GenericTypographic, 
                    out charsFitted, out linesFilled);

                string textToPrint = content.Substring(currentChar, Math.Min(charsFitted, content.Length - currentChar));
                e.Graphics.DrawString(textToPrint, normalFont, Brushes.Black, printArea);

                currentChar += charsFitted;
                e.HasMorePages = currentChar < content.Length;

                if (!e.HasMorePages)
                {
                    currentChar = 0; // 重置以便再次打印
                }
            }
        }

        #endregion
    }
}
