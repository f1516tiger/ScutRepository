
using System;

namespace ZyGames.Framework.Common.Event
{
    /// <summary>
    /// Time out notify event args
    /// </summary>
    public class TimeoutNotifyEventArgs : NotifyEventArgs
    {
        /// <summary>
        /// init
        /// </summary>
        /// <param name="secTimeout"></param>
        public TimeoutNotifyEventArgs(int secTimeout)
            : this(new TimeSpan(0, 0, secTimeout))
        {
        }

        /// <summary>
        /// init
        /// </summary>
        public TimeoutNotifyEventArgs(TimeSpan timeout)
        {
            ExpiredTime = DateTime.Now.Add(timeout);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected internal override bool Check()
        {
            return DateTime.Now >= ExpiredTime &&
                base.Check();
        }

    }
}
