using PedroTer7.FinancialMonkey.Common;

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
            .RequireAuthorization(AuthConfig.CustomerAuthPolicy)
            .WithTags("Customer");

        routes.MapGet("/products", GetProductsEndpoint);
        routes.MapGet("/products/{id}", GetProductEndpoint);
    }

    private static void MapAdminRoutes(RouteGroupBuilder v1RouteGroup)
    {
        var routes = v1RouteGroup
            .MapGroup("/admin")
            .RequireAuthorization(AuthConfig.AdminAuthPolicy)
            .WithTags("Admin");

        routes.MapGet("/products", GetProductsEndpoint);
        routes.MapGet("/products/{id}", GetProductEndpoint);
        routes.MapPost("/products", CreateProductEndpoint);
        routes.MapPut("/products/{id}", UpdateProductEndpoint);
        routes.MapDelete("/products/{id}", DeleteProductEndpoint);
    }
}
