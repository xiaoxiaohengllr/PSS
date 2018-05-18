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
    //商品类别表
    public class ProductTypesController : ApiController
    {
        public void Options() { }
        //查询所有商品类别
        public async Task<IHttpActionResult> Get_ProductTypes_SelectAll_Async()
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.ProductTypes.Select(pt => new
                {
                    pt.PTDes,
                    pt.PTID,
                    pt.PTName,
                    pt.PTParentID
                }).ToListAsync());
            }
        }
        //根据Id查询单个商品类别
        public async Task<IHttpActionResult> Get_ProductTypes_SelectById_Async(int id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.ProductTypes.Where(pt => pt.PTID == id).Select(pt => new
                {
                    pt.PTDes,
                    pt.PTID,
                    pt.PTName,
                    pt.PTParentID
                }).FirstOrDefaultAsync());
            }
        }
        //根据父类别查询商品类别
        public async Task<IHttpActionResult> Get_ProductTypes_SelectIsExistParent_Async(int PTID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.ProductTypes.Where(pt => pt.PTParentID == PTID).CountAsync() > 0);
            }
        }
        //根据父类别查询商品类别
        public async Task<IHttpActionResult> Get_ProductTypes_SelectByPTParentID_Async(int PTParentID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.ProductTypes.Where(pt => pt.PTParentID == PTParentID).Select(pt => new
                {
                    pt.PTDes,
                    pt.PTID,
                    pt.PTName,
                    pt.PTParentID
                }).ToListAsync());
            }
        }
        //判断商品类型名称是否重复
        public async Task<IHttpActionResult> Get_ProductTypes_IsExistPTName_Async(string PTID, string PTName)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new { valid = await db.ProductTypes.Where(pt => string.IsNullOrEmpty(PTID) ? (pt.PTName == PTName) : (pt.PTName == PTName && pt.PTID.ToString() != PTID)).CountAsync() <= 0 });
            }
        }
        //商品类别分页条件查询
        public async Task<IHttpActionResult> Get_ProductTypes_SelectWherePage_Async(int limit, int offset, string order, string PTName, int PTParentID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new
                {
                    total = await db.ProductTypes
                        .Where(pt => string.IsNullOrEmpty(PTName) || pt.PTName.Contains(PTName))
                        .Where(pt => PTParentID == -1 || pt.PTParentID == PTParentID)
                        .CountAsync(),
                    rows = await db.ProductTypes
                        .Where(pt => string.IsNullOrEmpty(PTName) || pt.PTName.Contains(PTName))
                        .Where(pt => PTParentID == -1 || pt.PTParentID == PTParentID)
                        .Select(pt => new{
                            pt.PTDes,
                            pt.PTID,
                            pt.PTName,
                            pt.PTParentID
                        })
                        .OrderByDescending(pt => pt.PTID).Skip(offset).Take(limit).ToListAsync()
                });
            }
        }
        //添加商品类别
        public async Task<IHttpActionResult> Post_ProductTypes_Add_Async(ProductTypes productTypes)
        {
            using (PSSEntities db = new PSSEntities())
            {
                db.ProductTypes.Add(productTypes);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //修改商品类别
        public async Task<IHttpActionResult> Put_ProductTypes_Edit_Async(ProductTypes productTypes)
        {
            using (PSSEntities db = new PSSEntities())
            {
                db.Entry<ProductTypes>(productTypes).State = EntityState.Modified;
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //删除商品类别
        public async Task<IHttpActionResult> Delete_ProductTypes_Delete_Async(int id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                ProductTypes p = await db.ProductTypes.FindAsync(id);
                if (p == null) return Ok(false);
                db.ProductTypes.Remove(p);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
    }
}
