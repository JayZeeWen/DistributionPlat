using NFine.Domain.Entity.AgentManage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity
{
    public class ScoreCashEntity : IEntity<ScoreCashEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        public string c_user_id { get; set; }

        [ForeignKey("c_user_id")]
        public virtual AgentEntity agent { get; set; }

        public int c_amount { get; set; }


        public int  c_cash_state { get; set; }

        public string c_bank_person { get; set; }

        public string c_bank_name { get; set; }

        public string c_bank_account { get; set; }

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

    public class CashListEntity
    {
        public string F_Id { get; set; }

        public string c_user_id { get; set; }

        public string c_agent_name { get; set; }

        public string c_agent_mobile { get; set; }


        public int c_amount { get; set; }


        public int c_cash_state { get; set; }

        public string c_bank_person { get; set; }

        public string c_bank_name { get; set; }

        public string c_bank_account { get; set; }

        public DateTime? F_CreatorTime { get; set; }
    }
    
}
