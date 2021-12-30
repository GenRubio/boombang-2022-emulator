using Proyect_Base.app.Collections;
using Proyect_Base.app.Connection;
using Proyect_Base.app.Controllers;
using Proyect_Base.app.Handlers;
using Proyect_Base.app.Threads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.forms
{
    class AppInit
    {
        public static void init()
        {
            FlashSocket.Initialize();
            HandlerManager.Initialize();
            CollectionsController.initCollections();
            ThreadsController.initThreads();
        }
        public static void appStarted(DateTime now)
        {
            TimeSpan span = (TimeSpan)(DateTime.Now - now);
            App.Form.WriteLine("Servidor iniciado en " + Math.Round(span.TotalSeconds, 2) + " segundos.", "success");
            App.Form.WriteLine("________________________________________________________________________________", "success");
        }
    }
}
