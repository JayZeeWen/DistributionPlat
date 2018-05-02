namespace Distribution.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_order")]
    public partial class Order
    {
        [Key]
        [StringLength(50)]
        public string F_Id { get; set; }

        [StringLength(50)]
        public string c_agent_id { get; set; }

        [StringLength(64)]
        public string c_order_num { get; set; }

        public decimal? c_total { get; set; }

        public int? c_state { get; set; }

        [StringLength(64)]
        public string c_express_num { get; set; }

        [StringLength(32)]
        public string c_express_name { get; set; }

        [StringLength(50)]
        public string F_CreatorUserId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? F_CreatorTime { get; set; }

        [StringLength(50)]
        public string F_DeleteMark { get; set; }

        [StringLength(50)]
        public string F_DeleteUserId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? F_DeleteTime { get; set; }

        [StringLength(50)]
        public string F_LastModifyUserId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? F_LastModifyTime { get; set; }
    }
}
