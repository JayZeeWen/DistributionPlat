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
    public class ScoreDetailApp
    {
        private IScoreDetailRepository service = new ScoreDetailRepository();

        public List<ScoreDetailEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<ScoreDetailEntity>();
            
            return service.FindList(expression, pagination);
        }


        public ScoreDetailEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }
        public void SubmitForm(ScoreDetailEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
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
        public void UpdateForm(ScoreDetailEntity userEntity)
        {
            service.Update(userEntity);
        }

        public List<ScoreDetailEntity> GetCashList(Pagination page)
        {
            return service.GetDetailList(page);
        }

        public List<ScoreDetaiListEntity> GetCashViewList(Pagination page)
        {
            page.sidx = "c_create_date desc";
            var list = service.GetDetailList(page);
            List<ScoreDetaiListEntity> viewList = new List<ScoreDetaiListEntity>();
            AgentApp app = new AgentApp();
            foreach (var item in list)
            {
                ScoreDetaiListEntity entity = new ScoreDetaiListEntity();
                var agent = app.GetForm(item.c_user_id);
                if(agent != null )
                {
                    entity.c_agent_name = agent.c_name;
                    entity.c_agent_mobile = agent.c_mobile;
                }
                entity.F_Id = item.F_Id;
                entity.c_user_id = item.c_user_id;
                entity.c_amount = item.c_amount;
                entity.c_reason = item.c_reason;
                entity.c_create_date = item.F_CreatorTime;
                viewList.Add(entity);
            }
            return viewList;
        }

    }
}
