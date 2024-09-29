
using System.Threading.Tasks;

namespace ZyGames.Framework.RPC.Http
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConfigurationTrait
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostContext"></param>
        /// <param name="configValues"></param>
        /// <returns></returns>
        Task<bool> Configure(IHttpAsyncHostHandlerContext hostContext, ConfigurationDictionary configValues);
    }
}
