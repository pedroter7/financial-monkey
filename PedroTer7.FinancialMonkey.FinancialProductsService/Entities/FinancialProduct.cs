using PedroTer7.FinancialMonkey.FinancialProductsService.ViewModels;

namespace PedroTer7.FinancialMonkey.FinancialProductsService.Entities;

public class FinancialProduct : BaseEntity
{
    public FinancialProduct() { }

    public FinancialProduct(FinancialProductInViewModel inViewModel)
    {
        ProductType = Enum.Parse<FinancialProductType>(inViewModel.ProductType);
        Name = inViewModel.Name;
        MinimalAmount = inViewModel.MinimalAmount;
        Currency = Enum.Parse<Currency>(inViewModel.Currency);
        ExpirationDate = inViewModel.ExpirationDate;
        OpenDate = inViewModel.OpenDate;
        LiquidityInDays = inViewModel.LiquidityInDays;
        InterestPerYear = inViewModel.InterestPerYear;
        Code = inViewModel.Code;
        FixedInterest = inViewModel.FixedInterest ?? false;
        InterestRule = inViewModel.InterestRule;
    }

    public virtual FinancialProductType ProductType { get; set; }
    public string Name { get; set; } = null!;
    public decimal MinimalAmount { get; set; } = 0;
    public Currency Currency { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DateTime OpenDate { get; set; }
    public uint LiquidityInDays { get; set; }
    public float? InterestPerYear { get; set; }
    public string? Code { get; set; } = null!;
    public bool FixedInterest { get; set; }
    public string? InterestRule { get; set; } = null!;
}
