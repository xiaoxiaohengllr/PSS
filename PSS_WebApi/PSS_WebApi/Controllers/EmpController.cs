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
        public void Options() { }
        //根据员工编号查询员工
        public async Task<IHttpActionResult> Get_Emp_SelectById_Async(int id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Emp.Where(e => e.EmpID == id).Select(e => new
                {
                    e.Dept.DName,
                    e.Did,
                    e.EmpAddr,
                    e.EmpBday,
                    e.EmpID,
                    e.EmpJob,
                    e.EmpMail,
                    e.EmpMark,
                    e.EmpName,
                    e.EmpQQ,
                    e.EmpSex,
                    e.EmpState,
                    e.EmpTcRate,
                    e.EmpTCType,
                    e.EmpTel,
                    e.IsSelect
                }).FirstOrDefaultAsync());
            }
        }
        //判断部门下是否存在员工
        public async Task<IHttpActionResult> Get_Emp_DeptIsExistEmp_Async(int DID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Emp.Where(e => e.Did == DID).CountAsync() > 0);
            }
        }
        //员工分页查询
        public async Task<IHttpActionResult> Get_Emp_SelectDeptPage_Async(int limit, int offset, string order, int DID, string EmpName)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new
                {
                    total = await db.Emp
                        .Where(e => e.EmpState == 1)
                        .Where(e => string.IsNullOrEmpty(EmpName) || e.EmpName.Contains(EmpName))
                        .Where(e => DID == -1 || e.Did == DID || e.Dept.DParentID == DID)
                        .CountAsync(),
                    rows = await db.Emp
                        .Where(e => e.EmpState == 1)
                        .Where(e => string.IsNullOrEmpty(EmpName) || e.EmpName.Contains(EmpName))
                        .Where(e => DID == -1 || e.Did == DID || e.Dept.DParentID == DID)
                        .Select(e => new
                        {
                            e.Dept.DName,
                            e.Did,
                            e.EmpAddr,
                            e.EmpBday,
                            e.EmpID,
                            e.EmpJob,
                            e.EmpMail,
                            e.EmpMark,
                            e.EmpName,
                            e.EmpQQ,
                            e.EmpSex,
                            e.EmpState,
                            e.EmpTcRate,
                            e.EmpTCType,
                            e.EmpTel,
                            e.IsSelect
                        })
                        .OrderByDescending(e => e.EmpID).Skip(offset).Take(limit).ToListAsync()
                });
            }
        }
        //添加员工
        public async Task<IHttpActionResult> Post_Emp_Add_Async(Emp emp)
        {
            using (PSSEntities db = new PSSEntities())
            {
                emp.EmpState = 1;
                db.Emp.Add(emp);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //修改员工
        public async Task<IHttpActionResult> Put_Emp_Edit_Async(Emp emp)
        {
            using (PSSEntities db = new PSSEntities())
            {
                var empEitd = db.Emp.Find(emp.EmpID);
                empEitd.Did = emp.Did;
                empEitd.EmpName = emp.EmpName;
                empEitd.EmpJob = emp.EmpJob;
                empEitd.EmpSex = emp.EmpSex;
                empEitd.EmpBday = emp.EmpBday;
                empEitd.EmpTCType = emp.EmpTCType;
                empEitd.EmpTcRate = emp.EmpTcRate;
                empEitd.EmpTel = emp.EmpTel;
                empEitd.EmpMail = emp.EmpMail;
                empEitd.EmpQQ = emp.EmpQQ;
                empEitd.EmpAddr = emp.EmpAddr;
                empEitd.IsSelect = emp.IsSelect;
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
    }
}
