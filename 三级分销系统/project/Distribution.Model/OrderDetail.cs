namespace Distribution.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_order_detail")]
    public partial class OrderDetail :BasicModel
    {
        [StringLength(50)]
        public string c_order_id { get; set; }

        [StringLength(50)]
        public string c_product_id { get; set; }

        [ForeignKey("c_product_id")]
        public Product Pro { get; set; }

        public int? c_amount { get; set; }

        public decimal? c_total { get; set; }

    }
}
