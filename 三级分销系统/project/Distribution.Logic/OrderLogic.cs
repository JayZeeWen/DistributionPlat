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
    public static class OrderLogic
    {
        #region BasicMethod
        public static Order GetEnityById(string id)
        {
            using (DistributionContext context = new DistributionContext())
            {
                return context.t_order.Find(id);
            }
        }

        public static Order FindEntity(Expression<Func<Order, bool>> predicate)
        {
            return CommLogic.FindEntity<Order>(predicate);
        }


        public static List<Order> GetList()
        {
            List<Order> list = new List<Order>();
            using (DistributionContext context = new DistributionContext())
            {
                list = context.t_order.ToList();
            }
            return list;
        }

        public static void InsertNewEntiy(Order NewOrder)
        {
            using (DistributionContext context = new DistributionContext())
            {
                NewOrder.F_Id = Guid.NewGuid().ToString();
                context.t_order.Add(NewOrder);
                context.SaveChanges();

            }
        }

        public static void UpdateEntity(Order UpdateOrder)
        {
            using (DistributionContext context = new DistributionContext())
            {
                Order ag = context.t_order.Find(UpdateOrder.F_Id);
                CommLogic.DeepClone<Order>(ag, UpdateOrder);
                context.SaveChanges();
            }
        }

        public static void DeleteEntity(string OrderId)
        {
            using (DistributionContext context = new DistributionContext())
            {
                Order DelOrder = context.t_order.Find(OrderId);
                context.t_order.Remove(DelOrder);
                context.SaveChanges();
            }
        }
        #endregion

        public static string GetNopayOrderByAgentId(string AgentId)
        {
            var order = FindEntity(t => t.c_agent_id == AgentId && t.c_state == (int)OrderState.InShoppingCard);
            if(order == null)
            {
                order = new Order();
                order.F_Id = Guid.NewGuid().ToString();
                order.c_agent_id = AgentId;
                order.c_state = (int)OrderState.InShoppingCard;
                order.c_order_num = DateTime.Now.ToString("yyyyMMddHHmmss-") + Guid.NewGuid().ToString().Substring(0, 6);
                order.F_CreatorTime = DateTime.Now;
                order.F_CreatorUserId = AgentId;
                order.F_DeleteMark = false;
                InsertNewEntiy(order);
            }
            return order.F_Id;
        }

       
    }
}
