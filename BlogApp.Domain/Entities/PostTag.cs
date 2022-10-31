namespace BlogApp.Domain.Entities;

public class PostTag : BaseEntity
{
    public string PostId { get; set; }
    public Post Post { get; set; }

    public string Tag { get; set; }
}