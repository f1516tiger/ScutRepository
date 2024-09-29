
using System.Threading.Tasks;

namespace ZyGames.Framework.RPC.Http
{
    /// <summary>
    /// Represents any HTTP response action to respond to a client with.
    /// </summary>
    public interface IHttpResponseAction
    {
        /// <summary>
        /// User identity
        /// </summary>
        string Identity { get;}

        /// <summary>
        /// 
        /// </summary>
        string RequestParams { get; }

        /// <summary>
        /// Execute the intended response action against the Response.
        /// </summary>
        /// <param name="context">The current connection's request/response context.</param>
        /// <returns>A task which represents an asynchronous operation to await or null if a synchronous operation already completed.</returns>
        Task Execute(IHttpRequestResponseContext context);
    }
}
