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
    //客户类别
    public class CustomerTypesController : ApiController
    {
        //查询所有客户类别
        public async Task<IHttpActionResult> Get_CustomerTypes_SelectAll_Async()
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.CustomerTypes.Select(ct => new
                {
                    ct.CTDes,
                    ct.CTID,
                    ct.CTName,
                    ct.CTParentID
                }).ToListAsync());
            }
        }
    }
}
