using BlogApp.API.Services;
using BlogApp.Application.Features.Dtos;
using BlogApp.Application.Features.Enums;
using BlogApp.Application.Services.Abstract;
using BlogApp.Common.BaseController;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[ServiceFilter(typeof(PermissionFilter))]
public class UserController : CustomBaseController
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    public UserController(IUserService userService, IRoleService roleService)
    {
        _userService = userService;
        _roleService = roleService;
    }

    [HttpGet]
    [Role((int)Enums.RoleGroup.User, (long)Enums.UserRoles.GetUsers)]
    public async Task<IActionResult> GetUsers([FromQuery] PageRequest pageRequest)
    {
        var users = await _userService.GetUsers(pageRequest);
        return CreateActionResult(users);
    }

    [HttpPost]
    [Role((int)Enums.RoleGroup.User, (long)Enums.UserRoles.CreateUser)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        return CreateActionResult(await _userService.CreateUser(createUserDto));
    }

    [HttpPost]
    [Role((int)Enums.RoleGroup.Role, (long)Enums.RoleRoles.CreateRole)]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto createRoleDto)
    {
        return CreateActionResult(await _roleService.CreateRole(createRoleDto));
    }

    [HttpPost]
    [Role((int)Enums.RoleGroup.Role, (long)Enums.RoleRoles.CreateRoleGroup)]
    public async Task<IActionResult> CreateRoleGroup([FromBody] CreateRoleGroupDto createRoleGroupDto)
    {
        return CreateActionResult(await _roleService.CreateRoleGroup(createRoleGroupDto));
    }

    [HttpPost]
    [Role((int)Enums.RoleGroup.Role, (long)Enums.RoleRoles.CreateUserRole)]
    public async Task<IActionResult> CreateUserRole([FromBody] CreateUserRoleDto createUserRoleDto)
    {
        return CreateActionResult(await _roleService.CreateUserRole(createUserRoleDto));
    }
}