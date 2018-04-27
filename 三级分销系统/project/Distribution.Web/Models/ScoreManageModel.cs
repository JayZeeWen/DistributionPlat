using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Distribution.Model;

namespace Distribution.Web.Models
{
    public class ScoreManageModel : AgentInfoModel
    {
        public PagerResult<ScoreDetail> scoreList { get; set; }
    }

    public class ScoreCashModel : AgentInfoModel
    {
        public PagerResult<ScoreCash> cashList { get; set; }
    }
}