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
    class BackpackHandler
    {
        public static void init()
        {
            HandlerManager.RegisterHandler(189180, new ProcessHandler(loadBackpack), true);
            HandlerManager.RegisterHandler(189181, new ProcessHandler(loadObject), true);
        }
        private static void loadObject(Session Session, ClientMessage Message)
        {
            try
            {
                int objectId = int.Parse(Message.Parameters[0, 0]);
                if (UserMiddleware.userLogged(Session))
                {
                    ServerMessage server = new ServerMessage(new byte[] { 189, 181 });
                    List<UserObject> userObjects = Session.User.getObjectsByObjectId(objectId);
                    foreach(UserObject userObject in userObjects)
                    {
                        userObject.loadBackpackObjectParametersHandler(server);
                    }
                    Session.SendData(server);
                }
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }
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
