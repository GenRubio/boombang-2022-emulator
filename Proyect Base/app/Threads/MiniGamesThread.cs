using Proyect_Base.app.Collections;
using Proyect_Base.app.Connection;
using Proyect_Base.app.Models;
using Proyect_Base.logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proyect_Base.app.Threads
{
    class MiniGamesThread
    {
        public static void init()
        {
            new Thread(searchInscriptions).Start();
        }
        private static void searchInscriptions()
        {
            while (true)
            {
                try
                {
                    invokeGoldenRingUsers();
                }
                catch(Exception ex)
                {
                    Log.error(ex);
                }
                Thread.Sleep(1000);
            }
        }
        private static void invokeGoldenRingUsers()
        {
            List<Session> registeredUsers = getUsersWithInscription(2, 2, 1);
            MiniGame miniGameArea = MiniGameAreaCollection.getMiniGameAreaByGameIdAndGameType(2, 2).Clone();
            if (miniGameArea != null)
            {
                miniGameArea.setUid();
                MiniGamesCollections.addMiniGameRing(miniGameArea);
                new MiniGameCallUsers(registeredUsers, miniGameArea);
            }
        }
        private static List<Session> getUsersWithInscription(int gameId, int gameType, int numberOfUsersToCall)
        {
            return SessionCollection.onlineUsers.Values.ToList().Where(i => i.User.miniGameInscriptions.Find(a => a.gameId == gameId && a.gameType == gameType) != null
            && i.User.inMiniGame == false).Take(numberOfUsersToCall).ToList();
        }
    }
}
