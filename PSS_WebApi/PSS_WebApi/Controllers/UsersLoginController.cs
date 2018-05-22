using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using PSS_WebApi.Models;

namespace PSS_WebApi.Controllers
{
    //用户登录接口
    public class UsersLoginController : ApiController
    {
        public void Options() { }
        //用户登录
        public async Task<IHttpActionResult> Post_Users_Login_Async(dynamic obj)
        {
            //获取登录提交的对象
            string UserLoginName = obj.UserLoginName;
            string UserLoginPwd = obj.UserLoginPwd;
            using (PSSEntities db = new PSSEntities())
            {
                //判断用户是否登录成功
                Users user = await db.Users.Where(u => u.UserLoginName == UserLoginName).Where(u => u.UserLoginPwd == UserLoginPwd).FirstOrDefaultAsync();
                return Ok(user != null ? new { user.UserLoginName, user.UsersID, user.UsersName, user.UserLoginPwd } : null);
            }
        }
    }
}
