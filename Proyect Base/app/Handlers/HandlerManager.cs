using Proyect_Base.app.Connection;
using Proyect_Base.forms;
using Proyect_Base.logs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Handlers
{
    public delegate void ProcessHandler(Session Client, ClientMessage Message);
    public class HandlerManager
    {
        private Session Session;
        public static ConcurrentDictionary<int, ProcessHandler> HandlersRegistrados = new ConcurrentDictionary<int, ProcessHandler>();
        public static void Initialize()
        {
            HandlerController.initListeners();
            App.Form.WriteLine("Se han cargado " + HandlersRegistrados.Count + " handlers.");
        }
        public HandlerManager(Session Session)
        {
            this.Session = Session;
        }
        public static void RegisterHandler(int HandlerInteger, ProcessHandler Process, bool Activar = true)
        {
            try
            {
                if ((Activar) && (Process != null) && (!HandlersRegistrados.ContainsKey(HandlerInteger)))
                {
                    HandlersRegistrados.TryAdd(HandlerInteger, Process);
                }
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
        public void Excecute(ClientMessage Message)
        {
            try
            {
                if (Message != null)
                {
                    if (HandlersRegistrados.ContainsKey(Message.GetInteger()))
                    {
                        HandlersRegistrados[Message.GetInteger()](Session, Message);
                        return;
                    }
                    App.Form.WriteLine("Falta: " + Message.GetInteger() + " -> " + Message.GetData());
                }
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
    }
}
