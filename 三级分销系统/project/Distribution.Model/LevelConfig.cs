namespace Distribution.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("t_level_config")]
    public partial class LevelConfig
    {
        [Key]
        public int c_id { get; set; }
        
        /// <summary>
        /// 等级
        /// </summary>
        public int? c_level { get; set; }
        
        /// <summary>
        /// 升级需要直推人数
        /// </summary>
        public int? c_need_nums { get; set; }
        
        /// <summary>
        /// 升级需要相应等级
        /// </summary>
        public int? c_need_level { get; set; }
        
        /// <summary>
        /// 达到相应等级的人数
        /// </summary>
        public int? c_level_num { get; set; }
        
        /// <summary>
        /// 推荐奖励
        /// </summary>
        public int? c_recomm_reward { get; set; }
        
        /// <summary>
        /// 购买奖励
        /// </summary>
        public int? c_buy_reward { get; set; }
        
        /// <summary>
        /// 是否删除
        /// </summary>
        public int? c_is_delete { get; set; }

        public DateTime? c_create_date { get; set; }
        
    }
}
