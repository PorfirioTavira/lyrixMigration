using System.Security.Cryptography;
using System.Text;
namespace Backend.Spotfiy;

class PkceHelper
{
    private string CodeVerifier = GenerateRandString(60);
    public static string GenerateRandString(int length)
    {
        const string Possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        byte[] bytes = RandomNumberGenerator.GetBytes(length);

        var sb = new StringBuilder(length);

        foreach (byte b in bytes)
        {
            sb.Append(Possible[b % Possible.Length]);
        }
        return sb.ToString();
    }

}

