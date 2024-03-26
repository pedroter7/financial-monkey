namespace PedroTer7.FinancialMonkey.FinancialProductsService;

public class PaginatedViewModel<T>
{
    public string? NextId { get; set; }
    public uint PageSize { get; set; }
    public T? Value { get; set; }
}
