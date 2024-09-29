
using Newtonsoft.Json;

namespace ZyGames.Framework.RPC.Http
{
    /// <summary>
    /// 
    /// </summary>
    public static class Json
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly JsonSerializer Serializer = JsonSerializer.Create(new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Include
        });
    }
}
