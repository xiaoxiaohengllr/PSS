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
    
    public partial class InOutDepot
    {
        public int IODID { get; set; }
        public string DepotID { get; set; }
        public Nullable<int> IODType { get; set; }
        public string IODNum { get; set; }
        public Nullable<System.DateTime> IODDate { get; set; }
        public Nullable<int> IODUser { get; set; }
        public string IODDesc { get; set; }
    }
}
