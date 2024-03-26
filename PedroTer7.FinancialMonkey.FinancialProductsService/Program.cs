using PedroTer7.FinancialMonkey.Common;
using PedroTer7.FinancialMonkey.FinancialProductsService.Endpoints;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSerilog();
builder.Services.AddCors();
builder.Services.AddFinancialMonkeySwagger();
builder.Services.AddFinancialMonkeyAuthentication();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(opts => opts.AllowAnyOrigin().AllowAnyHeader().WithMethods("GET", "POST", "PUT", "DELETE"));
app.UseAuthentication();
app.UseAuthorization();
app.UseFinancialMonkeyExceptionHandlingMiddleware();
V1Endpoints.Map(app);

app.Run();