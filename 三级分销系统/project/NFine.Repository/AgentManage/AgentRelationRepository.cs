/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: 三级分销平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Data;
using NFine.Domain.Entity.AgentManage;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System;

namespace NFine.Repository.SystemManage
{
    public class AgentRelationRepository : RepositoryBase<AgentRelationEntity>, IAgentRelationRepository
    {
        void IAgentRelationRepository.DeleteForm(string keyValue)
        {
            throw new NotImplementedException();
        }

        List<AgentRelationEntity> IAgentRelationRepository.GetCashList(Pagination page)
        {
            throw new NotImplementedException();
        }

        void IAgentRelationRepository.SubmitForm(AgentRelationEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {
            throw new NotImplementedException();
        }
    }
}
