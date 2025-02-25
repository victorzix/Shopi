using Shopi.Customer.API.Models;
using Shopi.Customer.API.Queries;

namespace Shopi.Customer.API.Interfaces;

public interface ICustomerWriteRepository
{
    Task<AppCustomer> Create(AppCustomer appCustomerData);
    Task<AppCustomer?> Update(AppCustomer customerData);
    Task Delete(AppCustomer customer);
}