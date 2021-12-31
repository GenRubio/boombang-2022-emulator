using Proyect_Base.app.DAO;
using Proyect_Base.app.Models;
using Proyect_Base.forms;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Collections
{
    class ShopObjectCollection
    {
        public static Dictionary<int, ShopObject> shopObjects = new Dictionary<int, ShopObject>();
        public static void init()
        {
            foreach (ShopObject shopObject in ShopObjectDAO.getShopObjects())
            {
                shopObjects.Add(shopObject.id, shopObject);
            }
            App.Form.WriteLine("Se han cargado: " + shopObjects.Count() + " objetos de catalago.");
        }
        public static ShopObject getShopObjectById(int id)
        {
            if (shopObjects.ContainsKey(id))
            {
                return shopObjects[id];
            }
            return null;
        }
    }
}
