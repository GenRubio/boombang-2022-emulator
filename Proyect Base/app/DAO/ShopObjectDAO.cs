using BurBian_ULTIMANTE;
using Proyect_Base.app.Models;
using Proyect_Base.logs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.DAO
{
    class ShopObjectDAO
    {
        public static List<ShopObject> getShopObjects()
        {
            List<ShopObject> shopObjects = new List<ShopObject>();
            try
            {
                SqlClient client = SqlManager.GetClient();
                client.SetParameter("active", 1);
                string query = "SELECT * FROM boombang_catalogo " +
                    "WHERE activado = @active " +
                    "ORDER BY categoria DESC";
                foreach (DataRow row in client.ExecuteQueryTable(query).Rows)
                {
                    if (row != null)
                    {
                        shopObjects.Add(new ShopObject(row));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
            return shopObjects;
        }
    }
}
