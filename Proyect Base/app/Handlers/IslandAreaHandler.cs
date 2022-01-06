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
            HandlerManager.RegisterHandler(189147, new ProcessHandler(removeUserArea), true);
            HandlerManager.RegisterHandler(189146, new ProcessHandler(changeColorsArea), true);
            HandlerManager.RegisterHandler(189136, new ProcessHandler(putObject), true);
            HandlerManager.RegisterHandler(189145, new ProcessHandler(moveObject), true);
            HandlerManager.RegisterHandler(189140, new ProcessHandler(removeObject), true);
            HandlerManager.RegisterHandler(189143, new ProcessHandler(changeRotationObject), true);
            HandlerManager.RegisterHandler(189142, new ProcessHandler(changeColorsObject), true);
            HandlerManager.RegisterHandler(189144, new ProcessHandler(changeSizeObject), true);
        }
        private static void removeUserArea(Session Session, ClientMessage Message)
        {
            try
            {
                int id = int.Parse(Message.Parameters[0, 0]);

                if (UserMiddleware.isIslandOwner(Session))
                {
                    IslandArea islandArea = (IslandArea)Session.User.Area;
                    Session sessionUser = islandArea.getSession(id);
                    if(sessionUser != null)
                    {
                        islandArea.removeUserHandler(sessionUser);
                    }
                }
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }
        }
        private static void changeColorsArea(Session Session, ClientMessage Message)
        {
            try
            {
                string colors = Message.Parameters[0, 0];
                string colorsRGB = Message.Parameters[1, 0];

                if (UserMiddleware.isIslandOwner(Session))
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
        private static void changeSizeObject(Session Session, ClientMessage Message)
        {
            try
            {
                int id = int.Parse(Message.Parameters[0, 0]);
                int shopObjectId = int.Parse(Message.Parameters[1, 0]);
                int x = int.Parse(Message.Parameters[2, 0]);
                int y = int.Parse(Message.Parameters[3, 0]);
                string coordinates = Message.Parameters[4, 0];
                string tam = Message.Parameters[5, 0];
                int rotation = int.Parse(Message.Parameters[6, 0]);

                if (UserMiddleware.isIslandOwner(Session))
                {
                    UserObject userObject = getUserObject(Session, id, true);
                    if (userObject != null)
                    {
                        IslandArea islandArea = (IslandArea)Session.User.Area;
                        userObject.updateAttributes(Session.User, Session.User.Area.id,
                            userObject.Posicion.x,
                            userObject.Posicion.y,
                            Convert.ToInt32(userObject.height),
                            userObject.ocupe,
                            userObject.rotation,
                            userObject.Color_1,
                            userObject.Color_2,
                            tam);
                        islandArea.changeSizeObjectHandler(userObject);
                        UserObjectDAO.updateUserObjectSizeInArea(userObject);
                    }
                }
            }
            catch (Exception ex)
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

                if (UserMiddleware.isIslandOwner(Session))
                {
                    UserObject userObject = getUserObject(Session, id, true);
                    if (userObject != null)
                    {
                        IslandArea islandArea = (IslandArea)Session.User.Area;
                        userObject.updateAttributes(Session.User, Session.User.Area.id,
                            userObject.Posicion.x,
                            userObject.Posicion.y,
                            Convert.ToInt32(userObject.height),
                            userObject.ocupe,
                            userObject.rotation,
                            color,
                            colorRGB,
                            userObject.size);
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
                int x = Convert.ToInt32(Message.Parameters[2, 0]);
                int y = Convert.ToInt32(Message.Parameters[3, 0]);
                string coordinates = Message.Parameters[4, 0];

                if (UserMiddleware.isIslandOwner(Session))
                {
                    UserObject userObject = getUserObject(Session, id, true);
                    if (userObject != null)
                    {
                        Point position = getPosition(Session, x, y);
                        if (!objectOnUser(Session, position, coordinates))
                        {
                            IslandArea islandArea = (IslandArea)Session.User.Area;
                            userObject.updateAttributes(Session.User, Session.User.Area.id,
                                x,
                                y,
                                Convert.ToInt32(userObject.height),
                                coordinates,
                                rotation,
                                userObject.Color_1,
                                userObject.Color_2,
                                userObject.size);
                            islandArea.rotateObjectHandler(userObject);
                            UserObjectDAO.rotateUserObjectInArea(userObject);
                        }
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

                if (UserMiddleware.isIslandOwner(Session))
                {
                    UserObject userObject = getUserObject(Session, id, true);
                    if (userObject != null)
                    {
                        IslandArea islandArea = (IslandArea)Session.User.Area;
                        islandArea.removeObject(Session, userObject);
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

                if (UserMiddleware.isIslandOwner(Session))
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

                if (UserMiddleware.isIslandOwner(Session))
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
                Point position = getPosition(Session, x, y);
                string shopObjectCoordinates = getShopObjectCoordinates(Session, userObject, position);
                if (shopObjectCoordinates != null)
                {
                    coordinates = shopObjectCoordinates;
                }
                if (!objectOnUser(Session, position, coordinates))
                {
                    IslandArea islandArea = (IslandArea)Session.User.Area;
                    userObject.updateAttributes(Session.User, islandArea.id, x, y, height, coordinates,
                        userObject.rotation, userObject.Color_1, userObject.Color_2, userObject.size);
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
        private static string getShopObjectCoordinates(Session Session, UserObject userObject, Point position)
        {
            ShopObject shopObject = ShopObjectCollection.getShopObjectById(userObject.ObjetoID);
            if (shopObject != null)
            {
                string shopObjectCoordinates = shopObject.something_1;
                if (shopObjectCoordinates != string.Empty)
                {
                    return Session.User.Area.MapaBytes.GetCoordinatesTakes(position, shopObjectCoordinates);
                }
            }
            return null;
        }
        private static Point getPosition(Session Session, int x, int y)
        {
            Point point = new Point(x, y);
            Point position = Session.User.Area.MapaBytes.GetCoordinates(point);
            return position;
        }
    }
}
