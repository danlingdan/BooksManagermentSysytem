using System;
using System.Windows.Forms;

namespace BooksManagermentSysytem.Helpers
{
    /// <summary>
    /// 输入对话框帮助类，替代 Microsoft.VisualBasic.Interaction.InputBox
    /// </summary>
    public static class InputDialog
    {
        /// <summary>
        /// 显示输入对话框
        /// </summary>
        /// <param name="prompt">提示信息</param>
        /// <param name="title">标题</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>用户输入的值，取消返回空字符串</returns>
        public static string Show(string prompt, string title, string defaultValue = "")
        {
            using (Form form = new Form())
            {
                form.Text = title;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterParent;
                form.MaximizeBox = false;
                form.MinimizeBox = false;
                form.ShowInTaskbar = false;
                form.ClientSize = new System.Drawing.Size(400, 150);
                form.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);

                Label label = new Label();
                label.Text = prompt;
                label.AutoSize = false;
                label.Location = new System.Drawing.Point(10, 10);
                label.Size = new System.Drawing.Size(380, 60);

                TextBox textBox = new TextBox();
                textBox.Text = defaultValue;
                textBox.Location = new System.Drawing.Point(10, 75);
                textBox.Size = new System.Drawing.Size(380, 23);

                Button okButton = new Button();
                okButton.Text = "确定";
                okButton.DialogResult = DialogResult.OK;
                okButton.Location = new System.Drawing.Point(225, 110);
                okButton.Size = new System.Drawing.Size(80, 30);

                Button cancelButton = new Button();
                cancelButton.Text = "取消";
                cancelButton.DialogResult = DialogResult.Cancel;
                cancelButton.Location = new System.Drawing.Point(310, 110);
                cancelButton.Size = new System.Drawing.Size(80, 30);

                form.Controls.Add(label);
                form.Controls.Add(textBox);
                form.Controls.Add(okButton);
                form.Controls.Add(cancelButton);
                form.AcceptButton = okButton;
                form.CancelButton = cancelButton;

                DialogResult result = form.ShowDialog();
                return result == DialogResult.OK ? textBox.Text : "";
            }
        }
    }
}
