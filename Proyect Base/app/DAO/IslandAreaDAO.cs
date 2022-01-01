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
                client.SetParameter("islandId", id);
                foreach (DataRow row in client.ExecuteQueryTable("SELECT * FROM escenarios_privados " +
                    "WHERE IslaID = @islandId").Rows)
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
            client.SetParameter("IslaID", Island.id);
            client.SetParameter("Nombre", name);
            client.SetParameter("Modelo", model);
            client.SetParameter("color_1", color_1);
            client.SetParameter("color_2", color_2);
            client.SetParameter("CreadorID", User.id);
            if (client.ExecuteNonQuery("INSERT INTO escenarios_privados " +
                "(`categoria`, `nombre`, `modelo`, `IslaID`, `color_1`, `color_2`, `CreadorID`) " +
                "VALUES (@categoria, @Nombre, @Modelo, @IslaID, @color_1, @color_2, @CreadorID)") == 1)
            {
                return getMakeIslandArea(User, Island);
            }
            return null;
        }
        private static IslandArea getMakeIslandArea(User User, Island Island)
        {
            SqlClient client = SqlManager.GetClient();
            client.SetParameter("CreadorID", User.id);
            client.SetParameter("IslaID", Island.id);
            client.SetParameter("categoria", 2);
            DataRow row = client.ExecuteQueryRow("SELECT * FROM escenarios_privados " +
                "WHERE CreadorID = @CreadorID " +
                "AND IslaID = @IslaID " +
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
                client.SetParameter("islandId", id);
                client.SetParameter("CreadorID", User.id);
                client.ExecuteNonQuery("DELETE FROM escenarios_privados " +
                    "WHERE IslaID = @islandId " +
                    "AND CreadorID = @CreadorID");
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
            client.SetParameter("CreadorID", User.id);
            client.SetParameter("islandId", islandId);
            client.SetParameter("nombre", name);
            client.ExecuteNonQuery("UPDATE escenarios_privados " +
                  "SET nombre = @nombre " +
                  "WHERE id = @id " +
                  "AND CreadorID = @CreadorID " +
                  "AND IslaID = @islandId");
        }
        public static void deleteIslandAreaById(User User, int id)
        {
            try
            {
                SqlClient client = SqlManager.GetClient();
                client.SetParameter("id", id);
                client.SetParameter("CreadorID", User.id);
                client.ExecuteNonQuery("DELETE FROM escenarios_privados " +
                    "WHERE id = @id " +
                    "AND CreadorID = @CreadorID");
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
    }
}
