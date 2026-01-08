using Microsoft.ML.Data;

namespace RecommendationEngine.Models
{
    /// <summary>
    /// ML.NET 矩阵分解训练数据模型
    /// 表示用户-图书评分记录
    /// </summary>
    public class BookRating
    {
        /// <summary>
        /// 读者卡号（用户ID）
        /// </summary>
        [LoadColumn(0)]
        public string UserId { get; set; }

        /// <summary>
        /// 书目ID
        /// </summary>
        [LoadColumn(1)]
        public uint BibliographyId { get; set; }

        /// <summary>
        /// 评分/隐式反馈分数
        /// 借阅记录可转换为隐式评分（如借阅=1，未借阅=0）
        /// </summary>
        [LoadColumn(2)]
        public float Label { get; set; }
    }

    /// <summary>
    /// ML.NET 矩阵分解预测结果
    /// </summary>
    public class BookRatingPrediction
    {
        /// <summary>
        /// 预测评分
        /// </summary>
        public float Score { get; set; }
    }

    /// <summary>
    /// ML.NET 矩阵分解训练配置
    /// </summary>
    public class MatrixFactorizationConfig
    {
        /// <summary>
        /// 隐因子数量（潜在特征维度）
        /// 较高值可捕获更复杂的模式，但训练更慢
        /// </summary>
        public int NumberOfIterations { get; set; } = 20;

        /// <summary>
        /// 近似秩（矩阵分解的秩）
        /// </summary>
        public int ApproximationRank { get; set; } = 8;

        /// <summary>
        /// 学习率
        /// </summary>
        public double LearningRate { get; set; } = 0.1;

        /// <summary>
        /// 是否使用隐式反馈模式
        /// true: 适用于借阅记录（无明确评分）
        /// false: 适用于有明确评分的场景
        /// </summary>
        public bool UseImplicitPreference { get; set; } = true;

        /// <summary>
        /// 置信度系数（仅隐式反馈模式有效）
        /// </summary>
        public double Alpha { get; set; } = 0.01;

        /// <summary>
        /// 正则化系数
        /// </summary>
        public double Lambda { get; set; } = 0.1;

        /// <summary>
        /// 历史数据天数
        /// </summary>
        public int HistoryDays { get; set; } = 365;

        /// <summary>
        /// 模型保存路径（可选）
        /// </summary>
        public string ModelPath { get; set; }

        /// <summary>
        /// 返回推荐数量
        /// </summary>
        public int TopN { get; set; } = 10;
    }

    /// <summary>
    /// ML.NET 模型训练结果
    /// </summary>
    public class MatrixFactorizationTrainingResult
    {
        /// <summary>
        /// 是否训练成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 训练耗时（毫秒）
        /// </summary>
        public long TrainingTimeMs { get; set; }

        /// <summary>
        /// 训练数据条数
        /// </summary>
        public int TrainingDataCount { get; set; }

        /// <summary>
        /// 唯一用户数
        /// </summary>
        public int UniqueUserCount { get; set; }

        /// <summary>
        /// 唯一书目数
        /// </summary>
        public int UniqueBookCount { get; set; }

        /// <summary>
        /// 评估指标：均方根误差
        /// </summary>
        public double? RootMeanSquaredError { get; set; }

        /// <summary>
        /// 评估指标：R平方
        /// </summary>
        public double? RSquared { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 模型文件路径
        /// </summary>
        public string ModelFilePath { get; set; }
    }
}
