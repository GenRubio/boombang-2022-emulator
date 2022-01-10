using Proyect_Base.app.Connection;
using Proyect_Base.app.socket;
using Proyect_Base.forms;
using Proyect_Base.logs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.web_socket
{
    public delegate void ProcessHandler(Session Session, WebMessage webMessage);
    public class WebSocketHandler
    {
        private Session Session;
        public static ConcurrentDictionary<string, ProcessHandler> webHandlers = new ConcurrentDictionary<string, ProcessHandler>();
        public static void Initialize()
        {
            WebSocketController.initListeners();
            App.Form.WriteLine("Se han cargado " + webHandlers.Count + " Web handlers.");
        }
        public WebSocketHandler(Session Session)
        {
            this.Session = Session;
        }
        public static void RegisterHandler(string key, ProcessHandler Process, bool active = true)
        {
            try
            {
                if ((active) && (Process != null) && (!webHandlers.ContainsKey(key)))
                {
                    webHandlers.TryAdd(key, Process);
                }
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
        public void Excecute(Session Session, WebMessage Message)
        {
            try
            {
                if (Message != null)
                {
                    if (webHandlers.ContainsKey(Message.GetKey()))
                    {
                        webHandlers[Message.GetKey()](Session, Message);
                        return;
                    }
                    if (Config.APP_SEND_NOT_PROGRAMMED_PACKETS)
                    {
                        App.Form.WriteLine("Falta: " + Message.GetKey() + " -> " + Message.GetData(), "warning");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
    }
}
