using Proyect_Base.app.Connection;
using Proyect_Base.app.DAO;
using Proyect_Base.app.Middlewares;
using Proyect_Base.app.Models;
using Proyect_Base.logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Handlers
{
    class IslandHandler
    {
        public static void init()
        {
            HandlerManager.RegisterHandler(189120, new ProcessHandler(makeIsland), true);
            HandlerManager.RegisterHandler(189124, new ProcessHandler(loadIsland), true);
            HandlerManager.RegisterHandler(189149, new ProcessHandler(deleteIsland), true);
        }
        private static void deleteIsland(Session Session, ClientMessage Message)
        {
            try
            {
                int id = int.Parse(Message.Parameters[0, 0]);

                if (UserMiddleware.userOutOfArea(Session))
                {
                    if (Session.User.removeIsland(id))
                    {
                        IslandDAO.deleteIslandById(id);
                    }
                }
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }
        }
        private static void loadIsland(Session Session, ClientMessage Message)
        {
            try
            {
                int id = int.Parse(Message.Parameters[0, 0]);

                if (UserMiddleware.userOutOfArea(Session))
                {
                    Island island = IslandDAO.getIslandById(id);
                    if (island != null)
                    {
                        island.loadIslandHandler(Session);
                    }
                }
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }
        }
        private static void makeIsland(Session Session, ClientMessage Message)
        {
            try
            {
                string name = Message.Parameters[0, 0];
                int model = int.Parse(Message.Parameters[1, 0]);

                ServerMessage server = new ServerMessage(new byte[] { 189, 120 });
                if (UserMiddleware.userOutOfArea(Session) && Session.User.islands.Count < 25)
                {
                    Island island = IslandDAO.getIslandByName(name);
                    if (island == null)
                    {
                        island = IslandDAO.makeIsland(name, model, Session.User);
                        if (island != null)
                        {
                            Session.User.addIsland(island);
                            server.AppendParameter(island.id);
                        }
                        else
                        {
                            server.AppendParameter(0);
                        }
                    }
                    else
                    {
                        server.AppendParameter(0);
                    }
                }
                else
                {
                    server.AppendParameter(0);
                }
                Session.SendData(server);
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }
        }
    }
}
