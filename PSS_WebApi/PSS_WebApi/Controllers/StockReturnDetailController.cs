using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using PSS_WebApi.Models;
using System.Data.Entity;

namespace PSS_WebApi.Controllers
{
    //采购退货单详单
    public class StockReturnDetailController : ApiController
    {
        public void Options() { }
        //根据SRID查询采购退货单详单
        public async Task<IHttpActionResult> Get_StockReturnDetail_SelectBySRID_Async(string SRID)
        {
            using (PSSEntities db=new PSSEntities())
            {
                return Ok(await db.StockReturnDetail.Where(srd=>srd.SRID== SRID).Select(srd=>new {
                    srd.ProID,
                    srd.SRID,
                    srd.SRDPrice,
                    srd.SRDAmount,
                    srd.SRDDesc,
                    srd.SRDID,
                    srd.Products.ProName,
                    srd.Products.ProductSpec.PSName,
                    srd.Products.ProductColor.PCName,
                    srd.Products.ProductTypes.PTName,
                    srd.Products.ProductUnit.PUName
                }).ToListAsync());
            }
        }
    }
}
