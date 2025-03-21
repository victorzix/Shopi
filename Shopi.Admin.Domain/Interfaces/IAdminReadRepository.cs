using Shopi.Admin.Domain.Entities;
using Shopi.Admin.Domain.Queries;

namespace Shopi.Admin.Domain.Interfaces;

public interface IAdminReadRepository
{
    Task<AppAdmin?> FilterAdmin(QueryAdmin query);
}