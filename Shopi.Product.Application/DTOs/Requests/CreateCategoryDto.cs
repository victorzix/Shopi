namespace Shopi.Product.Application.DTOs.Requests;

public class CreateCategoryDto
{
    public string Name { get; set; } 
    public Guid? ParentId { get; set; }
    public string? Description { get; set; }
}