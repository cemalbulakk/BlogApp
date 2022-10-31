using BlogApp.Application.Features.Dtos;
using BlogApp.Common.Dtos;
using BlogApp.Domain.Entities;
using BlogApp.Persistence.Repositories;

namespace BlogApp.Application.Services.Abstract;

public interface IRoleService : IAsyncRepository<Role>, IRepository<Role>
{
    Task<Response<CreateRoleDto>> CreateRole(CreateRoleDto createRoleDto);
    Task<Response<CreateUserRoleDto>> CreateUserRole(CreateUserRoleDto createUserRoleDto);
    Task<Response<CreateRoleGroupDto>> CreateRoleGroup(CreateRoleGroupDto createRoleGroupDto);

    public Task<Response<RoleModel>> GetRoleById(string userId, string roleGroupId, long bitwiseId);
    public Task<Response<List<RoleModel>>> GetRoleListByGroupId(string userId, string roleGroupId);
}