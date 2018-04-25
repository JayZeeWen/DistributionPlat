/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: 分销平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Data;
using NFine.Domain.Entity.AgentManage;
using NFine.Domain.Entity.SystemManage;

namespace NFine.Domain.IRepository.SystemManage
{
    public interface IAgentRepository : IRepositoryBase<AgentEntity>
    {
        void DeleteForm(string keyValue);
        void SubmitForm(AgentEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue);
    }
}
