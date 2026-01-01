using System;
using System.Windows.Forms;
using BooksManagermentSysytem.Forms;

namespace BooksManagermentSysytem
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 在 .NET Framework 4.8 中，DPI感知通过app.manifest和app.config配置
            // 无需在代码中调用API
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // 全局异常处理
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            
            Application.Run(new LoginForm());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show($"发生错误：{e.Exception.Message}", "错误", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            MessageBox.Show($"发生严重错误：{ex?.Message}", "严重错误", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
