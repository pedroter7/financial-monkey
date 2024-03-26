namespace PedroTer7.FinancialMonkey.Common;

public static class AuthConfig
{
    public static string AuthAuthority => Environment.GetEnvironmentVariable("AUTH_AUTHORITY")
        ?? throw new Exception("Could not find enviroment variable AUTH_AUTHORITY needed to setup auth");

    public const string AdminAuthPolicy = "admin";
    public const string CustomerAuthPolicy = "customer";
}
