namespace LoteTablas.Framework.Common.Cache
{
    public interface IInMemoryCache
    {
        T? Get<T>(string key);
        T? Add<T>(string key, T value, DateTimeOffset expiresAt);
        void Remove(string key);
    }
}
