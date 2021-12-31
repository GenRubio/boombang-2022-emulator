using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Helpers
{
    class ItemAreaHelper
    {
        public static int getRandomTimeNextItem()
        {
            int minMinutes = 1;
            int maxMinutes = 7;
            Random random = new Random();
            return random.Next(minMinutes, maxMinutes + 1) * 60;
        }
    }
}
