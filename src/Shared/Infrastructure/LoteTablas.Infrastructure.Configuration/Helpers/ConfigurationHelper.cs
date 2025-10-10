using Microsoft.Extensions.Configuration;

namespace LoteTablas.Infrastructure.Configuration.Helpers
{
    static class ConfigurationHelper
    {
        /// <summary>
        /// Gets an instance of IConfiguration using a appsettings.json file and an Azure App Config Instance
        /// </summary>
        /// <returns>IConfiguration</returns>
        public static IConfiguration GetConfiguration()
        {

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "dev";

            var builder = new ConfigurationBuilder()
               .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
