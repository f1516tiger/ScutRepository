
using System;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using Mono.Cecil;
using Mono.Cecil.Cil;
using ZyGames.Framework.Common.Log;

namespace ZyGames.Framework.Common.Build
{
    /// <summary>
    /// 实体属性重构生成
    /// </summary>
    public class WeavingEntityTask : System.Threading.Tasks.Task
    {
 
        public WeavingEntityTask() : base(_baseAction)
        {
            _baseAction = SearchForPropertiesAndAddMSIL;
        }

        private static Action _baseAction;
        private  void SearchForPropertiesAndAddMSIL()
        {
            string path = "";
            try
            {
                bool hasBuild = false;
                FilePattern = (FilePattern ?? "*.dll");
                if (!FilePattern.ToLower().EndsWith(".dll", StringComparison.Ordinal))
                {
                    FilePattern = FilePattern + ".dll";
                }
                var pathList = Directory.GetFiles(SolutionDir, FilePattern, SearchOption.AllDirectories);
                foreach (string assemblyPath in pathList)
                {
                    path = Path.GetFullPath(assemblyPath);
                    if (AssemblyBuilder.BuildToFile(path, SolutionDir))
                    {
                        hasBuild = true;
                    }
                }
                if (!hasBuild)
                {
                    TraceLog.WriteError("The model:\"" + FilePattern + "\" has not be builded.");
                }
            }
            catch (Exception ex)
            {
                TraceLog.WriteError("The model:\"" + FilePattern + "\" build error:" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// Gets or sets the solution dir.
        /// </summary>
        /// <value>The solution dir.</value>
        [Required]
        public string SolutionDir { get; set; }

        /// <summary>
        /// 过滤文件类型
        /// </summary>
        public string FilePattern { get; set; }

        /// <summary>
        /// assembly is support debug.
        /// </summary>
        public bool IsDebug { get; set; }


    }
}