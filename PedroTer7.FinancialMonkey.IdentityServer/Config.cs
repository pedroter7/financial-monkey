using Duende.IdentityServer.Models;

namespace PedroTer7.FinancialMonkey.IdentityServer;

public static class Config
{
    public static IEnumerable<ApiScope> ApiScopes =>
        [new ApiScope { Name = "admin", DisplayName = "Admin user" , UserClaims = ["uid"]},
         new ApiScope { Name = "customer", Description = "Customer user", UserClaims = ["uid"] }];

}