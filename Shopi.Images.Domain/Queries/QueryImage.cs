namespace Shopi.Images.Domain.Queries;

public class QueryImage
{
    public string Id { get; set; }

    public QueryImage()
    {
    }

    public QueryImage(string id)
    {
        Id = id;
    }
}