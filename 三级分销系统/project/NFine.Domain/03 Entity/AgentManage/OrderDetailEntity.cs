using NFine.Domain.Entity.AgentManage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity
{
    public class OrderDetailEntity : IEntity<OrderDetailEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        
        public string c_order_id { get; set; }

        public string c_product_id { get; set; }

        public int  c_amount { get; set; }

        public decimal c_total { get; set; }

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

    public class OrderDetailViewEntity
    {
        public string F_Id { get; set; }

        public string c_order_id { get; set; }

        public string c_product_id { get; set; }

        public string c_product_Name { get; set; }

        public string c_product_price { get; set; }

        public int c_amount { get; set; }

        public decimal c_total { get; set; }

        public DateTime? F_CreatorTime { get; set; }
    }
    
    
}
