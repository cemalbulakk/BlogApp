using BlogApp.Application.Features.Dtos;
using BlogApp.Common.Dtos;

namespace BlogApp.Application.Services.Abstract;

public interface IAuthService
{
    Task<Response<TokenInfo>> Auth(AuthDto authDto);
    Task<Response<TokenInfo>> RefreshToken(string refreshToken);
    void Revoke(string userId);
}