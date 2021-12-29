using Newtonsoft.Json.Linq;
using Proyect_Base.forms;
using Proyect_Base.logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Api
{
    class API
    {
        private string url { get; set; }
        private string parameters { get; set; }
        public API(string url)
        {
            this.url = url;
            this.parameters = "";
        }
        public void addParameter(string key, string value)
        {
            if (this.parameters == "")
            {
                this.parameters += ("?" + key + "=" + value);
            }
            else
            {
                this.parameters += ("&" + key + "=" + value);
            }
        }
        public JObject getObjects()
        {
            try
            {
                string urlPath = Config.API_HOST + "/" + this.url;
                string json = new WebClient().DownloadString(urlPath + this.parameters);
                if (json != "")
                {
                    return JObject.Parse(json);
                }
                else
                {
                   App.Form.WriteLine("Ruta: Ok | Respuesta: No encontrada");
                }
            }
            catch (Exception ex)
            {
                Log.error(ex);
            }
            return null;
        }
    }
}

