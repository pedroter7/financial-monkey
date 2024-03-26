using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PedroTer7.FinancialMonkey.IdentityServer.Entities;

public class ClientCredentialConfiguration : IEntityTypeConfiguration<ClientCredential>
{
    public void Configure(EntityTypeBuilder<ClientCredential> builder)
    {
        builder
            .Property(cc => cc.AllowedGrantTypes)
            .IsRequired()
            .HasMaxLength(500);
        builder
            .Property(cc => cc.AllowedScopes)
            .IsRequired()
            .HasMaxLength(500);
        builder
            .Property(cc => cc.Key)
            .IsRequired()
            .HasMaxLength(64);
        builder
            .HasIndex(cc => cc.Key)
            .IsUnique();
        builder
            .Property(cc => cc.Secret)
            .IsRequired()
            .HasMaxLength(64);
        builder
            .Property(cc => cc.Name)
            .IsRequired()
            .HasMaxLength(25);
        builder
            .Property(cc => cc.Description)
            .IsRequired()
            .HasMaxLength(100);
        builder
            .Property(cc => cc.CreationDateTime)
            .IsRequired()
            .HasDefaultValueSql("UTC_TIMESTAMP(0)");

        AddSeedData(builder);
    }

    private static void AddSeedData(EntityTypeBuilder<ClientCredential> builder)
    {
        builder.HasData([
            new ClientCredential
            {
                Id = 1,
                CreationDateTime = DateTime.Now,
                AllowedGrantTypes = "password",
                AllowedScopes = "admin",
                Description = "Client for admin users authentication",
                Key = "6f65a068-e943-11ee-b79b-03fb35a7a73f",
                Name = "Admin auth client",
                Secret = "9297320e-e943-11ee-882f-bf8013a44c6f"
            },
            new ClientCredential
            {
                Id = 2,
                CreationDateTime = DateTime.Now,
                AllowedGrantTypes = "password",
                AllowedScopes = "customer",
                Description = "Client for customer users authentication",
                Key = "ca32537e-e943-11ee-b968-2f83f8d52888",
                Name = "Customer auth client",
                Secret = "d0dec608-e943-11ee-ad8c-532040233037"
            },
        ]);
    }
}
