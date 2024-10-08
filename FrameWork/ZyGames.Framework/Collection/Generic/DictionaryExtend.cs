﻿
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ZyGames.Framework.Collection.Generic
{
    /// <summary>
    /// Thread safe of extend dictionary 
    /// </summary>
    [Serializable]
    public class DictionaryExtend<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly ConcurrentDictionary<TKey, TValue> _cacheStruct;

        /// <summary>
        /// 
        /// </summary>
        public DictionaryExtend()
        {
            _cacheStruct = new ConcurrentDictionary<TKey, TValue>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _cacheStruct.GetEnumerator();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _cacheStruct.TryAdd(item.Key, item.Value);
        }
        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            _cacheStruct.Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _cacheStruct.ContainsKey(item.Key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            int index = 0;
            var er = _cacheStruct.GetEnumerator();
            while (er.MoveNext())
            {
                if (index == arrayIndex && index < array.Length)
                {
                    array[index] = er.Current;
                }
                index++;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            TValue value;
            return _cacheStruct.TryRemove(item.Key, out value);
        }
        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get { return _cacheStruct.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            return _cacheStruct.ContainsKey(key);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            TryAdd(key, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryAdd(TKey key, TValue value)
        {
            return _cacheStruct.TryAdd(key, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        /// <returns></returns>
        public bool TryUpdate(TKey key, TValue newValue, TValue oldValue)
        {
            return _cacheStruct.TryUpdate(key, newValue, oldValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public TValue GetOrAdd(TKey key, Func<TKey,TValue> factory )
        {
            return _cacheStruct.GetOrAdd(key, factory);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return _cacheStruct.TryGetValue(key, out value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            TValue value;
            return TryRemove(key, out value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryRemove(TKey key, out TValue value)
        {
            return _cacheStruct.TryRemove(key, out value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get { return _cacheStruct.ContainsKey(key) ? _cacheStruct[key] : default(TValue); }
            set { _cacheStruct[key] = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public ICollection<TKey> Keys
        {
            get { return _cacheStruct.Keys; }
        }
        /// <summary>
        /// 
        /// </summary>
        public ICollection<TValue> Values
        {
            get { return _cacheStruct.Values; }
        }
    }
}