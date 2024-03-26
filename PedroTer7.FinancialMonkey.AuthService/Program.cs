using PedroTer7.FinancialMonkey.AuthService.Endpoints;
using PedroTer7.FinancialMonkey.AuthService.Options;
using System.Text.Json;
using FluentValidation;
using PedroTer7.FinancialMonkey.AuthService.ViewModels;
using PedroTer7.FinancialMonkey.AuthService.Services;
using Serilog;
using PedroTer7.FinancialMonkey.Common;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSerilog();

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

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(cfg => cfg.WithMethods("POST").AllowAnyOrigin().AllowAnyHeader());
app.UseFinancialMonkeyExceptionHandlingMiddleware();
V1Endpoints.Map(app);

app.Run();
