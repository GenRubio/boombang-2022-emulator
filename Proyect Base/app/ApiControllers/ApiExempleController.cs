using Proyect_Base.app.Api;
using Proyect_Base.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.ApiControllers
{
    class ApiExempleController
    {
        public static ApiModelExemple getLogin(string name, string password)
        {
            API api = new API("user-login");
            api.addParameter("username", name);
            api.addParameter("password", password);

            return new ApiModelExemple(api.getObjects());
        }
    }
}
