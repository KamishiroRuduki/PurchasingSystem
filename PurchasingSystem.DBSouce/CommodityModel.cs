using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchasingSystem.DBSouce
{
    public class CommodityModel
    {
        public int commodityID { get; set; }
        public int orderID { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public int Quantity { get; set; }
        public string URL { get; set; }
        public string Type { get; set; }
        public string CashRate { get; set; }
        public string PurchasingCost { get; set; }
        public int IsBuy { get; set; }
    }
}
