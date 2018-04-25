using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Distribution.Web.Controllers
{
    public class BasicController : Controller
    {
        public BasicController()
        {
            var UserInfo = NFine.Code.OperatorProvider.Provider.GetCurrent();
            ViewBag.UserId = "";
            ViewBag.User = "";
            if (UserInfo != null)
            {
                ViewBag.UserId = UserInfo.UserId;
                ViewBag.User = UserInfo.UserCode;
            }

        }
    }
}