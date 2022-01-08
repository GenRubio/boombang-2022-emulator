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
    class AreaNpcObjectRequirementDAO
    {
        public static List<AreaNpcObjectRequirement> getAreaNpcObjectRequirements(int npcObjectId)
        {
            List<AreaNpcObjectRequirement> areaNpcObjectRequirements = new List<AreaNpcObjectRequirement>();
            try
            {
                SqlClient client = SqlManager.GetClient();
                client.SetParameter("area_npc_object_id", npcObjectId);
                client.SetParameter("active", 1);
                foreach (DataRow row in client.ExecuteQueryTable("SELECT * FROM area_npc_object_requirements " +
                  "WHERE area_npc_object_id = @area_npc_object_id " +
                  "AND active = @active").Rows)
                {
                    areaNpcObjectRequirements.Add(new AreaNpcObjectRequirement(row));
                }
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
            return areaNpcObjectRequirements;
        }
    }
}
