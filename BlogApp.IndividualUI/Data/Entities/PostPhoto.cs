namespace BlogApp.IndividualUI.Data.Entities;

public class PostPhoto : BaseEntity
{
    public string PostId { get; set; }
    public Post Post { get; set; }

    public string Url { get; set; }
}