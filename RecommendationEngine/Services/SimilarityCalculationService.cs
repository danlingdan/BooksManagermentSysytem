using System;
using System.Collections.Generic;
using System.Linq;
using RecommendationEngine.Data;
using RecommendationEngine.Models;

namespace RecommendationEngine.Services
{
    /// <summary>
    /// 相似度预计算服务
    /// 用于离线计算图书间的相似度矩阵，存入数据库以加速推荐查询
    /// </summary>
    public class SimilarityCalculationService
    {
        private readonly RecommendationRepository _repository;

        /// <summary>
        /// 计算进度事件
        /// </summary>
        public event EventHandler<SimilarityProgressEventArgs> ProgressChanged;

        /// <summary>
        /// 初始化相似度计算服务
        /// </summary>
        public SimilarityCalculationService(string connectionString = null)
        {
            _repository = new RecommendationRepository(connectionString);
        }

        /// <summary>
        /// 初始化相似度计算服务
        /// </summary>
        public SimilarityCalculationService(RecommendationRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        #region 协同过滤相似度计算

        /// <summary>
        /// 计算基于协同过滤的相似度矩阵
        /// 使用Jaccard相似度：|A∩B| / |A∪B|
        /// </summary>
        /// <param name="minBorrowers">最少借阅人数阈值，低于此值的书目不参与计算</param>
        /// <param name="topSimilar">每本书保留的最相似书籍数量</param>
        /// <param name="minSimilarity">最低相似度阈值</param>
        /// <returns>计算结果统计</returns>
        public SimilarityCalculationResult CalculateCollaborativeSimilarity(
            int minBorrowers = 2,
            int topSimilar = 20,
            double minSimilarity = 0.05)
        {
            var result = new SimilarityCalculationResult
            {
                Type = SimilarityType.Collaborative,
                StartTime = DateTime.Now
            };

            try
            {
                OnProgressChanged("正在加载借阅数据...", 0);

                // 获取书目-借阅者映射
                var borrowerMap = _repository.GetBibliographyBorrowerMap();
                
                // 过滤掉借阅人数太少的书目
                var validBooks = borrowerMap
                    .Where(kv => kv.Value.Count >= minBorrowers)
                    .ToDictionary(kv => kv.Key, kv => kv.Value);

                var bookIds = validBooks.Keys.ToList();
                result.TotalBooks = bookIds.Count;

                OnProgressChanged(string.Format("共{0}本书参与计算...", bookIds.Count), 5);

                var allSimilarities = new List<BookSimilarity>();
                int processed = 0;

                // 计算每对书籍的Jaccard相似度
                for (int i = 0; i < bookIds.Count; i++)
                {
                    int bookA = bookIds[i];
                    var borrowersA = validBooks[bookA];

                    var bookSimilarities = new List<BookSimilarity>();

                    for (int j = 0; j < bookIds.Count; j++)
                    {
                        if (i == j) continue;

                        int bookB = bookIds[j];
                        var borrowersB = validBooks[bookB];

                        // 计算Jaccard相似度
                        int intersection = borrowersA.Intersect(borrowersB).Count();
                        if (intersection == 0) continue;

                        int union = borrowersA.Union(borrowersB).Count();
                        double similarity = (double)intersection / union;

                        if (similarity >= minSimilarity)
                        {
                            bookSimilarities.Add(new BookSimilarity
                            {
                                SourceBibliographyId = bookA,
                                TargetBibliographyId = bookB,
                                SimilarityScore = similarity,
                                Type = SimilarityType.Collaborative,
                                CalculatedTime = DateTime.Now
                            });
                        }
                    }

                    // 只保留Top N相似的
                    var topSimilarities = bookSimilarities
                        .OrderByDescending(s => s.SimilarityScore)
                        .Take(topSimilar)
                        .ToList();

                    allSimilarities.AddRange(topSimilarities);

                    processed++;
                    if (processed % 50 == 0 || processed == bookIds.Count)
                    {
                        int progress = 5 + (int)(85.0 * processed / bookIds.Count);
                        OnProgressChanged(
                            string.Format("计算进度: {0}/{1}", processed, bookIds.Count), 
                            progress);
                    }
                }

                OnProgressChanged("正在保存相似度数据...", 90);

                // 清除旧数据
                _repository.ClearSimilarities(SimilarityType.Collaborative);

                // 保存新数据
                result.SimilaritiesCalculated = allSimilarities.Count;
                _repository.SaveSimilarities(allSimilarities);

                result.EndTime = DateTime.Now;
                result.Success = true;

                OnProgressChanged("协同过滤相似度计算完成！", 100);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.EndTime = DateTime.Now;
            }

            return result;
        }

        #endregion

        #region 内容相似度计算

        /// <summary>
        /// 计算基于内容的相似度矩阵
        /// 综合分类和作者信息
        /// </summary>
        /// <param name="categoryWeight">分类权重（0-1）</param>
        /// <param name="authorWeight">作者权重（0-1）</param>
        /// <param name="topSimilar">每本书保留的最相似书籍数量</param>
        /// <param name="minSimilarity">最低相似度阈值</param>
        public SimilarityCalculationResult CalculateContentSimilarity(
            double categoryWeight = 0.4,
            double authorWeight = 0.6,
            int topSimilar = 20,
            double minSimilarity = 0.1)
        {
            var result = new SimilarityCalculationResult
            {
                Type = SimilarityType.ContentBased,
                StartTime = DateTime.Now
            };

            try
            {
                OnProgressChanged("正在加载书目内容数据...", 0);

                // 获取书目-内容映射 (categoryId, authorIds)
                var contentMap = _repository.GetBibliographyContentMap();
                var bookIds = contentMap.Keys.ToList();

                result.TotalBooks = bookIds.Count;

                OnProgressChanged(string.Format("共{0}本书参与计算...", bookIds.Count), 5);

                var allSimilarities = new List<BookSimilarity>();
                int processed = 0;

                for (int i = 0; i < bookIds.Count; i++)
                {
                    int bookA = bookIds[i];
                    var contentA = contentMap[bookA];

                    var bookSimilarities = new List<BookSimilarity>();

                    for (int j = 0; j < bookIds.Count; j++)
                    {
                        if (i == j) continue;

                        int bookB = bookIds[j];
                        var contentB = contentMap[bookB];

                        double similarity = CalculateContentSimilarityScore(
                            contentA, contentB, categoryWeight, authorWeight);

                        if (similarity >= minSimilarity)
                        {
                            bookSimilarities.Add(new BookSimilarity
                            {
                                SourceBibliographyId = bookA,
                                TargetBibliographyId = bookB,
                                SimilarityScore = similarity,
                                Type = SimilarityType.ContentBased,
                                CalculatedTime = DateTime.Now
                            });
                        }
                    }

                    // 只保留Top N
                    var topSimilarities = bookSimilarities
                        .OrderByDescending(s => s.SimilarityScore)
                        .Take(topSimilar)
                        .ToList();

                    allSimilarities.AddRange(topSimilarities);

                    processed++;
                    if (processed % 50 == 0 || processed == bookIds.Count)
                    {
                        int progress = 5 + (int)(85.0 * processed / bookIds.Count);
                        OnProgressChanged(
                            string.Format("计算进度: {0}/{1}", processed, bookIds.Count), 
                            progress);
                    }
                }

                OnProgressChanged("正在保存相似度数据...", 90);

                _repository.ClearSimilarities(SimilarityType.ContentBased);
                result.SimilaritiesCalculated = allSimilarities.Count;
                _repository.SaveSimilarities(allSimilarities);

                result.EndTime = DateTime.Now;
                result.Success = true;

                OnProgressChanged("内容相似度计算完成！", 100);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.EndTime = DateTime.Now;
            }

            return result;
        }

        private double CalculateContentSimilarityScore(
            Tuple<int?, HashSet<int>> contentA,
            Tuple<int?, HashSet<int>> contentB,
            double categoryWeight,
            double authorWeight)
        {
            double categoryScore = 0;
            double authorScore = 0;

            // 分类相似度：相同分类得1分
            if (contentA.Item1.HasValue && contentB.Item1.HasValue)
            {
                categoryScore = contentA.Item1.Value == contentB.Item1.Value ? 1.0 : 0.0;
            }

            // 作者相似度：Jaccard
            if (contentA.Item2.Count > 0 && contentB.Item2.Count > 0)
            {
                int intersection = contentA.Item2.Intersect(contentB.Item2).Count();
                int union = contentA.Item2.Union(contentB.Item2).Count();
                authorScore = union > 0 ? (double)intersection / union : 0;
            }

            // 加权求和
            double totalWeight = 0;
            double weightedSum = 0;

            if (contentA.Item1.HasValue && contentB.Item1.HasValue)
            {
                weightedSum += categoryScore * categoryWeight;
                totalWeight += categoryWeight;
            }

            if (contentA.Item2.Count > 0 || contentB.Item2.Count > 0)
            {
                weightedSum += authorScore * authorWeight;
                totalWeight += authorWeight;
            }

            return totalWeight > 0 ? weightedSum / totalWeight : 0;
        }

        #endregion

        #region 混合相似度计算

        /// <summary>
        /// 计算混合相似度矩阵
        /// 综合协同过滤和内容相似度
        /// </summary>
        /// <param name="collaborativeWeight">协同过滤权重</param>
        /// <param name="contentWeight">内容相似度权重</param>
        /// <param name="topSimilar">每本书保留的最相似书籍数量</param>
        /// <param name="minSimilarity">最低相似度阈值</param>
        public SimilarityCalculationResult CalculateHybridSimilarity(
            double collaborativeWeight = 0.6,
            double contentWeight = 0.4,
            int topSimilar = 20,
            double minSimilarity = 0.1)
        {
            var result = new SimilarityCalculationResult
            {
                Type = SimilarityType.Hybrid,
                StartTime = DateTime.Now
            };

            try
            {
                OnProgressChanged("正在加载数据...", 0);

                // 获取所有书目
                var allBookIds = _repository.GetAllBibliographyIds();
                result.TotalBooks = allBookIds.Count;

                // 获取协同过滤数据
                var borrowerMap = _repository.GetBibliographyBorrowerMap();

                // 获取内容数据
                var contentMap = _repository.GetBibliographyContentMap();

                OnProgressChanged(string.Format("共{0}本书参与计算...", allBookIds.Count), 5);

                var allSimilarities = new List<BookSimilarity>();
                int processed = 0;

                for (int i = 0; i < allBookIds.Count; i++)
                {
                    int bookA = allBookIds[i];

                    HashSet<string> borrowersA = null;
                    borrowerMap.TryGetValue(bookA, out borrowersA);

                    Tuple<int?, HashSet<int>> contentA = null;
                    contentMap.TryGetValue(bookA, out contentA);

                    var bookSimilarities = new List<BookSimilarity>();

                    for (int j = 0; j < allBookIds.Count; j++)
                    {
                        if (i == j) continue;

                        int bookB = allBookIds[j];

                        HashSet<string> borrowersB = null;
                        borrowerMap.TryGetValue(bookB, out borrowersB);

                        Tuple<int?, HashSet<int>> contentB = null;
                        contentMap.TryGetValue(bookB, out contentB);

                        // 计算协同过滤相似度
                        double collabScore = 0;
                        if (borrowersA != null && borrowersB != null && 
                            borrowersA.Count > 0 && borrowersB.Count > 0)
                        {
                            int intersection = borrowersA.Intersect(borrowersB).Count();
                            int union = borrowersA.Union(borrowersB).Count();
                            collabScore = union > 0 ? (double)intersection / union : 0;
                        }

                        // 计算内容相似度
                        double contentScore = 0;
                        if (contentA != null && contentB != null)
                        {
                            contentScore = CalculateContentSimilarityScore(contentA, contentB, 0.4, 0.6);
                        }

                        // 混合相似度
                        double hybridScore = collabScore * collaborativeWeight + 
                                            contentScore * contentWeight;

                        if (hybridScore >= minSimilarity)
                        {
                            bookSimilarities.Add(new BookSimilarity
                            {
                                SourceBibliographyId = bookA,
                                TargetBibliographyId = bookB,
                                SimilarityScore = hybridScore,
                                Type = SimilarityType.Hybrid,
                                CalculatedTime = DateTime.Now
                            });
                        }
                    }

                    var topSimilarities = bookSimilarities
                        .OrderByDescending(s => s.SimilarityScore)
                        .Take(topSimilar)
                        .ToList();

                    allSimilarities.AddRange(topSimilarities);

                    processed++;
                    if (processed % 50 == 0 || processed == allBookIds.Count)
                    {
                        int progress = 5 + (int)(85.0 * processed / allBookIds.Count);
                        OnProgressChanged(
                            string.Format("计算进度: {0}/{1}", processed, allBookIds.Count), 
                            progress);
                    }
                }

                OnProgressChanged("正在保存相似度数据...", 90);

                _repository.ClearSimilarities(SimilarityType.Hybrid);
                result.SimilaritiesCalculated = allSimilarities.Count;
                _repository.SaveSimilarities(allSimilarities);

                result.EndTime = DateTime.Now;
                result.Success = true;

                OnProgressChanged("混合相似度计算完成！", 100);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.EndTime = DateTime.Now;
            }

            return result;
        }

        #endregion

        #region 批量计算

        /// <summary>
        /// 执行全量相似度计算（三种类型）
        /// </summary>
        public List<SimilarityCalculationResult> CalculateAllSimilarities()
        {
            var results = new List<SimilarityCalculationResult>();

            OnProgressChanged("开始计算协同过滤相似度...", 0);
            results.Add(CalculateCollaborativeSimilarity());

            OnProgressChanged("开始计算内容相似度...", 33);
            results.Add(CalculateContentSimilarity());

            OnProgressChanged("开始计算混合相似度...", 66);
            results.Add(CalculateHybridSimilarity());

            OnProgressChanged("全部计算完成！", 100);

            return results;
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取相似度数据统计
        /// </summary>
        public SimilarityStatistics GetStatistics()
        {
            var stats = new SimilarityStatistics();

            stats.CollaborativeLastUpdate = _repository.GetSimilarityLastUpdateTime(SimilarityType.Collaborative);
            stats.ContentBasedLastUpdate = _repository.GetSimilarityLastUpdateTime(SimilarityType.ContentBased);
            stats.HybridLastUpdate = _repository.GetSimilarityLastUpdateTime(SimilarityType.Hybrid);

            return stats;
        }

        /// <summary>
        /// 清除所有预计算数据
        /// </summary>
        public int ClearAll()
        {
            return _repository.ClearAllSimilarities();
        }

        private void OnProgressChanged(string message, int progressPercent)
        {
            var handler = ProgressChanged;
            if (handler != null)
            {
                handler(this, new SimilarityProgressEventArgs(message, progressPercent));
            }
        }

        #endregion
    }

    #region 辅助类

    /// <summary>
    /// 相似度计算进度事件参数
    /// </summary>
    public class SimilarityProgressEventArgs : EventArgs
    {
        public string Message { get; private set; }
        public int ProgressPercent { get; private set; }

        public SimilarityProgressEventArgs(string message, int progressPercent)
        {
            Message = message;
            ProgressPercent = progressPercent;
        }
    }

    /// <summary>
    /// 相似度计算结果
    /// </summary>
    public class SimilarityCalculationResult
    {
        public SimilarityType Type { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public int TotalBooks { get; set; }
        public int SimilaritiesCalculated { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public TimeSpan Duration
        {
            get { return EndTime - StartTime; }
        }
    }

    /// <summary>
    /// 相似度统计信息
    /// </summary>
    public class SimilarityStatistics
    {
        public DateTime? CollaborativeLastUpdate { get; set; }
        public DateTime? ContentBasedLastUpdate { get; set; }
        public DateTime? HybridLastUpdate { get; set; }

        public bool HasCollaborativeData
        {
            get { return CollaborativeLastUpdate.HasValue; }
        }

        public bool HasContentBasedData
        {
            get { return ContentBasedLastUpdate.HasValue; }
        }

        public bool HasHybridData
        {
            get { return HybridLastUpdate.HasValue; }
        }
    }

    #endregion
}
