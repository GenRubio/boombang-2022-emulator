using Proyect_Base.app.Connection;
using Proyect_Base.app.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    public class MiniGameCallUsers
    {
        public List<Session> Sessions { get; set; }
        public MiniGame miniGame { get; set; }
        public int waitSecondsToMoveUser { get; set; }
        public MiniGameCallUsers(List<Session> Sessions, MiniGame miniGame)
        {
            this.Sessions = Sessions;
            this.miniGame = miniGame;
            this.waitSecondsToMoveUser = 10;
            new Thread(() => callUsers()).Start();
        }
        //FUNCTIONS
        public void callUsers()
        {
            sendAlertCallToUsers();
            Thread.Sleep(new TimeSpan(0, 0, this.waitSecondsToMoveUser));
            registerUsersInMiniGameArea();
        }
        private void sendAlertCallToUsers()
        {
            foreach (Session Session in this.Sessions)
            {
                if (UserMiddleware.userLogged(Session) && Session.User.inMiniGame == false)
                {
                    Session.User.inMiniGame = true;
                    sendAlertHandler(Session);
                }
            }
        }
        private void registerUsersInMiniGameArea()
        {
            foreach (Session Session in this.Sessions)
            {
                if (UserMiddleware.userLogged(Session) && Session.User.inMiniGame == true)
                {
                    if (UserMiddleware.userInArea(Session))
                    {
                        Session.User.Area.userLeavingAreaWebHandler(Session);
                        Session.User.Area.removeUserByCompassHandler(Session);
                        initLoad(Session);
                        Session.User.Area.userEntringAreaWebHandler(Session);
                    }
                    else
                    {
                        initLoad(Session);
                        Session.User.Area.userEntringAreaWebHandler(Session);
                    }
                }
            }
        }
        private void initLoad(Session Session)
        {
            this.miniGame.addUser(Session);
            this.miniGame.loadUserHandler(Session);
            this.miniGame.loadAreaHandler(Session);
        }
        //MODEL SETTERS

        //MODEL GETTERS

        //HANDLERS
        public void sendAlertHandler(Session Session)
        {
            Session.SendData(new ServerMessage(new byte[] { 207 }));
        }
    }
}
