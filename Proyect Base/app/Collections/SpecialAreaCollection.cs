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
    class SpecialAreaCollection
    {
        public static Dictionary<int, SpecialArea> specialAreas = new Dictionary<int, SpecialArea>();
        public static void init()
        {
            foreach (SpecialArea islandArea in SpecialAreaDAO.getIslandAreas())
            {
                specialAreas.Add(islandArea.id, islandArea);
            }
            App.Form.WriteLine("Se han cargado: " + specialAreas.Count() + " areas especiales.");
        }
        public static SpecialArea getSpecialAreaById(int id)
        {
            if (specialAreas.ContainsKey(id))
            {
                return specialAreas[id];
            }
            return null;
        }
        public static List<IslandArea> getIslandAreasByIslandId(int id)
        {
            List<IslandArea> islandAreas = new List<IslandArea>();
            foreach(SpecialArea specialArea in specialAreas.Values.ToList())
            {
                if (specialArea is IslandArea)
                {
                    islandAreas.Add((IslandArea)specialArea);
                }
            }
            return islandAreas.Where(i => i.islandId == id).ToList();
        }
        public static IslandArea getIslandAreaById(int id)
        {
            if (specialAreas.ContainsKey(id) && specialAreas[id] is IslandArea)
            {
                return (IslandArea)specialAreas[id];
            }
            return null;
        }
        public static void addIslandArea(IslandArea islandArea)
        {
            if (!specialAreas.ContainsKey(islandArea.id))
            {
                specialAreas.Add(islandArea.id, islandArea);
            }
        }
        public static void removeIslandArea(User User, int id)
        {
            if (specialAreas.ContainsKey(id) && specialAreas[id] is IslandArea)
            {
                IslandArea islandArea = (IslandArea)specialAreas[id];
                if (islandArea.userCreatorId == User.id)
                {
                    specialAreas.Remove(id);
                }
            }
        }
        public static void updateNameIslandArea(User User, int id, string name)
        {
            if (specialAreas.ContainsKey(id) && specialAreas[id] is IslandArea)
            {
                IslandArea islandArea = (IslandArea)specialAreas[id];
                if (islandArea.userCreatorId == User.id)
                {
                    specialAreas[id].name = name;
                }
            }
        }
    }
}
