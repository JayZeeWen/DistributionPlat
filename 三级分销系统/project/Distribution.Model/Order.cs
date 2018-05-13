namespace Distribution.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_order")]
    public partial class Order :BasicModel
    {

        [StringLength(50)]
        public string c_agent_id { get; set; }

        [StringLength(64)]
        public string c_order_num { get; set; }

        public decimal? c_total { get; set; }

        public int? c_state { get; set; }


        public string c_rec_person { get; set; }

        public string c_mobile { get; set; }

        public string c_address { get; set; }

        [StringLength(64)]
        public string c_express_num { get; set; }

        [StringLength(32)]
        public string c_express_name { get; set; }

        /// <summary>
        /// 订单类型 1：产品购买订单   2：加盟订单
        /// </summary>
        public int? c_order_type { get; set; }

        [StringLength(128)]
        public string c_remark { get; set; }


    }
}
