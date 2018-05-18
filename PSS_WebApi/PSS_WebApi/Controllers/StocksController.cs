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
    //采购单
    public class StocksController : ApiController
    {
        //判断采购单是否有供应商
        public async Task<IHttpActionResult> Get_Stocks_IsExistProductLend_Async(string PPID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Stocks.Where(s => s.PPID == PPID).CountAsync() > 0);
            }
        }
    }
}
