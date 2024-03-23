namespace PedroTer7.FinancialMonkey.IdentityServer.Entities;

public class BaseUserCredential
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime CreationDateTime { get; set; }
}
