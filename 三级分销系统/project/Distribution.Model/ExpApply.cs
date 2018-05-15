namespace Distribution.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_exp_apply")]
    public partial class ExpApply : BasicModel
    {

        [StringLength(50)]
        public string c_agent_id { get; set; }

        public int? c_apply_state { get; set; }

        public string c_remark { get; set; }

        public string c_image { get; set; }

    }
}
