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
    class IslandAreaDAO
    {
        public static List<IslandArea> getIslandAreasByIslandId(int id)
        {
            List<IslandArea> islandAreas = new List<IslandArea>();
            try
            {
                SqlClient client = SqlManager.GetClient();
                client.SetParameter("island_id", id);
                foreach (DataRow row in client.ExecuteQueryTable("SELECT * FROM special_areas " +
                    "WHERE island_id = @island_id").Rows)
                {
                    islandAreas.Add(new IslandArea(row));
                }
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }
            return islandAreas;
        }
        public static IslandArea makeIslandArea(Island Island, User User, string name, int model, string color_1, string color_2)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("categoria", 2);
            client.SetParameter("island_id", Island.id);
            client.SetParameter("Nombre", name);
            client.SetParameter("Modelo", model);
            client.SetParameter("color_1", color_1);
            client.SetParameter("color_2", color_2);
            client.SetParameter("user_id", User.id);
            if (client.ExecuteNonQuery("INSERT INTO special_areas " +
                "(`categoria`, `nombre`, `modelo`, `island_id`, `color_1`, `color_2`, `user_id`) " +
                "VALUES (@categoria, @Nombre, @Modelo, @island_id, @color_1, @color_2, @user_id)") == 1)
            {
                return getMakeIslandArea(User, Island);
            }
            return null;
        }
        private static IslandArea getMakeIslandArea(User User, Island Island)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("user_id", User.id);
            client.SetParameter("island_id", Island.id);
            client.SetParameter("categoria", 2);
            DataRow row = client.ExecuteQueryRow("SELECT * FROM special_areas " +
                "WHERE user_id = @user_id " +
                "AND island_id = @island_id " +
                "AND categoria = @categoria " +
                "ORDER BY id DESC LIMIT 1");
            if (row != null)
            {
                return new IslandArea(row);
            }
            return null;
        }
        public static void deleteIslandAreaByIslandId(User User, int id)
        {
            try
            {
                SqlClient client = SqlManager.GetClient();
                client.SetParameter("island_id", id);
                client.SetParameter("user_id", User.id);
                client.ExecuteNonQuery("DELETE FROM special_areas " +
                    "WHERE island_id = @island_id " +
                    "AND user_id = @user_id");
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
        public static void updateNameIslandArea(User User, int islandId, int id, string name)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("id", id);
            client.SetParameter("user_id", User.id);
            client.SetParameter("island_id", islandId);
            client.SetParameter("nombre", name);
            client.ExecuteNonQuery("UPDATE special_areas " +
                  "SET nombre = @nombre " +
                  "WHERE id = @id " +
                  "AND user_id = @user_id " +
                  "AND island_id = @island_id");
        }
        public static void deleteIslandAreaById(User User, int id)
        {
            try
            {
                SqlClient client = SqlManager.GetClient();
                client.SetParameter("id", id);
                client.SetParameter("user_id", User.id);
                client.ExecuteNonQuery("DELETE FROM special_areas " +
                    "WHERE id = @id " +
                    "AND user_id = @user_id");
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
        public static void updateColors(IslandArea islandArea)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("id", islandArea.id);
            client.SetParameter("color_1", islandArea.color_1);
            client.SetParameter("color_2", islandArea.color_2);
            client.ExecuteNonQuery("UPDATE special_areas " +
                "SET color_1 = @color_1, color_2 = @color_2 " +
                "WHERE id = @id");
        }
    }
}
