/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: 三级分销平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity;
using NFine.Domain.Entity.AgentManage;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NFine.Application.SystemManage
{
    public class ExpApplyApp
    {
        private IExpApplyRepository service = new ExpApplyRepository();
        private UserLogOnApp userLogOnApp = new UserLogOnApp();

        public List<ExpApplyEntity> GetList(Pagination pagination)
        {
            var expression = ExtLinq.True<ExpApplyEntity>();
            
            return service.FindList(expression, pagination);
        }

        public List<ExpApplyViewEntity> GetViewList(Pagination pagination)
        {
            var list = GetList(pagination);

            List<ExpApplyViewEntity> viewList = new List<ExpApplyViewEntity>();
            foreach (var item in list)
            {
                ExpApplyViewEntity i = new ExpApplyViewEntity();
                SetViewEneity(i, item);
                viewList.Add(i);
            }
            return viewList;

        }

        public void SetViewEneity(ExpApplyViewEntity viewEntity ,ExpApplyEntity eneity)
        {
            AgentApp p = new AgentApp();
            viewEntity.F_Id = eneity.F_Id;
            viewEntity.c_agent_id = eneity.c_agent_id;
            viewEntity.c_apply_state = eneity.c_apply_state;
            viewEntity.c_remark = eneity.c_remark;
            viewEntity.c_image = eneity.c_image;
            viewEntity.c_agent_id = eneity.c_agent_id;
            viewEntity.c_agent_id = eneity.c_agent_id;
            viewEntity.F_CreatorTime = eneity.F_CreatorTime;
            var agent = p.GetForm(eneity.c_agent_id);
            if(agent != null)
            {
                viewEntity.c_agent_name = agent.c_name;
                viewEntity.c_agent_mobile = agent.c_mobile;
                viewEntity.c_exp_state = agent.c_exp_state;
                viewEntity.c_score = agent.c_score;
            }
            
        }


        public ExpApplyEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }
        public void SubmitForm(ExpApplyEntity userEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                userEntity.Modify(keyValue);
            }
            else
            {
                userEntity.Create();
            }
            service.SubmitForm(userEntity, keyValue);
        }
        public void UpdateForm(ExpApplyEntity userEntity)
        {
            service.Update(userEntity);
        }
        

    }
}
