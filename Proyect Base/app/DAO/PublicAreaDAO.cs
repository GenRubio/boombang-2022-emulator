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
    class PublicAreaDAO
    {
        public static List<PublicArea> getPublicAreas()
        {
            List<PublicArea> publicAreas = new List<PublicArea>();
            try
            {
                SqlClient client = SqlManager.GetClient();
                client.SetParameter("category", 1);
                client.SetParameter("active", 1);
                string query = "SELECT * FROM public_areas " +
                    "WHERE categoria = @category " +
                    "AND active = @active " +
                    "ORDER BY prioridad ASC";
                foreach (DataRow row in client.ExecuteQueryTable(query).Rows)
                {
                    if (row != null)
                    {
                        publicAreas.Add(new PublicArea(row));
                    }
                }
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }
            return publicAreas;
        }
    }
}
