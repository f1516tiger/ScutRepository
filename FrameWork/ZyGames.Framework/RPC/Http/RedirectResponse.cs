
using System.Threading.Tasks;

namespace ZyGames.Framework.RPC.Http
{
    /// <summary>
    /// Provides a 302 redirect response to the given url.
    /// </summary>
    public sealed class RedirectResponse : IHttpResponseAction
    {
        /// <summary>
        /// 
        /// </summary>
        public string Url { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string Identity { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string RequestParams { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="identity"></param>
        public RedirectResponse(string url, string identity = null)
        {
            this.Url = url;
            Identity = identity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Execute(IHttpRequestResponseContext context)
        {
            context.Response.Redirect(Url);
            return null;
        }
    }
}
