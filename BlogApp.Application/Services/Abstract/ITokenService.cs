using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlogApp.Application.Services.Abstract;

public interface ITokenService
{
    JwtSecurityToken CreateToken(IEnumerable<Claim> authClaims);
    String GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(String token);
}