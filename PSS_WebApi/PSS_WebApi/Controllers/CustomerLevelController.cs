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
    //客户等级表
    public class CustomerLevelController : ApiController
    {
        //查询所有客户等级
        public async Task<IHttpActionResult> Get_CustomerLevel_SelectAll_Async()
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.CustomerLevel.Select(cl => new
                {
                    cl.CLAgio,
                    cl.CLID,
                    cl.CLName
                }).ToListAsync());
            }
        }
    }
}
