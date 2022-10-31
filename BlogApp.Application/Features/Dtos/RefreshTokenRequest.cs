namespace BlogApp.Application.Features.Dtos;

public class RefreshTokenRequest
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public string? UserId { get; set; }
}