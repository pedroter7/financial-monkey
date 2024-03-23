using Microsoft.EntityFrameworkCore;
using PedroTer7.FinancialMonkey.IdentityServer.Entities;

namespace PedroTer7.FinancialMonkey.IdentityServer.Contexts;

public class CredentialsContext(DbContextOptions<CredentialsContext> contextOptions) : DbContext(contextOptions)
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<ClientCredential> ClientCredentials { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        new CustomerConfiguration().Configure(builder.Entity<Customer>());
        new AdminConfiguration().Configure(builder.Entity<Admin>());
        new ClientCredentialConfiguration().Configure(builder.Entity<ClientCredential>());
    }
}
