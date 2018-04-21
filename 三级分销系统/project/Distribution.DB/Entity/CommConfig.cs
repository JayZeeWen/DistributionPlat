namespace Distribution.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_common_config")]
    public partial class CommConfig
    {
        [Key]
        public int c_id { get; set; }

        public int? c_category_id { get; set; }

        public int? c_key { get; set; }

        [StringLength(32)]
        public string c_value { get; set; }

        public int? c_is_delete { get; set; }

        public DateTime? c_create_date { get; set; }
    }
}
