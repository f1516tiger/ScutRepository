
using System.IO;
using System.Threading.Tasks;

#pragma warning disable 1998

namespace ZyGames.Framework.RPC.Http
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class JsonResponse : StatusResponse, IHttpResponseAction
    {
        readonly object _value;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="statusDescription"></param>
        /// <param name="value"></param>
        public JsonResponse(int statusCode, string statusDescription, object value)
            : base(statusCode, statusDescription)
        {
            _value = value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task Execute(IHttpRequestResponseContext context)
        {
            SetStatus(context);
            context.Response.ContentType = "application/json; charset=utf-8";
            //context.Response.ContentEncoding = UTF8.WithoutBOM;

            using (context.Response.OutputStream)
            {
                var tw = new StreamWriter(context.Response.OutputStream, UTF8.WithoutBOM);
                Json.Serializer.Serialize(tw, _value);
            }
        }
    }
}
