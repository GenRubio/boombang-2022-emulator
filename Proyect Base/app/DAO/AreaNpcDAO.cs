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
    class AreaNpcDAO
    {
        public static List<AreaNpc> getAreaNpc(int areaId)
        {
            List<AreaNpc> areaNpcs = new List<AreaNpc>();
            try
            {
                SqlClient client = SqlManager.GetClient();
                client.SetParameter("public_area_id", areaId);
                client.SetParameter("active", 1);
                foreach (DataRow row in client.ExecuteQueryTable("SELECT * FROM area_npcs " +
                  "WHERE public_area_id = @public_area_id " +
                  "AND active = @active").Rows)
                {
                    areaNpcs.Add(new AreaNpc(row));
                }
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }
            return areaNpcs;
        }
    }
}
