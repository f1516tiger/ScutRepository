
using System;
using ProtoBuf;
using ZyGames.Framework.Common.Serialization;

namespace ZyGames.Framework.Data
{
    /// <summary>
    /// sql
    /// </summary>
    [ProtoContract, Serializable]
    public class SqlParam
    {
        /// <summary>
        /// ParamName
        /// </summary>
        [ProtoMember(1)]
        public string ParamName;

        /// <summary>
        /// DbType
        /// </summary>
        [ProtoMember(2)]
        public int DbTypeValue;

        /// <summary>
        /// Size
        /// </summary>
        [ProtoMember(3)]
        public int Size;

        /// <summary>
        /// Value
        /// </summary>
        [ProtoMember(4)]
        public ProtoObject Value;
    }
}