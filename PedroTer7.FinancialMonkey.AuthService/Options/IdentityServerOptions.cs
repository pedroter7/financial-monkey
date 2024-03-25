using System.ComponentModel.DataAnnotations;

namespace PedroTer7.FinancialMonkey.AuthService.Options;

public class IdentityServerOptions
{
    public const string Key = "IdentityServer";

    [Required(AllowEmptyStrings = false)]
    public string BaseUrl { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string AdminScope { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string CustomerScope { get; set; } = null!;
}
