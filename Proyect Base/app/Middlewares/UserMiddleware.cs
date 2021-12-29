
using Proyect_Base.app.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Middlewares
{
    class UserMiddleware
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
    }
}
