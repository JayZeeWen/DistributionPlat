using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Distribution.Model;

namespace Distribution.Web.Models
{
    public class AgentInfoModel
    {
        public Agent agent { get; set; }

        /// <summary>
        /// 推荐人姓名
        /// </summary>
        public string RecomAgentName { get; set; }

        /// <summary>
        /// 总业绩
        /// </summary>
        public string TotalScore { get; set; }

        public string Level { get; set; }

        public string AgLevel { get; set; }

        /// <summary>
        /// 可折现积分
        /// </summary>
        public int CanCashScore { get; set; }
    }
}