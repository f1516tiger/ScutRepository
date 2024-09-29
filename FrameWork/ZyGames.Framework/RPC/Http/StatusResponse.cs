
using System.Threading.Tasks;

namespace ZyGames.Framework.RPC.Http
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class StatusResponse : IHttpResponseAction
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly int statusCode;
        /// <summary>
        /// 
        /// </summary>
        protected readonly string statusDescription;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="statusDescription"></param>
        /// <param name="identity"></param>
        protected StatusResponse(int statusCode, string statusDescription, string identity = null)
        {
            this.statusCode = statusCode;
            this.statusDescription = statusDescription;
            this.Identity = identity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected void SetStatus(IHttpRequestResponseContext context)
        {
            context.Response.StatusCode = statusCode;
            if (statusDescription != null)
                context.Response.StatusDescription = statusDescription;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Identity { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string RequestParams { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public abstract Task Execute(IHttpRequestResponseContext context);
    }
}
