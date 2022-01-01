using Proyect_Base.app.Collections;
using Proyect_Base.app.Connection;
using Proyect_Base.app.DAO;
using Proyect_Base.app.Middlewares;
using Proyect_Base.app.Models;
using Proyect_Base.logs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Handlers
{
    class IslandAreaHandler
    {
        public static void init()
        {
            HandlerManager.RegisterHandler(189136, new ProcessHandler(putObject), true);
        }
        private static void putObject(Session Session, ClientMessage Message)
        {
            try
            {
                int id = Convert.ToInt32(Message.Parameters[0, 0]);
                int x = Convert.ToInt32(Message.Parameters[1, 0]);
                int y = Convert.ToInt32(Message.Parameters[2, 0]);
                int height = Convert.ToInt32(Message.Parameters[3, 0]);
                string espacioOcupado = string.Empty;

                if (UserMiddleware.userInArea(Session) && Session.User.Area is IslandArea)
                {
                    if (validateUserCreator(Session))
                    {
                        UserObject userObject = Session.User.getObjectById(id);
                        if (userObject != null)
                        {
                            ShopObject shopObject = ShopObjectCollection.getShopObjectById(userObject.ObjetoID);
                            if (shopObject != null)
                            {
                                Point position = getPosition(Session, x, y);
                                string cordenadas_objeto = shopObject.something_1;
                                if (cordenadas_objeto != "")
                                {
                                    espacioOcupado = Session.User.Area.MapaBytes.GetCoordinatesTakes(position, cordenadas_objeto);
                                }
                                if (!objectOnUser(Session, position, espacioOcupado))
                                {
                                    updateUserObjectAttributes(userObject, Session.User, x, y, height, espacioOcupado);
                                    IslandArea islandArea = (IslandArea)Session.User.Area;
                                    Session.User.removeObjectBackpackHandler(Session, userObject);
                                    islandArea.putObjectHandler(Session, userObject);
                                    UserObjectDAO.putUserObjectToArea(Session.User, userObject);
                                    islandArea.addObject(userObject);
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }
        }
        private static void updateUserObjectAttributes(UserObject userObject, User User, int x, int y, int height, string espacioOcupado)
        {
            userObject.ocupe = espacioOcupado;
            userObject.ZonaID = User.Area.id;
            userObject.Posicion.x = x;
            userObject.Posicion.y = y;
            userObject.height = height.ToString();
        }
        private static bool objectOnUser(Session Session, Point position, string espacioOcupado)
        {
            if (espacioOcupado != string.Empty)
            {
                Session OnPosition = Session.User.Area.getSession(position.X, position.Y);
                if (OnPosition != null)
                {
                    Session.SendData(new ServerMessage(new byte[] { 189, 140 }, new object[] { 0 }));
                    return true;
                }
            }
            return false;
        }
        private static Point getPosition(Session Session, int x, int y)
        {
            Point P = new Point(x, y);
            Point Position = Session.User.Area.MapaBytes.GetCoordinates(P);
            return Position;
        }
        private static bool validateUserCreator(Session Session)
        {
            IslandArea islandArea = (IslandArea)Session.User.Area;
            if (islandArea.userCreatorId == Session.User.id)
            {
                return true;
            }
            return false;
        }
    }
}
