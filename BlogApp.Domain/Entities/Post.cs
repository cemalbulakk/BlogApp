﻿namespace BlogApp.Domain.Entities;

public class Post : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }

    public ICollection<PostTag> PostTags { get; set; }
    public ICollection<PostPhoto> PostPhotos { get; set; }
}