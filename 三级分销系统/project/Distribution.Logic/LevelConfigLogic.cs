using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Distribution.Model;
using Distribution.DB;
using System.Linq.Expressions;

namespace Distribution.Logic
{
    public static class LevelConfigLogic
    {
        #region BasicMethod
        public static LevelConfig GetEnityById(string id)
        {
            using (DistributionContext context = new DistributionContext())
            {
                return context.t_level_config.Find(id);
            }
        }

        public static LevelConfig FindEntity(Expression<Func<LevelConfig, bool>> predicate)
        {
            return CommLogic.FindEntity<LevelConfig>(predicate);
        }


        public static List<LevelConfig> GetList()
        {
            List<LevelConfig> list = new List<LevelConfig>();
            using (DistributionContext context = new DistributionContext())
            {
                list = context.t_level_config.ToList();
            }
            return list;
        }

        public static void InsertNewEntiy(LevelConfig NewProduct)
        {
            using (DistributionContext context = new DistributionContext())
            {
                NewProduct.c_id = Guid.NewGuid().ToString();
                context.t_level_config.Add(NewProduct);
                context.SaveChanges();

            }
        }

        public static void UpdateEntity(LevelConfig UpdateProduct)
        {
            using (DistributionContext context = new DistributionContext())
            {
                LevelConfig ag = context.t_level_config.Find(UpdateProduct.c_id);
                CommLogic.DeepClone<LevelConfig>(ag, UpdateProduct);
                context.SaveChanges();
            }
        }

        public static void DeleteEntity(int ProductId)
        {
            using (DistributionContext context = new DistributionContext())
            {
                LevelConfig DelProduct = context.t_level_config.Find(ProductId);
                context.t_level_config.Remove(DelProduct);
                context.SaveChanges();
            }
        }
        #endregion
    }
}
