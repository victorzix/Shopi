namespace Shopi.Product.API.Models;

public class ReviewResponses
{
    public Guid Id { get; set; }
    public Guid ReviewId { get; set; }

    public Review Review { get; set; }
    public Guid ParentId { get; set; }

    public string Comment { get; set; }
    public DateTime PostingDate { get; set; }
    public bool Visible { get; set; } = true;
}