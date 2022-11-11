using BlogApp.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BlogApp.Application.Features.Dtos;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using BlogApp.Common.Dtos;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using BlogApp.Application.Services.Abstract;

namespace BlogApp.UI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController()
        {
        }


        [AllowAnonymous]
        public async Task<IActionResult> Index(int? pageNumber)
        {
            pageNumber = pageNumber ?? 0;
            var model = new MvcResponse<PostDto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7203/api/Home/Posts?Index={pageNumber}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<MvcResponse<PostDto>>(apiResponse);
                }
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AuthDto authDto)
        {
            authDto.Email = "string";
            authDto.Password = "string";

            using (var httpClient = new HttpClient())
            {
                var serializeProduct = JsonConvert.SerializeObject(authDto);
                StringContent stringContent = new StringContent(serializeProduct, Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7203/api/Auth/Auth", stringContent))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<TokenResponse<TokenInfo>>(apiResponse);
                    var handler = new JwtSecurityTokenHandler();
                    var jwt = handler.ReadJwtToken(model.data?.Token);
                    var identity = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims, CookieAuthenticationDefaults.AuthenticationScheme));
                    await HttpContext.SignInAsync(identity);
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
    }
}