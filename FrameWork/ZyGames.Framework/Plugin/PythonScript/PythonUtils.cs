﻿
using System;
using System.Collections.Generic;
using Microsoft.Scripting;

namespace ZyGames.Framework.Plugin.PythonScript
{
    /// <summary>
    /// Python工具
    /// </summary>
    [Obsolete("使用ZyGames.Framework.Script.ScriptEngines替代")]
    public class PythonUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pythonCode"></param>
        /// <param name="kind"></param>
        /// <param name="args"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static T ExecuteCode<T>(string pythonCode, SourceCodeKind kind, PythonParam[] args, out PythonContext context)
        {
            return ExecuteCode<T>(new string[0], pythonCode, kind, args, out context);
        }

        /// <summary>
        /// 执行Python代码
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblys"></param>
        /// <param name="pythonCode"></param>
        /// <param name="kind"></param>
        /// <param name="args">全局变量</param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static T ExecuteCode<T>(string[] assemblys, string pythonCode, SourceCodeKind kind, PythonParam[] args, out PythonContext context)
        {
            context = PythonContext.CreateInstance();
            context.LoadAssembly(assemblys);
            context.SetVariable(args);
            return context.Execute<T>(pythonCode, kind);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pythonCode"></param>
        /// <param name="kind"></param>
        /// <param name="args"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static object ExecuteCode(string pythonCode, SourceCodeKind kind, PythonParam[] args, out PythonContext context)
        {
            return ExecuteCode(new string[0], pythonCode, kind, args, out context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblys"></param>
        /// <param name="pythonCode"></param>
        /// <param name="kind"></param>
        /// <param name="args">全局变量</param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static object ExecuteCode(string[] assemblys, string pythonCode, SourceCodeKind kind, PythonParam[] args, out PythonContext context)
        {
            context = PythonContext.CreateInstance();
            context.LoadAssembly(assemblys);
            context.SetVariable(args);
            return context.Execute(pythonCode, kind);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="funcArg"></param>
        /// <param name="varList"></param>
        /// <param name="pyScript"></param>
        /// <returns></returns>
        public static object CallFunc(string funcName, string funcArg, List<PythonParam> varList, string pyScript)
        {
            return CallFunc<object>(new string[0], funcName, funcArg, varList, pyScript);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblys"></param>
        /// <param name="funcName"></param>
        /// <param name="funcArg"></param>
        /// <param name="varList"></param>
        /// <param name="pyScript"></param>
        /// <returns></returns>
        public static object CallFunc(string[] assemblys, string funcName, string funcArg, List<PythonParam> varList, string pyScript)
        {
            return CallFunc<object>(assemblys, funcName, funcArg, varList, pyScript);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcName"></param>
        /// <param name="funcArg"></param>
        /// <param name="varList"></param>
        /// <param name="pyScript"></param>
        /// <returns></returns>
        public static T CallFunc<T>(string funcName, string funcArg, List<PythonParam> varList, string pyScript)
        {
            return CallFunc<T>(new string[0], funcName, funcArg, varList, pyScript);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblys"></param>
        /// <param name="funcName"></param>
        /// <param name="funcArg"></param>
        /// <param name="varList"></param>
        /// <param name="pyScript"></param>
        /// <returns></returns>
        public static T CallFunc<T>(string[] assemblys, string funcName, string funcArg, List<PythonParam> varList, string pyScript)
        {
            PythonContext context;
            ExecuteCode<string>(assemblys, pyScript, SourceCodeKind.Statements, varList.ToArray(), out context);
            if (context != null)
            {
                var funcMain = context.GetVariable<Func<string, T>>(funcName);
                if (funcMain != null)
                {
                    return funcMain(funcArg);
                }
            }
            return default(T);
        }
    }
}