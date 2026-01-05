using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BooksManagermentSysytem.Data;

namespace BooksManagermentSysytem.Helpers
{
    /// <summary>
    /// 借书证选择器辅助类
    /// 用于在ComboBox中显示和选择借书证号
    /// </summary>
    public static class CardIDSelector
    {
        /// <summary>
        /// 借书证项
        /// </summary>
        public class CardIDItem
        {
            public string CardID { get; set; }
            public string ReaderName { get; set; }
            public string ReaderType { get; set; }
            public string State { get; set; }

            public override string ToString()
            {
                return $"{CardID} - {ReaderName} ({ReaderType})";
            }
        }

        /// <summary>
        /// 初始化借书证选择下拉框
        /// </summary>
        /// <param name="comboBox">要初始化的ComboBox</param>
        /// <param name="onlyNormal">是否只显示状态正常的借书证</param>
        /// <param name="allowEmpty">是否允许空选项</param>
        public static void InitializeCardIDComboBox(ComboBox comboBox, bool onlyNormal = true, bool allowEmpty = true)
        {
            try
            {
                comboBox.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;

                LoadCardIDs(comboBox, onlyNormal, allowEmpty);
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化借书证列表失败：" + ex.Message, "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 加载借书证列表
        /// </summary>
        /// <param name="comboBox">ComboBox控件</param>
        /// <param name="onlyNormal">是否只显示状态正常的</param>
        /// <param name="allowEmpty">是否允许空选项</param>
        public static void LoadCardIDs(ComboBox comboBox, bool onlyNormal = true, bool allowEmpty = true)
        {
            try
            {
                string sql = @"
                    SELECT r.cardID, r.readername, r.readertype, rc.state
                    FROM reader r
                    INNER JOIN readcard rc ON r.cardID = rc.cardID";

                if (onlyNormal)
                {
                    sql += " WHERE rc.state = N'正常' AND rc.overdate >= GETDATE()";
                }

                sql += " ORDER BY r.cardID DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql);

                comboBox.Items.Clear();

                if (allowEmpty)
                {
                    comboBox.Items.Add(new CardIDItem 
                    { 
                        CardID = "", 
                        ReaderName = "- 请选择或输入借书证号 -", 
                        ReaderType = "", 
                        State = "" 
                    });
                }

                foreach (DataRow row in dt.Rows)
                {
                    comboBox.Items.Add(new CardIDItem
                    {
                        CardID = row["cardID"].ToString(),
                        ReaderName = row["readername"].ToString(),
                        ReaderType = row["readertype"].ToString(),
                        State = row["state"].ToString()
                    });
                }

                if (allowEmpty && comboBox.Items.Count > 0)
                {
                    comboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载借书证列表失败：" + ex.Message, "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 获取选中的借书证号
        /// </summary>
        /// <param name="comboBox">ComboBox控件</param>
        /// <returns>借书证号，如果未选择则返回用户输入的文本</returns>
        public static string GetSelectedCardID(ComboBox comboBox)
        {
            if (comboBox.SelectedItem is CardIDItem item && !string.IsNullOrEmpty(item.CardID))
            {
                return item.CardID;
            }

            // 如果用户直接输入了文本
            return comboBox.Text.Trim();
        }

        /// <summary>
        /// 设置选中的借书证号
        /// </summary>
        /// <param name="comboBox">ComboBox控件</param>
        /// <param name="cardID">要选中的借书证号</param>
        public static void SetSelectedCardID(ComboBox comboBox, string cardID)
        {
            if (string.IsNullOrWhiteSpace(cardID))
            {
                if (comboBox.Items.Count > 0)
                {
                    comboBox.SelectedIndex = 0;
                }
                return;
            }

            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i] is CardIDItem item && item.CardID == cardID)
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }

            // 如果找不到匹配项，直接设置文本
            comboBox.Text = cardID;
        }

        /// <summary>
        /// 刷新借书证列表
        /// </summary>
        /// <param name="comboBox">ComboBox控件</param>
        /// <param name="onlyNormal">是否只显示状态正常的</param>
        /// <param name="allowEmpty">是否允许空选项</param>
        public static void RefreshCardIDs(ComboBox comboBox, bool onlyNormal = true, bool allowEmpty = true)
        {
            string currentCardID = GetSelectedCardID(comboBox);
            LoadCardIDs(comboBox, onlyNormal, allowEmpty);
            
            if (!string.IsNullOrEmpty(currentCardID))
            {
                SetSelectedCardID(comboBox, currentCardID);
            }
        }

        /// <summary>
        /// 根据读者类型筛选借书证
        /// </summary>
        /// <param name="comboBox">ComboBox控件</param>
        /// <param name="readerType">读者类型，如果为null或空则显示全部</param>
        /// <param name="onlyNormal">是否只显示状态正常的</param>
        /// <param name="allowEmpty">是否允许空选项</param>
        public static void FilterByReaderType(ComboBox comboBox, string readerType, bool onlyNormal = true, bool allowEmpty = true)
        {
            try
            {
                string sql = @"
                    SELECT r.cardID, r.readername, r.readertype, rc.state
                    FROM reader r
                    INNER JOIN readcard rc ON r.cardID = rc.cardID
                    WHERE 1=1";

                if (onlyNormal)
                {
                    sql += " AND rc.state = N'正常' AND rc.overdate >= GETDATE()";
                }

                if (!string.IsNullOrWhiteSpace(readerType) && readerType != "全部")
                {
                    sql += " AND r.readertype = @readerType";
                }

                sql += " ORDER BY r.cardID DESC";

                DataTable dt;
                if (!string.IsNullOrWhiteSpace(readerType) && readerType != "全部")
                {
                    dt = DatabaseHelper.ExecuteQuery(sql, 
                        DatabaseHelper.CreateParameter("@readerType", readerType));
                }
                else
                {
                    dt = DatabaseHelper.ExecuteQuery(sql);
                }

                comboBox.Items.Clear();

                if (allowEmpty)
                {
                    comboBox.Items.Add(new CardIDItem 
                    { 
                        CardID = "", 
                        ReaderName = "- 请选择或输入借书证号 -", 
                        ReaderType = "", 
                        State = "" 
                    });
                }

                foreach (DataRow row in dt.Rows)
                {
                    comboBox.Items.Add(new CardIDItem
                    {
                        CardID = row["cardID"].ToString(),
                        ReaderName = row["readername"].ToString(),
                        ReaderType = row["readertype"].ToString(),
                        State = row["state"].ToString()
                    });
                }

                if (allowEmpty && comboBox.Items.Count > 0)
                {
                    comboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("筛选借书证失败：" + ex.Message, "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
