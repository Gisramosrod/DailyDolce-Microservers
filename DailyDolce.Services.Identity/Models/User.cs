using Microsoft.AspNetCore.Identity;

namespace DailyDolce.Services.Identity.Models {
    public class User : IdentityUser {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
