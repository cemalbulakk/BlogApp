namespace BlogApp.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }

    public ICollection<Post> Posts { get; set; }
}