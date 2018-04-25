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
    public static class CommConfigLogic
    {
        #region BasicMethod
        public static CommConfig GetEnityById(string  id)
        {
            using (DistributionContext context = new DistributionContext())
            {
                return context.t_common_config.Find(id);
            }
        }

        public static CommConfig FindEntity(Expression<Func<CommConfig, bool>> predicate)
        {
            return CommLogic.FindEntity<CommConfig>(predicate);
        }


        public static List<CommConfig> GetList()
        {
            List<CommConfig> list = new List<CommConfig>();
            using (DistributionContext context = new DistributionContext())
            {
                list = context.t_common_config.ToList();
            }
            return list;
        }

        public static void InsertNewEntiy(CommConfig NewCommConfig)
        {
            using (DistributionContext context = new DistributionContext())
            {
                NewCommConfig.c_id = Guid.NewGuid().ToString();
                context.t_common_config.Add(NewCommConfig);
                context.SaveChanges();
            }
        }

        public static void UpdateEntity(CommConfig UpdateCommConfig)
        {
            using (DistributionContext context = new DistributionContext())
            {
                CommConfig ag = context.t_common_config.Find(UpdateCommConfig.c_id);
                CommLogic.DeepClone<CommConfig>(ag, UpdateCommConfig);
                context.SaveChanges();
            }
        }

        public static void DeleteEntity(string CommConfigId)
        {
            using (DistributionContext context = new DistributionContext())
            {
                CommConfig DelCommConfig = context.t_common_config.Find(CommConfigId);
                context.t_common_config.Remove(DelCommConfig);
                context.SaveChanges();
            }
        } 
        #endregion

        public static string GetValueFromConfig(int categoryId,int? key)
        {
            return FindEntity(t => t.c_category_id == categoryId && t.c_key == key).c_value;
        }
    }
}
