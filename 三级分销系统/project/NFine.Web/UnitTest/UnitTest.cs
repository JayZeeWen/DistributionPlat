using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Distribution.DB;
using ComHelper;

namespace NFine.Web.UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (DistributionContext context = new DistributionContext ())
            {
                ScoreLogic.DealRewardScore(context, 5, RewartType.Recommend);
            }
        }
    }
}