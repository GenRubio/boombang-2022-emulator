using BurBian_ULTIMANTE;
using Proyect_Base.app.Connection;
using Proyect_Base.app.Helpers;
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
    class UserDAO
    {
        public static User getUserByLogin(string name, string password)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("name", name);
            client.SetParameter("password", UserHelper.hashMake(password));
            DataRow row = client.ExecuteQueryRow("SELECT * FROM users WHERE (name = @name AND password = @password) LIMIT 1");
            if (row != null)
            {
                return new User(row);
            }
            return null;
        }
        public static User getUserById(int id)
        {
            try
            {
                SqlClient client = SqlManager.GetClient();
                client.SetParameter("id", id);
                DataRow row = client.ExecuteQueryRow("SELECT * FROM users WHERE id = @id");
                if (row != null)
                {
                    return new User(row);
                }
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }
            return null;
        }
        public static void addGoldCoins(User User, int coins)
        {
            try
            {
                SqlClient client = SqlManager.GetClient();
                client.SetParameter("id", User.id);
                client.SetParameter("gold", coins);
                client.ExecuteNonQuery("UPDATE users SET oro = oro + @gold WHERE id = @id");
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }
        }
        public static void removeGoldCoins(User User, int coins)
        {
            try
            {
                SqlClient client = SqlManager.GetClient();
                client.SetParameter("id", User.id);
                client.SetParameter("gold", coins);
                client.ExecuteNonQuery("UPDATE users SET oro = oro - @gold WHERE id = @id");
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
        public static void addSilverCoins(User User, int coins)
        {
            try
            {
                SqlClient client = SqlManager.GetClient();
                client.SetParameter("id", User.id);
                client.SetParameter("silver", coins);
                client.ExecuteNonQuery("UPDATE users SET plata = plata + @silver WHERE id = @id");
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
        public static void removeSilverCoins(User User, int coins)
        {
            try
            {
                SqlClient client = SqlManager.GetClient();
                client.SetParameter("id", User.id);
                client.SetParameter("silver", coins);
                client.ExecuteNonQuery("UPDATE users SET plata = plata - @silver WHERE id = @id");
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
    }
}
