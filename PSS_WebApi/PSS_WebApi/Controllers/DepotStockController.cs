using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;
using PSS_WebApi.Models;

namespace PSS_WebApi.Controllers
{
    //库存表
    public class DepotStockController : ApiController
    {
        public void Options() { }
        //判断仓库是否存在商品
        public async Task<IHttpActionResult> Get_DepotStock_DepotIsExisProduct_Async(string depotId)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.DepotStock.Where(ds => ds.DepotID == depotId).CountAsync() > 0);
            }
        }
        //判断商品是否存在库存
        public async Task<IHttpActionResult> Get_DepotStock_DepotStockIsExisProduct_Async(int ProID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.DepotStock.Where(ds => ds.ProID == ProID).CountAsync() > 0);
            }
        }
    }
}
