using Microsoft.EntityFrameworkCore;
using PedroTer7.FinancialMonkey.IdentityServer.Contexts;
using Serilog;

namespace PedroTer7.FinancialMonkey.IdentityServer;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var mariaDbVersion = new MariaDbServerVersion(new Version(11, 0));
        var idServerConnString = builder.Configuration.GetConnectionString("IdentityServer");
        builder.Services.AddDbContext<CredentialsContext>(options =>
        {
            options.UseMySql(idServerConnString, mariaDbVersion)
                .LogTo(Console.WriteLine)
                .EnableDetailedErrors();
        });

        builder.Services.AddIdentityServer()
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddClientStore<ClientStore>()
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
            .AddInMemoryCaching();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.UseIdentityServer();
        return app;
    }
}
