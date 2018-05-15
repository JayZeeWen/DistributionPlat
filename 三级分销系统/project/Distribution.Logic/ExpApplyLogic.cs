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
    public static class ExpApplyLogic
    {
        #region BasicMethod
        public static ExpApply GetEnityById(string id)
        {
            using (DistributionContext context = new DistributionContext())
            {
                return context.t_exp_apply.Find(id);
            }
        }

        public static ExpApply FindEntity(Expression<Func<ExpApply, bool>> predicate)
        {
            return CommLogic.FindEntity<ExpApply>(predicate);
        }


        public static List<ExpApply> GetList()
        {
            List<ExpApply> list = new List<ExpApply>();
            using (DistributionContext context = new DistributionContext())
            {
                list = context.t_exp_apply.ToList();
            }
            return list;
        }

        public static void InsertNewEntiy(ExpApply NewExpApply)
        {
            using (DistributionContext context = new DistributionContext())
            {
                NewExpApply.F_Id = Guid.NewGuid().ToString();
                NewExpApply.F_CreatorTime = DateTime.Now;
                context.t_exp_apply.Add(NewExpApply);
                context.SaveChanges();

            }
        }

        public static void UpdateEntity(ExpApply UpdateExpApply)
        {
            using (DistributionContext context = new DistributionContext())
            {
                ExpApply ag = context.t_exp_apply.Find(UpdateExpApply.F_Id);
                CommLogic.DeepClone<ExpApply>(ag, UpdateExpApply);
                context.SaveChanges();
            }
        }

        public static void DeleteEntity(string ExpApplyId)
        {
            using (DistributionContext context = new DistributionContext())
            {
                ExpApply DelExpApply = context.t_exp_apply.Find(ExpApplyId);
                context.t_exp_apply.Remove(DelExpApply);
                context.SaveChanges();
            }
        }
        #endregion


        public static bool HasApplyingRecord(string agentId)
        {
            bool result = false;
            using (DistributionContext context = new DistributionContext ())
            {
                var list = context.t_exp_apply.Where(t => t.c_agent_id == agentId && t.c_apply_state == 0 );
                if(list.Count() > 0)
                {
                    result = true;
                }
            }
            return result;
        }
       
    }
}
