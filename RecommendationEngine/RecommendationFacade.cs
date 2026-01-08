using System;
using System.Collections.Generic;
using RecommendationEngine.Data;
using RecommendationEngine.Models;
using RecommendationEngine.Services;

namespace RecommendationEngine
{
    /// <summary>
    /// 推荐引擎统一入口（门面模式）
    /// 整合热门榜、相似书推荐、个性化推荐三大功能
    /// </summary>
    public class RecommendationFacade
    {
        private readonly RecommendationRepository _repository;
        private readonly TrendingService _trendingService;
        private readonly ContentBasedService _contentBasedService;
        private readonly PersonalizedService _personalizedService;
        private readonly SimilarityCalculationService _similarityService;

        /// <summary>
        /// 初始化推荐引擎
        /// </summary>
        /// <param name="connectionString">数据库连接字符串（可选，为空则从配置读取）</param>
        public RecommendationFacade(string connectionString = null)
        {
            _repository = new RecommendationRepository(connectionString);
            _trendingService = new TrendingService(_repository);
            _contentBasedService = new ContentBasedService(_repository);
            _personalizedService = new PersonalizedService(_repository);
            _similarityService = new SimilarityCalculationService(_repository);
        }

        /// <summary>
        /// 使用已有的Repository初始化
        /// </summary>
        /// <param name="repository">数据访问层</param>
        public RecommendationFacade(RecommendationRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            _repository = repository;
            _trendingService = new TrendingService(repository);
            _contentBasedService = new ContentBasedService(repository);
            _personalizedService = new PersonalizedService(repository);
            _similarityService = new SimilarityCalculationService(repository);
        }

        #region 热门榜 (Trending/Popular)

        /// <summary>
        /// 获取本周热门榜
        /// </summary>
        /// <param name="topN">返回数量，默认20</param>
        /// <param name="categoryFilter">分类过滤（可选）</param>
        public List<RecommendationResult> GetWeeklyTrending(int topN = 20, string categoryFilter = null)
        {
            return _trendingService.GetWeeklyTrending(topN, categoryFilter);
        }

        /// <summary>
        /// 获取本月热门榜
        /// </summary>
        /// <param name="topN">返回数量，默认20</param>
        /// <param name="categoryFilter">分类过滤（可选）</param>
        public List<RecommendationResult> GetMonthlyTrending(int topN = 20, string categoryFilter = null)
        {
            return _trendingService.GetMonthlyTrending(topN, categoryFilter);
        }

        /// <summary>
        /// 获取自定义热门榜
        /// </summary>
        /// <param name="config">热门榜配置</param>
        public List<RecommendationResult> GetTrending(TrendingConfig config)
        {
            return _trendingService.GetTrending(config);
        }

        #endregion

        #region 相似书推荐 (Because you viewed/borrowed X)

        /// <summary>
        /// 获取相似书推荐（综合：协同过滤 + 同作者 + 同分类）
        /// </summary>
        /// <param name="bibliographyId">书目ID</param>
        /// <param name="topN">返回数量，默认10</param>
        public List<RecommendationResult> GetSimilarBooks(int bibliographyId, int topN = 10)
        {
            return _contentBasedService.GetSimilarBooks(bibliographyId, topN);
        }

        /// <summary>
        /// 获取"借过此书的人还借了"推荐
        /// </summary>
        /// <param name="bibliographyId">书目ID</param>
        /// <param name="topN">返回数量，默认10</param>
        public List<RecommendationResult> GetAlsoBorrowed(int bibliographyId, int topN = 10)
        {
            return _contentBasedService.GetAlsoBorrowed(bibliographyId, topN);
        }

        /// <summary>
        /// 获取同作者其他作品
        /// </summary>
        /// <param name="bibliographyId">书目ID</param>
        /// <param name="topN">返回数量，默认10</param>
        public List<RecommendationResult> GetSameAuthorBooks(int bibliographyId, int topN = 10)
        {
            return _contentBasedService.GetSameAuthorBooks(bibliographyId, topN);
        }

        /// <summary>
        /// 获取同分类热门书
        /// </summary>
        /// <param name="bibliographyId">书目ID</param>
        /// <param name="topN">返回数量，默认10</param>
        public List<RecommendationResult> GetSameCategoryBooks(int bibliographyId, int topN = 10)
        {
            return _contentBasedService.GetSameCategoryBooks(bibliographyId, topN);
        }

        /// <summary>
        /// 设置是否使用预计算相似度
        /// </summary>
        public bool UsePrecomputedSimilarity
        {
            get { return _contentBasedService.UsePrecomputedSimilarity; }
            set { _contentBasedService.UsePrecomputedSimilarity = value; }
        }

        /// <summary>
        /// 检查指定书目是否有预计算相似度数据
        /// </summary>
        public bool HasPrecomputedSimilarity(int bibliographyId)
        {
            return _contentBasedService.HasPrecomputedData(bibliographyId);
        }

        #endregion

        #region 个性化推荐 (For You)

        /// <summary>
        /// 获取个性化推荐
        /// </summary>
        /// <param name="cardId">读者卡号</param>
        /// <param name="topN">返回数量，默认10</param>
        public List<RecommendationResult> GetForYou(string cardId, int topN = 10)
        {
            return _personalizedService.GetPersonalizedRecommendations(cardId, topN);
        }

        /// <summary>
        /// 获取个性化推荐（自定义配置）
        /// </summary>
        /// <param name="cardId">读者卡号</param>
        /// <param name="config">配置</param>
        public List<RecommendationResult> GetForYou(string cardId, PersonalizedConfig config)
        {
            return _personalizedService.GetPersonalizedRecommendations(cardId, config);
        }

        /// <summary>
        /// 获取用户画像
        /// </summary>
        /// <param name="cardId">读者卡号</param>
        public UserProfile GetUserProfile(string cardId)
        {
            return _personalizedService.GetUserProfile(cardId);
        }

        /// <summary>
        /// 检查用户是否有足够的历史数据进行个性化推荐
        /// </summary>
        /// <param name="cardId">读者卡号</param>
        /// <param name="minBorrowCount">最小借阅数量，默认3</param>
        public bool HasSufficientHistory(string cardId, int minBorrowCount = 3)
        {
            return _personalizedService.HasSufficientHistory(cardId, minBorrowCount);
        }

        #endregion

        #region 预计算相似度矩阵

        /// <summary>
        /// 计算协同过滤相似度矩阵
        /// </summary>
        /// <param name="minBorrowers">最少借阅人数阈值</param>
        /// <param name="topSimilar">每本书保留的最相似书籍数量</param>
        /// <param name="minSimilarity">最低相似度阈值</param>
        /// <param name="progressHandler">进度回调</param>
        public SimilarityCalculationResult CalculateCollaborativeSimilarity(
            int minBorrowers = 2,
            int topSimilar = 20,
            double minSimilarity = 0.05,
            EventHandler<SimilarityProgressEventArgs> progressHandler = null)
        {
            if (progressHandler != null)
            {
                _similarityService.ProgressChanged += progressHandler;
            }

            try
            {
                return _similarityService.CalculateCollaborativeSimilarity(minBorrowers, topSimilar, minSimilarity);
            }
            finally
            {
                if (progressHandler != null)
                {
                    _similarityService.ProgressChanged -= progressHandler;
                }
            }
        }

        /// <summary>
        /// 计算内容相似度矩阵
        /// </summary>
        /// <param name="categoryWeight">分类权重</param>
        /// <param name="authorWeight">作者权重</param>
        /// <param name="topSimilar">每本书保留的最相似书籍数量</param>
        /// <param name="minSimilarity">最低相似度阈值</param>
        /// <param name="progressHandler">进度回调</param>
        public SimilarityCalculationResult CalculateContentSimilarity(
            double categoryWeight = 0.4,
            double authorWeight = 0.6,
            int topSimilar = 20,
            double minSimilarity = 0.1,
            EventHandler<SimilarityProgressEventArgs> progressHandler = null)
        {
            if (progressHandler != null)
            {
                _similarityService.ProgressChanged += progressHandler;
            }

            try
            {
                return _similarityService.CalculateContentSimilarity(categoryWeight, authorWeight, topSimilar, minSimilarity);
            }
            finally
            {
                if (progressHandler != null)
                {
                    _similarityService.ProgressChanged -= progressHandler;
                }
            }
        }

        /// <summary>
        /// 计算混合相似度矩阵
        /// </summary>
        /// <param name="collaborativeWeight">协同过滤权重</param>
        /// <param name="contentWeight">内容相似度权重</param>
        /// <param name="topSimilar">每本书保留的最相似书籍数量</param>
        /// <param name="minSimilarity">最低相似度阈值</param>
        /// <param name="progressHandler">进度回调</param>
        public SimilarityCalculationResult CalculateHybridSimilarity(
            double collaborativeWeight = 0.6,
            double contentWeight = 0.4,
            int topSimilar = 20,
            double minSimilarity = 0.1,
            EventHandler<SimilarityProgressEventArgs> progressHandler = null)
        {
            if (progressHandler != null)
            {
                _similarityService.ProgressChanged += progressHandler;
            }

            try
            {
                return _similarityService.CalculateHybridSimilarity(collaborativeWeight, contentWeight, topSimilar, minSimilarity);
            }
            finally
            {
                if (progressHandler != null)
                {
                    _similarityService.ProgressChanged -= progressHandler;
                }
            }
        }

        /// <summary>
        /// 执行全量相似度计算（三种类型）
        /// </summary>
        /// <param name="progressHandler">进度回调</param>
        public List<SimilarityCalculationResult> CalculateAllSimilarities(
            EventHandler<SimilarityProgressEventArgs> progressHandler = null)
        {
            if (progressHandler != null)
            {
                _similarityService.ProgressChanged += progressHandler;
            }

            try
            {
                return _similarityService.CalculateAllSimilarities();
            }
            finally
            {
                if (progressHandler != null)
                {
                    _similarityService.ProgressChanged -= progressHandler;
                }
            }
        }

        /// <summary>
        /// 获取相似度计算统计信息
        /// </summary>
        public SimilarityStatistics GetSimilarityStatistics()
        {
            return _similarityService.GetStatistics();
        }

        /// <summary>
        /// 清除所有预计算相似度数据
        /// </summary>
        public int ClearAllSimilarities()
        {
            return _similarityService.ClearAll();
        }

        #endregion

        #region 缓存管理

        /// <summary>
        /// 清除所有缓存
        /// </summary>
        public void ClearAllCache()
        {
            _trendingService.ClearCache();
            _contentBasedService.ClearCache();
            _personalizedService.ClearCache();
        }

        /// <summary>
        /// 清除热门榜缓存
        /// </summary>
        public void ClearTrendingCache()
        {
            _trendingService.ClearCache();
        }

        /// <summary>
        /// 清除相似书推荐缓存
        /// </summary>
        public void ClearSimilarBooksCache()
        {
            _contentBasedService.ClearCache();
        }

        /// <summary>
        /// 清除用户个性化推荐缓存
        /// </summary>
        /// <param name="cardId">读者卡号，为空则清除所有用户缓存</param>
        public void ClearPersonalizedCache(string cardId = null)
        {
            _personalizedService.ClearCache(cardId);
        }

        #endregion
    }
}
