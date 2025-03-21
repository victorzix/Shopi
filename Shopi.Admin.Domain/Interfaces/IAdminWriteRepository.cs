using Shopi.Admin.Domain.Entities;

namespace Shopi.Admin.Domain.Interfaces;

public interface IAdminWriteRepository
{
    Task<AppAdmin> Create(AppAdmin appCustomerData);
    Task<AppAdmin?> Update(AppAdmin customerData);
    Task Delete(AppAdmin customer);
}