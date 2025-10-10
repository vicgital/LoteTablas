namespace LoteTablas.Application.Contracts.Configuration
{
    public interface IAppConfigurationManager
    {
        string GetValue(string key);
        string GetValue(string key, string defaultValue);
    }
}
