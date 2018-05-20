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
    public class EmpController : ApiController
    {
        //判断部门下是否存在员工
        public async Task<IHttpActionResult> Get_Emp_DeptIsExistEmp_Async(int DID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Emp.Where(e => e.Did == DID).CountAsync() > 0);
            }
        }
    }
}
