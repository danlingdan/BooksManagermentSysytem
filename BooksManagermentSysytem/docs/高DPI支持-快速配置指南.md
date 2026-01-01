# 高DPI支持 - 快速配置指南

## ⚠️ 重要：需要手动完成最后一步

高DPI支持已基本配置完成，但由于项目文件正在被Visual Studio使用，需要您手动完成最后一步配置。

## 📋 手动配置步骤（5分钟）

### 1️⃣ 关闭 Visual Studio
完全关闭当前的Visual Studio实例。

### 2️⃣ 编辑项目文件
1. 打开文件资源管理器
2. 导航到: `BooksManagermentSysytem\BooksManagermentSysytem.csproj`
3. 用记事本或任何文本编辑器打开此文件

### 3️⃣ 添加配置
在第一个 `<PropertyGroup>` 节点中（大约在第7-18行），添加以下内容：

**查找这段代码**:
```xml
<PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProjectGuid>{41B6DE0F-EC53-4E93-AD18-B2D0F9277B8D}</ProjectGuid>
    <OutputType>WinExe</OutputType>
    <RootNamespace>BooksManagermentSysytem</RootNamespace>
    <AssemblyName>BooksManagermentSysytem</AssemblyName>
    <TargetFrameworkVersion>v4.8</TargetFrameworkVersion>
    <FileAlignment>512</FileAlignment>
    <AutoGenerateBindingRedirects>true</AutoGenerateBindingRedirects>
    <Deterministic>true</Deterministic>
</PropertyGroup>
```

**在 `<Deterministic>true</Deterministic>` 后面添加这一行**:
```xml
<ApplicationManifest>src\app.manifest</ApplicationManifest>
```

**修改后应该是这样**:
```xml
<PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProjectGuid>{41B6DE0F-EC53-4E93-AD18-B2D0F9277B8D}</ProjectGuid>
    <OutputType>WinExe</OutputType>
    <RootNamespace>BooksManagermentSysytem</RootNamespace>
    <AssemblyName>BooksManagermentSysytem</AssemblyName>
    <TargetFrameworkVersion>v4.8</TargetFrameworkVersion>
    <FileAlignment>512</FileAlignment>
    <AutoGenerateBindingRedirects>true</AutoGenerateBindingRedirects>
    <Deterministic>true</Deterministic>
    <ApplicationManifest>src\app.manifest</ApplicationManifest>  <!-- ← 添加这一行 -->
</PropertyGroup>
```

### 4️⃣ 保存文件
保存 `BooksManagermentSysytem.csproj` 文件。

### 5️⃣ 重新打开 Visual Studio
1. 启动 Visual Studio
2. 打开解决方案
3. 重新生成解决方案（Ctrl + Shift + B）

## ✅ 验证配置成功

### 方法1：检查项目属性
1. 在解决方案资源管理器中右键点击项目
2. 选择"属性"
3. 查看"应用程序"标签页
4. 确认"清单"下拉框显示为 `src\app.manifest`

### 方法2：运行测试
1. 运行程序（F5）
2. 在Windows显示设置中更改缩放比例（100%, 125%, 150%, 200%）
3. 检查程序界面是否清晰（不模糊）

## 📊 配置完成检查清单

- [x] app.config 已配置 DPI 感知
- [x] app.manifest 文件已创建
- [ ] **项目文件已添加 ApplicationManifest 引用** ← 需要您完成
- [x] Program.cs 已移除不兼容的代码
- [x] 编译测试通过

## 🎯 完成后的效果

配置完成后，您的应用程序将：

✅ 在4K显示器上显示清晰锐利
✅ 支持100%-200%的显示缩放
✅ 在多显示器环境下自动适应DPI
✅ 避免文字和界面模糊

## ❓ 常见问题

### Q: 如果找不到 `<Deterministic>` 行怎么办？
A: 只要在第一个 `</PropertyGroup>` 之前（闭合标签前）添加 `<ApplicationManifest>src\app.manifest</ApplicationManifest>` 即可。

### Q: 添加后编译失败怎么办？
A: 检查：
1. 文件路径是否正确：`src\app.manifest`
2. XML 语法是否正确（标签闭合等）
3. 确认 `src\app.manifest` 文件确实存在

### Q: 我可以跳过这一步吗？
A: 技术上可以，但app.config中的DPI配置可能不会完全生效。强烈建议完成此步骤以获得最佳的高DPI支持。

## 📞 需要帮助？

如果遇到任何问题，请查看详细文档：
- `docs\高DPI支持配置说明.md`

---

**预计完成时间**: 5分钟
**难度**: ⭐☆☆☆☆ (非常简单)
**必需性**: ⚠️ 强烈推荐
