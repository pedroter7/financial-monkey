using PedroTer7.FinancialMonkey.AuthService.Endpoints;
using PedroTer7.FinancialMonkey.AuthService.Options;
using System.Text.Json;
using FluentValidation;
using PedroTer7.FinancialMonkey.AuthService.ViewModels;
using PedroTer7.FinancialMonkey.AuthService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.Services
    .AddOptions<ClientCredentialsOptions>()
    .Bind(builder.Configuration.GetSection(ClientCredentialsOptions.Key))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services
    .AddOptions<IdentityServerOptions>()
    .Bind(builder.Configuration.GetSection(IdentityServerOptions.Key))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddHttpClient();

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.ConfigureHttpJsonOptions(opts =>
{
    opts.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.AddValidatorsFromAssemblyContaining<AuthViewModel>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(cfg => cfg.WithMethods("POST").AllowAnyOrigin().AllowAnyHeader());

app.Use(async (ctx, next) =>
{
    try
    {
        await next(ctx);
    }
    catch (UnauthorizedAccessException)
    {
        ctx.Response.Clear();
        ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return;
    }
});

V1Endpoints.Map(app);

app.Run();
