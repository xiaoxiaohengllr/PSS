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
    //商品规格表
    public class ProductSpecController : ApiController
    {
        public void Options() { }
        //查询所有商品规格
        public async Task<IHttpActionResult> Get_ProductSpec_SelectAll_Async()
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.ProductSpec.Select(ps => new
                {
                    ps.PSID,
                    ps.PSName
                }).ToListAsync());
            }
        }
        //根据Id查询商品规格
        public async Task<IHttpActionResult> Get_ProductSpec_SelectById_Async(int pSID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.ProductSpec.Where(ps => ps.PSID == pSID).Select(ps => new
                {
                    ps.PSID,
                    ps.PSName
                }).FirstOrDefaultAsync());
            }
        }
        //查询商品商品规格是否重复
        public async Task<IHttpActionResult> Get_ProductSpec_ProductSpecNameIsExist_Async(string pSID, string pSName)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new { valid = await db.ProductSpec.Where(d => string.IsNullOrEmpty(pSID) ? (d.PSName == pSName) : (d.PSName == pSName && d.PSID.ToString() != pSID)).CountAsync() <= 0 });
            }
        }
        //查询商品规格分页
        public async Task<IHttpActionResult> Get_ProductSpec_SelectPage_Async(int limit, int offset, string order)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new
                {
                    total = await db.ProductSpec.CountAsync(),
                    rows = await db.ProductSpec.Select(ps => new
                    {
                        ps.PSID,
                        ps.PSName
                    })
                    .OrderByDescending(ps => ps.PSID).Skip(offset).Take(limit).ToListAsync()
                });
            }
        }
        //添加商品规格
        public async Task<IHttpActionResult> Post_ProductSpec_Add_Async(ProductSpec productSpec)
        {
            using (PSSEntities db = new PSSEntities())
            {
                db.ProductSpec.Add(productSpec);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //修改商品规格
        public async Task<IHttpActionResult> Put_ProductSpec_Edit_Async(ProductSpec productSpec)
        {
            using (PSSEntities db = new PSSEntities())
            {
                db.Entry<ProductSpec>(productSpec).State = EntityState.Modified;
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //删除商品规格
        public async Task<IHttpActionResult> Delete_ProductSpec_Edit_Async(int id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                var productSpec = await db.ProductSpec.FindAsync(id);
                if (productSpec == null)
                {
                    return Ok(0);
                }
                db.ProductSpec.Remove(productSpec);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
    }
}
