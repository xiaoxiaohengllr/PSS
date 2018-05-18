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
    //客户表
    public class CustomersController : ApiController
    {
        public void Options() { }
        //查询所有客户
        public async Task<IHttpActionResult> Get_Customers_SelectAll_Async()
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Customers.Select(c => new
                {
                    c.CLID,
                    c.CTID,
                    c.CusAddress,
                    c.CusBank,
                    c.CusBankCreate,
                    c.CusBeginIn,
                    c.CusCompany,
                    c.CusDesc,
                    c.CusFax,
                    c.CusGoogs,
                    c.CusID,
                    c.CusMail,
                    c.CusMan,
                    c.CusName,
                    c.CusPhone,
                    c.CusRateType,
                    c.CusSGoogs,
                    c.CusSorce,
                    c.CusState,
                    c.CusTel,
                    c.CusUrl,
                    c.CustomerLevel.CLName,
                    c.CustomerTypes.CTName
                }).ToListAsync());
            }
        }
        //根据Id查询客户
        public async Task<IHttpActionResult> Get_Customers_SelectById_Async(string id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Customers
                    .Where(c => c.CusID == id)
                    .Select(c => new
                    {
                        c.CLID,
                        c.CTID,
                        c.CusAddress,
                        c.CusBank,
                        c.CusBankCreate,
                        c.CusBeginIn,
                        c.CusCompany,
                        c.CusDesc,
                        c.CusFax,
                        c.CusGoogs,
                        c.CusID,
                        c.CusMail,
                        c.CusMan,
                        c.CusName,
                        c.CusPhone,
                        c.CusRateType,
                        c.CusSGoogs,
                        c.CusSorce,
                        c.CusState,
                        c.CusTel,
                        c.CusUrl,
                        c.CustomerLevel.CLName,
                        c.CustomerTypes.CTName
                    }).FirstOrDefaultAsync());
            }
        }
        //分页条件查询客户
        public async Task<IHttpActionResult> Get_Customers_SelectWherePage_Async(int limit, int offset, string order, int CLID, int CTID, string name)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new
                {
                    total = await db.Customers
                        .Where(c => c.CusState == 1)
                        .Where(c => string.IsNullOrEmpty(name) || c.CusName.Contains(name) || c.CusCompany.Contains(name))
                        .Where(c => CLID == 0 || c.CLID == CLID)
                        .Where(c => CTID == 0 || c.CTID == CLID)
                        .CountAsync(),
                    rows = await db.Customers
                        .Where(c => c.CusState == 1)
                        .Where(c => string.IsNullOrEmpty(name) || c.CusName.Contains(name) || c.CusCompany.Contains(name))
                        .Where(c => CLID == 0 || c.CLID == CLID)
                        .Where(c => CTID == 0 || c.CTID == CLID)
                        .Select(c => new
                        {
                            c.CLID,
                            c.CTID,
                            c.CusAddress,
                            c.CusBank,
                            c.CusBankCreate,
                            c.CusBeginIn,
                            c.CusCompany,
                            c.CusDesc,
                            c.CusFax,
                            c.CusGoogs,
                            c.CusID,
                            c.CusMail,
                            c.CusMan,
                            c.CusName,
                            c.CusPhone,
                            c.CusRateType,
                            c.CusSGoogs,
                            c.CusSorce,
                            c.CusState,
                            c.CusTel,
                            c.CusUrl,
                            c.CustomerLevel.CLName,
                            c.CustomerTypes.CTName
                        })
                        .OrderByDescending(c => c.CusID).Skip(offset).Take(limit).ToListAsync()
                });
            }
        }
    }
}
