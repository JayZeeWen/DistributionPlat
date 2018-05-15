/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: 三级分销平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;

namespace NFine.Application.SystemManage
{
    public class ScoreCashApp
    {
        private IScoreCashRepository service = new ScoreCashRepository();
        private UserLogOnApp userLogOnApp = new UserLogOnApp();

        public List<ScoreCashEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<ScoreCashEntity>();
            
            return service.FindList(expression, pagination);
        }


        public ScoreCashEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }
        public void SubmitForm(ScoreCashEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                userEntity.Modify(keyValue);
            }
            else
            {
                userEntity.Create();
            }
            service.SubmitForm(userEntity, userLogOnEntity, keyValue);
        }
        public void UpdateForm(ScoreCashEntity userEntity)
        {
            service.Update(userEntity);
        }

        public List<ScoreCashEntity> GetCashList(Pagination page)
        {
            return service.GetCashList(page);
        }

        public List<CashListEntity> GetCashViewList(Pagination page)
        {
            var list = service.FindList(page);
            List<CashListEntity> viewList = new List<CashListEntity>();
            AgentApp app = new AgentApp();
            foreach (var item in list)
            {
                CashListEntity entity = new CashListEntity();
                var agent = app.GetForm(item.c_user_id);
                if(agent != null )
                {
                    entity.c_agent_name = agent.c_name;
                    entity.c_agent_mobile = agent.c_mobile;
                }
                entity.F_Id = item.F_Id;
                entity.c_user_id = item.c_user_id;
                entity.c_amount = item.c_amount;
                entity.c_cash_state = item.c_cash_state;
                entity.c_bank_person = item.c_bank_person;
                entity.c_bank_name = item.c_bank_name;
                entity.c_bank_account = item.c_bank_account;
                entity.F_CreatorTime = item.F_CreatorTime;
                viewList.Add(entity);
            }
            return viewList;
        }

    }
}
