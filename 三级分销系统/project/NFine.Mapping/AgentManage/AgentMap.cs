using NFine.Domain.Entity.AgentManage;
/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: 分销平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System.Data.Entity.ModelConfiguration;

namespace NFine.Mapping.AgentManage
{
    public class AgentMap : EntityTypeConfiguration<AgentEntity>
    {
        public AgentMap()
        {
            this.ToTable("t_agent");

            this.HasKey(t => t.F_Id);
        }
    }
}
