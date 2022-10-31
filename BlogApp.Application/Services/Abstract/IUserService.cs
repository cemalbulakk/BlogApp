using BlogApp.Application.Features.Dtos;
using BlogApp.Common.Dtos;
using BlogApp.Domain.Entities;
using BlogApp.Persistence.Paging;
using BlogApp.Persistence.Repositories;

namespace BlogApp.Application.Services.Abstract;

public interface IUserService : IAsyncRepository<User>, IRepository<User>
{
    Task<Response<IPaginate<UserDto>>> GetUsers(PageRequest request);
}