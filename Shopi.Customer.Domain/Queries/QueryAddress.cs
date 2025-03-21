namespace Shopi.Customer.Domain.Queries;

public class QueryAddress
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }

    public QueryAddress(Guid id, Guid customerId)
    {
        Id = id;
        CustomerId = customerId;
    }
}