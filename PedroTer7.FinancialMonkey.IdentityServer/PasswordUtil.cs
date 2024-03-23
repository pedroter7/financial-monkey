using System.Security.Cryptography;
using System.Text;
using Duende.IdentityServer.Models;

namespace PedroTer7.FinancialMonkey.IdentityServer;

public static class PasswordUtil
{
    /// <summary>
    /// Salts and hashes a password using sha256.
    /// </summary>
    /// <param name="password"></param>
    /// <param name="salt"></param>
    /// <returns>a string with the following pattern: <salt>:<pwd_hash></returns>
    public static string HashPassword(string password, string salt)
    {
        var saltedPassword = $"{password}{salt}";
        return $"{salt}:{SHA256Sum(saltedPassword)}";
    }

    private static string SHA256Sum(string input)
    {
        byte[] data = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        StringBuilder sb = new();
        for (int i = 0; i < data.Length; i++)
        {
            sb.Append(data[i].ToString("x2"));
        }

        return sb.ToString();
    }

    public static string GenerateSalt()
    {
        const int length = 9;
        // Got this from https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
                            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    /// <summary>
    /// </summary>
    /// <param name="password">a password hashed by <see cref="PasswordUtil"/></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string GetSalt(string password)
    {
        var chunks = password.Split(":");
        if (chunks.Length != 2)
            throw new ArgumentException("Invalid password structure for the current config", nameof(password));

        return chunks[0];
    }
}
