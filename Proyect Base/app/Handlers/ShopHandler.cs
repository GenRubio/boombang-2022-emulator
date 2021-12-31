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
    class ShopHandler
    {
        public static void init()
        {
            HandlerManager.RegisterHandler(189133, new ProcessHandler(loadShop), true);
            HandlerManager.RegisterHandler(189134, new ProcessHandler(buyObjetByGold), true);
            HandlerManager.RegisterHandler(189137, new ProcessHandler(buyObjetBySilver), true);
        }
        private static void buyObjetByGold(Session Session, ClientMessage Message)
        {
            if (UserMiddleware.userInArea(Session))
            {
                try
                {
                    int id = int.Parse(Message.Parameters[0, 0]);
                    string tam = Message.Parameters[1, 0];

                    ShopObject shopObject = ShopObjectCollection.getShopObjectById(id);
                    if (shopObject != null)
                    {
                        if (Session.User.oro >= shopObject.Precio_Oro)
                        {
                            UserObject userObject = UserObjectDAO.createObjectUser(Session.User, shopObject, tam);
                            if (userObject != null)
                            {
                                Session.User.removeGoldCoins(Session, shopObject.Precio_Oro);
                                Session.User.addObject(userObject);
                                Session.User.addObjectToBackpackHandler(Session, userObject);
                                Session.SendData(new ServerMessage(new byte[] { 189, 134 }, new object[] { 1 }));
                            }
                        }
                        else
                        {
                            Session.SendData(new ServerMessage(new byte[] { 189, 134 }, new object[] { 0 }));
                        }
                    }
                    else
                    {
                        Session.SendData(new ServerMessage(new byte[] { 189, 134 }, new object[] { -1 }));
                    }
                }
                catch(Exception ex)
                {
                    Log.error(ex);
                }
            }
        }
        private static void buyObjetBySilver(Session Session, ClientMessage Message)
        {
            if (UserMiddleware.userInArea(Session))
            {
                try
                {
                    int id = int.Parse(Message.Parameters[0, 0]);
                    string tam = Message.Parameters[1, 0];

                    ShopObject shopObject = ShopObjectCollection.getShopObjectById(id);
                    if (shopObject != null)
                    {
                        if (Session.User.plata >= shopObject.Precio_Plata)
                        {
                            UserObject userObject = UserObjectDAO.createObjectUser(Session.User, shopObject, tam);
                            if (userObject != null)
                            {
                                Session.User.removeSilverCoins(Session, shopObject.Precio_Plata);
                                Session.User.addObject(userObject);
                                Session.User.addObjectToBackpackHandler(Session, userObject);
                                Session.SendData(new ServerMessage(new byte[] { 189, 134 }, new object[] { 1 }));
                            }
                        }
                        else
                        {
                            Session.SendData(new ServerMessage(new byte[] { 189, 134 }, new object[] { 0 }));
                        }
                    }
                    else
                    {
                        Session.SendData(new ServerMessage(new byte[] { 189, 134 }, new object[] { -1 }));
                    }
                }
                catch (Exception ex)
                {
                    Log.error(ex);
                }
            }
        }
        private static void loadShop(Session Session, ClientMessage Message)
        {
            ServerMessage server = new ServerMessage(new byte[] { 189, 133 });
            server.AppendParameter(ShopObjectCollection.shopObjects.Count());
            foreach(ShopObject shopObject in ShopObjectCollection.shopObjects.Values.ToList())
            {
                shopObject.getObjectParametersHandler(server);
            }
            Session.SendData(server);
        }
    }
}
