
using System.Net;
using System.Security.Principal;

namespace ZyGames.Framework.RPC.Http
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class HttpRequestContext : IHttpRequestContext
    {
        /// <summary>
        /// 
        /// </summary>
        public IHttpAsyncHostHandlerContext HostContext { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public HttpListenerRequest Request { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public IPrincipal User { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserHostAddress { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostContext"></param>
        /// <param name="request"></param>
        /// <param name="user"></param>
        /// <param name="userHostAddress"></param>
        public HttpRequestContext(IHttpAsyncHostHandlerContext hostContext, HttpListenerRequest request, IPrincipal user, string userHostAddress)
        {
            HostContext = hostContext;
            Request = request;
            User = user;
            UserHostAddress = userHostAddress;
        }
    }
}
