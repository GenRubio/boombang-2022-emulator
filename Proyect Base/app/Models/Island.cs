using Proyect_Base.app.Collections;
using Proyect_Base.app.Connection;
using Proyect_Base.app.DAO;
using Proyect_Base.forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    public class Island
    {
        public int id { get; set; }
        public int model { get; set; }
        public int uppertActive { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int userCreatorId { get; set; }
        public Island(DataRow row)
        {
            this.id = (int)row["id"];
            this.model = (int)row["modelo"];
            this.uppertActive = (int)row["uppercut"];
            this.name = (string)row["nombre"];
            this.description = (string)row["descripcion"];
            this.userCreatorId = (int)row["user_id"];
        }
        //FUNCTIONS
        public void removeAllAreas(Session Session)
        {
            foreach(IslandArea islandArea in getAreas())
            {
                islandArea.removeAllObjects(Session);
                SpecialAreaCollection.removeIslandArea(Session.User, islandArea.id);
            }
        }
        //MODEL SETTERS

        //MODEL GETTERS
        public int getCountUsersInIsland()
        {
            int count = 0;
            foreach(IslandArea islandArea in getAreas())
            {
                count += islandArea.users.Count();
            }
            return count;
        }
        public User userCreator()
        {
            return UserDAO.getUserById(this.userCreatorId);
        }
        public List<IslandArea> getAreas()
        {
            return SpecialAreaCollection.getIslandAreasByIslandId(this.id);
        }
        //HANDLERS
        public void loadIslandHandler(Session Session)
        {
            User user = this.userCreator();
            if (user != null)
            {
                ServerMessage server = new ServerMessage(new byte[] { 189, 124 });
                server.AppendParameter(this.id);
                server.AppendParameter(this.name);
                server.AppendParameter(this.description);
                server.AppendParameter(this.model);
                server.AppendParameter(this.uppertActive);
                server.AppendParameter(user.id);
                server.AppendParameter(user.name);
                server.AppendParameter(user.avatar);
                server.AppendParameter(user.colors);
                server.AppendParameter(null);
                server.AppendParameter(null);
                server.AppendParameter(null);
                server.AppendParameter(null);
                server.AppendParameter(null);
                server.AppendParameter(null);
                server.AppendParameter(null);
                server.AppendParameter(null);
                server.AppendParameter(null);
                server.AppendParameter(null);
                server.AppendParameter(null);
                server.AppendParameter(null);
                server.AppendParameter(null);
                server.AppendParameter(null);
                server.AppendParameter(null);
                server.AppendParameter(null);
                getIslandAreasParametersHandler(server);
                Session.SendData(server);
            }
        }
        private ServerMessage getIslandAreasParametersHandler(ServerMessage server)
        {
            List<IslandArea> islandAreas = getAreas();
            server.AppendParameter(islandAreas.Count());
            foreach(IslandArea islandArea in islandAreas)
            {
                server.AppendParameter(0);
                server.AppendParameter(islandArea.es_category);
                server.AppendParameter(islandArea.id);
                server.AppendParameter(islandArea.id);
                server.AppendParameter(islandArea.name);
                server.AppendParameter(islandArea.model);
                server.AppendParameter(0);
                server.AppendParameter(0);
                server.AppendParameter(0);
                server.AppendParameter(islandArea.users.Count());
                server.AppendParameter(0);
                server.AppendParameter((string.IsNullOrEmpty(islandArea.password) ? 0 : 1));
            }
            return server;
        }
        public void removeUsersFromAllAreasHandler()
        {
            foreach(IslandArea islandArea in getAreas())
            {
                islandArea.removeAllUsersToFlowerPower();
            }
        }
    }
}
