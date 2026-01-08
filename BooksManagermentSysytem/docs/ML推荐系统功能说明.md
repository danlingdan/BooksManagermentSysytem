# ML.NET 智能推荐系统功能说明

## 概述

图书馆管理系统已集成 ML.NET 机器学习框架，通过矩阵分解（Matrix Factorization）算法实现智能个性化推荐。该功能基于读者的借阅历史，利用协同过滤技术预测读者可能感兴趣的图书。

## 功能特点

### 🤖 机器学习推荐
- 基于 ML.NET 矩阵分解算法
- 自动学习用户借阅行为模式
- 预测用户对未借阅图书的兴趣度

### 🔀 混合推荐模式
- 支持纯 ML 推荐、传统协同过滤、混合推荐三种模式
- 混合模式结合两种算法优势，推荐效果更佳

### 📊 模型训练与管理
- 支持在线训练模型
- 可保存/加载训练好的模型
- 训练过程显示实时进度

---

## 使用指南

### 访问路径

```
主菜单 → 图书检索 → 图书推荐 → 🤖 ML智能推荐
```

### 界面说明

| 区域 | 说明 |
|------|------|
| 模型状态 | 显示当前模型是否已训练（✅ 已训练 / ⚠️ 未训练） |
| 推荐模式 | 下拉选择：ML推荐 / 混合推荐 / 传统推荐 |
| 训练模型 | 点击开始训练 ML 模型 |
| 获取推荐 | 根据选择的模式获取个性化推荐 |
| 进度条 | 训练时显示实时进度 |

### 使用流程

#### 首次使用

1. **登录系统**：使用借书证账号登录
2. **进入推荐页面**：菜单 → 图书检索 → 图书推荐
3. **切换标签页**：点击「🤖 ML智能推荐」标签
4. **训练模型**：点击「训练模型」按钮
5. **等待训练完成**：观察进度条，训练完成后会弹出提示
6. **获取推荐**：选择推荐模式，点击「获取推荐」

#### 日常使用

模型训练完成后，下次使用只需：
1. 进入 ML智能推荐 标签页
2. 选择推荐模式
3. 点击「获取推荐」

> ⚠️ **注意**：模型数据保存在内存中，程序关闭后需重新训练。如需持久化，可使用 MLModelManagementControl 保存模型文件。

---

## 推荐模式说明

### 1. ML推荐（纯机器学习）

使用矩阵分解算法预测用户评分，完全基于机器学习。

**优点**：
- 能发现深层次的兴趣模式
- 可推荐跨分类的图书

**适用场景**：
- 借阅历史丰富的用户
- 希望发现新领域图书

### 2. 混合推荐（推荐）

结合 ML 推荐和传统协同过滤，默认权重各 50%。

**优点**：
- 综合两种算法优势
- 推荐结果更稳定可靠

**适用场景**：
- 大多数用户的首选模式

### 3. 传统推荐

基于用户相似度和分类偏好的协同过滤。

**优点**：
- 无需训练模型
- 推荐理由更直观

**适用场景**：
- 模型未训练时的备选
- 借阅历史较少的新用户

---

## 技术架构

### 核心组件

```
RecommendationEngine/
├── Models/
│   ├── RecommendationModels.cs    # 推荐结果模型
│   └── MLModels.cs                # ML.NET 数据模型
├── Services/
│   ├── MatrixFactorizationService.cs  # ML.NET 推荐服务 ⭐
│   ├── PersonalizedService.cs         # 传统协同过滤
│   ├── ContentBasedService.cs         # 内容推荐
│   └── TrendingService.cs             # 热门榜
├── Data/
│   └── RecommendationRepository.cs    # 数据访问层
└── RecommendationFacade.cs            # 统一入口（门面模式）
```

### 算法原理

**矩阵分解（Matrix Factorization）**

将用户-图书评分矩阵分解为两个低维矩阵：
- 用户特征矩阵 U (用户数 × k)
- 图书特征矩阵 V (图书数 × k)

预测评分 = U[user] · V[book]^T

其中 k 为隐因子数量（默认 8），表示潜在的兴趣维度。

### 训练数据

系统使用借阅记录作为训练数据：

| 字段 | 说明 |
|------|------|
| UserId | 读者借书证号 |
| BibliographyId | 书目ID |
| Label | 隐式评分（借阅次数，1-5分） |

---

## 配置参数

### 训练配置（MatrixFactorizationConfig）

| 参数 | 默认值 | 说明 |
|------|--------|------|
| NumberOfIterations | 20 | 迭代次数，越高越精确但越慢 |
| ApproximationRank | 8 | 隐因子维度，越高捕获越复杂的模式 |
| HistoryDays | 365 | 使用多少天的历史数据 |
| Alpha | 0.01 | 置信度系数（隐式反馈） |
| Lambda | 0.1 | 正则化系数，防止过拟合 |

### 推荐配置

| 参数 | 默认值 | 说明 |
|------|--------|------|
| TopN | 15 | 返回推荐数量 |
| ExcludeBorrowed | true | 排除已借阅的书 |
| MLWeight（混合模式） | 0.5 | ML推荐权重 |

---

## API 接口

### RecommendationFacade 入口方法

```csharp
// 训练模型
MatrixFactorizationTrainingResult TrainMLModel(
    MatrixFactorizationConfig config = null,
    EventHandler<MatrixFactorizationProgressEventArgs> progressHandler = null)

// 获取 ML 推荐
List<RecommendationResult> GetMLRecommendations(
    string cardId, 
    int topN = 10, 
    bool excludeBorrowed = true)

// 获取混合推荐
List<RecommendationResult> GetHybridRecommendations(
    string cardId, 
    int topN = 10, 
    double mlWeight = 0.5)

// 预测单个评分
float PredictRating(string cardId, int bibliographyId)

// 保存/加载模型
void SaveMLModel(string filePath)
bool LoadMLModel(string filePath)

// 模型状态
bool IsMLModelTrained { get; }
```

### 使用示例

```csharp
var facade = new RecommendationFacade(connectionString);

// 训练模型
var config = new MatrixFactorizationConfig
{
    NumberOfIterations = 20,
    ApproximationRank = 8,
    HistoryDays = 365
};

var result = facade.TrainMLModel(config, (s, e) => 
{
    Console.WriteLine($"{e.ProgressPercentage}%: {e.Message}");
});

if (result.Success)
{
    Console.WriteLine($"训练完成！RMSE: {result.RootMeanSquaredError:F4}");
    
    // 获取推荐
    var recommendations = facade.GetMLRecommendations("BRW-2025-1-000001", 10);
    
    foreach (var rec in recommendations)
    {
        Console.WriteLine($"{rec.BookName} - 推荐度: {rec.Score:P0}");
    }
}
```

---

## 常见问题

### Q: 为什么推荐结果为空？

**可能原因**：
1. 借阅历史不足（建议至少 3 条借阅记录）
2. 模型未训练（ML推荐模式需要先训练）
3. 所有图书都已借阅过

**解决方法**：
- 切换到「传统推荐」或「混合推荐」模式
- 确保已点击「训练模型」

### Q: 训练模型需要多长时间？

取决于数据量：
- 100 条借阅记录：< 1 秒
- 1000 条借阅记录：1-3 秒
- 10000 条借阅记录：5-15 秒

### Q: 模型会自动更新吗？

当前版本不支持自动更新。建议定期手动重新训练以纳入新的借阅数据。

### Q: 如何提高推荐准确度？

1. 增加训练数据（更长的历史天数）
2. 调整隐因子维度（ApproximationRank）
3. 增加迭代次数（NumberOfIterations）
4. 使用混合推荐模式

---

## 版本信息

| 组件 | 版本 |
|------|------|
| ML.NET | 5.0.0 |
| ML.NET Recommender | 0.23.0 |
| 目标框架 | .NET Framework 4.8 |

---

## 相关文件

| 文件路径 | 说明 |
|----------|------|
| `RecommendationEngine\Models\MLModels.cs` | ML数据模型定义 |
| `RecommendationEngine\Services\MatrixFactorizationService.cs` | 核心ML服务 |
| `BooksManagermentSysytem\src\Controls\RecommendationControl.cs` | 推荐界面控件 |
| `BooksManagermentSysytem\src\Controls\MLModelManagementControl.cs` | ML管理控件 |

---

## 更新日志

### 2025-01-xx
- ✅ 新增 ML.NET 矩阵分解推荐功能
- ✅ 集成到图书推荐界面（新增「ML智能推荐」标签页）
- ✅ 支持模型训练、预测、保存/加载
- ✅ 支持 ML/传统/混合三种推荐模式
- ✅ 添加 MLModelManagementControl 管理控件
