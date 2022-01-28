using Proyect_Base.app.Connection;
using Proyect_Base.app.Pathfinding;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    public class MiniGame : Area
    {
        public long uid { get; set; }
        public bool open { get; set; }
        public MiniGame(DataRow row) :
            base(row)
        {
            this.open = true;
        }
        //FUNCTIONS
        public void addUser(Session Session)
        {
            int key = this.getAreaKeyForUser();
            this.users.TryAdd(key, Session);
            Session.User.Area = this;
            Session.User.areaKey = key;
            Session.User.Bloqueos = new PreLocks();
            Session.User.Ultra_Bloqueos = new UltraLocks();
            Session.User.Movimientos = new Trayectoria(Session);
            Session.User.Posicion = new Posicion(11, 11, 4);
        }
        //MODEL SETTERS
        public void setUid()
        {
            this.uid = DateTime.Now.Ticks;
        }
        //MODEL GETTERS
        public MiniGame Clone()
        {
            return (MiniGame)this.MemberwiseClone();
        }
        //HANDLERS
    }
}
