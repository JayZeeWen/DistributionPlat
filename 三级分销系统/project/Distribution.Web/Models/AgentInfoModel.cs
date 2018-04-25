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

        public string RecomAgentName { get; set; }

        public string TotalScore { get; set; }

        public string Level { get; set; }

        public string AgLevel { get; set; }
    }
}