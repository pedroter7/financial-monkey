using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PedroTer7.FinancialMonkey.FinancialProductsService.Entities;
using PedroTer7.FinancialMonkey.FinancialProductsService.Repository;
using PedroTer7.FinancialMonkey.FinancialProductsService.ViewModels;


namespace PedroTer7.FinancialMonkey.FinancialProductsService.Endpoints;

public static partial class V1Endpoints
{
    internal static async Task<Ok<PaginatedViewModel<IEnumerable<FinancialProductOutViewModel>>>> GetProductsEndpoint(
        [FromServices] IFinancialProductsRepository repository,
        [FromQuery] string? nextId = null,
        [FromQuery] uint pageSize = 10
    )
    {
        var res = await repository.GetProductsPage(nextId, pageSize);
        return TypedResults.Ok(new PaginatedViewModel<IEnumerable<FinancialProductOutViewModel>>()
        {
            Value = res.Item1.Select(p => new FinancialProductOutViewModel(p)),
            NextId = res.Item2
        });
    }

    internal static async Task<Results<Ok<FinancialProductOutViewModel>, NotFound>> GetProductEndpoint(
        [FromServices] IFinancialProductsRepository repository,
        [FromRoute] string id
    )
    {
        var res = await repository.GetProduct(id);
        if (res is null) return TypedResults.NotFound();
        return TypedResults.Ok(new FinancialProductOutViewModel(res));
    }

    internal async static Task<Results<Created<FinancialProductOutViewModel>, ValidationProblem>> CreateProductEndpoint(
        [FromBody] FinancialProductInViewModel model,
        [FromServices] FinancialProductInViewModelValidator validator,
        [FromServices] IFinancialProductsRepository repository
    )
    {
        var validationResult = validator.Validate(model);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(validationResult.ToDictionary());
        }

        var res = await repository.CreateProduct(new FinancialProduct(model));
        return TypedResults.Created($"/products/{res.Id}", new FinancialProductOutViewModel(res));
    }

    internal async static Task<Results<Ok<FinancialProductOutViewModel>, ValidationProblem, NotFound>> UpdateProductEndpoint(
        [FromBody] FinancialProductInViewModel model,
        [FromRoute] string id,
        [FromServices] FinancialProductInViewModelValidator validator,
        [FromServices] IFinancialProductsRepository repository
    )
    {
        var validationResult = validator.Validate(model);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(validationResult.ToDictionary());
        }

        var res = await repository.UpdateProduct(id, new FinancialProduct(model));
        if (res is null) return TypedResults.NotFound();
        return TypedResults.Ok(new FinancialProductOutViewModel(res));
    }

    internal static async Task<Results<Ok<FinancialProductOutViewModel>, NotFound>> DeleteProductEndpoint(
        [FromRoute] string id,
        [FromServices] IFinancialProductsRepository repository
    )
    {
        var result = await repository.Delete(id);
        if (result is null) return TypedResults.NotFound();
        return TypedResults.Ok(new FinancialProductOutViewModel(result));
    }
}
