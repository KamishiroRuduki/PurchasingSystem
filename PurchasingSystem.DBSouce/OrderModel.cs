using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchasingSystem.DBSouce
{
   public class OrderModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Guid UserID { get; set; }
        public Decimal PriceSum { get; set; }
        public DateTime CreateDate { get; set; }
        public int? IsBuy { get; set; }
        public int? IsSent { get; set; }
        public string Remarks { get; set; }
        public int? Amount { get; set; }

        public int? ShippingFee { get; set; }

        public int OrderStatus { get; set; }

        public string CashRate { get; set; }
        public string PurchasingCost { get; set; }
    }
}
