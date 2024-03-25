using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PedroTer7.FinancialMonkey.AuthService.Services;
using PedroTer7.FinancialMonkey.AuthService.ViewModels;

namespace PedroTer7.FinancialMonkey.AuthService.Endpoints;

public static class V1Endpoints
{
    public static void Map(WebApplication app)
    {
        var routeGroup = app.MapGroup("/api/v1");

        routeGroup
            .MapPost("/admin/auth", AdminAuth)
            .WithOpenApi();

        routeGroup
            .MapPost("/auth", CustomerAuth)
            .WithOpenApi();
    }

    private static async Task<Results<Ok<TokenViewModel>, ValidationProblem>> AdminAuth(
            [FromBody] AuthViewModel authData,
            [FromServices] IValidator<AuthViewModel> validator,
            [FromServices] ITokenService tokenService)
    {
        var validationResults = validator.Validate(authData);
        if (!validationResults.IsValid)
        {
            return TypedResults.ValidationProblem(validationResults.ToDictionary());
        }

        return TypedResults.Ok(await tokenService.GetAdminToken(authData.Email, authData.Password));
    }

    private static async Task<Results<Ok<TokenViewModel>, ValidationProblem>> CustomerAuth(
            [FromBody] AuthViewModel authData,
            [FromServices] IValidator<AuthViewModel> validator,
            [FromServices] ITokenService tokenService)
    {
        var validationResults = validator.Validate(authData);
        if (!validationResults.IsValid)
        {
            return TypedResults.ValidationProblem(validationResults.ToDictionary());
        }

        return TypedResults.Ok(await tokenService.GetCustomerToken(authData.Email, authData.Password));
    }
}
