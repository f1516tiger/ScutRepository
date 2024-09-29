
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace ZyGames.Framework.Data.Sql
{
    ///<summary>
    /// MSSQL数据库参数缓存
    ///</summary>
    internal sealed class SqlHelperParameterCache
    {
        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());
        private SqlHelperParameterCache()
        {
        }
        private static SqlParameter[] DiscoverSpParameterSet(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new ArgumentNullException("spName");
            }
            SqlCommand sqlCommand = new SqlCommand(spName, connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlCommandBuilder.DeriveParameters(sqlCommand);
            connection.Close();
            if (!includeReturnValueParameter)
            {
                sqlCommand.Parameters.RemoveAt(0);
            }
            int count = sqlCommand.Parameters.Count;
            SqlParameter[] array = new SqlParameter[count];
            sqlCommand.Parameters.CopyTo(array, 0);
            SqlParameter[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                SqlParameter sqlParameter = array2[i];
                sqlParameter.Value = DBNull.Value;
            }
            return array;
        }
        private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
        {
            int num = originalParameters.Length;
            SqlParameter[] array = new SqlParameter[num];
            int i = 0;
            int num2 = num;
            while (i < num2)
            {
                array[i] = (SqlParameter)((ICloneable)originalParameters[i]).Clone();
                i++;
            }
            return array;
        }
        ///<summary>
        ///</summary>
        ///<param name="connectionString"></param>
        ///<param name="commandText"></param>
        ///<param name="commandParameters"></param>
        ///<exception cref="ArgumentNullException"></exception>
        public static void CacheParameterSet(string connectionString, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new ArgumentNullException("connectionString");
            }
            if (commandText == null || commandText.Length == 0)
            {
                throw new ArgumentNullException("commandText");
            }
            string key = connectionString + ":" + commandText;
            SqlHelperParameterCache.paramCache[key] = commandParameters;
        }
        ///<summary>
        ///</summary>
        ///<param name="connectionString"></param>
        ///<param name="commandText"></param>
        ///<returns></returns>
        ///<exception cref="ArgumentNullException"></exception>
        public static SqlParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new ArgumentNullException("connectionString");
            }
            if (commandText == null || commandText.Length == 0)
            {
                throw new ArgumentNullException("commandText");
            }
            string key = connectionString + ":" + commandText;
            SqlParameter[] array = SqlHelperParameterCache.paramCache[key] as SqlParameter[];
            if (array == null)
            {
                return null;
            }
            return SqlHelperParameterCache.CloneParameters(array);
        }
        ///<summary>
        ///</summary>
        ///<param name="connectionString"></param>
        ///<param name="spName"></param>
        ///<returns></returns>
        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName)
        {
            return SqlHelperParameterCache.GetSpParameterSet(connectionString, spName, false);
        }
        ///<summary>
        ///</summary>
        ///<param name="connectionString"></param>
        ///<param name="spName"></param>
        ///<param name="includeReturnValueParameter"></param>
        ///<returns></returns>
        ///<exception cref="ArgumentNullException"></exception>
        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            if (connectionString == null || connectionString.Length == 0)
            {
                throw new ArgumentNullException("connectionString");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new ArgumentNullException("spName");
            }
            SqlParameter[] result;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlParameter[] spParameterSetInternal = SqlHelperParameterCache.GetSpParameterSetInternal(sqlConnection, spName, includeReturnValueParameter);
                result = spParameterSetInternal;
            }
            return result;
        }
        internal static SqlParameter[] GetSpParameterSet(SqlConnection connection, string spName)
        {
            return SqlHelperParameterCache.GetSpParameterSet(connection, spName, false);
        }
        internal static SqlParameter[] GetSpParameterSet(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            SqlParameter[] result;
            using (SqlConnection sqlConnection = (SqlConnection)((ICloneable)connection).Clone())
            {
                SqlParameter[] spParameterSetInternal = SqlHelperParameterCache.GetSpParameterSetInternal(sqlConnection, spName, includeReturnValueParameter);
                result = spParameterSetInternal;
            }
            return result;
        }
        private static SqlParameter[] GetSpParameterSetInternal(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if (spName == null || spName.Length == 0)
            {
                throw new ArgumentNullException("spName");
            }
            string key = connection.ConnectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");
            SqlParameter[] array = SqlHelperParameterCache.paramCache[key] as SqlParameter[];
            if (array == null)
            {
                SqlParameter[] array2 = SqlHelperParameterCache.DiscoverSpParameterSet(connection, spName, includeReturnValueParameter);
                SqlHelperParameterCache.paramCache[key] = array2;
                array = array2;
            }
            return SqlHelperParameterCache.CloneParameters(array);
        }
    }
}