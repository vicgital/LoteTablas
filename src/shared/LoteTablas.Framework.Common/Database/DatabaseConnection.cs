using LoteTablas.Framework.Common.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LoteTablas.Framework.Common.Database
{
    public class DatabaseConnection(IAppConfiguration config) : IDatabaseConnection
    {

        private readonly string _connectionString = config.GetValue("LOTETABLAS_DB_CONNECTION_STRING");

        public IDbConnection GetDbSqlConnection() =>
            new SqlConnection(_connectionString);


    }
}
