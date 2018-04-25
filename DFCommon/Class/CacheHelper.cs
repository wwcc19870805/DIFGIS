using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DFCommon.Class
{
    /// <summary>
    /// 用于操作自定义缓存
    /// </summary>
    public class CacheHelper
    {
        private static Dictionary<string,object> Cache = new Dictionary<string,object>();
        public static object SetCache(string key, object val)
        {
            if (!Cache.ContainsKey(key))
                Cache.Add(key, val);
            return Cache[key];
        }

        public static object GetCache(string key)
        {
            if (Cache.ContainsKey(key))
                return Cache[key];
            else return null;
        }

        public static List<string> GetKeys(object obj)
        {
            List<string> keys = new List<string>();
            foreach (KeyValuePair<string, object> kp in Cache)
            {
                if (obj == kp.Value) keys.Add(kp.Key);
            }
            return keys;
        }

        public static void RemoveCache(string key)
        {
            if (Cache[key] != null)
                Cache.Remove(key);
        }

        public static void RemoveAllCache()
        {
            Cache.Clear();
        }
    }
}
