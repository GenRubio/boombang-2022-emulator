﻿using Proyect_Base.app.Connection;
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
            this.islandId = int.Parse(row["IslaID"].ToString());
            this.userCreatorId = int.Parse(row["CreadorID"].ToString());
            this.password = row["clave"].ToString();
        }
        //FUNCTIONS
        public void addObject(UserObject userObject)
        {
            this.objects.Add(userObject);
        }
        public bool WalkByObjects(int x, int y)
        {
            List<Posicion> PointOccuped = new List<Posicion>();
            foreach (UserObject Item in this.objects)
            {
                try
                {
                    if (Item.ocupe != "")
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
                                PointOccuped.Add(new Posicion(s_x, s_y));
                            }
                            s_x = 0;
                            s_y = 0;
                        }
                    }
                }
                catch
                {
                    continue;
                }
            }

            foreach (Posicion pos in PointOccuped)
            {
                if (pos.x == x && pos.y == y)
                {
                    return false;
                }
            }

            return true;
        }
        //MODEL SETTERS

        //MODEL GETTERS
        public User getUser()
        {
            return UserDAO.getUserById(this.userCreatorId);
        }
        public Island getIsland()
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
            server.AppendParameter(this.id);
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
    }
}
