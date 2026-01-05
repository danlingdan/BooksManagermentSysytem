using System;
using System.Windows.Forms;

namespace BooksManagermentSysytem.Forms
{
    /// <summary>
    /// MainForm Designer 扩展部分
    /// 用于添加角色权限管理和批量角色分配菜单项
    /// </summary>
    /// <remarks>
    /// 请在MainForm.Designer.cs的InitializeComponent方法中手动添加以下代码
    /// </remarks>
    public partial class MainForm
    {
        // 在字段声明区域添加（文件末尾，#endregion之前）：
        // private System.Windows.Forms.ToolStripMenuItem menuPermissionManagement;
        // private System.Windows.Forms.ToolStripMenuItem menuBatchRoleAssignment;

        // 在InitializeComponent方法开始处添加（在其他菜单项实例化之后）：
        private void InitializeNewMenuItems()
        {
            this.menuPermissionManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBatchRoleAssignment = new System.Windows.Forms.ToolStripMenuItem();
        }

        // 在InitializeComponent方法中，配置menuPermissionManagement（在menuUserManagement之后）：
        private void ConfigurePermissionManagementMenu()
        {
            // 
            // menuPermissionManagement
            // 
            this.menuPermissionManagement.Name = "menuPermissionManagement";
            this.menuPermissionManagement.Size = new System.Drawing.Size(180, 24);
            this.menuPermissionManagement.Text = "角色权限管理";
            this.menuPermissionManagement.Click += new System.EventHandler(this.menuPermissionManagement_Click);
        }

        // 配置menuBatchRoleAssignment：
        private void ConfigureBatchRoleAssignmentMenu()
        {
            // 
            // menuBatchRoleAssignment
            // 
            this.menuBatchRoleAssignment.Name = "menuBatchRoleAssignment";
            this.menuBatchRoleAssignment.Size = new System.Drawing.Size(180, 24);
            this.menuBatchRoleAssignment.Text = "批量角色分配";
            this.menuBatchRoleAssignment.Click += new System.EventHandler(this.menuBatchRoleAssignment_Click);
        }
    }
}
