using Proyect_Base.app.Collections;
using Proyect_Base.app.Models;
using Proyect_Base.forms;
using Proyect_Base.logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proyect_Base.app.Threads
{
    class ItemAreaThread
    {
        public static void init()
        {
            new Thread(ItemArea).Start();
        }
        private static void ItemArea()
        {
            List<PublicArea> areas = getAreas();
            while (true)
            {
                try
                {
                    foreach (PublicArea area in areas)
                    {
                        sendTresureChestGold(area);
                        sendTresureChestSilver(area);
                    }
                }
                catch(Exception ex)
                {
                    Log.error(ex);
                }
                Thread.Sleep(1000);
            }
        }
        private static void sendTresureChestSilver(PublicArea area)
        {
            if (area.users.Count >= area.minUsersToSendItemTresureChestSilver)
            {
                if (area.timeToSendNextItemTresureChestSilver <= 0)
                {
                    area.addItem(ItemAreaCollection.getItemAreaByModel(2));
                }
                else
                {
                    area.timeToSendNextItemTresureChestSilver--;
                }
            }
        }
        private static void sendTresureChestGold(PublicArea area)
        {
            if (area.users.Count >= area.minUsersToSendItemTresureChestGold)
            {
                if (area.timeToSendNextItemTresureChestGold <= 0)
                {
                    area.addItem(ItemAreaCollection.getItemAreaByModel(1));
                }
                else
                {
                    area.timeToSendNextItemTresureChestGold--;
                }
            }
        }
        private static List<PublicArea> getAreas()
        {
            List<PublicArea> publicAreas = PublicAreaCollection.publicAreas.Values.ToList();
            List<GameArea> gameAreas = GameAreaCollection.gameAreas.Values.ToList();
            return publicAreas.Concat(gameAreas).ToList();
        }
    }
}
