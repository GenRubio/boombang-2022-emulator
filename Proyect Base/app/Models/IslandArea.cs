using Proyect_Base.app.Connection;
using Proyect_Base.app.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    public class IslandArea : SpecialArea
    {
        public int islandId { get; set; }
        public string password { get; set; }
        public int userCreatorId { get; set; }
        public IslandArea(DataRow row) :
         base(row)
        {
            this.islandId = int.Parse(row["IslaID"].ToString());
            this.userCreatorId = int.Parse(row["CreadorID"].ToString());
            this.password = row["clave"].ToString();
        }
        //FUNCTIONS

        //MODEL SETTERS

        //MODEL GETTERS
        public User getUser()
        {
            return UserDAO.getUserById(this.userCreatorId);
        }
        public Island getIslad()
        {
            return IslandDAO.getIslandById(this.islandId);
        }
        //HANDLERS
        public void loadAreaObjectsHandler(Session Session)
        {
            ServerMessage server = new ServerMessage(new byte[] { 128, 121, 121 });
            server.AppendParameter(1);
            getAreaParametersHandler(server);
            loadUsersInAreaHandler(server);
            server.AppendParameter(0);
            server.AppendParameter(0);
            server.AppendParameter(0);
            server.AppendParameter(0);
            server.AppendParameter(0);
            server.AppendParameter(0);
            Session.SendData(server);
        }
        private ServerMessage getAreaParametersHandler(ServerMessage server)
        {
            server.AppendParameter(0);
            server.AppendParameter(this.islandId);
            server.AppendParameter(this.id);
            server.AppendParameter(this.id);
            server.AppendParameter(this.color_1);
            server.AppendParameter(this.color_2);
            server.AppendParameter(0);
            server.AppendParameter(getUser().id);
            server.AppendParameter(-1);
            server.AppendParameter(-1);
            server.AppendParameter(-1);
            server.AppendParameter(-1);
            server.AppendParameter(-1);
            server.AppendParameter(-1);
            server.AppendParameter(0);
            server.AppendParameter(-1);
            server.AppendParameter(-1);
            server.AppendParameter(-1);
            server.AppendParameter(-1);
            server.AppendParameter(-1);
            server.AppendParameter(-1);
            getAreaObjectsParametersHandler(server);
            server.AppendParameter(0);
            server.AppendParameter(this.users.Count());
            return server;
        }
        private ServerMessage getAreaObjectsParametersHandler(ServerMessage server)
        {
            server.AppendParameter(0);//Objetos comprado
            return server;
        }
        public void loadAreaParametersHandler(Session Session)
        {
            ServerMessage server = new ServerMessage(new byte[] { 175 });
            server.AppendParameter(new object[] { 1, 0, 0 });
            server.AppendParameter(new object[] { 2, 0, 0 });
            server.AppendParameter(new object[] { 3, 0, 0 });

            Island island = getIslad();
            if (island != null)
            {
                server.AppendParameter(new object[] { 4, (island.uppertActive == 0 ? -1 : 0), 0 });
                server.AppendParameter(new object[] { 5, 0, 0 });
            }
            Session.SendData(server);
        }
    }
}
