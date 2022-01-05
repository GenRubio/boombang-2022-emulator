using Proyect_Base.app.Connection;
using Proyect_Base.app.DAO;
using Proyect_Base.app.Pathfinding;
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
        public List<UserObject> objects { get; set; }
        public IslandArea(DataRow row) :
         base(row)
        {
            this.islandId = int.Parse(row["island_id"].ToString());
            this.userCreatorId = int.Parse(row["user_id"].ToString());
            this.password = row["password"].ToString();
        }
        //FUNCTIONS
        public void addObject(UserObject userObject)
        {
            this.objects.Add(userObject);
        }
        public bool WalkByObjects(int x, int y)
        {
            List<Posicion> occupiedPoints = getObjectsCoordinates();
            Posicion posicion = occupiedPoints.Find(i => i.x == x && i.y == y);
            if (posicion != null)
            {
                return false;
            }
            return true;
        }
        public void removeAllUsers()
        {
            foreach (Session Session in this.users.Values.ToList())
            {
                removeUserHandler(Session);
            }
        }
        public void removeAllUsersToFlowerPower()
        {
            foreach (Session Session in this.users.Values.ToList())
            {
                removeUserToFlowerPowerHandler(Session);
            }
        }
        public void removeAllObjects(Session Session)
        {
            foreach(UserObject userObject in this.objects)
            {
                removeObject(Session, userObject);
            }
        }
        public void removeObject(Session Session, UserObject userObject)
        {
            userObject.updateAttributes(Session.User, 0, 0, 0, 0, "", 0,
                            userObject.Color_1, userObject.Color_2);
            removeObjectHandler(userObject);
            Session.User.addObjectToBackpackHandler(Session, userObject);
            UserObjectDAO.putOrRemoveUserObjectFromArea(Session.User, userObject);
        }
        //MODEL SETTERS

        //MODEL GETTERS
        private List<Posicion> getObjectsCoordinates()
        {
            List<Posicion> occupiedPoints = new List<Posicion>();
            List<UserObject> userObjects = this.objects.Where(i => i.ocupe != "").ToList();
            foreach (UserObject Item in userObjects)
            {
                int s_x = 0;
                int s_y = 0;
                foreach (string punto in Item.ocupe.Split(','))
                {
                    if (s_x == 0)
                    {
                        s_x = Convert.ToInt32(punto);
                        continue;
                    }
                    if (s_y == 0)
                    {
                        s_y = Convert.ToInt32(punto);
                        occupiedPoints.Add(new Posicion(s_x, s_y));
                    }
                    s_x = 0;
                    s_y = 0;
                }
            }
            return occupiedPoints;
        }
        public User getUser()
        {
            return UserDAO.getUserById(this.userCreatorId);
        }
        public Island getIsland()
        {
            return IslandDAO.getIslandById(this.islandId);
        }
        public UserObject getObjectById(int id)
        {
            return this.objects.Find(i => i.id == id);
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
            User userCreator = getUser();
            if (userCreator != null)
            {
                server.AppendParameter(0);
                server.AppendParameter(this.islandId);
                server.AppendParameter(this.id);
                server.AppendParameter(this.id);
                server.AppendParameter(this.color_1);
                server.AppendParameter(this.color_2);
                server.AppendParameter(0);
                server.AppendParameter(userCreator.id);
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
                getAreaObjectsParametersHandler(userCreator, server);
                server.AppendParameter(0);
                server.AppendParameter(this.users.Count());
            }
            return server;
        }
        private ServerMessage getAreaObjectsParametersHandler(User User, ServerMessage server)
        {
            this.objects = User.islandAreaObjects(this.id);
            server.AppendParameter(this.objects.Count());
            foreach(UserObject userObject in objects)
            {
                server.AppendParameter(userObject.id);
                server.AppendParameter(userObject.ObjetoID);
                server.AppendParameter(userObject.Posicion.x);
                server.AppendParameter(userObject.Posicion.y);
                server.AppendParameter(userObject.rotation);
                server.AppendParameter(userObject.size);
                server.AppendParameter("");//TOP O TEXTO EN OTROS OBJETOS.
                server.AppendParameter(userObject.ocupe);
                server.AppendParameter(userObject.Color_1);
                server.AppendParameter(userObject.Color_2);
                server.AppendParameter(Convert.ToInt32(userObject.height) > 0 ? userObject.height : userObject.data);
            }
            return server;
        }
        public void loadAreaParametersHandler(Session Session)
        {
            ServerMessage server = new ServerMessage(new byte[] { 175 });
            server.AppendParameter(new object[] { 1, 0, 0 });
            server.AppendParameter(new object[] { 2, 0, 0 });
            server.AppendParameter(new object[] { 3, 0, 0 });

            Island island = getIsland();
            if (island != null)
            {
                server.AppendParameter(new object[] { 4, (island.uppertActive == 0 ? -1 : 0), 0 });
                server.AppendParameter(new object[] { 5, 0, 0 });
            }
            Session.SendData(server);
        }
        public void putObjectHandler(Session Session, UserObject userObject)
        {
            ServerMessage server = new ServerMessage(new byte[] { 189, 136 });
            server.AppendParameter(userObject.id);
            server.AppendParameter(userObject.ObjetoID);
            server.AppendParameter(userObject.ZonaID);
            server.AppendParameter(Session.User.id);
            server.AppendParameter(userObject.Posicion.x);
            server.AppendParameter(userObject.Posicion.y);
            server.AppendParameter(userObject.rotation);
            server.AppendParameter(userObject.size);
            server.AppendParameter("");//top o nombre
            server.AppendParameter(userObject.ocupe);
            server.AppendParameter(userObject.Color_1);
            server.AppendParameter(userObject.Color_2);
            server.AppendParameter(Convert.ToInt32(userObject.height) > 0 ? userObject.height : userObject.data);
            SendData(server);
        }
        public void moveObjectHandler(UserObject userObject)
        {
            ServerMessage server = new ServerMessage(new byte[] { 189, 145 });
            server.AppendParameter(userObject.id);
            server.AppendParameter(userObject.Posicion.x);
            server.AppendParameter(userObject.Posicion.y);
            server.AppendParameter(userObject.height);
            server.AppendParameter(userObject.ocupe);
            SendData(server);
        }
        public void removeObjectHandler(UserObject userObject)
        {
            ServerMessage server = new ServerMessage(new byte[] { 189, 140 });
            server.AppendParameter(userObject.id);
            SendData(server);
        }
        public void rotateObjectHandler(UserObject userObject)
        {
            ServerMessage server = new ServerMessage(new byte[] { 189, 143 });
            server.AppendParameter(userObject.id);
            server.AppendParameter(userObject.rotation);
            server.AppendParameter(userObject.ocupe);
            SendData(server);
        }
        public void changeObjectColorsHandler(UserObject userObject)
        {
            ServerMessage server = new ServerMessage(new byte[] { 189, 142 });
            server.AppendParameter(userObject.id);
            server.AppendParameter(userObject.Color_1);
            server.AppendParameter(userObject.Color_2);
            server.AppendParameter(userObject.ObjetoID);
            SendData(server);
        }
        public void changeColorsAreaHandler()
        {
            SendData(new ServerMessage(new byte[] { 189, 146 }, new object[] { this.id, this.color_1, this.color_2 }));
        }
    }
}
