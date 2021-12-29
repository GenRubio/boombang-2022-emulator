using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SqlKata.Compilers;
using SqlKata.Execution;
using SqlKata.Extensions;

namespace Proyect_Base.database.sqlkata
{
    class SqlKata
    {
        static MySqlConnection connection = new MySqlConnection("Host=" + Config.KATA_HOST + ";Port=" + Config.KATA_PORT + ";User=" + Config.KATA_USERNAME + ";Password=" + Config.KATA_PASSWORD + ";Database=" + Config.KATA_DATABASE + ";SslMode=" + Config.KATA_SSL_MODE + "");
        static MySqlCompiler compiler = new MySqlCompiler();

        public static QueryFactory query() => new QueryFactory(connection, compiler);
    }
}
