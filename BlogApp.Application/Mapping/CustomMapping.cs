using AutoMapper;
using BlogApp.Application.Features.Dtos;
using BlogApp.Application.Services.Abstract;
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

        CreateMap<CreateCategoryDto, Category>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();

        CreateMap<CreatePostDto, Post>().ReverseMap();
        CreateMap<CreatePostTagDto, PostTag>().ReverseMap();
        CreateMap<CreatePostPhotoDto, PostPhoto>().ReverseMap();

        CreateMap<Post, PostDto>().ReverseMap();
        CreateMap<PostPhoto, PostPhotoDto>().ReverseMap();
        CreateMap<PostTag, PostTagDto>().ReverseMap();
    }
}