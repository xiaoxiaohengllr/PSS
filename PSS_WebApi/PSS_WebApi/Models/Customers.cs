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
    
    public partial class Customers
    {
        public string CusID { get; set; }
        public Nullable<int> CLID { get; set; }
        public string CusName { get; set; }
        public string CusCompany { get; set; }
        public string CusMan { get; set; }
        public string CusDesc { get; set; }
        public Nullable<int> CTID { get; set; }
        public string CusTel { get; set; }
        public string CusAddress { get; set; }
        public string CusPhone { get; set; }
        public string CusFax { get; set; }
        public string CusMail { get; set; }
        public string CusUrl { get; set; }
        public string CusBank { get; set; }
        public string CusBankCreate { get; set; }
        public string CusGoogs { get; set; }
        public string CusSGoogs { get; set; }
        public string CusRateType { get; set; }
        public Nullable<double> CusBeginIn { get; set; }
        public Nullable<int> CusSorce { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        public Nullable<int> CusState { get; set; }
    
        public virtual CustomerLevel CustomerLevel { get; set; }
        public virtual CustomerTypes CustomerTypes { get; set; }
    }
}
