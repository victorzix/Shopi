namespace Shopi.Customer.Domain.Queries;

public class QueryCustomer
{
    public string? Email { get; set; }
    public string? Document { get; set; }
    public Guid? Id { get; set; }

    public QueryCustomer() { }

    public QueryCustomer(string? email, string? document, Guid? id)
    {
        Email = email;
        Document = document;
        Id = id;
    }
}