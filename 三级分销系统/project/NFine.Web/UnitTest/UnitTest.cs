using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Distribution.Logic;
using Distribution.Model;
using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity;

namespace NFine.Web.UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            #region Insert
            //Agent newAg = new Agent();
            //newAg.c_name = "代理商E2";
            //newAg.c_state = 1;
            //newAg.c_levle = 1;
            //newAg.c_agent_level = 1;
            //AgentLogic.InsertNewEntiy(newAg);
            #endregion

            #region Update
            //Agent ag =  AgentLogic.GetEnityById(5);
            //ag.c_login_pwd = "233333";
            //AgentLogic.UpdateEntity(ag); 
            #endregion

            #region Delete
            //AgentLogic.DeleteEntity(7);
            #endregion



            OrderApp oApp = new OrderApp();
            OrderEntity order = new OrderEntity();
            order.c_agent_id = "9e234a73-1d70-4991-abfe-2886cc8f1cc8";
            order.c_mobile = "15077777777";
            order.c_state = (int)OrderState.NoDeliver;
            order.c_remark = "代理商订单";
            order.c_order_num = DateTime.Now.ToString("YYYYMMDDhhmmss");
            order.c_order_type = (int)OrderType.Agent;
            oApp.SubmitForm(order, "");
        }

        public void TestRewardAndLevelUp()
        {
            #region 推荐奖励

            //被推荐人
            Agent ag = AgentLogic.GetEnityById("5");

            //积分奖励
            ScoreLogic.DealRewardScore(ag.c_id, RewartType.Recommend);
            ScoreLogic.DealProvinceReward(ag);


            //升级
            AgentRelation ar = AgentRelationLogic.FindEntity(t => t.c_child_id == ag.c_id);
            Agent recomm_ag = AgentLogic.GetEnityById(ar.c_parent_id);
            LevelLogic.IsLevelUpWithCondition(recomm_ag);
            #endregion

        }
    }
}