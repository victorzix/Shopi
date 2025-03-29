namespace Shopi.BFF.DTOs.Product;

public class UpdateProductDto
{
    public string? Name { get; set; }
    public string? Sku { get; set; }
    public int? Price { get; set; }
    public int? Quantity { get; set; }
    public string? Description { get; set; }
    public string? Manufacturer { get; set; }
}