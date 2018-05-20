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
    //商品表
    public class ProductsController : ApiController
    {
        public void Options() { }
        //查询所有商品
        public async Task<IHttpActionResult> Get_Products_Select_Async()
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Products.Select(p => new
                {
                    p.PCID,
                    p.ProDesc,
                    p.ProductColor.PCName,
                    p.ProductSpec.PSName,
                    p.ProductTypes.PTName,
                    p.ProductUnit.PUName,
                    p.ProID,
                    p.ProImg,
                    p.ProInPrice,
                    p.ProJP,
                    p.ProMax,
                    p.ProMin,
                    p.ProName,
                    p.ProPrice,
                    p.ProState,
                    p.ProTM,
                    p.ProWorkShop,
                    p.PSID,
                    p.PUID,
                    p.PTID
                }).ToListAsync());
            }
        }
        //根据Id查询商品
        public async Task<IHttpActionResult> Get_Products_SelectById_Async(int id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Products
                    .Where(p => p.ProID == id)
                    .Select(p => new
                    {
                        p.PCID,
                        p.ProDesc,
                        p.ProductColor.PCName,
                        p.ProductSpec.PSName,
                        p.ProductTypes.PTName,
                        p.ProductUnit.PUName,
                        p.ProID,
                        p.ProImg,
                        p.ProInPrice,
                        p.ProJP,
                        p.ProMax,
                        p.ProMin,
                        p.ProName,
                        p.ProPrice,
                        p.ProState,
                        p.ProTM,
                        p.ProWorkShop,
                        p.PSID,
                        p.PUID,
                        p.PTID
                    }).FirstOrDefaultAsync());


            }
        }
        //判断商品表是否有此商品类型
        public async Task<IHttpActionResult> Get_Products_IsExistProductType_Async(int PTID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Products.Where(p => p.PTID == PTID).CountAsync() > 0);
            }
        }
        //判断商品表是否有此商品单位
        public async Task<IHttpActionResult> Get_Products_IsExistProductUnit_Async(int PUID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Products.Where(p => p.PUID == PUID).CountAsync() > 0);
            }
        }
        //判断商品表是否有此商品颜色
        public async Task<IHttpActionResult> Get_Products_IsExistProductColor_Async(int PCID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Products.Where(p => p.PCID == PCID).CountAsync() > 0);
            }
        }
        //判断商品表是否有此商品规格
        public async Task<IHttpActionResult> Get_Products_IsExistProductSpec_Async(int PSID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Products.Where(p => p.PSID == PSID).CountAsync() > 0);
            }
        }
        //查询商品名称是否存在
        public async Task<IHttpActionResult> Get_Products_ProductsNameIsExist_Async(string ProID, string ProName)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new { valid = await db.Products.Where(d => string.IsNullOrEmpty(ProID) ? (d.ProName == ProName) : (d.ProName == ProName && d.ProID.ToString() != ProID)).CountAsync() <= 0 });
            }
        }
        //商品分页条件查询
        public async Task<IHttpActionResult> Get_Products_SelectWherePage_Async(int limit, int offset, string order, string name, int PTID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new
                {
                    total = await db.Products
                        .Where(p => p.ProState == 1)
                        .Where(p => string.IsNullOrEmpty(name) || p.ProName.Contains(name) || p.ProJP.Contains(name))
                        .Where(p => PTID == -1 || p.PTID == PTID)
                        .CountAsync(),
                    rows = await db.Products
                        .Where(p => p.ProState == 1)
                        .Where(p => string.IsNullOrEmpty(name) || p.ProName.Contains(name) || p.ProJP.Contains(name))
                        .Where(p => PTID == -1 || p.PTID == PTID)
                        .Select(p => new
                        {
                            p.PCID,
                            p.ProDesc,
                            p.ProductColor.PCName,
                            p.ProductSpec.PSName,
                            p.ProductTypes.PTName,
                            p.ProductUnit.PUName,
                            p.ProID,
                            p.ProImg,
                            p.ProInPrice,
                            p.ProJP,
                            p.ProMax,
                            p.ProMin,
                            p.ProName,
                            p.ProPrice,
                            p.ProState,
                            p.ProTM,
                            p.ProWorkShop,
                            p.PSID,
                            p.PUID,
                            p.PTID
                        })
                        .OrderByDescending(p => p.ProID).Skip(offset).Take(limit).ToListAsync()
                });
            }
        }
        //添加商品
        public async Task<IHttpActionResult> Post_Products_Add_Async(Products products)
        {
            using (PSSEntities db = new PSSEntities())
            {
                products.ProState = 1;
                db.Products.Add(products);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //修改商品
        public async Task<IHttpActionResult> Put_Products_Edit_Async(Products products)
        {
            using (PSSEntities db = new PSSEntities())
            {
                products.ProState = 1;
                db.Entry<Products>(products).State = EntityState.Modified;
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //删除商品
        public async Task<IHttpActionResult> Delete_Products_Delete_Async(int id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                var products = db.Products.Find(id);
                if (products==null)
                {
                    return Ok(0);
                }
                db.Products.Remove(products);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
    }
}
