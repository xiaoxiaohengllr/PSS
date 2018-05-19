using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSS.Controllers
{
    public class BasicController : Controller
    {
        //商品分类页面
        public ActionResult Type()
        {
            return View();
        }
        //添加和修改商品分类页面
        public ActionResult TypeEdit()
        {
            return View();
        }
        //仓库管理页面
        public ActionResult Depots()
        {
            return View();
        }
        //添加和修改仓库页面
        public ActionResult DepotsEdit()
        {
            return View();
        }
        //商品基础数据管理页面
        public ActionResult ProductEssentialData()
        {
            return View();
        }
        //供应商页面
        public ActionResult ProductLend()
        {
            return View();
        }
        //添加和修改供应商页面
        public ActionResult ProductLendEdit()
        {
            return View();
        }
        //客户资料页面
        public ActionResult Customers()
        {
            return View();
        }
        //添加和修改客户资料页面
        public ActionResult CustomersEdit()
        {
            return View();
        }
        public ActionResult Bank()
        {
            return View();
        }
    }
}