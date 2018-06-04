using NFine.Code;
using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web
{
    [HandlerLogin]
    public abstract class ControllerBase : Controller
    {
        public Log FileLog
        {
            get { return LogFactory.GetLogger(this.GetType().ToString()); }
        }
        [HttpGet]
        [HandlerAuthorize]
        public virtual ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [HandlerAuthorize]
        public virtual ActionResult Form()
        {
            return View();
        }
        [HttpGet]
        [HandlerAuthorize]
        public virtual ActionResult Details()
        {
            return View();
        }
        protected virtual ActionResult Success(string message)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message }.ToJson());
        }
        protected virtual ActionResult Success(string message, object data)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message, data = data }.ToJson());
        }
        protected virtual ActionResult Error(string message)
        {
            return Content(new AjaxResult { state = ResultType.error.ToString(), message = message }.ToJson());
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
                return Content(new AjaxResult { state = ResultType.success.ToString(), message = fileName }.ToJson());
            }
            catch (Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
                throw;
            }
        }
    }
}
