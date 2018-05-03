namespace Distribution.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_product")]
    public partial class Product :BasicModel
    {

        [StringLength(50)]
        public string c_name { get; set; }

        public int? c_price { get; set; }

        [StringLength(128)]
        public string c_desc { get; set; }

        [StringLength(128)]
        public string c_image { get; set; }

    }
}
