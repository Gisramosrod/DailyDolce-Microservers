using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace DailyDolce.Services.Identity {
    public static class SD {

        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
             new List<ApiScope> {
                new ApiScope("dailyDolce", "Daily Dolce Server"),
                new ApiScope(name: "read", displayName:"Read your data."),
                new ApiScope(name: "write", displayName:"Write your data."),
                new ApiScope(name: "delete", displayName:"Delete your data.")
             };

        public static IEnumerable<Client> Clients =>
            new List<Client> {
                new Client() {
                    ClientId="dailyDolce",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.Code,
                    RedirectUris={"https://localhost:7268/signin-oidc"},
                    PostLogoutRedirectUris={"https://localhost:7268/signout-callback-oidc"},
                    AllowedScopes=new List<string> (){
                        "dailyDolce",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email
                    }
                }
            };
    }
}
