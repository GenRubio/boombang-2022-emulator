using Proyect_Base.app.DAO;
using Proyect_Base.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Collections
{
    class ItemAreaCollection
    {
        public static Dictionary<int, ItemArea> itemAreas = new Dictionary<int, ItemArea>();
        public static void init()
        {
            foreach (ItemArea itemArea in ItemAreaDAO.getItemAreas())
            {
                itemAreas.Add(itemArea.modelo, itemArea);
            }
        }
        public static ItemArea getItemAreaByModel(int model)
        {
            if (itemAreas.ContainsKey(model))
            {
                return itemAreas[model];
            }
            return null;
        }
    }
}
