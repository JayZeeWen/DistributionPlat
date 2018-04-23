
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Distribution.Logic;
using Distribution.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            //Agent ag = AgentLogic.GetEnityById(5);
            //ag.c_login_pwd = "233333";
            //AgentLogic.UpdateEntity(ag); 
            #endregion

            #region Delete
            //AgentLogic.DeleteEntity(7);
            #endregion
            //var list = ScoreLogic.DealProvinceReward(ag);
            var ag2 = AgentLogic.FindEntity(t => t.c_mobile == "15012341234");

        }
    }
}