using BlogApp.Application.Features.Dtos;
using BlogApp.Application.Services.Abstract;
using BlogApp.Common.BaseController;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BlogApp.Common.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Auth([FromBody] AuthDto authDto)
        {
            var token = await _authService.Auth(authDto);
            var getPrincipalFromExpiredToken = _tokenService.GetPrincipalFromExpiredToken(token.Data.Token);
            await HttpContext.SignInAsync(getPrincipalFromExpiredToken);
            return CreateActionResult(token);
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return CreateActionResult(await _authService.RefreshToken(new RefreshTokenRequest { UserId = userId }));
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}
