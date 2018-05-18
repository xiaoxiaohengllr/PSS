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
    //供应商
    public class ProductLendController : ApiController
    {
        public void Options() { }
        //查询所有供应商
        public async Task<IHttpActionResult> Get_ProductLend_SelectAll_Async()
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.ProductLend.Select(pl => new
                {
                    pl.IsDefault,
                    pl.PPAddress,
                    pl.PPBank,
                    pl.PPCompany,
                    pl.PPDesc,
                    pl.PPEmail,
                    pl.PPFax,
                    pl.PPGoods,
                    pl.PPID,
                    pl.PPMan,
                    pl.PPName,
                    pl.PPState,
                    pl.PPTel,
                    pl.PPUrl
                }).ToListAsync());
            }
        }
        //根据Id查询供应商
        public async Task<IHttpActionResult> Get_ProductLend_SelectById_Async(string id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.ProductLend
                    .Where(pl => pl.PPID == id)
                    .Select(pl => new
                    {
                        pl.IsDefault,
                        pl.PPAddress,
                        pl.PPBank,
                        pl.PPCompany,
                        pl.PPDesc,
                        pl.PPEmail,
                        pl.PPFax,
                        pl.PPGoods,
                        pl.PPID,
                        pl.PPMan,
                        pl.PPName,
                        pl.PPState,
                        pl.PPTel,
                        pl.PPUrl
                    }).FirstOrDefaultAsync());


            }
        }
        //供应商分页条件查询
        public async Task<IHttpActionResult> Get_ProductLend_SelectWherePage_Async(int limit, int offset, string order, string name)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new
                {
                    total = await db.ProductLend
                        .Where(pl => pl.PPState == 1)
                        .Where(pl => string.IsNullOrEmpty(name) || pl.PPName.Contains(name) || pl.PPCompany.Contains(name))
                        .CountAsync(),
                    rows = await db.ProductLend
                        .Where(p => p.PPState == 1)
                        .Where(pl => string.IsNullOrEmpty(name) || pl.PPName.Contains(name) || pl.PPCompany.Contains(name))
                        .Select(pl => new
                        {
                            pl.IsDefault,
                            pl.PPAddress,
                            pl.PPBank,
                            pl.PPCompany,
                            pl.PPDesc,
                            pl.PPEmail,
                            pl.PPFax,
                            pl.PPGoods,
                            pl.PPID,
                            pl.PPMan,
                            pl.PPName,
                            pl.PPState,
                            pl.PPTel,
                            pl.PPUrl
                        })
                        .OrderByDescending(pl => pl.PPID).Skip(offset).Take(limit).ToListAsync()
                });
            }
        }
        //判断公司名称是否重复
        public async Task<IHttpActionResult> Get_ProductLend_IsExistPPCompany_Async(string PPID, string PPCompany)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new { valid = await db.ProductLend.Where(pl => string.IsNullOrEmpty(PPID) ? (pl.PPCompany == PPCompany) : (pl.PPCompany == PPCompany && pl.PPID.ToString() != PPID)).CountAsync() <= 0 });
            }
        }
        public async Task<IHttpActionResult> Post_ProductLend_Add_Async(ProductLend productLend)
        {
            using (PSSEntities db = new PSSEntities())
            {
                ObjectParameter no = new ObjectParameter("no","");
                db.proc_ProductLend(no);
                productLend.PPID = no.Value.ToString();
                productLend.PPState = 1;
                db.ProductLend.Add(productLend);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //修改供应商
        public async Task<IHttpActionResult> Put_ProductLend_Edit_Async(ProductLend productLend)
        {
            using (PSSEntities db = new PSSEntities())
            {
                productLend.PPState = 1;
                db.Entry<ProductLend>(productLend).State = EntityState.Modified;
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //修改供应商
        public async Task<IHttpActionResult> Delete_ProductLend_Delete_Async(string id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                ProductLend productLend = db.ProductLend.Find(id);
                if (productLend == null)
                {
                    return Ok(0);
                }
                db.ProductLend.Remove(productLend);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
    }
}
