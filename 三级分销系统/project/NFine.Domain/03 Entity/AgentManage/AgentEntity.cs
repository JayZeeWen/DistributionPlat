using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.AgentManage
{
    public class AgentEntity : IEntity<AgentEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        public string c_name { get; set; }

        public string c_mobile { get; set; }

        public string c_login_pwd { get; set; }

        public string c_safe_pwd { get; set; }

        public string c_bank_name { get; set; }

        public string c_bank_account { get; set; }

        public string c_address { get; set; }

        public int? c_state { get; set; }

        public int? c_score { get; set; }

        public int? c_levle { get; set; }

        public int? c_agent_level { get; set; }

        /// <summary>
        /// 是否已经奖励
        /// </summary>
        public bool? c_had_reward { get; set; }

        /// <summary>
        /// 代理类型：0：体验店  1：加盟店
        /// </summary>
        public int? c_agnet_type { get; set; }


        /// <summary>
        /// 体验状态 0：未体验  1：已体验
        /// </summary>
        public int? c_exp_state { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public string c_rec_person { get; set; }

        /// <summary>
        /// 收件人电话
        /// </summary>
        public string c_rec_mobile { get; set; }

        /// <summary>
        /// 持卡人
        /// </summary>
        public string c_bank_person { get; set; }

        public string c_voucher_path { get; set; }

        public DateTime? c_create_date { get; set; }


        public DateTime? F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public string F_DeleteUserId { get; set; }

        public bool? F_DeleteMark { get; set; }
    }

    public class AgentViewModel
    {
        #region agent

        public string F_Id { get; set; }

        public string c_name { get; set; }

        public string c_mobile { get; set; }

        public string c_login_pwd { get; set; }

        public string c_safe_pwd { get; set; }

        public string c_bank_name { get; set; }

        public string c_bank_account { get; set; }

        public string c_address { get; set; }

        public int? c_state { get; set; }

        public int? c_score { get; set; }

        public int? c_levle { get; set; }

        public int? c_agent_level { get; set; }

        /// <summary>
        /// 是否已经奖励
        /// </summary>
        public bool? c_had_reward { get; set; }

        /// <summary>
        /// 代理类型：0：体验店  1：加盟店
        /// </summary>
        public int? c_agnet_type { get; set; }


        /// <summary>
        /// 体验状态 0：未体验  1：已体验
        /// </summary>
        public int? c_exp_state { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public string c_rec_person { get; set; }

        /// <summary>
        /// 收件人电话
        /// </summary>
        public string c_rec_mobile { get; set; }

        /// <summary>
        /// 持卡人
        /// </summary>
        public string c_bank_person { get; set; }

        public string c_voucher_path { get; set; }

        public DateTime? c_create_date { get; set; }


        public DateTime? F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public string F_DeleteUserId { get; set; }

        public bool? F_DeleteMark { get; set; } 
        #endregion

        /// <summary>
        /// 代理商代数
        /// </summary>
        public int? agentGen { get; set; }
    }
}
