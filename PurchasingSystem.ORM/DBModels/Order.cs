namespace PurchasingSystem.ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public int ID { get; set; }

        public Guid UserID { get; set; }

        public decimal PriceSum { get; set; }

        public DateTime CreateDate { get; set; }

        public int IsBuy { get; set; }

        public int IsSent { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; }

        public int? Amount { get; set; }

        public int? ShippingFee { get; set; }
    }
}
