using DailyDolce.Service.OrderApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace DailyDolce.Service.OrderApi.Data {
    public class DataContext : DbContext {
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderUserModel> OrderUsers { get; set; }
        public DbSet<OrderProductModel> OrderProducts { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) {

        }
    }
}
