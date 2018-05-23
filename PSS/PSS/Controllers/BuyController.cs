using PSS.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSS.Controllers
{
    public class BuyController : Controller
    {
        [UsersFilter]
        //商品库存
        public ActionResult DepotStock()
        {
            return View();
        }
    }
}