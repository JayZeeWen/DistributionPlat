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
    public static class ScoreCashLogic
    {
        #region BasicMethod
        public static ScoreCash GetEnityById(string id)
        {
            using (DistributionContext context = new DistributionContext())
            {
                return context.t_score_cash.Find(id);
            }
        }

        public static ScoreCash FindEntity(Expression<Func<ScoreCash, bool>> predicate)
        {
            return CommLogic.FindEntity<ScoreCash>(predicate);
        }


        public static List<ScoreCash> GetList()
        {
            List<ScoreCash> list = new List<ScoreCash>();
            using (DistributionContext context = new DistributionContext())
            {
                list = context.t_score_cash.ToList();
            }
            return list;
        }

        public static void InsertNewEntiy(ScoreCash NewScoreCash)
        {
            using (DistributionContext context = new DistributionContext())
            {
                NewScoreCash.F_Id = Guid.NewGuid().ToString();
                context.t_score_cash.Add(NewScoreCash);
                context.SaveChanges();

            }
        }

        public static void UpdateEntity(ScoreCash UpdateScoreCash)
        {
            using (DistributionContext context = new DistributionContext())
            {
                ScoreCash ag = context.t_score_cash.Find(UpdateScoreCash.F_Id);
                CommLogic.DeepClone<ScoreCash>(ag, UpdateScoreCash);
                ag.F_LastModifyTime = DateTime.Now;
                context.SaveChanges();
            }
        }

        public static void DeleteEntity(int ScoreCashId)
        {
            using (DistributionContext context = new DistributionContext())
            {
                ScoreCash DelScoreCash = context.t_score_cash.Find(ScoreCashId);
                context.t_score_cash.Remove(DelScoreCash);
                context.SaveChanges();
            }
        }
        #endregion

        public static int GetTotalCashScoreByState(string agentId, CashScoreState state)
        {
            int result = 0;
            using (DistributionContext context = new DistributionContext())
            {
                var sum = context.t_score_cash.Where(t => t.c_user_id == agentId && t.c_cash_state == (int)state).Sum(t => t.c_amount);
                if (sum != null)
                {
                    result = (int)sum;
                }
            }
            return result;
        }
    }
}
