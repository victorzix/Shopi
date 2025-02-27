namespace Shopi.Product.API.DTOs;

public class CreateCategoryDto
{
    public string Name { get; set; } 
    public Guid? ParentId { get; set; }
    public string? Description { get; set; }
}