
using System.IO;
using System.Net;
using System.Security.Principal;

namespace ZyGames.Framework.RPC.Http
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class HttpRequestResponseContext : IHttpRequestResponseContext
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
        public HttpListenerResponse Response { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public Stream OutputStream { get { return Response.OutputStream; } }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestState"></param>
        /// <param name="response"></param>
        public HttpRequestResponseContext(IHttpRequestContext requestState, HttpListenerResponse response)
        {
            HostContext = requestState.HostContext;
            Request = requestState.Request;
            User = requestState.User;

            Response = response;
        }
    }
}
