/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: 三级分销平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity.AgentManage;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFine.Application.SystemManage
{
    public class AgentRelationApp
    {
        private IAgentRelationRepository service = new AgentRelationRepository();

        public List<AgentRelationEntity> GetListByParentId(List<string> parentIds)
        {
            string pIds = "";
            foreach (var item in parentIds)
            {
                pIds += "'" + item + "',";
            }
            pIds = pIds.Substring(0, pIds.Length - 1);
            
            string sql = string.Format(@" select * from t_agent_relation ar where ar.c_parent_id in( {0} ) ", pIds);
            return service.FindList(sql);
        }
        
    }
}
