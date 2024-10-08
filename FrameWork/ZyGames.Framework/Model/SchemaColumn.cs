﻿
using System;
using System.Collections.Generic;

namespace ZyGames.Framework.Model
{
    /// <summary>
    /// 实体架构的列信息
    /// </summary>
    public class SchemaColumn
    {
        /// <summary>
        /// 
        /// </summary>
        public SchemaColumn()
        {
            DbType = ColumnDbType.Varchar;
            Isnullable = true;
            CanRead = true;
            CanWrite = true;
        }

        /// <summary>
        /// 反射出的类型参数的个数
        /// </summary>
        internal int GenericArgs { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 列类型
        /// </summary>
        public Type ColumnType
        {
            get;
            set;
        }

        /// <summary>
        /// 是否支持可读
        /// </summary>
        public bool CanRead { get; set; }
        /// <summary>
        /// 是否支持可写
        /// </summary>
        public bool CanWrite { get; set; }

        /// <summary>
        /// 列的大小长度
        /// </summary>
        public int ColumnLength { get; set; }
        /// <summary>
        /// 列的小数位数
        /// </summary>
        public int ColumnScale { get; set; }
        /// <summary>
        /// 列允许为空
        /// </summary>
        public bool Isnullable { get; set; }

        /// <summary>
        /// 读写模式
        /// </summary>
        public FieldModel FieldModel
        {
            get;
            set;
        }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsKey
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsUnique { get; set; }

        /// <summary>
        /// 是否自增
        /// </summary>
        public bool IsIdentity
        {
            get;
            set;
        }

        /// <summary>
        /// 自增开始编号
        /// </summary>
        public int IdentityNo { get; set; }

        /// <summary>
        /// 禁用或排除数据库取值
        /// </summary>
        public bool Disable
        {
            get;
            set;
        }

        /// <summary>
        /// 是否使用Json序列化
        /// </summary>
        [Obsolete("use IsSerialized.")]
        public bool IsJson
        {
            get { return IsSerialized; }
            set { IsSerialized = value; }
        }

        /// <summary>
        /// 是否序列化存储
        /// </summary>
        public bool IsSerialized
        {
            get;
            set;
        }

        /// <summary>
        /// 使用Json日期格式化
        /// </summary>
        public string JsonDateTimeFormat
        {
            get;
            set;
        }
        /// <summary>
        /// 取值范围
        /// </summary>
        public int MinRange
        {
            get;
            set;
        }

        /// <summary>
        /// 取值范围
        /// </summary>
        public int MaxRange
        {
            get;
            set;
        }

        /// <summary>
        /// Db映射类型
        /// </summary>
        public ColumnDbType DbType
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasChild { get { return Children != null && Children.Count > 0; } }

        /// <summary>
        /// 子类集合
        /// </summary>
        public List<SchemaColumn> Children { get; set; }


        /// <summary>
        /// The column Is list child type.
        /// </summary>
        public bool IsList
        {
            get
            {
                return GenericArgs == 1;
            }
        }
        /// <summary>
        /// The column Is dictionary child type.
        /// </summary>
        public bool IsDictionary
        {
            get
            {
                return GenericArgs == 2;
            }
        }
    }
}