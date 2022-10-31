using AutoMapper;
using BlogApp.Application.Features.Dtos;
using BlogApp.Application.Services.Abstract;
using BlogApp.Common.BaseController;
using BlogApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BlogController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IPostService _postService;
            public BlogController(ICategoryService categoryService, IPostService postService)
            {
                _categoryService = categoryService;
                _postService = postService;
            }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            return CreateActionResult(await _categoryService.CreateCategory(createCategoryDto));
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] PageRequest request)
        {
            return CreateActionResult(await _categoryService.GetCategories(request));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostDto createPostDto)
        {
            return CreateActionResult(await _postService.CreatePost(createPostDto));
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts([FromQuery] PageRequest request)
        {
            return CreateActionResult(await _postService.GetPost(request));
        }
    }
}
