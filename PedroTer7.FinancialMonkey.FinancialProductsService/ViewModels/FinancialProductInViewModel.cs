using PedroTer7.FinancialMonkey.FinancialProductsService.Entities;

namespace PedroTer7.FinancialMonkey.FinancialProductsService.ViewModels;

public class FinancialProductInViewModel
{
    public FinancialProductInViewModel() { }

    public FinancialProductInViewModel(FinancialProduct p)
    {
        ProductType = p.ProductType.ToString();
        Name = p.Name;
        MinimalAmount = p.MinimalAmount;
        Currency = p.Currency.ToString();
        ExpirationDate = p.ExpirationDate;
        OpenDate = p.OpenDate;
        LiquidityInDays = p.LiquidityInDays;
        FixedInterest = p.FixedInterest;
        InterestPerYear = p.InterestPerYear;
        Code = p.Code;
        InterestRule = p.InterestRule;
    }

    public string ProductType { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal MinimalAmount { get; set; }
    public string Currency { get; set; } = null!;
    public DateTime ExpirationDate { get; set; }
    public DateTime OpenDate { get; set; }
    public uint LiquidityInDays { get; set; }
    public bool? FixedInterest { get; set; }
    public float? InterestPerYear { get; set; }
    public string? Code { get; set; }
    public string? InterestRule { get; set; }
}
