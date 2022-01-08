using Proyect_Base.app.Collections;
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
    class NpcHandler
    {
        public static void init()
        {
            HandlerManager.RegisterHandler(123120, new ProcessHandler(loadObjects), true);
            HandlerManager.RegisterHandler(123121, new ProcessHandler(buyObject), true);
        }
        private static void buyObject(Session Session, ClientMessage Message)
        {
            try
            {
                int npcObjectId = int.Parse(Message.Parameters[0, 0]);

                if (UserMiddleware.userInArea(Session) && Session.User.Area is PublicArea publicArea)
                {
                    AreaNpc areaNpc = publicArea.getAreaNpcWithOutMoviments();
                    if (areaNpc != null)
                    {
                        AreaNpcObject areaNpcObject = areaNpc.getObjectById(npcObjectId);
                        if (areaNpcObject != null
                            && userHasGoldCoinsPrice(Session, areaNpcObject)
                            && userHasSilverCoinsPrice(Session, areaNpcObject)
                            && userHasRequirementObjects(Session, areaNpcObject))
                        {
                            removeGoldCoinsUser(Session, areaNpcObject);
                            removeSilverCoinsUser(Session, areaNpcObject);
                            removeRequirementObjectsUser(Session, areaNpcObject);
                            addNpcObjectToUser(Session, areaNpcObject);

                            Session.SendData(new ServerMessage(new byte[] { 123, 121 }, new object[] { 1 }));
                        }
                        else
                        {
                            Session.SendData(new ServerMessage(new byte[] { 123, 121 }, new object[] { 0 }));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
        }
        private static void addNpcObjectToUser(Session Session, AreaNpcObject areaNpcObject)
        {
            ShopObject shopObject = ShopObjectCollection.getShopObjectById(areaNpcObject.shop_object_id);
            if (shopObject != null)
            {
                UserObject userObject = UserObjectDAO.createObjectUser(Session.User, shopObject, "tam_n");
                if (userObject != null)
                {
                    Session.User.addObject(userObject);
                    Session.User.addObjectToBackpackHandler(Session, userObject);
                }
            }
        }
        private static void removeRequirementObjectsUser(Session Session, AreaNpcObject areaNpcObject)
        {
            foreach (AreaNpcObjectRequirement objectRequirement in areaNpcObject.areaNpcObjectRequirements)
            {
                foreach (UserObject userObject in Session.User.objects.Values.Where(i => i.ObjetoID == objectRequirement.shop_object_id
                 && i.ZonaID == 0).Take(objectRequirement.amount).ToList())
                {
                    Session.SendData(new ServerMessage(new byte[] { 189, 169 }, new object[] { -1, objectRequirement.shop_object_id, 1 }));
                    UserObjectDAO.deleteUserObject(Session.User, userObject);
                }
            }
        }
        private static void removeSilverCoinsUser(Session Session, AreaNpcObject areaNpcObject)
        {
            if (areaNpcObject.price_silver > 0)
            {
                Session.User.removeSilverCoins(Session, areaNpcObject.price_silver);
            }
        }
        private static void removeGoldCoinsUser(Session Session, AreaNpcObject areaNpcObject)
        {
            if (areaNpcObject.price_gold > 0)
            {
                Session.User.removeGoldCoins(Session, areaNpcObject.price_gold);
            }
        }
        private static bool userHasRequirementObjects(Session Session, AreaNpcObject areaNpcObject)
        {
            bool validate = true;
            foreach (AreaNpcObjectRequirement objectRequirement in areaNpcObject.areaNpcObjectRequirements)
            {
                if (!objectsInUserBackpack(Session, objectRequirement.shop_object_id, objectRequirement.amount))
                {
                    validate = false;
                    break;
                }
            }
            return validate;
        }
        private static bool objectsInUserBackpack(Session Session, int objetId, int amount)
        {
            List<UserObject> userObjects = Session.User.objects.Values.Where(i => i.ObjetoID == objetId && i.ZonaID == 0).ToList();
            if (userObjects.Count >= amount)
            {
                return true;
            }
            return false;
        }
        private static bool userHasSilverCoinsPrice(Session Session, AreaNpcObject areaNpcObject)
        {
            if (areaNpcObject.price_silver > 0 && Session.User.plata >= areaNpcObject.price_silver)
            {
                return true;
            }
            else if (areaNpcObject.price_silver < 0)
            {
                return true;
            }
            return false;
        }
        private static bool userHasGoldCoinsPrice(Session Session, AreaNpcObject areaNpcObject)
        {
            if (areaNpcObject.price_gold > 0 && Session.User.oro >= areaNpcObject.price_gold)
            {
                return true;
            }
            else if (areaNpcObject.price_gold < 0)
            {
                return true;
            }
            return false;
        }
        private static void loadObjects(Session Session, ClientMessage Message)
        {
            try
            {
                if (UserMiddleware.userInArea(Session) && Session.User.Area is PublicArea publicArea)
                {
                    AreaNpc areaNpc = publicArea.getAreaNpcWithOutMoviments();
                    if (areaNpc != null)
                    {
                        areaNpc.loadNpcContentHandler(Session);
                    }
                }
            }
            catch(Exception ex)
            {
                Log.error(ex);
            }
        }
    }
}
