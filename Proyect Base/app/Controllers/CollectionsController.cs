using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Collections
{
    class CollectionsController
    {
        public static void initCollections()
        {
            PublicAreaCollection.init();
            GameAreaCollection.init();
            ItemAreaCollection.init();
            ShopObjectCollection.init();
            SpecialAreaCollection.init();
            MiniGameAreaCollection.init();
        }
    }
}
