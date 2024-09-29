
using System;
using ProtoBuf;

namespace ZyGames.Framework.Model
{
    /// <summary>
    /// 共享实体数据基类
    /// </summary>
    [ProtoContract, Serializable]
    public abstract class ShareEntity : AbstractEntity, IComparable<ShareEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        protected ShareEntity()
            : base(false)
        {

        }
		/// <summary>
		/// Initializes a new instance of the <see cref="ZyGames.Framework.Model.ShareEntity"/> class.
		/// </summary>
        /// <param name="isReadonly">If set to <c>true</c> is readonly. no used</param>
        protected ShareEntity(bool isReadonly)
            : base(isReadonly)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="access">no used</param>
        protected ShareEntity(AccessLevel access)
            : base(access)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public virtual int CompareTo(ShareEntity other)
        {
            return base.CompareTo(other);
        }
    }
}