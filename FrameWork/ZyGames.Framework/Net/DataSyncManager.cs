﻿

using System.Collections.Generic;
using ZyGames.Framework.Model;
using ZyGames.Framework.Net.Redis;
using ZyGames.Framework.Net.Sql;

namespace ZyGames.Framework.Net
{
    /// <summary>
    /// 数据同步管理类
    /// </summary>
    public static class DataSyncManager
    {
        #region SQL
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IDataSender GetDataSender(bool isChange = true, string connectKey = null)
        {
            return new SqlDataSender(isChange, connectKey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static IDataReceiver GetDataGetter(SchemaTable schema, DbDataFilter filter)
        {
            return new SqlDataReceiver(schema, filter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="schema"></param>
        /// <param name="filter"></param>
        /// <param name="setFunc"></param>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public static bool TryReceiveSql<T>(SchemaTable schema, DbDataFilter filter, EntityPropertySetFunc<T> setFunc, out List<T> dataList)
            where T : new()
        {
            return new SqlDataReceiver(schema, filter).TryReceive(setFunc, out dataList);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="schema"></param>
        /// <param name="filter"></param>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public static bool TryReceiveSql<T>(SchemaTable schema, DbDataFilter filter, out List<T> dataList)
            where T : ISqlEntity, new()
        {
            return new SqlDataReceiver(schema, filter).TryReceive(out dataList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList"></param>
        /// <param name="isChange"></param>
        /// <param name="synchronous"></param>
        /// <returns></returns>
        public static bool SendSql<T>(IEnumerable<T> dataList, bool isChange = false, bool synchronous = false)
            where T : ISqlEntity
        {
            return SendSql(dataList, isChange, null, null, synchronous);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList"></param>
        /// <param name="isChange"></param>
        /// <param name="getPropertyFunc"></param>
        /// <param name="postColumnFunc"></param>
        /// <param name="synchronous">is sync data to db</param>
        /// <returns></returns>
        public static bool SendSql<T>(IEnumerable<T> dataList, bool isChange, EntityPropertyGetFunc<T> getPropertyFunc, EnttiyPostColumnFunc<T> postColumnFunc = null, bool synchronous = false)
            where T : ISqlEntity
        {
            return new SqlDataSender(isChange).Send(dataList, getPropertyFunc, postColumnFunc, synchronous);
        }
        #endregion


        #region Redis

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IDataSender GetRedisSender(SchemaTable schema, string key)
        {
            return new RedisDataSender(new TransSendParam(key) { Schema = schema });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static IDataReceiver GetRedisGetter(SchemaTable schema, string redisKey)
        {
            return new RedisDataGetter(redisKey, schema);
        }
        #endregion
    }

}