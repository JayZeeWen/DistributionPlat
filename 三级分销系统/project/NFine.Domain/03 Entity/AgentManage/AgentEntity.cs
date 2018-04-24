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

        public DateTime? c_create_date { get; set; }

        public DateTime? F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public string F_DeleteUserId { get; set; }

        public bool? F_DeleteMark { get; set; }
    }
}
