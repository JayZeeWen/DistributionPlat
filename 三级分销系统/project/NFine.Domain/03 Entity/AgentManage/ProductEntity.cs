using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.AgentManage
{
    public class ProductEntity : IEntity<ProductEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        public string c_name { get; set; }

        public int c_price { get; set; }

        public string c_desc { get; set; }

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
}
