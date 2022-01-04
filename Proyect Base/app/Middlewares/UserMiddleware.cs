
using Proyect_Base.app.Connection;
using Proyect_Base.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Middlewares
{
    public class UserMiddleware
    {
        public static bool userNotLogged(Session Session)
        {
            if (Session != null && Session.User == null)
            {
                return true;
            }
            return false;
        }
        public static bool userLogged(Session Session)
        {
            if (Session != null && Session.User != null)
            {
                return true;
            }
            return false;
        }
        public static bool userOutOfArea(Session Session)
        {
            if (userLogged(Session) && Session.User.Area == null)
            {
                return true;
            }
            return false;
        }
        public static bool userInArea(Session Session)
        {
            if (userLogged(Session) && Session.User.Area != null)
            {
                return true;
            }
            return false;
        }
        public static bool isIslandOwner(Session Session)
        {
            if (userInArea(Session)
                && Session.User.Area is IslandArea islandArea
                && islandArea.userCreatorId == Session.User.id)
            {
                return true;
            }
            return false;
        }
    }
}
