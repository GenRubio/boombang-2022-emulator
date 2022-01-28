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
    class MiniGameAreaDAO
    {
        public static List<MiniGame> getMiniGameAreas()
        {
            List<MiniGame> miniGameAreas = new List<MiniGame>();
            try
            {
                SqlClient client = SqlManager.GetClient();
                string query = "SELECT * FROM game_areas";
                foreach (DataRow row in client.ExecuteQueryTable(query).Rows)
                {
                    if (row != null)
                    {
                        miniGameAreas.Add(new MiniGame(row));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
            return miniGameAreas;
        }
    }
}
