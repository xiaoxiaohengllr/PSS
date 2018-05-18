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
    //银行表
    public class BankController : ApiController
    {
        public void Options() { }
        //查询所有银行
        public async Task<IHttpActionResult> Get_Bank_SelectAll_Async()
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Bank.Select(b => new
                {
                    b.BankAddress,
                    b.BankFullName,
                    b.BankID,
                    b.BankMan,
                    b.BankMoney,
                    b.BankName,
                    b.BankState,
                    b.BankTel,
                    b.IsDefault
                }).ToListAsync());
            }
        }
        //根据Id查询银行
        public async Task<IHttpActionResult> Get_Bank_SelectById_Async(string id)
        {
            using (PSSEntities db = new PSSEntities())
            {
                return Ok(await db.Bank
                    .Where(b => b.BankID == id)
                    .Select(b => new
                    {
                        b.BankAddress,
                        b.BankFullName,
                        b.BankID,
                        b.BankMan,
                        b.BankMoney,
                        b.BankName,
                        b.BankState,
                        b.BankTel,
                        b.IsDefault
                    }).FirstOrDefaultAsync());
            }
        }
    }
}
