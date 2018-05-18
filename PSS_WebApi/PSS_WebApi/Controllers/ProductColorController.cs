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
    //商品颜色表
    public class ProductColorController : ApiController
    {
        public void Options() { }
        //查询所有商品颜色
        public async Task<IHttpActionResult> Get_ProductColor_SelectAll_Async()
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.ProductColor.Select(pc => new
                {
                    pc.PCID,
                    pc.PCName
                }).ToListAsync());
            }
        }
        //根据Id查询商品颜色
        public async Task<IHttpActionResult> Get_ProductColor_SelectById_Async(int pCID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.ProductColor.Where(pc => pc.PCID == pCID).Select(pc => new
                {
                    pc.PCID,
                    pc.PCName
                }).FirstOrDefaultAsync());
            }
        }
        //查询商品商品颜色是否重复
        public async Task<IHttpActionResult> Get_ProductColor_ProductColorNameIsExist_Async(string pCID, string pCName)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new { valid = await db.ProductColor.Where(d => string.IsNullOrEmpty(pCID) ? (d.PCName == pCName) : (d.PCName == pCName && d.PCID.ToString() != pCID)).CountAsync() <= 0 });
            }
        }
        //查询商品颜色分页
        public async Task<IHttpActionResult> Get_ProductColor_SelectPage_Async(int limit, int offset, string order)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new
                {
                    total = await db.ProductColor.CountAsync(),
                    rows = await db.ProductColor.Select(pc => new
                    {
                        pc.PCID,
                        pc.PCName
                    })
                    .OrderByDescending(pc => pc.PCID).Skip(offset).Take(limit).ToListAsync()
                });
            }
        }
        //添加商品颜色
        public async Task<IHttpActionResult> Post_ProductColor_Add_Async(ProductColor productColor)
        {
            using (PSSEntities db = new PSSEntities())
            {
                db.ProductColor.Add(productColor);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //修改商品颜色
        public async Task<IHttpActionResult> Put_ProductColor_Edit_Async(ProductColor productColor)
        {
            using (PSSEntities db = new PSSEntities())
            {
                db.Entry<ProductColor>(productColor).State = EntityState.Modified;
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //删除商品颜色
        public async Task<IHttpActionResult> Delete_ProductColor_Edit_Async(int id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                var productColor = await db.ProductColor.FindAsync(id);
                if (productColor == null)
                {
                    return Ok(0);
                }
                db.ProductColor.Remove(productColor);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
    }
}
