using Proyect_Base.app.Collections;
using Proyect_Base.app.Connection;
using Proyect_Base.app.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    public class ItemArea
    {
        public int id { get; private set; }
        public string nombre { get; set; }
        public string message { get; set; }
        public int shop_object_id { get; set; }
        public int modelo { get; set; }
        public int tipo_salida { get; set; }
        public int tipo_caida { get; set; }
        public int tiempo_aparicion { get; set; }
        public int tiempo_desaparicion { get; set; } //In seconds
        public int keyInArea { get; set; }
        public Point areaPosition { get; set; }
        public ItemArea(DataRow row)
        {
            this.id = (int)row["id"];
            this.nombre = (string)row["nombre"];
            this.message = row["message"].ToString();
            this.shop_object_id = row["shop_object_id"].ToString() == string.Empty ? -1 : int.Parse(row["shop_object_id"].ToString());
            this.modelo = (int)row["modelo"];
            this.tipo_salida = (int)row["tipo_salida"];
            this.tipo_caida = (int)row["tipo_caida"];
            this.tiempo_aparicion = (int)row["tiempo_aparicion"];
            this.tiempo_desaparicion = (int)row["remove_in"];
        }
        //FUNCTIONS

        //MODEL SETTERS
        public ShopObject getShopObject()
        {
            return ShopObjectCollection.getShopObjectById(this.shop_object_id);
        }
        public void setAreaPosition(Point position)
        {
            this.areaPosition = position;
        }
        public void setKeyInArea(int key)
        {
            this.keyInArea = key;
        }
        //MODEL GETTERS
        public bool userOnItem(Session Session)
        {
            if (Session.User.Posicion.x == areaPosition.X && Session.User.Posicion.y == areaPosition.Y)
            {
                return true;
            }
            return false;
        }
        public ItemArea Clone()
        {
            return (ItemArea)this.MemberwiseClone();
        }
        //HANDLERS
    }
}
