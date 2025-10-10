using LoteTablas.Application.Contracts.Cache;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace LoteTablas.Infrastructure.Cache.Managers
{
    public class MemoryCacheManager(IMemoryCache memoryCache) : IMemoryCacheManager
    {
        private readonly IMemoryCache _memoryCache = memoryCache;

        public T? Add<T>(string key, T value, DateTimeOffset expiresAt)
        {
            var copyValue = CopyValue<T>(value);
            return _memoryCache.Set(key, copyValue, expiresAt);
        }

        public T? Get<T>(string key)
        {
            if (_memoryCache.TryGetValue<T>(key, out var result))
            {
                return result is null ? default : CopyValue(result);
            }

            return default;
        }

        public void Remove(string key) =>
            _memoryCache.Remove(key);

        /// <summary>
        /// Create a copy of the value
        /// to avoid updating reference data(in memory)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        private static T? CopyValue<T>(T result)
        {
            if (result is null)
                return default;

            var serializedValue = JsonSerializer.Serialize(result);
            return JsonSerializer.Deserialize<T>(serializedValue);
        }

    }
}
