using Shopi.Customer.API.Models;
using Shopi.Customer.API.Queries;

namespace Shopi.Customer.API.Interfaces;

public interface IAddressReadRepository
{
    Task<IEnumerable<Address?>> List(ListAddressesQuery query);
    Task<Address?> Get(GetAddressQuery query);
}