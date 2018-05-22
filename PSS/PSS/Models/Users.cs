using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSS.Models
{
    //用户
    public class Users
    {
        public int UsersID { get; set; }
        public string UsersName { get; set; }
        public string UserLoginName { get; set; }
        public string UserLoginPwd { get; set; }
    }
}