namespace BlogApp.Application.Features.Enums;

[AttributeUsage(AttributeTargets.All)]
public class RoleAttribute : Attribute
{
    public string RoleGroupId { get; }
    public Int64 BitwiseId { get; }

    public RoleAttribute(string roleGroupId, Int64 bitwiseId)
    {
        RoleGroupId = roleGroupId;
        BitwiseId = BitwiseId;
    }

}