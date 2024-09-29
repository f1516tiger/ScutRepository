

using System;

namespace ZyGames.Framework.Script
{
    /// <summary>
    /// CSharp文件信息
    /// </summary>
    [Serializable]
    public class CSharpFileInfo : ScriptFileInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileCode"></param>
        /// <param name="fileName"></param>
        public CSharpFileInfo(string fileCode, string fileName)
            : base(fileCode, fileName)
        {
        }

    }
}