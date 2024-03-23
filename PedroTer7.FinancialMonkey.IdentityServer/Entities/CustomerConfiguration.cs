using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PedroTer7.FinancialMonkey.IdentityServer.Entities;

public class CustomerConfiguration : BaseUserCredentialConfiguration<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        base.Configure(builder);
        builder.HasData([
            new Customer
            {
                Id = 1,
                CreationDateTime = DateTime.Now,
                Email = "customer_1@gmail.com",
                Password = PasswordUtil.HashPassword("$customer1", PasswordUtil.GenerateSalt())
            },
            new Customer
            {
                Id = 2,
                CreationDateTime = DateTime.Now,
                Email = "customer_2@somecorp.com",
                Password = PasswordUtil.HashPassword("$customer2", PasswordUtil.GenerateSalt())
            },
            new Customer
            {
                Id = 3,
                CreationDateTime = DateTime.Now,
                Email = "customer_3@thirdcopr.com",
                Password = PasswordUtil.HashPassword("$customer3", PasswordUtil.GenerateSalt())
            }
        ]);
    }
}
