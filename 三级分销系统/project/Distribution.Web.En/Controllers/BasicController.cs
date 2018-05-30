using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Distribution.Web.En.Models;
using Distribution.Model;
using Distribution.Logic;
using NFine.Code;

namespace Distribution.Web.En.Controllers
{
    public class BasicController : Controller
    {
        public AgentInfoModel agentInfo { get; set; }
        public BasicController()
        {
            var UserInfo = NFine.Code.OperatorProvider.Provider.GetCurrent();
            ViewBag.UserId = "";
            ViewBag.User = "";
            if(agentInfo == null )
            {
                agentInfo = new AgentInfoModel();
                if (UserInfo != null)
                {
                    ViewBag.UserId = UserInfo.UserId;
                    ViewBag.User = UserInfo.UserCode;
                    Agent ag = AgentLogic.GetEnityById(UserInfo.UserId);
                    if (ag == null)
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
                    int fisrtCount = 0,  secondCount = 0, otherCount = 0, expCount = 0;
                    var readSessionAgent = OperatorProvider.Provider.GetAgentInfo( agentInfo.agent.c_id);
                    if (readSessionAgent != null )
                    {
                        fisrtCount = readSessionAgent.FirstCount;
                        secondCount = readSessionAgent.SecondCount;
                        otherCount = readSessionAgent.DeptCount;
                        expCount = readSessionAgent.ExpCount;
                    }
                    else
                    {
                        fisrtCount = AgentRelationLogic.GetFirstCount(ag.c_id, out secondCount, out otherCount, out expCount);
                        AgentInfo sInfo = new AgentInfo();
                        sInfo.AgeId = agentInfo.agent.c_id;
                        sInfo.FirstCount = fisrtCount;
                        sInfo.SecondCount = secondCount;
                        sInfo.DeptCount = otherCount;
                        sInfo.ExpCount = expCount;
                        OperatorProvider.Provider.AddCurrentAgentInfo(sInfo, agentInfo.agent.c_id);
                    }
                    agentInfo.FirstCount = fisrtCount;
                    agentInfo.SecondCount = secondCount;
                    agentInfo.OtherCount = otherCount;
                    agentInfo.ExpCount = expCount;
                    agentInfo.TotalScore = totalScore.ToString();
                    int dealingScore = ScoreCashLogic.GetTotalCashScoreByState(ag.c_id, CashScoreState.Dealing);
                    agentInfo.CanCashScore = ((int)agentInfo.agent.c_score - dealingScore);
                    agentInfo.expLevelUpScore = Convert.ToInt32(CommConfigLogic.GetValueFromConfig((int)ConfigCategory.ScoreConfigCate, (int)RewardConfigKey.expLevelUpScore)); ;
                    agentInfo.prodStartAmount = Convert.ToInt32(CommConfigLogic.GetValueFromConfig((int)ConfigCategory.ScoreConfigCate, (int)RewardConfigKey.productAmount));
                }
            }
            
        }
    }
}