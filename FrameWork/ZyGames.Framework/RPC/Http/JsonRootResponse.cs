﻿
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

#pragma warning disable 1998

namespace ZyGames.Framework.RPC.Http
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonRootResponse : IHttpResponseAction
    {
        // NOTE(jsd): Fields are serialized to JSON in lexical definition order.

        // NOTE(jsd): This is here primarily for JSONP compatibility.
        /// <summary>
        /// 
        /// </summary>
        public readonly int statusCode;
        /// <summary>
        /// 
        /// </summary>
        public readonly bool success;
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public readonly string statusDescription;
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public readonly string message;
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public readonly RestfulLink[] links;
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public readonly object meta;
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public readonly object[] errors;

        // NOTE(jsd): `results` must be last.
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public readonly object results;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="statusDescription"></param>
        /// <param name="message"></param>
        /// <param name="links"></param>
        /// <param name="meta"></param>
        /// <param name="errors"></param>
        /// <param name="results"></param>
        public JsonRootResponse(int statusCode = 200, string statusDescription = null, string message = null, RestfulLink[] links = null, object meta = null, object[] errors = null, object results = null)
        {
            this.statusCode = statusCode;
            this.statusDescription = statusDescription;
            this.success = statusCode == 200;
            this.message = message;
            this.links = links;
            this.meta = meta;
            this.errors = errors;
            this.results = results;
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Identity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RequestParams { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IHttpRequestResponseContext context)
        {
            context.Response.StatusCode = statusCode;
            if (statusDescription != null)
                context.Response.StatusDescription = statusDescription;
            context.Response.ContentType = "application/json; charset=utf-8";
            // NOTE(jsd): Not concerned at all about setting ContentLength64.

            // NOTE(jsd): This seems to be a hot line re: performance.
            //context.Response.ContentEncoding = UTF8.WithoutBOM;

#if false
            var rsp = context.Response.OutputStream;
            using (rsp)
            {
                var tw = new StreamWriter(rsp, UTF8.WithoutBOM);
                Json.Serializer.Serialize(tw, this);
                tw.Close();
            }
#else
            // NOTE(jsd): Just testing out some stuff here...
            var sb = new StringBuilder(1024);
            using (var sw = new StringWriter(sb))
            {
                Json.Serializer.Serialize(sw, this);
            }

            var rsp = context.Response.OutputStream;
            var enc = UTF8.WithoutBOM;

            // Get the response as a single string instance (horrible but apparently the only way to easily calculate
            // the byte count of the response):
            var text = sb.ToString();
            var bytes = enc.GetBytes(text);

            // Set the length header:
            context.Response.ContentLength64 = bytes.LongLength;
            // Write the response:
            await rsp.WriteAsync(bytes, 0, bytes.Length);
#endif
        }
    }
}
