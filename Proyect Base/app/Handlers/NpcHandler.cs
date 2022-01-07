using Proyect_Base.app.Connection;
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
            //HandlerManager.RegisterHandler(123120, new ProcessHandler(loadObjects), true);
        }
        private static void loadObjects(Session Session, ClientMessage Message)
        {
            ServerMessage server = new ServerMessage(new byte[] { 123, 120 });
            server.AppendParameter(1);

            server.AppendParameter(1);
            server.AppendParameter(0);
            server.AppendParameter(100);
            server.AppendParameter(100);
            server.AppendParameter(1);
            Session.SendData(server);
        }
    }
}
