using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using BlogApp.Application.Features.Dtos;
using BlogApp.Application.Services.Abstract;
using BlogApp.Common.Dtos;
using BlogApp.Domain.Contexts;
using BlogApp.Domain.Entities;
using BlogApp.Persistence.Paging;
using BlogApp.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Infrastructure.Services.Concrete;

public class PostService : EfRepositoryBase<Post, AppDbContext>, IPostService
{
    private readonly IMapper _mapper;

    public PostService(AppDbContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
    }

    public async Task<Response<CreatePostDto>> CreatePost(CreatePostDto createPostDto)
    {
        try
        {
            var map = _mapper.Map<Post>(createPostDto);
            await Context.Posts.AddAsync(map);
            await Context.SaveChangesAsync();
            return Response<CreatePostDto>.Success(_mapper.Map<CreatePostDto>(map), 200);
        }
        catch (Exception e)
        {
            return Response<CreatePostDto>.Fail(e.Message, 400);
        }
    }

    public async Task<Response<IPaginate<PostDto>>> GetPost(PageRequest request)
    {
        try
        {
            var posts = await base.GetAllAsync(include: x => x.Include(x1 => x1.Category)
                    .Include(x2 => x2.PostPhotos)
                    .Include(x3 => x3.PostTags))
                .Select(x => _mapper.Map<PostDto>(x))
                .ToPaginateAsync(request.Index,
                    request.Size);

            return posts.Items.Any()
                ? Response<IPaginate<PostDto>>.Success(posts, 200)
                : Response<IPaginate<PostDto>>.Fail(400);
        }
        catch (Exception e)
        {
            return Response<IPaginate<PostDto>>.Fail(e.Message, 400);
        }
    }
}