using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Distribution.Model;
using Distribution.Logic;

namespace Distribution.Web.En.Models
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

        public int FirstCount { get; set; }

        public int SecondCount { get; set; }

        public int OtherCount { get; set; }

        public int ExpCount { get; set; }
        /// <summary>
        /// 是否已体验
        /// </summary>
        public int HadExp { get; set; }

        public int prodStartAmount { get; set; }

        public int expLevelUpScore { get; set; }
    }

    public class AgentJsonModel
    {
        public string id {get;set;}


        public string mobile { get; set; }

        public string name { get; set; }

        public string level{get;set;}

        public List<AgentJsonModel> secondList { get; set; }

        public string gender { get; set; }

    }

    public static class AgentHelper
    {
        public static void setJsonModelFromEntity(AgentJsonModel jModel , Agent agent)
        {
            jModel.id = agent.c_id;
            jModel.name = agent.c_name;
            jModel.mobile = agent.c_mobile;
            jModel.level = CommConfigLogic.GetValueFromConfig((int)ConfigCategory.PostitionLevel, agent.c_levle);
        }

        public static List<AgentJsonModel> getJsonListFromEntityList( List<Agent> agetnList)
        {
            var list = new List<AgentJsonModel>();
            foreach (var item in agetnList)
            {
                AgentJsonModel m = new AgentJsonModel();
                setJsonModelFromEntity(m, item);
                list.Add(m);
            }
            return list;
        }
    }


}