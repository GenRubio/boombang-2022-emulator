using Proyect_Base.app.DAO;
using Proyect_Base.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Collections
{
    class PublicAreaCollection
    {
        public static Dictionary<int, PublicArea> publicAreas = new Dictionary<int, PublicArea>();
        public static void init()
        {
            foreach(PublicArea publicArea in PublicAreaDAO.getPublicAreas())
            {
                publicAreas.Add(publicArea.id, publicArea);
            }
        }
        public static PublicArea getPublicAreaById(int id)
        {
            if (publicAreas.ContainsKey(id))
            {
                return publicAreas[id];
            }
            return null;
        }
    }
}
