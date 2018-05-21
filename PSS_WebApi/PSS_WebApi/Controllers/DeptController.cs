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
    //部门表
    public class DeptController : ApiController
    {
        public void Options() { }
        //查询所有部门
        public async Task<IHttpActionResult> Get_Dept_SelectAll_Async()
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Dept
                    .Where(d=>d.DState)
                    .Select(d => new
                    {
                        d.DID,
                        d.DName,
                        d.DParentID,
                        d.DState
                    }).ToListAsync());
            }
        }
        //根据Id查询部门
        public async Task<IHttpActionResult> Get_Dept_SelectById_Async(int id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Dept.Where(d => d.DID == id)
                    .Select(d => new
                    {
                        d.DID,
                        d.DName,
                        d.DParentID,
                        d.DState
                    }).FirstOrDefaultAsync());
            }
        }
        //根据上级编号查询
        public async Task<IHttpActionResult> Get_Dept_SelectByDParentID_Async(int DParentID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Dept
                    .Where(d => d.DState)
                    .Where(d => d.DParentID == DParentID)
                    .Select(d => new
                    {
                        d.DID,
                        d.DName,
                        d.DParentID,
                        d.DState
                    }).ToListAsync());
            }
        }
        //判断部门名称是否存在
        public async Task<IHttpActionResult> Get_Dept_DeptNameIsExist_Async(string DName, string DID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new { valid = await db.Dept.Where(d => string.IsNullOrEmpty(DID) ? (d.DName == DName) : (d.DName == DName && d.DID.ToString() != DID)).CountAsync() <= 0 });
            }
        }
        //部门分页查询
        public async Task<IHttpActionResult> Get_Dept_SelectDeptPage_Async(int limit, int offset, string order, string DName)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new
                {
                    total = await db.Dept
                        .Where(d => d.DState)
                        .Where(d => string.IsNullOrEmpty(DName) || d.DName.Contains(DName))
                        .CountAsync(),
                    rows = await db.Dept
                        .Where(d => d.DState)
                        .Where(d => string.IsNullOrEmpty(DName) || d.DName.Contains(DName))
                        .Select(d => new
                        {
                            d.DID,
                            d.DName,
                            d.DParentID,
                            d.DState
                        })
                        .OrderByDescending(d => d.DID).Skip(offset).Take(limit).ToListAsync()
                });
            }
        }
        //添加部门
        public async Task<IHttpActionResult> Post_Dept_Add_Async(Dept dept)
        {
            using (PSSEntities db = new PSSEntities())
            {
                dept.DState = true;
                db.Dept.Add(dept);
                return Ok(await db.SaveChangesAsync()); ;
            }
        }
        //修改部门
        public async Task<IHttpActionResult> Put_Dept_Edit_Async(Dept dept)
        {
            using (PSSEntities db = new PSSEntities())
            {
                dept.DState = true;
                db.Entry<Dept>(dept).State = EntityState.Modified;
                return Ok(await db.SaveChangesAsync()); ;
            }
        }
        //删除部门
        public async Task<IHttpActionResult> Delete_Dept_Delete_Async(int id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                Dept dept = await db.Dept.FindAsync(id);
                if (dept == null)
                {
                    return Ok(0);
                }
                dept.DState = false;
                db.Entry<Dept>(dept).State = EntityState.Modified;
                return Ok(await db.SaveChangesAsync()); ;
            }
        }
    }
}
