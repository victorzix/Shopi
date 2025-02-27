namespace Shopi.Product.API.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? ParentId { get; set; }
    public string? Description { get; set; }
    public bool Visible { get; set; } = true;

    public List<AppProductCategory>? AppProductCategories { get; set; }
}