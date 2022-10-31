using BlogApp.Common.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class HomeController : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> Users()
    {
        return Ok("aasd");
    }
}