namespace BlogApp.IndividualUI.Data.Entities;

public class BaseEntity
{
    public BaseEntity()
    {
        Id = Guid.NewGuid().ToString("D");
    }
    public string Id { get; set; }
    public DateTime CreateDate { get; set; }
    public string CreateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string? UpdateBy { get; set; }
    public bool IsActive { get; set; }
}