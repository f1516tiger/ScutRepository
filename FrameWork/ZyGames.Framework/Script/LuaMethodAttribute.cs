
using System;

namespace ZyGames.Framework.Script
{
    /// <summary>
    /// Lua方法属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class LuaMethodAttribute : Attribute
    {
        private string _funcName;

        /// <summary>
        /// init
        /// </summary>
        /// <param name="funcName"></param>
        public LuaMethodAttribute(string funcName)
        {
            _funcName = funcName;
        }

        /// <summary>
        /// 
        /// </summary>
        public string FuncName
        {
            get { return _funcName; }
        }
    }
}
