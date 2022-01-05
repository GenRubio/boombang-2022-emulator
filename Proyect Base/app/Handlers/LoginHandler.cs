using Proyect_Base.app.Collections;
using Proyect_Base.app.Connection;
using Proyect_Base.app.DAO;
using Proyect_Base.app.Helpers;
using Proyect_Base.app.Middlewares;
using Proyect_Base.app.Models;
using Proyect_Base.forms;
using Proyect_Base.logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proyect_Base.app.Handlers
{
    class LoginHandler
    {
        public static void init()
        {
            HandlerManager.RegisterHandler(120121, new ProcessHandler(login), true);
        }
        private static void login(Session Session, ClientMessage Message)
        {
            Thread.Sleep(new TimeSpan(0, 0, 0, 0, 500));
            try
            {
                if (UserMiddleware.userNotLogged(Session))
                {
                    string name = Message.Parameters[0, 0];
                    string password = Message.Parameters[1, 0];

                    User User = UserDAO.getUserByLogin(name, password);
                    if (User != null)
                    {
                        if (UserHelper.userOnline(User))
                        {
                            Session.SendData(new ServerMessage(new byte[] { 120, 121 }, new object[] { 2 }));
                        }
                        else
                        {
                            SessionCollection.addOnlineUser(User, Session);
                            new Thread(() => UserDAO.updateLastConnection(User)).Start();
                            User.initLoginHandler(Session);
                        }
                    }
                    else
                    {
                        Session.SendData(new ServerMessage(new byte[] { 120, 121 }, new object[] { 0 }));
                    }
                }
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }
        }
    }
}
