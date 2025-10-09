using Grpc.Net.Client;
using LoteTablas.Framework.Common.Azure;
using LoteTablas.Framework.Common.Cache;
using LoteTablas.Framework.Common.Configuration;
using LoteTablas.Framework.Common.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Formatting.Compact;

namespace LoteTablas.Framework.Common
{
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Injects IConfiguration and IAppConfiguration
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddSingleton(configuration)
                .AddSingleton<IAppConfiguration, AppConfiguration>();



        /// <summary>
        /// Adds Serilog Logging
        /// </summary>
        /// <param name="services">instance of IServiceCollection</param>
        /// <param name="configuration">current app's IConfiguration</param>
        /// <returns></returns>
        public static IServiceCollection AddSerilogLogging(this IServiceCollection services, IConfiguration configuration)
        {

            var provider = services.BuildServiceProvider();

            Log.Logger = new LoggerConfiguration()
              .WriteTo.Console(new CompactJsonFormatter())
              .ReadFrom.Configuration(configuration)
              .Enrich.WithSpan()
              .CreateLogger();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
                loggingBuilder.AddDebug();
                loggingBuilder.AddSerilog();
            });

            return services;
        }


        /// <summary>
        /// Injects IDatabaseConnection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDatabaseConnection(this IServiceCollection services) =>
            services.AddSingleton<IDatabaseConnection, DatabaseConnection>();



        /// <summary>
        /// Injects IAzureBlobServiceClient
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddBlobService(this IServiceCollection services) =>
            services.AddSingleton<IAzureBlobService, AzureBlobService>();


        /// <summary>
        /// Injects IAzureServiceBus
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBus(this IServiceCollection services) =>
            services.AddSingleton<IAzureServiceBus, AzureServiceBus>();

        /// <summary>
        /// Injects IInMemoryCache
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInMemoryCache(this IServiceCollection services) =>
            services.AddMemoryCache().AddSingleton<IInMemoryCache, InMemoryCache>();

        /// <summary>
        /// Injects Grpc Services from the Upstream config section 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IServiceCollection AddGrpcUpstreams(this IServiceCollection services, IConfiguration configuration, Dictionary<string, Type> types)
        {
            foreach (IConfigurationSection item in configuration.GetSection("Upstreams").GetChildren())
            {
                if (!types.TryGetValue(item.Key, out var type))
                    continue;

                services.TryAddSingleton(type, (IServiceProvider p) => Activator.CreateInstance(type, GrpcChannel.ForAddress(item.Value)));

            }

            return services;
        }


    }
}
