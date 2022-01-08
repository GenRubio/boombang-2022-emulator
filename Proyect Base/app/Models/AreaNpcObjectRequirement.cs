using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    public class AreaNpcObjectRequirement
    {
        public int id { get; set; }
        public int area_npc_object_id { get; set; }
        public int shop_object_id { get; set; }
        public int amount { get; set; }
        public bool active { get; set; }
        public AreaNpcObjectRequirement(DataRow row)
        {
            this.id = (int)row["id"];
            this.area_npc_object_id = (int)row["area_npc_object_id"];
            this.shop_object_id = (int)row["shop_object_id"];
            this.amount = (int)row["amount"];
            this.active = bool.Parse(row["active"].ToString());
        }
    }
}
