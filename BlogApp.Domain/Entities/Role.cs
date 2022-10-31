namespace BlogApp.Domain.Entities;

public class Role : BaseEntity
{
    public long? BitwiseId { get; set; }
    public string RoleName { get; set; }

    public string? RoleGroupId { get; set; }
    public RoleGroup RoleGroup { get; set; }
}