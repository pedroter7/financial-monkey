using IdentityModel.Client;
using Microsoft.Extensions.Options;
using PedroTer7.FinancialMonkey.AuthService.Options;
using PedroTer7.FinancialMonkey.AuthService.ViewModels;

namespace PedroTer7.FinancialMonkey.AuthService.Services;

internal class TokenService(IHttpClientFactory httpClientFactory, IOptions<IdentityServerOptions> identityServerOptions,
    IOptions<ClientCredentialsOptions> clientCredentialsOptions) : ITokenService
{
    private static DiscoveryDocumentResponse? _discoveryDocumentCache = null;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly IOptions<IdentityServerOptions> _idServerOptions = identityServerOptions;
    private readonly IOptions<ClientCredentialsOptions> _clientCredentialsOptions = clientCredentialsOptions;

    private HttpClient CreateHttpClient() => _httpClientFactory.CreateClient();

    public Task<TokenViewModel> GetAdminToken(string email, string password)
    {
        return GetToken(_clientCredentialsOptions.Value.AdminAuth, email, password);
    }

    public Task<TokenViewModel> GetCustomerToken(string email, string password)
    {
        return GetToken(_clientCredentialsOptions.Value.CustomerAuth, email, password);
    }

    private async Task<TokenViewModel> GetToken(ClientCredentials clientCredentials, string email, string password)
    {
        var client = CreateHttpClient();
        var tokenRequest = await CreateTokenRequest(clientCredentials, email, password);
        var resp = await client.RequestPasswordTokenAsync(tokenRequest);
        AssertTokenResponseIsPositive(resp);
        return CreateTokenViewModel(resp);
    }

    private static void AssertTokenResponseIsPositive(TokenResponse resp)
    {
        if (resp.IsError)
        {
            if (resp.Error == "invalid_grant")
                throw new UnauthorizedAccessException("Invalid credentials");

            throw new UnauthorizedAccessException("Error requesting token. See inner exception for details.",
                resp.Exception ?? new Exception("Got error but Exception was empty."));
        }
    }

    private async Task<PasswordTokenRequest> CreateTokenRequest(ClientCredentials clientCredentials, string email, string password)
        => new()
        {
            Address = await GetTokenEndpoint(),
            ClientId = clientCredentials.Id,
            ClientSecret = clientCredentials.Secret,
            UserName = email,
            Password = password
        };

    private static TokenViewModel CreateTokenViewModel(TokenResponse resp)
        => new()
        {
            Token = resp.AccessToken ?? "",
            Type = resp.TokenType ?? "",
            Lifetime = resp.ExpiresIn
        };

    private async Task<string> GetTokenEndpoint()
    {
        if (_discoveryDocumentCache is null)
        {
            var httpClient = CreateHttpClient();
            var resp = await httpClient.GetDiscoveryDocumentAsync(_idServerOptions.Value.BaseUrl);
            if (resp.IsError)
            {
                throw new Exception($"Error getting discovery document from ID Server. Exception message: {resp.Exception?.Message}");
            }
            _discoveryDocumentCache = resp;
        }

        return _discoveryDocumentCache.TokenEndpoint ??
            throw new Exception($"Could not get {nameof(DiscoveryDocumentResponse.TokenEndpoint)} from {nameof(_discoveryDocumentCache)}");
    }
}
