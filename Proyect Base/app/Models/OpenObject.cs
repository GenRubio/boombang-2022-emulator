using Proyect_Base.app.Collections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    public class OpenObject
    {
        public int id { get; private set; }
        public int shop_object_id { get; set; }
        public int within_shop_object_id { get; set; }
        public int coins_gold { get; set; } //Si no se nesesita dejar en -1
        public int coins_silver { get; set; } //Si no se nesesita dejar en -1
        public double probability { get; set; } //Si no se nesesita dejar en -1
        public int amount { get; set; }
        public OpenObject(DataRow row)
        {
            this.id = int.Parse(row["id"].ToString());
            this.shop_object_id = int.Parse(row["shop_object_id"].ToString());
            this.within_shop_object_id = int.Parse(row["within_shop_object_id"].ToString() == string.Empty ? "-1" : row["within_shop_object_id"].ToString());
            this.coins_gold = int.Parse(row["coins_gold"].ToString());
            this.coins_silver = int.Parse(row["coins_silver"].ToString());
            this.probability = double.Parse(row["probability"].ToString());
            this.amount = int.Parse(row["amount"].ToString());
        }
        //FUNCTIONS

        //MODEL SETTERS

        //MODEL GETTERS
        public ShopObject getWithinObject()
        {
            ShopObject shopObject = ShopObjectCollection.getShopObjectById(this.within_shop_object_id);
            if (shopObject != null)
            {
                return shopObject;
            }
            return null;
        }
        //HANDLERS
    }
}
