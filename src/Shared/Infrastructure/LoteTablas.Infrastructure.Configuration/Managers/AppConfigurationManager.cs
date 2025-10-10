using LoteTablas.Application.Contracts.Configuration;
using Microsoft.Extensions.Configuration;

namespace LoteTablas.Infrastructure.Configuration.Managers
{
    public class AppConfigurationManager(IConfiguration config) : IAppConfigurationManager
    {

        private readonly IConfiguration _config = config;

        public string GetValue(string key)
        {
            var value = _config[key] ?? throw new ArgumentException($"{key} was not found in App Configuration");
            return value;
        }

        public string GetValue(string key, string defaultValue)
        {
            var value = _config[key] ?? defaultValue;
            return value;
        }
    }
}
