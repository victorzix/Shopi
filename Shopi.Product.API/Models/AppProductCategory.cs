namespace Shopi.Product.API.Models;

public class AppProductCategory
{
    public Guid ProductId { get; set; }
    public AppProduct AppProduct { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}