using DailyDolce.Services.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DailyDolce.Services.Identity.Data {
    public class DataContext : IdentityDbContext<User> {

        public DataContext(DbContextOptions<DataContext> options) : base(options) {

        }
    }
}
