
using System;
using System.IO;
using System.Security.Cryptography;

namespace ZyGames.Framework.RPC.Http
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SHA1OutputStream : Stream
    {
        private long position;
        private SHA1 sha1;
        private Stream output;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="output"></param>
        public SHA1OutputStream(Stream output)
        {
            this.output = output;

            this.sha1 = SHA1.Create();
            this.position = 0L;
        }
        /// <summary>
        /// 
        /// </summary>
        public override bool CanRead { get { return false; } }
        /// <summary>
        /// 
        /// </summary>
        public override bool CanSeek { get { return false; } }
        /// <summary>
        /// 
        /// </summary>
        public override bool CanWrite { get { return true; } }
        /// <summary>
        /// 
        /// </summary>
        public override void Flush()
        {
            output.Flush();
        }
        /// <summary>
        /// 
        /// </summary>
        public override long Length
        {
            get { return output.Length; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override long Position
        {
            get
            {
                return this.position;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void SetLength(long value)
        {
            output.SetLength(value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            sha1.TransformBlock(buffer, offset, count, null, 0);
            output.Write(buffer, offset, count);
            position += count;
        }

        private readonly static byte[] dum = new byte[0];
        private bool isFinal = false;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetHash()
        {
            if (!isFinal)
            {
                sha1.TransformFinalBlock(dum, 0, 0);
                isFinal = true;
            }

            return sha1.Hash;
        }
    }
}
