using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using RecommendationEngine.Data;
using RecommendationEngine.Models;

namespace RecommendationEngine.Services
{
    /// <summary>
    /// 热门榜推荐服务
    /// 提供基于借阅量和增长率的热门图书推荐
    /// </summary>
    public class TrendingService
    {
        private readonly RecommendationRepository _repository;
        private readonly MemoryCache _cache;
        private readonly TimeSpan _cacheExpiration;

        /// <summary>
        /// 初始化热门榜服务
        /// </summary>
        /// <param name="repository">数据访问层</param>
        /// <param name="cacheExpirationMinutes">缓存过期时间（分钟）</param>
        public TrendingService(RecommendationRepository repository, int cacheExpirationMinutes = 30)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _cache = MemoryCache.Default;
            _cacheExpiration = TimeSpan.FromMinutes(cacheExpirationMinutes);
        }

        /// <summary>
        /// 获取热门榜（7天）
        /// </summary>
        /// <param name="topN">返回数量</param>
        /// <param name="categoryFilter">分类过滤</param>
        public List<RecommendationResult> GetWeeklyTrending(int topN = 20, string categoryFilter = null)
        {
            return GetTrending(new TrendingConfig
            {
                Days = 7,
                TopN = topN,
                ConsiderGrowthRate = true,
                GrowthRateWeight = 0.3,
                CategoryFilter = categoryFilter
            });
        }

        /// <summary>
        /// 获取热门榜（30天）
        /// </summary>
        /// <param name="topN">返回数量</param>
        /// <param name="categoryFilter">分类过滤</param>
        public List<RecommendationResult> GetMonthlyTrending(int topN = 20, string categoryFilter = null)
        {
            return GetTrending(new TrendingConfig
            {
                Days = 30,
                TopN = topN,
                ConsiderGrowthRate = true,
                GrowthRateWeight = 0.2,
                CategoryFilter = categoryFilter
            });
        }

        /// <summary>
        /// 获取热门榜（自定义配置）
        /// </summary>
        /// <param name="config">热门榜配置</param>
        public List<RecommendationResult> GetTrending(TrendingConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            string cacheKey = string.Format("trending_{0}_{1}_{2}", 
                config.Days, config.TopN, config.CategoryFilter ?? "all");

            var cached = _cache.Get(cacheKey) as List<RecommendationResult>;
            if (cached != null)
            {
                return cached;
            }

            var results = _repository.GetBorrowStatistics(config.Days, config.TopN * 2, config.CategoryFilter);

            if (config.ConsiderGrowthRate && results.Count > 0)
            {
                results = CalculateScoresWithGrowthRate(results, config);
            }
            else
            {
                CalculateScoresWithoutGrowthRate(results);
            }

            results = results
                .OrderByDescending(r => r.Score)
                .Take(config.TopN)
                .ToList();

            for (int i = 0; i < results.Count; i++)
            {
                results[i].Reason = GenerateReason(results[i], i + 1, config.Days);
            }

            _cache.Set(cacheKey, results, DateTimeOffset.Now.Add(_cacheExpiration));

            return results;
        }

        /// <summary>
        /// 计算带增长率的分数
        /// </summary>
        private List<RecommendationResult> CalculateScoresWithGrowthRate(
            List<RecommendationResult> results, TrendingConfig config)
        {
            if (results.Count == 0)
            {
                return results;
            }

            int maxBorrowCount = results.Max(r => r.BorrowCount);
            if (maxBorrowCount == 0)
            {
                maxBorrowCount = 1;
            }

            foreach (var result in results)
            {
                double growthRate = _repository.GetBorrowGrowthRate(
                    result.BibliographyId, config.Days, config.Days);

                result.GrowthRate = growthRate;

                double normalizedBorrowCount = (double)result.BorrowCount / maxBorrowCount;

                double normalizedGrowthRate = Math.Max(0, Math.Min(1, (growthRate + 1) / 2));

                result.Score = normalizedBorrowCount * (1 - config.GrowthRateWeight) 
                             + normalizedGrowthRate * config.GrowthRateWeight;
            }

            return results;
        }

        /// <summary>
        /// 计算不带增长率的分数
        /// </summary>
        private void CalculateScoresWithoutGrowthRate(List<RecommendationResult> results)
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
        /// 生成推荐理由
        /// </summary>
        private string GenerateReason(RecommendationResult result, int rank, int days)
        {
            string timeRange = days <= 7 ? "本周" : "本月";

            if (result.GrowthRate > 0.5)
            {
                return string.Format("{0}热门第{1}名，借阅量飙升{2:P0}", timeRange, rank, result.GrowthRate);
            }
            else if (result.GrowthRate > 0)
            {
                return string.Format("{0}热门第{1}名，借阅量持续上涨", timeRange, rank);
            }
            else
            {
                return string.Format("{0}热门第{1}名，{2}次借阅", timeRange, rank, result.BorrowCount);
            }
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        public void ClearCache()
        {
            var cacheKeys = _cache
                .Where(kvp => kvp.Key.StartsWith("trending_"))
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var key in cacheKeys)
            {
                _cache.Remove(key);
            }
        }
    }
}
