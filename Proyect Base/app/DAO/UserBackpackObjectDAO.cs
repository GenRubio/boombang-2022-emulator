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
    class UserBackpackObjectDAO
    {
        public static List<UserBackpackObject> getUserBackpackObjects(User User)
        {
            List<UserBackpackObject> backpackObjects = new List<UserBackpackObject>();
            try
            {
                SqlClient client = SqlManager.GetClient();
                client.SetParameter("sala_id", 0);
                client.SetParameter("UserID", User.id);
                string query = "SELECT ItemID, COUNT(ItemID) AS total FROM user_objects " +
                    "WHERE sala_id = @sala_id " +
                    "AND UserID = @UserID " +
                    "GROUP BY ItemID";
                foreach (DataRow row in client.ExecuteQueryTable(query).Rows)
                {
                    if (row != null)
                    {
                        backpackObjects.Add(new UserBackpackObject(row));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
            return backpackObjects;
        }
    }
}
