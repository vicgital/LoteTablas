using Microsoft.Extensions.Configuration;

namespace LoteTablas.Framework.Common.Configuration
{
    public class AppConfiguration : IAppConfiguration
    {
        private readonly IConfiguration _config;

        public AppConfiguration(IConfiguration config)
        {
            _config = config;
        }

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
