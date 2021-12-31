using Proyect_Base.app.Connection;
using Proyect_Base.app.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Handlers
{
    class BackpackHandler
    {
        public static void init()
        {
            HandlerManager.RegisterHandler(189180, new ProcessHandler(loadBackpack), true);
        }
        private static void loadBackpack(Session Session, ClientMessage Message)
        {
            if (UserMiddleware.userLogged(Session))
            {
                Session.User.loadBackpackObjectsHandler(Session);
            }
        }
    }
}
