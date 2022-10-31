using AutoMapper;
using BlogApp.Application.Features.Dtos;
using BlogApp.Domain.Entities;

namespace BlogApp.Application.Mapping;

public class CustomMapping : Profile
{
    public CustomMapping()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<CreateUserDto, User>().ReverseMap();

        CreateMap<CreateRoleDto, Role>().ReverseMap();
        CreateMap<CreateRoleGroupDto, RoleGroup>().ReverseMap();
    }
}