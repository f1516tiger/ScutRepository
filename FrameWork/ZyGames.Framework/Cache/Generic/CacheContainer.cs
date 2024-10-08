﻿
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Model;

namespace ZyGames.Framework.Cache.Generic
{
    /// <summary>
    /// 缓存容器对象，每种T类型实体分配一个容器对象；
    /// </summary>
    [ProtoContract, Serializable]
    public class CacheContainer : BaseDisposable
    {
        private BaseCollection _collection;

        internal CacheContainer(bool isReadOnly)
        {
            _collection = isReadOnly
                ? (BaseCollection)new ReadonlyCacheCollection()
                : new CacheCollection(true);
            LoadingStatus = LoadingStatus.None;
        }

        /// <summary>
        /// 是否数据已加载成功
        /// </summary>
        public bool HasLoadSuccess
        {
            get { return LoadingStatus == LoadingStatus.Success; }
        }

        /// <summary>
        /// 加载状态
        /// </summary>
        public LoadingStatus LoadingStatus
        {
            get;
            private set;
        }

        /// <summary>
        /// 数据是否为空
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return _collection == null;
            }
        }
        /// <summary>
        /// 缓存项(CacheItemSet)的集合
        /// 主键:实体Key或PersonalId
        /// 键值：缓存项CacheItemSet（包括过期配置对象，缓存具体实体或实体集合）
        /// </summary>
        public BaseCollection Collection
        {
            get
            {
                return _collection;
            }
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadOnly
        {
            get { return _collection.IsReadOnly; }
        }


        /// <summary>
        /// 释放
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //释放 托管资源 
                if (_collection != null)
                {
                    _collection.Dispose();
                }
                _collection = null;
            }
            base.Dispose(disposing);
        }

        internal void ResetStatus()
        {
            if (_collection == null)
            {
                return;
            }
            LoadingStatus = LoadingStatus.None;
        }

        /// <summary>
        /// 执行加载数据工厂
        /// </summary>
        /// <param name="loadFactory"></param>
        /// <param name="isReload">是否重新加载</param>
        /// <exception cref="NullReferenceException"></exception>
        internal void OnLoadFactory(Func<bool, bool> loadFactory, bool isReload)
        {
            if (_collection == null)
            {
                TraceLog.WriteError("LoadFactory loaded fail,collection is null");
                return;
            }
            //重新加载或未加载成功时，执行加载数据工厂
            if (isReload || !HasLoadSuccess)
            {
                if (loadFactory != null)
                {
                    LoadingStatus = loadFactory(isReload) ? LoadingStatus.Success : LoadingStatus.Error;
                }
            }
        }
    }
}