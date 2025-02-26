using MediatR;
using Shopi.Core.Utils;
using Shopi.Customer.API.Models;

namespace Shopi.Customer.API.Queries;

public class GetAddressQuery : IRequest<ApiResponses<Address>>
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }

    public GetAddressQuery(Guid id, Guid customerId)
    {
        Id = id;
        CustomerId = customerId;
    }
}