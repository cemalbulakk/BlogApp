using Microsoft.AspNetCore.Identity;

namespace BlogApp.Application.Features.Dtos;

public class UserDto
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public virtual string UserName { get; set; }
    public virtual string Email { get; set; }
    public virtual bool EmailConfirmed { get; set; }
    public virtual string PhoneNumber { get; set; }
    public virtual bool PhoneNumberConfirmed { get; set; }
    public virtual bool TwoFactorEnabled { get; set; }
    public virtual bool LockoutEnabled { get; set; }
    public virtual int AccessFailedCount { get; set; }
}