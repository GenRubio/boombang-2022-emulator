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
        public static Dictionary<int, Island> getUserIslands(User User)
        {
            Dictionary<int, Island> islands = new Dictionary<int, Island>();
            try
            {
                SqlClient client = SqlManager.GetClient();
                client.SetParameter("user_id", User.id);
                foreach (DataRow row in client.ExecuteQueryTable("SELECT * FROM islas " +
                    "WHERE creadorID = @user_id").Rows)
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
            client.ExecuteQueryRow("DELETE FROM islas WHERE id = @id");
        }
        public static Island getIslandByName(string name)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("name", name);
            DataRow row = client.ExecuteQueryRow("SELECT * FROM islas WHERE nombre = @name LIMIT 1");
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
            client.SetParameter("creador", User.id);
            if (client.ExecuteNonQuery("INSERT INTO islas " +
                "(`Nombre`, `Modelo`, `CreadorID`) " +
                "VALUES (@nombre, @modelo, @creador)") == 1)
            {
                return getIslandByName(name);
            }
            return null;
        }
        public static Island getIslandById(int id)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("id", id);
            DataRow row = client.ExecuteQueryRow("SELECT * FROM islas WHERE id = @id");
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
            client.SetParameter("name", name);
            foreach (DataRow row in client.ExecuteQueryTable("SELECT * FROM islas " +
                "WHERE Nombre LIKE '%" + name + "%'").Rows)
            {
                islands.Add(new Island(row));
            }
            return islands;
        }
    }
}
