
namespace ZyGames.Framework.RPC.Http
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHttpAsyncHostHandlerContext
    {
        /// <summary>
        /// 
        /// </summary>
        IHttpAsyncHost Host { get; }
        /// <summary>
        /// 
        /// </summary>
        IHttpAsyncHandler Handler { get; }
    }
}
