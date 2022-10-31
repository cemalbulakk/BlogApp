namespace BlogApp.Application.Features.Dtos;

public class CreateUserDto
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public virtual string? UserName { get; set; }
    public virtual string? Email { get; set; }
    public string Password { get; set; }
    public virtual string? PhoneNumber { get; set; }
    public virtual bool TwoFactorEnabled { get; set; }
    public virtual bool LockoutEnabled { get; set; }
}