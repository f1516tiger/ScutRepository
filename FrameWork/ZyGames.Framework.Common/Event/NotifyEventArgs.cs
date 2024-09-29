
using System;
using ZyGames.Framework.Common.Log;

namespace ZyGames.Framework.Common.Event
{
    /// <summary>
    /// 
    /// </summary>
    public delegate void NotifyCallback(NotifyEventArgs e);

    /// <summary>
    /// Notify event args
    /// </summary>
    public class NotifyEventArgs : EventArgs
    {
        /// <summary>
        /// Whether to interrupt
        /// </summary>
        public bool Interrupt { get; set; }

        /// <summary>
        /// User object
        /// </summary>
        public object Target { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ExpiredTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public event NotifyCallback Callback;

        /// <summary>
        /// Check where
        /// </summary>
        /// <returns></returns>
        internal protected virtual bool Check()
        {
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        internal void OnCallback()
        {
            //try
            //{
                NotifyCallback handler = Callback;
                if (handler != null) handler(this);
            //}
            //catch (Exception ex)
            //{
            //    TraceLog.WriteError("Notify callback error:{0}", ex);
            //}
        }

    }
}
