using PSS_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
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
                    c.CustomerTypes.CTName,
                    c.IsDefault
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
                        c.CustomerTypes.CTName,
                        c.IsDefault
                    }).FirstOrDefaultAsync());
            }
        }
        //判断客户等级是否存在
        public async Task<IHttpActionResult> Get_Customers_IsExistCustomerLevel_Async(int CLID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Customers.Where(c => c.CLID == CLID).CountAsync() > 0);
            }
        }
        //判断客户类型是否存在
        public async Task<IHttpActionResult> Get_Customers_IsExistCustomerTypes_Async(int CTID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Customers.Where(c => c.CTID == CTID).CountAsync() > 0);
            }
        }
        //查询公司名称是否存在
        public async Task<IHttpActionResult> Get_Customers_SelectIsExistCusName_Async(string CusCompany, string CusID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new { valid = await db.Customers.Where(c => string.IsNullOrEmpty(CusID) ? (c.CusCompany == CusCompany) : (c.CusCompany == CusCompany && c.CusID.ToString() != CusID)).CountAsync() <= 0 });
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
                        .Where(c => CTID == 0 || c.CTID == CTID)
                        .CountAsync(),
                    rows = await db.Customers
                        .Where(c => c.CusState == 1)
                        .Where(c => string.IsNullOrEmpty(name) || c.CusName.Contains(name) || c.CusCompany.Contains(name))
                        .Where(c => CLID == 0 || c.CLID == CLID)
                        .Where(c => CTID == 0 || c.CTID == CTID)
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
                            c.CustomerTypes.CTName,
                            c.IsDefault
                        })
                        .OrderByDescending(c => c.CusID).Skip(offset).Take(limit).ToListAsync()
                });
            }
        }
        //添加客户
        public async Task<IHttpActionResult> Post_Customers_Add_Async(Customers customers)
        {
            using (PSSEntities db = new PSSEntities())
            {
                //获取客户编号
                ObjectParameter s = new ObjectParameter("no", "");
                db.proc_Customers(s);
                customers.CusID = s.Value.ToString();
                customers.CusState = 1;
                db.Customers.Add(customers);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //修改客户
        public async Task<IHttpActionResult> Put_Customers_Edit_Async(Customers customers)
        {
            using (PSSEntities db = new PSSEntities())
            {
                customers.CusState = 1;
                db.Entry<Customers>(customers).State = EntityState.Modified;
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //删除客户
        public async Task<IHttpActionResult> Delete_Customers_Delete_Async(string id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                var customers = await db.Customers.FindAsync(id);
                if (customers == null)
                {
                    return Ok(0);
                }
                db.Customers.Remove(customers);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
    }
}
