using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Distribution.Web.En.Models;
using NFine.Domain.Entity.SystemSecurity;
using NFine.Application;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Application.SystemManage;
using NFine.Application.SystemSecurity;
using Distribution.Logic;
using Distribution.Model;
using System.Web.Services;
using System.IO;
using System.Configuration;

namespace Distribution.Web.En.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult CheckLogin(string username, string password, string authCode)
        {
            LogEntity logEntity = new LogEntity();
            logEntity.F_ModuleName = "系统登录";
            logEntity.F_Type = DbLogType.Login.ToString();
            try
            {
                if (Session["nfine_session_verifycode"].IsEmpty() || Md5.md5(authCode.ToLower(), 16) != Session["nfine_session_verifycode"].ToString())
                {
                    throw new Exception("Verification code error, please retype.");
                }

                Agent user = AgentLogic.CheckLogin(username, password);
                //UserEntity userEntity = new UserApp().CheckLogin(model.Mobile, model.Password);
                if (user != null)
                {
                    OperatorModel operatorModel = new OperatorModel();
                    operatorModel.UserId = user.c_id.ToString();
                    operatorModel.UserCode = user.c_mobile;
                    operatorModel.UserName = user.c_name;
                    //operatorModel.CompanyId = userEntity.F_OrganizeId;
                    //operatorModel.DepartmentId = userEntity.F_DepartmentId;
                    //operatorModel.RoleId = userEntity.F_RoleId;
                    operatorModel.LoginIPAddress = Net.Ip;
                    operatorModel.LoginIPAddressName = Net.GetLocation(operatorModel.LoginIPAddress);
                    operatorModel.LoginTime = DateTime.Now;
                    operatorModel.LoginToken = DESEncrypt.Encrypt(Guid.NewGuid().ToString());
                    operatorModel.IsSystem = false;

                    OperatorProvider.Provider.AddCurrent(operatorModel);
                    logEntity.F_Account = user.c_mobile;
                    logEntity.F_NickName = user.c_name;
                    logEntity.F_Result = true;
                    logEntity.F_Description = "登录成功";
                    new LogApp().WriteDbLog(logEntity);
                }
                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。" }.ToJson());
            }
            catch (Exception ex)
            {
                logEntity.F_Account = username;
                logEntity.F_NickName = username;
                logEntity.F_Result = false;
                logEntity.F_Description = "登录失败，" + ex.Message;
                new LogApp().WriteDbLog(logEntity);
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult CheckLoginForBack(string keyValue)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。" }.ToJson());
        }

        [WebMethod(EnableSession = true)]
        [HttpGet]
        public ActionResult GetAuthCode()
        {

            return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        }




        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Protcol()
        {
            return View();
        }


        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult Register(string agentName, string username, string password, string recMobile,string savePath,string agentType)
        {
            LogEntity logEntity = new LogEntity();
            logEntity.F_ModuleName = "系统登录";
            logEntity.F_Type = DbLogType.Login.ToString();
            try
            {
                int at = Convert.ToInt32(agentType);
                AgentLogic.CheckRegist(agentName, username, recMobile, password, savePath, at);
                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "注册成功。" }.ToJson());
            }
            catch (Exception ex)
            {
                logEntity.F_Account = username;
                logEntity.F_NickName = username;
                logEntity.F_Result = false;
                logEntity.F_Description = "注册失败，" + ex.Message;
                new LogApp().WriteDbLog(logEntity);
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
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
                string fileName = string.Format("{0}_{1}", DateTime.Now.ToString("HHmmss") + Guid.NewGuid().ToString().Substring(0, 4), hpf.FileName);
                string path = ConfigurationManager.AppSettings["SaveFilePath"];
                string datePath = DateTime.Now.ToString("yyyyMM") + "/";
                path += datePath;
                string basePath = path;
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }
                string fullPath = basePath + fileName;
                hpf.SaveAs(fullPath);
                return Content(new AjaxResult { state = ResultType.success.ToString(), message = datePath + fileName }.ToJson());
            }
            catch (Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
                throw;
            }
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            new LogApp().WriteDbLog(new LogEntity
            {
                F_ModuleName = "系统登录",
                F_Type = DbLogType.Exit.ToString(),
                F_Account = OperatorProvider.Provider.GetCurrent().UserCode,
                F_NickName = OperatorProvider.Provider.GetCurrent().UserName,
                F_Result = true,
                F_Description = "安全退出系统",
            });
            Session.Abandon();
            Session.Clear();
            OperatorProvider.Provider.RemoveCurrent();
            var userInfo = NFine.Code.OperatorProvider.Provider.GetCurrent();
            OperatorProvider.Provider.RemoveAgentInfo(userInfo.UserId);
            return RedirectToAction("Login", "Account");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region 帮助程序
        // 用于在添加外部登录名时提供 XSRF 保护
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}