﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shop.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SHOPEntities : DbContext
    {
        public SHOPEntities()
            : base("name=SHOPEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<ProductImg> ProductImg { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<ProductTypeF> ProductTypeF { get; set; }
        public DbSet<ProductTypeS> ProductTypeS { get; set; }
        public DbSet<vw_Product> vw_Product { get; set; }
    }
}
