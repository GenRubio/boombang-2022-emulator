using Proyect_Base.app.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Handlers
{
    class FriendsHandler
    {
        public static void init()
        {
            HandlerManager.RegisterHandler(132120, new ProcessHandler(loadFriends), true);
            HandlerManager.RegisterHandler(132121, new ProcessHandler(loadMessages), true);
        }
        private static void loadFriends(Session Session, ClientMessage ClientPacket)
        {
            ServerMessage server = new ServerMessage(new byte[] { 132, 120 });
            server.AppendParameter(0);
            Session.SendData(server);
        }
        private static void loadMessages(Session Session, ClientMessage ClientPacket)
        {
            ServerMessage server = new ServerMessage(new byte[] { 132, 121 });
            server.AppendParameter(0);
            Session.SendData(server);
        }
    }
}
