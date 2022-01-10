using Proyect_Base.forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Proyect_Base.web_socket
{
    public class WebMessage
    {
        private string data { get; set; }
        public string [] Parameters { get; set; }
        public WebMessage(string data)
        {
            this.data = data;
            this.Parameters = GetParameters();
        }
        public string GetKey()
        {
            return Regex.Split(this.data, "³²")[0];
        }
        public string[] GetParameters()
        {
            return Regex.Split(this.data, "³²")[1].Split('³');
        }
        public string GetData()
        {
            return this.data;
        }
        public string GetIP()
        {
            return this.Parameters[this.Parameters.Length -1];
        }
        public string GetUID()
        {
            try
            {
                return Regex.Split(this.data, "³²")[1].Split('³')[0];
            }
            catch
            {
                App.Form.WriteLine("El paquete tiene estructura incorrecta.", "error");
            }
            return "-1";
        }
    }
}
