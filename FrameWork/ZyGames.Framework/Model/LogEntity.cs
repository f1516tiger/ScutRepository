
using System;
using ProtoBuf;

namespace ZyGames.Framework.Model
{
    /// <summary>
    /// 日志实体基类
    /// </summary>
    [ProtoContract, Serializable]
    public abstract class LogEntity : AbstractEntity
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="ZyGames.Framework.Model.LogEntity"/> class.
		/// </summary>
        protected LogEntity()
            : base(false)
        {

        }

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected internal override int GetIdentityId()
		{
			return DefIdentityId;
		}

    }
}