using Shopi.Customer.Domain.Entities;
using Shopi.Customer.Domain.Queries;

namespace Shopi.Customer.Domain.Interfaces;

public interface IAddressReadRepository
{
    Task<IEnumerable<Address?>> List(QueryAddresses query);
    Task<Address?> Get(QueryAddress query);
}