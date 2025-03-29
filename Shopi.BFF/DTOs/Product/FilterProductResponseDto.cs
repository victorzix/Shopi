namespace Shopi.BFF.DTOs.Product;

public class FilterProductResponseDto
{
    public IReadOnlyCollection<Product>? Products { get; set; }
    public int Total { get; set; }
}