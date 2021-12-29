using Proyect_Base.forms;
using Proyect_Base.logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Connection
{
    class FlashSocket
    {
        public static TcpListener Listener;
        public static void Initialize()
        {
            try
            {
                Listener = new TcpListener(IPAddress.Any, Config.FLASH_SOCKET_PORT);
                Listener.Start();
                WaitConnections();
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
        private static void WaitConnections()
        {
            try
            {
                Listener.BeginAcceptSocket(ProcessConnection, null);
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
        private static void ProcessConnection(IAsyncResult ar)
        {
            try
            {
                Socket socket = Listener.EndAcceptSocket(ar);
                if (socket != null)
                {
                    if (socket.Connected)
                    {
                        new Session(0, socket);
                    }
                }
                WaitConnections();
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
    }
}
