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
    class GameAreaDAO
    {
        public static List<GameArea> getGameAreas()
        {
            List<GameArea> gameAreas = new List<GameArea>();
            try
            {
                SqlClient client = SqlManager.GetClient();
                client.SetParameter("category", 3);
                client.SetParameter("active", 1);
                string query = "SELECT * FROM public_areas " +
                    "WHERE categoria = @category " +
                    "AND active = @active " +
                    "ORDER BY prioridad ASC";
                foreach (DataRow row in client.ExecuteQueryTable(query).Rows)
                {
                    if (row != null)
                    {
                        gameAreas.Add(new GameArea(row));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
            return gameAreas;
        }
    }
}
