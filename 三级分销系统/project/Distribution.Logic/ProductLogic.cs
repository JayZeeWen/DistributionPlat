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
    public static class ProductLogic
    {
        #region BasicMethod
        public static Product GetEnityById(string id)
        {
            using (DistributionContext context = new DistributionContext())
            {
                return context.t_product.Find(id);
            }
        }

        public static Product FindEntity(Expression<Func<Product, bool>> predicate)
        {
            return CommLogic.FindEntity<Product>(predicate);
        }


        public static List<Product> GetList()
        {
            List<Product> list = new List<Product>();
            using (DistributionContext context = new DistributionContext())
            {
                list = context.t_product.ToList();
            }
            return list;
        }

        public static void InsertNewEntiy(Product NewProduct)
        {
            using (DistributionContext context = new DistributionContext())
            {
                NewProduct.F_Id = Guid.NewGuid().ToString();
                context.t_product.Add(NewProduct);
                context.SaveChanges();

            }
        }

        public static void UpdateEntity(Product UpdateProduct)
        {
            using (DistributionContext context = new DistributionContext())
            {
                Product ag = context.t_product.Find(UpdateProduct.F_Id);
                CommLogic.DeepClone<Product>(ag, UpdateProduct);
                context.SaveChanges();
            }
        }

        public static void DeleteEntity(int ProductId)
        {
            using (DistributionContext context = new DistributionContext())
            {
                Product DelProduct = context.t_product.Find(ProductId);
                context.t_product.Remove(DelProduct);
                context.SaveChanges();
            }
        }
        #endregion

       
    }
}
