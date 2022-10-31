using Microsoft.AspNetCore.Identity;

namespace BlogApp.Domain.Entities;

public class User : IdentityUser
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
}