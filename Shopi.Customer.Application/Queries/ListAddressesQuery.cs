using MediatR;
using Shopi.Core.Utils;
using Shopi.Customer.Domain.Entities;

namespace Shopi.Customer.Application.Queries;

public class ListAddressesQuery : IRequest<ApiResponses<IEnumerable<Address?>>>
{
    public Guid CustomerId { get; set; }

    public ListAddressesQuery(Guid customerId)
    {
        CustomerId = customerId;
    }
}