using Proyect_Base.app.Connection;
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
        public int minUsersToSendItemTresureChestGold { get; set; }
        public int timeToSendItemTresureChestGold { get; set; } //In seconds
        public int timeToSendNextItemTresureChestGold { get; set; }
        public int minUsersToSendItemTresureChestSilver { get; set; }
        public int timeToSendItemTresureChestSilver { get; set; } //In seconds
        public int timeToSendNextItemTresureChestSilver { get; set; }
        public Dictionary<int, ItemArea> items { get; set; }
        public PublicArea(DataRow row) : 
            base(row)
        {
            setTresureChestGold();
            setTresureChestSilver();
            this.items = new Dictionary<int, ItemArea>();
        }
        private void setTresureChestGold()
        {
            this.minUsersToSendItemTresureChestGold = 1;
            this.timeToSendItemTresureChestGold = 10;
            this.timeToSendNextItemTresureChestGold = this.timeToSendItemTresureChestGold;
        }
        private void setTresureChestSilver()
        {
            this.minUsersToSendItemTresureChestSilver = 1;
            this.timeToSendItemTresureChestSilver = 15;
            this.timeToSendNextItemTresureChestSilver = this.timeToSendItemTresureChestSilver;
        }
        public void addItem(ItemArea Item)
        {
            if (Item != null)
            {
                int itemKey = getKeyForItem();
                this.items.Add(itemKey, Item);
                resetTimeNextItem(Item.modelo);
                sendItemHandler(itemKey, Item, getLocationForItem());
                new Thread(() => removeItemByTime(itemKey, Item)).Start();
            }
        }
        private void resetTimeNextItem(int itemModel)
        {
            switch (itemModel)
            {
                case 1:
                    this.timeToSendNextItemTresureChestGold = this.timeToSendItemTresureChestGold;
                    break;
                case 2:
                    this.timeToSendNextItemTresureChestSilver = this.timeToSendItemTresureChestSilver;
                    break;
            }
        }
        private void sendItemHandler(int itemKey, ItemArea Item, Point Position)
        {
            ServerMessage server = new ServerMessage(new byte[] { 200, 120 });
            server.AppendParameter(itemKey);
            server.AppendParameter(Item.id);
            server.AppendParameter(Position.X);
            server.AppendParameter(Position.Y);
            server.AppendParameter(Item.modelo);
            server.AppendParameter(Item.tipo_caida);
            server.AppendParameter(Item.tipo_salida);//TipoApertura
            server.AppendParameter(Item.tiempo_aparicion);//TiempoAparicion
            SendData(server);
        }
        private void removeItemByTime(int itemKey, ItemArea Item)
        {
            try
            {
                int timeToRemoveItem = Item.tiempo_desaparicion;
                while (timeToRemoveItem > 0 && itemInArea(itemKey))
                {
                    timeToRemoveItem--;
                    Thread.Sleep(new TimeSpan(0, 0, 1));
                }
                if (itemInArea(itemKey))
                {
                    this.items.Remove(itemKey);
                    removeItemHandler(itemKey);
                }
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
        private void removeItemHandler(int itemKey)
        {
            ServerMessage server = new ServerMessage(new byte[] { 200, 123 });
            server.AppendParameter(1);
            server.AppendParameter(itemKey);
            SendData(server);
        }
        private bool itemInArea(int itemKey)
        {
            if (items.ContainsKey(itemKey))
            {
                return true;
            }
            return false;
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
