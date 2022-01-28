using Proyect_Base.app.Connection;
using Proyect_Base.app.DAO;
using Proyect_Base.app.Helpers;
using Proyect_Base.app.Middlewares;
using Proyect_Base.app.Pathfinding;
using Proyect_Base.forms;
using Proyect_Base.logs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    public class PublicArea : Area
    {
        public int priority { get; set; }
        public int active { get; set; }
        public int minUsersToSendItemTresureChestGold { get; set; }
        public int timeToSendItemTresureChestGold { get; set; }
        public int timeToSendNextItemTresureChestGold { get; set; }
        public int minUsersToSendItemTresureChestSilver { get; set; }
        public int timeToSendItemTresureChestSilver { get; set; }
        public int timeToSendNextItemTresureChestSilver { get; set; }
        public Dictionary<int, ItemArea> items { get; set; }
        public List<AreaNpc> areaNpcs { get; set; }
        public PublicArea(DataRow row) : 
            base(row)
        {
            this.priority = int.Parse(row["prioridad"].ToString());
            this.active = int.Parse(row["active"].ToString());
            setTresureChestGold();
            setTresureChestSilver();
            this.items = new Dictionary<int, ItemArea>();
            setAreaNpc();
        }
        //MODEL SETTERS
        private void setTresureChestGold()
        {
            this.minUsersToSendItemTresureChestGold = 1;
            this.timeToSendItemTresureChestGold = ItemAreaHelper.getRandomTimeNextItem();//In seconds
            this.timeToSendNextItemTresureChestGold = this.timeToSendItemTresureChestGold;
        }
        private void setTresureChestSilver()
        {
            this.minUsersToSendItemTresureChestSilver = 1;
            this.timeToSendItemTresureChestSilver = ItemAreaHelper.getRandomTimeNextItem(); //In seconds
            this.timeToSendNextItemTresureChestSilver = this.timeToSendItemTresureChestSilver;
        }
        private void setAreaNpc()
        {
            this.areaNpcs = AreaNpcDAO.getAreaNpc(this.id);
        }
        //MODEL GETTERS
        public AreaNpc getAreaNpcWithOutMoviments()
        {
            if (this.areaNpcs.Count > 0)
            {
                return this.areaNpcs.Find(i => i.isNpcWithMoviment() == false);
            }
            return null;
        }
        private Point getLocationForItem()
        {
            Point itemLocation = this.MapaBytes.GetRandomPlace();
            while (this.getSession(itemLocation.X, itemLocation.Y) != null)
            {
                itemLocation = this.MapaBytes.GetRandomPlace();
            }
            return itemLocation;
        }
        private int getKeyForItem()
        {
            int key = new Random().Next(1, 50000);
            while (this.items.ContainsKey(key))
            {
                key = new Random().Next(1, 50000);
            }
            return key;
        }
        public bool npcOcupedPoint(int x, int y)
        {
            if (areaNpcs.Find(i => i.poster_model == 0 && i.poster_type == 0 && i.Posicion.x == x && i.Posicion.y == y) != null)
            {
                return true;
            }
            return false;
        }
        //FUNCTIONS
        public void addItem(ItemArea Item)
        {
            if (Item != null)
            {
                int itemKey = getKeyForItem();
                ItemArea newItemArea = Item.Clone();
                newItemArea.setAreaPosition(getLocationForItem());
                newItemArea.setKeyInArea(itemKey);
                this.items.Add(itemKey, newItemArea);

                resetTimeNextItem(newItemArea.modelo);
                sendItem(newItemArea);

                new Thread(() => removeItemByTime(newItemArea)).Start();
            }
        }
        private void resetTimeNextItem(int itemModel)
        {
            switch (itemModel)
            {
                case 1:
                    setTresureChestGold();
                    break;
                case 2:
                    setTresureChestSilver();
                    break;
            }
        }
        public void loadItems(Session Session)
        {
            foreach(ItemArea itemArea in this.items.Values.ToList())
            {
                Session.SendData(sendItemHandler(itemArea));
            }
        }
        private void sendItem(ItemArea Item)
        {
            SendData(sendItemHandler(Item));
        }
        public bool removeItem(ItemArea Item)
        {
            if (itemInArea(Item.keyInArea))
            {
                this.items.Remove(Item.keyInArea);
                removeItemHandler(Item.keyInArea);
                return true;
            }
            return false;
        }
        private void removeItemByTime(ItemArea Item)
        {
            try
            {
                while (Item.tiempo_desaparicion > 0 && itemInArea(Item.keyInArea))
                {
                    Item.tiempo_desaparicion--;
                    Thread.Sleep(new TimeSpan(0, 0, 1));
                }
                if (itemInArea(Item.keyInArea))
                {
                    removeItemHandler(Item.keyInArea);
                    Thread.Sleep(new TimeSpan(0, 0, 0, 0, 5000));
                    this.items.Remove(Item.keyInArea);
                }
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
        private bool itemInArea(int itemKey)
        {
            if (items.ContainsKey(itemKey))
            {
                return true;
            }
            return false;
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
        //HANDLERS
        private void removeItemHandler(int itemKey)
        {
            ServerMessage server = new ServerMessage(new byte[] { 200, 123 });
            server.AppendParameter(1);
            server.AppendParameter(itemKey);
            SendData(server);
        }
        private ServerMessage sendItemHandler(ItemArea Item)
        {
            ServerMessage server = new ServerMessage(new byte[] { 200, 120 });
            server.AppendParameter(Item.keyInArea);
            server.AppendParameter(Item.id);
            server.AppendParameter(Item.areaPosition.X);
            server.AppendParameter(Item.areaPosition.Y);
            server.AppendParameter(Item.modelo);
            server.AppendParameter(Item.tipo_caida);
            server.AppendParameter(Item.tipo_salida);//TipoApertura
            server.AppendParameter(Item.tiempo_aparicion);//TiempoAparicion
            return server;
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
            if (this.areaNpcs.Count > 0)
            {
                server.AppendParameter(this.areaNpcs.Count);
                foreach (AreaNpc areaNpc in this.areaNpcs)
                {
                    server.AppendParameter(new object[] {
                        areaNpc.id,
                        areaNpc.xml_text_id,
                        areaNpc.modelo,
                        areaNpc.name,
                        areaNpc.Posicion.x,
                        areaNpc.Posicion.y,
                        areaNpc.Posicion.z,
                        areaNpc.poster_type,
                        areaNpc.poster_model
                    });
                }
            }
            else
            {
                server.AppendParameter(0);
            }
            return server;
        }
    }
}
