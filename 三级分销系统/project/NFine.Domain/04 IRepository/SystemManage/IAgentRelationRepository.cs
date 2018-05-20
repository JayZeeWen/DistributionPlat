using NFine.Code;
/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: 三级分销平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Data;
using NFine.Domain.Entity;
using NFine.Domain.Entity.AgentManage;
using NFine.Domain.Entity.SystemManage;
using System.Collections.Generic;

namespace NFine.Domain.IRepository.SystemManage
{
    public interface IAgentRelationRepository : IRepositoryBase<AgentRelationEntity>
    {
        void DeleteForm(string keyValue);
        void SubmitForm(AgentRelationEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue);


        List<AgentRelationEntity> GetCashList(Pagination page);

    }
}
