using NFine.Domain.Entity.AgentManage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity
{
    public class ScoreDetailEntity : IEntity<ScoreDetailEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        public string c_user_id { get; set; }
        

        public int c_amount { get; set; }

        public string c_reason { get; set; }

        public DateTime c_create_date { get; set; }
        

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

    public class ScoreDetaiListEntity
    {
        public string F_Id { get; set; }

        public string c_user_id { get; set; }

        public AgentEntity agent { get; set; }


        public int c_amount { get; set; }

        public string c_reason { get; set; }

        public string c_agent_name { get; set; }

        public string c_agent_mobile { get; set; }


        public DateTime? c_create_date { get; set; }
    }
    
}
