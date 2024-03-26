using FluentValidation;
using PedroTer7.FinancialMonkey.FinancialProductsService.Entities;

namespace PedroTer7.FinancialMonkey.FinancialProductsService.ViewModels;

public class FinancialProductInViewModelValidator : AbstractValidator<FinancialProductInViewModel>
{
    public FinancialProductInViewModelValidator()
    {
        RuleFor(p => p.ProductType)
            .NotEmpty()
            .IsEnumName(typeof(FinancialProductType), false);

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(p => p.Currency)
            .NotNull()
            .IsEnumName(typeof(Currency), true);

        RuleFor(p => p.ExpirationDate)
            .GreaterThan(p => p.OpenDate)
            .WithMessage("{PropertyName} must be greater than open date");

        RuleFor(p => p.OpenDate)
            .LessThan(p => p.ExpirationDate)
            .WithMessage("{PropertyName} must be less than expiration date");

        RuleFor(p => p.FixedInterest)
            .NotNull()
            .WithMessage($"{{PropertyType}} must not be null when product is of type {FinancialProductType.Investments}")
            .When(p => GetType(p) == FinancialProductType.Investments);

        RuleFor(p => p.InterestPerYear)
            .NotNull()
            .WithMessage($"{{PropertyName}} must be provided when financial product has fixed interest rate or is of type {FinancialProductType.Savings}")
            .When(p => p.FixedInterest == true || GetType(p) == FinancialProductType.Savings);

        RuleFor(p => p.Code)
            .NotNull()
            .WithMessage($"{{PropertyName}} must be provided when product is of type {FinancialProductType.Investments}")
            .MaximumLength(15)
            .When(p => GetType(p) == FinancialProductType.Investments);

        RuleFor(p => p.InterestRule)
            .NotNull()
            .WithMessage("{PropertyName} must be provided when product doesn't have a fixed interest rate")
            .MaximumLength(20)
            .When(p => GetType(p) == FinancialProductType.Investments && p.FixedInterest != true);
    }

    private static FinancialProductType? GetType(FinancialProductInViewModel p)
        => Enum.TryParse<FinancialProductType>(p.ProductType, out var pType) ? pType : null;
}
