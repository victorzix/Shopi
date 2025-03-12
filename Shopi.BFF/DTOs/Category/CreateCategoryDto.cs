namespace Shopi.BFF.DTOs.Category;

public class CreateCategoryDto
{
    public string Name { get; set; } 
    public Guid? ParentId { get; set; }
    public string? Description { get; set; }
}