namespace BlogApp.Application.Features.Enums;

public class Enums
{
    public const string User = "599900c6-5380-4abf-9cf9-8a07cea12c11";
    public const string Role = "1A194D86-2FB7-4E51-B585-708194E8729E";
    
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