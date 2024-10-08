﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;
using System.Configuration;
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Configuration;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Data.MySql;
using ZyGames.Framework.Data.Sql;
using ZyGames.Framework.Model;

namespace ZyGames.Framework.Data
{

    /// <summary>
    /// 数据连接提供类
    /// </summary>
    public sealed class DbConnectionProvider
    {
        private static ConcurrentDictionary<string, DbBaseProvider> dbProviders = new ConcurrentDictionary<string, DbBaseProvider>();

        /// <summary>
        /// Connection count
        /// </summary>
        public static int Count
        {
            get { return dbProviders.Count; }
        }
        /// <summary>
        /// 初始化DB连接
        /// </summary>
        /// <exception cref="Exception"></exception>
        public static void Initialize()
        {
            var connectionList = ConfigManager.Configger.GetConfig<ConnectionSection>();
            foreach (var section in connectionList)
            {
                var setting = ConnectionSetting.Create(section.Name, section.ProviderName, section.ConnectionString.Trim());
                if (setting.ProviderType == DbProviderType.Unkown)
                {
                    if (setting.DbLevel != DbLevel.LocalMySql && setting.DbLevel != DbLevel.LocalSql)
                    {
                        TraceLog.WriteWarn("Db connection not found provider type, {0} connectionString:{1}", section.Name, setting.ConnectionString);
                    }
                    continue;
                }
                var dbBaseProvider = CreateDbProvider(setting);
                try
                {
                    dbBaseProvider.CheckConnect();
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Not connect to the database server \"{0}\" database \"{1}\".", dbBaseProvider.ConnectionSetting.DataSource, dbBaseProvider.ConnectionSetting.DatabaseName), ex);
                }
                dbProviders.AddOrUpdate(section.Name, dbBaseProvider, (k, oldValue) => dbBaseProvider);
            }

        }

        /// <summary>
        /// 查找库连接
        /// </summary>
        /// <returns></returns>
        public static KeyValuePair<string, DbBaseProvider> Find(DbLevel dbLevel)
        {
            return dbProviders.Where(pair => pair.Value.ConnectionSetting.DbLevel == dbLevel).FirstOrDefault();
        }

        /// <summary>
        /// 取第一个
        /// </summary>
        /// <returns></returns>
        public static KeyValuePair<string, DbBaseProvider> FindFirst()
        {
            var providers = dbProviders.Where(
                pair => pair.Value.ConnectionSetting.DbLevel != DbLevel.LocalSql &&
                    pair.Value.ConnectionSetting.DbLevel != DbLevel.LocalMySql)
                .ToList();
            return providers.FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectKey"></param>
        /// <returns></returns>
        public static DbBaseProvider CreateDbProvider(string connectKey)
        {
            if (string.IsNullOrEmpty(connectKey))
            {
                return null;
            }
            DbBaseProvider dbBaseProvider;
            if (dbProviders.TryGetValue(connectKey, out dbBaseProvider))
            {
                return dbBaseProvider;
            }
            var connSection = ConfigManager.Configger.GetConnetion<ConnectionSection>(connectKey);
            if (connSection != null)
            {
                string connectionString = connSection.ConnectionString;
                try
                {
                    dbBaseProvider = CreateDbProvider(connSection.Name, connSection.ProviderName, connectionString);
                    dbProviders.TryAdd(connectKey, dbBaseProvider);
                }
                catch
                {
                    TraceLog.WriteError("ProviderName:{0} instance failed.", connSection.ProviderName);
                }
            }
            else
            {
                var section = ConfigurationManager.ConnectionStrings[connectKey];
                if (section == null)
                {
                    return null;
                }
                try
                {
                    dbBaseProvider = CreateDbProvider(section.Name, section.ProviderName, section.ConnectionString);
                    dbProviders.TryAdd(connectKey, dbBaseProvider);
                }
                catch
                {
                    TraceLog.WriteError("ProviderName:{0} instance failed.", section.ProviderName);
                }
            }

            return dbBaseProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        public static DbBaseProvider CreateDbProvider(SchemaTable schema)
        {
            if (!string.IsNullOrEmpty(schema.ConnectKey))
            {
                return CreateDbProvider(schema.ConnectKey);
            }
            if (!string.IsNullOrEmpty(schema.ConnectionString))
            {
                return CreateDbProvider(schema.ConnectKey, schema.ConnectionProviderType, schema.ConnectionString);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="providerTypeName"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static DbBaseProvider CreateDbProvider(string name, string providerTypeName, string connectionString)
        {
            var setting = ConnectionSetting.Create(name, providerTypeName, connectionString);
            return CreateDbProvider(setting);
        }

        private static DbBaseProvider CreateDbProvider(ConnectionSetting setting)
        {
            Type type = TryGetProviderType(setting.ProviderTypeName) ?? typeof(SqlDataProvider);
            return type.CreateInstance<DbBaseProvider>(setting);
        }

        private static Type TryGetProviderType(string providerTypeName)
        {
            if (string.IsNullOrEmpty(providerTypeName)) return null;

            Type type;
            Type temp;
            if ((temp = typeof(MySqlDataProvider)).Name.IsEquals(providerTypeName, true))
            {
                type = temp;
            }
            else if ((temp = typeof(SqlDataProvider)).Name.IsEquals(providerTypeName, true))
            {
                type = temp;
            }
            else
            {
                throw new NotSupportedException("Not support \"" + providerTypeName + "\" db provider");
            }
            return type;
        }

    }
}