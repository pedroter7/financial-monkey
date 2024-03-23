using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PedroTer7.FinancialMonkey.IdentityServer.Entities;

public class AdminConfiguration : BaseUserCredentialConfiguration<Admin>
{
    public override void Configure(EntityTypeBuilder<Admin> builder)
    {
        base.Configure(builder);
        builder.HasData([
            new Admin{
                Id = 1,
                CreationDateTime = DateTime.Now,
                Email = "adm1@financialmonkey.com",
                Password = PasswordUtil.HashPassword("$admin1", PasswordUtil.GenerateSalt())
            },
            new Admin{
                Id = 2,
                CreationDateTime = DateTime.Now,
                Email = "adm2@financialmonkey.com",
                Password = PasswordUtil.HashPassword("$admin2", PasswordUtil.GenerateSalt())
            }
        ]);
    }
}
