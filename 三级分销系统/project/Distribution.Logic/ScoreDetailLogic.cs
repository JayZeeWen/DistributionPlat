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
    public static class ScoreDetailLogic
    {
        #region BasicMethod
        public static ScoreDetail GetEnityById(int id)
        {
            using (DistributionContext context = new DistributionContext())
            {
                return context.t_score_detail.Find(id);
            }
        }

        public static ScoreDetail FindEntity(Expression<Func<ScoreDetail, bool>> predicate)
        {
            return CommLogic.FindEntity<ScoreDetail>(predicate);
        }




        public static List<ScoreDetail> GetList()
        {
            List<ScoreDetail> list = new List<ScoreDetail>();
            using (DistributionContext context = new DistributionContext())
            {
                list = context.t_score_detail.ToList();
            }
            return list;
        }


        public static void InsertNewEntiy(ScoreDetail NewScoreDetail)
        {
            using (DistributionContext context = new DistributionContext())
            {
                context.t_score_detail.Add(NewScoreDetail);
                context.SaveChanges();
            }
        }

        public static void UpdateEntity(ScoreDetail UpdateScoreDetail)
        {
            using (DistributionContext context = new DistributionContext())
            {
                ScoreDetail ag = context.t_score_detail.Find(UpdateScoreDetail.c_id);
                CommLogic.DeepClone<ScoreDetail>(ag, UpdateScoreDetail);
                context.SaveChanges();
            }
        }

        public static void DeleteEntity(int ScoreDetailId)
        {
            using (DistributionContext context = new DistributionContext())
            {
                ScoreDetail DelScoreDetail = context.t_score_detail.Find(ScoreDetailId);
                context.t_score_detail.Remove(DelScoreDetail);
                context.SaveChanges();
            }
        } 
        #endregion

        public static int GetTotalScore(string agentId)
        {
            int result = 0;
            using (DistributionContext context = new DistributionContext ())
            {
                int? score  =  context.t_score_detail.Where(t => t.c_user_id == agentId && t.c_amount > 0).Sum(t => t.c_amount);
                if(score != null)
                {
                    result = (int)score;
                }
            }
            return result;
        }

    }
}
