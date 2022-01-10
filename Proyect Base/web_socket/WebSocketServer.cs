using Proyect_Base.app.Collections;
using Proyect_Base.app.Connection;
using Proyect_Base.forms;
using Proyect_Base.logs;
using Proyect_Base.web_socket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Proyect_Base.app.socket
{
	class WebSocketServer
	{
        /**  Socket vercion 1.2 superfluid **/
        public static Socket WebSocket = null;
        private static readonly string serverIP = Config.WEB_SOCKET_HOST;
        private static readonly int serverPort = Config.WEB_SOCKET_PORT;
        public static void Initialize()
        {
            if (Config.WEB_SOCKET_ACTIVE)
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Bind(new IPEndPoint(IPAddress.Parse(serverIP), serverPort));
                socket.Listen(0);
                App.Form.WriteLine("WebSocket connection open (Port: " + serverPort + ")", "success");

                var client = socket.Accept();
                WebSocket = client;

                while (true)
                {
                    reciveData(client);
                }
            }
        }
        private static void reciveData(Socket client)
        {
            if (client.Connected)
            {
                try
                {
                    var buffer = new byte[255];
                    int rec = client.Receive(buffer, 0, buffer.Length, 0);
                    Array.Resize(ref buffer, rec);

                    WebMessage webMessage = new WebMessage(Encoding.UTF8.GetString(buffer));
                    Session Session = SessionCollection.getSessionByUID(webMessage.GetUID(), webMessage.GetIP());
                    if (Session != null)
                    {
                        WebSocketHandler webSocketHandler = new WebSocketHandler(Session);
                        webSocketHandler.Excecute(Session, webMessage);
                    }
                }
                catch (Exception ex)
                {
                    Log.error(ex);
                }
            }
        }
        public static void sendData(Socket client, string data)
        {
            if (Config.WEB_SOCKET_ACTIVE)
            {
                var buffer = Encoding.UTF8.GetBytes(data);
                client.Send(buffer, 0, buffer.Length, 0);
            }
        }
    }
}

