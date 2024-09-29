
using System;

namespace ZyGames.Framework.Plugin.PythonScript
{
    /// <summary>
    /// python bariable
    /// </summary>
    [Obsolete("使用ZyGames.Framework.Script.ScriptEngines替代")]
    public class PythonParam
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public PythonParam(string name , object value)
        {
            Name = name;
            Value = value;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public object Value
        {
            get;
            set;
        }
    }
}