using FluentValidation;

namespace PedroTer7.FinancialMonkey.AuthService.ViewModels;

public class AuthViewModelValidator : AbstractValidator<AuthViewModel>
{
    public AuthViewModelValidator()
    {
        RuleFor(v => v.Email).NotEmpty().EmailAddress();
        RuleFor(v => v.Password).NotEmpty();
    }
}
