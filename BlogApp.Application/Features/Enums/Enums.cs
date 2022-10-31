namespace BlogApp.Application.Features.Enums;

public class Enums
{
    public enum RoleGroup
    {
        User = 1,
        Role = 2
    }
    public enum UserRoles
    {
        CreateUser = 1,
        UpdateUser = 2,
        DeleteUser = 4,
        GetUsers = 8,
    }

    public enum RoleRoles
    {
        CreateRole = 1,
        CreateRoleGroup = 2,
        CreateUserRole = 4,
    }
}