using DailyDolce.Services.Identity.Models;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Security.Claims;

namespace DailyDolce.Services.Identity.Services {
    public class ProfileService : IProfileService {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;

        public ProfileService(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory) {

            _userManager = userManager;
            _roleManager = roleManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context) {

            string sub = context.Subject.GetSubjectId();
            User user = await _userManager.FindByIdAsync(sub);
            ClaimsPrincipal userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);

            List<Claim> claims = userClaims.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));
            claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));

            if (_userManager.SupportsUserRole) {
                IList<string> roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles) claims.Add(new Claim(JwtClaimTypes.Role, role));
            }

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context) {
            string sub = context.Subject.GetSubjectId();
            User user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
