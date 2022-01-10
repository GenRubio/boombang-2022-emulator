using Proyect_Base.app.Connection;
using Proyect_Base.app.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Collections
{
    class SessionCollection
    {
        public static ConcurrentDictionary<int, Session> onlineUsers = new ConcurrentDictionary<int, Session>();
        public static void removeOnlineUser(User User)
        {
            if (onlineUsers.ContainsKey(User.id))
            {
                Session Session = null;
                onlineUsers.TryRemove(User.id, out Session);
            }
        }
        public static void addOnlineUser(User User, Session Session)
        {
            onlineUsers.TryAdd(User.id, Session);
            Session.User = User;
        }
        public static Session getSessionByUserId(int id)
        {
            if (onlineUsers.ContainsKey(id))
            {
                return onlineUsers[id];
            }
            return null;
        }
        public static Session getSessionByUID(string uid, string IP)
        {
            return onlineUsers.Values.ToList().Find(i => i.UID == uid && i.IP == IP);
        }
    }
}
