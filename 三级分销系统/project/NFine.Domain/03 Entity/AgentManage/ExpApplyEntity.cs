using NFine.Domain.Entity.AgentManage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity
{
    public class ExpApplyEntity : IEntity<ExpApplyEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        public string c_agent_id { get; set; }

        public int? c_apply_state { get; set; }

        public string c_remark { get; set; }

        public string c_image { get; set; }

        #region Interface

        public DateTime? F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public string F_DeleteUserId { get; set; }

        public bool? F_DeleteMark { get; set; } 
        #endregion
    }

    public class ExpApplyViewEntity
    {
        public string F_Id { get; set; }

        public string c_agent_id { get; set; }


        public int? c_apply_state { get; set; }


        public string c_image { get; set; }

        public string c_agent_name { get; set; }

        public string c_agent_mobile { get; set; }

        public int? c_exp_state { get; set; }

        public int? c_score { get; set; }

        public DateTime? F_CreatorTime { get; set; }


        public string c_remark { get; set; }
    }
    
}
