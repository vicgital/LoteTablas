using LoteTablas.Framework.Common.Configuration;

namespace LoteTablas.Framework.Common.Azure
{
    public class AzureServiceBus(IAppConfiguration config) : IAzureServiceBus
    {
        private readonly string _connectionString = config.GetValue("LOTETABLAS_SB_CONNECTION_STRING");

        public Task SendMessage(string queueName, string json)
        {
            throw new NotImplementedException();
        }

        public Task SendMessages(string queueName, List<string> jsonItems)
        {
            throw new NotImplementedException();
        }
    }
}
