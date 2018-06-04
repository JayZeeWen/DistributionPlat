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
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace NFine.Web.Areas.AgentManage.Controllers
{
    public class ConfigController : ControllerBase
    {
        private CommConfigApp configApp = new CommConfigApp();
        private LevelConfigApp levelApp = new LevelConfigApp();
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var list = configApp.GetList(pagination, ((int)ConfigCategory.ScoreConfigCate), keyword);
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
            var data = configApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(CommConfigEntity commEntity, string keyValue)
        {
            configApp.SubmitForm(commEntity, keyValue);
            return Success("操作成功。");
        }


        public ActionResult LevelConfigIndex()
        {
            return View();
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetLevelJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = levelApp.GetItemList(pagination),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        public ActionResult LevelForm()
        {
            return View();
        }


        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetLevelFormJson(string keyValue)
        {
            var data = levelApp.GetDetailForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitLevelForm(LevelConfigEntity commEntity, string keyValue)
        {
            levelApp.SubmitForm(commEntity, keyValue);
            return Success("操作成功。");
        }

        [HttpGet]
        [HandlerAuthorize]
        public virtual ActionResult HPConfig()
        {
            return View();
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetHPConfigJson(Pagination pagination, string keyword)
        {
            var list = configApp.GetList(pagination, ((int)ConfigCategory.HPConfig), keyword);
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
        [HandlerAuthorize]
        public virtual ActionResult HPConfigForm()
        {
            return View();
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetHPConfigFormJson(string keyValue)
        {
            var data = configApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitHPConfigForm(CommConfigEntity commEntity, string keyValue)
        {
            configApp.SubmitForm(commEntity, keyValue);
            return Success("操作成功。");
        }

       
    }
}
