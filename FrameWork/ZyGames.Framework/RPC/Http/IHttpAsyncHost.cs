
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZyGames.Framework.RPC.Http
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHttpAsyncHost
    {
        /// <summary>
        /// Gets the set of server bindings.
        /// </summary>
        List<string> Prefixes { get; }

        /// <summary>
        /// Run the server host and block the current thread.
        /// </summary>
        /// <param name="uriPrefixes"></param>
        Task Run(params string[] uriPrefixes);
    }
}
