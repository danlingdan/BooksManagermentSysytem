using System;
using System.Data;
using System.Data.SqlClient;

namespace BooksManagermentSysytem.Data
{
    /// <summary>
    /// 数据库访问帮助类
    /// </summary>
    public static class DatabaseHelper
    {
        private static readonly string ConnectionString;

        static DatabaseHelper()
        {
            // 优先从配置文件读取连接字符串，默认使用 LocalDB
            ConnectionString = GetConnectionStringFromConfig() 
                ?? @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LibraryDB;Integrated Security=True;MultipleActiveResultSets=True;Connection Timeout=10";
        }

        /// <summary>
        /// 从配置读取连接字符串
        /// </summary>
        private static string GetConnectionStringFromConfig()
        {
            try
            {
                // 使用反射读取 ConfigurationManager（避免直接引用 System.Configuration）
                var configType = Type.GetType("System.Configuration.ConfigurationManager, System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
                if (configType != null)
                {
                    var connectionStringsProperty = configType.GetProperty("ConnectionStrings");
                    var connectionStrings = connectionStringsProperty?.GetValue(null);
                    if (connectionStrings != null)
                    {
                        var indexer = connectionStrings.GetType().GetProperty("Item", new[] { typeof(string) });
                        var connStringSetting = indexer?.GetValue(connectionStrings, new object[] { "LibraryDB" });
                        if (connStringSetting != null)
                        {
                            var connectionStringProperty = connStringSetting.GetType().GetProperty("ConnectionString");
                            return connectionStringProperty?.GetValue(connStringSetting) as string;
                        }
                    }
                }
            }
            catch
            {
                // 如果反射失败，使用默认值
            }
            return null;
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        /// <summary>
        /// 执行非查询SQL语句
        /// </summary>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 执行查询并返回第一行第一列
        /// </summary>
        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// 执行查询并返回DataTable
        /// </summary>
        public static DataTable ExecuteQuery(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        public static DataTable ExecuteStoredProcedure(string procedureName, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        /// <summary>
        /// 执行带事务的多条SQL语句
        /// </summary>
        public static bool ExecuteTransaction(params Tuple<string, SqlParameter[]>[] commands)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var command in commands)
                        {
                            using (SqlCommand cmd = new SqlCommand(command.Item1, conn, transaction))
                            {
                                if (command.Item2 != null && command.Item2.Length > 0)
                                {
                                    cmd.Parameters.AddRange(command.Item2);
                                }
                                cmd.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 创建SQL参数
        /// </summary>
        public static SqlParameter CreateParameter(string name, object value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }

        /// <summary>
        /// 创建SQL参数（指定类型）
        /// </summary>
        public static SqlParameter CreateParameter(string name, SqlDbType type, object value)
        {
            SqlParameter param = new SqlParameter(name, type);
            param.Value = value ?? DBNull.Value;
            return param;
        }

        /// <summary>
        /// 创建输出参数
        /// </summary>
        public static SqlParameter CreateOutputParameter(string name, SqlDbType type)
        {
            SqlParameter param = new SqlParameter(name, type);
            param.Direction = ParameterDirection.Output;
            return param;
        }

        /// <summary>
        /// 创建输出参数（指定大小）
        /// </summary>
        public static SqlParameter CreateOutputParameter(string name, SqlDbType type, int size)
        {
            SqlParameter param = new SqlParameter(name, type, size);
            param.Direction = ParameterDirection.Output;
            return param;
        }
    }
}
