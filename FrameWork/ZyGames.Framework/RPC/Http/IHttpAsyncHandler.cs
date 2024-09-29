
using System.Threading.Tasks;

namespace ZyGames.Framework.RPC.Http
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHttpAsyncHandler
    {
        /// <summary>
        /// Main execution method of the handler which returns an HTTP response intent.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        Task<IHttpResponseAction> Execute(IHttpRequestContext state);
    }
}
