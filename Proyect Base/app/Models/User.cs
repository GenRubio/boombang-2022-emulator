using Proyect_Base.app.Connection;
using Proyect_Base.app.DAO;
using Proyect_Base.app.Helpers;
using Proyect_Base.app.Pathfinding;
using Proyect_Base.app.Pathfinding.A_Star;
using Proyect_Base.forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    public class User
    {
        public int id { get; set; }
        public int areaKey { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string security { get; set; }
        public int avatar { get; set; }
        public string colors { get; set; }
        public int age { get; set; }
        public bool vip { get; set; }
        public DateTime end_vip { get; set; }
        public string email { get; set; }
        public string current_ip { get; set; }
        public int oro { get; set; }
        public int plata { get; set; }
        public bool admin { get; set; }
        public string description { get; set; }
        public int besos_enviados { get; set; }
        public int besos_recibidos { get; set; }
        public int jugos_enviados { get; set; }
        public int jugos_recibidos { get; set; }
        public int flores_enviadas { get; set; }
        public int flores_recibidas { get; set; }
        public int uppers_enviados { get; set; }
        public int uppers_recibidos { get; set; }
        public int cocos_enviados { get; set; }
        public int cocos_recibidos { get; set; }
        public string hobby_1 { get; set; }
        public string hobby_2 { get; set; }
        public string hobby_3 { get; set; }
        public string deseo_1 { get; set; }
        public string deseo_2 { get; set; }
        public string deseo_3 { get; set; }
        public int Votos_Legal { get; set; }
        public int Votos_Sexy { get; set; }
        public int Votos_Simpatico { get; set; }
        public DateTime last_connection { get; set; }
        public DateTime fecha_registro { get; set; }
        public int rings_ganados { get; set; }
        public int puntos_cocos { get; set; }
        public int puntos_ninja { get; set; }
        public int puntos_sendero { get; set; }
        public bool GorroToro { get; set; }
        public bool GorroAtrevido { get; set; }
        public bool GorroPanda { get; set; }
        public bool GorroRana { get; set; }
        public bool GorroConejo { get; set; }
        public bool TrajeLobo { get; set; }
        public bool TrajeZombi { get; set; }
        public bool TrajeEsqueleto { get; set; }
        public int UpperSelect { get; set; }
        public int NinjaLevel { get; set; }
        public int Ninja_FinishLevel { get; set; }
        public int NivelCocos { get; set; }
        public int Cocos_FinishLevel { get; set; }
        public int CocoSelect { get; set; }
        public Area Area { get; set; }
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
        public List<UserBackpackObject> backpackObjects { get; set; }
        public Dictionary<int, UserObject> objects { get; set; }
        public Dictionary<int, Island> islands { get; set; }
        public User(DataRow row)
        {
            this.id = int.Parse(row["id"].ToString());
            this.name = row["name"].ToString();
            this.password = row["password"].ToString();
            this.security = row["security_password"].ToString();
            this.avatar = int.Parse(row["avatar_model_id"].ToString());
            this.colors = row["colors"].ToString();
            this.age = int.Parse(row["age"].ToString());
            this.vip = bool.Parse(row["vip"].ToString());
            this.end_vip = TimeHelper.getDate(row["end_vip"].ToString());
            this.email = row["email"].ToString();
            this.oro = int.Parse(row["coins_gold"].ToString());
            this.plata = int.Parse(row["coins_silver"].ToString());
            this.admin = bool.Parse(row["admin"].ToString());
            this.description = row["description"].ToString();
            this.besos_enviados = int.Parse(row["besos_enviados"].ToString());
            this.besos_recibidos = int.Parse(row["besos_recibidos"].ToString());
            this.jugos_enviados = int.Parse(row["jugos_enviados"].ToString());
            this.jugos_recibidos = int.Parse(row["jugos_recibidos"].ToString());
            this.flores_enviadas = int.Parse(row["flores_enviadas"].ToString());
            this.flores_recibidas = int.Parse(row["flores_recibidas"].ToString());
            this.uppers_enviados = int.Parse(row["uppers_enviados"].ToString());
            this.uppers_recibidos = int.Parse(row["uppers_recibidos"].ToString());
            this.cocos_enviados = int.Parse(row["cocos_enviados"].ToString());
            this.cocos_recibidos = int.Parse(row["cocos_recibidos"].ToString());
            this.hobby_1 = row["hobby_1"].ToString();
            this.hobby_2 = row["hobby_2"].ToString();
            this.hobby_3 = row["hobby_3"].ToString();
            this.deseo_1 = row["deseo_1"].ToString();
            this.deseo_2 = row["deseo_2"].ToString();
            this.deseo_3 = row["deseo_3"].ToString();
            this.rings_ganados = int.Parse(row["rings_ganados"].ToString());
            this.puntos_ninja = int.Parse(row["puntos_ninja"].ToString());
            this.puntos_cocos = int.Parse(row["puntos_cocos"].ToString());
            this.puntos_sendero = int.Parse(row["senderos_ganados"].ToString());
            this.Votos_Legal = int.Parse(row["votos_legal"].ToString());
            this.Votos_Sexy = int.Parse(row["votos_sexy"].ToString());
            this.Votos_Simpatico = int.Parse(row["votos_simpatico"].ToString());
            this.GorroToro = bool.Parse(row["gr_toro"].ToString());
            this.GorroRana = bool.Parse(row["gr_rana"].ToString());
            this.GorroPanda = bool.Parse(row["gr_panda"].ToString());
            this.GorroConejo = bool.Parse(row["gr_conejo"].ToString());
            this.GorroAtrevido = bool.Parse(row["gr_atrevido"].ToString());
            this.TrajeLobo = bool.Parse(row["tr_lobo"].ToString());
            this.TrajeZombi = bool.Parse(row["tr_zombi"].ToString());
            this.TrajeEsqueleto = bool.Parse(row["tr_esqueleto"].ToString());
            this.fecha_registro = TimeHelper.getDate(row["fecha_registro"].ToString());
            this.last_connection = TimeHelper.getDate(row["last_connection"].ToString());
            this.UpperSelect = ObtenerUppertLevel();
            this.CargarDatosNinja();
            this.CargarDatosCocos();
            this.CocoSelect = NivelCocos;
            this.Area = null;
            this.areaKey = -1;
            this.Bloqueos = new PreLocks();
            this.Ultra_Bloqueos = new UltraLocks();
            this.backpackObjects = new List<UserBackpackObject>();
            this.objects = setObjects();
            this.islands = setIslands();
        }
        //MODEL SETTERS
        public void CargarDatosNinja()
        {
            if (puntos_ninja >= 0 && puntos_ninja <= 199)
            {
                NinjaLevel = 0;
                Ninja_FinishLevel = 200;
            }
            if (puntos_ninja >= 200 && puntos_ninja <= 204)
            {
                NinjaLevel = 1;
                Ninja_FinishLevel = 205;
            }
            if (puntos_ninja >= 205 && puntos_ninja <= 209)
            {
                NinjaLevel = 2;
                Ninja_FinishLevel = 210;
            }
            if (puntos_ninja >= 210 && puntos_ninja <= 224)
            {
                NinjaLevel = 3;
                Ninja_FinishLevel = 225;
            }
            if (puntos_ninja >= 225 && puntos_ninja <= 249)
            {
                NinjaLevel = 4;
                Ninja_FinishLevel = 250;
            }
            if (puntos_ninja >= 250 && puntos_ninja <= 274)
            {
                NinjaLevel = 5;
                Ninja_FinishLevel = 275;
            }
            if (puntos_ninja >= 275 && puntos_ninja <= 299)
            {
                NinjaLevel = 6;
                Ninja_FinishLevel = 300;
            }
            if (puntos_ninja >= 300 && puntos_ninja <= 349)
            {
                NinjaLevel = 7;
                Ninja_FinishLevel = 350;
            }
            if (puntos_ninja >= 350 && puntos_ninja <= 399)
            {
                NinjaLevel = 8;
                Ninja_FinishLevel = 400;
            }
            if (puntos_ninja >= 400 && puntos_ninja <= 499)
            {
                NinjaLevel = 9;
                Ninja_FinishLevel = 500;
            }
            if (puntos_ninja >= 500)
            {
                NinjaLevel = 10;
                Ninja_FinishLevel = 500;
            }
        }
        public void CargarDatosCocos()
        {
            if (puntos_cocos >= 0 && puntos_cocos <= 4)
            {
                NivelCocos = 0;
                Cocos_FinishLevel = 5;
            }
            if (puntos_cocos >= 5 && puntos_cocos <= 9)
            {
                NivelCocos = 1;
                Cocos_FinishLevel = 10;
            }
            if (puntos_cocos >= 10 && puntos_cocos <= 24)
            {
                NivelCocos = 2;
                Cocos_FinishLevel = 25;
            }
            if (puntos_cocos >= 25 && puntos_cocos <= 49)
            {
                NivelCocos = 3;
                Cocos_FinishLevel = 50;
            }
            if (puntos_cocos >= 50 && puntos_cocos <= 74)
            {
                NivelCocos = 4;
                Cocos_FinishLevel = 75;
            }
            if (puntos_cocos >= 75 && puntos_cocos <= 99)
            {
                NivelCocos = 5;
                Cocos_FinishLevel = 100;
            }
            if (puntos_cocos >= 100 && puntos_cocos <= 149)
            {
                NivelCocos = 6;
                Cocos_FinishLevel = 150;
            }
            if (puntos_cocos >= 150 && puntos_cocos <= 199)
            {
                NivelCocos = 7;
                Cocos_FinishLevel = 200;
            }
            if (puntos_cocos >= 200 && puntos_cocos <= 299)
            {
                NivelCocos = 8;
                Cocos_FinishLevel = 300;
            }
            if (puntos_cocos >= 300)
            {
                NivelCocos = 9;
                Cocos_FinishLevel = 300;
            }
        }
        public void setBackpackObjects(List<UserBackpackObject> backpackObjects)
        {
            this.backpackObjects = backpackObjects;
        }
        private Dictionary<int, Island> setIslands()
        {
            return IslandDAO.getUserIslands(this);
        }
        private Dictionary<int, UserObject> setObjects()
        {
            return UserObjectDAO.getUserObjects(this);
        }
        //MODEL GETTERS
        public int ObtenerUppertLevel()
        {
            if (rings_ganados >= 1 && rings_ganados <= 9) return 1;
            if (rings_ganados >= 10 && rings_ganados <= 24) return 2;
            if (rings_ganados >= 25 && rings_ganados <= 49) return 3;
            if (rings_ganados >= 50 && rings_ganados <= 99) return 4;
            if (rings_ganados >= 100 && rings_ganados <= 199) return 5;
            if (rings_ganados >= 200 && rings_ganados <= 499) return 6;
            if (rings_ganados >= 500 && rings_ganados <= 999) return 7;
            if (rings_ganados >= 1000 && rings_ganados <= 1999) return 8;
            if (rings_ganados >= 2000) return 9;
            return 0;
        }
        public void getItemAreaReward(Session Session, ItemArea itemArea)
        {
            switch (itemArea.modelo)
            {
                case 1:
                    int goldCoins = 1000;
                    this.Area.sendNotificationHandler(this.name + " " + itemArea.message);
                    addGoldCoins(Session, goldCoins);
                    break;
                case 2:
                    int silverCoins = 250;
                    this.Area.sendNotificationHandler(this.name + " " + itemArea.message);
                    addSilverCoins(Session, silverCoins);
                    break;
            }
            addItemObjectToUser(Session, itemArea);
        }
        public Island getIsland(int id)
        {
            if (this.islands.ContainsKey(id))
            {
                return this.islands[id];
            }
            return null;
        }
        public List<UserObject> islandAreaObjects(int islandAreaId)
        {
            return this.objects.Values.ToList().Where(i => i.ZonaID == islandAreaId).ToList();
        }
        public List<UserObject> getObjectsByObjectId(int id)
        {
            return this.objects.Values.ToList().Where(i => i.ObjetoID == id).ToList();
        }
        public UserObject getObjectById(int id)
        {
            if (this.objects.ContainsKey(id))
            {
                return this.objects[id];
            }
            return null;
        }
        //FUNCTIONS
        private void addItemObjectToUser(Session Session, ItemArea itemArea)
        {
            ShopObject shopObject = itemArea.getShopObject();
            if (shopObject != null)
            {
                UserObject userObject = UserObjectDAO.createObjectUser(this, shopObject, "tam_n");
                if (userObject != null)
                {
                    addObject(userObject);
                    addObjectToBackpackHandler(Session, userObject);
                }
            }
        }
        public void removeIsland(int id)
        {
            this.islands.Remove(id);
        }
        public void addIsland(Island island)
        {
            if (!this.islands.ContainsKey(island.id))
            {
                this.islands.Add(island.id, island);
            }
        }
        public void addObject(UserObject userObject)
        {
            if (!this.objects.ContainsKey(userObject.id))
            {
                this.objects.Add(userObject.id, userObject);
            }
        }
        public void addGoldCoins(Session Session, int coins)
        {
            this.oro += coins;
            addGoldCoinsHandler(Session, coins);
            new Thread(() => UserDAO.addGoldCoins(this, coins)).Start();
        }
        public void removeGoldCoins(Session Session, int coins)
        {
            this.oro -= coins;
            removeGoldCoinsHandler(Session, coins);
            new Thread(() => UserDAO.removeGoldCoins(this, coins)).Start();
        }
        public void addSilverCoins(Session Session, int coins)
        {
            this.plata += coins;
            addSilverCoinsHandler(Session, coins);
            new Thread(() => UserDAO.addSilverCoins(this, coins)).Start();
        }
        public void removeSilverCoins(Session Session, int coins)
        {
            this.plata -= coins;
            removeSilverCoinsHandler(Session, coins);
            new Thread(() => UserDAO.removeSilverCoins(this, coins)).Start();
        }
        //HANDLERS
        public void removeObjectBackpackHandler(Session Session, UserObject userObject)
        {
            Session.SendData(new ServerMessage(new byte[] { 189, 169 }, new object[] { userObject.id, userObject.ObjetoID, 1 }));
        }
        public void initLoginHandler(Session Session)
        {
            ServerMessage server = new ServerMessage(new byte[] { 120, 121 });
            server.AppendParameter(1);
            server.AppendParameter(name);
            server.AppendParameter(avatar);
            server.AppendParameter(colors);
            server.AppendParameter(email);
            server.AppendParameter(age);
            server.AppendParameter(2);
            server.AppendParameter("BoomBang");
            server.AppendParameter(0);
            server.AppendParameter(id);
            server.AppendParameter(admin ? 1 : 0);
            server.AppendParameter(oro);
            server.AppendParameter(plata);
            server.AppendParameter(200);
            server.AppendParameter(5);
            server.AppendParameter(0);
            server.AppendParameter(0);
            server.AppendParameter(190);//Concurso Actual
            server.AppendParameter(0);//puntos en concurso
            server.AppendParameter(0);
            server.AppendParameter("ES");
            server.AppendParameter(0);
            server.AppendParameter(0);
            server.AppendParameter(vip);//Vip
            server.AppendParameter(-1);// Fecha de caducidad VIP
            server.AppendParameter(1);
            server.AppendParameter(0);// Clave de seguridad 0= off 1 = ON
            server.AppendParameter(0);
            server.AppendParameter(0);
            server.AppendParameter(new object[] { 1, 0 });
            server.AppendParameter(0);
            server.AppendParameter(0);//cambioNombre(Session)
            Session.SendData(server);
        }
        private void addGoldCoinsHandler(Session Session, int coins)
        {
            Session.SendData(new ServerMessage(new byte[] { 162 }, new object[] { coins }));
        }
        private void removeGoldCoinsHandler(Session Session, int coins)
        {
            Session.SendData(new ServerMessage(new byte[] { 161 }, new object[] { coins }));
        }
        private void addSilverCoinsHandler(Session Session, int coins)
        {
            Session.SendData(new ServerMessage(new byte[] { 166 }, new object[] { coins }));
        }
        private void removeSilverCoinsHandler(Session Session, int coins)
        {
            Session.SendData(new ServerMessage(new byte[] { 168 }, new object[] { coins }));
        }
        public void loadBackpackObjectsHandler(Session Session)
        {
            setBackpackObjects(UserBackpackObjectDAO.getUserBackpackObjects(this));
            ServerMessage server = new ServerMessage(new byte[] { 189, 180 });
            foreach(UserBackpackObject userBackpackObject in this.backpackObjects)
            {
                server.AppendParameter(userBackpackObject.itemId);
                server.AppendParameter(userBackpackObject.count);
            }
            Session.SendData(server);
        }
        public void addObjectToBackpackHandler(Session Session, UserObject userObject)
        {
            Session.SendData(new ServerMessage(new byte[] { 189, 139 }, new object[] {
                                    userObject.id,
                                    userObject.ObjetoID,
                                    userObject.Color_1,
                                    userObject.Color_2,
                                    userObject.size,
                                    0,
                                    false,
                                    false,
                                    1
                                }));
        }
    }
}
