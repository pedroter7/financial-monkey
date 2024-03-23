using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using Microsoft.EntityFrameworkCore;
using PedroTer7.FinancialMonkey.IdentityServer.Contexts;

namespace PedroTer7.FinancialMonkey.IdentityServer;

public class ClientStore(CredentialsContext credentialsContext) : IClientStore
{
    private readonly CredentialsContext _credentialsContext = credentialsContext;

    public async Task<Client?> FindClientByIdAsync(string clientId)
    {
        var c = await _credentialsContext
                    .ClientCredentials
                    .AsNoTracking()
                    .Where(c => c.Key == clientId)
                    .FirstOrDefaultAsync();

        if (c is null) return null;

        return new Client
        {
            AllowedGrantTypes = c.AllowedGrantTypes.Split(' '),
            AllowedScopes = c.AllowedScopes.Split(' '),
            ClientId = c.Key,
            ClientSecrets = [new Secret(c.Secret.Sha256())],
            ClientName = c.Name,
            Description = c.Description
        };
    }
}
