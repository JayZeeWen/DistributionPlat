using NFine.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Distribution.Model;
using Distribution.Logic;
using Distribution.Web.En.Models;

namespace Distribution.Web.En.Controllers
{
    public class ProductController : BasicController
    {
        // GET: Product
        public ActionResult Index(ProductModel model)
        {
            var pageIndex = Request.QueryString["pageindex"];
            int index = 0;
            int pageSize = 10;
            Int32.TryParse(pageIndex, out index);
            if (index == 0)
            {
                index = 1;
            }
            var UserInfo = NFine.Code.OperatorProvider.Provider.GetCurrent();
            if (UserInfo == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ProductModel viewModel = new ProductModel();
            if (base.agentInfo != null)
            {
                CommLogic.DeepClone<AgentInfoModel>(viewModel, agentInfo);
                List<Product> list = ProductLogic.GetList().Where(t => t.F_DeleteMark == false || t.F_DeleteMark == null ).ToList();
                viewModel.productList = new PagerResult<Product>();
                viewModel.productList.DataList = list.OrderByDescending(t => t.F_CreatorTime).Skip<Product>((index - 1) * pageSize).Take(pageSize);
                viewModel.productList.Code = 0;
                viewModel.productList.Total = list.Count();
                viewModel.productList.PageIndex = index;
                viewModel.productList.PageSize = pageSize;
                viewModel.productList.RequestUrl = "Index?pageindex=" + index;
            }
            return View(viewModel);
        }

        public ActionResult OrderRecord(OrderModel model)
        {
            var pageIndex = Request.QueryString["pageindex"];
            int index = 0;
            int pageSize = 10;
            Int32.TryParse(pageIndex, out index);
            if (index == 0)
            {
                index = 1;
            }
            var UserInfo = NFine.Code.OperatorProvider.Provider.GetCurrent();
            if (UserInfo == null)
            {
                return RedirectToAction("Login", "Account");
            }
            OrderModel viewModel = new OrderModel();
            if (base.agentInfo != null)
            {
                CommLogic.DeepClone<AgentInfoModel>(viewModel, agentInfo);
                List<Order> list = OrderLogic.GetList().Where(t => t.c_agent_id == agentInfo.agent.c_id).ToList();
                viewModel.orderList = new PagerResult<Order>();
                viewModel.orderList.DataList = list.OrderByDescending(t => t.F_CreatorTime).Skip<Order>((index - 1) * pageSize).Take(pageSize);
                viewModel.orderList.Code = 0;
                viewModel.orderList.Total = list.Count();
                viewModel.orderList.PageIndex = index;
                viewModel.orderList.PageSize = pageSize;
                viewModel.orderList.RequestUrl = "Index?pageindex=" + index;
            }
            return View(viewModel);
        }


        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult AddProToCard(string productId, int Amount, int Price)
        {
            AjaxResult result = new AjaxResult();
            try
            {
                string agentId = agentInfo.agent.c_id;
                string OrderId = OrderLogic.GetNopayOrderByAgentId(agentId);
                OrderDetail detail = new OrderDetail();
                detail.F_CreatorUserId = agentId;
                detail.c_product_id = productId;
                detail.c_amount = Amount;
                detail.c_total = Amount * Price;
                detail.c_order_id = OrderId;
                OrderDetailLogic.InsertNewEntiy(detail);
                result.state = ResultType.success.ToString();
                result.message = "成功";
                return Content(result.ToJson());
            }
            catch (Exception ex)
            {
                result.state = ResultType.error.ToString();
                result.message = string.Format("提交失败({0})", ex.Message);
                return Content(result.ToJson());
                throw;
            }
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult PostOrder(string OrderId)
        {
            AjaxResult result = new AjaxResult();
            try
            {
                Order order = OrderLogic.GetEnityById(OrderId);
                if(order == null)
                {
                    throw new Exception("未找到订单");
                }
                order.c_total = OrderDetailLogic.SumOrderTotal(OrderId);
                order.c_order_num = DateTime.Now.ToString("yyyyMMddHHmmss-") + Guid.NewGuid().ToString().Substring(0, 6);
                order.c_state = (int)OrderState.NoDeliver;
                order.c_rec_person = agentInfo.agent.c_rec_person;
                order.c_mobile = agentInfo.agent.c_rec_mobile;
                order.c_address = agentInfo.agent.c_address;
                if(agentInfo.CanCashScore < order.c_total)
                {
                    throw new Exception("剩余积分不足以支付订单内所有商品");
                }
                OrderLogic.UpdateEntity(order);
                int changeScore = 0 -(int)order.c_total;
                ScoreDetailLogic.UpdateAgentScore(agentInfo.agent.c_id, changeScore, "购买产品");
                var detailList = OrderDetailLogic.GetList().Where(t => t.c_order_id == order.F_Id);
                if(detailList.Count() > 0 )
                {
                    int totalAmount = (int)detailList.Sum(t => t.c_amount);
                    ScoreLogic.DealRewardScore(agentInfo.agent.c_id, RewartType.Purchase, totalAmount);
                }
                
                result.state = ResultType.success.ToString();
                result.message = "成功";
                return Content(result.ToJson());
            }
            catch (Exception ex)
            {
                result.state = ResultType.error.ToString();
                result.message = string.Format("提交失败({0})", ex.Message);
                return Content(result.ToJson());
                throw;
            }
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult GetOrderDetail(string OrderId)
        {
            AjaxResult result = new AjaxResult();
            try
            {
                var list = OrderDetailLogic.GetList().Where(t => t.c_order_id == OrderId).ToList();
                foreach (var item in list)
                {
                    item.Pro = ProductLogic.GetEnityById(item.c_product_id);
                }
                result.data = list;
                result.state = ResultType.success.ToString();
                result.message = "成功";
                return Content(result.ToJson());
            }
            catch (Exception ex)
            {
                result.state = ResultType.error.ToString();
                result.message = string.Format("({0})", ex.Message);
                return Content(result.ToJson());
                throw;
            }
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult DelOrderDetail(string DetailId)
        {
            AjaxResult result = new AjaxResult();
            try
            {
                var detail = OrderDetailLogic.GetEnityById(DetailId);
                if (detail != null)
                {
                    string orderId = detail.c_order_id;
                    OrderDetailLogic.DeleteEntity(DetailId);

                    if(!string.IsNullOrEmpty(orderId))
                    {
                        var list = OrderDetailLogic.GetList().Where(t => t.c_order_id == orderId);
                        if(list.Count() == 0 )
                        {
                            OrderLogic.DeleteEntity(orderId);
                        }
                    }
                }                    
                result.state = ResultType.success.ToString();
                result.message = "成功";
                
                return Content(result.ToJson());
            }
            catch (Exception ex)
            {
                result.state = ResultType.error.ToString();
                result.message = string.Format("({0})", ex.Message);
                return Content(result.ToJson());
                throw;
            }
        }
    }
}