using LoteTablas.Infrastructure.Configuration.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace LoteTablas.Infrastructure.Database.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddLoteTablasMongoDatabaseConnection(
            this IServiceCollection services,
            IConfiguration config)
        {

            var mongoConnectionString = config[EnvironmentVariableNames.LOTETABLAS_MONGODB_CONNECTION_STRING] ?? throw new ArgumentException($"{EnvironmentVariableNames.LOTETABLAS_MONGODB_CONNECTION_STRING} was not found in App Configuration");

            var settings = MongoClientSettings.FromConnectionString(mongoConnectionString);
            // Set the ServerApi field of the settings object to set the version of the Stable API on the client
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            // Register IMongoClient as a singleton service.
            services.AddScoped<IMongoDatabase>(sp =>
            {
                var client = new MongoClient(settings);
                return client.GetDatabase("lotetablas");
            });
            return services;
        }

    }
}
