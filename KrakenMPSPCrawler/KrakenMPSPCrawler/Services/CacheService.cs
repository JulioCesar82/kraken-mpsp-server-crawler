using System.Collections.Generic;

namespace KrakenMPSPCrawler.Services
{
    public static class CacheService
    {
        private static readonly Dictionary<string, string> StringCache = new Dictionary<string, string>();
        private static readonly Dictionary<string, object> ObjectCache = new Dictionary<string, object>();

        public static string TryGetCache(string key)
        {
            if (StringCache.ContainsKey(key)) return StringCache[key];
            return null;
        }

        public static void TrySetCache(string key, string value)
        {
            if (!StringCache.ContainsKey(key))
            {
                StringCache.Add(key, value);
            }
        }

        public static object TryGetCacheObject(string key)
        {
            if (ObjectCache.ContainsKey(key)) return ObjectCache[key];
            return null;
        }

        public static void TrySetCacheObject(string key, object value)
        {
            if (!ObjectCache.ContainsKey(key))
            {
                ObjectCache.Add(key, value);
            }
        }
    }
}
