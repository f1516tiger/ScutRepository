

using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

#pragma warning disable 1998

namespace ZyGames.Framework.RPC.Http
{
    /// <summary>
    /// 
    /// </summary>
    public class ByteResponse : StatusResponse, IHttpResponseAction
    {
        readonly byte[] data;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="statusDescription"></param>
        /// <param name="value"></param>
        /// <param name="identity"></param>
        public ByteResponse(int statusCode, string statusDescription, byte[] value, string identity = null)
            : base(statusCode, statusDescription, identity)
        {
            data = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public event Action<IHttpRequestResponseContext> CookieHandle;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected virtual void SetCookie(IHttpRequestResponseContext context)
        {
            Action<IHttpRequestResponseContext> handler = CookieHandle;
            if (handler != null) handler(context);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task Execute(IHttpRequestResponseContext context)
        {
            if (data == null)
            {
                context.Response.StatusCode = 500;
                return;
            }

            SetStatus(context);
            SetCookie(context);
            //js call need
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (context.Request.QueryString["showjson"] == "1")
            {
                context.Response.ContentType = "application/json";
                context.Response.SendChunked = false;
                context.Response.ContentLength64 = data.Length;
                using (Stream output = context.Response.OutputStream)
                {
                    await output.WriteAsync(data, 0, data.Length);
                    output.Close();
                }
            }
            else
            {
                context.Response.ContentType = "application/octet-stream";
                context.Response.SendChunked = false;
                int offset = 0;
                if (data.Length > 3 && data[offset] == 0x1f && data[offset + 1] == 0x8b && data[offset + 2] == 0x08 && data[offset + 3] == 0x00)
                {
                    context.Response.AddHeader("Content-Encoding", "gzip");
                }

                context.Response.ContentLength64 = data.Length;
                using (Stream output = context.Response.OutputStream)
                {
                    await output.WriteAsync(data, 0, data.Length);
                    output.Close();
                }
            }
        }
    }
}
