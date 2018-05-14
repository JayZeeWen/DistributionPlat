using NFine.Domain.Entity.AgentManage;
/*******************************************************************************
* Copyright © 2016 NFine.Framework 版权所有
* Author: NFine
* Description: 三级分销平台
* Website：http://www.nfine.cn
*********************************************************************************/
using System.Data.Entity.ModelConfiguration;

namespace NFine.Mapping.AgentManage
{
    public class PubNoticeMap : EntityTypeConfiguration<PubNoticeEntity>
    {
        public PubNoticeMap()
        {
            this.ToTable("t_public_notice");

            this.HasKey(t => t.F_Id);

        }
    }
}
