
using System;
using System.Diagnostics;
using ZyGames.Framework.Common.Log;

namespace ZyGames.Framework.Plugin.Test
{
    /// <summary>
    /// 
    /// </summary>
    public static class CaseManager
    {
        private static event CaseEventHandle Casehandle;

        static CaseManager()
        {
            Casehandle += new CaseEventHandle(ProcessCase);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseCase"></param>
        public static void Add(BaseCase baseCase)
        {
            var args = new CaseEventArgs() { Case = baseCase };

            CaseEventHandle temp = Casehandle;
            if (temp != null)
            {
                foreach (CaseEventHandle handler in temp.GetInvocationList())
                {
                    handler.BeginInvoke(null, args, new AsyncCallback(EndAsync), null);
                }
            }
        }

        private static void EndAsync(IAsyncResult ar)
        {
        }

        private static void ProcessCase(object sender, CaseEventArgs args)
        {
            if (args == null || args.Case == null)
            {
                return;
            }

            try
            {
                args.Case.TestCase();
            }
            catch (Exception ex)
            {
                string msg = string.Format("\"{0}\"用例>>测试失败:{1}", args.Case.Name, ex);
                TraceLog.WriteLine(msg);
            }
        }
    }
}