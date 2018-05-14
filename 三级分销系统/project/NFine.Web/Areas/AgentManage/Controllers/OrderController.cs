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
    public class OrderController : ControllerBase
    {
        private OrderApp orderApp = new OrderApp();
        private CommConfigApp commApp = new CommConfigApp();
        private UserLogOnApp userLogOnApp = new UserLogOnApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string orderType)
        {
            int type = 0;
            int.TryParse(orderType, out type);
            var list = orderApp.GetViewList(pagination, type);
            var data = new
            {
                rows = list,
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
            var data = orderApp.GetViewForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(OrderEntity orderEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {
            orderEntity.c_state = (int)OrderState.HadDeal;
            orderApp.SubmitForm(orderEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            orderApp.DeleteForm(keyValue);
            return Success("删除成功。");
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
