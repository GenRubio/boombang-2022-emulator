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
        public List<string> Parameters { get; set; }
        public WebMessage(string data)
        {
            this.data = data;
            this.Parameters = GetParameters();
        }
        public string GetKey()
        {
            try
            {
                return Regex.Split(this.data, "³²")[0];
            }
            catch
            {
                App.Form.WriteLine("El paquete tiene estructura incorrecta.", "error");
            }
            return "-1";
        }
        public List<string> GetParameters()
        {
            List<string> parameters = new List<string>();
            try
            {
                string data = Regex.Split(this.data, "³²")[1];
                if (data.Contains("³"))
                {
                    string[] positions = Regex.Split(this.data, "³²")[1].Split('³');
                    for (int i = 0; i < positions.Count(); i++)
                    {
                        parameters.Add(positions[i]);
                    }
                }
            }
            catch
            {
                App.Form.WriteLine("El paquete tiene estructura incorrecta.", "error");
            }
            return parameters;
        }
        public string GetData()
        {
            return this.data;
        }
        public string GetIP()
        {
            try
            {
                return this.Parameters[this.Parameters.Count - 1];
            }
            catch
            {
                App.Form.WriteLine("El paquete tiene estructura incorrecta.", "error");
            }
            return "-1";
        }
        public string GetUID()
        {
            try
            {
                return this.Parameters[0];
            }
            catch
            {
                App.Form.WriteLine("El paquete tiene estructura incorrecta.", "error");
            }
            return "-1";
        }
    }
}
