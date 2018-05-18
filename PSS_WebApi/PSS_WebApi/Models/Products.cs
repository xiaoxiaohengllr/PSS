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
    
    public partial class Products
    {
        public int ProID { get; set; }
        public Nullable<int> PTID { get; set; }
        public Nullable<int> PUID { get; set; }
        public Nullable<int> PCID { get; set; }
        public Nullable<int> PSID { get; set; }
        public string ProName { get; set; }
        public string ProJP { get; set; }
        public string ProTM { get; set; }
        public string ProWorkShop { get; set; }
        public Nullable<int> ProMax { get; set; }
        public Nullable<int> ProMin { get; set; }
        public Nullable<decimal> ProInPrice { get; set; }
        public Nullable<decimal> ProPrice { get; set; }
        public string ProDesc { get; set; }
        public Nullable<int> ProState { get; set; }
        public byte[] ProImg { get; set; }
    
        public virtual ProductColor ProductColor { get; set; }
        public virtual ProductSpec ProductSpec { get; set; }
        public virtual ProductTypes ProductTypes { get; set; }
        public virtual ProductUnit ProductUnit { get; set; }
    }
}
