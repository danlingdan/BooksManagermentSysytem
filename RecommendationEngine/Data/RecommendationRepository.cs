using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using RecommendationEngine.Models;

namespace RecommendationEngine.Data
{
    /// <summary>
    /// 推荐系统数据访问层
    /// 注意：表名和字段名与LibraryDB保持一致
    ///   - BIBLIOGRAPHY: 书目表
    ///   - BOOK_ITEM: 馆藏表 (item_barcode)
    ///   - bookborrow: 借阅明细表 (cardID, bookID, borrowdate)
    ///   - BOOK_CATEGORY: 分类表
    ///   - BIBLIO_AUTHOR + AUTHOR: 作者关联
    /// </summary>
    public class RecommendationRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// 初始化数据访问层
        /// </summary>
        /// <param name="connectionString">数据库连接字符串，为空则从配置读取</param>
        public RecommendationRepository(string connectionString = null)
        {
            _connectionString = connectionString ?? GetConnectionStringFromConfig()
                ?? @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LibraryDB;Integrated Security=True;MultipleActiveResultSets=True;Connection Timeout=10";
        }

        private static string GetConnectionStringFromConfig()
        {
            try
            {
                var setting = ConfigurationManager.ConnectionStrings["LibraryDB"];
                return setting?.ConnectionString;
            }
            catch
            {
                return null;
            }
        }

        #region 热门榜相关查询

        /// <summary>
        /// 获取指定时间段内的借阅统计
        /// </summary>
        public List<RecommendationResult> GetBorrowStatistics(int days, int topN, string categoryFilter = null)
        {
            var results = new List<RecommendationResult>();
            var cutoffDate = DateTime.Now.AddDays(-days);

            // 使用正确的表名和字段名：
            // - BIBLIOGRAPHY, BOOK_ITEM, bookborrow, BOOK_CATEGORY
            // - bookborrow字段: bookborrow_id, cardID, bookID, borrowdate
            // - 作者通过 BIBLIO_AUTHOR + AUTHOR 关联
            string sql = @"
                SELECT TOP (@topN)
                    b.bibliography_id,
                    b.bibliography_name,
                    b.ISBN,
                    b.publish,
                    c.category_code,
                    c.category_name,
                    COUNT(bb.bookborrow_id) AS borrow_count,
                    (SELECT STRING_AGG(a.author_name, ', ') WITHIN GROUP (ORDER BY ba.author_order)
                     FROM BIBLIO_AUTHOR ba 
                     INNER JOIN AUTHOR a ON a.author_id = ba.author_id
                     WHERE ba.bibliography_id = b.bibliography_id) AS authors
                FROM BIBLIOGRAPHY b
                INNER JOIN BOOK_ITEM bi ON bi.bibliography_id = b.bibliography_id
                INNER JOIN bookborrow bb ON bb.bookID = bi.item_barcode
                LEFT JOIN BOOK_CATEGORY c ON c.category_id = b.category_id
                WHERE bb.borrowdate >= @cutoffDate";

            if (!string.IsNullOrEmpty(categoryFilter))
            {
                sql += " AND c.category_code LIKE @categoryFilter + '%'";
            }

            sql += @"
                GROUP BY b.bibliography_id, b.bibliography_name, b.ISBN, b.publish, 
                         c.category_code, c.category_name
                ORDER BY borrow_count DESC";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@topN", topN);
                    cmd.Parameters.AddWithValue("@cutoffDate", cutoffDate);
                    if (!string.IsNullOrEmpty(categoryFilter))
                    {
                        cmd.Parameters.AddWithValue("@categoryFilter", categoryFilter);
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new RecommendationResult
                            {
                                BibliographyId = reader.GetInt32(0),
                                BookName = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                ISBN = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                Publisher = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                CategoryCode = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                CategoryName = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                BorrowCount = reader.GetInt32(6),
                                Authors = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                Type = RecommendationType.Trending
                            });
                        }
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// 获取借阅增长率
        /// </summary>
        public double GetBorrowGrowthRate(int bibliographyId, int currentDays, int previousDays)
        {
            string sql = @"
                SELECT 
                    (SELECT COUNT(*) FROM bookborrow bb 
                     INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                     WHERE bi.bibliography_id = @bibliographyId 
                       AND bb.borrowdate >= @currentStart) AS current_count,
                    (SELECT COUNT(*) FROM bookborrow bb 
                     INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                     WHERE bi.bibliography_id = @bibliographyId 
                       AND bb.borrowdate >= @previousStart 
                       AND bb.borrowdate < @currentStart) AS previous_count";

            var currentStart = DateTime.Now.AddDays(-currentDays);
            var previousStart = DateTime.Now.AddDays(-currentDays - previousDays);

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@bibliographyId", bibliographyId);
                    cmd.Parameters.AddWithValue("@currentStart", currentStart);
                    cmd.Parameters.AddWithValue("@previousStart", previousStart);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int currentCount = reader.GetInt32(0);
                            int previousCount = reader.GetInt32(1);

                            if (previousCount == 0)
                            {
                                return currentCount > 0 ? 1.0 : 0.0;
                            }

                            return (double)(currentCount - previousCount) / previousCount;
                        }
                    }
                }
            }

            return 0.0;
        }

        #endregion

        #region 相似书推荐相关查询

        /// <summary>
        /// 获取同分类的图书
        /// </summary>
        public List<RecommendationResult> GetBooksByCategory(int bibliographyId, int categoryId, int topN)
        {
            var results = new List<RecommendationResult>();

            string sql = @"
                SELECT TOP (@topN)
                    b.bibliography_id,
                    b.bibliography_name,
                    b.ISBN,
                    b.publish,
                    c.category_code,
                    c.category_name,
                    (SELECT STRING_AGG(a.author_name, ', ') WITHIN GROUP (ORDER BY ba.author_order)
                     FROM BIBLIO_AUTHOR ba 
                     INNER JOIN AUTHOR a ON a.author_id = ba.author_id
                     WHERE ba.bibliography_id = b.bibliography_id) AS authors,
                    (SELECT COUNT(*) FROM bookborrow bb 
                     INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                     WHERE bi.bibliography_id = b.bibliography_id) AS borrow_count
                FROM BIBLIOGRAPHY b
                LEFT JOIN BOOK_CATEGORY c ON c.category_id = b.category_id
                WHERE b.category_id = @categoryId AND b.bibliography_id != @bibliographyId
                ORDER BY borrow_count DESC";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@topN", topN);
                    cmd.Parameters.AddWithValue("@categoryId", categoryId);
                    cmd.Parameters.AddWithValue("@bibliographyId", bibliographyId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new RecommendationResult
                            {
                                BibliographyId = reader.GetInt32(0),
                                BookName = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                ISBN = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                Publisher = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                CategoryCode = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                CategoryName = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                Authors = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                BorrowCount = reader.GetInt32(7),
                                Type = RecommendationType.Similar,
                                Reason = "同分类图书"
                            });
                        }
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// 获取同作者的其他图书
        /// </summary>
        public List<RecommendationResult> GetBooksBySameAuthor(int bibliographyId, int topN)
        {
            var results = new List<RecommendationResult>();

            // 通过BIBLIO_AUTHOR关联查找同作者的书
            string sql = @"
                SELECT TOP (@topN)
                    b.bibliography_id,
                    b.bibliography_name,
                    b.ISBN,
                    b.publish,
                    c.category_code,
                    c.category_name,
                    (SELECT STRING_AGG(a2.author_name, ', ') WITHIN GROUP (ORDER BY ba2.author_order)
                     FROM BIBLIO_AUTHOR ba2 
                     INNER JOIN AUTHOR a2 ON a2.author_id = ba2.author_id
                     WHERE ba2.bibliography_id = b.bibliography_id) AS authors
                FROM BIBLIOGRAPHY b
                INNER JOIN BIBLIO_AUTHOR ba ON ba.bibliography_id = b.bibliography_id
                LEFT JOIN BOOK_CATEGORY c ON c.category_id = b.category_id
                WHERE ba.author_id IN (
                    SELECT author_id FROM BIBLIO_AUTHOR WHERE bibliography_id = @bibliographyId
                )
                AND b.bibliography_id != @bibliographyId
                GROUP BY b.bibliography_id, b.bibliography_name, b.ISBN, b.publish, 
                         c.category_code, c.category_name";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@topN", topN);
                    cmd.Parameters.AddWithValue("@bibliographyId", bibliographyId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new RecommendationResult
                            {
                                BibliographyId = reader.GetInt32(0),
                                BookName = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                ISBN = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                Publisher = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                CategoryCode = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                CategoryName = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                Authors = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                Type = RecommendationType.Similar,
                                Reason = "同作者作品"
                            });
                        }
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// 获取借过此书的人还借了什么（协同过滤）
        /// </summary>
        public List<RecommendationResult> GetAlsoBorrowedBooks(int bibliographyId, int topN)
        {
            var results = new List<RecommendationResult>();

            // bookborrow表字段: cardID, bookID, borrowdate
            string sql = @"
                SELECT TOP (@topN)
                    b.bibliography_id,
                    b.bibliography_name,
                    b.ISBN,
                    b.publish,
                    c.category_code,
                    c.category_name,
                    COUNT(*) AS co_borrow_count,
                    (SELECT STRING_AGG(a.author_name, ', ') WITHIN GROUP (ORDER BY ba.author_order)
                     FROM BIBLIO_AUTHOR ba 
                     INNER JOIN AUTHOR a ON a.author_id = ba.author_id
                     WHERE ba.bibliography_id = b.bibliography_id) AS authors
                FROM bookborrow bb1
                INNER JOIN BOOK_ITEM bi1 ON bb1.bookID = bi1.item_barcode
                INNER JOIN bookborrow bb2 ON bb2.cardID = bb1.cardID AND bb2.bookID != bb1.bookID
                INNER JOIN BOOK_ITEM bi2 ON bb2.bookID = bi2.item_barcode
                INNER JOIN BIBLIOGRAPHY b ON b.bibliography_id = bi2.bibliography_id
                LEFT JOIN BOOK_CATEGORY c ON c.category_id = b.category_id
                WHERE bi1.bibliography_id = @bibliographyId
                  AND bi2.bibliography_id != @bibliographyId
                GROUP BY b.bibliography_id, b.bibliography_name, b.ISBN, b.publish, 
                         c.category_code, c.category_name
                ORDER BY co_borrow_count DESC";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@topN", topN);
                    cmd.Parameters.AddWithValue("@bibliographyId", bibliographyId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int coBorrowCount = reader.GetInt32(6);
                            results.Add(new RecommendationResult
                            {
                                BibliographyId = reader.GetInt32(0),
                                BookName = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                ISBN = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                Publisher = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                CategoryCode = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                CategoryName = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                BorrowCount = coBorrowCount,
                                Authors = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                Type = RecommendationType.Similar,
                                Reason = string.Format("借过此书的{0}位读者也借了这本", coBorrowCount)
                            });
                        }
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// 获取图书的分类ID
        /// </summary>
        public int? GetBookCategoryId(int bibliographyId)
        {
            string sql = "SELECT category_id FROM BIBLIOGRAPHY WHERE bibliography_id = @bibliographyId";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@bibliographyId", bibliographyId);
                    var result = cmd.ExecuteScalar();
                    return result == DBNull.Value ? null : (int?)Convert.ToInt32(result);
                }
            }
        }

        #endregion

        #region 个性化推荐相关查询

        /// <summary>
        /// 获取用户借阅历史
        /// </summary>
        public List<UserBehavior> GetUserBorrowHistory(string cardId, int days)
        {
            var results = new List<UserBehavior>();
            var cutoffDate = DateTime.Now.AddDays(-days);

            // bookborrow: cardID, bookID, borrowdate, bookborrow_id
            string sql = @"
                SELECT 
                    bb.bookborrow_id,
                    bb.cardID,
                    bi.bibliography_id,
                    bb.borrowdate
                FROM bookborrow bb
                INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                WHERE bb.cardID = @cardId AND bb.borrowdate >= @cutoffDate
                ORDER BY bb.borrowdate DESC";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@cardId", cardId);
                    cmd.Parameters.AddWithValue("@cutoffDate", cutoffDate);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new UserBehavior
                            {
                                BehaviorId = reader.GetInt64(0),
                                CardId = reader.GetString(1),
                                BibliographyId = reader.GetInt32(2),
                                BehaviorTime = reader.GetDateTime(3),
                                Type = BehaviorType.Borrow,
                                Weight = 1.0
                            });
                        }
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// 获取用户已借阅的书目ID集合
        /// </summary>
        public HashSet<int> GetUserBorrowedBibliographyIds(string cardId)
        {
            var ids = new HashSet<int>();

            string sql = @"
                SELECT DISTINCT bi.bibliography_id
                FROM bookborrow bb
                INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                WHERE bb.cardID = @cardId";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@cardId", cardId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ids.Add(reader.GetInt32(0));
                        }
                    }
                }
            }

            return ids;
        }

        /// <summary>
        /// 获取相似用户（基于共同借阅）
        /// </summary>
        public List<Tuple<string, double>> GetSimilarUsers(string cardId, int topN, int historyDays)
        {
            var results = new List<Tuple<string, double>>();
            var cutoffDate = DateTime.Now.AddDays(-historyDays);

            string sql = @"
                WITH UserBooks AS (
                    SELECT DISTINCT bi.bibliography_id
                    FROM bookborrow bb
                    INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                    WHERE bb.cardID = @cardId AND bb.borrowdate >= @cutoffDate
                ),
                OtherUserBooks AS (
                    SELECT bb.cardID, bi.bibliography_id
                    FROM bookborrow bb
                    INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                    WHERE bb.cardID != @cardId AND bb.borrowdate >= @cutoffDate
                )
                SELECT TOP (@topN)
                    oub.cardID,
                    COUNT(DISTINCT oub.bibliography_id) * 1.0 / 
                        (SELECT COUNT(*) FROM UserBooks) AS similarity
                FROM OtherUserBooks oub
                INNER JOIN UserBooks ub ON oub.bibliography_id = ub.bibliography_id
                GROUP BY oub.cardID
                HAVING COUNT(DISTINCT oub.bibliography_id) >= 2
                ORDER BY similarity DESC";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@cardId", cardId);
                    cmd.Parameters.AddWithValue("@topN", topN);
                    cmd.Parameters.AddWithValue("@cutoffDate", cutoffDate);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(Tuple.Create(
                                reader.GetString(0),
                                reader.GetDouble(1)
                            ));
                        }
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// 获取指定用户借过但目标用户未借的书
        /// </summary>
        public List<RecommendationResult> GetBooksFromSimilarUsers(
            List<Tuple<string, double>> similarUsers, 
            HashSet<int> excludeBibliographyIds,
            int topN)
        {
            var results = new List<RecommendationResult>();

            if (similarUsers.Count == 0)
            {
                return results;
            }

            var userParams = new List<string>();
            for (int i = 0; i < similarUsers.Count; i++)
            {
                userParams.Add(string.Format("@user{0}", i));
            }

            string sql = string.Format(@"
                SELECT TOP (@topN)
                    b.bibliography_id,
                    b.bibliography_name,
                    b.ISBN,
                    b.publish,
                    c.category_code,
                    c.category_name,
                    COUNT(DISTINCT bb.cardID) AS recommender_count,
                    (SELECT STRING_AGG(a.author_name, ', ') WITHIN GROUP (ORDER BY ba.author_order)
                     FROM BIBLIO_AUTHOR ba 
                     INNER JOIN AUTHOR a ON a.author_id = ba.author_id
                     WHERE ba.bibliography_id = b.bibliography_id) AS authors
                FROM bookborrow bb
                INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                INNER JOIN BIBLIOGRAPHY b ON b.bibliography_id = bi.bibliography_id
                LEFT JOIN BOOK_CATEGORY c ON c.category_id = b.category_id
                WHERE bb.cardID IN ({0})
                GROUP BY b.bibliography_id, b.bibliography_name, b.ISBN, b.publish, 
                         c.category_code, c.category_name
                ORDER BY recommender_count DESC", string.Join(",", userParams));

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@topN", topN * 2);

                    for (int i = 0; i < similarUsers.Count; i++)
                    {
                        cmd.Parameters.AddWithValue(string.Format("@user{0}", i), similarUsers[i].Item1);
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int bibliographyId = reader.GetInt32(0);

                            if (excludeBibliographyIds.Contains(bibliographyId))
                            {
                                continue;
                            }

                            int recommenderCount = reader.GetInt32(6);
                            results.Add(new RecommendationResult
                            {
                                BibliographyId = bibliographyId,
                                BookName = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                ISBN = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                Publisher = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                CategoryCode = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                CategoryName = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                Authors = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                BorrowCount = recommenderCount,
                                Type = RecommendationType.Personalized,
                                Reason = string.Format("{0}位与您阅读喜好相似的读者也借过", recommenderCount)
                            });

                            if (results.Count >= topN)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// 获取用户偏好分类
        /// </summary>
        public Dictionary<int, int> GetUserCategoryPreferences(string cardId, int historyDays)
        {
            var preferences = new Dictionary<int, int>();
            var cutoffDate = DateTime.Now.AddDays(-historyDays);

            string sql = @"
                SELECT b.category_id, COUNT(*) AS borrow_count
                FROM bookborrow bb
                INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                INNER JOIN BIBLIOGRAPHY b ON b.bibliography_id = bi.bibliography_id
                WHERE bb.cardID = @cardId 
                  AND bb.borrowdate >= @cutoffDate
                  AND b.category_id IS NOT NULL
                GROUP BY b.category_id
                ORDER BY borrow_count DESC";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@cardId", cardId);
                    cmd.Parameters.AddWithValue("@cutoffDate", cutoffDate);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            preferences[reader.GetInt32(0)] = reader.GetInt32(1);
                        }
                    }
                }
            }

            return preferences;
        }

        /// <summary>
        /// 根据偏好分类获取推荐书籍
        /// </summary>
        public List<RecommendationResult> GetBooksByPreferredCategories(
            Dictionary<int, int> categoryPreferences,
            HashSet<int> excludeBibliographyIds,
            int topN)
        {
            var results = new List<RecommendationResult>();

            if (categoryPreferences.Count == 0)
            {
                return results;
            }

            var sortedCategories = new List<int>(categoryPreferences.Keys);
            sortedCategories.Sort((a, b) => categoryPreferences[b].CompareTo(categoryPreferences[a]));

            var topCategories = sortedCategories.GetRange(0, Math.Min(5, sortedCategories.Count));

            var categoryParams = new List<string>();
            for (int i = 0; i < topCategories.Count; i++)
            {
                categoryParams.Add(string.Format("@cat{0}", i));
            }

            string sql = string.Format(@"
                SELECT TOP (@topN)
                    b.bibliography_id,
                    b.bibliography_name,
                    b.ISBN,
                    b.publish,
                    c.category_code,
                    c.category_name,
                    (SELECT COUNT(*) FROM bookborrow bb 
                     INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                     WHERE bi.bibliography_id = b.bibliography_id) AS borrow_count,
                    (SELECT STRING_AGG(a.author_name, ', ') WITHIN GROUP (ORDER BY ba.author_order)
                     FROM BIBLIO_AUTHOR ba 
                     INNER JOIN AUTHOR a ON a.author_id = ba.author_id
                     WHERE ba.bibliography_id = b.bibliography_id) AS authors
                FROM BIBLIOGRAPHY b
                LEFT JOIN BOOK_CATEGORY c ON c.category_id = b.category_id
                WHERE b.category_id IN ({0})
                ORDER BY borrow_count DESC", string.Join(",", categoryParams));

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@topN", topN * 2);

                    for (int i = 0; i < topCategories.Count; i++)
                    {
                        cmd.Parameters.AddWithValue(string.Format("@cat{0}", i), topCategories[i]);
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int bibliographyId = reader.GetInt32(0);

                            if (excludeBibliographyIds.Contains(bibliographyId))
                            {
                                continue;
                            }

                            string categoryName = reader.IsDBNull(5) ? "" : reader.GetString(5);
                            results.Add(new RecommendationResult
                            {
                                BibliographyId = bibliographyId,
                                BookName = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                ISBN = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                Publisher = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                CategoryCode = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                CategoryName = categoryName,
                                BorrowCount = reader.GetInt32(6),
                                Authors = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                Type = RecommendationType.Personalized,
                                Reason = string.Format("基于您对「{0}」类图书的喜好", categoryName)
                            });

                            if (results.Count >= topN)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return results;
        }

        #endregion

        #region 预计算相似度矩阵相关操作

        /// <summary>
        /// 获取所有书目ID列表
        /// </summary>
        public List<int> GetAllBibliographyIds()
        {
            var ids = new List<int>();

            string sql = "SELECT bibliography_id FROM BIBLIOGRAPHY ORDER BY bibliography_id";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ids.Add(reader.GetInt32(0));
                        }
                    }
                }
            }

            return ids;
        }

        /// <summary>
        /// 获取有借阅记录的书目ID列表
        /// </summary>
        public List<int> GetBorrowedBibliographyIds()
        {
            var ids = new List<int>();

            string sql = @"
                SELECT DISTINCT bi.bibliography_id
                FROM bookborrow bb
                INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                ORDER BY bi.bibliography_id";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ids.Add(reader.GetInt32(0));
                        }
                    }
                }
            }

            return ids;
        }

        /// <summary>
        /// 获取书目的借阅用户集合（用于计算协同过滤相似度）
        /// </summary>
        public Dictionary<int, HashSet<string>> GetBibliographyBorrowerMap()
        {
            var map = new Dictionary<int, HashSet<string>>();

            string sql = @"
                SELECT bi.bibliography_id, bb.cardID
                FROM bookborrow bb
                INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int bibId = reader.GetInt32(0);
                            string cardId = reader.GetString(1);

                            if (!map.ContainsKey(bibId))
                            {
                                map[bibId] = new HashSet<string>();
                            }
                            map[bibId].Add(cardId);
                        }
                    }
                }
            }

            return map;
        }

        /// <summary>
        /// 获取书目的分类和作者信息（用于计算内容相似度）
        /// </summary>
        public Dictionary<int, Tuple<int?, HashSet<int>>> GetBibliographyContentMap()
        {
            var map = new Dictionary<int, Tuple<int?, HashSet<int>>>();

            // 先获取分类
            string sqlCategory = "SELECT bibliography_id, category_id FROM BIBLIOGRAPHY";
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sqlCategory, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int bibId = reader.GetInt32(0);
                            int? categoryId = reader.IsDBNull(1) ? null : (int?)reader.GetInt32(1);
                            map[bibId] = Tuple.Create(categoryId, new HashSet<int>());
                        }
                    }
                }
            }

            // 获取作者
            string sqlAuthor = "SELECT bibliography_id, author_id FROM BIBLIO_AUTHOR";
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sqlAuthor, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int bibId = reader.GetInt32(0);
                            int authorId = reader.GetInt32(1);

                            if (map.ContainsKey(bibId))
                            {
                                map[bibId].Item2.Add(authorId);
                            }
                        }
                    }
                }
            }

            return map;
        }

        /// <summary>
        /// 批量保存相似度数据
        /// </summary>
        public int SaveSimilarities(List<BookSimilarity> similarities)
        {
            if (similarities == null || similarities.Count == 0)
            {
                return 0;
            }

            int savedCount = 0;

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // 使用事务批量插入
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string sql = @"
                            MERGE INTO book_similarity AS target
                            USING (SELECT @source AS source_bibliography_id, 
                                          @target AS target_bibliography_id, 
                                          @type AS similarity_type) AS source
                            ON target.source_bibliography_id = source.source_bibliography_id 
                               AND target.target_bibliography_id = source.target_bibliography_id
                               AND target.similarity_type = source.similarity_type
                            WHEN MATCHED THEN
                                UPDATE SET similarity_score = @score, calculated_time = GETDATE()
                            WHEN NOT MATCHED THEN
                                INSERT (source_bibliography_id, target_bibliography_id, similarity_score, similarity_type, calculated_time)
                                VALUES (@source, @target, @score, @type, GETDATE());";

                        using (var cmd = new SqlCommand(sql, conn, transaction))
                        {
                            cmd.Parameters.Add("@source", SqlDbType.Int);
                            cmd.Parameters.Add("@target", SqlDbType.Int);
                            cmd.Parameters.Add("@score", SqlDbType.Decimal);
                            cmd.Parameters.Add("@type", SqlDbType.TinyInt);

                            foreach (var sim in similarities)
                            {
                                cmd.Parameters["@source"].Value = sim.SourceBibliographyId;
                                cmd.Parameters["@target"].Value = sim.TargetBibliographyId;
                                cmd.Parameters["@score"].Value = sim.SimilarityScore;
                                cmd.Parameters["@type"].Value = (byte)sim.Type;

                                savedCount += cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return savedCount;
        }

        /// <summary>
        /// 获取预计算的相似书籍
        /// </summary>
        public List<BookSimilarity> GetPrecomputedSimilarities(int bibliographyId, SimilarityType type, int topN)
        {
            var results = new List<BookSimilarity>();

            string sql = @"
                SELECT TOP (@topN)
                    similarity_id,
                    source_bibliography_id,
                    target_bibliography_id,
                    similarity_score,
                    similarity_type,
                    calculated_time
                FROM book_similarity
                WHERE source_bibliography_id = @bibliographyId
                  AND similarity_type = @type
                ORDER BY similarity_score DESC";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@topN", topN);
                    cmd.Parameters.AddWithValue("@bibliographyId", bibliographyId);
                    cmd.Parameters.AddWithValue("@type", (byte)type);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new BookSimilarity
                            {
                                SimilarityId = reader.GetInt64(0),
                                SourceBibliographyId = reader.GetInt32(1),
                                TargetBibliographyId = reader.GetInt32(2),
                                SimilarityScore = (double)reader.GetDecimal(3),
                                Type = (SimilarityType)reader.GetByte(4),
                                CalculatedTime = reader.GetDateTime(5)
                            });
                        }
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// 检查是否存在预计算的相似度数据
        /// </summary>
        public bool HasPrecomputedSimilarities(int bibliographyId, SimilarityType type)
        {
            string sql = @"
                SELECT COUNT(*) FROM book_similarity 
                WHERE source_bibliography_id = @bibliographyId AND similarity_type = @type";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@bibliographyId", bibliographyId);
                    cmd.Parameters.AddWithValue("@type", (byte)type);

                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        /// <summary>
        /// 获取相似度数据的最后更新时间
        /// </summary>
        public DateTime? GetSimilarityLastUpdateTime(SimilarityType type)
        {
            string sql = "SELECT MAX(calculated_time) FROM book_similarity WHERE similarity_type = @type";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@type", (byte)type);
                    var result = cmd.ExecuteScalar();
                    return result == DBNull.Value ? null : (DateTime?)result;
                }
            }
        }

        /// <summary>
        /// 清除指定类型的相似度数据
        /// </summary>
        public int ClearSimilarities(SimilarityType type)
        {
            string sql = "DELETE FROM book_similarity WHERE similarity_type = @type";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@type", (byte)type);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 清除所有相似度数据
        /// </summary>
        public int ClearAllSimilarities()
        {
            string sql = "DELETE FROM book_similarity";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 根据相似度获取书目详情
        /// </summary>
        public List<RecommendationResult> GetBookDetailsBySimilarities(List<BookSimilarity> similarities)
        {
            var results = new List<RecommendationResult>();

            if (similarities == null || similarities.Count == 0)
            {
                return results;
            }

            var bibIds = new List<int>();
            var scoreMap = new Dictionary<int, double>();

            foreach (var sim in similarities)
            {
                bibIds.Add(sim.TargetBibliographyId);
                scoreMap[sim.TargetBibliographyId] = sim.SimilarityScore;
            }

            var idParams = new List<string>();
            for (int i = 0; i < bibIds.Count; i++)
            {
                idParams.Add(string.Format("@id{0}", i));
            }

            string sql = string.Format(@"
                SELECT 
                    b.bibliography_id,
                    b.bibliography_name,
                    b.ISBN,
                    b.publish,
                    c.category_code,
                    c.category_name,
                    (SELECT COUNT(*) FROM bookborrow bb 
                     INNER JOIN BOOK_ITEM bi ON bb.bookID = bi.item_barcode
                     WHERE bi.bibliography_id = b.bibliography_id) AS borrow_count,
                    (SELECT STRING_AGG(a.author_name, ', ') WITHIN GROUP (ORDER BY ba.author_order)
                     FROM BIBLIO_AUTHOR ba 
                     INNER JOIN AUTHOR a ON a.author_id = ba.author_id
                     WHERE ba.bibliography_id = b.bibliography_id) AS authors
                FROM BIBLIOGRAPHY b
                LEFT JOIN BOOK_CATEGORY c ON c.category_id = b.category_id
                WHERE b.bibliography_id IN ({0})", string.Join(",", idParams));

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    for (int i = 0; i < bibIds.Count; i++)
                    {
                        cmd.Parameters.AddWithValue(string.Format("@id{0}", i), bibIds[i]);
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int bibId = reader.GetInt32(0);
                            results.Add(new RecommendationResult
                            {
                                BibliographyId = bibId,
                                BookName = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                ISBN = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                Publisher = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                CategoryCode = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                CategoryName = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                BorrowCount = reader.GetInt32(6),
                                Authors = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                Score = scoreMap[bibId],
                                Type = RecommendationType.Similar,
                                Reason = string.Format("相似度: {0:P1}", scoreMap[bibId])
                            });
                        }
                    }
                }
            }

            // 按相似度分数排序
            results.Sort((a, b) => b.Score.CompareTo(a.Score));

            return results;
        }

        #endregion
    }
}
