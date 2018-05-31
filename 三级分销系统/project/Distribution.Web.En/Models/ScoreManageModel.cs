using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Distribution.Model;

namespace Distribution.Web.En.Models
{
    public class ScoreManageModel : AgentInfoModel
    {
        public PagerResult<ScoreDetail> scoreList { get; set; }
    }

    public class ScoreCashModel : AgentInfoModel
    {
        public PagerResult<ScoreCash> cashList { get; set; }
    }

    public class ProductModel : AgentInfoModel
    {
        public PagerResult<Product> productList { get; set; }
    }

    public class OrderModel : AgentInfoModel
    {
        public PagerResult<Order> orderList { get; set; }
    }

    public class CustRelationModel : AgentInfoModel
    {
        public List<AgentJsonModel> fistList { get; set; }
    }
}