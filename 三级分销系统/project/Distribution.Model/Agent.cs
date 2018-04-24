namespace Distribution.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_agent")]
    public partial class Agent
    {
        [Column("F_Id")]
        [Key]
        public string c_id { get; set; }

        [StringLength(50)]
        public string c_name { get; set; }

        [StringLength(32)]
        public string c_mobile { get; set; }

        [StringLength(32)]
        public string c_login_pwd { get; set; }

        [StringLength(32)]
        public string c_safe_pwd { get; set; }

        [StringLength(32)]
        public string c_bank_name { get; set; }

        [StringLength(32)]
        public string c_bank_account { get; set; }

        [StringLength(64)]
        public string c_address { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        [StringLength(32)]
        public string c_rec_person { get; set; }

        /// <summary>
        /// 收件人电话
        /// </summary>
        [StringLength(32)]
        public string c_rec_mobile { get; set; }

        /// <summary>
        /// 持卡人
        /// </summary>
        [StringLength(32)]
        public string c_bank_person { get; set; }

        public int? c_state { get; set; }

        public int? c_score { get; set; }

        public int? c_levle { get; set; }

        public int? c_agent_level { get; set; }

        public DateTime? c_create_date { get; set; }

        public string c_voucher_path { get; set; }
    }
}
