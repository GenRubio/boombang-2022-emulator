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
    class AreaNpcObjectDAO
    {
        public static List<AreaNpcObject> getAreaNpcObjects(int npcId)
        {
            List<AreaNpcObject> areaNpcObjects = new List<AreaNpcObject>();
            try
            {
                SqlClient client = SqlManager.GetClient();
                client.SetParameter("area_npc_id", npcId);
                client.SetParameter("active", 1);
                foreach (DataRow row in client.ExecuteQueryTable("SELECT * FROM area_npc_objects " +
                  "WHERE area_npc_id = @area_npc_id " +
                  "AND active = @active").Rows)
                {
                    areaNpcObjects.Add(new AreaNpcObject(row));
                }
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }
            return areaNpcObjects;
        }
    }
}
