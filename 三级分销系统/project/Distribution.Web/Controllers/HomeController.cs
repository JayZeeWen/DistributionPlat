using NFine.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Distribution.Model;
using Distribution.Logic;

namespace Distribution.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var UserInfo = NFine.Code.OperatorProvider.Provider.GetCurrent();
            ViewBag.User = "";
            if (UserInfo != null)
            {
                ViewBag.User = UserInfo.UserCode; 
            }

            return View();
        }
        public ActionResult UserInfo(Agent model)
        {
            var UserInfo = NFine.Code.OperatorProvider.Provider.GetCurrent();
            ViewBag.User = "";
            ViewBag.RecomAgent = "";
            ViewBag.TotalScore = 0;
            ViewBag.Level= "";
            ViewBag.AgLevel = "";
            if (UserInfo == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.User = UserInfo.UserCode;
            

            Agent ag = AgentLogic.GetEnityById(UserInfo.UserId);
            AgentRelation ar = AgentRelationLogic.FindEntity(t => t.c_child_id == ag.c_id);
            if(ag.c_levle != null)
            {
                ViewBag.AgLevel = CommConfigLogic.GetValueFromConfig(1, ag.c_levle);
            }
            if(ag.c_agent_level != null )
            {
                ViewBag.AgLevel = CommConfigLogic.GetValueFromConfig(2, ag.c_agent_level);
            }
            if(ar != null)
            {
                ViewBag.RecomAgent = AgentLogic.GetEnityById(ar.c_parent_id).c_name;
            }
            return View(ag);
        }


        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitInfo(string agentId, string realName, string address, string addressee, string tel, string bank_aXX, string bank_holder, string bank_num, string aliAccount)
        {
            AjaxResult result = new AjaxResult();
            Agent ag = AgentLogic.GetEnityById(agentId);
            if(ag == null)
            {
                result.state = ResultType.error.ToString();
                result.message = "未找到代理商";
            }
            ag.c_name = realName;
            ag.c_address = address;
            ag.c_rec_person = addressee;
            ag.c_rec_mobile = tel;
            ag.c_bank_name = bank_aXX;
            ag.c_bank_person = bank_holder;
            ag.c_bank_account = bank_num;
            ag.c_ali_account = aliAccount;
            ag.c_state = 2;
            AgentLogic.UpdateEntity(ag);
            result.state = ResultType.success.ToString();
            result.message = "成功";
            return Content(result.ToJson());
        }
    }
}