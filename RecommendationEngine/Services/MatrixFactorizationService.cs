using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using RecommendationEngine.Data;
using RecommendationEngine.Models;

namespace RecommendationEngine.Services
{
    /// <summary>
    /// 基于 ML.NET 矩阵分解的推荐服务
    /// 使用协同过滤算法进行个性化推荐
    /// </summary>
    public class MatrixFactorizationService
    {
        private readonly RecommendationRepository _repository;
        private readonly MLContext _mlContext;
        private readonly MemoryCache _cache;
        private readonly TimeSpan _cacheExpiration;

        private ITransformer _model;
        private PredictionEngine<BookRating, BookRatingPrediction> _predictionEngine;
        private HashSet<uint> _knownBibliographyIds;
        private Dictionary<string, uint> _userIdMapping;
        private bool _isModelTrained;

        /// <summary>
        /// 模型是否已训练
        /// </summary>
        public bool IsModelTrained => _isModelTrained;

        /// <summary>
        /// 训练进度事件
        /// </summary>
        public event EventHandler<MatrixFactorizationProgressEventArgs> ProgressChanged;

        /// <summary>
        /// 初始化 ML.NET 推荐服务
        /// </summary>
        /// <param name="repository">数据访问层</param>
        /// <param name="seed">随机种子（用于可重复性）</param>
        /// <param name="cacheExpirationMinutes">缓存过期时间（分钟）</param>
        public MatrixFactorizationService(RecommendationRepository repository, int? seed = null, int cacheExpirationMinutes = 60)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mlContext = seed.HasValue ? new MLContext(seed.Value) : new MLContext();
            _cache = MemoryCache.Default;
            _cacheExpiration = TimeSpan.FromMinutes(cacheExpirationMinutes);
            _knownBibliographyIds = new HashSet<uint>();
            _userIdMapping = new Dictionary<string, uint>();
            _isModelTrained = false;
        }

        /// <summary>
        /// 训练矩阵分解模型
        /// </summary>
        /// <param name="config">训练配置</param>
        /// <returns>训练结果</returns>
        public MatrixFactorizationTrainingResult TrainModel(MatrixFactorizationConfig config = null)
        {
            config = config ?? new MatrixFactorizationConfig();
            var result = new MatrixFactorizationTrainingResult();
            var stopwatch = Stopwatch.StartNew();

            try
            {
                ReportProgress("正在加载训练数据...", 0);

                // 获取借阅数据作为训练集
                var trainingData = _repository.GetMLTrainingData(config.HistoryDays);

                if (trainingData == null || trainingData.Count == 0)
                {
                    result.Success = false;
                    result.ErrorMessage = "没有足够的训练数据";
                    return result;
                }

                result.TrainingDataCount = trainingData.Count;
                result.UniqueUserCount = trainingData.Select(d => d.UserId).Distinct().Count();
                result.UniqueBookCount = trainingData.Select(d => d.BibliographyId).Distinct().Count();

                ReportProgress(string.Format("已加载 {0} 条借阅记录", trainingData.Count), 10);

                // 构建用户ID映射和书目ID集合
                BuildMappings(trainingData);

                // 创建 IDataView
                IDataView dataView = _mlContext.Data.LoadFromEnumerable(trainingData);

                ReportProgress("正在配置训练管道...", 20);

                // 构建训练管道
                var options = new MatrixFactorizationTrainer.Options
                {
                    MatrixColumnIndexColumnName = "UserIdEncoded",
                    MatrixRowIndexColumnName = "BibliographyIdEncoded",
                    LabelColumnName = "Label",
                    NumberOfIterations = config.NumberOfIterations,
                    ApproximationRank = config.ApproximationRank,
                    Alpha = config.Alpha,
                    Lambda = config.Lambda
                };

                // 添加特征工程：将 UserId 和 BibliographyId 编码为数值
                var pipeline = _mlContext.Transforms.Conversion.MapValueToKey(
                        inputColumnName: "UserId",
                        outputColumnName: "UserIdEncoded")
                    .Append(_mlContext.Transforms.Conversion.MapValueToKey(
                        inputColumnName: "BibliographyId",
                        outputColumnName: "BibliographyIdEncoded"))
                    .Append(_mlContext.Recommendation().Trainers.MatrixFactorization(options));

                ReportProgress("正在训练模型...", 30);

                // 训练模型
                _model = pipeline.Fit(dataView);
                _isModelTrained = true;

                ReportProgress("模型训练完成", 80);

                // 创建预测引擎
                _predictionEngine = _mlContext.Model.CreatePredictionEngine<BookRating, BookRatingPrediction>(_model);

                // 评估模型（如果有测试数据）
                if (trainingData.Count > 100)
                {
                    ReportProgress("正在评估模型...", 85);
                    var predictions = _model.Transform(dataView);
                    var metrics = _mlContext.Regression.Evaluate(predictions, labelColumnName: "Label", scoreColumnName: "Score");
                    result.RootMeanSquaredError = metrics.RootMeanSquaredError;
                    result.RSquared = metrics.RSquared;
                }

                // 保存模型（如果指定了路径）
                if (!string.IsNullOrEmpty(config.ModelPath))
                {
                    ReportProgress("正在保存模型...", 95);
                    SaveModel(config.ModelPath);
                    result.ModelFilePath = config.ModelPath;
                }

                stopwatch.Stop();
                result.TrainingTimeMs = stopwatch.ElapsedMilliseconds;
                result.Success = true;

                ReportProgress("训练完成！", 100);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.TrainingTimeMs = stopwatch.ElapsedMilliseconds;
            }

            return result;
        }

        /// <summary>
        /// 为用户获取 ML.NET 推荐
        /// </summary>
        /// <param name="cardId">读者卡号</param>
        /// <param name="topN">返回数量</param>
        /// <param name="excludeBorrowed">是否排除已借阅的书</param>
        /// <returns>推荐结果列表</returns>
        public List<RecommendationResult> GetRecommendations(string cardId, int topN = 10, bool excludeBorrowed = true)
        {
            if (string.IsNullOrEmpty(cardId))
            {
                throw new ArgumentNullException(nameof(cardId));
            }

            if (!_isModelTrained || _predictionEngine == null)
            {
                throw new InvalidOperationException("模型尚未训练，请先调用 TrainModel 方法");
            }

            string cacheKey = string.Format("ml_rec_{0}_{1}", cardId, topN);
            var cached = _cache.Get(cacheKey) as List<RecommendationResult>;
            if (cached != null)
            {
                return cached;
            }

            // 获取用户已借阅的书目ID
            HashSet<int> borrowedIds = new HashSet<int>();
            if (excludeBorrowed)
            {
                borrowedIds = _repository.GetUserBorrowedBibliographyIds(cardId);
            }

            // 为用户预测所有未借阅书籍的评分
            var predictions = new List<Tuple<uint, float>>();

            foreach (uint bibId in _knownBibliographyIds)
            {
                if (excludeBorrowed && borrowedIds.Contains((int)bibId))
                {
                    continue;
                }

                var rating = new BookRating
                {
                    UserId = cardId,
                    BibliographyId = bibId,
                    Label = 0
                };

                try
                {
                    var prediction = _predictionEngine.Predict(rating);
                    if (!float.IsNaN(prediction.Score) && prediction.Score > 0)
                    {
                        predictions.Add(Tuple.Create(bibId, prediction.Score));
                    }
                }
                catch
                {
                    // 跳过预测失败的项
                }
            }

            // 按预测分数排序，取 TopN
            var topPredictions = predictions
                .OrderByDescending(p => p.Item2)
                .Take(topN)
                .ToList();

            if (topPredictions.Count == 0)
            {
                return new List<RecommendationResult>();
            }

            // 获取书籍详情
            var bibIds = topPredictions.Select(p => (int)p.Item1).ToList();
            var results = _repository.GetBibliographyDetails(bibIds);

            // 设置分数和推荐类型
            foreach (var result in results)
            {
                var pred = topPredictions.FirstOrDefault(p => p.Item1 == (uint)result.BibliographyId);
                if (pred != null)
                {
                    result.Score = NormalizeScore(pred.Item2);
                    result.Type = RecommendationType.Personalized;
                    result.Reason = "基于机器学习的个性化推荐";
                }
            }

            // 按分数排序
            results = results.OrderByDescending(r => r.Score).ToList();

            _cache.Set(cacheKey, results, DateTimeOffset.Now.Add(_cacheExpiration));

            return results;
        }

        /// <summary>
        /// 预测用户对指定书籍的评分
        /// </summary>
        /// <param name="cardId">读者卡号</param>
        /// <param name="bibliographyId">书目ID</param>
        /// <returns>预测评分</returns>
        public float PredictRating(string cardId, int bibliographyId)
        {
            if (!_isModelTrained || _predictionEngine == null)
            {
                throw new InvalidOperationException("模型尚未训练");
            }

            var rating = new BookRating
            {
                UserId = cardId,
                BibliographyId = (uint)bibliographyId,
                Label = 0
            };

            var prediction = _predictionEngine.Predict(rating);
            return prediction.Score;
        }

        /// <summary>
        /// 保存模型到文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public void SaveModel(string filePath)
        {
            if (_model == null)
            {
                throw new InvalidOperationException("没有可保存的模型");
            }

            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            _mlContext.Model.Save(_model, null, filePath);
        }

        /// <summary>
        /// 从文件加载模型
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public bool LoadModel(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }

            try
            {
                _model = _mlContext.Model.Load(filePath, out _);
                _predictionEngine = _mlContext.Model.CreatePredictionEngine<BookRating, BookRatingPrediction>(_model);
                _isModelTrained = true;

                // 重新加载书目ID集合
                var allBibIds = _repository.GetAllBibliographyIds();
                _knownBibliographyIds = new HashSet<uint>(allBibIds.Select(id => (uint)id));

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 清除推荐缓存
        /// </summary>
        /// <param name="cardId">读者卡号，为空则清除所有</param>
        public void ClearCache(string cardId = null)
        {
            var cacheKeys = _cache
                .Where(kvp => kvp.Key.StartsWith("ml_rec_"))
                .Select(kvp => kvp.Key)
                .ToList();

            if (!string.IsNullOrEmpty(cardId))
            {
                cacheKeys = cacheKeys.Where(k => k.Contains(cardId)).ToList();
            }

            foreach (var key in cacheKeys)
            {
                _cache.Remove(key);
            }
        }

        /// <summary>
        /// 构建用户ID和书目ID映射
        /// </summary>
        private void BuildMappings(List<BookRating> trainingData)
        {
            _userIdMapping.Clear();
            _knownBibliographyIds.Clear();

            uint userIndex = 0;
            foreach (var data in trainingData)
            {
                if (!_userIdMapping.ContainsKey(data.UserId))
                {
                    _userIdMapping[data.UserId] = userIndex++;
                }

                _knownBibliographyIds.Add(data.BibliographyId);
            }
        }

        /// <summary>
        /// 归一化分数到 0-1 范围
        /// </summary>
        private double NormalizeScore(float score)
        {
            // ML.NET 矩阵分解输出可能超出 0-1 范围
            // 使用 sigmoid 函数归一化
            return 1.0 / (1.0 + Math.Exp(-score));
        }

        /// <summary>
        /// 报告进度
        /// </summary>
        private void ReportProgress(string message, int percentage)
        {
            ProgressChanged?.Invoke(this, new MatrixFactorizationProgressEventArgs
            {
                Message = message,
                ProgressPercentage = percentage
            });
        }
    }

    /// <summary>
    /// 矩阵分解进度事件参数
    /// </summary>
    public class MatrixFactorizationProgressEventArgs : EventArgs
    {
        /// <summary>
        /// 进度消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 进度百分比 (0-100)
        /// </summary>
        public int ProgressPercentage { get; set; }
    }
}
