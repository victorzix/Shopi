using Shopi.Customer.Domain.Entities;

namespace Shopi.Customer.Domain.Interfaces;

public interface IAddressWriteRepository
{
    Task<Address> Create(Address address);
    Task<Address?> Update(Address address);
    Task Delete(Address address);
}