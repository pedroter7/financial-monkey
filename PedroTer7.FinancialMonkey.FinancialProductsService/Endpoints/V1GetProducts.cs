using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
}
