﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZyGames.Framework.Common.Configuration;

namespace ZyGames.Framework.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class EntitySection : ConfigSection
    {
        /// <summary>
        /// 
        /// </summary>
        public EntitySection()
        {
            LogTableNameFormat = ConfigUtils.GetSetting("Log.TableName.Format", "log_$date{0}");
            LogPriorBuildMonth = ConfigUtils.GetSetting("Log.PriorBuild.Month", 2);
            EnableModifyTimeField = ConfigUtils.GetSetting("Schema.EnableModifyTimeField", false);
        }

        /// <summary>
        /// Log table name format string.
        /// </summary>
        public string LogTableNameFormat { get; set; }

        /// <summary>
        /// prior build month table, default:3
        /// </summary>
        public int LogPriorBuildMonth { get; set; }

        /// <summary>
        /// enable field of ModifyTime
        /// </summary>
        public bool EnableModifyTimeField { get; set; }
    }
}
