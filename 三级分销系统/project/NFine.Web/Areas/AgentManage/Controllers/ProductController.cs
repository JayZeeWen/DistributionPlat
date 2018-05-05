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
using System.Web;
using System.Configuration;
using System.IO;

namespace NFine.Web.Areas.AgentManage.Controllers
{
    public class ProductController : ControllerBase
    {
        private ProductApp productApp = new ProductApp();
        private CommConfigApp commApp = new CommConfigApp();
        private UserLogOnApp userLogOnApp = new UserLogOnApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = productApp.GetList(pagination, keyword),
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
            var data = productApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            ProductEntity entity = productApp.GetForm(keyValue);
            entity.F_DeleteMark = true;
            productApp.SubmitForm(entity, keyValue);
            return Success("下架成功。");
        }

        [HttpGet]
        public ActionResult Info()
        {
            return View();
        }

        public ActionResult SubmitForm(ProductEntity entity, UserLogOnEntity userLogOnEntity, string keyValue)
        {   
            productApp.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        public ActionResult UploadFile()
        {
            try
            {
                HttpPostedFileBase hpf = Request.Files["fileUpload"]; //主要是这个地方获取到值就没问题了  
                if (hpf.ContentLength == 0)
                {
                    throw new Exception("未找到上传的文件");
                }
                string fileName = string.Format("{0}_{1}", DateTime.Now.ToString("yyyyMMddHHmmss") + Guid.NewGuid().ToString().Substring(0, 4), hpf.FileName);
                string path = ConfigurationManager.AppSettings["ProImgSavePath"];
                path = Server.MapPath(path);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fullPath = path + fileName;
                hpf.SaveAs(fullPath);
                return Content(new AjaxResult { state = ResultType.success.ToString(), message =  fileName }.ToJson());
            }
            catch (Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
                throw;
            }
        }


    }
}
