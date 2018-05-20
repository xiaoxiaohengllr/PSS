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
        //商品资料页面
        public ActionResult Products()
        {
            return View();
        }
        //添加和修改商品资料页面
        public ActionResult ProductsEdit()
        {
            return View();
        }
        public ActionResult ProductsImgUpload()
        {
            try
            {
                //获得文件对象
                HttpPostedFileBase hpfb = Request.Files[0];
                string fileName = System.Guid.NewGuid().ToString();
                //设置保存路径
                string name = string.Format("{0}{1}{2}",
                      Server.MapPath("~/images/"),
                      fileName,
                      System.IO.Path.GetExtension(hpfb.FileName)
                   );
                //保存文件
                hpfb.SaveAs(name);
                return Json(new { file = fileName + System.IO.Path.GetExtension(hpfb.FileName) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { file = "" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}