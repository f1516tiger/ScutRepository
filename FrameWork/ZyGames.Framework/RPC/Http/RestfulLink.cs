
using Newtonsoft.Json;

namespace ZyGames.Framework.RPC.Http
{
    /// <summary>
    /// 
    /// </summary>
    public class RestfulLink
    {
        /// <summary>
        /// 
        /// </summary>
        readonly protected string title;
        /// <summary>
        /// 
        /// </summary>
        readonly protected string rel;
        /// <summary>
        /// 
        /// </summary>
        readonly protected string href;

        internal RestfulLink(string title, string href, string rel)
        {
            this.title = title;
            this.rel = rel;
            this.href = href;
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get { return title; } }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("rel")]
        public string Rel { get { return rel; } }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("href")]
        public string Href { get { return href; } }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="href"></param>
        /// <param name="rel"></param>
        /// <returns></returns>
        public static RestfulLink Create(string title, string href, string rel = "related")
        {
            return new RestfulLink(title, href, rel);
        }
    }
}
