using System.Security.Cryptography;
using System.Text;
namespace Backend.Spotfiy;

class PkceHelper
{
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

    public static byte[] Sha256(string input)
    {
        using SHA256 sha = SHA256.Create();
        return sha.ComputeHash(Encoding.ASCII.GetBytes(input));
    }

    public static string Base64Encode(byte[] bytes)
    {
        string encoded = Convert.ToBase64String(bytes);
        encoded = encoded.TrimEnd('=')
                         .Replace('+', '-')
                         .Replace('/', '_');
        return encoded;
    }

}

