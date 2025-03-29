namespace Shopi.Product.Domain.Entities;

public class AppProductCategory
{
    public Guid ProductId { get; set; }
    public AppProduct AppProduct { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}