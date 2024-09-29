
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZyGames.Framework.RPC.Sockets.WebSocket
{
    /// <summary>
    /// 
    /// </summary>
    public class DataSegmentFrame
    {
        /// <summary>
        /// 
        /// </summary>
        public MessageHeadFrame Head { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ArraySegment<byte> Data { get; set; }
    }
}
