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
using Distribution.Logic;


namespace NFine.Web.Areas.AgentManage.Controllers
{
    public class ScoreCashController : ControllerBase
    {
        private ScoreCashApp scoreApp = new ScoreCashApp();
        private CommConfigApp commApp = new CommConfigApp();
        private UserLogOnApp userLogOnApp = new UserLogOnApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = scoreApp.GetCashViewList(pagination),
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
            var data = scoreApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        //[HttpPost]
        //[HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        //public ActionResult SubmitForm(AgentEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        //{
        //    scoreApp.SubmitForm(userEntity, userLogOnEntity, keyValue);
        //    return Success("操作成功。");
        //}
        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            scoreApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
        
        //[HttpPost]
        //[HandlerAjaxOnly]
        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        //public ActionResult SubmitRevisePassword(string userPassword, string keyValue)
        //{
        //    userLogOnApp.RevisePassword(userPassword, keyValue);
        //    return Success("重置密码成功。");
        //}
        //[HttpPost]
        //[HandlerAjaxOnly]
        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        //public ActionResult DisabledAccount(string keyValue)
        //{
        //    AgentEntity userEntity = new AgentEntity();
        //    userEntity.F_Id = keyValue;
        //    userEntity.c_state = 0;//0：未审核   1：审核通过
        //    scoreApp.UpdateForm(userEntity);
        //    return Success("账户禁用成功。");
        //}
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DealScoreCash(string keyValue,int state)
        {
            ScoreCashEntity entity = scoreApp.GetForm(keyValue);
            entity.F_Id = keyValue;
            if(state ==  1 )
            {
                entity.c_cash_state = (int)Distribution.Model.CashScoreState.Succ;//0：未审核   1：审核通过
                ScoreDetailLogic.UpdateAgentScore(entity.c_user_id, -entity.c_amount, "积分提现");
            }
            else if(state == 2 )
            {
                entity.c_cash_state = (int)Distribution.Model.CashScoreState.Back;//0：未审核   1：审核通过
            }            
            scoreApp.UpdateForm(entity);
            return Success("处理成功。");
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

        
    }
}
