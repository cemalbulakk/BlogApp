namespace BlogApp.Domain.Entities;

public class RoleGroup : BaseEntity
{
    public string GroupName { get; set; }

    public virtual ICollection<Role> Roles { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }
}