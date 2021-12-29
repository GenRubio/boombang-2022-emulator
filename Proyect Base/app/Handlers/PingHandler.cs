using Proyect_Base.app.Connection;
using Proyect_Base.app.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Handlers
{
    class PingHandler
    {
        public static void init()
        {
            HandlerManager.RegisterHandler(163, new ProcessHandler(ping), true);
        }
        private static void ping(Session Session, ClientMessage Message)
        {
            if (Session.ping == 0)
            {
                Session.ping = TimeHelper.GetCurrentAndAdd(AddType.Segundos, 9);
            }
            else
            {
                if (TimeHelper.GetDifference(Session.ping) >= 1)
                {
                    Session.SendData(new ServerMessage(new byte[] { 128, 124 }));
                    Session.closeConnection();
                    return;
                }
            }
            Session.ping = TimeHelper.GetCurrentAndAdd(AddType.Segundos, 9);
            Session.SendData(new ServerMessage(new byte[] { 163 }, new object[] { 10 }));
        }
    }
}
