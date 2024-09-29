
using System;

namespace ZyGames.Framework.RPC.Http
{
    /// <summary>
    /// An exception to be converted to a JSON error result.
    /// </summary>
    public sealed class JsonResultException : Exception
    {
        private readonly int _statusCode;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        public JsonResultException(int statusCode, string message)
            : base(message)
        {
            _statusCode = statusCode;
        }
        /// <summary>
        /// 
        /// </summary>
        public int StatusCode { get { return _statusCode; } }
    }
}
