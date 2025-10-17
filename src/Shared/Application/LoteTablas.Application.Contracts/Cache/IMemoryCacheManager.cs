namespace LoteTablas.Application.Contracts.Cache
{
    public interface IMemoryCacheManager
    {
        T? Get<T>(string key);
        T? Add<T>(string key, T value, DateTimeOffset expiresAt);
        void Remove(string key);
        int GetCacheDuration();
    }
}
