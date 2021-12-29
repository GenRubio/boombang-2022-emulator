using Proyect_Base.app.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Handlers
{
    class ShopHandler
    {
        public static void init()
        {
            HandlerManager.RegisterHandler(189133, new ProcessHandler(loadShop), true);
        }
        private static void loadShop(Session Session, ClientMessage Message)
        {
            ServerMessage server = new ServerMessage(new byte[] { 189, 133 });
            server.AppendParameter(-1);
            Session.SendData(server);
        }
    }
}
