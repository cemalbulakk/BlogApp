namespace BlogApp.Common.Dtos;

public class TokenInfo
{
    public string Token { get; set; }
    public DateTime ExpireDate { get; set; }
    public string RefreshToken { get; set; }
}