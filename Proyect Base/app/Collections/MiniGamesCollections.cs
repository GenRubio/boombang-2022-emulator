using Proyect_Base.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Collections
{
    class MiniGamesCollections
    {
        public static Dictionary<long, MiniGame> miniGameRings = new Dictionary<long, MiniGame>();

        public static void addMiniGameRing(MiniGame miniGame)
        {
            if (!miniGameRings.ContainsKey(miniGame.uid))
            {
                miniGameRings.Add(miniGame.uid, miniGame);
            }
        }
    }
}
