using Microsoft.AspNetCore.Mvc;
using PedroTer7.FinancialMonkey.Common;
using PedroTer7.FinancialMonkey.FinancialProductsService.ViewModels;

namespace PedroTer7.FinancialMonkey.FinancialProductsService.Endpoints;

public static partial class V1Endpoints
{
    public static void Map(WebApplication app)
    {
        var v1RouteGroup = app.MapGroup("/api/v1").WithOpenApi();
        MapCustomerRoutes(v1RouteGroup);
        MapAdminRoutes(v1RouteGroup);
    }

    private static void MapCustomerRoutes(RouteGroupBuilder v1RouteGroup)
    {
        var routes = v1RouteGroup
            .MapGroup("/customer")
            .RequireAuthorization(AuthConfig.CustomerAuthPolicy);

        routes.MapGet("/products", GetProductsEndpoint);
        routes.MapGet("/products/{id}", ([FromRoute] string id) => "Ok, Customer");
    }

    private static void MapAdminRoutes(RouteGroupBuilder v1RouteGroup)
    {
        var routes = v1RouteGroup
            .MapGroup("/admin")
            .RequireAuthorization(AuthConfig.AdminAuthPolicy);

        routes.MapGet("/products", GetProductsEndpoint);
        routes.MapGet("/products/{id}", ([FromRoute] string id) => "Ok, admin");
        routes.MapPost("/products", ([FromBody] FinancialProductInViewModel product) => "Ok, admin");
        routes.MapPut("/products/{id}", ([FromRoute] string id, [FromBody] FinancialProductInViewModel product) => $"Ok, {id}, {product}");
        routes.MapDelete("/products/{id}", ([FromRoute] string id) => $"Ok, {id}");
    }
}
