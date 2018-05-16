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
using System.Linq;

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

        public int  GetTotalAgentScore()
        {
            AgentApp agentLogic = new AgentApp ();
            return agentLogic.GetTotalScore();
        }

        public void SumScore(out int fSum, out int sSum, out int dSum)
        {
            var list = service.FindList("select * from t_score_detail t where CONVERT(varchar(10),t.c_create_date,120) =  CONVERT(varchar(10),GETDATE()    ,120) ");
            //一代每日积分
            int firstSum = list.Where(t => t.c_reason.Contains("推荐") && t.c_reason.Contains("直推人")).Sum(t => t.c_amount);

            //二代每日积分
            int secondSum = list.Where(t => t.c_reason.Contains("推荐") && t.c_reason.Contains("二代")).Sum(t => t.c_amount);

            //部门每日积分
            int deptSum = list.Where(t => t.c_reason.Contains("推荐") && (t.c_reason.Contains("部门") || t.c_reason.Contains("二代外"))).Sum(t => t.c_amount);
            fSum = firstSum;
            sSum = secondSum;
            dSum = deptSum;
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
            var list = service.FindList(page);
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
                entity.c_create_date = item.c_create_date;
                viewList.Add(entity);
            }
            return viewList;
        }

    }
}
