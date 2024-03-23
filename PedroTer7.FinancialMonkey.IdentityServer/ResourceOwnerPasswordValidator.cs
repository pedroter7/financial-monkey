using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using Microsoft.EntityFrameworkCore;
using PedroTer7.FinancialMonkey.IdentityServer.Contexts;
using PedroTer7.FinancialMonkey.IdentityServer.Entities;

namespace PedroTer7.FinancialMonkey.IdentityServer;

public class ResourceOwnerPasswordValidator(CredentialsContext credentialsContext) : IResourceOwnerPasswordValidator
{
    private readonly CredentialsContext _credentialsContext = credentialsContext;

    private enum AuthScope
    {
        Admin,
        Customer,
        None
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var authScope = GetScope(context);
        var success = await AuthenticateAsync(context.UserName, context.Password, authScope);
        if (success)
        {
            context.Result = await BuildSuccessGrantValidationResultAsync(context.UserName, authScope);
        }
        else
        {
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid credentials");
        }
    }

    private static AuthScope GetScope(ResourceOwnerPasswordValidationContext context)
    {
        var requestedScopes = context.Request.RequestedScopes;
        if (requestedScopes?.Contains("admin") is true) return AuthScope.Admin;
        else if (requestedScopes?.Contains("customer") is true) return AuthScope.Customer;
        return AuthScope.None;
    }

    private Task<bool> AuthenticateAsync(string email, string password, AuthScope scope)
        => scope switch
        {
            AuthScope.Admin => AuthenticateAdminAsync(email, password),
            AuthScope.Customer => AuthenticateCustomerAsync(email, password),
            _ => Task.FromResult(false)
        };

    private Task<GrantValidationResult> BuildSuccessGrantValidationResultAsync(string email, AuthScope scope)
        => scope switch
        {
            AuthScope.Admin => BuildAdminGrantValidationResult(email),
            AuthScope.Customer => BuildCustomerGrantValidationResult(email),
            _ => throw new ArgumentException($"Invalid {nameof(AuthScope)}", nameof(scope))
        };

    private async Task<GrantValidationResult> BuildAdminGrantValidationResult(string email)
    {
        var adm = await GetCredentialAsync(email, _credentialsContext.Admins.AsQueryable()) as Admin
            ?? throw new ArgumentException($"Could not find admin with email {email}", nameof(email));

        return new GrantValidationResult(adm.Id.ToString(), "password");
    }

    private async Task<GrantValidationResult> BuildCustomerGrantValidationResult(string email)
    {
        var customer = await GetCredentialAsync(email, _credentialsContext.Customers.AsQueryable()) as Customer
            ?? throw new ArgumentException($"Could not find customer with email {email}", nameof(email));

        return new GrantValidationResult(customer.Id.ToString(), "password");
    }

    private Task<bool> AuthenticateAdminAsync(string email, string password)
    {
        return AuthenticateBaseUserCredential(email, password, _credentialsContext.Admins.AsQueryable());
    }

    private Task<bool> AuthenticateCustomerAsync(string email, string password)
    {
        return AuthenticateBaseUserCredential(email, password, _credentialsContext.Customers.AsQueryable());
    }

    private static async Task<bool> AuthenticateBaseUserCredential(string email, string password, IQueryable<BaseUserCredential> collection)
    {
        var cred = await GetCredentialAsync(email, collection);
        if (cred is null) return false;
        return PasswordsMatch(password, cred.Password);
    }

    private static bool PasswordsMatch(string rawAttemptedPassword, string hashedPassword)
    {
        try
        {
            var salt = PasswordUtil.GetSalt(hashedPassword);
            var attemptedPasswordHash = PasswordUtil.HashPassword(rawAttemptedPassword, salt);
            return attemptedPasswordHash == hashedPassword;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    private static Task<BaseUserCredential?> GetCredentialAsync(string email, IQueryable<BaseUserCredential> collection)
    {
        return collection.FirstOrDefaultAsync(c => c.Email == email);
    }
}
