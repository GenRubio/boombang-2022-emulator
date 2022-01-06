using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    public class OpenObjectList
    {
        public double Probability { get; set; }
        public OpenObject Item { get; set; }
        public OpenObjectList(double Probability, OpenObject Item)
        {
            this.Probability = Probability;
            this.Item = Item;
        }
    }
}
