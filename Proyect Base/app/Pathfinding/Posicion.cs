using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Pathfinding
{
    public class Posicion
    {
        public int x, y, z;
        public Posicion(int x, int y, int z = 4)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
