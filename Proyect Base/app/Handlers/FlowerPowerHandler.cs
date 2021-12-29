﻿using Proyect_Base.app.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Handlers
{
    class FlowerPowerHandler
    {
        public static void init()
        {
            HandlerManager.RegisterHandler(120149, new ProcessHandler(unknow), true);
            HandlerManager.RegisterHandler(120141, new ProcessHandler(unknow), true);
            HandlerManager.RegisterHandler(120134, new ProcessHandler(loadEvent), true);
        }
        private static void unknow(Session Session, ClientMessage Message)
        {
            return;
        }
        private static void loadEvent(Session Session, ClientMessage Message)
        {
            ServerMessage server = new ServerMessage(new byte[] { 120, 134 }, new object[] { 0 });
            Session.SendData(server);
        }
    }
}
