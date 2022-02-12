using Proyect_Base.app.Events;
using Proyect_Base.app.WebEvents;
using Proyect_Base.logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.socket
{
    class WebSocketController
    {
        public static void initListeners()
        {
            try
            {
                TestWebEvent.init();
                ChatWebEvent.init();
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
    }
}
