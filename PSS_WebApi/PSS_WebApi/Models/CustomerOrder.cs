//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace PSS_WebApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomerOrder
    {
        public string COID { get; set; }
        public string CusID { get; set; }
        public Nullable<System.DateTime> CODate { get; set; }
        public Nullable<System.DateTime> CORefDate { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> COState { get; set; }
        public string CODesc { get; set; }
    }
}
