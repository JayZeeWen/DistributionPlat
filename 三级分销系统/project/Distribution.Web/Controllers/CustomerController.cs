using NFine.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Distribution.Model;
using Distribution.Logic;
using Distribution.Web.Models;

namespace Distribution.Web.Controllers
{
    public class CustomerController : BasicController
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
                viewModel.productList.DataList = list.Skip<Product>((index - 1) * pageSize).Take(pageSize).OrderByDescending(t => t.F_CreatorTime);
                viewModel.productList.Code = 0;
                viewModel.productList.Total = list.Count();
                viewModel.productList.PageIndex = index;
                viewModel.productList.PageSize = pageSize;
                viewModel.productList.RequestUrl = "Index?pageindex=" + index;
            }
            return View(viewModel);
        }
        
        public ActionResult Test()
        {
            return View();
        }

        public string data()
        {
            string s = @"{

                    ""name"": ""代理商"",
		            ""number"": 1500000000,
""children"": [{

          ""name"": ""张啊啊啊"",
		""number"": 138522222222},
{

          ""name"": ""欧阳盛大"",
		""number"": 15012341234}]
            }";
            return s;
        }
        
        
    }
}