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
            HandlerManager.RegisterHandler(189146, new ProcessHandler(changeColorsArea), true);
            HandlerManager.RegisterHandler(189136, new ProcessHandler(putObject), true);
            HandlerManager.RegisterHandler(189145, new ProcessHandler(moveObject), true);
            HandlerManager.RegisterHandler(189140, new ProcessHandler(removeObject), true);
            HandlerManager.RegisterHandler(189143, new ProcessHandler(changeRotationObject), true);
            HandlerManager.RegisterHandler(189142, new ProcessHandler(changeColorsObject), true);
        }
        private static void changeColorsArea(Session Session, ClientMessage Message)
        {
            try
            {
                string colors = Message.Parameters[0, 0];
                string colorsRGB = Message.Parameters[1, 0];

                if (validateUserCreator(Session))
                {
                    IslandArea islandArea = (IslandArea)Session.User.Area;
                    islandArea.color_1 = colors;
                    islandArea.color_2 = colorsRGB;
                    islandArea.changeColorsAreaHandler();
                    IslandAreaDAO.updateColors(islandArea);
                }
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }
        }
        private static void changeColorsObject(Session Session, ClientMessage Message)
        {
            try
            {
                int id = Convert.ToInt32(Message.Parameters[0, 0]);
                string color = Message.Parameters[1, 0];
                string colorRGB = Message.Parameters[2, 0];
                int shopObjectId = Convert.ToInt32(Message.Parameters[3, 0]);

                if (validateUserCreator(Session))
                {
                    UserObject userObject = getUserObject(Session, id, true);
                    if (userObject != null)
                    {
                        IslandArea islandArea = (IslandArea)Session.User.Area;
                        updateUserObjectAttributes(userObject,
                            Session.User.Area.id,
                            userObject.Posicion.x,
                            userObject.Posicion.y,
                            Convert.ToInt32(userObject.height),
                            userObject.ocupe,
                            userObject.rotation,
                            color,
                            colorRGB);
                        islandArea.changeObjectColorsHandler(userObject);
                        UserObjectDAO.updateUserObjectColorsInArea(userObject);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
        private static void changeRotationObject(Session Session, ClientMessage Message)
        {
            try
            {
                int id = Convert.ToInt32(Message.Parameters[0, 0]);
                int rotation = Convert.ToInt32(Message.Parameters[6, 0]);

                if (validateUserCreator(Session))
                {
                    UserObject userObject = getUserObject(Session, id, true);
                    if (userObject != null)
                    {
                        IslandArea islandArea = (IslandArea)Session.User.Area;
                        updateUserObjectAttributes(userObject, 
                            Session.User.Area.id, 
                            userObject.Posicion.x, 
                            userObject.Posicion.y, 
                            Convert.ToInt32(userObject.height), 
                            userObject.ocupe, 
                            rotation,
                            userObject.Color_1, 
                            userObject.Color_2);
                        islandArea.rotateObjectHandler(userObject);
                        UserObjectDAO.rotateUserObjectInArea(userObject);
                    }
                }
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }
        }
        private static void removeObject(Session Session, ClientMessage Message)
        {
            try
            {
                int id = Convert.ToInt32(Message.Parameters[0, 0]);

                if (validateUserCreator(Session))
                {
                    UserObject userObject = getUserObject(Session, id, true);
                    if (userObject != null)
                    {
                        IslandArea islandArea = (IslandArea)Session.User.Area;
                        updateUserObjectAttributes(userObject, Session.User.Area.id, 0, 0, 0, "", userObject.rotation,
                            userObject.Color_1, userObject.Color_2);
                        islandArea.removeObjectHandler(userObject);
                        Session.User.addObjectToBackpackHandler(Session, userObject);
                        UserObjectDAO.putOrRemoveUserObjectFromArea(Session.User, userObject);
                    }
                }
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }
        }
        private static void moveObject(Session Session, ClientMessage Message)
        {
            try
            {
                int id = int.Parse(Message.Parameters[0, 0]);
                int x = int.Parse(Message.Parameters[1, 0]);
                int y = int.Parse(Message.Parameters[2, 0]);
                int height = int.Parse(Message.Parameters[3, 0]);
                string coordinates = string.Empty;

                if (validateUserCreator(Session))
                {
                    putObjectArea(Session, id, x, y, coordinates, height, true);
                }
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }
        }
        private static void putObject(Session Session, ClientMessage Message)
        {
            try
            {
                int id = Convert.ToInt32(Message.Parameters[0, 0]);
                int x = Convert.ToInt32(Message.Parameters[1, 0]);
                int y = Convert.ToInt32(Message.Parameters[2, 0]);
                int height = Convert.ToInt32(Message.Parameters[3, 0]);
                string coordinates = string.Empty;

                if (validateUserCreator(Session))
                {
                    putObjectArea(Session, id, x, y, coordinates, height);
                }
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }
        }
        private static void putObjectArea(Session Session, 
            int id, int x, int y, string coordinates, int height, bool objectArea = false)
        {
            UserObject userObject = getUserObject(Session, id, objectArea);
            if (userObject != null)
            {
                ShopObject shopObject = ShopObjectCollection.getShopObjectById(userObject.ObjetoID);
                if (shopObject != null)
                {
                    Point position = getPosition(Session, x, y);
                    string shopObjectCoordinates = shopObject.something_1;
                    if (shopObjectCoordinates != string.Empty)
                    {
                        coordinates = Session.User.Area.MapaBytes.GetCoordinatesTakes(position, shopObjectCoordinates);
                    }
                    if (!objectOnUser(Session, position, coordinates))
                    {
                        IslandArea islandArea = (IslandArea)Session.User.Area;
                        updateUserObjectAttributes(userObject, islandArea.id, x, y, height, coordinates, 
                            userObject.rotation, userObject.Color_1, userObject.Color_2);
                        if (objectArea)
                        {
                            islandArea.moveObjectHandler(userObject);
                        }
                        else
                        {
                            Session.User.removeObjectBackpackHandler(Session, userObject);
                            islandArea.putObjectHandler(Session, userObject);
                            islandArea.addObject(userObject);
                        }
                        UserObjectDAO.putOrRemoveUserObjectFromArea(Session.User, userObject);
                    }
                }
            }
        }
        private static UserObject getUserObject(Session Session, int id, bool objectArea)
        {
            if (objectArea)
            {
                IslandArea islandArea = (IslandArea)Session.User.Area;
                return islandArea.getObjectById(id);
            }
            else
            {
                return Session.User.getObjectById(id);
            }
        }
        private static void updateUserObjectAttributes(UserObject userObject, 
            int areaId, int x, int y, int height, string coordinates, int rotation, string color, string colorRGB)
        {
            userObject.ocupe = coordinates;
            userObject.ZonaID = areaId;
            userObject.Posicion.x = x;
            userObject.Posicion.y = y;
            userObject.height = height.ToString();
            userObject.rotation = rotation;
            userObject.Color_1 = color;
            userObject.Color_2 = colorRGB;
        }
        private static bool objectOnUser(Session Session, Point position, string coordinates)
        {
            if (coordinates != string.Empty)
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
            Point point = new Point(x, y);
            Point position = Session.User.Area.MapaBytes.GetCoordinates(point);
            return position;
        }
        private static bool validateUserCreator(Session Session)
        {
            if(UserMiddleware.userInArea(Session)
                && Session.User.Area is IslandArea islandArea
                && islandArea.userCreatorId == Session.User.id)
            {
                return true;
            }
            return false;
        }
    }
}
