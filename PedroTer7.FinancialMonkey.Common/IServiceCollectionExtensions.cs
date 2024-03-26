using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace PedroTer7.FinancialMonkey.Common;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddFinancialMonkeyAuthentication(this IServiceCollection services)
    {
        var authority = AuthConfig.AuthAuthority;

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(cfg =>
            {
                cfg.Authority = authority;
                cfg.TokenValidationParameters.ValidateAudience = false;
                cfg.RequireHttpsMetadata = false;
                cfg.IncludeErrorDetails = true;
            });

        services
            .AddAuthorizationBuilder()
            .AddPolicy(AuthConfig.AdminAuthPolicy,
                policy => policy.RequireAuthenticatedUser().RequireClaim("uid").RequireClaim("scope", "admin"))
            .AddPolicy(AuthConfig.CustomerAuthPolicy,
                policy => policy.RequireClaim("uid").RequireAssertion(context => HasScope("customer", context.User)));

        return services;
    }

    private static bool HasScope(string scope, ClaimsPrincipal user)
        => user.HasClaim(c => c.Type == "scope" && c.Value.Split(' ').Contains(scope));

    // Got some of this from https://dev.to/moe23/net-6-minimal-api-authentication-jwt-with-swagger-and-open-api-2chh
    public static IServiceCollection AddFinancialMonkeySwagger(this IServiceCollection services)
    {
        var securityScheme = new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JSON Web Token based security",
        };

        var securityReq = new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                },
                Array.Empty<string>()
            }
        };

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(o =>
        {
            o.AddSecurityDefinition("Bearer", securityScheme);
            o.AddSecurityRequirement(securityReq);
        });

        return services;
    }
}
