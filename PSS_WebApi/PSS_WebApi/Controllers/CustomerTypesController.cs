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
        public void Options() { }
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

                }).ToListAsync());
            }
        }
        //根据Id查询客户类别
        public async Task<IHttpActionResult> Get_CustomerTypes_SelectById_Async(int id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.CustomerTypes.Where(ps => ps.CTID == id).Select(ps => new
                {
                    ps.CTID,
                    ps.CTName
                }).FirstOrDefaultAsync());
            }
        }
        //查询客户类别是否重复
        public async Task<IHttpActionResult> Get__CustomerTypesNameIsExist_Async(string CTID, string CTName)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new { valid = await db.CustomerTypes.Where(d => string.IsNullOrEmpty(CTID) ? (d.CTName == CTName) : (d.CTName == CTName && d.CTID.ToString() != CTID)).CountAsync() <= 0 });
            }
        }
        //查询客户类别分页
        public async Task<IHttpActionResult> Get_CustomerTypes_SelectPage_Async(int limit, int offset, string order)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new
                {
                    total = await db.CustomerTypes.CountAsync(),
                    rows = await db.CustomerTypes.Select(pc => new
                    {
                        pc.CTID,
                        pc.CTName
                    })
                    .OrderByDescending(pc => pc.CTID).Skip(offset).Take(limit).ToListAsync()
                });
            }
        }
        //添加
        public async Task<IHttpActionResult> Post_CustomerTypes_Add_Async(CustomerTypes customerTypes)
        {
            using (PSSEntities db = new PSSEntities())
            {
                db.CustomerTypes.Add(customerTypes);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //修改
        public async Task<IHttpActionResult> Put_CustomerTypes_Edit_Async(CustomerTypes customerTypes)
        {
            using (PSSEntities db = new PSSEntities())
            {
                db.Entry<CustomerTypes>(customerTypes).State = EntityState.Modified;
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //删除
        public async Task<IHttpActionResult> Delete_CustomerTypes_Edit_Async(int id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                var customerTypes = await db.CustomerTypes.FindAsync(id);
                if (customerTypes == null)
                {
                    return Ok(0);
                }
                db.CustomerTypes.Remove(customerTypes);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
    }
}
