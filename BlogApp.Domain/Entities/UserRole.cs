namespace BlogApp.Domain.Entities;

public class UserRole : BaseEntity
{
    public string? UserId { get; set; }
    public string? RoleGroupId { get; set; }
    public long? Roles { get; set; }

    public virtual RoleGroup RoleGroup { get; set; }
    public virtual User User { get; set; }
}