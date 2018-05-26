using PSS_WebApi.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace PSS_WebApi.Controllers
{
    //采购退货单
    public class StockReturnController : ApiController
    {
        public void Options() { }
        //根据Id查询采购退货单
        public async Task<IHttpActionResult> Get_StockReturn_SelectById_Async(string id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.StockReturn.Where(sr => sr.SRID == id).Select(sr => new
                {
                    sr.SRID,
                    sr.SIDID,
                    sr.SRCreateTime,
                    sr.SRDate,
                    sr.SRDesc,
                    sr.SRState,
                    sr.SRUser,
                    sr.Users.UsersName
                }).FirstOrDefaultAsync());
            }
        }
        //修改退货订单状态
        public async Task<IHttpActionResult> Get_StockReturn_EditStockInSRState_Async(int SRState, string SRID)
        {
            SRState = 2;
            using (PSSEntities db = new PSSEntities())
            {
                StockReturn stockReturn = db.StockReturn.Find(SRID);
                //修改状态为已审核
                stockReturn.SRState = SRState;
                //生成出入库记录单
                InOutDepot InOutDepot = new InOutDepot()
                {
                    DepotID = stockReturn.StockInDepot.DepotID,
                    IODNum = stockReturn.SRID,
                    IODUser = stockReturn.SRUser,
                    IODDate = DateTime.Now,
                    IODType = 2
                };
                db.InOutDepot.Add(InOutDepot);
                //获取出入库记录单编号
                decimal InOutDepotId = new PSSEntities().Database.SqlQuery<decimal>("SELECT IDENT_CURRENT('InOutDepot') + IDENT_INCR('InOutDepot')").SingleOrDefault();
                foreach (StockReturnDetail item in stockReturn.StockReturnDetail)
                {
                    //生成出入库记录详单
                    InOutDepotDetail InOutDepotDetail = new InOutDepotDetail()
                    {
                        IODID = Convert.ToInt32(InOutDepotId),
                        ProID = item.ProID,
                        IODDAmount = item.SRDAmount,
                        IODDPrice = item.SRDPrice
                    };
                    db.InOutDepotDetail.Add(InOutDepotDetail);
                    //从库存中出库
                    DepotStock ds = await db.DepotStock.Where(d => d.DepotID == stockReturn.StockInDepot.DepotID && d.ProID == item.ProID).FirstOrDefaultAsync();
                    if (ds!=null)
                    {
                        //减少库存数量
                        ds.DSAmount = ds.DSAmount - item.SRDAmount;
                    }
                    else
                    {
                        return Ok(0);
                    }
                }
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //条件查询退货单分页
        public async Task<IHttpActionResult> Get_StockReturn_SelectWherePage_Async(int limit, int offset, string order, string SIDID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(new
                {
                    total = await db.StockReturn
                        .Where(sr => string.IsNullOrEmpty(SIDID) || sr.SIDID.Contains(SIDID))
                        .CountAsync(),
                    rows = await db.StockReturn
                        .Where(sr => string.IsNullOrEmpty(SIDID) || sr.SIDID.Contains(SIDID))
                        .Select(sd => new
                        {
                            sd.SRID,
                            sd.SIDID,
                            sd.SRCreateTime,
                            sd.SRDate,
                            sd.SRDesc,
                            sd.SRState,
                            sd.SRUser,
                            sd.Users.UsersName
                        })
                        .OrderByDescending(d => d.SRID).Skip(offset).Take(limit).ToListAsync()
                });
            }
        }
        //添加退货单
        public async Task<IHttpActionResult> Post_StockReturn_Add_Async(StockReturn stockReturn)
        {
            using (PSSEntities db = new PSSEntities())
            {
                ObjectParameter no = new ObjectParameter("no", "");
                db.proc_StockOutDepot(no);
                stockReturn.SRID = no.Value.ToString();
                stockReturn.SRCreateTime = DateTime.Now;
                stockReturn.SRState = 1;
                db.StockReturn.Add(stockReturn);
                foreach (StockReturnDetail item in stockReturn.StockReturnDetail)
                {
                    item.SRID = no.Value.ToString();

                }
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //修改退货单
        public async Task<IHttpActionResult> Put_StockReturn_Edit_Async(StockReturn stockReturn)
        {
            using (PSSEntities db = new PSSEntities())
            {

                StockReturn sr = await db.StockReturn.FindAsync(stockReturn.SRID);
                sr.SIDID = stockReturn.SIDID;
                sr.SRDate = stockReturn.SRDate;
                db.Entry<StockReturn>(sr).State = EntityState.Modified;
                foreach (StockReturnDetail item in stockReturn.StockReturnDetail)
                {
                    item.SRID = sr.SRID;
                }
                db.StockReturnDetail.AddRange(stockReturn.StockReturnDetail);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
        //删除退货单
        public async Task<IHttpActionResult> Delete_StockReturn_Delete_Async(string id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                var StockReturn = db.StockReturn.Find(id);
                if (StockReturn == null || StockReturn.SRState != 1)
                {
                    return Ok(false);
                }
                db.StockReturnDetail.RemoveRange(db.StockReturnDetail.Where(s => s.SRID == StockReturn.SRID));
                db.StockReturn.Remove(StockReturn);
                return Ok(await db.SaveChangesAsync() > 0);
            }
        }
    }
}
