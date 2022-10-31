using AutoMapper;
using BlogApp.Application.Features.Dtos;
using BlogApp.Application.Services.Abstract;
using BlogApp.Common.Dtos;
using BlogApp.Domain.Contexts;
using BlogApp.Domain.Entities;
using BlogApp.Persistence.Paging;
using BlogApp.Persistence.Repositories;

namespace BlogApp.Infrastructure.Services.Concrete;

public class CategoryService : EfRepositoryBase<Category, AppDbContext>, ICategoryService
{
    private readonly IMapper _mapper;

    public CategoryService(AppDbContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
    }

    public async Task<Response<CreateCategoryDto>> CreateCategory(CreateCategoryDto createCategoryDto)
    {
        try
        {
            var category = _mapper.Map<Category>(createCategoryDto);
            var response = await base.AddAsync(category);
            return !String.IsNullOrWhiteSpace(response.Id)
                ? Response<CreateCategoryDto>.Success(_mapper.Map<CreateCategoryDto>(response), 200)
                : Response<CreateCategoryDto>.Fail(400);
        }
        catch (Exception e)
        {
            return Response<CreateCategoryDto>.Fail(400);
        }
    }

    public async Task<Response<IPaginate<CategoryDto>>> GetCategories(PageRequest request)
    {
        try
        {
            var categories = await base.GetAllAsync().Select(x => _mapper.Map<CategoryDto>(x)).ToPaginateAsync(request.Index, request.Size);
            return categories.Items.Any()
                ? Response<IPaginate<CategoryDto>>.Success(categories, 200)
                : Response<IPaginate<CategoryDto>>.Fail(400);
        }
        catch (Exception e)
        {
            return Response<IPaginate<CategoryDto>>.Fail(400);
        }
    }
}