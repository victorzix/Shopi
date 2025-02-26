using Shopi.Customer.API.Models;

namespace Shopi.Customer.API.Interfaces;

public interface IAddressWriteRepository
{
    Task<Address> Create(Address address);
    Task<Address?> Update(Address address);
    Task Delete(Address address);
}