using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using RecommendationEngine.Data;
using RecommendationEngine.Models;

namespace RecommendationEngine.Services
{
    /// <summary>
    /// 基于内容的推荐服务
    /// 提供相似书推荐（同分类、同作者、协同过滤）
    /// 支持使用预计算相似度矩阵加速查询
    /// </summary>
    public class ContentBasedService
    {
        private readonly RecommendationRepository _repository;
        private readonly MemoryCache _cache;
        private readonly TimeSpan _cacheExpiration;

        /// <summary>
        /// 是否优先使用预计算相似度
        /// </summary>
        public bool UsePrecomputedSimilarity { get; set; } = true;

        /// <summary>
        /// 预计算相似度的首选类型
        /// </summary>
        public SimilarityType PreferredSimilarityType { get; set; } = SimilarityType.Hybrid;

        /// <summary>
        /// 初始化相似书推荐服务
        /// </summary>
        /// <param name="repository">数据访问层</param>
        /// <param name="cacheExpirationMinutes">缓存过期时间（分钟）</param>
        public ContentBasedService(RecommendationRepository repository, int cacheExpirationMinutes = 60)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _cache = MemoryCache.Default;
            _cacheExpiration = TimeSpan.FromMinutes(cacheExpirationMinutes);
        }

        /// <summary>
        /// 获取相似书推荐（综合）
        /// 优先使用预计算相似度，若不存在则实时计算
        /// </summary>
        /// <param name="bibliographyId">书目ID</param>
        /// <param name="topN">返回数量</param>
        public List<RecommendationResult> GetSimilarBooks(int bibliographyId, int topN = 10)
        {
            string cacheKey = string.Format("similar_{0}_{1}", bibliographyId, topN);

            var cached = _cache.Get(cacheKey) as List<RecommendationResult>;
            if (cached != null)
            {
                return cached;
            }

            List<RecommendationResult> results;

            // 尝试使用预计算相似度
            if (UsePrecomputedSimilarity && TryGetFromPrecomputed(bibliographyId, topN, out results))
            {
                _cache.Set(cacheKey, results, DateTimeOffset.Now.Add(_cacheExpiration));
                return results;
            }

            // 回退到实时计算
            results = CalculateSimilarBooksRealtime(bibliographyId, topN);

            _cache.Set(cacheKey, results, DateTimeOffset.Now.Add(_cacheExpiration));

            return results;
        }

        /// <summary>
        /// 尝试从预计算相似度获取结果
        /// </summary>
        private bool TryGetFromPrecomputed(int bibliographyId, int topN, out List<RecommendationResult> results)
        {
            results = null;

            // 按优先级尝试不同类型的预计算相似度
            var typesToTry = new[] { PreferredSimilarityType };
            if (PreferredSimilarityType == SimilarityType.Hybrid)
            {
                typesToTry = new[] { SimilarityType.Hybrid, SimilarityType.Collaborative, SimilarityType.ContentBased };
            }

            foreach (var type in typesToTry)
            {
                if (_repository.HasPrecomputedSimilarities(bibliographyId, type))
                {
                    var similarities = _repository.GetPrecomputedSimilarities(bibliographyId, type, topN);
                    if (similarities.Count > 0)
                    {
                        results = _repository.GetBookDetailsBySimilarities(similarities);

                        // 更新推荐理由
                        string typeDesc = GetSimilarityTypeDescription(type);
                        foreach (var item in results)
                        {
                            item.Reason = string.Format("{0} (相似度: {1:P0})", typeDesc, item.Score);
                        }

                        return true;
                    }
                }
            }

            return false;
        }

        private string GetSimilarityTypeDescription(SimilarityType type)
        {
            switch (type)
            {
                case SimilarityType.Collaborative:
                    return "借阅行为相似";
                case SimilarityType.ContentBased:
                    return "内容特征相似";
                case SimilarityType.Hybrid:
                    return "综合相似推荐";
                default:
                    return "相似推荐";
            }
        }

        /// <summary>
        /// 实时计算相似书（原有逻辑）
        /// </summary>
        private List<RecommendationResult> CalculateSimilarBooksRealtime(int bibliographyId, int topN)
        {
            var results = new List<RecommendationResult>();

            // 1. 获取协同过滤结果（借过此书的人还借了什么）- 权重最高
            var alsoBorrowed = _repository.GetAlsoBorrowedBooks(bibliographyId, topN);
            foreach (var item in alsoBorrowed)
            {
                item.Score = 0.5; // 基础分0.5，协同过滤权重高
            }
            results.AddRange(alsoBorrowed);

            // 2. 获取同作者的书 - 中等权重
            var sameAuthor = _repository.GetBooksBySameAuthor(bibliographyId, topN);
            foreach (var item in sameAuthor)
            {
                var existing = results.FirstOrDefault(r => r.BibliographyId == item.BibliographyId);
                if (existing != null)
                {
                    existing.Score += 0.3;
                    existing.Reason = "同作者且借过此书的读者也借了";
                }
                else
                {
                    item.Score = 0.3;
                    results.Add(item);
                }
            }

            // 3. 获取同分类的书 - 较低权重
            int? categoryId = _repository.GetBookCategoryId(bibliographyId);
            if (categoryId.HasValue)
            {
                var sameCategory = _repository.GetBooksByCategory(bibliographyId, categoryId.Value, topN);
                foreach (var item in sameCategory)
                {
                    var existing = results.FirstOrDefault(r => r.BibliographyId == item.BibliographyId);
                    if (existing != null)
                    {
                        existing.Score += 0.2;
                    }
                    else
                    {
                        item.Score = 0.2;
                        results.Add(item);
                    }
                }
            }

            // 按分数排序并取TopN
            results = results
                .OrderByDescending(r => r.Score)
                .ThenByDescending(r => r.BorrowCount)
                .Take(topN)
                .ToList();

            // 归一化分数
            NormalizeScores(results);

            return results;
        }

        /// <summary>
        /// 强制使用实时计算获取相似书
        /// </summary>
        public List<RecommendationResult> GetSimilarBooksRealtime(int bibliographyId, int topN = 10)
        {
            return CalculateSimilarBooksRealtime(bibliographyId, topN);
        }

        /// <summary>
        /// 获取"借过此书的人还借了"推荐
        /// </summary>
        /// <param name="bibliographyId">书目ID</param>
        /// <param name="topN">返回数量</param>
        public List<RecommendationResult> GetAlsoBorrowed(int bibliographyId, int topN = 10)
        {
            string cacheKey = string.Format("alsoBorrowed_{0}_{1}", bibliographyId, topN);

            var cached = _cache.Get(cacheKey) as List<RecommendationResult>;
            if (cached != null)
            {
                return cached;
            }

            List<RecommendationResult> results;

            // 尝试使用预计算的协同过滤相似度
            if (UsePrecomputedSimilarity && 
                _repository.HasPrecomputedSimilarities(bibliographyId, SimilarityType.Collaborative))
            {
                var similarities = _repository.GetPrecomputedSimilarities(
                    bibliographyId, SimilarityType.Collaborative, topN);
                results = _repository.GetBookDetailsBySimilarities(similarities);

                foreach (var item in results)
                {
                    item.Reason = string.Format("借过此书的读者也借了这本 (相似度: {0:P0})", item.Score);
                }
            }
            else
            {
                results = _repository.GetAlsoBorrowedBooks(bibliographyId, topN);
                NormalizeScoresByBorrowCount(results);
            }

            _cache.Set(cacheKey, results, DateTimeOffset.Now.Add(_cacheExpiration));

            return results;
        }

        /// <summary>
        /// 获取同作者其他作品
        /// </summary>
        /// <param name="bibliographyId">书目ID</param>
        /// <param name="topN">返回数量</param>
        public List<RecommendationResult> GetSameAuthorBooks(int bibliographyId, int topN = 10)
        {
            string cacheKey = string.Format("sameAuthor_{0}_{1}", bibliographyId, topN);

            var cached = _cache.Get(cacheKey) as List<RecommendationResult>;
            if (cached != null)
            {
                return cached;
            }

            var results = _repository.GetBooksBySameAuthor(bibliographyId, topN);

            for (int i = 0; i < results.Count; i++)
            {
                results[i].Score = 1.0 - (i * 0.1);
            }

            _cache.Set(cacheKey, results, DateTimeOffset.Now.Add(_cacheExpiration));

            return results;
        }

        /// <summary>
        /// 获取同分类热门书
        /// </summary>
        /// <param name="bibliographyId">书目ID</param>
        /// <param name="topN">返回数量</param>
        public List<RecommendationResult> GetSameCategoryBooks(int bibliographyId, int topN = 10)
        {
            string cacheKey = string.Format("sameCategory_{0}_{1}", bibliographyId, topN);

            var cached = _cache.Get(cacheKey) as List<RecommendationResult>;
            if (cached != null)
            {
                return cached;
            }

            int? categoryId = _repository.GetBookCategoryId(bibliographyId);
            if (!categoryId.HasValue)
            {
                return new List<RecommendationResult>();
            }

            var results = _repository.GetBooksByCategory(bibliographyId, categoryId.Value, topN);

            NormalizeScoresByBorrowCount(results);

            _cache.Set(cacheKey, results, DateTimeOffset.Now.Add(_cacheExpiration));

            return results;
        }

        /// <summary>
        /// 检查是否存在预计算相似度数据
        /// </summary>
        public bool HasPrecomputedData(int bibliographyId)
        {
            return _repository.HasPrecomputedSimilarities(bibliographyId, SimilarityType.Hybrid) ||
                   _repository.HasPrecomputedSimilarities(bibliographyId, SimilarityType.Collaborative) ||
                   _repository.HasPrecomputedSimilarities(bibliographyId, SimilarityType.ContentBased);
        }

        /// <summary>
        /// 归一化分数
        /// </summary>
        private void NormalizeScores(List<RecommendationResult> results)
        {
            if (results.Count == 0)
            {
                return;
            }

            double maxScore = results.Max(r => r.Score);
            if (maxScore > 0)
            {
                foreach (var result in results)
                {
                    result.Score = result.Score / maxScore;
                }
            }
        }

        /// <summary>
        /// 根据借阅量归一化分数
        /// </summary>
        private void NormalizeScoresByBorrowCount(List<RecommendationResult> results)
        {
            if (results.Count == 0)
            {
                return;
            }

            int maxBorrowCount = results.Max(r => r.BorrowCount);
            if (maxBorrowCount == 0)
            {
                maxBorrowCount = 1;
            }

            foreach (var result in results)
            {
                result.Score = (double)result.BorrowCount / maxBorrowCount;
            }
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        public void ClearCache()
        {
            var prefixes = new[] { "similar_", "alsoBorrowed_", "sameAuthor_", "sameCategory_" };

            var cacheKeys = _cache
                .Where(kvp => prefixes.Any(p => kvp.Key.StartsWith(p)))
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var key in cacheKeys)
            {
                _cache.Remove(key);
            }
        }
    }
}
