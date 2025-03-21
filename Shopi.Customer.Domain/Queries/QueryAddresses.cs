namespace Shopi.Customer.Domain.Queries;

public class QueryAddresses
{
    public Guid CustomerId { get; set; }

    public QueryAddresses(Guid customerId)
    {
        CustomerId = customerId;
    }
}