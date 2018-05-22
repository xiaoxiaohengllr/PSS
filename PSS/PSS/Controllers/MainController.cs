using PSS.Attribute;
using PSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSS.Controllers
{
    public class MainController : Controller
    {
        [UsersFilter]
        //首页
        public ActionResult Index()
        {
            return View();
        }
        [UsersFilter]
        public ActionResult Main()
        {
            return View();
        }
        //登录
        public ActionResult Login()
        {
            return View();
        }
        //绑定Session
        public ActionResult SetSession(Users user)
        {
            HttpContext.Session.Add("user",user);
            return Json("");
        }
        //注销Session
        public ActionResult RemoveSession(Users user)
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Login");
        }
    }
}