namespace BlogApp.Application.Features.Dtos;

public class CreatePostDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string CategoryId { get; set; }

    public List<CreatePostPhotoDto> PostPhotos { get; set; }
    public List<CreatePostTagDto> PostTags { get; set; }
}