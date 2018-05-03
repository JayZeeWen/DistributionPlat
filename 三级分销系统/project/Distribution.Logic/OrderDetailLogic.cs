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
    public static class OrderDetailLogic
    {
        #region BasicMethod
        public static OrderDetail GetEnityById(int id)
        {
            using (DistributionContext context = new DistributionContext())
            {
                return context.t_order_detail.Find(id);
            }
        }

        public static OrderDetail FindEntity(Expression<Func<OrderDetail, bool>> predicate)
        {
            return CommLogic.FindEntity<OrderDetail>(predicate);
        }


        public static List<OrderDetail> GetList()
        {
            List<OrderDetail> list = new List<OrderDetail>();
            using (DistributionContext context = new DistributionContext())
            {
                list = context.t_order_detail.ToList();
            }
            return list;
        }

        public static void InsertNewEntiy(OrderDetail NewOrder)
        {
            using (DistributionContext context = new DistributionContext())
            {
                NewOrder.F_Id = Guid.NewGuid().ToString();
                NewOrder.F_CreatorTime = DateTime.Now;
                NewOrder.F_DeleteMark = false;
                context.t_order_detail.Add(NewOrder);
                context.SaveChanges();

            }
        }

        public static void UpdateEntity(OrderDetail UpdateOrder)
        {
            using (DistributionContext context = new DistributionContext())
            {
                OrderDetail ag = context.t_order_detail.Find(UpdateOrder.F_Id);
                CommLogic.DeepClone<OrderDetail>(ag, UpdateOrder);
                context.SaveChanges();
            }
        }

        public static void DeleteEntity(string OrderId)
        {
            using (DistributionContext context = new DistributionContext())
            {
                OrderDetail DelOrder = context.t_order_detail.Find(OrderId);
                context.t_order_detail.Remove(DelOrder);
                context.SaveChanges();
            }
        }
        #endregion


        
        public static int SumOrderTotal(string OrderId)
        {
            int sum = 0 ;
            var list = GetList().Where(t => t.c_order_id == OrderId);
            if(list.Count() > 0 )
            {
                sum = Convert.ToInt32(list.Sum(t => t.c_total));
            }
            return sum;
        }

       
    }
}
