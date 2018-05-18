using PSS_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PSS_WebApi.Controllers
{
    //商品表
    public class ProductsController : ApiController
    {
        //判断商品表是否有此商品类型
        public async Task<IHttpActionResult> Get_Products_IsExistProductType_Async(int PTID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Products.Where(p => p.PTID == PTID).CountAsync() > 0);
            }
        }
        //判断商品表是否有此商品单位
        public async Task<IHttpActionResult> Get_Products_IsExistProductUnit_Async(int PUID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Products.Where(p => p.PUID == PUID).CountAsync() > 0);
            }
        }
        //判断商品表是否有此商品颜色
        public async Task<IHttpActionResult> Get_Products_IsExistProductColor_Async(int PCID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Products.Where(p => p.PCID == PCID).CountAsync() > 0);
            }
        }
        //判断商品表是否有此商品规格
        public async Task<IHttpActionResult> Get_Products_IsExistProductSpec_Async(int PSID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Products.Where(p => p.PSID == PSID).CountAsync() > 0);
            }
        }
    }
}
