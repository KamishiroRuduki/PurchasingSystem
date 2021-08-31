using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PurchasingSystem.ORM.DBModels
{
    public partial class ContextModel : DbContext
    {
        public ContextModel()
            : base("name=DefaultConnectionString")
        {
        }

        public virtual DbSet<Commodity> Commodities { get; set; }
        public virtual DbSet<CostData> CostDatas { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<UserInfo> UserInfoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CostData>()
                .Property(e => e.Value)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Manager>()
                .Property(e => e.Account)
                .IsUnicode(false);

            modelBuilder.Entity<Manager>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.PriceSum)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Order>()
                .Property(e => e.CashRate)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.PurchasingCost)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.Account)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.PWD)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.MobilePhone)
                .IsUnicode(false);
        }
    }
}
