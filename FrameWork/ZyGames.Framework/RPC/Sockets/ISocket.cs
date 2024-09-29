﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ZyGames.Framework.RPC.Sockets
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ISocket
    {
        /// <summary>
        /// not proccess buildpack
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="buffer"></param>
        /// <param name="callback"></param>
        protected internal abstract Task<bool> SendAsync(ExSocket socket, byte[] buffer, Action<SocketAsyncResult> callback);

        /// <summary>
        /// has trigger CloseHandshake method
        /// </summary>
        /// <param name="ioEventArgs"></param>
        /// <param name="opCode"></param>
        /// <param name="reason"></param>
        protected internal abstract void Closing(SocketAsyncEventArgs ioEventArgs, sbyte opCode, string reason);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public abstract Task PostSend(ExSocket socket, byte[] data, int offset, int count);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="opCode"></param>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public abstract Task PostSend(ExSocket socket, sbyte opCode, byte[] data, int offset, int count);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="opCode"></param>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="callback"></param>
        public abstract Task PostSend(ExSocket socket, sbyte opCode, byte[] data, int offset, int count, Action<SocketAsyncResult> callback);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        public abstract void Ping(ExSocket socket);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        public abstract void Pong(ExSocket socket);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="reason"></param>
        public abstract void CloseHandshake(ExSocket socket, string reason);
    }
}
