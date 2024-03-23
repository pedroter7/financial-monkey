namespace PedroTer7.FinancialMonkey.IdentityServer.Entities;

public class ClientCredential
{
    public int Id { get; set; }
    public string Key { get; set; } = null!;
    public string Secret { get; set; } = null!;
    public string AllowedGrantTypes { get; set; } = null!;
    public string AllowedScopes { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime CreationDateTime { get; set; }
}
