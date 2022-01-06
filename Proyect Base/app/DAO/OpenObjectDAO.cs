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
    class OpenObjectDAO
    {
        public static List<OpenObject> getOpenObjectsByObjectId(int id)
        {
            List<OpenObject> openObjects = new List<OpenObject>();
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("shop_object_id", id);
            foreach (DataRow row in client.ExecuteQueryTable("SELECT * FROM open_objects " +
                   "WHERE shop_object_id = @shop_object_id").Rows)
            {
                openObjects.Add(new OpenObject(row));
            }
            return openObjects;
        }
    }
}
