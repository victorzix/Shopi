namespace Shopi.Images.Domain.Queries;

public class QueryImages
{
    public Guid ProductId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int Limit { get; set; } = 3;

    public QueryImages()
    {
    }

    public QueryImages(Guid productId, int pageNumber, int limit)
    {
        ProductId = productId;
        PageNumber = pageNumber;
        Limit = limit;
    }
}