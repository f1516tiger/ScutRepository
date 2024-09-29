
using System.Net;
using System.Security.Principal;

namespace ZyGames.Framework.RPC.Http
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHttpRequestContext
    {
        /// <summary>
        /// 
        /// </summary>
        IHttpAsyncHostHandlerContext HostContext { get; }
        /// <summary>
        /// 
        /// </summary>
        HttpListenerRequest Request { get; }
        /// <summary>
        /// 
        /// </summary>
        IPrincipal User { get; }
        /// <summary>
        /// 
        /// </summary>
        string UserHostAddress { get; }
    }
}
