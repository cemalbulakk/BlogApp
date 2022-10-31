using BlogApp.Application.Features.Dtos;
using BlogApp.Common.Dtos;
using BlogApp.Domain.Entities;
using BlogApp.Persistence.Paging;
using BlogApp.Persistence.Repositories;

namespace BlogApp.Application.Services.Abstract;

public interface IPostService : IAsyncRepository<Post>, IRepository<Post>
{
    Task<Response<CreatePostDto>> CreatePost(CreatePostDto createPostDto);
    Task<Response<IPaginate<PostDto>>> GetPost(PageRequest request);
}