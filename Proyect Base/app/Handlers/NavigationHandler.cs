﻿using Proyect_Base.app.Collections;
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
                else if(Session.User.Area is GameArea)
                {
                    GameArea gameArea = (GameArea)Session.User.Area;
                    gameArea.loadAreaObjectsHandler(Session);
                    gameArea.loadAreaParametersHandler(Session);
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
                        loadArea(id, Session);
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
        private static void loadArea(int id, Session Session)
        {
            if (!loadPublicArea(id, Session) && !loadGameArea(id, Session))
            {
                Session.SendData(new ServerMessage(new byte[] { 128, 120 }, new object[] { -1 }));
            }
        }
        private static bool loadGameArea(int id, Session Session)
        {
            GameArea gameArea = GameAreaCollection.getGameAreaById(id);
            if (gameArea != null && gameArea.users.Count < gameArea.max_visitors)
            {
                if (UserMiddleware.userInArea(Session))
                {
                    Session.User.Area.removeUserByCompassHandler(Session);
                    initLoadGameArea(Session, gameArea);
                }
                else
                {
                    initLoadGameArea(Session, gameArea);
                }
                return true;
            }
            return false;
        }
        private static void initLoadGameArea(Session Session, GameArea gameArea)
        {
            gameArea.addUser(Session);
            gameArea.loadUserHandler(Session);
            gameArea.loadAreaHandler(Session);
        }
        private static bool loadPublicArea(int id, Session Session)
        {
            PublicArea publicArea = PublicAreaCollection.getPublicAreaById(id);
            if (publicArea != null && publicArea.users.Count < publicArea.max_visitors)
            {
                if (UserMiddleware.userInArea(Session))
                {
                    Session.User.Area.removeUserByCompassHandler(Session);
                    initLoadPublicArea(Session, publicArea);
                }
                else
                {
                    initLoadPublicArea(Session, publicArea);
                }
                return true;
            }
            return false;
        }
        private static void initLoadPublicArea(Session Session, PublicArea publicArea)
        {
            publicArea.addUser(Session);
            publicArea.loadUserHandler(Session);
            publicArea.loadAreaHandler(Session);
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
                        loadGameAreas(Session);
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
        private static void loadGameAreas(Session Session)
        {
            ServerMessage server = new ServerMessage(new byte[] { 154, 32 });
            server.AppendParameter(3);
            foreach (GameArea gameArea in GameAreaCollection.gameAreas.Values.ToList())
            {
                server.AppendParameter(new object[] { 
                    gameArea.es_category, 
                    gameArea.es_category, 
                    gameArea.id, 
                    gameArea.name, 
                    gameArea.users.Count, 0, 0, 0, -1, 0 
                });
            }
            Session.SendData(server);
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
