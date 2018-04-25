using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq.Expressions;
using Distribution.DB;

namespace Distribution.Logic
{
    public static class CommLogic
    {
        public static void DeepClone<T>(T OriEntity, T UpdateEntity)
        {
            PropertyInfo[] props = null;
            Type type = typeof(T);
            props = type.GetProperties();
            foreach (var p in props)
            {
                var value = p.GetValue(UpdateEntity);
                p.SetValue(OriEntity, value);
            }
        }

        public static TEntity FindEntity<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class,new()
        {
            using (DistributionContext dbcontext = new DistributionContext ())
            {
                return dbcontext.Set<TEntity>().FirstOrDefault(predicate);
            }
        }


    }
}
