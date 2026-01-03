using System;
using System.Drawing;
using System.Windows.Forms;

namespace BooksManagermentSysytem.Utils
{
    /// <summary>
    /// DPI辅助类，用于统一处理高DPI缩放问题
    /// </summary>
    public static class DpiHelper
    {
        /// <summary>
        /// 标准DPI值（96）
        /// </summary>
        public const float StandardDpi = 96F;

        /// <summary>
        /// 为窗体设置正确的DPI缩放模式
        /// </summary>
        /// <param name="form">要设置的窗体</param>
        public static void SetupDpiScaling(Form form)
        {
            if (form == null)
                return;

            form.AutoScaleMode = AutoScaleMode.Dpi;
            form.AutoScaleDimensions = new SizeF(StandardDpi, StandardDpi);
        }

        /// <summary>
        /// 为用户控件设置正确的DPI缩放模式
        /// </summary>
        /// <param name="control">要设置的用户控件</param>
        public static void SetupDpiScaling(UserControl control)
        {
            if (control == null)
                return;

            control.AutoScaleMode = AutoScaleMode.Dpi;
            control.AutoScaleDimensions = new SizeF(StandardDpi, StandardDpi);
        }

        /// <summary>
        /// 规范化控件的Margin，避免DPI缩放时的额外间距
        /// </summary>
        /// <param name="control">要处理的控件</param>
        public static void NormalizeMargins(Control control)
        {
            if (control == null)
                return;

            control.Margin = Padding.Empty;
            
            foreach (Control child in control.Controls)
            {
                NormalizeMargins(child);
            }
        }

        /// <summary>
        /// 获取当前屏幕的DPI缩放比例
        /// </summary>
        /// <param name="control">参考控件</param>
        /// <returns>缩放比例（1.0表示100%）</returns>
        public static float GetScaleFactor(Control control)
        {
            if (control == null)
                return 1.0f;

            using (Graphics g = control.CreateGraphics())
            {
                return g.DpiX / StandardDpi;
            }
        }

        /// <summary>
        /// 根据DPI缩放值
        /// </summary>
        /// <param name="value">原始值</param>
        /// <param name="scaleFactor">缩放比例</param>
        /// <returns>缩放后的值</returns>
        public static int Scale(int value, float scaleFactor)
        {
            return (int)Math.Round(value * scaleFactor);
        }

        /// <summary>
        /// 根据DPI缩放大小
        /// </summary>
        /// <param name="size">原始大小</param>
        /// <param name="scaleFactor">缩放比例</param>
        /// <returns>缩放后的大小</returns>
        public static Size Scale(Size size, float scaleFactor)
        {
            return new Size(
                Scale(size.Width, scaleFactor),
                Scale(size.Height, scaleFactor)
            );
        }

        /// <summary>
        /// 根据DPI缩放点
        /// </summary>
        /// <param name="point">原始点</param>
        /// <param name="scaleFactor">缩放比例</param>
        /// <returns>缩放后的点</returns>
        public static Point Scale(Point point, float scaleFactor)
        {
            return new Point(
                Scale(point.X, scaleFactor),
                Scale(point.Y, scaleFactor)
            );
        }
    }
}
