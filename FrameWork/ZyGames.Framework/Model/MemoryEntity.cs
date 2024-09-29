
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ProtoBuf;

namespace ZyGames.Framework.Model
{
    /// <summary>
    /// 内存中一定时间存在的实体，不存储数据库
    /// </summary>
    [ProtoContract, Serializable]
    public class MemoryEntity : EntityChangeEvent, IDataExpired, ISqlEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public MemoryEntity()
            : base(true)
        {
        }

        /// <summary>
        /// entity modify time.
        /// </summary>
        [ProtoMember(100025)]
        public DateTime TempTimeModify { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override void InitializeChangeEvent()
        {
            //禁用Change事件绑定
        }

        /// <summary>k
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual bool RemoveExpired(string key)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string GetKeyCode()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual int GetMessageQueueId()
        {
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        public string PersonalId { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDelete { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual void ResetState()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Reset()
        {
            ResetState();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual DateTime GetCreateTime()
        {
            return DateTime.Now;
        }

    }
}