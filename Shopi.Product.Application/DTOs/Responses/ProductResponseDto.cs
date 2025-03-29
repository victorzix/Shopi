namespace Shopi.Product.Application.DTOs.Responses;

public class ProductResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Sku { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public string Manufacturer { get; set; }
    public bool Visible { get; set; }
    public bool IsActive { get; set; }
}