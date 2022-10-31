using System.Security.Cryptography;
using System.Text;

namespace BlogApp.Common.Helpers;

public static class Helper
{
    public static string HashSha256(String text)
    {
        return Convert.ToHexString(SHA256.HashData(Encoding.ASCII.GetBytes(text)));
    }
}