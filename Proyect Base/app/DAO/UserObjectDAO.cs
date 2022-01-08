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
                string query = "SELECT * FROM user_objects " +
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
                string query = "SELECT * FROM user_objects " +
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
        public static void putOrRemoveUserObjectFromArea(User User, UserObject userObject)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("id", userObject.id);
            client.SetParameter("ds", userObject.ZonaID);
            client.SetParameter("sd", userObject.Posicion.x);
            client.SetParameter("y", userObject.Posicion.y);
            client.SetParameter("height", userObject.height);
            client.SetParameter("userid", User.id);
            client.SetParameter("ocupe", userObject.ocupe);
            client.SetParameter("rotation", userObject.rotation);
            client.ExecuteNonQuery("UPDATE user_objects " +
                "SET sala_id = @ds, x = @sd, y = @y, height = @height, UserID = @userid, ocupe = @ocupe, rotation = @rotation  " +
                "WHERE id = @id");
        }
        public static void deleteUserObject(User User, UserObject userObject)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("id", userObject.id);
            client.SetParameter("userid", User.id);
            client.ExecuteNonQuery("DELETE FROM user_objects " +
               "WHERE id = @id AND UserID = @userid");
        }
        public static void rotateUserObjectInArea(UserObject userObject)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("id", userObject.id);
            client.SetParameter("r", userObject.rotation);
            client.SetParameter("x", userObject.Posicion.x);
            client.SetParameter("y", userObject.Posicion.y);
            client.SetParameter("ocupe", userObject.ocupe);
            client.ExecuteNonQuery("UPDATE user_objects " +
                "SET rotation = @r, x = @x, y = @y, ocupe = @ocupe " +
                "WHERE id = @id");
        }
        public static void updateUserObjectColorsInArea(UserObject userObject)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("id", userObject.id);
            client.SetParameter("itemid", userObject.ObjetoID);
            client.SetParameter("color", userObject.Color_1);
            client.SetParameter("rgb", userObject.Color_2);
            client.ExecuteNonQuery("UPDATE user_objects " +
                "SET color = @color, rgb_ratio = @rgb " +
                "WHERE id = @id AND ItemID = @itemid");
        }
        public static void updateUserObjectSizeInArea(UserObject userObject)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("id", userObject.id);
            client.SetParameter("x", userObject.Posicion.x);
            client.SetParameter("y", userObject.Posicion.y);
            client.SetParameter("size", userObject.size);
            client.SetParameter("coor", userObject.ocupe);
            client.ExecuteNonQuery("UPDATE user_objects " +
                "SET X = @x, Y = @y, size = @size, ocupe = @coor " +
                "WHERE id = @id");
        }
        public static UserObject getUserObjectByItemAndUserIds(int userId, int itemId)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("ItemID", itemId);
            client.SetParameter("UserID", userId);
            DataRow row = client.ExecuteQueryRow("SELECT * FROM user_objects " +
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
                client.ExecuteNonQuery("INSERT INTO user_objects " +
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
