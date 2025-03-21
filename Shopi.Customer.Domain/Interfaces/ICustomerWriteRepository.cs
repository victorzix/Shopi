using Shopi.Customer.Domain.Entities;
using Shopi.Customer.Domain.Queries;

namespace Shopi.Customer.Domain.Interfaces;

public interface ICustomerWriteRepository
{
    Task<AppCustomer> Create(AppCustomer appCustomerData);
    Task<AppCustomer?> Update(AppCustomer customerData);
    Task Delete(AppCustomer customer);
}