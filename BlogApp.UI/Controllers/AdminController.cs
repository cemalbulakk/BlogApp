using BlogApp.Application.Features.Dtos;
using BlogApp.Application.Services.Abstract;
using BlogApp.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlogApp.UI.Controllers
{
    public class AdminController : Controller
    {
        public AdminController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PostList([FromQuery] PageRequest request)
        {
            var model = new MvcResponse<PostDto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7203/api/Home/Posts?Index={request.Index}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<MvcResponse<PostDto>>(apiResponse);
                }
            }

            //var posts = await _postService.GetPost(request);
            return View(model);
        }
    }
}
