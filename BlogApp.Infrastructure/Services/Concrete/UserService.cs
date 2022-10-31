using AutoMapper;
using BlogApp.Application.Features.Dtos;
using BlogApp.Application.Services.Abstract;
using BlogApp.Common.Dtos;
using BlogApp.Domain.Contexts;
using BlogApp.Domain.Entities;
using BlogApp.Persistence.Paging;
using BlogApp.Persistence.Repositories;
using System.Drawing;
using BlogApp.Common.Helpers;

namespace BlogApp.Infrastructure.Services.Concrete;

public class UserService : EfRepositoryBase<User, AppDbContext>, IUserService
{
    private readonly IMapper _mapper;
    public UserService(AppDbContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
    }

    public async Task<Response<IPaginate<UserDto>>> GetUsers(PageRequest request)
    {
        try
        {
            var response = await base.GetAllAsync().Select(x => _mapper.Map<UserDto>(x)).ToPaginateAsync(request.Index, request.Size);
            return Response<IPaginate<UserDto>>.Success(response, 200);
        }
        catch (Exception e)
        {
            return Response<IPaginate<UserDto>>.Fail(e.Message, 400);
        }
    }

    public async Task<Response<UserDto>> CreateUser(CreateUserDto createUserDto)
    {
        try
        {
            var user = _mapper.Map<User>(createUserDto);
            var passwordHash = Helper.HashSha256(createUserDto.Password);
            user.PasswordHash = passwordHash;
            var response = await base.AddAsync(user);
            return !String.IsNullOrEmpty(response.Id)
                ? Response<UserDto>.Success(_mapper.Map<UserDto>(response), 200)
                : Response<UserDto>.Fail("User not created.", 400);
        }
        catch (Exception e)
        {
            return Response<UserDto>.Fail(e.Message, 400);
        }
    }
}