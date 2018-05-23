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
    //仓库表
    public class DepotsController : ApiController
    {
        public void Options() { }
        //查询所有仓库
        public async Task<IHttpActionResult> Get_Depots_SelectAll_Async()
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Depots.Where(d=>d.DepotState==1).Select(d => new
                {
                    d.DepotAddress,
                    d.DepotDesc,
                    d.DepotID,
                    d.DepotMan,
                    d.DepotName,
                    d.DepotState,
                    d.DepotTel,
                    d.IsDefault
                }).ToListAsync());
            }
        }
        //根据Id查询仓库
        public async Task<IHttpActionResult> Get_Depots_SelectById_Async(string id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Depots
                    .Where(d => d.DepotID == id)
                    .Select(d => new
                    {
                        d.DepotAddress,
                        d.DepotDesc,
                        d.DepotID,
                        d.DepotMan,
                        d.DepotName,
                        d.DepotState,
                        d.DepotTel,
                        d.IsDefault
                    }).FirstOrDefaultAsync());
            }
        }
        //仓库分页条件查询
        public async Task<IHttpActionResult> Get_Depots_SelectWherePage_Async(int limit, int offset, string order, string DepotName)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new
                {
                    total = await db.Depots
                        .Where(d => d.DepotState == 1)
                        .Where(d => string.IsNullOrEmpty(DepotName) || d.DepotName.Contains(DepotName))
                        .CountAsync(),
                    rows = await db.Depots
                        .Where(d => d.DepotState == 1)
                        .Where(d => string.IsNullOrEmpty(DepotName) || d.DepotName.Contains(DepotName))
                        .Select(d => new
                        {
                            d.DepotAddress,
                            d.DepotDesc,
                            d.DepotID,
                            d.DepotMan,
                            d.DepotName,
                            d.DepotState,
                            d.DepotTel,
                            d.IsDefault
                        })
                        .OrderByDescending(d => d.DepotID).Skip(offset).Take(limit).ToListAsync()
                });
            }
        }
        //判断仓库名称是否重复
        public async Task<IHttpActionResult> Get_Depots_IsExistDepotName_Async(string DepotID, string DepotName)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new { valid = await db.Depots.Where(d => string.IsNullOrEmpty(DepotID) ? (d.DepotName == DepotName) : (d.DepotName == DepotName && d.DepotID.ToString() != DepotID)).CountAsync() <= 0 });
            }
        }
        //添加仓库
        public async Task<IHttpActionResult> Post_Depots_Add_Async(Depots depots)
        {
            using (PSSEntities db = new PSSEntities())
            {
                //获取仓库编号
                ObjectParameter s = new ObjectParameter("no", "");
                db.proc_Depots(s);
                depots.DepotID = s.Value.ToString();
                depots.DepotState = 1;
                db.Depots.Add(depots);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //修改仓库
        public async Task<IHttpActionResult> Put_Depots_Edit_Async(Depots depots)
        {
            using (PSSEntities db = new PSSEntities())
            {
                depots.DepotState = 1;
                db.Entry<Depots>(depots).State = EntityState.Modified;
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //删除仓库
        public async Task<IHttpActionResult> Delete_Depots_Delete_Async(string id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                var depots = await db.Depots.FindAsync(id);
                if (depots == null)
                {
                    return Ok(0);
                }
                db.Depots.Remove(depots);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
    }
}
