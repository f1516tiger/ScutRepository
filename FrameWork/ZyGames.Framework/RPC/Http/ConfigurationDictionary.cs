
using System;
using System.Collections.Generic;

namespace ZyGames.Framework.RPC.Http
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ConfigurationDictionary
    {
        readonly Dictionary<string, List<string>> _values;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public ConfigurationDictionary(Dictionary<string, List<string>> values)
        {
            _values = values;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ConfigurationDictionary Parse(IEnumerable<string> args)
        {
            var values = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

            foreach (var arg in args)
            {
                // Split at first '=':
                int eqidx;
                if ((eqidx = arg.IndexOf('=')) == -1) continue;

                string key, value;
                key = arg.Substring(0, eqidx);
                value = arg.Substring(eqidx + 1);

                // Create the list of values for the key if necessary:
                List<string> list;
                if (!values.TryGetValue(key, out list))
                {
                    list = new List<string>();
                    values.Add(key, list);
                }

                // Add the value to the list:
                list.Add(value);
            }

            return new ConfigurationDictionary(values);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string SingleValue(string key)
        {
            List<string> list;
            if (!_values.TryGetValue(key, out list) || list.Count == 0)
                throw new Exception(String.Format("Configuration key '{0}' is required", key));
            if (list.Count > 1)
                throw new Exception(String.Format("Configuration key '{0}' has more than one value", key));
            return list[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public string SingleValueOrDefault(string key, string defaultValue)
        {
            List<string> list;
            if (!_values.TryGetValue(key, out list) || list.Count == 0)
                return defaultValue;
            if (list.Count > 1)
                throw new Exception(String.Format("Configuration key '{0}' has more than one value", key));
            return list[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> Values(string key)
        {
            List<string> list;
            if (!_values.TryGetValue(key, out list))
                throw new Exception(String.Format("Configuration key '{0}' is required", key));
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool TryGetValue(string key, out List<string> values)
        {
            return _values.TryGetValue(key, out values);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetSingleValue(string key, out string value)
        {
            value = null;
            List<string> list;
            if (!_values.TryGetValue(key, out list)) return false;

            if (list.Count > 1)
                throw new Exception(String.Format("Configuration key '{0}' has more than one value", key));
            value = list[0];
            return true;
        }
    }
}
