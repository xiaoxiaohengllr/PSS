﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PSSEntities : DbContext
    {
        public PSSEntities()
            : base("name=PSSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bank> Bank { get; set; }
        public virtual DbSet<CheckDepot> CheckDepot { get; set; }
        public virtual DbSet<CheckDepotDetail> CheckDepotDetail { get; set; }
        public virtual DbSet<Compose> Compose { get; set; }
        public virtual DbSet<ComposeDetail> ComposeDetail { get; set; }
        public virtual DbSet<CustomerLevel> CustomerLevel { get; set; }
        public virtual DbSet<CustomerOrder> CustomerOrder { get; set; }
        public virtual DbSet<CustomerOrderDetail> CustomerOrderDetail { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<CustomerTypes> CustomerTypes { get; set; }
        public virtual DbSet<Depots> Depots { get; set; }
        public virtual DbSet<DepotStock> DepotStock { get; set; }
        public virtual DbSet<Dept> Dept { get; set; }
        public virtual DbSet<DevolveDetail> DevolveDetail { get; set; }
        public virtual DbSet<Devolves> Devolves { get; set; }
        public virtual DbSet<Emp> Emp { get; set; }
        public virtual DbSet<InOutDepot> InOutDepot { get; set; }
        public virtual DbSet<InOutDepotDetail> InOutDepotDetail { get; set; }
        public virtual DbSet<LostDetail> LostDetail { get; set; }
        public virtual DbSet<Losts> Losts { get; set; }
        public virtual DbSet<OtherInDepot> OtherInDepot { get; set; }
        public virtual DbSet<OtherInDepotDetail> OtherInDepotDetail { get; set; }
        public virtual DbSet<OtherOutDepot> OtherOutDepot { get; set; }
        public virtual DbSet<OtherOutDepotDetail> OtherOutDepotDetail { get; set; }
        public virtual DbSet<PayOffDetail> PayOffDetail { get; set; }
        public virtual DbSet<PayOffs> PayOffs { get; set; }
        public virtual DbSet<PopedomRole> PopedomRole { get; set; }
        public virtual DbSet<Popedoms> Popedoms { get; set; }
        public virtual DbSet<ProduceInDepot> ProduceInDepot { get; set; }
        public virtual DbSet<ProduceInDepotDeteil> ProduceInDepotDeteil { get; set; }
        public virtual DbSet<ProduceOutDepot> ProduceOutDepot { get; set; }
        public virtual DbSet<ProduceOutDepotDetail> ProduceOutDepotDetail { get; set; }
        public virtual DbSet<ProductColor> ProductColor { get; set; }
        public virtual DbSet<ProductLend> ProductLend { get; set; }
        public virtual DbSet<ProductLendTypes> ProductLendTypes { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ProductSpec> ProductSpec { get; set; }
        public virtual DbSet<ProductTypes> ProductTypes { get; set; }
        public virtual DbSet<ProductUnit> ProductUnit { get; set; }
        public virtual DbSet<QuotePrice> QuotePrice { get; set; }
        public virtual DbSet<QuotePriceDetail> QuotePriceDetail { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SaleDepot> SaleDepot { get; set; }
        public virtual DbSet<SaleDepotDetail> SaleDepotDetail { get; set; }
        public virtual DbSet<SaleReturn> SaleReturn { get; set; }
        public virtual DbSet<SaleReturnDetail> SaleReturnDetail { get; set; }
        public virtual DbSet<SplitDetail> SplitDetail { get; set; }
        public virtual DbSet<Splits> Splits { get; set; }
        public virtual DbSet<StockDetail> StockDetail { get; set; }
        public virtual DbSet<StockInDepot> StockInDepot { get; set; }
        public virtual DbSet<StockInDepotDetail> StockInDepotDetail { get; set; }
        public virtual DbSet<StockOutDepot> StockOutDepot { get; set; }
        public virtual DbSet<StockOutDepotDetail> StockOutDepotDetail { get; set; }
        public virtual DbSet<Stocks> Stocks { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersRole> UsersRole { get; set; }
        public virtual DbSet<StockReturn> StockReturn { get; set; }
        public virtual DbSet<StockReturnDetail> StockReturnDetail { get; set; }
    
        public virtual int proc_CFNO(ObjectParameter no)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_CFNO", no);
        }
    
        public virtual int proc_common_create_flowNo(string tableName, string pkNo, string prx, ObjectParameter no)
        {
            var tableNameParameter = tableName != null ?
                new ObjectParameter("tableName", tableName) :
                new ObjectParameter("tableName", typeof(string));
    
            var pkNoParameter = pkNo != null ?
                new ObjectParameter("pkNo", pkNo) :
                new ObjectParameter("pkNo", typeof(string));
    
            var prxParameter = prx != null ?
                new ObjectParameter("prx", prx) :
                new ObjectParameter("prx", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_common_create_flowNo", tableNameParameter, pkNoParameter, prxParameter, no);
        }
    
        public virtual int proc_CPNO(ObjectParameter no)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_CPNO", no);
        }
    
        public virtual int proc_CusOrder(ObjectParameter no)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_CusOrder", no);
        }
    
        public virtual int proc_Cust_no(ObjectParameter no)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_Cust_no", no);
        }
    
        public virtual int proc_Customers(ObjectParameter no)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_Customers", no);
        }
    
        public virtual int proc_Depots(ObjectParameter no)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_Depots", no);
        }
    
        public virtual int proc_Losts(ObjectParameter no)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_Losts", no);
        }
    
        public virtual int proc_PayOffs(ObjectParameter no)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_PayOffs", no);
        }
    
        public virtual int proc_ProductLend(ObjectParameter no)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_ProductLend", no);
        }
    
        public virtual int proc_ProID(ObjectParameter no)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_ProID", no);
        }
    
        public virtual int proc_SaleReturn(ObjectParameter no)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_SaleReturn", no);
        }
    
        public virtual int proc_SCNO(ObjectParameter no)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_SCNO", no);
        }
    
        public virtual int proc_SDNO(ObjectParameter no)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_SDNO", no);
        }
    
        public virtual int proc_SLNO(ObjectParameter no)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_SLNO", no);
        }
    
        public virtual int proc_StID(ObjectParameter no)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_StID", no);
        }
    
        public virtual int proc_StockInDepot(ObjectParameter no)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_StockInDepot", no);
        }
    
        public virtual int proc_StockOutDepot(ObjectParameter no)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_StockOutDepot", no);
        }
    
        public virtual int PS_proc_CommonPager(string tblName, string fldName, Nullable<int> pageSize, Nullable<int> pageIndex, Nullable<bool> isCount, Nullable<bool> orderType, string strWhere)
        {
            var tblNameParameter = tblName != null ?
                new ObjectParameter("tblName", tblName) :
                new ObjectParameter("tblName", typeof(string));
    
            var fldNameParameter = fldName != null ?
                new ObjectParameter("fldName", fldName) :
                new ObjectParameter("fldName", typeof(string));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var isCountParameter = isCount.HasValue ?
                new ObjectParameter("IsCount", isCount) :
                new ObjectParameter("IsCount", typeof(bool));
    
            var orderTypeParameter = orderType.HasValue ?
                new ObjectParameter("OrderType", orderType) :
                new ObjectParameter("OrderType", typeof(bool));
    
            var strWhereParameter = strWhere != null ?
                new ObjectParameter("strWhere", strWhere) :
                new ObjectParameter("strWhere", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PS_proc_CommonPager", tblNameParameter, fldNameParameter, pageSizeParameter, pageIndexParameter, isCountParameter, orderTypeParameter, strWhereParameter);
        }
    }
}
