﻿
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ZyGames.Framework.RPC.Http
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SHA1TextReader : TextReader
    {
        readonly SHA1 sha1;
        readonly TextReader input;
        readonly Encoder encoder;

        readonly static byte[] dum = new byte[0];
        bool isFinal = false;

        readonly char[] ibuf;
        readonly byte[] obuf;
        int ibufIndex;
        const int bufferSize = 8192;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encoding"></param>
        public SHA1TextReader(TextReader input, Encoding encoding)
            : base()
        {
            this.input = input;
            this.encoder = encoding.GetEncoder();

            this.sha1 = SHA1.Create();

            this.ibuf = new char[bufferSize];
            this.obuf = new byte[bufferSize];
            this.ibufIndex = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int Peek()
        {
            return input.Peek();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int Read()
        {
            int c = input.Read();
            if (c == -1)
            {
                transformBuffer();
                return c;
            }

            ibuf[ibufIndex++] = (char)c;
            if (ibufIndex >= bufferSize)
            {
                transformBuffer();
            }
            return c;
        }

        private void transformBuffer()
        {
            if (ibufIndex == 0) return;

            // Encode the characters using the encoder and SHA1 those bytes:
            int nb = encoder.GetBytes(ibuf, 0, ibufIndex, obuf, 0, false);
            sha1.TransformBlock(obuf, 0, nb, null, 0);

            ibufIndex = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetHash()
        {
            if (!isFinal)
            {
                transformBuffer();

                sha1.TransformFinalBlock(dum, 0, 0);
                isFinal = true;
            }

            return sha1.Hash;
        }
    }
}
