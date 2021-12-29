using Proyect_Base.app.Collections;
using Proyect_Base.app.Helpers;
using Proyect_Base.app.Pathfinding;
using Proyect_Base.logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Proyect_Base.app.Connection;
using Proyect_Base.app.Models;
using Proyect_Base.forms;
using Proyect_Base.app.Middlewares;

namespace Proyect_Base.app.Threads
{
    public class PathfindingThread
    {
        public static void Initialize()
        {
            new Thread(Pathfinding).Start();
        }
        private static void Pathfinding()
        {
            while (true)
            {
                try
                {
                    foreach (Session Session in SessionCollection.onlineUsers.Values.ToList())
                    {
                        if (UserMiddleware.userInArea(Session) && checkOldMoviments(Session))
                        {
                            Posicion NewPoint = Session.User.Movimientos.SiguienteMovimiento();
                            if (validateNextMoviment(Session, NewPoint))
                            {
                                if (Session.User.Area.MapaBytes.IsWalkable(NewPoint.x, NewPoint.y))
                                {
                                    setNextPositionUser(Session, NewPoint);
                                    Session.User.Area.userWalkHandler(Session);
                                }
                                else
                                {
                                    Session.User.Movimientos.Movimientos.Clear();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.error(ex);
                }
                Thread.Sleep(1);
            }
        }
        private static bool validateNextMoviment(Session Session, Posicion NewPoint)
        {
            if (Session.User.Movimientos.MovementIsVerifield(NewPoint)
                && !Session.User.Bloqueos.IsBlock(Bloqueo.Block)
                && !Session.User.Bloqueos.IsBlock(Bloqueo.Caminando))
            {
                return true;
            }
            return false;
        }
        private static bool checkOldMoviments(Session Session)
        {
            if (Session.User.Movimientos != null && Session.User.Movimientos.Movimientos.Count != 0)
            {
                return true;
            }
            return false;
        }
        private static void setNextPositionUser(Session Session, Posicion NewPoint)
        {
            Session.User.Bloqueos.SetLock(Bloqueo.Caminando, TimeHelper.GetCurrentAndAdd(AddType.Milisegundos, 680));
            Session.User.Posicion = NewPoint;
            Session.User.LastPoint = NewPoint;
            Session.User.Movimientos.Movimientos.Remove(Session.User.Posicion);
        }
    }
}
