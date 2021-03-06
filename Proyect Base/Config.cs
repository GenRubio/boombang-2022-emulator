using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base
{
    class Config
    {
        //App config
        public static string APP_NAME = "BoomBang Server";
        public static bool APP_SEND_NOT_PROGRAMMED_PACKETS = true;
        //FlashSocket config
        public static int FLASH_SOCKET_PORT = 2001;
        //API config
        public static string API_HOST = "http://127.0.0.1:8000/webapi";
        //WebSocket config
        public static bool WEB_SOCKET_ACTIVE = true;
        public static readonly string WEB_SOCKET_HOST = "127.0.0.1";
        public static readonly int WEB_SOCKET_PORT = 3300;
        //Mysql connection config
        public static string DB_HOST = "localhost";
        public static uint DB_PORT = 3306;
        public static string DB_DATABASE = "boombang";
        public static string DB_USERNAME = "root";
        public static string DB_PASSWORD = "";
        public static uint DB_TIMEOUT = 100000;
        //SqlKata connection config
        public static string KATA_HOST = "localhost";
        public static string KATA_PORT = "3306";
        public static string KATA_DATABASE = "boombang";
        public static string KATA_USERNAME = "root";
        public static string KATA_PASSWORD = "";
        public static string KATA_SSL_MODE = "None";
        //Mailtrap config
        public static string MAILTRAP_HOST = "";
        public static string MAILTRAP_USERNAME = "";
        public static string MAILTRAP_PASSWORD = "";
        public static string MAILTRAP_FROM = "";
        public static string MAILTRAP_TO = "";
    }
}
