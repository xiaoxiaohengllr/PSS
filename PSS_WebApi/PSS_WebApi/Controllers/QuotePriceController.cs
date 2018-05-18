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
    //报价单表
    public class QuotePriceController : ApiController
    {
        //查询所有报价单
        public async Task<IHttpActionResult> Get_QuotePrice_SelectAll_Async()
        {
            using (PSSEntities db = new PSSEntities())
            {
                var quotePrice = await db.QuotePrice
                    .Where(q => q.QPState == 1)
                    .Select(qp => new
                    {
                        qp.CusID,
                        qp.QPDate,
                        qp.QPDesc,
                        qp.QPID,
                        qp.QPState,
                        qp.Users.UsersName,
                        qp.UserID,
                    }).ToListAsync();
                return Ok(quotePrice);
            }
        }
    }
}
