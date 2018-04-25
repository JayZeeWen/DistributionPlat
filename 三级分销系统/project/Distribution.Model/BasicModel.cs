using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Distribution.Model
{
    public abstract class BasicModel
    {
        [Key]
        [StringLength(50)]
        public string F_Id { get; set; }

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
