namespace BlogApp.Application.Features.Dtos;

public class RoleModel
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string RoleGroupId { get; set; }
    public long BitwiseId { get; set; }
    public string GroupName { get; set; }
    public string RoleName { get; set; }
}