
using System;

namespace ZyGames.Framework.Plugin.Test
{
    internal delegate void CaseEventHandle(object sender, CaseEventArgs args);

    internal class CaseEventArgs : EventArgs
    {
        public BaseCase Case
        {
            get;
            set;
        }
    }
}