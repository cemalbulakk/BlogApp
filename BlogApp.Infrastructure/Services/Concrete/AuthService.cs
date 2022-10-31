using System.IdentityModel.Tokens.Jwt;
using BlogApp.Application.Features.Dtos;
using BlogApp.Application.Services.Abstract;
using BlogApp.Common.Dtos;
using BlogApp.Common.Helpers;
using BlogApp.Domain.Contexts;
using BlogApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace BlogApp.Infrastructure.Services.Concrete;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context, ITokenService tokenService, IConfiguration configuration)
    {
        _context = context;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    public async Task<Response<TokenInfo>> Auth(AuthDto authDto)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(authDto.Email));
            if (user is null) return Response<TokenInfo>.Fail("email or password wrong.", 400);

            var passwordHash = Helper.HashSha256(authDto.Password);
            if (!user.PasswordHash.Equals(passwordHash)) return Response<TokenInfo>.Fail("email or password wrong.", 400);

            await Revoke(user.Id);

            var authClaims = await AuthClaims(user);
            var token = _tokenService.CreateToken(authClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            await _context.UserTokens.AddAsync(new UserToken()
            {
                UserId = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                ExpireDate = token.ValidTo
            });
            await _context.SaveChangesAsync();

            var model = new TokenInfo()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                ExpireDate = token.ValidTo
            };

            return Response<TokenInfo>.Success(model, 200);
        }
        catch (Exception e)
        {
            return Response<TokenInfo>.Fail(e.Message, 400);
        }

    }

    public async Task<Response<TokenInfo>> RefreshToken(RefreshTokenRequest refreshTokenRequest)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(refreshTokenRequest.UserId));

            if (user == null) return Response<TokenInfo>.Fail("not found.", 400);
            await Revoke(user.Id);
            var newToken = _tokenService.CreateToken(await AuthClaims(user));
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            await _context.UserTokens.AddAsync(new UserToken()
            {
                UserId = user.Id,
                RefreshToken = newRefreshToken,
                Token = new JwtSecurityTokenHandler().WriteToken(newToken),
                ExpireDate = newToken.ValidTo
            });
            await _context.SaveChangesAsync();

            var tokenDto = new TokenInfo
            {
                Token = new JwtSecurityTokenHandler().WriteToken(newToken),
                RefreshToken = newRefreshToken,
                ExpireDate = newToken.ValidTo
            };

            return Response<TokenInfo>.Success(tokenDto, 200);

        }
        catch (Exception e)
        {
            return Response<TokenInfo>.Fail(e.Message, 400);
        }
    }

    public Task Revoke(string userId)
    {
        var userTokens = _context.UserTokens.Where(x => x.UserId.Equals(userId));
        _context.RemoveRange(userTokens);
        _context.SaveChangesAsync();
        return Task.CompletedTask;
    }

    private async Task<List<Claim>> AuthClaims(User user)
    {
        #region Find User Roles

        var userRoles = await _context.UserRoles
            .Include(x => x.RoleGroup)
            .ThenInclude(x => x.Roles)
            .Where(x => x.UserId != null && x.UserId.Equals(user.Id)).ToListAsync(); // Find user all roles

        #endregion


        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecurityKey"] ?? string.Empty);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new List<Claim>()),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var authClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user?.Id ?? string.Empty),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

        userRoles.ForEach(userRole =>
        {
            authClaims.AddRange(userRole.RoleGroup.Roles.Select(role => new Claim(ClaimTypes.Role, role.RoleName)));
        });

        tokenDescriptor.Subject.AddClaims(authClaims);
        return authClaims;
    }
}