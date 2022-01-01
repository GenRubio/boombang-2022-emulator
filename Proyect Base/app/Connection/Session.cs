using Proyect_Base.app.Collections;
using Proyect_Base.app.Handlers;
using Proyect_Base.app.Helpers;
using Proyect_Base.app.Middlewares;
using Proyect_Base.app.Models;
using Proyect_Base.forms;
using Proyect_Base.logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Connection
{
    public class Session
    {
        public int ID;
        public string IP;
        public Socket Client;
        private byte[] buffer;
        private AsyncCallback CallBack;
        public HandlerManager Invoker;
        public User User;
        public Encryption Encryption;
        public double ping;
        public Session(int key, Socket client)
        {
            this.ID = key;
            this.Client = client;
            this.Encryption = new Encryption();
            this.buffer = new byte[2048];
            this.IP = client.RemoteEndPoint.ToString().Split(':')[0];
            this.CallBack = new AsyncCallback(RecivedInformation);
            this.Invoker = new HandlerManager(this);
            this.User = null;
            this.ping = 0;
            this.WaitForData();
        }
        private void WaitForData()
        {
            try
            {
                Client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, CallBack, Client);
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
        private void RecivedInformation(IAsyncResult Result)
        {
            try
            {
                if (Client == null) { return; }
                int Length = Client.EndReceive(Result);
                if (Length == 0 || Length > this.buffer.Length)
                {
                    closeConnection(); return;
                }
                char[] Chars = new char[Length];
                Encryption.Encoding.GetChars(buffer, 0, Length, Chars, 0);
                string Data = new String(Chars);
                if (!Policy(Data))
                {
                    Data = Encryption.Encoding.GetString(Encryption.Decrypt(Encryption.Encoding.GetBytes(Data)));
                    ProcessData(Data);
                }
                WaitForData();
            }
            catch (Exception ex)
            {
                Log.error(ex);
                closeConnection();
            }
        }
        private bool Policy(string Data)
        {
            if (Data.Contains("<policy-file-request/>"))
            {
                Client.Send(Encryption.Encoding.GetBytes("<?xml version=\"1.0\"?>\r\n<!DOCTYPE cross-domain-policy SYSTEM \"/xml/dtds/cross-domain-policy.dtd\">\r\n<cross-domain-policy>\r\n<allow-access-from domain=\"*\" to-ports=\"" + Config.FLASH_SOCKET_PORT + "\" />\r\n</cross-domain-policy>\0"));
                return true;
            }
            return false;
        }
        private void ProcessData(string Data)
        {
            try
            {
                if (Client == null) { return; }
                if (!Client.Connected) { return; }
                if (Data[0] != Convert.ToChar(177))
                {
                    closeConnection();
                }
                string[] Datas = Data.Split(Convert.ToChar(177));
                for (int i = 1; i < Datas.Length; i++)
                {
                    Invoker.Excecute(new ClientMessage(Convert.ToChar(177) + Datas[i]));
                }
            }
            catch (ArgumentNullException an)
            {
                Log.error(an);
            }
            catch (IndexOutOfRangeException io)
            {
                Log.error(io);
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
        public void SendData(ServerMessage server)
        {
            try
            {
                if (Client.Connected)
                {
                    Client.Send(Encryption.Encrypt(server.GetContent()));
                }
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
        public void closeConnection()
        {
            try
            {
                removeOnlineUser();
                removeUserFromArea();
                closeClient();
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
        private void removeOnlineUser()
        {
            if (UserMiddleware.userLogged(this))
            {
                SessionCollection.removeOnlineUser(User);
            }
        }
        private void removeUserFromArea()
        {
            if (UserMiddleware.userInArea(this))
            {
                this.User.Area.removeUserHandler(this);
            }
        }
        private void closeClient()
        {
            if (this.Client != null)
            {
                this.Client.Shutdown(SocketShutdown.Both);
                this.Client.Close();
            }
        }
    }
}
