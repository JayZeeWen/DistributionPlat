/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: 三级分销平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFine.Application.SystemManage
{
    public class AgentApp
    {
        private IAgentRepository service = new AgentRepository();

        public List<AgentRepository> GetList()
        {
            return service.IQueryable().ToList();
        }
        public AgentRepository GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            if (service.IQueryable().Count(t => t.F_ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                service.Delete(t => t.c_id == keyValue);
            }
        }
        public void SubmitForm(AgentRepository AgentRepository, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                AgentRepository.Modify(keyValue);
                service.Update(AgentRepository);
            }
            else
            {
                AgentRepository.Create();
                service.Insert(AgentRepository);
            }
        }
    }
}
