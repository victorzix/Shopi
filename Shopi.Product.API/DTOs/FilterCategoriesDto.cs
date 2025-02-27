namespace Shopi.Product.API.DTOs;

public class FilterCategoriesDto
{
    public string? Name { get; set; }
    public Guid? ParentId { get; set; }
    public bool Visible { get; set; } = true;
}