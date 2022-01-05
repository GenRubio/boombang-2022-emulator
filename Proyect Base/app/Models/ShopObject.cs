using Proyect_Base.app.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    class ShopObject
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public int Precio_Oro { get; set; }
        public int Precio_Plata { get; set; }
        public int Categoria { get; set; }
        public string Color_1 { get; set; }
        public string Color_2 { get; set; }
        public string size_m { get; set; }
        public string size_b { get; set; }
        public string size_s { get; set; }
        public string something_1 { get; set; }
        public string something_2 { get; set; }
        public string something_3 { get; set; }
        public string something_4 { get; set; }
        public string something_5 { get; set; }
        public string something_6 { get; set; }
        public string something_10 { get; set; }
        public int vip { get; set; }
        public string something_11 { get; set; }
        public string something_12 { get; set; }
        public string something_13 { get; set; }
        public string something_14 { get; set; }
        public string something_15 { get; set; }
        public string something_16 { get; set; }
        public string something_17 { get; set; }
        public int Activado { get; set; }
        public ShopObject(DataRow row)
        {
            this.id = (int)row["id"];
            this.Nombre = (string)row["swf"];
            this.vip = (int)row["vip"];
            this.Precio_Oro = (int)row["precio_oro"];
            this.Precio_Plata = (int)row["precio_plata"];
            this.Categoria = (int)row["categoria"];
            this.Color_1 = (string)row["colores"];
            this.Color_2 = (string)row["colores_rgb"];
            this.size_m = (string)row["size_m"];
            this.size_b = (string)row["size_b"];
            this.size_s = (string)row["size_s"];
            this.something_1 = (string)row["something_1"];
            this.something_2 = (string)row["something_2"];
            this.something_3 = (string)row["something_3"];
            this.something_4 = (string)row["something_4"];
            this.something_5 = (string)row["something_5"];
            this.something_6 = (string)row["something_6"];
            this.something_10 = (string)row["something_10"];
            this.something_11 = (string)row["something_11"];
            this.something_12 = (string)row["something_12"];
            this.something_13 = (string)row["something_13"];
            this.something_14 = (string)row["something_14"];
            this.something_15 = (string)row["something_15"];
            this.something_16 = (string)row["something_16"];
            this.something_17 = (string)row["something_17"];
            this.Activado = (int)row["activado"];
        }
        //FUNCTIONS

        //MODEL SETTERS

        //MODEL GETTERS

        //HANDLERS
        public ServerMessage getObjectParametersHandler(ServerMessage server)
        {
            server.AppendParameter(this.id);
            server.AppendParameter(this.Nombre);
            server.AppendParameter(this.Precio_Oro);
            server.AppendParameter(this.Precio_Plata);
            server.AppendParameter(this.Categoria);
            server.AppendParameter(this.Color_1);
            server.AppendParameter(this.Color_2);
            server.AppendParameter(this.size_m);
            server.AppendParameter(this.size_b);
            server.AppendParameter(this.size_s);
            server.AppendParameter(this.something_1);
            server.AppendParameter(this.something_2);
            server.AppendParameter(this.something_3);
            server.AppendParameter(this.something_4);
            server.AppendParameter(this.something_5);
            server.AppendParameter(this.something_6);
            server.AppendParameter(this.something_10);
            server.AppendParameter(this.vip);
            server.AppendParameter(this.something_11);
            server.AppendParameter(this.Activado);
            server.AppendParameter(this.something_12);
            server.AppendParameter(this.something_13);
            server.AppendParameter(this.something_14);
            server.AppendParameter(this.something_15);
            server.AppendParameter(this.something_16);
            server.AppendParameter(this.something_17);

            return server;
        }
    }
}
