using Proyect_Base.app.Connection;
using Proyect_Base.app.Pathfinding;
using Proyect_Base.forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    public class SpecialArea : Area
    {
        public string color_1 { get; set; }
        public string color_2 { get; set; }
        public SpecialArea(DataRow row) :
         base(row)
        {
            this.color_1 = row["color_1"].ToString();
            this.color_2 = row["color_2"].ToString();
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
            Session.User.Posicion = new Posicion(this.MapaBytes.posX, this.MapaBytes.posY, 4);
        }
        //MODEL SETTERS

        //MODEL GETTERS

        //HANDLERS
    }
}
