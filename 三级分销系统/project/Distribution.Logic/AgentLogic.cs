using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Distribution.DB;
using Distribution.Model;

namespace Distribution.Logic
{
    public static class AgentLogic
    {
        public static Agent GetEnityById(int id )
        {
            using (DistributionContext context = new DistributionContext())
            {
                return  context.t_agent.Find(id);
            }
        }

        public static List<Agent> GetList()
        {
            List<Agent> list = new List<Agent>();
            using (DistributionContext context = new DistributionContext())
            {
                list = context.t_agent.ToList();
            }
            return list;
        }

        public static void InsertNewEntiy(Agent NewAgent)
        {
            using (DistributionContext context = new DistributionContext ())
            {
                context.t_agent.Add(NewAgent);
                context.SaveChanges();
            }
        }

        public static void UpdateEntity(Agent UpdateAgent)
        {
            using (DistributionContext context = new DistributionContext())
            {
                Agent ag = context.t_agent.Find(UpdateAgent.c_id);
                CommLogic.DeepClone<Agent>(ag, UpdateAgent);
                context.SaveChanges();
            }
        }

        public static void DeleteEntity(int AgentId)
        {
            using (DistributionContext context = new DistributionContext())
            {
                Agent DelAgent = context.t_agent.Find(AgentId);
                context.t_agent.Remove(DelAgent);
                context.SaveChanges();
            }
        }
    }
}
