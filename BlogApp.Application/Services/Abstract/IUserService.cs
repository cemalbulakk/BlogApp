using BlogApp.Domain.Entities;
using BlogApp.Persistence.Repositories;

namespace BlogApp.Application.Services.Abstract;

public interface IUserService : IAsyncRepository<User>, IRepository<User>
{
    
}