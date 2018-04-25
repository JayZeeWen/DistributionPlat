namespace Distribution.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_score_cash")]
    public partial class ScoreCash 
    {

        [Key]
        [StringLength(50)]
        public string F_Id { get; set; }

        [StringLength(32)]
        public string c_user_id { get; set; }

        public int? c_amount { get; set; }

        public int? c_cash_state { get; set; }

        [StringLength(32)]
        public string c_bank_person { get; set; }

        [StringLength(32)]
        public string c_bank_name { get; set; }

        [StringLength(32)]
        public string c_bank_account { get; set; }

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
