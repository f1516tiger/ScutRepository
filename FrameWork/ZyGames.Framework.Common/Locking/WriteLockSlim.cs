
using System;

namespace ZyGames.Framework.Common.Locking
{
    /// <summary>
    /// 过期不使用
    /// </summary>
    [Obsolete]
    class WriteLockSlim : ILocking
    {
        private readonly Func<int, bool> _enterCallback;
        private readonly Action _exitCallback;
        private readonly int _timeOut;

        public WriteLockSlim(Func<int, bool> enterCallback, Action exitCallback, int timeOut = 1000)
        {
            if (enterCallback == null)
            {
                throw new ArgumentNullException("enterCallback");
            }
            if (exitCallback == null)
            {
                throw new ArgumentNullException("exitCallback");
            }
            _enterCallback = enterCallback;
            _exitCallback = exitCallback;
            _timeOut = timeOut;
        }

        public bool IsLocked
        {
            get;
            private set;
        }

        public bool TryEnterLock()
        {
            IsLocked = _enterCallback(_timeOut);
            return IsLocked;
        }

        public void Dispose()
        {
            if (IsLocked && _exitCallback != null)
            {
                _exitCallback();
            }
        }


    }
}