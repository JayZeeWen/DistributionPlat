using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.AgentManage
{
    public class LevelConfigEntity : IEntity<LevelConfigEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        public int? c_level { get; set; }

        public int? c_need_nums { get; set; }

        public int? c_need_level { get; set; }
        public int? c_level_num { get; set; }
        public int? c_recomm_reward { get; set; }
        public int? c_buy_reward { get; set; }
        public int? c_is_delete { get; set; }

        public DateTime? c_create_date { get; set; }


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

    public class LevelDetailEntity
    {
        public string F_Id { get; set; }

        public int? c_level { get; set; }

        public string LevelName { get; set; }

        public int? c_need_nums { get; set; }

        public int? c_need_level { get; set; }

        public string needLevelName { get; set; }
        public int? c_level_num { get; set; }
        public int? c_recomm_reward { get; set; }
        public int? c_buy_reward { get; set; }
        public int? c_is_delete { get; set; }

        public DateTime? c_create_date { get; set; }

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

}
