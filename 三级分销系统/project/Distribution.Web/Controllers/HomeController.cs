using NFine.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Distribution.Model;
using Distribution.Logic;
using Distribution.Web.Models;

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
        public ActionResult UserInfo(AgentInfoModel model)
        {
            var UserInfo = NFine.Code.OperatorProvider.Provider.GetCurrent();
            ViewBag.User = "";
            
            if (UserInfo == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.User = UserInfo.UserCode;
            

            Agent ag = AgentLogic.GetEnityById(UserInfo.UserId);
            AgentInfoModel viewModel = new AgentInfoModel();
            viewModel.agent = ag;
            AgentRelation ar = AgentRelationLogic.FindEntity(t => t.c_child_id == ag.c_id);
            if(ag.c_levle != null)
            {
                viewModel.Level = CommConfigLogic.GetValueFromConfig(1, ag.c_levle);
            }
            if(ag.c_agent_level != null )
            {
                viewModel.AgLevel = CommConfigLogic.GetValueFromConfig(2, ag.c_agent_level);
            }
            if(ar != null)
            {
                viewModel.RecomAgentName = AgentLogic.GetEnityById(ar.c_parent_id).c_name;
            }
            viewModel.TotalScore = ScoreDetailLogic.GetTotalScore(ag.c_id).ToString();
                ;
            return View(viewModel);
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

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult ScoreCash(string agentId ,int amount,string bankAccount ,string bankPerson,string bankName)
        {
            AjaxResult result = new AjaxResult();
            try
            {
                ScoreCash sc = new ScoreCash();
                sc.F_Id = Guid.NewGuid().ToString();
                sc.c_user_id = agentId.ToString();
                sc.c_cash_state = 0;
                sc.c_amount = 100;
                sc.c_bank_name = bankName;
                sc.c_bank_person = bankPerson;
                sc.c_bank_account = bankAccount;
                sc.F_CreatorTime = DateTime.Now;
                ScoreCashLogic.InsertNewEntiy(sc);
                result.state = ResultType.success.ToString();
                result.message = "成功";
                return Content(result.ToJson());
            }
            catch (Exception ex)
            {
                result.state = ResultType.error.ToString();
                result.message = string.Format("提交失败({0})",ex.Message);
                return Content(result.ToJson());
                throw;
            }
        }
    }
}