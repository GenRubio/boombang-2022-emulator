using Proyect_Base.app.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    public class AreaNpcObject
    {
        public int id { get; set; }
        public int area_npc_id { get; set; }
        public int shop_object_id { get; set; }
        public int price_gold { get; set; }
        public int price_silver { get; set; }
        public bool active { get; set; }
        public List<AreaNpcObjectRequirement> areaNpcObjectRequirements { get; set; }
        public AreaNpcObject(DataRow row)
        {
            this.id = (int)row["id"];
            this.area_npc_id = (int)row["area_npc_id"];
            this.shop_object_id = (int)row["shop_object_id"];
            this.price_gold = (int)row["price_gold"];
            this.price_silver = (int)row["price_silver"];
            this.active = bool.Parse(row["active"].ToString());
            this.areaNpcObjectRequirements = AreaNpcObjectRequirementDAO.getAreaNpcObjectRequirements(this.id);
        }
    }
}
