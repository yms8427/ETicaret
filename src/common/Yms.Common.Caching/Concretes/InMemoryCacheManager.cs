using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using Yms.Common.Caching.Abstractions;

namespace Yms.Common.Caching.Concretes
{
    class InMemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache memoryCache;

        public InMemoryCacheManager(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public void Add<T>(string key, T data) where T : class
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(15)
            };
            memoryCache.Set(key, data, options);
        }

        public T Get<T>(string key) where T : class
        {
            if (memoryCache.TryGetValue(key, out T value))
            {
                return value;
            }
            return null;
        }

        public T GetOrCreate<T>(string key, Func<T> function) where T : class
        {
            var cachedValue = Get<T>(key);
            if (cachedValue == null)
            {
                var rawData = function.Invoke();
                Add(key, rawData);
                return rawData;
            }
            return cachedValue;
        }

        public bool Update<T>(string key, Action<T> action) where T : class
        {
            var cachedValue = Get<T>(key);
            if (cachedValue != null)
            {
                action(cachedValue);
                return true;
            }
            return false;
        }
    }
}
