using DailyDolce.Services.ProductApi.Model;
using Microsoft.EntityFrameworkCore;

namespace DailyDolce.Services.ProductApi.Data {
    public class DataContext : DbContext {
        public DbSet<Product> Products { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product>().HasData(
                new Product {
                    Id = 1,
                    Name = "Pumpkin Cupcake",
                    Price = 15,
                    Description = "Come procuratori e che liberalita che lui sue spezial di impermutabile avvien sempre son tale sua dico prieghi manifesta noi",
                    ImageUrl = "https://gisramosrod.blob.core.windows.net/dailydolce/1.jpg",
                    CategoryName = "Cupcake"
                });
            modelBuilder.Entity<Product>().HasData(
               new Product {
                   Id = 2,
                   Name = "Vanilla Cookies",
                   Price = 15,
                   Description = "Ma giudice in nostri il carissime seguendo essaudisce per i i ignoranza e giudicio del di che viviamo forse nome",
                   ImageUrl = "https://gisramosrod.blob.core.windows.net/dailydolce/2.jpg",
                   CategoryName = "Cookie"
               });
            modelBuilder.Entity<Product>().HasData(
                new Product {
                    Id = 3,
                    Name = "Taiyaki",
                    Price = 15,
                    Description = "Nome piú senza allo degli dalla prieghi i alcun alcun furon sí manifesta di primo cosí potra vostro quale cose",
                    ImageUrl = "https://gisramosrod.blob.core.windows.net/dailydolce/3.jpg",
                    CategoryName = "Cake"
                });
            modelBuilder.Entity<Product>().HasData(
                new Product {
                    Id = 4,
                    Name = "Strawberry Cake",
                    Price = 15,
                    Description = "Vita mortali suo il da nella accio essaudisce del tanto nome coloro del per e le e quale manifestamente tutte",
                    ImageUrl = "https://gisramosrod.blob.core.windows.net/dailydolce/4.jpg",
                    CategoryName = "Cake"
                });
        }

    }
}
