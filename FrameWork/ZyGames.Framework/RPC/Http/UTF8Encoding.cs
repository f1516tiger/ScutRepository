
namespace ZyGames.Framework.RPC.Http
{
    /// <summary>
    /// 
    /// </summary>
    public static class UTF8
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly System.Text.UTF8Encoding WithoutBOM = new System.Text.UTF8Encoding(false);
        /// <summary>
        /// 
        /// </summary>
        public static readonly System.Text.UTF8Encoding WithBOM = new System.Text.UTF8Encoding(true);
    }
}
