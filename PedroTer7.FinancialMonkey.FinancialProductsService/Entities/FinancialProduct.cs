namespace PedroTer7.FinancialMonkey.FinancialProductsService.Entities;

public abstract class FinancialProduct : BaseEntity
{
    public virtual FinancialProductType ProductType { get; set; }
    public string Name { get; set; } = null!;
    public decimal MinimalAmount { get; set; } = 0;
    public Currency Currency { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DateTime OpenDate { get; set; }
    public uint LiquidityInDays { get; set; }
    public float? InterestPerYear { get; set; }
    public string Code { get; set; } = null!;
    public bool FixedInterest { get; set; }
    public string InterestRule { get; set; } = null!;
}
