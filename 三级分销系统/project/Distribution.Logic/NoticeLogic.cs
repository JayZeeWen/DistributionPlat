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
    public static class NoticeLogic
    {
        #region BasicMethod
        public static PublicNotice GetEnityById(string id)
        {
            using (DistributionContext context = new DistributionContext())
            {
                return context.t_public_notice.Find(id);
            }
        }

        public static PublicNotice FindEntity(Expression<Func<PublicNotice, bool>> predicate)
        {
            return CommLogic.FindEntity<PublicNotice>(predicate);
        }


        public static List<PublicNotice> GetList()
        {
            List<PublicNotice> list = new List<PublicNotice>();
            using (DistributionContext context = new DistributionContext())
            {
                list = context.t_public_notice.ToList();
            }
            return list;
        }

        public static void InsertNewEntiy(PublicNotice NewProduct)
        {
            using (DistributionContext context = new DistributionContext())
            {
                NewProduct.F_Id = Guid.NewGuid().ToString();
                context.t_public_notice.Add(NewProduct);
                context.SaveChanges();

            }
        }

        public static void UpdateEntity(PublicNotice UpdateProduct)
        {
            using (DistributionContext context = new DistributionContext())
            {
                PublicNotice ag = context.t_public_notice.Find(UpdateProduct.F_Id);
                CommLogic.DeepClone<PublicNotice>(ag, UpdateProduct);
                context.SaveChanges();
            }
        }

        public static void DeleteEntity(int ProductId)
        {
            using (DistributionContext context = new DistributionContext())
            {
                PublicNotice DelProduct = context.t_public_notice.Find(ProductId);
                context.t_public_notice.Remove(DelProduct);
                context.SaveChanges();
            }
        }
        #endregion

       
    }
}
