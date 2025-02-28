using Shopi.Admin.API.Models;

namespace Shopi.Admin.API.Interfaces;

public interface IAdminWriteRepository
{
    Task<AppAdmin> Create(AppAdmin appCustomerData);
    Task<AppAdmin?> Update(AppAdmin customerData);
    Task Delete(AppAdmin customer);
}