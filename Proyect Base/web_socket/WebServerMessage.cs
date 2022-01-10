using Proyect_Base.app.Connection;
using Proyect_Base.app.socket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.web_socket
{
    public class WebServerMessage
    {
        private string channel { get; set; }
        public List<string> Parameters { get; set; }
        public WebServerMessage(string channel)
        {
            this.channel = channel;
            this.Parameters = new List<string>();
        }
        public void addParameter(string parameter)
        {
            this.Parameters.Add(parameter);
        }
        public void SendToWeb(Session Session)
        {
            string data = channel + "³²";
            data += Session.UID + "³";
            foreach (string parameter in this.Parameters)
            {
                data += parameter + "³";
            }
            data = data.Remove(data.Length - 1, 1);
            WebSocketServer.sendData(WebSocketServer.WebSocket, data);
        }
    }
}
