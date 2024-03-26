using PedroTer7.FinancialMonkey.FinancialProductsService.Entities;

namespace PedroTer7.FinancialMonkey.FinancialProductsService.Repository;

public interface IFinancialProductsRepository
{
    /// <summary>
    /// Gets a page of entries from data store. Pages are ordered by id.
    /// </summary>
    /// <param name="firstId">Requested page first ID</param>
    /// <param name="pageSize"></param>
    /// <returns>A Tuple containing the requested page and the first ID of the next page, if it exists</returns>
    /// <exception cref="ArgumentException"></exception>
    Task<Tuple<ICollection<FinancialProduct>, string?>> GetProductsPage(string? firstId, uint pageSize);
    Task<FinancialProduct?> GetProduct(string id);
    Task<FinancialProduct> CreateProduct(FinancialProduct product);
    Task<FinancialProduct?> UpdateProduct(string id, FinancialProduct product);
    Task<FinancialProduct> Delete(string id);
}
