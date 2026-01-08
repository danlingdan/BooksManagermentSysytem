using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using RecommendationEngine.Data;
using RecommendationEngine.Models;

namespace RecommendationEngine.Services
{
    /// <summary>
    /// 个性化推荐服务
    /// 基于用户历史行为的协同过滤推荐
    /// </summary>
    public class PersonalizedService
    {
        private readonly RecommendationRepository _repository;
        private readonly MemoryCache _cache;
        private readonly TimeSpan _cacheExpiration;

        /// <summary>
        /// 初始化个性化推荐服务
        /// </summary>
        /// <param name="repository">数据访问层</param>
        /// <param name="cacheExpirationMinutes">缓存过期时间（分钟）</param>
        public PersonalizedService(RecommendationRepository repository, int cacheExpirationMinutes = 30)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _cache = MemoryCache.Default;
            _cacheExpiration = TimeSpan.FromMinutes(cacheExpirationMinutes);
        }

        /// <summary>
        /// 获取个性化推荐（For You）
        /// </summary>
        /// <param name="cardId">读者卡号</param>
        /// <param name="topN">返回数量</param>
        public List<RecommendationResult> GetPersonalizedRecommendations(string cardId, int topN = 10)
        {
            return GetPersonalizedRecommendations(cardId, new PersonalizedConfig
            {
                TopN = topN,
                SimilarUserCount = 20,
                HistoryDays = 180,
                ExcludeBorrowed = true,
                MinSimilarityThreshold = 0.1
            });
        }

        /// <summary>
        /// 获取个性化推荐（自定义配置）
        /// </summary>
        /// <param name="cardId">读者卡号</param>
        /// <param name="config">配置</param>
        public List<RecommendationResult> GetPersonalizedRecommendations(string cardId, PersonalizedConfig config)
        {
            if (string.IsNullOrEmpty(cardId))
            {
                throw new ArgumentNullException(nameof(cardId));
            }

            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            string cacheKey = string.Format("personalized_{0}_{1}", cardId, config.TopN);

            var cached = _cache.Get(cacheKey) as List<RecommendationResult>;
            if (cached != null)
            {
                return cached;
            }

            var results = new List<RecommendationResult>();

            // 获取用户已借阅的书目ID（用于排除）
            HashSet<int> borrowedIds = new HashSet<int>();
            if (config.ExcludeBorrowed)
            {
                borrowedIds = _repository.GetUserBorrowedBibliographyIds(cardId);
            }

            // 策略1：基于相似用户的协同过滤
            var collaborativeResults = GetCollaborativeFilteringResults(cardId, borrowedIds, config);
            results.AddRange(collaborativeResults);

            // 策略2：基于用户偏好分类的推荐
            if (results.Count < config.TopN)
            {
                var categoryResults = GetCategoryBasedResults(cardId, borrowedIds, config.TopN - results.Count, config.HistoryDays);
                
                foreach (var item in categoryResults)
                {
                    if (!results.Any(r => r.BibliographyId == item.BibliographyId))
                    {
                        results.Add(item);
                    }
                }
            }

            // 按分数排序
            results = results
                .OrderByDescending(r => r.Score)
                .Take(config.TopN)
                .ToList();

            // 归一化分数
            NormalizeScores(results);

            _cache.Set(cacheKey, results, DateTimeOffset.Now.Add(_cacheExpiration));

            return results;
        }

        /// <summary>
        /// 基于相似用户的协同过滤
        /// </summary>
        private List<RecommendationResult> GetCollaborativeFilteringResults(
            string cardId, HashSet<int> excludeIds, PersonalizedConfig config)
        {
            // 找到相似用户
            var similarUsers = _repository.GetSimilarUsers(cardId, config.SimilarUserCount, config.HistoryDays);

            // 过滤掉相似度太低的用户
            similarUsers = similarUsers
                .Where(u => u.Item2 >= config.MinSimilarityThreshold)
                .ToList();

            if (similarUsers.Count == 0)
            {
                return new List<RecommendationResult>();
            }

            // 获取相似用户借过但当前用户没借过的书
            var results = _repository.GetBooksFromSimilarUsers(similarUsers, excludeIds, config.TopN);

            // 计算分数（基于推荐者数量和相似度）
            foreach (var result in results)
            {
                result.Score = 0.6 + (result.BorrowCount * 0.04); // 基础分0.6，每增加一个推荐者+0.04
                result.Score = Math.Min(1.0, result.Score);
            }

            return results;
        }

        /// <summary>
        /// 基于用户偏好分类的推荐
        /// </summary>
        private List<RecommendationResult> GetCategoryBasedResults(
            string cardId, HashSet<int> excludeIds, int topN, int historyDays)
        {
            // 获取用户偏好分类
            var categoryPreferences = _repository.GetUserCategoryPreferences(cardId, historyDays);

            if (categoryPreferences.Count == 0)
            {
                return new List<RecommendationResult>();
            }

            // 获取偏好分类中的热门书
            var results = _repository.GetBooksByPreferredCategories(categoryPreferences, excludeIds, topN);

            // 计算分数
            foreach (var result in results)
            {
                result.Score = 0.4 + (result.BorrowCount * 0.02); // 基础分0.4
                result.Score = Math.Min(0.8, result.Score); // 最高0.8（低于协同过滤）
            }

            return results;
        }

        /// <summary>
        /// 获取用户画像
        /// </summary>
        /// <param name="cardId">读者卡号</param>
        /// <param name="historyDays">历史天数</param>
        public UserProfile GetUserProfile(string cardId, int historyDays = 180)
        {
            if (string.IsNullOrEmpty(cardId))
            {
                throw new ArgumentNullException(nameof(cardId));
            }

            string cacheKey = string.Format("profile_{0}", cardId);

            var cached = _cache.Get(cacheKey) as UserProfile;
            if (cached != null)
            {
                return cached;
            }

            var profile = new UserProfile
            {
                CardId = cardId,
                LastUpdated = DateTime.Now
            };

            // 获取借阅历史
            profile.BorrowedBibliographyIds = _repository.GetUserBorrowedBibliographyIds(cardId);

            // 获取分类偏好
            var categoryPrefs = _repository.GetUserCategoryPreferences(cardId, historyDays);
            int totalBorrows = categoryPrefs.Values.Sum();
            if (totalBorrows > 0)
            {
                foreach (var kvp in categoryPrefs)
                {
                    profile.CategoryPreferences[kvp.Key] = (double)kvp.Value / totalBorrows;
                }
            }

            _cache.Set(cacheKey, profile, DateTimeOffset.Now.Add(TimeSpan.FromHours(2)));

            return profile;
        }

        /// <summary>
        /// 检查用户是否有足够的历史数据进行个性化推荐
        /// </summary>
        /// <param name="cardId">读者卡号</param>
        /// <param name="minBorrowCount">最小借阅数量</param>
        public bool HasSufficientHistory(string cardId, int minBorrowCount = 3)
        {
            var borrowedIds = _repository.GetUserBorrowedBibliographyIds(cardId);
            return borrowedIds.Count >= minBorrowCount;
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
        /// 清除用户相关缓存
        /// </summary>
        /// <param name="cardId">读者卡号，为空则清除所有</param>
        public void ClearCache(string cardId = null)
        {
            var cacheKeys = _cache
                .Where(kvp => kvp.Key.StartsWith("personalized_") || kvp.Key.StartsWith("profile_"))
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
    }
}
