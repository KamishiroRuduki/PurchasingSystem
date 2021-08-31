namespace PurchasingSystem.ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Commodity")]
    public partial class Commodity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int OrderID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string URL { get; set; }

        public int? Price { get; set; }

        public int Quantity { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public int IsBuy { get; set; }
    }
}
