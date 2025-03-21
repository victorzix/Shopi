using Shopi.Customer.Domain.Entities;
using Shopi.Customer.Domain.Queries;

namespace Shopi.Customer.Domain.Interfaces;

public interface ICustomerReadRepository
{
    Task<AppCustomer?> FilterClient(QueryCustomer query);
}