using Proyect_Base.app.DAO;
using Proyect_Base.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Collections
{
    class GameAreaCollection
    {
        public static Dictionary<int, GameArea> gameAreas = new Dictionary<int, GameArea>();
        public static void loadGameAreas()
        {
            foreach (GameArea gameArea in GameAreaDAO.getGameAreas())
            {
                gameAreas.Add(gameArea.id, gameArea);
            }
        }
        public static GameArea getGameAreaById(int id)
        {
            if (gameAreas.ContainsKey(id))
            {
                return gameAreas[id];
            }
            return null;
        }
    }
}
