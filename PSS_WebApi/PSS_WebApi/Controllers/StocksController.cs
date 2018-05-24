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
    //采购单
    public class StocksController : ApiController
    {
        public void Options() { }
        //判断采购单是否有供应商
        public async Task<IHttpActionResult> Get_Stocks_IsExistProductLend_Async(string PPID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Stocks.Where(s => s.PPID == PPID).CountAsync() > 0);
            }
        }
        //根据Id查询采购订单
        public async Task<IHttpActionResult> Get_Stocks_SelectById_Async(string id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Stocks.Where(s => s.StockID == id).Select(s => new
                {
                    s.StockID,
                    s.PPID,
                    s.StockInDate,
                    s.StockState,
                    s.StockUser,
                    s.StockDate,
                    s.Users.UsersName,
                    s.ProductLend.PPName,
                    s.ProductLend.PPCompany
                }).FirstOrDefaultAsync());
            }
        }
        //修改采购单状态
        public async Task<IHttpActionResult> Get_Stocks_EditStockState_Async(int StockState,string StockID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                Stocks stock = db.Stocks.Find(StockID);
                stock.StockState = StockState;
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //采购单条件查询分页
        public async Task<IHttpActionResult> Get_Stocks_SelectPage_Async(int limit, int offset, string order, string PPName)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new
                {
                    total = await db.Stocks.Where(s => string.IsNullOrEmpty(PPName) || s.ProductLend.PPName == PPName).CountAsync(),
                    rows = await db.Stocks.Where(s => string.IsNullOrEmpty(PPName) || s.ProductLend.PPName == PPName).Select(s => new
                    {
                        s.StockID,
                        s.PPID,
                        s.StockInDate,
                        s.StockState,
                        s.StockUser,
                        s.StockDate,
                        s.Users.UsersName,
                        s.ProductLend.PPName,
                        s.ProductLend.PPCompany
                    })
                    .OrderByDescending(s => s.StockDate).Skip(offset).Take(limit).ToListAsync()
                });
            }
        }
        //添加采购单
        public async Task<IHttpActionResult> Post_Stocks_Add_Async(Stocks Stock)
        {
            using (PSSEntities db = new PSSEntities())
            {
                ObjectParameter no = new ObjectParameter("no", "");
                db.proc_StID(no);
                Stock.StockID = no.Value.ToString();
                Stock.StockState = 1;
                Stock.StockDate = DateTime.Now;
                foreach (StockDetail item in Stock.StockDetail)
                {
                    item.StockID = Stock.StockID;
                    item.SDetailDepAmount = 0;
                }
                db.Stocks.Add(Stock);
                db.StockDetail.AddRange(Stock.StockDetail);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //修改采购单
        public async Task<IHttpActionResult> Put_Stocks_Edit_Async(Stocks Stock)
        {
            using (PSSEntities db = new PSSEntities())
            {
                Stocks s = await db.Stocks.FindAsync(Stock.StockID);
                s.PPID = Stock.PPID;
                s.StockInDate = Stock.StockInDate;
                db.Entry<Stocks>(s).State = EntityState.Modified;
                foreach (StockDetail item in Stock.StockDetail)
                {
                    item.StockID = s.StockID;
                    item.SDetailDepAmount = 0;
                }
                db.StockDetail.AddRange(Stock.StockDetail);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //删除采购单
        public async Task<IHttpActionResult> Delete_Stocks_Delete_Async(string id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                var Stocks = db.Stocks.Find(id);
                if (Stocks == null || Stocks.StockState != 1)
                {
                    return Ok(false);
                }
                db.StockDetail.RemoveRange(db.StockDetail.Where(sd => sd.StockID == Stocks.StockID));
                db.Stocks.Remove(Stocks);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
    }
}
