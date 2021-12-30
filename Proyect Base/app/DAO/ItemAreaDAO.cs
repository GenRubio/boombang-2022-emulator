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
    class ItemAreaDAO
    {
        public static List<ItemArea> getItemAreas()
        {
            List<ItemArea> itemAreas = new List<ItemArea>();
            try
            {
                SqlClient client = SqlManager.GetClient();
                client.SetParameter("estado", 1);
                string query = "SELECT * FROM contest_item " +
                    "WHERE estado = @estado";
                foreach (DataRow row in client.ExecuteQueryTable(query).Rows)
                {
                    if (row != null)
                    {
                        itemAreas.Add(new ItemArea(row));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
            return itemAreas;
        }
    }
}
