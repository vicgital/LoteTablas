using LoteTablas.Application.Contracts.Cache;
using LoteTablas.Infrastructure.Cache.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace LoteTablas.Infrastructure.Cache.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInMemoryCache(this IServiceCollection services) =>
            services.AddMemoryCache().AddSingleton<IMemoryCacheManager, MemoryCacheManager>();
    }
}
