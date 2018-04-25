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
        public static ScoreCash GetEnityById(int id)
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
                try
                {
                    NewScoreCash.F_CreatorTime = DateTime.Now;
                    NewScoreCash.F_CreatorUserId = "999";
                    NewScoreCash.F_DeleteMark = "0";
                    NewScoreCash.F_DeleteTime = DateTime.Now;
                    NewScoreCash.F_DeleteUserId = "999";
                    NewScoreCash.F_LastModifyUserId = "999";
                    NewScoreCash.F_LastModifyTime = DateTime.Now;
                    context.t_score_cash.Add(NewScoreCash);


                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    string s = e.Message;
                    throw;
                }
            }
        }

        public static void UpdateEntity(ScoreCash UpdateScoreCash)
        {
            using (DistributionContext context = new DistributionContext())
            {
                ScoreCash ag = context.t_score_cash.Find(UpdateScoreCash.F_Id);
                CommLogic.DeepClone<ScoreCash>(ag, UpdateScoreCash);
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

        public static int GetTotalCashScoreByState(string agentId,CashScoreState state)
        {
            int result = 0;
            using (DistributionContext context = new DistributionContext ())
            {
               var sum  =  context.t_score_cash.Where(t => t.c_user_id == agentId &&  t.c_cash_state == (int)state).Sum(t => t.c_amount);
                if(sum != null)
                {
                    result = (int)sum;
                }
            }
            return result;
        }
    }
}
