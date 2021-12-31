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
    class ItemAreaCollection
    {
        public static Dictionary<int, ItemArea> itemAreas = new Dictionary<int, ItemArea>();
        public static void init()
        {
            foreach (ItemArea itemArea in ItemAreaDAO.getItemAreas())
            {
                itemAreas.Add(itemArea.modelo, itemArea);
            }
            App.Form.WriteLine("Se han cargado: " + itemAreas.Count() + " items de area.");
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
