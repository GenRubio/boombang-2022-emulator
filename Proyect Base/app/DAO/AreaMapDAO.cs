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
            client.SetParameter("modelo", model);
            DataRow row = client.ExecuteQueryRow("SELECT * FROM public_area_maps " +
                "WHERE modelo = @modelo");
            if (row != null)
            {
                return new AreaMap(row);
            }
            return null;
        }
        public static AreaMap getPrivateAreaMap(int model)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("modelo", model);
            DataRow row = client.ExecuteQueryRow("SELECT * FROM special_area_maps " +
                "WHERE modelo = @modelo");
            if (row != null)
            {
                return new AreaMap(row);
            }
            return null;
        }
        public static AreaMap getGameAreaMap(int model)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("modelo", model);
            DataRow row = client.ExecuteQueryRow("SELECT * FROM game_area_maps " +
                "WHERE modelo = @modelo");
            if (row != null)
            {
                return new AreaMap(row);
            }
            return null;
        }
    }
}
