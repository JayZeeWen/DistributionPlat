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
    public class HomeController : BasicController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserInfo(AgentInfoModel model)
        {
            var UserInfo = NFine.Code.OperatorProvider.Provider.GetCurrent();
            if (UserInfo == null)
            {
                return RedirectToAction("Login", "Account");
            }
            AgentInfoModel viewModel = new AgentInfoModel();
            if(base.agentInfo != null)
            {
                viewModel = agentInfo;
            }
            return View(viewModel);
        }

        public ActionResult ScoreManager(ScoreManageModel model)
        {
            var pageIndex = Request.QueryString["pageindex"];
            int index = 0 ;
            int pageSize = 15;
            Int32.TryParse(pageIndex, out index);
            if(index == 0 )
            {
                index = 1;
            }
            var UserInfo = NFine.Code.OperatorProvider.Provider.GetCurrent();
            if (UserInfo == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ScoreManageModel viewModel = new ScoreManageModel();
            if (base.agentInfo != null)
            {
                CommLogic.DeepClone<AgentInfoModel>(viewModel, agentInfo);
                List<ScoreDetail> list = ScoreDetailLogic.GetList().Where(t => t.c_user_id == agentInfo.agent.c_id).ToList();
                viewModel.scoreList = new PagerResult<ScoreDetail>();
                viewModel.scoreList.DataList = list.Skip<ScoreDetail>((index - 1) * pageSize).Take(pageSize).OrderByDescending(t => t.c_create_date);
                viewModel.scoreList.Code = 0;
                viewModel.scoreList.Total = list.Count();
                viewModel.scoreList.PageIndex = index;
                viewModel.scoreList.PageSize = pageSize;
                viewModel.scoreList.RequestUrl = "ScoreManager?pageindex=" + index;
            }
            return View(viewModel);
        }

        public ActionResult ScoreCashManager(ScoreCashModel model)
        {
            var pageIndex = Request.QueryString["pageindex"];
            int index = 0;
            int pageSize = 15;
            Int32.TryParse(pageIndex, out index);
            if (index == 0)
            {
                index = 1;
            }
            var UserInfo = NFine.Code.OperatorProvider.Provider.GetCurrent();
            if (UserInfo == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ScoreCashModel viewModel = new ScoreCashModel();
            if (base.agentInfo != null)
            {
                CommLogic.DeepClone<AgentInfoModel>(viewModel, agentInfo);
                List<ScoreCash> list = ScoreCashLogic.GetList().Where(t => t.c_user_id == agentInfo.agent.c_id).ToList();
                viewModel.cashList = new PagerResult<ScoreCash>();
                viewModel.cashList.DataList = list.Skip<ScoreCash>((index - 1) * pageSize).Take(pageSize).OrderByDescending(t => t.F_CreatorTime);
                viewModel.cashList.Code = 0;
                viewModel.cashList.Total = list.Count();
                viewModel.cashList.PageIndex = index;
                viewModel.cashList.PageSize = pageSize;
                viewModel.cashList.RequestUrl = "ScoreCashManager?pageindex=" + index;
            }
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
                sc.c_user_id = agentId.ToString();
                sc.c_cash_state = 0;
                sc.c_amount = amount;
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