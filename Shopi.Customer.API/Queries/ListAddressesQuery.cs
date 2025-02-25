using MediatR;
using Shopi.Core.Utils;
using Shopi.Customer.API.Models;

namespace Shopi.Customer.API.Queries;

public class ListAddressesQuery : IRequest<ApiResponses<IEnumerable<Address?>>>
{
    public Guid CustomerId { get; set; }

    public ListAddressesQuery(Guid customerId)
    {
        CustomerId = customerId;
    }
}