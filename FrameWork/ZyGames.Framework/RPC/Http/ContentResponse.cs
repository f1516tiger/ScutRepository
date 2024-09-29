
using System.Threading.Tasks;

namespace ZyGames.Framework.RPC.Http
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ContentResponse : StatusResponse
    {
        readonly string response;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="statusDescription"></param>
        /// <param name="response"></param>
        public ContentResponse(int statusCode, string statusDescription, string response)
            : base(statusCode, statusDescription)
        {
            this.response = response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task Execute(IHttpRequestResponseContext context)
        {
            SetStatus(context);
            context.Response.ContentLength64 = response.Length;
            context.Response.SendChunked = false;
            context.Response.ContentType = "text/html";

            using (context.Response.OutputStream)
            using (var tw = new System.IO.StreamWriter(context.Response.OutputStream, UTF8.WithoutBOM, 65536, true))
                await tw.WriteAsync(response);
        }
    }
}
