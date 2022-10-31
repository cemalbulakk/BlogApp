namespace BlogApp.Domain.Entities;

public class UserToken : BaseEntity
{
    public string UserId { get; set; }
    public User User { get; set; }

    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpireDate { get; set; }
}