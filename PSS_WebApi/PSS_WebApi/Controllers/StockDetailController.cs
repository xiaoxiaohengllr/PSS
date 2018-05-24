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
    public class StockDetailController : ApiController
    {
        public void Options() { }
        //根据StockID查询采购详细订单
        public async Task<IHttpActionResult> Get_StockDetail_SelectById_Async(string StockID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.StockDetail.Select(sd=>new {
                    sd.StockID,
                    sd.SDetailID,
                    sd.ProID,
                    sd.SDetailAmount,
                    sd.SDetailPrice,
                    sd.Products.ProductColor.PCName,
                    sd.Products.ProName,
                    sd.Products.ProductTypes.PTName,
                    sd.Products.ProductUnit.PUName,
                    sd.Products.ProductSpec.PSName
                }).Where(sd => sd.StockID == StockID).ToListAsync());
            }
        }
    }
}
