using Proyect_Base.forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proyect_Base.app.socket
{
	class SocketServer
	{
        /**  Socket vercion 1.1 **/
        public static Socket WebSocket = null;
        private static bool ThreadReceivData = true;
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

                while (ThreadReceivData)
                {
                    reciveData(client);
                }
            }
        }

        public static void sendData(SocketData socketData)
        {
            try
            {
                var buffer = Encoding.UTF8.GetBytes(socketData.channel + "|" + socketData.parameters);
                WebSocket.Send(buffer, 0, buffer.Length, 0);
            }
            catch (Exception e)
            {
                App.Form.WriteLine(e.Message, "error");
            }
        }
        private static void reciveData(Socket client)
        {
            try
            {
                var buffer = new byte[255];
                int rec = client.Receive(buffer, 0, buffer.Length, 0);
                Array.Resize(ref buffer, rec);

                string idType = getIdType(Encoding.UTF8.GetString(buffer));
                string[] parameters = getParameters(Encoding.UTF8.GetString(buffer));

                if (parameters.Length > 0)
                {
                    string client_uid = parameters[0];

                    //if (getSession(client_uid) != null)
                    //{
                    //    CallPackage.init(getSession(client_uid), idType, parameters);
                    //}
                    //else
                    //{
                    //    App.Form.WriteLine("No se ha podido obtener Session.", "error");
                    //}
                }
                else
                {
                    //App.Form.WriteLine("No se ha podido obtener parametros.", "error");
                }
            }
            catch
            {
                //Emulator.Form.WriteLine("Node is closed.", "error");
                ThreadReceivData = false;
                return;
            }

        }
        //private static SessionInstance getSession(string token_uid)
        //{
        //    foreach (SessionInstance session in UserManager.UsuariosOnline.Values.ToList())
        //    {
        //        if (session.User.token_uid == token_uid)
        //        {
        //            return session;
        //        }
        //    }
        //    return null;
        //}
        private static string getIdType(string buffer)
        {
            string[] data = buffer.Split('|');
            return data[0];
        }
        private static string[] getParameters(string buffer)
        {
            string[] data = buffer.Split('|');
            string[] parameters = data[1].Split(',');
            return parameters;
        }
    }
}

