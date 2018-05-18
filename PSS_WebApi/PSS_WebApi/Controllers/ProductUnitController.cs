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
    //商品单位表
    public class ProductUnitController : ApiController
    {
        public void Options() { }
        //查询所有商品单位
        public async Task<IHttpActionResult> Get_ProductUnit_SelectAll_Async()
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.ProductUnit.Select(pu => new
                {
                    pu.PUID,
                    pu.PUName
                }).ToListAsync());
            }
        }
        //根据Id查询商品单位
        public async Task<IHttpActionResult> Get_ProductUnit_SelectById_Async(int pUID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.ProductUnit.Where(pu => pu.PUID == pUID).Select(pu => new
                {
                    pu.PUID,
                    pu.PUName
                }).FirstOrDefaultAsync());
            }
        }
        //查询商品单位名称是否重复
        public async Task<IHttpActionResult> Get_ProductUnit_ProductUnitNameIsExist_Async(string pUID,string pUName)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new { valid = await db.ProductUnit.Where(d => string.IsNullOrEmpty(pUID) ? (d.PUName == pUName) : (d.PUName == pUName && d.PUID.ToString() != pUID)).CountAsync() <= 0 });
            }
        }
        //查询商品单位分页
        public async Task<IHttpActionResult> Get_ProductUnit_SelectPage_Async(int limit, int offset, string order)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new
                {
                    total = await db.ProductUnit.CountAsync(),
                    rows = await db.ProductUnit.Select(pu => new
                    {
                        pu.PUID,
                        pu.PUName
                    })
                    .OrderByDescending(pu => pu.PUID).Skip(offset).Take(limit).ToListAsync()
                });
            }
        }
        //添加商品单位
        public async Task<IHttpActionResult> Post_ProductUnit_Add_Async(ProductUnit productUnit)
        {
            using (PSSEntities db = new PSSEntities())
            {
                db.ProductUnit.Add(productUnit);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //修改商品单位
        public async Task<IHttpActionResult> Put_ProductUnit_Edit_Async(ProductUnit productUnit)
        {
            using (PSSEntities db = new PSSEntities())
            {
                db.Entry<ProductUnit>(productUnit).State = EntityState.Modified;
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //删除商品单位
        public async Task<IHttpActionResult> Delete_ProductUnit_Edit_Async(int id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                var productUnit = await db.ProductUnit.FindAsync(id);
                if (productUnit == null)
                {
                    return Ok(0);
                }
                db.ProductUnit.Remove(productUnit);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
    }
}
