using BurBian_ULTIMANTE;
using Proyect_Base.app.Models;
using Proyect_Base.forms;
using Proyect_Base.logs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.DAO
{
    class UserObjectDAO
    {
        public static Dictionary<int, UserObject> getUserBackpackObjects(User User)
        {
            Dictionary<int, UserObject> userObjects = new Dictionary<int, UserObject>();
            try
            {
                SqlClient client = SqlManager.GetClient();
                client.SetParameter("UserID", User.id);
                client.SetParameter("sala_id", 0);
                string query = "SELECT * FROM boombang_buyitems " +
                    "WHERE UserID = @UserID " +
                    "AND sala_id = @sala_id";
                foreach (DataRow row in client.ExecuteQueryTable(query).Rows)
                {
                    if (row != null)
                    {
                        userObjects.Add((int)row["id"], new UserObject(row));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
            return userObjects;
        }
        public static Dictionary<int, UserObject> getUserObjects(User User)
        {
            Dictionary<int, UserObject> userObjects = new Dictionary<int, UserObject>();
            try
            {
                SqlClient client = SqlManager.GetClient();
                client.SetParameter("UserID", User.id);
                string query = "SELECT * FROM boombang_buyitems " +
                    "WHERE UserID = @UserID";
                foreach (DataRow row in client.ExecuteQueryTable(query).Rows)
                {
                    if (row != null)
                    {
                        userObjects.Add((int)row["id"], new UserObject(row));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
            return userObjects;
        }
        public static UserObject getUserObjectByItemAndUserIds(int userId, int itemId)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("ItemID", itemId);
            client.SetParameter("UserID", userId);
            DataRow row = client.ExecuteQueryRow("SELECT * FROM boombang_buyitems " +
                "WHERE ItemID = @ItemID AND UserID = @UserID ORDER BY id DESC LIMIT 1");
            if (row != null)
            {
                return new UserObject(row);
            }
            return null;
        }
        public static UserObject createObjectUser(User User, ShopObject shopObject, string tam)
        {
            try
            {
                SqlClient client = SqlManager.GetClient();
                client.SetParameter("UserIDe", User.id);
                client.SetParameter("ItemID", shopObject.id);
                client.SetParameter("swf", shopObject.Nombre);
                client.SetParameter("color", shopObject.Color_1);
                client.SetParameter("rgb_ratio", shopObject.Color_2);
                client.SetParameter("size", tam);
                client.SetParameter("rotation", 0);
                client.SetParameter("something_4", string.Empty);
                client.ExecuteNonQuery("INSERT INTO boombang_buyitems " +
                    "(ItemID,swf,color,rgb_ratio,size,rotation,something_4,UserID,ocupe,data) " +
                    "VALUES (@ItemID, @swf, @color, @rgb_ratio, @size, @rotation, @something_4, @UserIDe,'','');");

                return getUserObjectByItemAndUserIds(User.id, shopObject.id);
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
            return null;
        }
    }
}
