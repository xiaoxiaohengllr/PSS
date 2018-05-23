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
        public void Options() { }
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
        //根据Id查询客户等级
        public async Task<IHttpActionResult> Get_CustomerLevel_SelectById_Async(int CLID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.CustomerLevel.Where(pu => pu.CLID == CLID).Select(pu => new
                {
                    pu.CLID,
                    pu.CLName
                }).FirstOrDefaultAsync());
            }
        }
        //查询客户等级名称是否重复
        public async Task<IHttpActionResult> Get_CustomerLevel_CustomerLevelNameIsExist_Async(string CLID, string CLName)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new { valid = await db.CustomerLevel.Where(d => string.IsNullOrEmpty(CLID) ? (d.CLName == CLName) : (d.CLName == CLName && d.CLID.ToString() != CLID)).CountAsync() <= 0 });
            }
        }
        //查询客户等级分页
        public async Task<IHttpActionResult> Get_CustomerLevel_SelectPage_Async(int limit, int offset, string order)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new
                {
                    total = await db.CustomerLevel.CountAsync(),
                    rows = await db.CustomerLevel.Select(pu => new
                    {
                        pu.CLID,
                        pu.CLName
                    })
                    .OrderByDescending(pu => pu.CLID).Skip(offset).Take(limit).ToListAsync()
                });
            }
        }
        //添加客户等级
        public async Task<IHttpActionResult> Post_CustomerLevel_Add_Async(CustomerLevel customerLevel)
        {
            using (PSSEntities db = new PSSEntities())
            {
                db.CustomerLevel.Add(customerLevel);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //修改客户等级
        public async Task<IHttpActionResult> Put_CustomerLevel_Edit_Async(CustomerLevel customerLevel)
        {
            using (PSSEntities db = new PSSEntities())
            {
                db.Entry<CustomerLevel>(customerLevel).State = EntityState.Modified;
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //删除客户等级
        public async Task<IHttpActionResult> Delete_CustomerLevel_Edit_Async(int id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                var customerLevel = await db.CustomerLevel.FindAsync(id);
                if (customerLevel == null)
                {
                    return Ok(0);
                }
                db.CustomerLevel.Remove(customerLevel);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
    }
}
