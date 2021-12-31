using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    class ApiModelExemple
    {
        public string username { get; set; }
        public string password { get; set; }
        public ApiModelExemple(dynamic api)
        {
            this.username = api.username;
            this.password = api.password;
        }
        //FUNCTIONS

        //MODEL SETTERS

        //MODEL GETTERS

        //HANDLERS
    }
}
