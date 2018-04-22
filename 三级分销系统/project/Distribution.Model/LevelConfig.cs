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
        /// �ȼ�
        /// </summary>
        public int? c_level { get; set; }
        
        /// <summary>
        /// ������Ҫֱ������
        /// </summary>
        public int? c_need_nums { get; set; }
        
        /// <summary>
        /// ������Ҫ��Ӧ�ȼ�
        /// </summary>
        public int? c_need_level { get; set; }
        
        /// <summary>
        /// �ﵽ��Ӧ�ȼ�������
        /// </summary>
        public int? c_level_num { get; set; }
        
        /// <summary>
        /// �Ƽ�����
        /// </summary>
        public int? c_recomm_reward { get; set; }
        
        /// <summary>
        /// ������
        /// </summary>
        public int? c_buy_reward { get; set; }
        
        /// <summary>
        /// �Ƿ�ɾ��
        /// </summary>
        public int? c_is_delete { get; set; }

        public DateTime? c_create_date { get; set; }
        
    }
}
