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
    class SpecialAreaDAO
    {
        public static List<SpecialArea> getIslandAreas()
        {
            List<SpecialArea> specialAreas = new List<SpecialArea>();
            try
            {
                SqlClient client = SqlManager.GetClient();
                foreach (DataRow row in client.ExecuteQueryTable("SELECT * FROM escenarios_privados").Rows)
                {
                    if ((int)row["categoria"] == 2)
                    {
                        specialAreas.Add(new IslandArea(row));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
            return specialAreas;
        }
        public static SpecialArea getSpecialAreaById(int id)
        {
            try
            {
                SqlClient client = SqlManager.GetClient();
                client.SetParameter("id", id);
                DataRow row = client.ExecuteQueryRow("SELECT * FROM escenarios_privados " +
                   "WHERE id = @id");
                if (row != null)
                {
                    if ((int)row["categoria"] == 2)
                    {
                        return new IslandArea(row);
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
            return null;
        }
    }
}
