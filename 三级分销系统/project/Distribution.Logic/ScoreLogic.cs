using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Distribution.DB;
using Distribution.Model;

namespace Distribution.Logic
{
    

    public static class ScoreLogic
    {
        /// <summary>
        /// 处理积分奖励逻辑
        /// </summary>
        /// <param name="AgentId">代理商（被推荐人或是产品购买人）</param>
        /// <param name="reType">奖励类型：推荐奖励   购买奖励</param>
        /// <returns></returns>
        public static bool DealRewardScore( int AgentId, RewartType reType)
        {
            bool result = true;
            using (DistributionContext context = new DistributionContext())
            {
                int RecommId = context.t_agent_relation.Where(c => c.c_child_id == AgentId).First().ParentAgent.c_id;
                int firsSc;//直推奖励
                int secoSc;//二代奖励
                string desc = "";
                if (reType == RewartType.Recommend)
                {
                    firsSc = 3500;
                    secoSc = 1000;
                    desc = "推荐";
                }
                else
                {
                    firsSc = 75;
                    secoSc = 25;
                    desc = "购买";
                }

                //1、直推人更新积分
                UpdateAgentScore(RecommId, firsSc, context, "直推人【" + desc + "】积分奖励");

                //2、二代更新积分
                var sec_list = context.t_agent_relation.Where(c => c.c_child_id == RecommId);
                if (sec_list.Count() == 0)//若没有二代则直接返回
                {
                    return result;
                }
                Agent SeconAgent = sec_list.FirstOrDefault().ParentAgent;
                UpdateAgentScore(SeconAgent.c_id, secoSc, context, "二代【" + desc + "】积分奖励");

                #region 二代外积分更改逻辑
                int maxLevel = GetMaxLevel(AgentId, context);
                var list_Config = context.t_level_config.Where(f => f.c_level == maxLevel && f.c_is_delete == 0);
                if (maxLevel == 1 || list_Config.Count() == 0)
                {
                    return result;
                }
                int rewardScore = (int)list_Config.First().c_recomm_reward;
                if (reType == RewartType.Purchase)
                {
                    rewardScore = (int)list_Config.First().c_buy_reward;
                }

                if (rewardScore > 0)
                {
                    RewardForCorreLevel(SeconAgent, ref rewardScore, reType, context);
                }

                #endregion

            }
            return result;
        }

        /// <summary>
        /// 省级代理奖励
        /// </summary>
        /// <param name="BeRecommAgent">被推荐代理商</param>
        /// <returns></returns>
        public static bool DealProvinceReward(Agent BeRecommAgent)
        {

            bool result = true;
            int ProReward = 100;
            using (DistributionContext context = new DistributionContext())
            {
                RewardForProvice(BeRecommAgent, ProReward, context); 
            }
            return true;
        }
        /// <summary>
        /// 更新代理商积分并增加积分明细记录
        /// </summary>
        /// <param name="AgentId"></param>
        /// <param name="ChangeScore"></param>
        /// <param name="context"></param>
        /// <param name="reason"></param>
        public static void UpdateAgentScore(int AgentId, int ChangeScore,DistributionContext context = null,string reason = "操作变更")
        {
            if(context == null)
            {
                context = new DistributionContext();
            }
            Agent ag = context.t_agent.Find(AgentId);
            ag.c_score = ag.c_score + ChangeScore;

            ScoreDetail sd = new ScoreDetail();
            sd.c_amount = ChangeScore;
            sd.c_reason = reason;
            sd.c_user_id = AgentId;
            sd.c_create_date = DateTime.Now;
            context.t_score_detail.Add(sd);
            context.SaveChanges();
        }

        /// <summary>
        /// (递归)找到代理商的所有上级中的最高级别
        /// </summary>
        /// <param name="AgentId"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static int GetMaxLevel(int AgentId,DistributionContext context = null )
        {
            if(context == null )
            {
                context = new DistributionContext();
            }
            int max_Level = 1;
            int parLevel = 0;
            var list = context.t_agent_relation.Where(f => f.c_child_id == AgentId);
            if(list.Count() == 0 )
            {
                return (int)context.t_agent.Find(AgentId).c_levle;
            }
            max_Level = (int)list.First().ParentAgent.c_levle;
            parLevel = GetMaxLevel(list.First().ParentAgent.c_id, context);
            max_Level = parLevel > max_Level ? parLevel : max_Level;
            return max_Level;
        }

        /// <summary>
        /// (递归)相应等级奖励相应积分（按照极差制度） 极差制度，上级奖励= 总奖励 - 下级奖励
        /// </summary>
        /// <param name="ParAgent"></param>
        /// <param name="RewardScore"></param>
        /// <param name="reType"></param>
        /// <param name="context"></param>
        private static void RewardForCorreLevel(Agent ParAgent,ref int RewardScore,RewartType reType, DistributionContext context)
        {
            string desc = "";
            var list = context.t_agent_relation.Where(f => f.c_child_id == ParAgent.c_id);
            if(list.Count() == 0 )
            {
                return;
            }
            Agent ag = list.First().ParentAgent;//上级代理
            var list_config = context.t_level_config.Where(f => f.c_level == ag.c_levle && f.c_is_delete == 0 );
            int needReward = 0;//等级奖励
            if (list_config.Count() != 0 )
            {
                if(reType == RewartType.Recommend)//推荐代理商奖励
                {
                    needReward = (int)list_config.First().c_recomm_reward;
                    desc = "推荐";
                }
                else//购买产品奖励
                {
                    needReward = (int)list_config.First().c_buy_reward;
                    desc = "购买";
                }
                
            }
            needReward = Math.Min(RewardScore, needReward);
            if(needReward != 0 )
            {
                UpdateAgentScore(ag.c_id, needReward, context, "二代外【"+ desc +"】积分奖励");
                RewardScore -= needReward;//极差制度，上级奖励= 总奖励 - 下级奖励
            }            
            RewardForCorreLevel(ag,ref RewardScore, reType, context);

        }

        private static void RewardForProvice(Agent BeRecommAgent, int ProReward, DistributionContext context)
        {
            var list = context.t_agent_relation.Where(f => f.c_child_id == BeRecommAgent.c_id);
            if (list.Count() == 0)
            {
                return;
            }
            Agent parAgent = list.First().ParentAgent;
            if(parAgent.c_agent_level == (int)AgentLevel.ProvieceAgent)
            {
                UpdateAgentScore(parAgent.c_id, ProReward, context, "省代理部门推荐奖励");
                return;
            }
            RewardForProvice(parAgent, ProReward, context);
        }

        
    }

}
