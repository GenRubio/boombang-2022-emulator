using Proyect_Base.app.Collections;
using Proyect_Base.app.Connection;
using Proyect_Base.app.DAO;
using Proyect_Base.app.Pathfinding;
using Proyect_Base.app.Pathfinding.A_Star;
using Proyect_Base.forms;
using Proyect_Base.logs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    public class AreaNpc
    {
        public int id { get; set; }
        public int public_area_id { get; set; }
        public string name { get; set; }
        public string swf { get; set; }
        public int modelo { get; set; }
        public int xml_text_id { get; set; }
        public int poster_type { get; set; }
        public int poster_model { get; set; }
        public int pos_x { get; set; }
        public int pos_y { get; set; }
        public int pos_z { get; set; }
        public bool active { get; set; }
        #region Pathfinding
        public PathFinder PathFinder { get; set; }
        public SearchParameters searchParameters { get; set; }
        public Trayectoria Movimientos { get; set; }
        public Posicion LastPoint { get; set; }
        public Posicion Posicion { get; set; }
        #endregion
        #region PreLocks
        public PreLocks Bloqueos { get; set; }
        public UltraLocks Ultra_Bloqueos { get; set; }
        #endregion
        public List<AreaNpcObject> areaNpcObjects { get; set; }
        public AreaNpc(DataRow row)
        {
            this.id = (int)row["id"];
            this.public_area_id = row["public_area_id"].ToString() == string.Empty ? -1 : int.Parse(row["public_area_id"].ToString());
            this.name = row["name"].ToString();
            this.swf = row["swf"].ToString();
            this.modelo = (int)row["modelo"];
            this.xml_text_id = (int)row["xml_text_id"];
            this.poster_type = (int)row["poster_type"];
            this.poster_model = (int)row["poster_model"];
            this.pos_x = (int)row["pos_x"];
            this.pos_y = (int)row["pos_y"];
            this.pos_z = (int)row["pos_z"];
            this.active = bool.Parse(row["active"].ToString());
            this.Posicion = new Posicion((int)row["pos_x"], (int)row["pos_y"], (int)row["pos_z"]);
            this.areaNpcObjects = AreaNpcObjectDAO.getAreaNpcObjects(this.public_area_id);
        }
        //FUNCTIONS

        //MODEL SETTERS

        //MODEL GETTERS
        public string getNewPatchCoordenates(Random random)
        {
            List<string> positions = new List<string>();
            positions.Add("11127111371114711157111671117711187111971120711217");
            positions.Add("101240913408144071540615805158");
            positions.Add("1212113125141251512516125");
            positions.Add("121211313113147");
            positions.Add("12103131051410515105");
            positions.Add("10124091340814407154071670717707187");
            positions.Add("10102090920908609076");

            return positions[random.Next(0, positions.Count())];
        }
        public bool isNpcWithMoviment()
        {
            if (this.poster_type == 0 && this.poster_model == 0)
            {
                return true;
            }
            return false;
        }
        public PublicArea getArea()
        {
            return PublicAreaCollection.getPublicAreaById(this.public_area_id);
        }
        public AreaNpcObject getObjectById(int id)
        {
            return this.areaNpcObjects.Find(i => i.id == id);
        }
        //HANDLERS
        public void sendWalkHandler(PublicArea publicArea)
        {
            ServerMessage server = new ServerMessage(new byte[] { 182 });
            server.AppendParameter(3);
            server.AppendParameter(this.id);
            server.AppendParameter(this.Posicion.x);
            server.AppendParameter(this.Posicion.y);
            server.AppendParameter(this.Posicion.z);
            server.AppendParameter(750);
            server.AppendParameter(1);
            publicArea.SendData(server);
        }
        public void loadNpcContentHandler(Session Session)
        {
            ServerMessage server = new ServerMessage(new byte[] { 123, 120 });
            server.AppendParameter(this.poster_model);
            foreach(AreaNpcObject areaNpcObject in this.areaNpcObjects)
            {
                server.AppendParameter(areaNpcObject.id);
                server.AppendParameter(0);
                server.AppendParameter(areaNpcObject.price_gold);
                server.AppendParameter(areaNpcObject.price_silver);
                server.AppendParameter(areaNpcObject.shop_object_id);

                getRequirementObjectsHandler(server, areaNpcObject);
            }
            Session.SendData(server);
        }
        private ServerMessage getRequirementObjectsHandler(ServerMessage server, AreaNpcObject areaNpcObject)
        {
            string objectIds = "-1";
            string amounts = "-1";
            if (areaNpcObject.areaNpcObjectRequirements.Count > 0)
            {
                objectIds = "";
                amounts = "";
                foreach (AreaNpcObjectRequirement areaNpcObjectRequirement in areaNpcObject.areaNpcObjectRequirements)
                {
                    objectIds += areaNpcObjectRequirement.shop_object_id + "³";
                    amounts += areaNpcObjectRequirement.amount + "³";
                }
                if (objectIds.Length != 0)
                {
                    objectIds = objectIds.Remove(objectIds.Length - 1, 1);
                }
            }
            server.AppendParameter(objectIds);
            server.AppendParameter(amounts);

            return server;
        }
        public void loadInscriptionGamePanel(Session Session)
        {
            ServerMessage server = new ServerMessage(new byte[] { 210, 127, 120 });
            server.AppendParameter(1);
            switch (Session.User.Area.model)
            {
                case 102:
                    server.AppendParameter(2);
                    break;
            }

            server.AppendParameter(0);

            server.AppendParameter(100);
            server.AppendParameter(1);
            server.AppendParameter(1);
            server.AppendParameter(1);
            server.AppendParameter(1);
            server.AppendParameter(1);
            server.AppendParameter(-1);
            server.AppendParameter(100);
            server.AppendParameter(2);
            server.AppendParameter(3);
            server.AppendParameter(0);
            server.AppendParameter(0);
            server.AppendParameter(1);
            server.AppendParameter(1);
            server.AppendParameter(0);
            server.AppendParameter(1);
            server.AppendParameter(2);
            server.AppendParameter(-1);
            server.AppendParameter(10);
            Session.SendData(server);
        }
    }
}
