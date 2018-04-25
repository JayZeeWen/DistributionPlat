
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
            AgentRelation newAg = new AgentRelation();
            newAg.c_child_id = "03db06d5-4f9e-44e1-a448-027316ed3dcd";
            newAg.c_parent_id = "10";
            AgentRelationLogic.InsertNewEntiy(newAg);
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