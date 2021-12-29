using Proyect_Base.app.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Handlers
{
    class HouseHandler
    {
        public static void init()
        {
            HandlerManager.RegisterHandler(120143, new ProcessHandler(loadHouse), true);
        }
        private static void loadHouse(Session Session, ClientMessage Message)
        {
            ServerMessage server = new ServerMessage(new byte[] { 120, 143 });
            server.AppendParameter(-1);
            Session.SendData(server);
        }
    }
}
