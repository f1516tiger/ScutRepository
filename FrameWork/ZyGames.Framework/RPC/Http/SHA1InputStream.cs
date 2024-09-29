
using System;
using System.IO;
using System.Security.Cryptography;

namespace ZyGames.Framework.RPC.Http
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SHA1InputStream : Stream
    {
        private long position;
        private SHA1 sha1;
        private Stream input;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public SHA1InputStream(Stream input)
        {
            this.input = input;

            this.sha1 = SHA1.Create();
            this.position = 0L;
        }
        /// <summary>
        /// 
        /// </summary>
        public override bool CanRead { get { return true; } }
        /// <summary>
        /// 
        /// </summary>
        public override bool CanSeek { get { return false; } }
        /// <summary>
        /// 
        /// </summary>
        public override bool CanWrite { get { return false; } }
        /// <summary>
        /// 
        /// </summary>
        public override void Flush()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        public override long Length
        {
            get { return input.Length; }
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
            int nr = input.Read(buffer, offset, count);
            if (nr == 0) return 0;

            sha1.TransformBlock(buffer, offset, nr, null, 0);
            position += nr;

            return nr;
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
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
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
