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
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = agentApp.GetList(pagination, keyword),
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
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitRevisePassword(string userPassword, string keyValue)
        {
            userLogOnApp.RevisePassword(userPassword, keyValue);
            return Success("重置密码成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DisabledAccount(string keyValue)
        {
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
            AgentEntity userEntity = new AgentEntity();
            userEntity.F_Id = keyValue;
            userEntity.c_state = 1;//0：未审核   1：审核通过
            agentApp.UpdateForm(userEntity);

            #region 推荐奖励

            //被推荐人
            Agent ag = AgentLogic.GetEnityById(keyValue);

            //积分奖励
            ScoreLogic.DealRewardScore(ag.c_id, RewartType.Recommend);
            ScoreLogic.DealProvinceReward(ag);


            //升级
            AgentRelation ar = AgentRelationLogic.FindEntity(t => t.c_child_id == ag.c_id);
            Agent recomm_ag = AgentLogic.GetEnityById(ar.c_parent_id);
            LevelLogic.IsLevelUpWithCondition(recomm_ag);
            #endregion

            return Success("账户启用成功。");
        }

        [HttpGet]
        public ActionResult Info()
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
