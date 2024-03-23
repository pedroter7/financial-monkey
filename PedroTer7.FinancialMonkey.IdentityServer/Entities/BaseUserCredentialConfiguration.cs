using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PedroTer7.FinancialMonkey.IdentityServer.Entities;

public abstract class BaseUserCredentialConfiguration<T> : IEntityTypeConfiguration<T>
where T : BaseUserCredential
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder
            .Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(150);
        builder
            .HasIndex(u => u.Email)
            .IsUnique();
        builder
            .Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(9 + 1 + 64); // salt + : + sha256
        builder
            .Property(u => u.CreationDateTime)
            .IsRequired()
            .HasDefaultValueSql("UTC_TIMESTAMP(0)");
    }
}
