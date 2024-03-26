using PedroTer7.FinancialMonkey.FinancialProductsService.Entities;

namespace PedroTer7.FinancialMonkey.FinancialProductsService.ViewModels;

public class FinancialProductOutViewModel : FinancialProductInViewModel
{
    public FinancialProductOutViewModel() : base() { }

    public FinancialProductOutViewModel(FinancialProduct p) : base(p)
    {
        Id = p.Id;
        CreationDateTime = p.CreationDateTime;
    }

    public string Id { get; set; } = null!;
    public DateTime CreationDateTime { get; set; }
}
