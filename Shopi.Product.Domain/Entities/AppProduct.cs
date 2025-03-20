namespace Shopi.Product.Domain.Entities;

public class AppProduct
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Sku { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public string Manufacturer { get; set; }
    public bool Visible { get; set; } = true;

    public List<AppProductCategory>? ProductCategories { get; set; }
    public List<Review>? Reviews { get; set; }
}