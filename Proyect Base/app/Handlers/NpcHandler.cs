using Proyect_Base.app.Connection;
using Proyect_Base.app.Middlewares;
using Proyect_Base.app.Models;
using Proyect_Base.logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Handlers
{
    class NpcHandler
    {
        public static void init()
        {
            HandlerManager.RegisterHandler(123120, new ProcessHandler(loadObjects), true);
        }
        private static void loadObjects(Session Session, ClientMessage Message)
        {
            try
            {
                if (UserMiddleware.userInArea(Session) && Session.User.Area is PublicArea publicArea)
                {
                    AreaNpc areaNpc = publicArea.getAreaNpcWithOutMoviments();
                    if (areaNpc != null)
                    {
                        areaNpc.loadNpcContentHandler(Session);
                    }
                }
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }
        }
    }
}
