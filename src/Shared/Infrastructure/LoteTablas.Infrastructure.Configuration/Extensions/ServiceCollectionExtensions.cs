using LoteTablas.Application.Contracts.Cache;
using LoteTablas.Application.Contracts.Configuration;
using LoteTablas.Infrastructure.Configuration.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoteTablas.Infrastructure.Configuration.Extensions
{
    public static class ServiceCollectionExtensions
    {
        // Add your extension methods here
        public static IServiceCollection AddConfigurationManager(this IServiceCollection services, IConfiguration config) =>
            services.AddSingleton<IConfiguration>(config).AddSingleton<IAppConfigurationManager, AppConfigurationManager>();
    }
}
