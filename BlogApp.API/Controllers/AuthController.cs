using BlogApp.Application.Features.Dtos;
using BlogApp.Application.Services.Abstract;
using BlogApp.Common.BaseController;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BlogApp.Common.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
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
        [AllowAnonymous]
        public async Task<IActionResult> Auth([FromBody] AuthDto authDto)
        {
            return CreateActionResult(await _authService.Auth(authDto));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {

            return CreateActionResult(await _authService.RefreshToken(refreshToken));
        }

        [HttpPost]
        public IActionResult LogOut()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null) _authService.Revoke(userId);
            return CreateActionResult(Response<NoContent>.Success(200));
        }
    }
}
