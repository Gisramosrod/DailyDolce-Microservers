using DailyDolce.Services.Identity.Data;
using DailyDolce.Services.Identity.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Security.Claims;

namespace DailyDolce.Services.Identity.Initializer {
    public class DbInitializer : IDbInitializer {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            DataContext context, 
            UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager) {

            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize() {

            if (_roleManager.FindByNameAsync(SD.Admin).Result != null) return;

            User adminUser = new User() {
                UserName = "admin1@gmail.com",
                Email = "admin1@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "111111111",
                FirstName = "Bilbo",
                LastName = "Baggins"
            };
            InitializeUser(adminUser, SD.Admin);

            User customerUser = new User() {
                UserName = "customer1@gmail.com",
                Email = "customer1@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "111111111",
                FirstName = "Gandalf",
                LastName = "The Grey"
            };
            InitializeUser(customerUser, SD.Customer);
        }

        private void InitializeUser(User user, string role) {

            _roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
            _userManager.CreateAsync(user, "User123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(user, role).GetAwaiter().GetResult();

            var tempUser = _userManager.AddClaimsAsync(user, new List<Claim>() {
                new Claim(JwtClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(JwtClaimTypes.GivenName, user.FirstName),
                new Claim(JwtClaimTypes.FamilyName, user.LastName),
                new Claim(JwtClaimTypes.Role, role)
            }).Result;
        }
    }
}
