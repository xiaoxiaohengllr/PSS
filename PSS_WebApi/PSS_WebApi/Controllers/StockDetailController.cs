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
        //根据id查询采购详细订单
        public async Task<IHttpActionResult> Get_StockDetail_SelectById_Async(int id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.StockDetail.Select(sd => new {
                    sd.StockID,
                    sd.SDetailID,
                    sd.ProID,
                    sd.SDetailAmount,
                    sd.SDetailDepAmount,
                    sd.SDetailPrice,
                    sd.Products.ProductColor.PCName,
                    sd.Products.ProName,
                    sd.Products.ProductTypes.PTName,
                    sd.Products.ProductUnit.PUName,
                    sd.Products.ProductSpec.PSName
                }).Where(sd => sd.SDetailID == id).FirstOrDefaultAsync());
            }
        }
        //根据采购单编号查询
        public async Task<IHttpActionResult> Get_StockDetail_SelectByStockID_Async(string StockID)
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
                    sd.Products.ProductSpec.PSName,
                    sd.SDetailDepAmount
                }).Where(sd => sd.StockID == StockID).ToListAsync());
            }
        }
        //根据采购单Id查询采购详细单分页
        public async Task<IHttpActionResult> Get_StockDetail_SelectWherePage_Async(int limit, int offset, string order, string StockID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new
                {
                    total = await db.StockDetail
                        .Where(d => d.StockID == StockID)
                        .CountAsync(),
                    rows = await db.StockDetail
                        .Where(d => d.StockID == StockID)
                        .Select(sd => new
                        {
                            sd.StockID,
                            sd.SDetailID,
                            sd.ProID,
                            sd.SDetailAmount,
                            sd.SDetailPrice,
                            sd.Products.ProductColor.PCName,
                            sd.Products.ProName,
                            sd.Products.ProductTypes.PTName,
                            sd.Products.ProductUnit.PUName,
                            sd.Products.ProductSpec.PSName,
                            sd.SDetailDepAmount
                        })
                        .OrderByDescending(d => d.StockID).Skip(offset).Take(limit).ToListAsync()
                });
            }
        }
    }
}
