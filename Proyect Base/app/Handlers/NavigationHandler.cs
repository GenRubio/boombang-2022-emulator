using Proyect_Base.app.Collections;
using Proyect_Base.app.Connection;
using Proyect_Base.app.DAO;
using Proyect_Base.app.Middlewares;
using Proyect_Base.app.Models;
using Proyect_Base.forms;
using Proyect_Base.logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Handlers
{
    class NavigationHandler
    {
        public static void init()
        {
            HandlerManager.RegisterHandler(15432, new ProcessHandler(loadNavBar), true);
            HandlerManager.RegisterHandler(128125, new ProcessHandler(goToArea), true);
            HandlerManager.RegisterHandler(128121, new ProcessHandler(loadArea), true);
        }
        private static void loadArea(Session Session, ClientMessage Message)
        {
            if (UserMiddleware.userInArea(Session)){
                if (Session.User.Area is PublicArea)
                {
                    PublicArea publicArea = (PublicArea)Session.User.Area;
                    publicArea.loadAreaObjectsHandler(Session);
                    publicArea.loadAreaParametersHandler(Session);
                }
            }
        }
        private static void goToArea(Session Session, ClientMessage Message)
        {
            try
            {
                int id = int.Parse(Message.Parameters[1, 0]);
                int es_category = int.Parse(Message.Parameters[0, 0]);

                switch (Message.GetInteger())
                {
                    case 128125:
                        loadPublicArea(id, Session);
                        break;
                    case 128120:
                        loadPrivateArea(id, Session);
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
        private static void loadPublicArea(int id, Session Session)
        {
            PublicArea publicArea = PublicAreaCollection.getPublicAreaById(id);
            if (publicArea != null && publicArea.users.Count < publicArea.max_visitors)
            {
                if (UserMiddleware.userInArea(Session))
                {
                    Session.User.Area.removeUser(Session);
                }
                else
                {
                    publicArea.addUser(Session);
                    publicArea.loadUserHandler(Session);
                    publicArea.loadAreaHandler(Session);
                }
            }
            else
            {
                Session.SendData(new ServerMessage(new byte[] { 128, 120 }, new object[] { -1 }));
            }
        }
        private static void loadPrivateArea(int id, Session Session)
        {

        }
        private static void loadNavBar(Session Session, ClientMessage Message)
        {
            try
            {
                int category = int.Parse(Message.Parameters[0, 0]);
                switch (category)
                {
                    case 1:
                        loadPublicAreas(Session);
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                }
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }
        }
        private static void loadPublicAreas(Session Session)
        {
            ServerMessage server = new ServerMessage(new byte[] { 154, 32 });
            server.AppendParameter(1);
            foreach (PublicArea publicArea in PublicAreaCollection.publicAreas.Values.ToList())
            {
                server.AppendParameter(new object[] {
                    publicArea.es_category,
                    publicArea.es_category,
                    publicArea.id,
                    publicArea.name,
                    publicArea.users.Count, 0, 0, 0, -1, 0
                });
            }
            Session.SendData(server);
        }
    }
}
