using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Proyect_Base;
using Proyect_Base.logs;

namespace BurBian_ULTIMANTE
{
    public static class SqlManager
    {
        public static SqlClient GetClient()
        {
            return CreateClient();
        }

        private static SqlClient CreateClient()
        {
            MySqlConnection Connection = new MySqlConnection(GenerateConnectionString());
            Connection.Open();
            return new SqlClient(Connection);
        }

        public static string GenerateConnectionString()
        {
            MySqlConnectionStringBuilder ConnectionStringBuilder = new MySqlConnectionStringBuilder();
            ConnectionStringBuilder.Server = Config.DB_HOST;
            ConnectionStringBuilder.Port = Config.DB_PORT;
            ConnectionStringBuilder.UserID = Config.DB_USERNAME;
            ConnectionStringBuilder.Password = Config.DB_PASSWORD;
            ConnectionStringBuilder.Database = Config.DB_DATABASE;
            ConnectionStringBuilder.ConnectionTimeout = Config.DB_TIMEOUT;
            return ConnectionStringBuilder.ToString();
        }
    }
}