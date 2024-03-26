using Bogus;
using PedroTer7.FinancialMonkey.FinancialProductsService.Entities;
using PedroTer7.FinancialMonkey.FinancialProductsService.ViewModels;

namespace PedroTer7.FinancialMonkey.FinancialProductsService.Tests;

public static class MockFactory
{
    public static FinancialProductInViewModel CreateValidInViewModel()
    {
        var faker = new Faker();
        var type = faker.PickRandom<FinancialProductType>();
        var code = type is FinancialProductType.Investments ? faker.Random.AlphaNumeric(faker.Random.Int(1, 15)) : null;
        var fixedInterest = type is FinancialProductType.Investments ? (bool?)faker.Random.Bool() : null;
        var interestPerYear = fixedInterest is true || type is FinancialProductType.Savings ? (float?)faker.Random.Float(0.1f, 100f) : null;
        var interestRule = fixedInterest is false ? faker.Random.AlphaNumeric(faker.Random.Int(1, 20)) : null;
        var openDate = faker.Date.Future(1);

        return new FinancialProductInViewModel()
        {
            Code = code,
            Currency = faker.PickRandom<Currency>().ToString(),
            ExpirationDate = openDate.AddMonths(faker.Random.Int(1, 30)),
            FixedInterest = fixedInterest,
            InterestPerYear = interestPerYear,
            InterestRule = interestRule,
            LiquidityInDays = (uint)faker.Random.Int(0, 45),
            MinimalAmount = faker.Finance.Amount(),
            Name = faker.Random.AlphaNumeric(faker.Random.Int(1, 100)),
            OpenDate = openDate,
            ProductType = type.ToString()
        };
    }

    public static FinancialProductInViewModel CreateInvalidInViewModel()
    {
        var faker = new Faker();

        return new FinancialProductInViewModel()
        {
            Code = faker.Random.AlphaNumeric(50),
            Currency = faker.Random.AlphaNumeric(50),
            ExpirationDate = faker.Date.Future(1),
            FixedInterest = null,
            InterestPerYear = null,
            InterestRule = faker.Random.AlphaNumeric(50),
            LiquidityInDays = (uint)faker.Random.Int(0, 45),
            MinimalAmount = faker.Finance.Amount(),
            Name = faker.Random.AlphaNumeric(200),
            OpenDate = faker.Date.Future(10),
            ProductType = faker.Random.AlphaNumeric(50)
        };
    }
}
