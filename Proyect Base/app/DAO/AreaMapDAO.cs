using BurBian_ULTIMANTE;
using Proyect_Base.app.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.DAO
{
    class AreaMapDAO
    {
        public static AreaMap getPublicAreaMap(int model)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("id", model);
            DataRow row = client.ExecuteQueryRow("SELECT * FROM mapas_publicos WHERE id = @id");
            if (row != null)
            {
                return new AreaMap(row);
            }
            return null;
        }
        public static AreaMap getPrivateAreaMap(int model)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("id", model);
            DataRow row = client.ExecuteQueryRow("SELECT * FROM mapas_privados WHERE id = @id");
            if (row != null)
            {
                return new AreaMap(row);
            }
            return null;
        }
        public static AreaMap getGameAreaMap(int model)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("id", model);
            DataRow row = client.ExecuteQueryRow("SELECT * FROM mapas_mgame WHERE id = @id");
            if (row != null)
            {
                return new AreaMap(row);
            }
            return null;
        }
    }
}
