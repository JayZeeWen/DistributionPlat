namespace Distribution.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_public_notice")]
    public partial class PublicNotice : BasicModel
    {

        [StringLength(50)]
        public string c_pub_user { get; set; }
        
        
        public string c_pub_title { get; set; }

        [StringLength(128)]
        public string c_content { get; set; }

        [StringLength(128)]
        public string c_remark { get; set; }

        public DateTime c_pub_time { get; set; }

        public int c_state { get; set; }


    }
}
