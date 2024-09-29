
using System;

namespace ZyGames.Framework.RPC.Http
{
    /// <summary>
    /// 
    /// </summary>
    public static class ByteArrayExtensions
    {
        private static readonly char[] hexChars = new char[16] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToHexString(this byte[] value)
        {
            return ToHexString(value, 0, value.Length);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string ToHexString(this byte[] value, int offset, int count)
        {
            char[] c = new char[count * 2];
            int i;
            for (i = 0; (i + offset < value.Length) && (i < count); ++i)
            {
                c[i * 2 + 0] = hexChars[value[i + offset] >> 4];
                c[i * 2 + 1] = hexChars[value[i + offset] & 15];
            }
            return new string(c, 0, i * 2);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string AsNullIfEmpty(this string value)
        {
            if (String.IsNullOrEmpty(value)) return null;
            return value;
        }
    }
}
