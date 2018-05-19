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
    //客户订单
    public class CustomerOrderController : ApiController
    {
        //判断客户订单是否有此客户
        public async Task<IHttpActionResult> Get_CustomerOrder_IsExistCustomer_Async(string CusID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.CustomerOrder.Where(co => co.CusID == CusID).CountAsync() > 0);
            }
        }
    }
}
