using DailyDolce.Services.CouponApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyDolce.Services.CouponApi.Data {
    public class DataContext : DbContext {
        public DbSet<CouponModel> Coupons { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CouponModel>().HasData(
                new CouponModel {
                    Id = 1,
                    CouponCode = "10OFF",
                    Discount = 10
                });

            modelBuilder.Entity<CouponModel>().HasData(
               new CouponModel {
                   Id = 2,
                   CouponCode = "20OFF",
                   Discount = 20
               });
        }
    }
}
