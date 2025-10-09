using System.Data;

namespace LoteTablas.Framework.Common.Database
{
    public interface IDatabaseConnection
    {
        IDbConnection GetDbSqlConnection();

    }
}
