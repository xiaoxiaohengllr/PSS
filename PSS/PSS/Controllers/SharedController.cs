using PSS.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSS.Controllers
{
    [UsersFilter]
    public class SharedController : Controller
    {
        //客户选择页面
        public ActionResult CustomersList()
        {
            return View();
        }
        //供应商选择页面
        public ActionResult ProductLendList()
        {
            return View();
        }
        //商品类别选择页面
        public ActionResult ProductsList()
        {
            return View();
        }
    }
}