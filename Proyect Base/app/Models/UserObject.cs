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
    public class UserObject
    {
        public int id { get; set; }
        public int ObjetoID { get; set; }
        public string Color_1 { get; set; }
        public string Color_2 { get; set; }
        public string size{ get; set; }
        public int rotation{ get; set; }
        public string something_4{ get; set; }
        public int UserID{ get; set; }
        public int ZonaID{ get; set; }
        public Posicion Posicion{ get; set; }
        public string height{ get; set; }
        public string ocupe{ get; set; }
        public string data{ get; set; }
        public string swf{ get; set; }
        public UserObject(DataRow row)
        {
            this.id = (int)row["id"];
            this.ObjetoID = (int)row["ItemID"];
            this.Color_1 = (string)row["color"];
            this.Color_2 = (string)row["rgb_ratio"];
            this.size = (string)row["size"];
            this.rotation = (int)row["rotation"];
            this.something_4 = (string)row["something_4"];
            this.UserID = (int)row["UserID"];
            this.ZonaID = (int)row["sala_id"];
            this.Posicion = new Posicion((int)row["x"], (int)row["y"]);
            this.height = (string)row["height"];
            this.ocupe = (string)row["ocupe"];
            this.data = (string)row["data"];
            this.swf = (string)row["swf"];
        }
        //FUNCTIONS
        public void updateAttributes(User User, 
            int areaId, int x, int y, int height, string coordinates, int rotation, string color, string colorRGB, string size)
        {
            updateAttributesUserObjectOrAreaObject(this, areaId, x, y, 
                height, coordinates, rotation, color, colorRGB, size);

            if (User != null)
            {
                UserObject userObject = User.getObjectById(this.id);
                if (userObject != null)
                {
                    updateAttributesUserObjectOrAreaObject(userObject, areaId, x, y,
                        height, coordinates, rotation, color, colorRGB, size);
                }
            }
        }
        private void updateAttributesUserObjectOrAreaObject(UserObject userObject, 
            int areaId, int x, int y, int height, string coordinates, int rotation, string color, string colorRGB, string size)
        {
            userObject.ocupe = coordinates;
            userObject.ZonaID = areaId;
            userObject.Posicion.x = x;
            userObject.Posicion.y = y;
            userObject.height = height.ToString();
            userObject.rotation = rotation;
            userObject.Color_1 = color;
            userObject.Color_2 = colorRGB;
            userObject.size = size;
        }
        //MODEL SETTERS

        //MODEL GETTERS

        //HANDLERS
        public ServerMessage loadBackpackObjectParametersHandler(ServerMessage server)
        {
            server.AppendParameter(this.id);
            server.AppendParameter(this.ObjetoID);
            server.AppendParameter(this.Color_1);
            server.AppendParameter(this.Color_2);
            server.AppendParameter(this.size);
            server.AppendParameter(this.rotation);
            server.AppendParameter(false);//top numero, nombre
            server.AppendParameter(false);
            server.AppendParameter(1);
            return server;
        }
    }
}
