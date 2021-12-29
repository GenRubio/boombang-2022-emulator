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

        //FlashSocket config
        public static int FLASH_SOCKET_PORT = 2001;

        //API config
        public static string API_HOST = "http://127.0.0.1:8000/webapi";

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
        public static string MAILTRAP_HOST = "smtp.mailtrap.io";
        public static string MAILTRAP_USERNAME = "8024af792dc05a";
        public static string MAILTRAP_PASSWORD = "091212b076a7a8";
        public static string MAILTRAP_FROM = "piotr@mailtrap.io";
        public static string MAILTRAP_TO = "elizabeth@westminster.co.uk";

        //WebSocket config
        public static bool WEB_SOCKET_ACTIVE = false;
        public static readonly string WEB_SOCKET_HOST = "127.0.0.1";
        public static readonly int WEB_SOCKET_PORT = 3300;
    }
}
