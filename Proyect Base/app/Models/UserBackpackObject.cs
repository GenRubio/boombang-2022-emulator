using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    public class UserBackpackObject
    {
        public int itemId { get; set; }
        public int count { get; set; }
        public UserBackpackObject(DataRow row)
        {
            this.itemId = (int)row["ItemID"];
            this.count = Convert.ToInt32(row["total"]);
        }
    }
}
