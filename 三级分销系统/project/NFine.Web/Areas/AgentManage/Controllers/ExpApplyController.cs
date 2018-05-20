using Distribution.Logic;
using Distribution.Model;
/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: 分销平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity;
using NFine.Domain.Entity.AgentManage;
using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace NFine.Web.Areas.AgentManage.Controllers
{
    public class ExpApplyController : ControllerBase
    {
        private ExpApplyApp entityApp = new ExpApplyApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination)
        {
            var list = entityApp.GetViewList(pagination);
            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult CheckApply(string keyValue)
        {
            ExpApplyEntity entity = entityApp.GetForm(keyValue);

            AgentApp agApp = new AgentApp();
            AgentEntity ag = agApp.GetForm(entity.c_agent_id);

            //更新申请表
            entity.c_apply_state = 1;
            entityApp.SubmitForm(entity,keyValue);

            //更新代理商表
            
            ag.c_agnet_type = (int)AgentType.Fran;
            agApp.SubmitForm(ag, null, ag.F_Id);

            #region 推荐奖励
            //积分奖励
            ScoreLogic.DealRewardScore(ag.F_Id, RewartType.Recommend);

            Agent ag2 = AgentLogic.GetEnityById(entity.c_agent_id);
            ScoreLogic.DealProvinceReward(ag2);
            #endregion

            //扣减积分
            int score = Convert.ToInt32( CommConfigLogic.GetValueFromConfig((int)ConfigCategory.ScoreConfigCate, (int)RewardConfigKey.expLevelUpScore));            
            ScoreDetailLogic.UpdateAgentScore(entity.c_agent_id, -score, "体验店升级扣除");

            //升级
            AgentRelation ar = AgentRelationLogic.FindEntity(t => t.c_child_id == ag.F_Id);
            Agent recomm_ag = AgentLogic.GetEnityById(ar.c_parent_id);
            LevelLogic.IsLevelUpWithCondition(recomm_ag);

            #region 生成代理商订单
            Order order = new Order();
            order.c_agent_id = ag.F_Id;
            order.c_mobile = ag.c_mobile;
            order.c_state = (int)OrderState.NoDeliver;
            order.c_remark = "代理商订单";
            order.c_order_num = DateTime.Now.ToString("yyyyMMddHHmmss-") + Guid.NewGuid().ToString().Substring(0, 6);
            order.c_order_type = (int)OrderType.Agent;
            OrderLogic.InsertNewEntiy(order);
            #endregion
            

            return Success("审核成功。");
        }

        
    }
}
