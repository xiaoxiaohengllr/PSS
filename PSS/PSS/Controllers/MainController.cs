using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSS.Controllers
{
    public class MainController : Controller
    {
        //首页
        public ActionResult Index()
        {
            return View();
        }
        //
        public ActionResult Main()
        {
            return View();
        }
    }
}