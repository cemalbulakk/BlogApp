using AutoMapper;
using BlogApp.Application.Features.Dtos;
using BlogApp.Application.Services.Abstract;
using BlogApp.Common.Dtos;
using BlogApp.Common.Helpers;
using BlogApp.Domain.Contexts;
using BlogApp.Domain.Entities;
using BlogApp.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BlogApp.Infrastructure.Services.Concrete;

public class RoleService : EfRepositoryBase<Role, AppDbContext>, IRoleService
{
    private readonly IMapper _mapper;

    public RoleService(AppDbContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
    }

    public async Task<Response<CreateRoleDto>> CreateRole(CreateRoleDto createRoleDto)
    {
        try
        {
            var roles = base.GetAllAsync(x => x.RoleGroupId != null && x.RoleGroupId.Equals(createRoleDto.RoleGroupId));
            if (!roles.Any())
            {
                var firstRoleInGroup = new Role()
                {
                    BitwiseId = 1,
                    RoleGroupId = createRoleDto.RoleGroupId,
                    RoleName = createRoleDto.RoleName
                };

                var firstRoleInGroupResponse = await base.AddAsync(firstRoleInGroup);
                return !String.IsNullOrEmpty(firstRoleInGroupResponse.Id)
                    ? Response<CreateRoleDto>.Success(_mapper.Map<CreateRoleDto>(firstRoleInGroupResponse), 200)
                    : Response<CreateRoleDto>.Fail("not created.", 400);
            }

            var lastRoleInGroup = await roles.OrderBy(x => x.BitwiseId).LastAsync();
            var newRole = new Role()
            {
                BitwiseId = (lastRoleInGroup?.BitwiseId ?? 1) * 2,
                RoleGroupId = createRoleDto.RoleGroupId,
                RoleName = createRoleDto.RoleName,
            };
            var response = await base.AddAsync(newRole);
            return !String.IsNullOrEmpty(response.Id)
                ? Response<CreateRoleDto>.Success(_mapper.Map<CreateRoleDto>(response), 200)
                : Response<CreateRoleDto>.Fail("not created.", 400);
        }
        catch (Exception e)
        {
            return Response<CreateRoleDto>.Fail(e.Message, 400);

        }
    }

    public async Task<Response<CreateUserRoleDto>> CreateUserRole(CreateUserRoleDto createUserRoleDto)
    {
        try
        {
            var role = await base.GetAsync(x => x.Id.Equals(createUserRoleDto.RoleId));
            if (role is null) Response<CreateUserRoleDto>.Fail("not found.", 400);

            var userRole = await Context.UserRoles.AsQueryable().AsNoTracking().FirstOrDefaultAsync(x =>
                x.RoleGroupId != null && x.UserId == createUserRoleDto.UserId && x.RoleGroupId.Equals(createUserRoleDto.RoleGroupId));

            if (userRole is not null)
            {
                if (role?.BitwiseId != (userRole.Roles & role?.BitwiseId))
                {
                    userRole.Roles += role?.BitwiseId;
                    Context.UserRoles.Update(userRole);
                    await Context.SaveChangesAsync();
                    return Response<CreateUserRoleDto>.Success(createUserRoleDto, 200);
                }
                else
                {
                    return Response<CreateUserRoleDto>.Fail("Authority already defined", 400);
                }
            }
            else
            {
                var newUserRole = new UserRole()
                {
                    UserId = createUserRoleDto.UserId,
                    RoleGroupId = createUserRoleDto.RoleGroupId,
                    Roles = role?.BitwiseId
                };
                await Context.UserRoles.AddAsync(newUserRole);
                await Context.SaveChangesAsync();
                return Response<CreateUserRoleDto>.Success(createUserRoleDto, 200);
            }
        }
        catch (Exception e)
        {
            return Response<CreateUserRoleDto>.Fail(e.Message, 400);

        }
    }

    public async Task<Response<CreateRoleGroupDto>> CreateRoleGroup(CreateRoleGroupDto createRoleGroupDto)
    {
        try
        {
            var roleGroup = _mapper.Map<RoleGroup>(createRoleGroupDto);
            await Context.RoleGroups.AddAsync(roleGroup);
            await Context.SaveChangesAsync();
            return Response<CreateRoleGroupDto>.Success(createRoleGroupDto, 200);
        }
        catch (Exception e)
        {
            return Response<CreateRoleGroupDto>.Fail(e.Message, 400);
        }
    }

    public async Task<Response<RoleModel>> GetRoleById(string userId, string roleGroupId, long bitwiseId)
    {
        var model = new RoleModel();
        var userRole = await Context.UserRoles.Include(x => x.RoleGroup)
            .FirstOrDefaultAsync(x => x.RoleGroupId != null && x.UserId != null && x.UserId.Equals(userId) && x.RoleGroupId.Equals(roleGroupId));

        if (userRole is null) return Response<RoleModel>.Fail("not found.", 400);

        if (bitwiseId != (userRole.Roles & bitwiseId)) return Response<RoleModel>.Fail("not found.", 400);
        {
            var role = await Context.Roles.FirstOrDefaultAsync(x => x.RoleGroupId != null && x.BitwiseId.Equals(bitwiseId) && x.RoleGroupId.Equals(roleGroupId));
            if (role == null) return Response<RoleModel>.Fail("not found.", 400);
            model = new RoleModel()
            {
                BitwiseId = (long)role.BitwiseId,
                RoleGroupId = role.RoleGroupId,
                GroupName = userRole.RoleGroup.GroupName,
                UserId = userRole.UserId,
                RoleName = role.RoleName,
                Id = role.Id
            };
            return Response<RoleModel>.Success(model, 200);
        }

    }

    public async Task<Response<List<RoleModel>>> GetRoleListByGroupId(string userId, string roleGroupId)
    {
        var model = new List<RoleModel>();
        var userRole =
            await Context.UserRoles.FirstOrDefaultAsync(x =>
                x.UserId != null && x.RoleGroupId != null && x.RoleGroupId.Equals(roleGroupId) && x.UserId.Equals(userId));

        if (userRole is not null)
        {
            var allRoles = await Context.Roles.Include(x => x.RoleGroup)
                .Where(x => x.RoleGroupId != null && x.RoleGroupId.Equals(roleGroupId)).ToListAsync();

            allRoles.ForEach(x =>
            {
                if (x.BitwiseId == (userRole.Roles & x.BitwiseId))
                {
                    model.Add(new RoleModel()
                    {
                        BitwiseId = (long)x.BitwiseId,
                        RoleGroupId = x.RoleGroupId,
                        GroupName = userRole.RoleGroup.GroupName,
                        UserId = userRole.UserId,
                        RoleName = x.RoleName,
                        Id = x.Id
                    });
                }
            });

            return Response<List<RoleModel>>.Success(model, 200);
        }
        else
        {
            return Response<List<RoleModel>>.Fail("not found.", 400);
        }

    }
}