using System;
using System.Windows.Forms;

namespace BooksManagermentSysytem.Forms
{
    /// <summary>
    /// MainForm Designer 扩展说明
    /// 本文件提供添加新菜单项的指导说明
    /// </summary>
    /// <remarks>
    /// 若要添加 ML 模型管理菜单项，请按以下步骤在 MainForm.Designer.cs 中修改：
    /// 
    /// 1. 在字段声明区域添加：
    ///    private System.Windows.Forms.ToolStripMenuItem menuMLModelManagement;
    /// 
    /// 2. 在 InitializeComponent 方法中实例化：
    ///    this.menuMLModelManagement = new System.Windows.Forms.ToolStripMenuItem();
    /// 
    /// 3. 配置菜单项：
    ///    this.menuMLModelManagement.Name = "menuMLModelManagement";
    ///    this.menuMLModelManagement.Size = new System.Drawing.Size(148, 24);
    ///    this.menuMLModelManagement.Text = "ML模型管理";
    ///    this.menuMLModelManagement.Click += new System.EventHandler(this.menuMLModelManagement_Click);
    /// 
    /// 4. 将菜单项添加到 menuAdmin.DropDownItems 中
    /// 
    /// 或者：直接使用图书推荐控件中的 ML 标签页（已集成）
    /// </remarks>
    public partial class MainForm
    {
        // 此文件仅提供说明，无需编译代码
        // 实际 ML 推荐功能已集成到 RecommendationControl 的 ML智能推荐 标签页中
    }
}
