using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Helpers
{
    class ItemAreaHelper
    {
        private static Random Random = new Random();
        public static int getRandomTimeNextItem()
        {
            int minMinutes = 1;
            int maxMinutes = 7;
            return Random.Next(minMinutes, maxMinutes + 1) * 60;
        }
    }
}
