﻿using PSS_WebApi.Models;
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
    //采购入库单详单
    public class StockInDepotDetailController : ApiController
    {
        public void Options() { }
        //根据SIDID查询入采购入库单详单
        public async Task<IHttpActionResult> Get_StockInDepotDetail_SelectBySIDID_Async(string SIDID)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.StockInDepotDetail.Where(s => s.SIDID == SIDID).Select(s => new
                {
                    s.ProID,
                    s.SIDDAmount,
                    s.SIDDDesc,
                    s.SIDDID,
                    s.SIDDPrice,
                    s.SIDID,
                    s.Products.ProName,
                    s.Products.ProductTypes.PTName,
                    s.Products.ProductUnit.PUName,
                    s.Products.ProductSpec.PSName,
                    s.Products.ProductColor.PCName
                }).ToListAsync());
            }
        }
        //判断商品是否全部入库
        public async Task<IHttpActionResult> Get_StockInDepotDetail_SelectById_Async(string SIDID, string SS)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.StockInDepotDetail.Where(s => s.SIDID == SIDID).CountAsync() == db.StockInDepot.Find(SIDID).Stocks.StockDetail.Count());
            }
        }
    }
}
