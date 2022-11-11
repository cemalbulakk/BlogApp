namespace BlogApp.Application.Features.Dtos;

public class PostDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreateDate { get; set; }

    public CategoryDto Category { get; set; }
    public List<PostPhotoDto> PostPhotos { get; set; }
    public List<PostTagDto> PostTags { get; set; }
}