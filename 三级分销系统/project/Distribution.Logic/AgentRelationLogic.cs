using System;
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
        public static AgentRelation GetEnityById(int id )
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

        public static void DeleteEntity(int AgentRelationId)
        {
            using (DistributionContext context = new DistributionContext())
            {
                AgentRelation DelAgentRelation = context.t_agent_relation.Find(AgentRelationId);
                context.t_agent_relation.Remove(DelAgentRelation);
                context.SaveChanges();
            }
        }
    }
}
