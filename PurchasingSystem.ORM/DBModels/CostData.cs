namespace PurchasingSystem.ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CostData")]
    public partial class CostData
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeName { get; set; }

        public decimal Value { get; set; }
    }
}
