﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Distribution.DB;
using Distribution.Model;
using System.Linq.Expressions;

namespace Distribution.Logic
{
    public static class AgentRelationLogic
    {
        public static AgentRelation GetEnityById(string  id )
        {
            using (DistributionContext context = new DistributionContext())
            {
                return  context.t_agent_relation.Find(id);
            }
        }

        public static AgentRelation FindEntity(Expression<Func<AgentRelation, bool>> predicate)
        {
            return CommLogic.FindEntity<AgentRelation>(predicate);
        }
        public static List<AgentRelation> GetList()
        {
            List<AgentRelation> list = new List<AgentRelation>();
            using (DistributionContext context = new DistributionContext())
            {
                list = context.t_agent_relation.ToList();
            }
            return list;
        }

        public static void InsertNewEntiy(AgentRelation NewAgentRelation)
        {
            using (DistributionContext context = new DistributionContext ())
            {
                NewAgentRelation.c_id = Guid.NewGuid().ToString();
                context.t_agent_relation.Add(NewAgentRelation);
                context.SaveChanges();
            }
        }

        public static void UpdateEntity(AgentRelation UpdateAgentRelation)
        {
            using (DistributionContext context = new DistributionContext())
            {
                AgentRelation ag = context.t_agent_relation.Find(UpdateAgentRelation.c_id);
                CommLogic.DeepClone<AgentRelation>(ag, UpdateAgentRelation);
                context.SaveChanges();
            }
        }

        public static void DeleteEntity(string AgentRelationId)
        {
            using (DistributionContext context = new DistributionContext())
            {
                AgentRelation DelAgentRelation = context.t_agent_relation.Find(AgentRelationId);
                context.t_agent_relation.Remove(DelAgentRelation);
                context.SaveChanges();
            }
        }

        public static int GetFirstCount(string agentId,out int secondCount ,out int otherCount)
        {
            using (DistributionContext context = new DistributionContext ())
            {
                var list = context.t_agent_relation.ToList();
                var firstList = list.Where(t => t.c_parent_id == agentId && t.ChildrenAgent != null).ToList();
                var pIds = firstList.Select(t => t.c_child_id).ToList();
                var secondList = list.Where(t => pIds.Contains(t.c_parent_id) && t.ChildrenAgent != null).ToList();
                secondCount = secondList.Count();//二代数量
                var otherParentIds = secondList.Select(t => t.c_child_id).ToList();
                otherCount = list.Where(t => otherParentIds.Contains(t.c_parent_id) && t.ChildrenAgent != null).Count();
                return firstList.Count();
            }
            
        }

        public static List<Agent> GetFirstCustomer(string agentId)
        {
            using (DistributionContext context = new DistributionContext ())
            {
                var list = context.t_agent_relation.Where(t => t.c_parent_id == agentId).ToList();
                return list.Select(t => t.ChildrenAgent).ToList();
                
            }
        }
    }
}
