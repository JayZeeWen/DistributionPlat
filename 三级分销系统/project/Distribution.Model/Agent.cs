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
        [Key]
        public int c_id { get; set; }

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

        public int? c_state { get; set; }

        public int? c_score { get; set; }

        public int? c_levle { get; set; }

        public int? c_agent_level { get; set; }

        public DateTime? c_create_date { get; set; }

        public string c_voucher_path { get; set; }
    }
}
