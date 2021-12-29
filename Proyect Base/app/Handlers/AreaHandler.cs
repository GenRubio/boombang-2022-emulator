using Proyect_Base.app.Connection;
using Proyect_Base.app.Helpers;
using Proyect_Base.app.Middlewares;
using Proyect_Base.app.Models;
using Proyect_Base.app.Pathfinding;
using Proyect_Base.forms;
using Proyect_Base.logs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Handlers
{
    class AreaHandler
    {
        public static void init()
        {
            HandlerManager.RegisterHandler(135, new ProcessHandler(look), true);
            HandlerManager.RegisterHandler(182, new ProcessHandler(walk), true);
            HandlerManager.RegisterHandler(128124, new ProcessHandler(getOutArea), true);
            HandlerManager.RegisterHandler(214, new ProcessHandler(gameRankingPanel));
        }
        private static void gameRankingPanel(Session Session, ClientMessage Message)
        {
            try
            {
                int gameId = int.Parse(Message.Parameters[0, 0]);
                if (UserMiddleware.userInArea(Session) && Session.User.Area is GameArea)
                {
                    GameArea gameArea = (GameArea)Session.User.Area;
                    gameArea.loadRankingPanelHandler(Session, gameId);
                }
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }
        }
        private static void getOutArea(Session Session, ClientMessage Message)
        {
            if (UserMiddleware.userInArea(Session))
            {
                Session.User.Area.removeUserByCompassHandler(Session);
            }
        }
        private static void look(Session Session, ClientMessage Message)
        {
            if (UserMiddleware.userInArea(Session))
            {
                if (Session.User.Bloqueos.IsBlock(Bloqueo.Mirada)) return;
                if (Session.User.Ultra_Bloqueos.IsBlock(UltraType.Mirada)) return;
                int Z = int.Parse(Message.Parameters[1, 0]);
                if (Z >= 1 && Z <= 8)
                {
                    Session.User.Bloqueos.SetLock(Bloqueo.Mirada, TimeHelper.GetCurrentAndAdd(AddType.Milisegundos, 25));
                    Session.User.Ultra_Bloqueos.Verificar(UltraType.Mirada, Z);
                    Session.User.Posicion.z = Z;
                    Session.User.Area.userLookDirectionHandler(Session);
                }
            }
        }
        private static void walk(Session Session, ClientMessage Message)
        {
            if (UserMiddleware.userInArea(Session))
            {
                if (Session.User.Area.category != 2 && Session.User.Bloqueos.IsBlock(Bloqueo.Block)) {
                    return;
                }
                Session.User.Movimientos = new Trayectoria(Session);
                List<Posicion> ListPositions = new List<Posicion>();
                string Steps = Message.Parameters[1, 0];
                while (Steps != "")
                {
                    int x = int.Parse(Steps.Substring(0, 2));
                    int y = int.Parse(Steps.Substring(2, 2));
                    int z = int.Parse(Steps.Substring(4, 1));
                    ListPositions.Add(new Posicion(x, y, z));
                    Steps = Steps.Substring(5);
                }
                if (Session.User.Area.category != 2)
                {
                    ListPositions.Reverse();
                    Session.User.Movimientos.EndLocation = new Point(ListPositions[0].x, ListPositions[0].y);
                    Session.User.Movimientos.IniciarCaminado();
                    return;
                }
                Session.User.Movimientos.EndLocation = new Point(ListPositions[ListPositions.Count - 1].x, ListPositions[ListPositions.Count - 1].y);
                Session.User.Movimientos.IniciarCaminado();
            }
        }
    }
}
