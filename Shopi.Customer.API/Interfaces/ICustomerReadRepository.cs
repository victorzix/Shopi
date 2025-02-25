using Shopi.Customer.API.Models;
using Shopi.Customer.API.Queries;

namespace Shopi.Customer.API.Interfaces;

public interface ICustomerReadRepository
{
    Task<AppCustomer?> GetByEmailOrDocument(GetByEmailOrDocumentQuery query);
    Task<AppCustomer?> GetById(Guid id);
}