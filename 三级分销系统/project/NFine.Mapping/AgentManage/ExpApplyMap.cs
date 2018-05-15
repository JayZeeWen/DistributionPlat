﻿using NFine.Domain.Entity;
/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: 三级分销平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System.Data.Entity.ModelConfiguration;

namespace NFine.Mapping.AgentManage
{
    public class ExpApplyMap : EntityTypeConfiguration<ExpApplyEntity>
    {
        public ExpApplyMap()
        {
            this.ToTable("t_exp_apply");

            this.HasKey(t => t.F_Id);

        }
    }
}
