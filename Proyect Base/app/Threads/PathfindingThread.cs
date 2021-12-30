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
        private static int timeMsNextMoviment = 680;
        public static void init()
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
                                    new Thread(() => checkUserOnItemArea(Session)).Start();
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
        private static void checkUserOnItemArea(Session Session)
        {
            if (Session.User.Area is PublicArea)
            {
                checkUserOnItemPublicArea(Session);
            }
            else if (Session.User.Area is GameArea)
            {
                checkUserOnItemGameArea(Session);
            }
        }
        private static void checkUserOnItemGameArea(Session Session)
        {
            GameArea gameArea = (GameArea)Session.User.Area;
            foreach (ItemArea itemArea in gameArea.items.Values.ToList())
            {
                if (itemArea.userOnItem(Session) && gameArea.removeItem(itemArea))
                {
                    Session.User.getItemAreaReward(Session, itemArea);
                    break;
                }
            }
        }
        private static void checkUserOnItemPublicArea(Session Session)
        {
            PublicArea publicArea = (PublicArea)Session.User.Area;
            foreach (ItemArea itemArea in publicArea.items.Values.ToList())
            {
                if (itemArea.userOnItem(Session) && publicArea.removeItem(itemArea))
                {
                    Session.User.getItemAreaReward(Session, itemArea);
                    break;
                }
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
            Session.User.Bloqueos.SetLock(Bloqueo.Caminando, TimeHelper.GetCurrentAndAdd(AddType.Milisegundos, timeMsNextMoviment));
            Session.User.Posicion = NewPoint;
            Session.User.LastPoint = NewPoint;
            Session.User.Movimientos.Movimientos.Remove(Session.User.Posicion);
        }
    }
}
