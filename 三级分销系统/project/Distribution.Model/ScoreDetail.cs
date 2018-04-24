namespace Distribution.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_score_detail")]
    public partial class ScoreDetail
    {
        [Key]
        public int c_id { get; set; }

        public string c_user_id { get; set; }

        public int? c_amount { get; set; }

        [StringLength(128)]
        public string c_reason { get; set; }

        public DateTime? c_create_date { get; set; }
    }
}
