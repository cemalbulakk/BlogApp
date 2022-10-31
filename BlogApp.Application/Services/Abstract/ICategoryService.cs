using BlogApp.Application.Features.Dtos;
using BlogApp.Common.Dtos;
using BlogApp.Domain.Entities;
using BlogApp.Persistence.Paging;
using BlogApp.Persistence.Repositories;

namespace BlogApp.Application.Services.Abstract;

public interface ICategoryService : IAsyncRepository<Category>, IRepository<Category>
{
    Task<Response<CreateCategoryDto>> CreateCategory(CreateCategoryDto createCategoryDto);
    Task<Response<IPaginate<CategoryDto>>> GetCategories(PageRequest request);
}