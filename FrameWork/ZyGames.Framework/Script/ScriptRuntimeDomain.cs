
using System;
using System.Reflection;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Log;

namespace ZyGames.Framework.Script
{
    /// <summary>
    /// 
    /// </summary>
    public class ScriptRuntimeDomain : IDisposable
    {
        private AppDomain _currDomain;
        private ScriptDomainContext _context;
        private ScriptRuntimeScope _scope;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="privateBinPaths"></param>
        /// <returns></returns>
        public ScriptRuntimeDomain(string name, string[] privateBinPaths)
        {
            //AppDomainSetup setup = new AppDomainSetup();
            //setup.ApplicationName       = name;
            //setup.ApplicationBase       = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            //setup.PrivateBinPath        = string.Join(";", privateBinPaths);
            //setup.CachePath             = setup.ApplicationBase;
            //setup.ShadowCopyFiles       = "true";
            //setup.ShadowCopyDirectories = setup.ApplicationBase;
            InitDomain(name);
        }
 
        private void InitDomain(string name)
        {
#if STATIC

#else
            var handle = AppDomain.CurrentDomain.CreateInstance(name, name);
            if (handle != null)
            {
                _currDomain               = handle.Unwrap() as AppDomain;
          //      _currDomain

            }

            var type = typeof(ScriptDomainContext);
            _context = (ScriptDomainContext)_currDomain.CreateInstanceFromAndUnwrap(type.Assembly.GetName().CodeBase, type.FullName);
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        public ScriptRuntimeScope Scope
        {
            get { return _scope; }
        }

        /// <summary>
        /// Main function args.
        /// </summary>
        public string[] MainArgs { get; set; }

        /// <summary>
        /// IMainScript
        /// </summary>
        public IMainScript MainInstance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        internal string PrivateBinPath
        {
            get { return _currDomain.SetupInformation.ApplicationBase; }
        }

        internal void LoadAssembly(string key, string assemblyName)
        {
#if STATIC
            Assembly.LoadFrom(assemblyName);
#else
            _context.LoadAssembly(key, assemblyName);
#endif
        }
        private ScriptRuntimeScope CreateRuntimeScope(ScriptSettupInfo settupInfo, string amsKey, Type type)
        {
#if STATIC
            return type.CreateInstance<ScriptRuntimeScope>(settupInfo);
#else
            return _context.GetInstance(amsKey, type.FullName, settupInfo) as ScriptRuntimeScope;
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        public ScriptRuntimeScope CreateScope(ScriptSettupInfo settupInfo)
        {
            var type = typeof(ScriptRuntimeScope);
            string amsKey = type.Assembly.GetName().Name;
            _scope = CreateRuntimeScope(settupInfo, amsKey, type);
            if (_scope != null)
            {
                _scope.Init();
            }
            return _scope;
        }


        /// <summary>
        /// 
        /// </summary>
        public void Unload()
        {
            try
            {
                if (_currDomain != null)
                {
                    AppDomain.Unload(_currDomain);
                }
            }
            catch (Exception ex)
            {
                TraceLog.WriteError("Script domain error:{0}", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Unload();
            _currDomain = null;
            _scope.Dispose();
            _scope = null;
            _context = null;
            GC.SuppressFinalize(this);
        }

    }
}
