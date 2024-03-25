using PedroTer7.FinancialMonkey.AuthService.ViewModels;

namespace PedroTer7.FinancialMonkey.AuthService.Services;

public interface ITokenService
{
    /// <summary>
    /// Requests token from ID Server for admin users.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    /// <exception cref="UnauthorizedAccessException">When credentials are invalid</exception>
    Task<TokenViewModel> GetAdminToken(string email, string password);

    /// <summary>
    /// Requests token from ID Server for customer users.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    /// <exception cref="UnauthorizedAccessException">When credentials are invalid</exception>
    Task<TokenViewModel> GetCustomerToken(string email, string password);
}
