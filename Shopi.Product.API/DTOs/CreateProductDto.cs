namespace Shopi.Product.API.DTOs;

public class CreateProductDto
{
    public string Name { get; set; }
    public string? Sku { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public string Manufacturer { get; set; }
    public List<Guid>? CategoriesIds { get; set; }
}