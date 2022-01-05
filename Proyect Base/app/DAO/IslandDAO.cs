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
    class IslandDAO
    {
        public static List<Island> getIslandsAll()
        {
            List<Island> islands = new List<Island>();
            SqlClient client = SqlManager.GetClient();
            foreach (DataRow row in client.ExecuteQueryTable("SELECT * FROM islands").Rows)
            {
                islands.Add(new Island(row));
            }
            return islands;
        }
        public static Dictionary<int, Island> getUserIslands(User User)
        {
            Dictionary<int, Island> islands = new Dictionary<int, Island>();
            try
            {
                SqlClient client = SqlManager.GetClient();
                client.SetParameter("user_id", User.id);
                foreach (DataRow row in client.ExecuteQueryTable("SELECT * FROM islands " +
                    "WHERE user_id = @user_id").Rows)
                {
                    islands.Add((int)row["id"], new Island(row));
                }
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }
            return islands;
        }
        public static void deleteIslandById(int id)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("id", id);
            client.ExecuteQueryRow("DELETE FROM islands WHERE id = @id");
        }
        public static Island getIslandByName(string name)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("nombre", name);
            DataRow row = client.ExecuteQueryRow("SELECT * FROM islands WHERE nombre = @nombre LIMIT 1");
            if (row != null)
            {
                return new Island(row);
            }
            return null;
        }
        public static Island makeIsland(string name, int model, User User)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("nombre", name);
            client.SetParameter("modelo", model);
            client.SetParameter("user_id", User.id);
            if (client.ExecuteNonQuery("INSERT INTO islands " +
                "(`nombre`, `modelo`, `user_id`) " +
                "VALUES (@nombre, @modelo, @user_id)") == 1)
            {
                return getIslandByName(name);
            }
            return null;
        }
        public static Island getIslandById(int id)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("id", id);
            DataRow row = client.ExecuteQueryRow("SELECT * FROM islands WHERE id = @id");
            if (row != null)
            {
                return new Island(row);
            }
            return null;
        }
        public static List<Island> getIslandsByName(string name)
        {
            List<Island> islands = new List<Island>();
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("nombre", name);
            foreach (DataRow row in client.ExecuteQueryTable("SELECT * FROM islands " +
                "WHERE nombre LIKE '%" + name + "%'").Rows)
            {
                islands.Add(new Island(row));
            }
            return islands;
        }
    }
}
