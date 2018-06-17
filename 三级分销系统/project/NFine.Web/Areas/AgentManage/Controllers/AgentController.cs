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
    public class AgentController : ControllerBase
    {
        private AgentApp agentApp = new AgentApp();
        private CommConfigApp commApp = new CommConfigApp();
        private UserLogOnApp userLogOnApp = new UserLogOnApp();

        [HttpGet]
        [HandlerAuthorize]
        public override ActionResult Index()
        {
            var lsit =  AgentLogic.GetList();
            ViewBag.TotalCount = lsit.Count();
            ViewBag.TodayCount = lsit.Where(t => t.c_create_date >= DateTime.Now.Date).Count();
            return View();
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword, string level, string agentLevel, string   state)
        {
            int l, al,st;
            int.TryParse(level, out l);
            int.TryParse(agentLevel, out al);
            if(string.IsNullOrEmpty(state))
            {
                st = -1;
            }
            else
            {
                int.TryParse(state, out st);
            }
            
            var list = agentApp.GetViewList(pagination, keyword, l, al, st);
            var data = new
            {
                rows = list ,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = agentApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(AgentEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {

            var orgAge = agentApp.GetForm(keyValue);
            if (orgAge != null && userEntity.c_score != orgAge.c_score)
            {
                int changeScore = (int)(userEntity.c_score - orgAge.c_score);
                ScoreDetailLogic.UpdateAgentScore(keyValue,changeScore, "管理员后台变更积分");
            }            
            agentApp.SubmitForm(userEntity, userLogOnEntity, keyValue);
            
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitScore(string keyValue ,int score ,string reason )
        {
            var orgAge = agentApp.GetForm(keyValue);
            ScoreDetailLogic.UpdateAgentScore(keyValue, score, reason);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            var orgAge = agentApp.GetForm(keyValue);
            int firstCount =  AgentRelationLogic.GetFirstCustomer(keyValue).Count();
            if (orgAge.c_state != 0 || firstCount > 0 )
            {
                return Error("删除失败，审核已通过或伞下有会员。");
            }
            agentApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
        [HttpGet]
        public ActionResult RevisePassword()
        {
            return View();
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitRevisePassword(string userPassword, string keyValue)
        {
            userLogOnApp.RevisePassword(userPassword, keyValue);
            Agent ag = AgentLogic.GetEnityById(keyValue);
            ag.c_login_pwd = userPassword;
            AgentLogic.UpdateEntity(ag);
            return Success("重置密码成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DisabledAccount(string keyValue)
        {
            int firstCount = AgentRelationLogic.GetFirstCustomer(keyValue).Count();
            if ( firstCount > 0)
            {
                return Error("禁用失败，伞下有会员。");
            }
            AgentEntity userEntity = new AgentEntity();
            userEntity.F_Id = keyValue;
            userEntity.c_state = 0;//0：未审核   1：审核通过
            agentApp.UpdateForm(userEntity);
            return Success("账户禁用成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult EnabledAccount(string keyValue)
        {
            bool hadReward = agentApp.hadReward(keyValue);
            AgentEntity userEntity = new AgentEntity();
            userEntity.F_Id = keyValue;
            userEntity.c_state = 1;//0：未审核   1：审核通过
            userEntity.c_had_reward = true;
            agentApp.UpdateForm(userEntity);

            Agent ag = AgentLogic.GetEnityById(keyValue);

            //升级
            AgentRelation ar = AgentRelationLogic.FindEntity(t => t.c_child_id == ag.c_id);
            Agent recomm_ag = AgentLogic.GetEnityById(ar.c_parent_id);
            LevelLogic.IsLevelUpWithCondition(recomm_ag);
            if (!hadReward)
            {
                #region 推荐奖励
                //积分奖励
                ScoreLogic.DealRewardScore(ag.c_id, RewartType.Recommend);
                
                #endregion

                if(ag.c_agnet_type != (int)AgentType.Exp)//体验店计算上下级奖励即可
                {

                    #region 生成代理商订单
                    Order order = new Order();
                    order.c_agent_id = ag.c_id;
                    order.c_mobile = ag.c_mobile;
                    order.c_state = (int)OrderState.NoDeliver;
                    order.c_remark = "代理商订单";
                    order.c_order_num = DateTime.Now.ToString("yyyyMMddHHmmss-") + Guid.NewGuid().ToString().Substring(0, 6);
                    order.c_order_type = (int)OrderType.Agent;
                    OrderLogic.InsertNewEntiy(order);
                    #endregion
                }
                
            }


            return Success("审核成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult expAgent(string keyValue)
        {
            AgentEntity entity = agentApp.GetForm(keyValue);
            entity.c_exp_state = 1;
            agentApp.UpdateForm(entity);
            return Success("更改成功。");
        }

        [HttpGet]
        public ActionResult Info()
        {
            return View();
        }

        public ActionResult ChangeScore()
        {
            return View();
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetLevelJson(int enCode)
        {

            var data = CommConfigLogic.GetConfigListByCate(enCode);
            List<object> list = new List<object>();
            foreach (var item in data)
            {
                list.Add(new { id = item.c_key, text = item.c_value });
            }
            return Content(list.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult LoginForward(string keyValue)
        {
            AgentEntity data = agentApp.GetForm(keyValue);

            OperatorModel operatorModel = new OperatorModel();
            operatorModel.UserId = data.F_Id.ToString();
            operatorModel.UserCode = data.c_mobile;
            operatorModel.UserName = data.c_name;
            operatorModel.LoginIPAddress = Net.Ip;
            operatorModel.LoginIPAddressName = Net.GetLocation(operatorModel.LoginIPAddress);
            operatorModel.LoginTime = DateTime.Now;
            operatorModel.LoginToken = DESEncrypt.Encrypt(Guid.NewGuid().ToString());
            operatorModel.IsSystem = false;
            OperatorProvider.Provider.AddCurrent(operatorModel, Configs.GetValue("LoginForwardKey"));
            
            return Success("账户登陆成功。");
        }
        
    }
}
