using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Distribution.Web.Models;
using Distribution.Model;
using Distribution.Logic;

namespace Distribution.Web.Controllers
{
    public class BasicController : Controller
    {
        public AgentInfoModel agentInfo { get; set; }
        public BasicController()
        {
            var UserInfo = NFine.Code.OperatorProvider.Provider.GetCurrent();
            ViewBag.UserId = "";
            ViewBag.User = "";
            agentInfo = new AgentInfoModel();
            if (UserInfo != null)
            {
                ViewBag.UserId = UserInfo.UserId;
                ViewBag.User = UserInfo.UserCode;
                Agent ag = AgentLogic.GetEnityById(UserInfo.UserId);
                if(ag == null)
                {
                    return;
                }
                agentInfo.agent = ag;
                AgentRelation ar = AgentRelationLogic.FindEntity(t => t.c_child_id == ag.c_id);
                if (ag.c_levle != null)
                {
                    agentInfo.Level = CommConfigLogic.GetValueFromConfig(1, ag.c_levle);
                }
                if (ag.c_agent_level != null)
                {
                    agentInfo.AgLevel = CommConfigLogic.GetValueFromConfig(2, ag.c_agent_level);
                }
                if (ar != null)
                {
                    agentInfo.RecomAgentName = AgentLogic.GetEnityById(ar.c_parent_id).c_name;
                }
                int totalScore = ScoreDetailLogic.GetTotalScore(ag.c_id);
                int secondCount = 0, otherCount = 0,expCount;

                agentInfo.FirstCount = AgentRelationLogic.GetFirstCount(ag.c_id,out secondCount, out otherCount,out expCount);
                agentInfo.SecondCount = secondCount;
                agentInfo.OtherCount = otherCount;
                agentInfo.ExpCount = expCount;
                agentInfo.TotalScore = totalScore.ToString();
                int dealingScore = ScoreCashLogic.GetTotalCashScoreByState(ag.c_id, CashScoreState.Dealing);
                agentInfo.CanCashScore = ((int)agentInfo.agent.c_score - dealingScore);
            }
        }
    }
}