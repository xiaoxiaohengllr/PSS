using PSS.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSS.Controllers
{
    [UsersFilter]
    public class BuyController : Controller
    {
        //商品库存
        public ActionResult DepotStock()
        {
            return View();
        }
        //采购订单
        public ActionResult Stocks()
        {
            return View();
        }
        //添加或修改采购订单
        public ActionResult StocksEdit()
        {
            return View();
        }
        //入库订单
        public ActionResult StockInDepot()
        {
            return View();
        }
        //添加修改入库订单
        public ActionResult StockInDepotEdit() {
            return View();
        }

    }
}