using Proyect_Base.app.Connection;
using Proyect_Base.app.Helpers;
using Proyect_Base.forms;
using Proyect_Base.web_socket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Events
{
    class TestWebEvent
    {
        public static void init()
        {
            WebSocketHandler.RegisterHandler("test-message", new ProcessHandler(test), true);
        }
        private static void test(Session Session, WebMessage Message)
        {
            App.Form.WriteLine(Message.Parameters[0]);
            App.Form.WriteLine(Message.Parameters[1]);
            App.Form.WriteLine(UserHelper.makeSessionUID());
        }
    }
}
