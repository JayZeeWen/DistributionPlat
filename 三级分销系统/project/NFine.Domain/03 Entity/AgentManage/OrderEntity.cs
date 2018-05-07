using NFine.Domain.Entity.AgentManage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity
{
    public class OrderEntity : IEntity<OrderEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        public string c_agent_id { get; set; }

        public string c_order_num { get; set; }


        public decimal? c_total { get; set; }

        public int c_state { get; set; }

        public string c_express_num { get; set; }

        public string c_express_name { get; set; }

        public string c_rec_person { get; set; }

        public string c_mobile { get; set; }

        public string c_address { get; set; }

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

    public class OrderViewEntity
    {
        public string F_Id { get; set; }

        public string c_agent_id { get; set; }

        public string c_agent_name { get; set; }

        public string c_agent_mobile { get; set; }

        public string c_order_num { get; set; }

        public List<OrderDetailViewEntity> detailList { get; set; }

        public decimal? c_total { get; set; }

        public int c_state { get; set; }

        public string c_express_num { get; set; }

        public string c_express_name { get; set; }

        public string c_rec_person { get; set; }

        public string c_mobile { get; set; }

        public string c_address { get; set; }

        public DateTime? F_CreatorTime { get; set; }
    }
    
}
