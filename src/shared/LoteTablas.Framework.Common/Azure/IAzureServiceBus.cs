namespace LoteTablas.Framework.Common.Azure
{
    public interface IAzureServiceBus
    {
        Task SendMessage(string queueName, string json);
        Task SendMessages(string queueName, List<string> jsonItems);

    }
}
