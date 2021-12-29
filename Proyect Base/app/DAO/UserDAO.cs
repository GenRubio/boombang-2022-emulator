using BurBian_ULTIMANTE;
using Proyect_Base.app.Helpers;
using Proyect_Base.app.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.DAO
{
    class UserDAO
    {
        public static User getUserByLogin(string name, string password)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("name", name);
            client.SetParameter("password", UserHelper.hashMake(password));
            DataRow row = client.ExecuteQueryRow("SELECT * FROM users WHERE (name = @name AND password = @password) LIMIT 1");
            if (row != null)
            {
                return new User(row);
            }
            return null;
        }
    }
}
