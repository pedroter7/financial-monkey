using Duende.IdentityServer.Models;

namespace PedroTer7.FinancialMonkey.IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        [new IdentityResources.OpenId()];

    public static IEnumerable<ApiScope> ApiScopes =>
        [new ApiScope { Name = "admin", DisplayName = "Admin user" },
         new ApiScope { Name = "customer", Description = "Customer user" },
         new ApiScope { Name = "someapi", Description = "Some API" }];

}