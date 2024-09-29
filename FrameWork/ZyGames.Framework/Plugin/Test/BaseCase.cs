
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ZyGames.Framework.Common.Log;

namespace ZyGames.Framework.Plugin.Test
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    /// <code>
    /// public class DemoCase : BaseCase
    /// {
    ///     public DemoCase() : base("Demo")
    ///     {
    ///     }
    /// 
    ///     public void TestCase()
    ///     {
    ///         //do samething
    ///     }
    /// }
    /// </code>
    /// </example>
    public abstract class BaseCase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        protected BaseCase(string name)
        {
            this.Name = name;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public abstract void TestCase();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="equalValue"></param>
        public void InspectEquals(object value, object equalValue)
        {
            if (!Equals(value, equalValue))
            {
                throw new Exception(string.Format("{0}:比较值不相等!", Name));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void InspectIsNull(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value", Name + ":验证值为空!");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void ThreadRun(ThreadStart action)
        {
            new Thread(action).Start();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Task TaskRun(Action action)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    if (action != null)
                    {
                        action();
                    }
                }
                catch (Exception ex)
                {
                    TraceLog.WriteError("TaskRun:{0}", ex);
                }
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Task TaskRun(Action<Stopwatch> action)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    if (action != null)
                    {
                        var watch = StartNewWatch();
                        action(watch);
                    }
                }
                catch (Exception ex)
                {
                    TraceLog.WriteError("TaskRun:{0}", ex);
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public Task TaskRun(string name, Action<string, Stopwatch> action)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    if (action != null)
                    {
                        var watch = StartNewWatch();
                        action(name, watch);
                    }
                }
                catch (Exception ex)
                {
                    TraceLog.WriteError("TaskRun:{0}", ex);
                }
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void WriteLine(string message)
        {
            TraceLog.WriteLine("{0}用例>>{1}", Name, message);
        }

        private Stopwatch StartNewWatch()
        {
            var watch = new Stopwatch();
            watch.Start();
            return watch;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msTimeout"></param>
        public void ThreadSleep(int msTimeout)
        {
            Thread.Sleep(msTimeout);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool TaskWaitAll(Task[] tasks, int timeout = 0)
        {
            if (timeout > 0)
            {
                return Task.WaitAll(tasks, timeout);
            }
            Task.WaitAll(tasks);
            return true;
        }
    }
}