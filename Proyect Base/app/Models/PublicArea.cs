using Proyect_Base.app.Connection;
using Proyect_Base.app.Middlewares;
using Proyect_Base.app.Pathfinding;
using Proyect_Base.forms;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    public class PublicArea : Area
    {
        public PublicArea(DataRow row) : 
            base(row)
        {
        
        }
        public void addUser(Session Session)
        {
            int key = this.getAreaKeyForUser();
            this.users.TryAdd(key, Session);
            Session.User.Area = this;
            Session.User.areaKey = key;
            Session.User.Bloqueos = new PreLocks();
            Session.User.Ultra_Bloqueos = new UltraLocks();
            Session.User.Movimientos = new Trayectoria(Session);
            Session.User.Posicion = new Posicion(this.MapaBytes.posX, this.MapaBytes.posY, 4);
        }
        public void loadAreaParametersHandler(Session Session)
        {
            ServerMessage server = new ServerMessage(new byte[] { 175 });
            server.AppendParameter(new object[] { 1, 0, 0 });
            server.AppendParameter(new object[] { 2, 0, 0 });
            server.AppendParameter(new object[] { 3, 0, 0 });
            server.AppendParameter(new object[] { 4, 0, 0 });//Precio Upper
            server.AppendParameter(new object[] { 5, 0, 0 });//Precio Coco
            Session.SendData(server);
        }
        public void loadAreaObjectsHandler(Session Session)
        {
            ServerMessage server = new ServerMessage(new byte[] { 128, 121, 120 });
            loadAreaNpcHandler(server);
            loadUsersInAreaHandler(server);
            Session.SendData(server);
        }
        private ServerMessage loadAreaNpcHandler(ServerMessage server)
        {
            server.AppendParameter(0);
            return server;
        }
    }
}
