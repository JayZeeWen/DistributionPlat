namespace Distribution.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_agent_relation")]
    public partial class AgentRelation
    {
        [Key]
        [Column("F_Id")]
        public string  c_id { get; set; }

        [StringLength(50)]
        public string c_parent_id { get; set; }

        [StringLength(50)]
        public string c_child_id { get; set; }

        [ForeignKey("c_parent_id")]
        public virtual Agent ParentAgent { get; set; }

        [ForeignKey("c_child_id")]
        public virtual Agent ChildrenAgent { get; set; }

        public DateTime? c_create_date { get; set; }
    }
}
