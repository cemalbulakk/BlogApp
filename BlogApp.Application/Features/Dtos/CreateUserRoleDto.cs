namespace BlogApp.Application.Features.Dtos;

public class CreateUserRoleDto
{
    public string? UserId { get; set; }
    public string? RoleGroupId { get; set; }
    public string? RoleId { get; set; }
}