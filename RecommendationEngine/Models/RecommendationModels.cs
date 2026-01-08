using System;
using System.Collections.Generic;

namespace RecommendationEngine.Models
{
    /// <summary>
    /// 推荐结果
    /// </summary>
    public class RecommendationResult
    {
        /// <summary>
        /// 推荐的图书ID
        /// </summary>
        public string BookId { get; set; }

        /// <summary>
        /// 书目ID
        /// </summary>
        public int BibliographyId { get; set; }

        /// <summary>
        /// 书名
        /// </summary>
        public string BookName { get; set; }

        /// <summary>
        /// ISBN
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Authors { get; set; }

        /// <summary>
        /// 分类代码
        /// </summary>
        public string CategoryCode { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 出版社
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// 推荐分数（0-1之间，越高越推荐）
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// 推荐理由
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 推荐类型
        /// </summary>
        public RecommendationType Type { get; set; }

        /// <summary>
        /// 借阅次数（用于热门榜）
        /// </summary>
        public int BorrowCount { get; set; }

        /// <summary>
        /// 增长率（用于热门榜）
        /// </summary>
        public double GrowthRate { get; set; }
    }

    /// <summary>
    /// 推荐类型枚举
    /// </summary>
    public enum RecommendationType
    {
        /// <summary>
        /// 热门榜
        /// </summary>
        Trending,

        /// <summary>
        /// 相似书推荐
        /// </summary>
        Similar,

        /// <summary>
        /// 个性化推荐
        /// </summary>
        Personalized
    }

    /// <summary>
    /// 用户行为记录
    /// </summary>
    public class UserBehavior
    {
        /// <summary>
        /// 行为ID
        /// </summary>
        public long BehaviorId { get; set; }

        /// <summary>
        /// 读者卡号
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// 书目ID
        /// </summary>
        public int BibliographyId { get; set; }

        /// <summary>
        /// 行为类型
        /// </summary>
        public BehaviorType Type { get; set; }

        /// <summary>
        /// 行为时间
        /// </summary>
        public DateTime BehaviorTime { get; set; }

        /// <summary>
        /// 评分（1-5，可选）
        /// </summary>
        public int? Rating { get; set; }

        /// <summary>
        /// 权重（用于计算推荐分数）
        /// </summary>
        public double Weight { get; set; }
    }

    /// <summary>
    /// 用户行为类型
    /// </summary>
    public enum BehaviorType
    {
        /// <summary>
        /// 浏览
        /// </summary>
        View = 1,

        /// <summary>
        /// 借阅
        /// </summary>
        Borrow = 2,

        /// <summary>
        /// 收藏
        /// </summary>
        Favorite = 3,

        /// <summary>
        /// 评分
        /// </summary>
        Rate = 4,

        /// <summary>
        /// 预约
        /// </summary>
        Reserve = 5
    }

    /// <summary>
    /// 图书相似度
    /// </summary>
    public class BookSimilarity
    {
        /// <summary>
        /// 相似度ID
        /// </summary>
        public long SimilarityId { get; set; }

        /// <summary>
        /// 源书目ID
        /// </summary>
        public int SourceBibliographyId { get; set; }

        /// <summary>
        /// 目标书目ID
        /// </summary>
        public int TargetBibliographyId { get; set; }

        /// <summary>
        /// 相似度分数（0-1）
        /// </summary>
        public double SimilarityScore { get; set; }

        /// <summary>
        /// 相似度类型
        /// </summary>
        public SimilarityType Type { get; set; }

        /// <summary>
        /// 计算时间
        /// </summary>
        public DateTime CalculatedTime { get; set; }
    }

    /// <summary>
    /// 相似度类型
    /// </summary>
    public enum SimilarityType
    {
        /// <summary>
        /// 基于内容（分类、作者等）
        /// </summary>
        ContentBased = 1,

        /// <summary>
        /// 基于协同过滤（用户行为）
        /// </summary>
        Collaborative = 2,

        /// <summary>
        /// 混合
        /// </summary>
        Hybrid = 3
    }

    /// <summary>
    /// 热门榜配置
    /// </summary>
    public class TrendingConfig
    {
        /// <summary>
        /// 统计天数
        /// </summary>
        public int Days { get; set; } = 7;

        /// <summary>
        /// 返回数量
        /// </summary>
        public int TopN { get; set; } = 20;

        /// <summary>
        /// 是否考虑增长率
        /// </summary>
        public bool ConsiderGrowthRate { get; set; } = true;

        /// <summary>
        /// 增长率权重（0-1）
        /// </summary>
        public double GrowthRateWeight { get; set; } = 0.3;

        /// <summary>
        /// 分类过滤（可选）
        /// </summary>
        public string CategoryFilter { get; set; }
    }

    /// <summary>
    /// 个性化推荐配置
    /// </summary>
    public class PersonalizedConfig
    {
        /// <summary>
        /// 返回数量
        /// </summary>
        public int TopN { get; set; } = 10;

        /// <summary>
        /// 相似用户数量
        /// </summary>
        public int SimilarUserCount { get; set; } = 20;

        /// <summary>
        /// 历史行为天数
        /// </summary>
        public int HistoryDays { get; set; } = 180;

        /// <summary>
        /// 是否排除已借阅的书
        /// </summary>
        public bool ExcludeBorrowed { get; set; } = true;

        /// <summary>
        /// 最低相似度阈值
        /// </summary>
        public double MinSimilarityThreshold { get; set; } = 0.1;
    }

    /// <summary>
    /// 用户画像
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// 读者卡号
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// 偏好分类（分类ID -> 偏好分数）
        /// </summary>
        public Dictionary<int, double> CategoryPreferences { get; set; } = new Dictionary<int, double>();

        /// <summary>
        /// 偏好作者（作者名 -> 偏好分数）
        /// </summary>
        public Dictionary<string, double> AuthorPreferences { get; set; } = new Dictionary<string, double>();

        /// <summary>
        /// 借阅的书目ID集合
        /// </summary>
        public HashSet<int> BorrowedBibliographyIds { get; set; } = new HashSet<int>();

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdated { get; set; }
    }

    /// <summary>
    /// 推荐缓存项
    /// </summary>
    public class RecommendationCache
    {
        /// <summary>
        /// 缓存键
        /// </summary>
        public string CacheKey { get; set; }

        /// <summary>
        /// 推荐类型
        /// </summary>
        public RecommendationType Type { get; set; }

        /// <summary>
        /// 关联ID（如用户ID、书目ID）
        /// </summary>
        public string RelatedId { get; set; }

        /// <summary>
        /// 推荐结果JSON
        /// </summary>
        public string ResultJson { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }
    }
}
