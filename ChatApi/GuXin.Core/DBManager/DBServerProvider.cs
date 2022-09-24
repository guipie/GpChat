using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GuXin.Core.Configuration;
using GuXin.Core.Const;
using GuXin.Core.Dapper;
using GuXin.Core.EFDbContext;
using GuXin.Core.Enums;
using GuXin.Core.Extensions;

namespace GuXin.Core.DBManager
{
    public class DBServerProvider
    {
        private static Dictionary<string, string> ConnectionPool = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private static readonly string DefaultConnName = "defalut";

        static DBServerProvider()
        {
            SetConnection(DefaultConnName, AppSetting.DbConnectionString);
        }
        public static void SetConnection(string key, string val)
        {
            if (ConnectionPool.ContainsKey(key))
            {
                ConnectionPool[key] = val;
                return;
            }
            ConnectionPool.Add(key, val);
        }
        /// <summary>
        /// 设置默认数据库连接
        /// </summary>
        /// <param name="val"></param>
        public static void SetDefaultConnection(string val)
        {
            SetConnection(DefaultConnName, val);
        }

        public static string GetConnectionString(string key)
        {
            key = key ?? DefaultConnName;
            if (ConnectionPool.ContainsKey(key))
            {
                return ConnectionPool[key];
            }
            return key;
        }
        /// <summary>
        /// 获取默认数据库连接
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            return GetConnectionString(DefaultConnName);
        }
        public static IDbConnection GetDbConnection(string connString = null)
        {
            if (connString==null)
            {
                connString = ConnectionPool[DefaultConnName];
            }  
            if (DBType.Name == DbCurrentType.MySql.ToString())
            {
                return new MySql.Data.MySqlClient.MySqlConnection(connString);
            }
            if (DBType.Name == DbCurrentType.PgSql.ToString())
            {
                return new NpgsqlConnection(connString);
            }
            return new SqlConnection(connString); 
        }

        public static GuXinContext DbContext
        {
            get { return GetEFDbContext(); }
        }
        public static GuXinContext GetEFDbContext()
        {
            return GetEFDbContext(null);
        }
        public static GuXinContext GetEFDbContext(string dbName)
        {
            GuXinContext beefContext = Utilities.HttpContext.Current.RequestServices.GetService(typeof(GuXinContext)) as GuXinContext;
            if (dbName != null)
            {
                if (!ConnectionPool.ContainsKey(dbName))
                {
                    throw new Exception("数据库连接名称错误");
                }
                beefContext.Database.GetDbConnection().ConnectionString = ConnectionPool[dbName];
            }
            return beefContext;
        }

        public static void SetDbContextConnection(GuXinContext beefContext, string dbName)
        {
            if (!ConnectionPool.ContainsKey(dbName))
            {
                throw new Exception("数据库连接名称错误");
            }
            beefContext.Database.GetDbConnection().ConnectionString = ConnectionPool[dbName];
        }
        /// <summary>
        /// 获取实体的数据库连接
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="defaultDbContext"></param>
        /// <returns></returns>
        public static void GetDbContextConnection<TEntity>(GuXinContext defaultDbContext)
        {
            //string connstr= defaultDbContext.Database.GetDbConnection().ConnectionString;
            // if (connstr != ConnectionPool[DefaultConnName])
            // {
            //     defaultDbContext.Database.GetDbConnection().ConnectionString = ConnectionPool[DefaultConnName];
            // };
        }

        public static ISqlDapper SqlDapper
        {
            get
            {
                return new SqlDapper(DefaultConnName);
            }
        }
        public static ISqlDapper GetSqlDapper(string dbName = null)
        {
            return new SqlDapper(dbName ?? DefaultConnName);
        }
        public static ISqlDapper GetSqlDapper<TEntity>()
        {
            //获取实体真实的数据库连接池对象名，如果不存在则用默认数据连接池名
            string dbName = typeof(TEntity).GetTypeCustomValue<DBConnectionAttribute>(x => x.DBName) ?? DefaultConnName;
            return GetSqlDapper(dbName);
        }

    }
}
