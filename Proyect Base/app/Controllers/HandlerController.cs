using Proyect_Base.logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Handlers
{
    class HandlerController
    {
        public static void initListeners()
        {
            try
            {
                PingHandler.init();
                LoginHandler.init();
                FlowerPowerHandler.init();
                FriendsHandler.init();
                ShopHandler.init();
                BackpackHandler.init();
                HouseHandler.init();
                NavigationHandler.init();
                AreaHandler.init();
                IslandHandler.init();
                IslandAreaHandler.init();
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
    }
}
