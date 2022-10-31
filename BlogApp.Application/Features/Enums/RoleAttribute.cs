namespace BlogApp.Application.Features.Enums;

[AttributeUsage(AttributeTargets.All)]
public class RoleAttribute : Attribute
{
    public int RoleGroupId { get; }
    public Int64 BitwiseId { get; }

    public RoleAttribute(int roleGroupId, Int64 bitwiseId)
    {
        RoleGroupId = roleGroupId;
        BitwiseId = BitwiseId;
    }

}