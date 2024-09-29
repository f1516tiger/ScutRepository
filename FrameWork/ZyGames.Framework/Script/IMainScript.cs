

namespace ZyGames.Framework.Script
{
    /// <summary>
    /// Script main class interface
    /// </summary>
    public interface IMainScript
    {
        /// <summary>
        /// Main
        /// </summary>
        void Start(string[] args);

        /// <summary>
        /// 
        /// </summary>
        byte[] ProcessRequest(object package, object param);

        /// <summary>
        /// Stop
        /// </summary>
        void Stop();

        /// <summary>
        /// Main
        /// </summary>
        void ReStart();
    }
}