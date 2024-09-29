
using System;

namespace ZyGames.Framework.Data
{
    /// <summary>
    /// 数据列对象
    /// </summary>
    public class DbColumn
    {
        /// <summary>
        /// 
        /// </summary>
        public DbColumn()
        {
            Isnullable = true;
        }

        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Sets key is true in Entity field
        /// </summary>
        public bool IsKey { get; set; }

        /// <summary>
        /// Get Db table have key no
        /// </summary>
        public int KeyNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsUnique { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public long Length { get; set; }
        /// <summary>
        /// decimal类型指精度范围
        /// </summary>
        public int Scale { get; set; }
        /// <summary>
        /// 是否可为空
        /// </summary>
        public bool Isnullable { get; set; }
        /// <summary>
        /// 是否是修改列，False则新增列
        /// </summary>
        public bool IsModify { get; set; }

		/// <summary>
		/// 是否是自增列
		/// </summary>
		/// <value><c>true</c> if this instance is identity; otherwise, <c>false</c>.</value>
		public bool IsIdentity{ get; set; }


        /// <summary>
        /// 自增开始编号
        /// </summary>
        public int IdentityNo { get; set; }

        /// <summary>
        /// DB是否有自增编号
        /// </summary>
        public bool HaveIncrement { get; set; }
        /// <summary>
        /// Db映射类型
        /// </summary>
        public string DbType
        {
            get;
            set;
        }

    }
}