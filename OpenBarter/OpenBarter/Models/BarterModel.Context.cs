﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenBarter.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Category> Categories { get; set; }
        public DbSet<ForTrade> ForTrades { get; set; }
        public DbSet<ForTradeStatus> ForTradeStatuses { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<UserData> UserDatas { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Want> Wants { get; set; }
        public DbSet<WantStatus> WantStatuses { get; set; }
        public DbSet<OfferForTrade> OfferForTrades { get; set; }
        public DbSet<OfferForWant> OfferForWants { get; set; }
    }
}