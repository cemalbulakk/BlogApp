namespace BlogApp.IndividualUI.Data.Entities;

public class Post : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string CategoryId { get; set; }
    public Category Category { get; set; }

    public ICollection<PostTag> PostTags { get; set; }
    public ICollection<PostPhoto> PostPhotos { get; set; }
}