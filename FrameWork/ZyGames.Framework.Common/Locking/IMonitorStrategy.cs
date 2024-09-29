
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZyGames.Framework.Common.Locking
{
    /// <summary>
    /// Monitor锁策略接口
    /// </summary>
    public interface IMonitorStrategy
    {
        /// <summary>
        /// 尝试进入锁
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        bool TryEnterLock(LockCallback handle);
        /// <summary>
        /// 获取锁操作接口
        /// </summary>
        /// <returns></returns>
        ILocking Lock();
    }
}