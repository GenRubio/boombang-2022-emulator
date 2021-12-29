using Proyect_Base.app.Connection;
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
            ServerMessage server = new ServerMessage(new byte[] { 189, 180 });
            server.AppendParameter(-1);
            Session.SendData(server);
        }
    }
}
