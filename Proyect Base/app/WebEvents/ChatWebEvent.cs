using Proyect_Base.app.Connection;
using Proyect_Base.forms;
using Proyect_Base.web_socket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.WebEvents
{
    class ChatWebEvent
    {
        public static void init()
        {
            WebSocketHandler.RegisterHandler("send-message", new ProcessHandler(sendMessageHandler), true);
        }
        private static void sendMessageHandler(Session Session, WebMessage Message)
        {
            try
            {
                string message = Message.Parameters[1];
                Session.User.Area.sendUserMessageHandler(Session, message);
            }
            catch(Exception ex)
            {
                App.Form.WriteLine(ex.Message);
            }
        }
    }
}
