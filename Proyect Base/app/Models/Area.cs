using Proyect_Base.app.Connection;
using Proyect_Base.app.DAO;
using Proyect_Base.app.Middlewares;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    public class Area
    {
        public int id { get; set; }
        public string name { get; set; }
        public int model { get; set; }
        public int es_category { get; set; }
        public int category { get; set; }
        public int max_visitors { get; set; }
        public int uppert_price { get; set; }
        public int coco_price { get; set; }
        public int sub_areas { get; set; }
        public int priority { get; set; }
        public int active { get; set; }
        public ConcurrentDictionary<int, Session> users { get; set; }
        public AreaMap MapaBytes { get; set; }
        public Area(DataRow row)
        {
            this.id = int.Parse(row["id"].ToString());
            this.name = row["nombre"].ToString();
            this.model = int.Parse(row["modelo"].ToString());
            this.es_category = int.Parse(row["es_categoria"].ToString());
            this.category = int.Parse(row["categoria"].ToString());
            this.max_visitors = int.Parse(row["max_visitantes"].ToString());
            this.uppert_price = int.Parse(row["uppert"].ToString());
            this.coco_price = int.Parse(row["coco"].ToString());
            this.sub_areas = int.Parse(row["sub_escenarios"].ToString());
            this.priority = int.Parse(row["prioridad"].ToString());
            this.active = int.Parse(row["Visible"].ToString());
            this.users = new ConcurrentDictionary<int, Session>();
            this.MapaBytes = setAreaMap();
        }
        //FUNCTIONS
        private bool removeUser(Session Session)
        {
            if (this.users.ContainsKey(Session.User.areaKey))
            {
                this.users.TryRemove(Session.User.areaKey, out Session);
                ServerMessage server = new ServerMessage(new byte[] { 128, 123 }, new object[] { Session.User.areaKey });
                SendData(server);
                Session.User.Area = null;
                Session.User.areaKey = -1;
                return true;
            }
            return false;
        }
        //MODEL SETTERS
        private AreaMap setAreaMap()
        {
            if (this.category == 1 || this.category == 3)
            {
                return AreaMapDAO.getPublicAreaMap(this.id);
            }
            if (this.category == 2 || this.category == 4)
            {
                return AreaMapDAO.getPrivateAreaMap(this.id);
            }
            if (this.category == 5)
            {
                return AreaMapDAO.getGameAreaMap(this.id);
            }
            return null;
        }
        //MODEL GETTERS
        public int getAreaKeyForUser()
        {
            int key = 1;
            while (this.users.ContainsKey(key))
            {
                key++;
            }
            return key;
        }
        public Session getSession(int Key)
        {
            if (this.users.ContainsKey(Key))
            {
                return this.users[Key];
            }
            return null;
        }
        public Session getSession(int x, int y)
        {
            foreach (Session Session in this.users.Values)
            {
                if (Session.User.Posicion.x == x && Session.User.Posicion.y == y)
                {
                    return Session;
                }
            }
            return null;
        }
        private ServerMessage getUserDataPackage(Session Session, ServerMessage server)
        {
            server.AppendParameter(Session.User.areaKey);
            server.AppendParameter(Session.User.name);
            server.AppendParameter(Session.User.avatar);
            server.AppendParameter(Session.User.colors);
            server.AppendParameter(Session.User.Posicion.x);
            server.AppendParameter(Session.User.Posicion.y);
            server.AppendParameter(Session.User.Posicion.z);
            server.AppendParameter("BoomBang");
            server.AppendParameter(Session.User.age);
            server.AppendParameter(1);
            server.AppendParameter(new object[] { (Session.User.NinjaLevel >= 1 ? 12 : 0), (Session.User.TrajeZombi == 1 ? 15 : 0), (Session.User.TrajeLobo == 1 ? 16 : 0), (Session.User.TrajeEsqueleto == 1 ? 17 : 0) });
            server.AppendParameter(-1);
            server.AppendParameter(new object[] { (Session.User.GorroToro == 1 ? 3 : 0), (Session.User.GorroAtrevido == 1 ? 4 : 0), (Session.User.GorroRana == 1 ? 5 : 0), (Session.User.GorroPanda == 1 ? 6 : 0), (Session.User.GorroConejo == 1 ? 6 : 0) });
            server.AppendParameter(Session.User.UpperSelect);
            server.AppendParameter(Session.User.ObtenerUppertLevel());
            server.AppendParameter(Session.User.CocoSelect);
            server.AppendParameter(Session.User.NivelCocos);
            server.AppendParameter(new object[] { Session.User.hobby_1, Session.User.hobby_2, Session.User.hobby_3 });
            server.AppendParameter(new object[] { Session.User.deseo_1, Session.User.deseo_2, Session.User.deseo_3 });
            server.AppendParameter(new object[] { Session.User.Votos_Legal, Session.User.Votos_Sexy, Session.User.Votos_Simpatico });
            server.AppendParameter(Session.User.description);
            server.AppendParameter(new object[] { Session.User.besos_enviados, Session.User.besos_recibidos, Session.User.jugos_enviados, Session.User.jugos_recibidos, Session.User.flores_enviadas, Session.User.flores_recibidas, Session.User.uppers_enviados, Session.User.uppers_recibidos, Session.User.cocos_enviados, Session.User.cocos_recibidos, "0³" + Session.User.rings_ganados + "³0³0³0³" + Session.User.puntos_sendero + "³0³0³" + (Session.User.NivelCocos + 1) + "³" + Session.User.puntos_cocos + "³0³" + Session.User.Cocos_FinishLevel + "³" + Session.User.NinjaLevel + "³" + Session.User.puntos_ninja + "³0³" + Session.User.Ninja_FinishLevel });
            server.AppendParameter(Session.User.admin == 1 ? 1 : Session.User.rings_ganados >= 2000 ? 1 : 0);
            server.AppendParameter(0);
            server.AppendParameter(1);
            server.AppendParameter(0);
            server.AppendParameter(Session.User.id);

            return server;
        }
        //HANDLERS
        public void removeUserByCompassHandler(Session Session)
        {
            if (removeUser(Session))
            {
                Session.SendData(new ServerMessage(new byte[] { 128, 124 }));
            }
        }
        public void sendNotificationHandler(string message)
        {
            SendData(new ServerMessage(new byte[] { 186 }, new object[] { 0, message, 3 }));
        }
        public void removeUserHandler(Session Session)
        {
            if (removeUser(Session))
            {
                Session.SendData(new ServerMessage(new byte[] { 153 }));
            }
        }
        public void loadUserHandler(Session Session)
        {
            ServerMessage server = new ServerMessage(new byte[] { 128, 122 });
            getUserDataPackage(Session, server);
            Session.SendData(server);
        }
        public ServerMessage loadUsersInAreaHandler(ServerMessage server)
        {
            foreach(Session Session in this.users.Values.ToList())
            {
                getUserDataPackage(Session, server);
            }
            return server;
        }
        public void loadAreaHandler(Session Session)
        {
            ServerMessage server = new ServerMessage(new byte[] { 128, 120 });
            server.AppendParameter(1);
            server.AppendParameter(this.es_category);
            server.AppendParameter(0);
            server.AppendParameter(0);
            server.AppendParameter(0);
            server.AppendParameter(0);
            server.AppendParameter(this.model);
            server.AppendParameter(this.name);
            server.AppendParameter(0);
            Session.SendData(server);
        }
        public void userLookDirectionHandler(Session Session)
        {
            ServerMessage server = new ServerMessage(new byte[] { 135 }, new object[] { 
                Session.User.areaKey, 
                Session.User.Posicion.x, 
                Session.User.Posicion.y, 
                Session.User.Posicion.z 
            });
            SendData(server);
        }
        public void userWalkHandler(Session Session)
        {
            ServerMessage server = new ServerMessage(new byte[] { 182 });
            server.AppendParameter(1);
            server.AppendParameter(Session.User.areaKey);
            server.AppendParameter(Session.User.Posicion.x);
            server.AppendParameter(Session.User.Posicion.y);
            server.AppendParameter(Session.User.Posicion.z);
            server.AppendParameter(750);
            server.AppendParameter((Session.User.Movimientos.Movimientos.Count >= 1 ? 1 : 0));
            SendData(server);
        }
        public void SendData(ServerMessage server, Session userSession = null)
        {
            foreach(Session Session in this.users.Values.ToList())
            {
                if (userSession != null)
                {
                    if (UserMiddleware.userInArea(Session) && UserMiddleware.userInArea(userSession)
                        && Session.User.id != userSession.User.id)
                    {
                        Session.SendData(server);
                    }
                }
                else
                {
                    if (UserMiddleware.userInArea(Session))
                    {
                        Session.SendData(server);
                    }
                }
            }
        }
    }
}
