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
        //仓库库存条件查询分页
        public async Task<IHttpActionResult> Get_DepotStock_SelectWherePage(int limit, int offset, string order, string DepotID, string ProName)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new
                {
                    total = await db.DepotStock
                        .Where(ds => string.IsNullOrEmpty(DepotID) || ds.DepotID == DepotID)
                        .Where(ds => string.IsNullOrEmpty(ProName) || ds.Products.ProName.Contains(ProName))
                        .CountAsync(),
                    rows = await db.DepotStock
                        .Where(ds => string.IsNullOrEmpty(DepotID) || ds.DepotID == DepotID)
                        .Where(ds => string.IsNullOrEmpty(ProName) || ds.Products.ProName.Contains(ProName))
                        .Select(ds => new
                        {
                            ds.DSID,
                            ds.ProID,
                            ds.DepotID,
                            ds.DSAmount,
                            ds.DSPrice,
                            ds.Depots.DepotName,
                            ds.Products.ProductColor.PCName,
                            ds.Products.ProductTypes.PTName,
                            ds.Products.ProductUnit.PUName,
                            ds.Products.ProductSpec.PSName,
                            ds.Products.ProPrice,
                            ds.Products.ProName,
                            ds.Products.ProWorkShop,
                            ds.Products.ProImg,
                            ds.Products.ProMax,
                            ds.Products.ProMin,
                            ds.Products.ProTM,
                            ds.Products.ProJP,
                        })
                        .OrderByDescending(ds => ds.DSID).Skip(offset).Take(limit).ToListAsync()
                });
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
