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
    //入库订单
    public class StockInDepotController : ApiController
    {
        public void Options() { }
        //根据Id查询入库订单
        public async Task<IHttpActionResult> Get_StockInDepot_SelectById_Async(string id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.StockInDepot.Where(sd => sd.SIDID == id).Select(s => new
                {
                    s.StockID,
                    s.PPID,
                    s.ProductLend.PPName,
                    s.ProductLend.PPCompany,
                    s.DepotID,
                    s.Depots.DepotName,
                    s.SIDData,
                    s.SIDDate,
                    s.SIDDesc,
                    s.SIDDeliver,
                    s.SIDID,
                    s.SIDUser,
                    s.Users.UsersName,
                    s.SIDFreight
                }).FirstOrDefaultAsync());
            }
        }
        //修改采购入库单状态
        public async Task<IHttpActionResult> Get_StockInDepot_EditStockInDepoState_Async(int SIDData, string SIDID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                StockInDepot stockInDepot = db.StockInDepot.Find(SIDID);
                stockInDepot.SIDData = SIDData;
                //生成入库记录单
                InOutDepot InOutDepot = new InOutDepot()
                {
                    DepotID = stockInDepot.DepotID,
                    IODNum = stockInDepot.SIDID,
                    IODUser = stockInDepot.SIDUser,
                    IODDate = DateTime.Now,
                    IODType = 1
                };
                db.InOutDepot.Add(InOutDepot);
                decimal InOutDepotId = new PSSEntities().Database.SqlQuery<decimal>("SELECT IDENT_CURRENT('InOutDepot') + IDENT_INCR('InOutDepot')").SingleOrDefault();
                foreach (StockInDepotDetail item in stockInDepot.StockInDepotDetail)
                {
                    //生成入库祥单
                    InOutDepotDetail InOutDepotDetail = new InOutDepotDetail()
                    {
                        IODID= Convert.ToInt32(InOutDepotId),
                        ProID= item.ProID,
                        IODDAmount=item.SIDDAmount,
                        IODDPrice=item.SIDDPrice
                    };
                    db.InOutDepotDetail.Add(InOutDepotDetail);
                    DepotStock ds = await db.DepotStock.Where(d => d.DepotID == stockInDepot.DepotID && d.DSPrice == item.SIDDPrice && d.ProID == item.ProID).FirstOrDefaultAsync();
                    if (ds==null)
                    {
                        //添加到库存
                        DepotStock depotStock = new DepotStock()
                        {
                            DepotID = stockInDepot.DepotID,
                            ProID = item.ProID,
                            DSAmount = item.SIDDAmount,
                            DSPrice = item.SIDDPrice
                        };
                        db.DepotStock.Add(depotStock);
                    }
                    else
                    {
                        ds.DSAmount = ds.DSAmount + item.SIDDAmount;
                    }
                }

                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //入库订单条件查询分页
        public async Task<IHttpActionResult> Get_StockInDepot_SelectPage_Async(int limit, int offset, string order, string PPName)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new
                {
                    total = await db.StockInDepot
                        .Where(s => string.IsNullOrEmpty(PPName) || s.ProductLend.PPName == PPName)
                        .CountAsync(),
                    rows = await db.StockInDepot
                        .Where(s => string.IsNullOrEmpty(PPName) || s.ProductLend.PPName == PPName)
                        .Select(s => new
                        {
                            s.StockID,
                            s.PPID,
                            s.ProductLend.PPName,
                            s.ProductLend.PPCompany,
                            s.DepotID,
                            s.Depots.DepotName,
                            s.SIDData,
                            s.SIDDate,
                            s.SIDDesc,
                            s.SIDDeliver,
                            s.SIDID,
                            s.SIDUser,
                            s.Users.UsersName,
                            s.SIDFreight

                        })
                    .OrderByDescending(s => s.StockID).Skip(offset).Take(limit).ToListAsync()
                });
            }
        }
        //添加入库单
        public async Task<IHttpActionResult> Post_StockInDepot_Add_Async(StockInDepot stockInDepot)
        {
            using (PSSEntities db = new PSSEntities())
            {
                ObjectParameter no = new ObjectParameter("no", "");
                db.proc_StockInDepot(no);
                stockInDepot.SIDID = no.Value.ToString();
                stockInDepot.SIDData = 1;
                foreach (StockInDepotDetail item in stockInDepot.StockInDepotDetail)
                {
                    item.SIDID = stockInDepot.SIDID;
                }
                db.StockInDepot.Add(stockInDepot);
                db.StockInDepotDetail.AddRange(stockInDepot.StockInDepotDetail);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //修改入库单
        public async Task<IHttpActionResult> Put_StockInDepot_Edit_Async(StockInDepot stockInDepot)
        {
            using (PSSEntities db = new PSSEntities())
            {
                StockInDepot s = await db.StockInDepot.FindAsync(stockInDepot.SIDID);
                s.PPID = stockInDepot.PPID;
                s.DepotID = stockInDepot.DepotID;
                s.StockID = stockInDepot.StockID;
                s.SIDDate = stockInDepot.SIDDate;
                s.SIDDeliver = stockInDepot.SIDDeliver;
                s.SIDFreight = stockInDepot.SIDFreight;
                db.Entry<StockInDepot>(s).State = EntityState.Modified;
                foreach (StockInDepotDetail item in stockInDepot.StockInDepotDetail)
                {
                    item.SIDID = stockInDepot.SIDID;
                }
                db.StockInDepotDetail.AddRange(stockInDepot.StockInDepotDetail);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //删除入库单
        public async Task<IHttpActionResult> Delete_StockInDepot_Delete_Async(string id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                var StockInDepot = db.StockInDepot.Find(id);
                if (StockInDepot == null || StockInDepot.SIDData != 1)
                {
                    return Ok(false);
                }
                db.StockInDepotDetail.RemoveRange(db.StockInDepotDetail.Where(s => s.SIDID == StockInDepot.SIDID));
                db.StockInDepot.Remove(StockInDepot);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
    }
}
