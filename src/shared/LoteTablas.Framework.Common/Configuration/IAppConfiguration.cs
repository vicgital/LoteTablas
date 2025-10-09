namespace LoteTablas.Framework.Common.Configuration
{
    public interface IAppConfiguration
    {
        string GetValue(string key);
        string GetValue(string key, string defaultValue);
    }
}
