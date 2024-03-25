using System.ComponentModel.DataAnnotations;

namespace PedroTer7.FinancialMonkey.AuthService.Options;

public class ClientCredentialsOptions
{
    public const string Key = "ClientCredentials";

    [Required]
    public ClientCredentials AdminAuth { get; set; } = null!;

    [Required]
    public ClientCredentials CustomerAuth { get; set; } = null!;
}

public class ClientCredentials
{
    [Required(AllowEmptyStrings = false)]
    public string Id { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Secret { get; set; } = null!;
}
