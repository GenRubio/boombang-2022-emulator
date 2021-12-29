using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.socket
{
    class SocketData
    {
        public string channel { get; set; }
        public string parameters { get; set; }
        public SocketData(string channel)
        {
            this.channel = channel;
            this.parameters = "";
        }
        public void addParameter(string parameter)
        {
            if (this.parameters != "")
            {
                this.parameters += "," + parameter;
            }
            else
            {
                this.parameters += parameter;
            }
        }
    }
}
