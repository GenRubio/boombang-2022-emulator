using Proyect_Base.app.DAO;
using Proyect_Base.app.Models;
using Proyect_Base.forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Collections
{
    class MiniGameAreaCollection
    {
        public static Dictionary<int, MiniGame> miniGameAreas = new Dictionary<int, MiniGame>();
        public static void init()
        {
            foreach (MiniGame miniGameArea in MiniGameAreaDAO.getMiniGameAreas())
            {
                miniGameAreas.Add(miniGameArea.id, miniGameArea);
            }
            App.Form.WriteLine("Se han cargado: " + miniGameAreas.Count() + " minigame areas.");
        }
        public static MiniGame getMiniGameAreaById(int id)
        {
            if (miniGameAreas.ContainsKey(id))
            {
                return miniGameAreas[id];
            }
            return null;
        }
        public static MiniGame getMiniGameAreaByGameIdAndGameType(int gameId, int gameType)
        {
            return miniGameAreas.Values.ToList().Find(i => i.model == gameId && i.es_category == gameType);
        }
    }
}
