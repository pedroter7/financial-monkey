using System.Text.Json.Serialization;

namespace PedroTer7.FinancialMonkey.AuthService.ViewModels;

public class TokenViewModel
{
    public string Token { get; set; } = null!;
    public int Lifetime { get; set; }
    public string Type { get; set; } = null!;
}
