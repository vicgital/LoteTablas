using LoteTablas.Application.Contracts.Cache;
using LoteTablas.Application.Contracts.Configuration;
using LoteTablas.Infrastructure.Configuration.Constants;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace LoteTablas.Infrastructure.Cache.Managers
{
    public class MemoryCacheManager(
        IMemoryCache memoryCache,
        IAppConfigurationManager appConfigurationManager) : IMemoryCacheManager
    {
        private readonly IMemoryCache _memoryCache = memoryCache;
        private readonly IAppConfigurationManager _appConfigurationManager = appConfigurationManager;

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

        public int GetCacheDuration() =>
            int.Parse(_appConfigurationManager.GetValue(EnvironmentVariableNames.CACHE_DURATION, "5"));
    }
}
