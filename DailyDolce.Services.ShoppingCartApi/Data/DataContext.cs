using DailyDolce.Services.ShoppingCartApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyDolce.Services.ShoppingCartApi.Data {
    public class DataContext : DbContext {

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CartModel> Carts { get; set; }
        public DbSet<CartProductModel> CartProducts { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) {

        }
    }
}
