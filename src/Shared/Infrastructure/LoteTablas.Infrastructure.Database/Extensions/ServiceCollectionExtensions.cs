using LoteTablas.Application.Contracts.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace LoteTablas.Infrastructure.Database.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a scoped IDbConnection service to the IServiceCollection. 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddLoteTablasSqlDatabaseConnection(
        this IServiceCollection services,
        IAppConfigurationManager appConfigurationManager)
        {
            // Register IDbConnection as a scoped service.
            // A new connection will be created for each (or gRPC call).
            services.AddScoped<IDbConnection>(sp =>
            {
                var connectionString = appConfigurationManager.GetValue("LOTETABLAS_DB_CONNECTION_STRING");
                return new SqlConnection(connectionString);
            });

            return services;
        }
    }
}
